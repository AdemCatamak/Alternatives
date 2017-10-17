// TOOLS

#tool "nuget:?package=NuGet.CommandLine"
#tool "nuget:?package=NUnit.Runners&version=2.6.4"
#tool "nuget:?package=JetBrains.dotCover.CommandLineTools"

// VARIABLES

string solutionName = "Alternatives";
string solutionFullName = $"{solutionName}.sln";

string toolPath = "./tools";
string nugetPath = toolPath + "/nuget.exe";



// STAGE NAMES

string DefaultStage = "RESULT";
string TestStage = "Test";
string AnalysisStage = "Analysis";
string BuildStage = "Build";
string CleanStage = "Clean";
string NugetRestoreStage = "Nuget Restore";

// RUN OPERATION

var target = Argument("target", DefaultStage);

Task(DefaultStage)
.IsDependentOn(TestStage)
.IsDependentOn(AnalysisStage)
.Does(() =>{});

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
    foreach(var testDll in testDlls)
    {
        Console.WriteLine(testDll.FullPath);
        try
        {
            NUnit(testDll.FullPath);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
});

Task(BuildStage)
.IsDependentOn(NugetRestoreStage)
.Does(()=>
{
    MSBuild(solutionFullName);
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
});


RunTarget(target);