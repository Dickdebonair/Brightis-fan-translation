using GoogleSheetsApiV4.Contract.DataClasses;
using System.Text.Json.Serialization;

namespace TranslationToSource.Models.Sheets;

internal class TokenStorageData
{
    public Scope Scope { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ExpirationDate { get; set; }
}

[JsonSerializable(typeof(TokenStorageData))]
partial class TokenStorageDataContext : JsonSerializerContext
{
    public static readonly TokenStorageDataContext Instance = new();
}