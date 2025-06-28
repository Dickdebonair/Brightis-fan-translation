
namespace TranslationToSource;

public static class simpleLogger
{
    public static void CreateErrorHeader(string message)
    {
        Console.WriteLine($"************  {message} ************");
    }

    public static void CreateHeaderMessage(string message)
    {
        Console.WriteLine($"=========== {message} ===========");
    }

    public static void TranslationManagerParseErrorMessage(string columnName, string valueFromRow, string sheetName, int row)
    {
        simpleLogger.CreateErrorHeader($"Could not parse {columnName} from sheet: {sheetName} row: {row}");
        Console.WriteLine($"{columnName} from row: {valueFromRow}");
    }
    
}
