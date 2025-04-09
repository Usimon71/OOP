namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public static class LocalFileSystemPathValidator
{
    public static bool IsValidDirectory(string path)
    {
        return Directory.Exists(path);
    }

    public static bool IsValidFile(string path)
    {
        return File.Exists(path);
    }

    public static bool IsFullyQualifiedPath(string path)
    {
        return Path.IsPathFullyQualified(path);
    }

    public static bool IsSubPath(string basePath, string subPath)
    {
        string normalizedBasePath = Path
            .GetFullPath(basePath)
            .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        string normalizedSubPath = Path.GetFullPath(subPath);

        return normalizedSubPath.
            StartsWith(normalizedBasePath, StringComparison.OrdinalIgnoreCase)
               && (normalizedSubPath.Length == normalizedBasePath.Length
                    || normalizedSubPath[normalizedBasePath.Length] == Path.DirectorySeparatorChar);
    }
}
