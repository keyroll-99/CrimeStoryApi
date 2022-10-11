using Core.Utils;
using SqlRunner;
using SqlRunner.models;

namespace User.Core;

public static class UserScriptRunner
{
    public static void RunScripts(string connectionsString)
    {
        var scriptPath = $"{PathUtils.GetModulePath("User.Core")}{Path.DirectorySeparatorChar}Scripts";
        var setupModel = new SetupModel
        {
            ConnectionString = connectionsString,
            FolderPath = scriptPath,
            DataBaseType = DataBaseTypeEnum.Postgresql,
            DeployScriptsTableName = "UserDeployScripts",
            InitFolderPath = $"{scriptPath}/Init"
            
        };
        SqlScriptRunner.GetScriptRunner(setupModel).RunDeploy();
    }
}