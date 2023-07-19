using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EonaCat.DnsTester.Helpers
{
    internal class UrlHelper
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
        public static event EventHandler<string> Log;
        public static bool UseSearchEngineYahoo { get; set; }
        public static bool UseSearchEngineBing { get; set; }
        public static bool UseSearchEngineGoogle { get; set; }
        public static bool UseSearchEngineQwant { get; set; }
        public static bool UseSearchEngineWolfram { get; set; }
        public static bool UseSearchEngineStartPage { get; set; }
        public static bool UseSearchEngineYandex { get; set; }

        private static async Task<List<string>> GetRandomUrlsAsync(int totalUrls)
        {
            var letters = GetRandomLetters();
            var searchEngineUrls = GetSearchEngines();
            var rand = new Random();
            var urls = new List<string>();

            while (urls.Count < totalUrls)
            {
                var index = rand.Next(searchEngineUrls.Count);
                var searchEngine = searchEngineUrls.ElementAt(index);
                var url = searchEngine.Value + letters;

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            // find all .xxx.com addresses
                            var hostNames = Regex.Matches(responseString, @"[.](\w+[.]com)");

                            // Loop through the match collection to retrieve all matches and delete the leading "."
                            var uniqueNames = new HashSet<string>();
                            foreach (Match match in hostNames)
                            {
                                var name = match.Groups[1].Value;
                                if (name != $"{searchEngine.Key.ToLower()}.com")
                                {
                                    uniqueNames.Add(name);
                                }
                            }

                            // Add the names to the list
                            foreach (var name in uniqueNames)
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
                        else
                        {
                            // Handle non-successful status codes (optional)
                            searchEngineUrls.Remove(searchEngine.Key);
                            SetStatus($"{searchEngine.Key}: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (optional)
                        searchEngineUrls.Remove(searchEngine.Key);
                        SetStatus($"{searchEngine.Key}: {ex.Message}");
                    }
                }

                letters = GetRandomLetters();
                await Task.Delay(100).ConfigureAwait(false);
            }

            var urlText = "url" + (urls.Count > 1 ? "'s" : string.Empty);
            SetStatus($"{urls.Count} random {urlText} found");
            return urls;
        }


        private static Dictionary<string, string> GetSearchEngines()
        {
            var searchEngineUrls = new Dictionary<string, string>();
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

        public static async Task<List<string>> RetrieveUrlsAsync(int numThreads, int numUrlsPerThread)
        {
            var tasks = new Task[numThreads];

            var urlList = new List<string>();

            // start each thread to retrieve a subset of unique URLs
            for (var i = 0; i < numThreads; i++)
            {
                tasks[i] = Task.Run(async () => urlList.AddRange(await GetRandomUrlsAsync(numUrlsPerThread).ConfigureAwait(false)));
            }

            // wait for all threads to complete
            await Task.WhenAll(tasks).ConfigureAwait(false);

            return urlList;
        }

        private static string GetRandomLetters()
        {
            // Generate a cryptographically strong random string
            var randomBytes = new byte[32];
            RandomNumberGenerator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private static void SetStatus(string text)
        {
            Log?.Invoke(null, text);
        }
    }
}
