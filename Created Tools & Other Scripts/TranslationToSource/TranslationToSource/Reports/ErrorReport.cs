using TranslationToSource.Models.Reports;

namespace TranslationToSource.Reports;

public class ErrorReport
{
    public static readonly ErrorReport Instance = new();

    private readonly string _reportFile = CreateReportFile();
    private readonly Dictionary<string, List<ReportItem>> _sections = [];

    private string? _activeSectionName;

    public void EnterSection(string sectionName)
    {
        if (!_sections.ContainsKey(sectionName))
            _sections[sectionName] = [];

        _activeSectionName = sectionName;
    }

    public void ExitSection()
    {
        _activeSectionName = null;
    }

    public void AddReportItem(ReportItem item)
    {
        if (_activeSectionName is null)
            throw new InvalidOperationException("No active section in error report set.");

        AddReportItem(_activeSectionName, item);
    }

    public void AddReportItem(string sectionName, ReportItem item)
    {
        if (!_sections.TryGetValue(sectionName, out List<ReportItem>? items))
            _sections[sectionName] = items = [];

        items.Add(item);
    }

    public void Persist()
    {
        using var writer = new StreamWriter(File.OpenWrite(_reportFile));

        int maxSectionLength = _sections.Keys.Max(x => x.Length);
        foreach (string sectionName in _sections.Keys)
        {
            WriteSectionName(writer, sectionName, maxSectionLength);

            foreach (ReportItem item in _sections[sectionName])
                writer.WriteLine(item.Serialize());

            writer.WriteLine();
        }
    }

    private static void WriteSectionName(StreamWriter writer, string sectionName, int maxLength)
    {
        maxLength += 10;

        int paddingLeft = (maxLength - sectionName.Length) / 2;
        WritePadding(writer, paddingLeft, true);

        writer.Write(sectionName);

        int paddingRight = maxLength - sectionName.Length - paddingLeft;
        WritePadding(writer, paddingRight, false);

        writer.WriteLine();
    }

    private static void WritePadding(StreamWriter writer, int padding, bool isLeft)
    {
        switch (padding)
        {
            case > 1:
                if (!isLeft)
                    writer.Write(' ');

                writer.Write(new string('+', padding - 1));

                if (isLeft)
                    writer.Write(' ');
                break;

            case 1:
                writer.Write(' ');
                break;
        }
    }

    private static string CreateReportFile()
    {
        string reportName = Path.Combine($"reports\\errors_{DateTime.UtcNow:YYYY_MM_ddTHH_mm_ss_fff}.txt");
        string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reportName);

        string? reportDirectory = Path.GetDirectoryName(reportPath);
        if (!Directory.Exists(reportDirectory))
            Directory.CreateDirectory(reportDirectory);

        return reportPath;
    }
}
