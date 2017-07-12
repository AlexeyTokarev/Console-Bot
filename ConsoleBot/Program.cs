using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console-Bot";
            for (;;)
            {
                Console.Clear();
                string responseString = string.Empty;
                Console.Write("Введите запрос: ");

                // Запрос пользователя
                var query = Console.ReadLine(); 

                // Идентификатор базы знаний
                var knowledgebaseId = "da50c6c1-0e1f-467f-b94a-f82c0b0e1ac7"; 

                // Использование ключа подписи в QnA Maker
                var qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d"; 

                // Вписать адрес для работы с классом URI
                var qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v2.0"); 
                var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");

                // Добавление вопроса как части тела
                var postBody = $"{{\"question\": \"{query}\"}}"; 
                
                // Отсылаем ПОСТ запрос
                using (var client = new WebClient()) 
                {
                    // Изменияем кодировку
                    client.Encoding = System.Text.Encoding.UTF8; 

                    // Добавляем заголовок ключа подписи
                    client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey); 
                    client.Headers.Add("Content-Type", "application/json");

                    try
                    {
                        responseString = client.UploadString(builder.Uri, postBody);
                    }
                    catch
                    {
                        continue;
                    }

                    // Создам переменную, которая из класса ResponseModel возвращает нам PossibleAnswer
                    var response = JsonConvert.DeserializeObject<Response>(responseString);

                    var firstOrDefault = response.Answers.FirstOrDefault();

                    if (firstOrDefault != null)
                    {
                        Console.WriteLine(firstOrDefault.PossibleAnswer);
                    }
                    else
                    {
                        continue;
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
