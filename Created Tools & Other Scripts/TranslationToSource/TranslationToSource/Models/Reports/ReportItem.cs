namespace TranslationToSource.Models.Reports;

public abstract record ReportItem
{
    public abstract string Serialize();
}