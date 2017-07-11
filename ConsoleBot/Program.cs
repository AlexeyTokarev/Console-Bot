using Newtonsoft.Json;
using System;
using System.Net;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                Console.Clear();
                string responseString = string.Empty;

                var query = Console.ReadLine(); //User Query

                var knowledgebaseId = "2dcad03f-367b-4f2e-b8a4-1b28fbd7ebdd"; // Use knowledge base id created.
                var qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d"; //Use subscription key assigned to you.

                
                Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0"); //Build the URI
                var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");
                
                var postBody = $"{{\"question\": \"{query}\"}}"; //Add the question as part of the body
                
                using (WebClient client = new WebClient()) //Send the POST request
                {
                    client.Encoding = System.Text.Encoding.UTF8; //Set the encoding to UTF8

                    
                    client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey); //Add the subscription key header
                    client.Headers.Add("Content-Type", "application/json");

                    responseString = client.UploadString(builder.Uri, postBody);

                    // Создам переменную, которая из класса ResponseModel возвращает нам answer
                    var response = JsonConvert.DeserializeObject<ResponseModel>(responseString); 

                    Console.WriteLine(response.Answer);
                    Console.WriteLine(response.Score);
                    Console.ReadLine();
                }
            }
        }
    }
}
