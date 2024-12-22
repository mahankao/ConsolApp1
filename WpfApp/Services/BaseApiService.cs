using System;
using System.Net.Http;

namespace WpfApp.Services
{
    public abstract class BaseApiService
    {
        public HttpClient HttpClient;

        protected BaseApiService(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException("Base address cannot be null or empty.", nameof(baseAddress));

            HttpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
    }
}