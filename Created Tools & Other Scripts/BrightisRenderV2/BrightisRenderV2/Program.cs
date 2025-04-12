using BrightisRendererV2.Models.UI.Fonts;
using BrightisRendererV2.Sheets;
using BrightisRendererV2.UI.Configuration;
using BrightisRendererV2.UI.Fonts;
using BrightisRendererV2.UI.Forms;
using BrightisRendererV2.UI.Localizations;
using ImGui.Forms;
using Microsoft.Extensions.DependencyInjection;
using Veldrid;

var services = new ServiceCollection();
services.AddSingleton<ConfigProvider>();
services.AddSingleton<SpreadsheetManager>();
services.AddSingleton<OverlayTranslationManager>();

var serviceProvider = services.BuildServiceProvider();
var form = new MainForm(serviceProvider);

FontProvider.Instance.RegisterFont(Font.Roboto);
form.DefaultFont = FontProvider.Instance.GetFont(Font.Roboto);

var app = new Application(new Localizer(), GraphicsBackend.Vulkan);
app.Execute(form);