// TOOLS

//#tool "nuget:?package=NuGet.CommandLine"
#tool "nuget:?package=NUnit.Runners&version=2.6.4"
#tool "nuget:?package=JetBrains.dotCover.CommandLineTools"

// VARIABLES

string solutionName = "Alternatives";
string solutionFullName = $"{solutionName}.sln";
string BuildConfig = "Release";
string NugetPackageOutputDirectory = "./NugetPackages";
string NugetSourceUrl = "https://www.nuget.org/api/v2/package";

// STAGE NAMES

string DefaultStage = "RESULT";
string NugetPushStage = "Nuget Push";
string NugetPackStage = "Nuget Pack";
string TestStage = "Test";
string AnalysisStage = "Analysis";
string BuildStage = "Build";
string CleanStage = "Clean";
string NugetRestoreStage = "Nuget Restore";

// RUN OPERATION

var NugetApiKey = Argument("NugetApiKey", "");
var target = Argument("target", DefaultStage);

Task(DefaultStage)
.IsDependentOn(TestStage)
.IsDependentOn(AnalysisStage)
.IsDependentOn(NugetPushStage)
.Does(() =>{});


Task(NugetPushStage)
.IsDependentOn(NugetPackStage)
.ContinueOnError()
.Does(()=> 
{
    var npkgFiles = GetFiles(NugetPackageOutputDirectory + "/*.nupkg");
    foreach(var nupkgFile in npkgFiles)
    {
        var nugetPushSettings = new NuGetPushSettings
        {
            ApiKey = NugetApiKey,
            Source = NugetSourceUrl 
        };
        
        NuGetPush(nupkgFile.FullPath, nugetPushSettings);        
    }
});

Task(NugetPackStage)
.IsDependentOn(BuildStage)
.Does(()=> 
{
    var nuspecFiles = GetFiles("./**/*.nuspec");
    foreach(var nuspecFile in nuspecFiles)
    {
        Console.WriteLine(nuspecFile);
        var nuGetPackSettings = new NuGetPackSettings
                                    {
                                        Id                      = "Alternatives",
                                        Title                   = "Alternatives",
                                        Authors                 = new[] {"Adem Catamak"},
                                        Owners                  = new[] {"Adem Catamak"},
                                        Description             = "Common Extensions",
                                        ProjectUrl              = new Uri("https://github.com/AdemCatamak/Alternatives.git"),
                                        LicenseUrl              = new Uri("https://github.com/AdemCatamak/Alternatives/blob/master/LICENSE"),
                                        Tags                    = new [] {"C#", "Extensions"},
                                        RequireLicenseAcceptance= true,
                                        Symbols                 = true,
                                        OutputDirectory         = NugetPackageOutputDirectory
                                    };
                                    
        NuGetPack(nuspecFile, nuGetPackSettings);
    }
});

Task(AnalysisStage)
.IsDependentOn(BuildStage)
.Does(()=>
{
    DotCoverAnalyse(tool => 
    {
        tool.NUnit("./3_Tests/**/bin/**/*Test.dll");
    },
    new FilePath("./AnalysisResult.xml"),
    new DotCoverAnalyseSettings());
});


Task(TestStage)
.IsDependentOn(BuildStage)
.Does(()=>
{
    var testDlls = GetFiles("./3_Tests/**/bin/**/*Test.dll");
    bool success = true;
    foreach(var testDll in testDlls)
    {
        Console.WriteLine(testDll.FullPath);
        try
        {
            NUnit(testDll.FullPath);
        }
        catch(Exception e)
        {
            success = false;
            Console.WriteLine(e);
        }
    }

    if(!success)
    {
        throw new Exception(BuildStage + " FAIL");
    }
});

Task(BuildStage)
.IsDependentOn(NugetRestoreStage)
.Does(()=>
{
    MSBuild(solutionFullName, new MSBuildSettings
        {
            Verbosity = Verbosity.Minimal,
            ToolVersion = MSBuildToolVersion.VS2017,
            Configuration = BuildConfig,
            PlatformTarget = PlatformTarget.MSIL
        });
});

Task(NugetRestoreStage)
.IsDependentOn(CleanStage)
.Does(()=>
{
    NuGetRestore(solutionFullName, new NuGetRestoreSettings
    {
        NoCache = true,
        Verbosity = NuGetVerbosity.Detailed,
        PackagesDirectory = "./packages/"
    });
});


Task(CleanStage)
.Does(()=>
{
    var objDirectories = GetDirectories("./**/obj/*");

    foreach(var directory in objDirectories)
    {
        Console.WriteLine(directory);
        DeleteDirectory(directory, new DeleteDirectorySettings
        {
            Force = true,
            Recursive  = true
        });
    }
    
    var binDirectories = GetDirectories("./**/bin/*");

    foreach(var directory in binDirectories)
    {
        Console.WriteLine(directory);
        DeleteDirectory(directory, new DeleteDirectorySettings {
            Force = true,
            Recursive  = true
        });
    }
    
    Console.WriteLine(NugetPackageOutputDirectory);
    DeleteDirectory(NugetPackageOutputDirectory, new DeleteDirectorySettings {
        Force = true,
        Recursive  = true
    });
    
});


RunTarget(target);