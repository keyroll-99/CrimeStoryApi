using SqlRunner;
using SqlRunner.models;

namespace Authentication.Core;

public class AuthenticationScriptRunner
{
    public static void RunScripts(string connectionsString)
    {
        var setupModel = new SetupModel
        {
            ConnectionString = connectionsString,
            FolderPath = $"{GetPatchToScript()}",
            DataBaseType = DataBaseTypeEnum.Postgresql,
            DeployScriptsTableName = "DeployScripts"
        };
        SqlScriptRunner.GetScriptRunner(setupModel).RunDeploy();
    }

    private static string GetPatchToScript()
    {
        var rootPath = Directory.GetParent(Directory.GetCurrentDirectory());

        return
            $"{rootPath}{Path.DirectorySeparatorChar}Authentication{Path.DirectorySeparatorChar}Authentication.Core{Path.DirectorySeparatorChar}Scripts";
    }
}