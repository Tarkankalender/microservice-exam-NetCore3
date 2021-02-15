using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exam.City.Helper
{
    public class RestClientHelper:IDisposable
    {
      
        private bool disposedValue;

        public  async Task<string> GetAsync(string requestUri, HttpContext httpContext)
        {
            string result = "Error";
            var correlationId = Guid.NewGuid().ToString();

            httpContext.Request.Headers.TryGetValue("CorrelationId", out var values);
            if (values.Any())
            {
                correlationId = values.First();
            }

            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.Add("CorrelationId", correlationId);
                    result = await httpClient.GetStringAsync(requestUri);
                }
            }
            return result;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RestClientHelper()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
    