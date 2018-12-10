// TOOLS

#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=JetBrains.dotCover.CommandLineTools"

string[] Projects = new string[]{
    "Alternatives",
    "Alternatives.CollectionExtensions",
    "Alternatives.ConversionExtensions",
    "Alternatives.Crypto",
    "Alternatives.ReflectionExtensions",
};

// TOOLS

// ADDIN

// ARGUMENTS

string BranchName = Argument("branchName", "");
string BuildConfig = Argument("buildType", "Release");
string NugetApiKey = Argument("nugetKey", "");
string NugetSourceServerUrl = Argument("nugetServer", "https://api.nuget.org/v3/index.json");
string NugetSymbolServerUrl = Argument("nugetSymbolServer", "https://api.nuget.org/v3/index.json");
// string NugetSymbolServerUrl = Argument("nugetSymbolServer", "https://symbolsource.org");

// VARIABLES

string[] RemovableDirectories = new string[]{
"./**/bin/**",
"./**/obj/**",
"./**/build/**",
"./**/octoPackages/**"
};

string[] TestProjects = new string[]{
    "./**/*Test.csproj",
    "./**/*Tests.csproj",
};

var ReleaseBranch = new string[]
{
    "master"
};

string NugetPackageExtension = ".nupkg";
string NugetSymbolPackageName = "symbol";

// STAGE NAMES

string ResultStage = "RESULT";
string PushStage = "Push Package";
string PackageStage = "Create Package";
string AnalysisStage = "Analysis";
string TestStage = "Test";
string BuildStage = "Build";
string NugetRestoreStage = "Nuget Restore";
string CleanStage = "Clean";
string FindSlnStage = "Find .sln files";
string CheckEnvVarStage = "Check Environment Name";

// RUN OPERATION

var target = Argument("target", ResultStage);

Task(ResultStage)
.IsDependentOn(PushStage);

Task(PushStage)
.IsDependentOn(PackageStage)
.Does(()=>
{
    if(!ReleaseBranch.Contains(BranchName))
    {
        Console.WriteLine("This branch's artifacts are not published");
        return;
    }

    foreach (var project in Projects)
    {
        var npkgFiles = GetFiles($"./**/{project}/bin/{BuildConfig}/*.{NugetPackageExtension}");
        foreach(FilePath nupkgFile in npkgFiles)
        {
            Console.WriteLine();
            if(nupkgFile.ToString().Contains(NugetSymbolPackageName))
            {
                Console.WriteLine($"{nupkgFile} - Push to SymbolServer");
                PublishSymbolNugetPackage(project, nupkgFile, NugetSymbolServerUrl, NugetApiKey);
            }
            else
            {
                Console.WriteLine($"{nupkgFile} - Push to SourceServer");
                PublishSourceNugetPackage(project, nupkgFile, NugetSourceServerUrl, NugetApiKey);  
            }
        }
    }
});

Task(PackageStage)
.IsDependentOn(TestStage)
.Does(()=>
{
    foreach (var project in Projects)
    {
        FilePath csproj = GetFiles($"./**/{project}.csproj").First();
        DirectoryPath directory = csproj.GetDirectory();
        string d = directory.ToString();
        DotNetCorePack(d, new DotNetCorePackSettings()
                    {
                        Configuration = BuildConfig,
                        ArgumentCustomization = args => args.Append("--no-restore").Append("--include-symbols"),
                    });
    }
});

Task(AnalysisStage)
.IsDependentOn(BuildStage)
.ContinueOnError()
.Does(()=>
{
    DotCoverAnalyse(tool => 
    {
        tool.XUnit2(TestProjects);
    },
    new FilePath("./AnalysisResult.xml"),
    new DotCoverAnalyseSettings());
});

Task(TestStage)
.IsDependentOn(BuildStage)
.Does(()=>
{
    foreach (var testProject in TestProjects)
    {
        var projectFiles = GetFiles(testProject);
        foreach(var file in projectFiles)
        {
            DotNetCoreTest(file.FullPath);
        }
    }
});

Task(BuildStage)
.IsDependentOn(NugetRestoreStage)
.Does(()=>
{
    DotNetCoreBuild(".", new DotNetCoreBuildSettings()
                        {
                            Configuration = BuildConfig,
                            ArgumentCustomization = args => args.Append("--no-restore"),
                        });
});

Task(NugetRestoreStage)
.IsDependentOn(CleanStage)
.Does(()=>
{
    DotNetCoreRestore();
});


Task(CleanStage)
.IsDependentOn(CheckEnvVarStage)
.Does(()=>
{
    foreach (var directoryPath in RemovableDirectories)
    {
        var directories = GetDirectories(directoryPath);

        foreach (var directory in directories)
        {
            if(!DirectoryExists(directory))
                continue;

            Console.WriteLine("Directory is cleaning : " + directory.ToString());            
            DeleteDirectory(directory, new DeleteDirectorySettings
            {
                Force = true,
                Recursive  = true
            });
        }

    }   
});

Task(CheckEnvVarStage)
.Does(()=>
{
    if(string.IsNullOrEmpty(BranchName))
    {
        throw new Exception("Branch Name should be provided");
    }
    Console.WriteLine("Branch Name = " + BranchName);

    if(string.IsNullOrEmpty(NugetApiKey))
    {
        throw new Exception("Nuget Api Key should be provided");
    }
});

private void PublishSymbolNugetPackage (string packageId, FilePath packagePath, string NugetSourceServerUrl, string apiKey)
{ 
    var nugetPushSettings = new NuGetPushSettings
    {
        ApiKey = apiKey,
        Source = NugetSourceServerUrl 
    };
    
    NuGetPush(packagePath.FullPath, nugetPushSettings);  
}

private void PublishSourceNugetPackage (string packageId, FilePath packagePath, string NugetSourceServerUrl, string apiKey)
{
    if(IsNugetSourcePackagePublished(packageId, packagePath, NugetSourceServerUrl))
    {
        Console.WriteLine($"{packageId} is already published. Hence this package will be skipped");
        return;
    }
    
    var nugetPushSettings = new NuGetPushSettings
    {
        ApiKey = apiKey,
        Source = NugetSourceServerUrl 
    };
    
    NuGetPush(packagePath.FullPath, nugetPushSettings);  
}

private bool IsNugetSourcePackagePublished(string packageId, FilePath packagePath, string NugetSourceServerUrl) {
    string packageNameWithVersion = packagePath.GetFilename().ToString().Replace(".nupkg", "");
    var latestPublishedVersions = NuGetList(
        packageId,
        new NuGetListSettings 
        {
            Prerelease = true,
            Source = new string[]{NugetSourceServerUrl}
        }
    );

    return latestPublishedVersions.Any(p => packageNameWithVersion.EndsWith(p.Version));
}

RunTarget(target);