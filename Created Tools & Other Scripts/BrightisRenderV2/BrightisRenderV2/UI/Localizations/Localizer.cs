using ImGui.Forms.Localization;
using System.Text.Json;

namespace BrightisRendererV2.UI.Localizations;

internal class Localizer : BaseLocalizer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        TypeInfoResolver = DictionaryJsonContext.Default
    };

    private const string NameValue_ = "Name";

    protected override string UndefinedValue => "<undefined>";
    protected override string DefaultLocale => "en";

    public Localizer()
    {
        Initialize();
    }

    protected override IList<LanguageInfo> InitializeLocalizations()
    {
        string? applicationDirectory = Path.GetDirectoryName(Environment.ProcessPath);
        if (string.IsNullOrEmpty(applicationDirectory))
            return Array.Empty<LanguageInfo>();

        string localeDirectory = Path.Combine(applicationDirectory, "resources\\langs");
        if (!Directory.Exists(localeDirectory))
            return Array.Empty<LanguageInfo>();

        string[] localeFiles = Directory.GetFiles(localeDirectory);

        var result = new List<LanguageInfo>();
        foreach (string localeFile in localeFiles)
        {
            // Read text from stream
            string json = File.ReadAllText(localeFile);

            // Deserialize JSON
            var entries = JsonSerializer.Deserialize<Dictionary<string, string>>(json, JsonOptions);
            if (entries == null || !entries.TryGetValue(NameValue_, out string? name))
                continue;

            string locale = Path.GetFileNameWithoutExtension(localeFile);
            result.Add(new LanguageInfo(locale, name, entries));
        }

        return result;
    }

    protected override string InitializeLocale()
    {
        return SettingsProvider.Instance.Get("BrightisRendererV2.Settings.Locale", string.Empty);
    }

    protected override void SetCurrentLocale(string locale)
    {
        base.SetCurrentLocale(locale);

        SettingsProvider.Instance.Set("BrightisRendererV2.Settings.Locale", locale);
    }
}