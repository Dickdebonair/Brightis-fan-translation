namespace TranslationToSource;

public static class FileLogger
{
    private static string ErrorFileName;
    public static void init()
    {
        ErrorFileName = Path.Combine(".", $"ErrorsThatOccured-{DateTime.Now.ToString("s")}");
    }

    private static void EnsureErrorFileExists()
    {
        Directory.CreateDirectory(ErrorFileName);
    }

    public async static Task WriteErrorsToFile(List<string> errors)
    {
        await File.AppendAllLinesAsync(ErrorFileName, errors);
    }

    public static void AddErrorHead(string headerText, List<string> errors)
    {
        errors.Add("");
        errors.Add($"+++++++++++++++++++ {headerText} +++++++++++++++++++");

    }
}
