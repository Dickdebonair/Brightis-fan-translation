using TranslationToSource.Models.Reports;

namespace TranslationToSource.Reports;

class ConsoleReport
{
    public static readonly ConsoleReport Instance = new();

    public void WriteSectionName(string sectionName)
    {
        Console.Write("=== ");
        Console.WriteLine(sectionName);
    }

    public void WriteReportItem(ReportItem item)
    {
        Console.WriteLine(item.Serialize());
    }
}