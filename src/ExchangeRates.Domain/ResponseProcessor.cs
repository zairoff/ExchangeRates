using Newtonsoft.Json;

namespace ExchangeRates.Domain
{
    public static class ResponseProcessor
    {
        public static async Task<T> ProcessResponseAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            await EnsureSuccessAsync(httpResponseMessage);

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent)!;
        }

        public static async Task ProcessResponseAsync(this HttpResponseMessage httpResponseMessage)
        {
            await EnsureSuccessAsync(httpResponseMessage);
        }

        public static async Task EnsureSuccessAsync(this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var error = new ExchangeRatesError
                {
                    Message = $"Error response status code reported: {(int)httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase}",
                    AbsoluteUri = httpResponseMessage?.RequestMessage?.RequestUri?.AbsoluteUri!
                };

                if (httpResponseMessage?.RequestMessage?.Content != null)
                {
                    error.RequestContent = await httpResponseMessage.RequestMessage.Content.ReadAsStringAsync();
                }

                if (httpResponseMessage?.Content != null)
                {
                    error.ResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                }

                throw new ExchangeRatesException(JsonConvert.SerializeObject(error), error.Message);
            }
        }
    }
}