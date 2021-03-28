using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DieUnausstehlichenWeb.Services
{
    public class UptimeProvider : IUptimeProvider
    {
        private readonly Dictionary<string, UpState> _upStates;
        private readonly IList<WebsiteDescriptor> _websites;
        
        public UptimeProvider(IConfiguration configuration)
        {
            var config = configuration.GetSection("UpState").Get<UptimeCheckerConfiguration>();

            _upStates = new Dictionary<string, UpState>();
            foreach (var (name, _) in config.Websites)
            {
                _upStates[name] = UpState.Unavailable;
            }

            _websites = config.Websites.Select(pair => new WebsiteDescriptor
            {
                Name = pair.Key,
                Address = pair.Value
            }).ToList();
        }
        
        public IReadOnlyDictionary<string, UpState> GetStates() => new ReadOnlyDictionary<string, UpState>(_upStates);

        public async Task CheckUpstate()
        {
            foreach (var websiteDescriptor in _websites)
            {
                _upStates[websiteDescriptor.Name] = await IsUp(websiteDescriptor.Address) ? UpState.Up : UpState.Down;
            }
        }

        private static async Task<bool> IsUp(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var message = await httpClient.GetAsync(url);
                return message.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class UptimeCheckerConfiguration
    {
        public Dictionary<string, string> Websites { get; set; }
    }

    public enum UpState
    {
        /// <summary>
        /// The service is online.
        /// </summary>
        Up,

        /// <summary>
        /// The service is offline or could not reached.
        /// </summary>
        Down,

        /// <summary>
        /// The service has not yet been checked.
        /// </summary>
        Unavailable
    }

    public record WebsiteDescriptor
    {
        public string Name;
        public string Address;
    }
}