using McqsUI.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace McqsUI.ApiClients
{
    public class WeatherForecastAPI
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        public WeatherForecastAPI(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;

            //set baseAddress
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

        public async Task<List<WeatherForecast>> GetForecastAPIAsync(DateTime startDate)
        {
            var list = new List<WeatherForecast>();

            var response = await _httpClient.GetAsync(
                "api/weather?startDate=" + startDate.ToShortDateString());

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
            }

            return list == null ? new List<WeatherForecast>() : list;
        }
    }

}
