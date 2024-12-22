using BrightistRenderer.Models.UI.Fonts;
using BrightistRenderer.UI.Fonts;
using BrightistRenderer.UI.Forms;
using BrightistRenderer.UI.Localizations;
using ImGui.Forms;
using Veldrid;

var app = new Application(new Localizer(), GraphicsBackend.Vulkan);
var form = new MainForm();

FontProvider.Instance.RegisterFont(Font.Roboto);
form.DefaultFont = FontProvider.Instance.GetFont(Font.Roboto);

app.Execute(form);
