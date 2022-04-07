using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;

namespace GoogleSuggestion
{
    //Define Class
    class QuestionsExplorer
    {
        public List<string> GetQuestions(string questionType, string userInput, string countryCode)
        {
            var questionResults = new List<string>(); //List To Return

            //Build Google Search Query
            string searchQuery = questionType + " " + userInput + " ";
            string googleSearchUrl =
            "http://google.com/complete/search?output=toolbar&gl=" + countryCode + "&q=" + searchQuery;


            //Call The URL and Read Data
            using (HttpClient client = new HttpClient())
            {
                string result = client.GetStringAsync(googleSearchUrl).Result;

                //Parse The XML Documents
                XDocument doc = XDocument.Parse(result);

                foreach (XElement element in doc.Descendants("CompleteSuggestion"))
                {
                    string question = element.Element("suggestion")?.Attribute("data")?.Value;
                    questionResults.Add(question);
                }

            }

            return questionResults;

        }



    }

    class Program
    {
        static void Main(string[] args)
        {
            //Get a Keyword From The User
            Console.WriteLine("Enter a Keyword:");
            var userInput = Console.ReadLine();

            //Create Object of the QuestionsExplorer Class
            var qObj = new QuestionsExplorer();

            //Call The Method and pass the parameters
            var questions = qObj.GetQuestions("what", userInput, "us");

            //Loop over the list and pring the questions
            foreach (var result in questions)
            {
                Console.WriteLine(result);
            }

            //Finish
            Console.WriteLine();
            Console.WriteLine("---Done---");
            Console.ReadLine();
        }
    }

   

}
