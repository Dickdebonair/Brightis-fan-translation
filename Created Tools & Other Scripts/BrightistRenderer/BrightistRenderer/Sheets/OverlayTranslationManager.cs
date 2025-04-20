using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using BrightistRenderer.Models.Sheets;
using BrightistRenderer.UI.Configuration;
using GoogleSheetsApiV4;
using GoogleSheetsApiV4.Contract;
using GoogleSheetsApiV4.Contract.DataClasses;

namespace BrightistRenderer.Sheets
{
    internal class OverlayTranslationManager
    {
        private static OverlayTranslationManager? _instance;

        private readonly ISheetManager _sheet;

        private readonly Dictionary<string, IList<OverlaySheetData>> _sheetLines = new();

        private OverlayTranslationManager(ISheetManager sheet)
        {
            _sheet = sheet;
        }

        public static OverlayTranslationManager Create()
        {
            if (_instance != null)
                return _instance;

            string clientId = ConfigProvider.Instance.GetClientId();
            string clientSecret = ConfigProvider.Instance.GetClientSecret();
            string sheetId = ConfigProvider.Instance.GetSheetId();

            IOAuth2TokenStorage tokenStorage = new OAuth2TokenStorage();
            ICodeFlowManager codeFlow = OAuth2CodeFlowManager.Create(Scope.Write, clientId, clientSecret, tokenStorage);
            ISheetManager sheet = GoogleApiConnector.Instance.CreateSheetManager(sheetId, codeFlow);

            return _instance = new OverlayTranslationManager(sheet);
        }

        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties, typeof(OverlayRawSheetData))]
        public async Task<IList<OverlaySheetData>?> GetTranslationsAsync(OverlayConfigData overlayConfig)
        {
            if (_sheetLines.TryGetValue(overlayConfig.SheetName, out IList<OverlaySheetData>? result))
                return result;

            _sheetLines[overlayConfig.SheetName] = result = new List<OverlaySheetData>();

            CellIdentifier startCell = CellIdentifier.Parse("A2");
            CellIdentifier endCell = CellIdentifier.Parse($"F{overlayConfig.SheetMaxRow}");

            OverlayRawSheetData[]? rows = await _sheet.GetRangeAsync<OverlayRawSheetData>(overlayConfig.SheetName, startCell, endCell);
            if (rows == null)
                return null;

            foreach (OverlayRawSheetData row in rows)
            {
                bool isValidTextType = Enum.TryParse(row.TextType, out TextType textType);

                result.Add(new OverlaySheetData
                {
                    OverlayIndex = overlayConfig.OverlaySlot,
                    Offset = long.Parse(row.Offset[2..], NumberStyles.HexNumber),
                    TextType = isValidTextType && textType < TextType.Count ? textType : null,
                    OriginalText = row.OriginalText,
                    TranslatedText = row.TranslatedText ?? string.Empty
                });
            }

            return result;
        }

        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties, typeof(OverlayUpdateRawSheetData))]
        public async Task UpdateTranslationsAsync(OverlayConfigData overlayConfig)
        {
            if (!_sheetLines.TryGetValue(overlayConfig.SheetName, out IList<OverlaySheetData>? sheetData))
                return;

            CellIdentifier startCell = CellIdentifier.Parse("F2");
            CellIdentifier endCell = CellIdentifier.Parse($"F{overlayConfig.SheetMaxRow}");

            var updateList = new List<OverlayUpdateRawSheetData>();
            foreach (OverlaySheetData data in sheetData)
                updateList.Add(new OverlayUpdateRawSheetData { TranslatedText = data.TranslatedText });

            await _sheet.UpdateRangeAsync(updateList, overlayConfig.SheetName, startCell, endCell);
        }
    }
}
