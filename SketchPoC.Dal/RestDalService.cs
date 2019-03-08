using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SketchPoC.Dal
{
    public class RestDalService : IDalService
    {
        private const string BaseAddressString = "http://152.96.234.185:9000/";
        private const string UploadEndpoint = "uploadLarge";
        private const string DownloadEndpoint = "downloadLarge";

        public string Save(Stream stream)
        {
            return PostStream(stream);

        }

        private static string PostStream(Stream stream)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseAddressString);

                    using (var content = new MultipartFormDataContent())
                    {

                        var streamContent = new StreamContent(stream);
                        streamContent.Headers.Add("Content-Type", "image/png");
                        streamContent.Headers.Add("Content-Disposition", "form-data; name=\"name\"; filename=\"image.png\"");

                        content.Add(streamContent);

                        using (var message = httpClient.PostAsync(UploadEndpoint, content).Result)
                        {
                            return message.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {

            }
            catch (ArgumentException ex)
            {

            }
            catch (AggregateException ex)
            {
            }
            return string.Empty;
        }

        public async Task<Stream> Download(string fileId)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"{BaseAddressString}{DownloadEndpoint}/{fileId}");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStreamAsync();
                    }
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {

            }
            catch (ArgumentException ex)
            {

            }
            catch (AggregateException ex)
            {
            }
            return null;
        }
    }
}
