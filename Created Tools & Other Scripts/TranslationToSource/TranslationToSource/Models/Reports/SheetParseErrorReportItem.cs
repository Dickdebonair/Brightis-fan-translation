namespace TranslationToSource.Models.Reports;

record SheetParseErrorReportItem(string SheetName, string ColumnName, int Row, string Value) : ReportItem
{
    public override string Serialize()
    {
        return $"Could not parse {ColumnName} with value {Value} from sheet {SheetName} row {Row}";
    }
}