using Core.Utils;
using SqlRunner;
using SqlRunner.models;

namespace Authentication.Core;

public static class AuthenticationScriptRunner
{
    public static void RunScripts(string connectionsString)
    {
        var scriptPath = $"{PathUtils.GetModulePath("Authentication.Core")}{Path.DirectorySeparatorChar}Scripts";
        var setupModel = new SetupModel
        {
            ConnectionString = connectionsString,
            FolderPath = scriptPath,
            DataBaseType = DataBaseTypeEnum.Postgresql,
            DeployScriptsTableName = "AuthenticationDeployScripts",
            InitFolderPath = $"{scriptPath}/Init"
        };
        SqlScriptRunner.GetScriptRunner(setupModel).RunDeploy();
    }
}