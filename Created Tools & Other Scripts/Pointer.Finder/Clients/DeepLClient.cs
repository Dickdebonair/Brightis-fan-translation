
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
        
        public async Task<TextResult[]> JISToEnglish(string jis, int tooManyRquestsCounter = 0) {
            try {
                return await translator.TranslateTextAsync([jis], "JA", "EN-GB");
            } catch(Exception e) {

                if(e.Message.Contains("Too many requests, DeepL servers are currently experiencing high load") && tooManyRquestsCounter < 5) {
                    await Task.Delay(3000);
                    return await JISToEnglish(jis, tooManyRquestsCounter++);
                } else {
                    throw e;
                }
            }
            
        }
    }
}