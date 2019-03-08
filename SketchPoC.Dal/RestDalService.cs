using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace SketchPoC.Dal
{
    public class RestDalService : IDalService
    {
        private const string BaseAddressString = "http://152.96.238.1:9000/";
        private const string UploadEndpoint = "upload";

        public void Save(Stream stream)
        {
            PostStream(stream);
        }

        private static void PostStream(Stream stream)
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
                            var input = message.Content.ReadAsStringAsync().Result;
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
        }
    }
}
