using BrightisRendererV2.Models.Sheets;
using BrightisRendererV2.Models.UI.Components;

namespace BrightisRendererV2.UI.Texts.Previews;

internal class PopupPreviewProvider
{
    public static PopupPreviewData? CreatePreviewData(IList<OverlaySheetData> texts, int index)
    {
        if (index < 0 || index >= texts.Count || texts[index].TextType != TextType.Popup)
            return null;

        OverlaySheetData mainSheetData;
        OverlaySheetData subSheetData;

        if (index == 0)
        {
            if (texts.Count <= 1)
                return null;

            if (texts[1].TextType != TextType.Popup)
                return null;

            mainSheetData = texts[0];
            subSheetData = texts[1];
        }
        else if (index == texts.Count - 1)
        {
            if (texts[index - 1].TextType != TextType.Popup)
                return null;

            mainSheetData = texts[index - 1];
            subSheetData = texts[index];
        }
        else
        {
            if (texts[index - 1].TextType == TextType.Popup)
            {
                mainSheetData = texts[index - 1];
                subSheetData = texts[index];
            }
            else if (texts[index + 1].TextType == TextType.Popup)
            {
                mainSheetData = texts[index];
                subSheetData = texts[index + 1];
            }
            else
                return null;
        }

        return new PopupPreviewData
        {
            PopupSheetData = mainSheetData,
            SubPopupSheetData = subSheetData,
            ActiveSheetData = texts[index]
        };
    }
}