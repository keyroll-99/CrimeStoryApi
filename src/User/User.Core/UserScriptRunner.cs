using SqlRunner;
using SqlRunner.models;

namespace User.Core;

public static class UserScriptRunner
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

        return $"{rootPath}{Path.DirectorySeparatorChar}User{Path.DirectorySeparatorChar}User.Core{Path.DirectorySeparatorChar}Scripts";
    }
}