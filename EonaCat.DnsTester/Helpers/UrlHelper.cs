using System;
using System.Collections.Concurrent;
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
        var urls = new ConcurrentDictionary<string, byte>();
        var letters = GetRandomLetters();
        var searchEngineUrls = GetSearchEngines().ToList();
        var random = new Random();

        while (urls.Count < totalUrls && searchEngineUrls.Count > 0)
        {
            await Task.Run(async () =>
            {
                var index = random.Next(searchEngineUrls.Count);
                var searchEngine = searchEngineUrls[index];
                var url = searchEngine.Value + letters;

                try
                {
                    var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var hostNames = Regex.Matches(responseString, @"[.](\w+[.]com)");

                        foreach (Match match in hostNames)
                        {
                            var name = match.Groups[1].Value;
                            if (name == $"{searchEngine.Key.ToLower()}.com") continue;

                            urls.TryAdd(name, 0); // TryAdd is thread-safe
                            if (urls.Count >= totalUrls)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        searchEngineUrls.RemoveAt(index);
                        SetStatus($"{searchEngine.Key}: {response.StatusCode}");
                    }

                    httpClient.Dispose();
                }
                catch (Exception ex)
                {
                    searchEngineUrls.RemoveAt(index);
                    SetStatus($"{searchEngine.Key}: {ex.Message}");
                }

                letters = GetRandomLetters();
                await Task.Delay(100).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        var urlText = "url" + (urls.Count > 1 ? "'s" : string.Empty);
        SetStatus($"{urls.Count} random {urlText} found");
        return urls.Keys.ToList();
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
            var tasks = new List<Task<List<string>>>();

            // Start each task to retrieve a subset of unique URLs
            for (var i = 0; i < numThreads; i++)
            {
                tasks.Add(GetRandomUrlsAsync(numUrlsPerThread));
            }

            // Wait for all tasks to complete
            var results = await Task.WhenAll(tasks).ConfigureAwait(false);

            // Flatten the results from all tasks into a single list
            var urlList = results.SelectMany(urls => urls).ToList();

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
