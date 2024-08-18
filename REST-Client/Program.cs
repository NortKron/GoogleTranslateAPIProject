using TranslateWebApp.Interfaces;

namespace REST_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ITranslationService translationService;
            RestTranslationClient clientREST = new RestTranslationClient();
            translationService = clientREST;

            List<string> listInput = new List<string>();
            string inputString = "";
            string targetLanguage = "ru";
            string sourceLanguage = "en";

            while (inputString != "3")
            {
                Console.Write("\nREST-Сlient of the translation service\nSelect command:\n1. Enter lines for translate\n2. Info\n3. Exit\n\nCommand: ");
                inputString = Console.ReadLine();

                switch (inputString)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("\nEnter source language:");
                        sourceLanguage = Console.ReadLine();

                        Console.WriteLine("\nEnter target language:");
                        targetLanguage = Console.ReadLine();

                        Console.WriteLine("\nEnter line for translate. Enter ';' to complete the entry");
                        listInput = new();

                        while (inputString != ";")
                        {
                            string newLine = Console.ReadLine();

                            if (newLine == ";")
                            {
                                if (listInput.Count == 0) break;

                                List<string> listOutput = await translationService.TranslateAsync(listInput, targetLanguage, sourceLanguage);

                                Console.WriteLine("\nTranslations:");
                                foreach (string output in listOutput)
                                    Console.WriteLine(output);

                                break;
                            }
                            else
                            {
                                listInput.Add(newLine);
                            }
                        }

                        break;

                    case "2":
                        string info = await translationService.GetServiceInfoAsync();
                        Console.Clear();
                        Console.WriteLine(info);
                        break;

                    case "3":
                        Console.WriteLine("Exit from the program...");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nUnkown commad. Repeat, please");
                        break;
                }
            }
        }
    }
}