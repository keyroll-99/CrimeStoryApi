namespace Core.Utils;

public static class PathUtils
{
    public static string GetModulePath(string module)
    {
        var moduleName = module.Split(".")[0];
        // Directory.GetParent always return path to boostrap project, because it's startup project
        var rootPath = Directory.GetParent(Directory.GetCurrentDirectory());

        return $"{rootPath}{Path.DirectorySeparatorChar}{moduleName}{Path.DirectorySeparatorChar}{module}";
    }
}