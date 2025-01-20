
using DeepL;
using DeepL.Model;


namespace Pointer.Finder.Clients {

    class DeepLClient {
    public string ApiKey { get; }
    public Translator translator { get; }

        public DeepLClient(string apiKey)
        {
            ApiKey = apiKey;

            translator = new Translator(apiKey);
        }
        
        public async Task<TextResult[]> JISToEnglish(string jis) {
            return await translator.TranslateTextAsync([jis], "JA", "EN-GB");
        }
    }
}