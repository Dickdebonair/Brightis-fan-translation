using System.Text.Json;
using GoogleSheetsApiV4.Contract;
using GoogleSheetsApiV4.Contract.DataClasses;
using TranslationToSource.Models.Sheets;

namespace TranslationToSource.Sheets;

internal class OAuth2TokenStorage : IOAuth2TokenStorage
{
    private TokenStorageData? _data;

    public OAuth2TokenStorage()
    {
        _data = Initialize();
    }

    public Scope? GetScope() => _data?.Scope;

    public string? GetAccessToken() => _data?.AccessToken;

    public string? GetRefreshToken() => _data?.RefreshToken;

    public DateTime GetExpiration() => _data?.ExpirationDate ?? DateTime.MinValue;

    public void SetScope(Scope scope)
    {
        _data ??= new TokenStorageData();
        _data.Scope = scope;

        Persist();
    }

    public void SetAccessToken(string accessToken)
    {
        _data ??= new TokenStorageData();
        _data.AccessToken = accessToken;

        Persist();
    }

    public void SetRefreshToken(string refreshToken)
    {
        _data ??= new TokenStorageData();
        _data.RefreshToken = refreshToken;

        Persist();
    }

    public void SetExpiration(DateTime expiration)
    {
        _data ??= new TokenStorageData();
        _data.ExpirationDate = expiration;

        Persist();
    }

    public void Persist()
    {
        string json = JsonSerializer.Serialize(_data, TokenStorageDataContext.Instance.TokenStorageData!);
        File.WriteAllText("oauth.json", json);
    }

    private static TokenStorageData? Initialize()
    {
        if (!File.Exists("oauth.json"))
            return null;

        string json = File.ReadAllText("oauth.json");
        return JsonSerializer.Deserialize(json, TokenStorageDataContext.Instance.TokenStorageData);
    }
}