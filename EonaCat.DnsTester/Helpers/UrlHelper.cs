using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EonaCat.DnsTester.Helpers
{
    internal class UrlHelper
    {
        private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();
        public static event EventHandler<string> Log;
        public static bool UseSearchEngineYahoo { get; set; }
        public static bool UseSearchEngineBing { get; set; }
        public static bool UseSearchEngineGoogle { get; set; }
        public static bool UseSearchEngineQwant { get; set; }
        public static bool UseSearchEngineAsk { get; set; }
        public static bool UseSearchEngineWolfram { get; set; }
        public static bool UseSearchEngineStartPage { get; set; }
        public static bool UseSearchEngineYandex { get; set; }

        private static async Task<List<string>> GetRandomUrls(int totalUrls)
        {
            var letters = GetRandomLetters();

            var searchEngineUrls = GetSearchEngines();

            Random rand = new Random();

            List<string> urls = new List<string>();
            while (urls.Count < totalUrls)
            {
                int index = rand.Next(searchEngineUrls.Count);
                KeyValuePair<string, string> searchEngine = searchEngineUrls.ElementAt(index);

                string url = searchEngine.Value + letters;

                using (var client = new WebClient())
                {
                    string responseString = null;
                    try
                    {
                        responseString = client.DownloadString(url);
                    }
                    catch (Exception ex)
                    {
                        searchEngineUrls.Remove(searchEngine.Key);
                        SetStatus($"{searchEngine.Key}: {ex.Message}");
                    }

                    if (responseString == null)
                    {
                        continue;
                    }

                    // find all .xxx.com addresses
                    MatchCollection hostNames = Regex.Matches(responseString, @"[.](\w+[.]com)");

                    // Loop through the match collection to retrieve all matches and delete the leading "."
                    HashSet<string> uniqueNames = new HashSet<string>();
                    foreach (Match match in hostNames)
                    {
                        string name = match.Groups[1].Value;
                        if (name != $"{searchEngine.Key.ToLower()}.com")
                        {
                            uniqueNames.Add(name);
                        }
                    }

                    // Add the names to the list
                    foreach (string name in uniqueNames)
                    {
                        if (urls.Count >= totalUrls)
                        {
                            break;
                        }

                        if (!urls.Contains(name))
                        {
                            urls.Add(name);
                        }
                    }
                }

                letters = GetRandomLetters();
                await Task.Delay(100);
            }

            var urlText = "url" + (urls.Count > 1 ? "'s" : string.Empty);
            SetStatus($"{urls.Count} random {urlText} found");
            return urls;
        }

        private static Dictionary<string, string> GetSearchEngines()
        {
            Dictionary<string, string> searchEngineUrls = new Dictionary<string, string>();
            if (UseSearchEngineYahoo)
            {
                searchEngineUrls.Add("Yahoo", "https://search.yahoo.com/search?p=");
            }

            if (UseSearchEngineBing)
            {
                searchEngineUrls.Add("Bing", "https://www.bing.com/search?q=");
            }

            if (UseSearchEngineGoogle)
            {
                searchEngineUrls.Add("Google", "https://www.google.com/search?q=");
            }

            if (UseSearchEngineQwant)
            {
                searchEngineUrls.Add("Qwant", "https://www.qwant.com/?q=");
            }

            if (UseSearchEngineAsk)
            {
                searchEngineUrls.Add("Ask", "https://www.ask.com/web?q=");
            }

            if (UseSearchEngineWolfram)
            {
                searchEngineUrls.Add("WolframAlpha", "https://www.wolframalpha.com/input/?i=");
            }

            if (UseSearchEngineStartPage)
            {
                searchEngineUrls.Add("StartPage", "https://www.startpage.com/do/dsearch?query=");
            }

            if (UseSearchEngineYandex)
            {
                searchEngineUrls.Add("Yandex", "https://www.yandex.com/search/?text=");
            }

            return searchEngineUrls;
        }

        public static async Task<List<string>> RetrieveUrls(int numThreads, int numUrlsPerThread)
        {
            Task[] tasks = new Task[numThreads];

            List<string> urlList = new List<string>();

            // start each thread to retrieve a subset of unique URLs
            for (int i = 0; i < numThreads; i++)
            {
                tasks[i] = Task.Run(async () => urlList.AddRange(await GetRandomUrls(numUrlsPerThread)));
            }

            // wait for all threads to complete
            await Task.WhenAll(tasks);

            return urlList;
        }

        private static string GetRandomLetters()
        {
            // Generate a cryptographically strong random string
            byte[] randomBytes = new byte[32];
            _randomNumberGenerator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private static void SetStatus(string text)
        {
            Log?.Invoke(null, text);
        }
    }
}
