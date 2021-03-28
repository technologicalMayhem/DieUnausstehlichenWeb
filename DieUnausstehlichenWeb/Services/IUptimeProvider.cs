using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieUnausstehlichenWeb.Services
{
    public interface IUptimeProvider
    {
        public IReadOnlyDictionary<string, UpState> GetStates();
        Task CheckUpstate();
    }
}