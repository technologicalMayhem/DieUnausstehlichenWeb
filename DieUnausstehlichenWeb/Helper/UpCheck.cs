using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DieUnausstehlichenWeb.Helper
{
    public static class UpCheck
    {
        public static async Task<bool> IsUp(string url)
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

        public static IHtmlContent CheckUpStates(IDictionary<string, string> dictionary)
        {
            var tasks = new List<Task<string>>();
            foreach (var (name, url) in dictionary)
            {
                var task = Task.Run(async () =>
                {
                    var isUp = await IsUp(url);
                    return isUp ? $"{name} is up." : $"{name} is currently down.";
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.Select(task => task as Task).ToArray());
            var result = tasks.Select(task => task.Result).ToList();

            var list = new TagBuilder("div");
            foreach (var s in result)
            {
                var status = new TagBuilder("div");
                status.InnerHtml.Append(s);
                list.InnerHtml.AppendHtml(status);
            }

            return list.RenderBody();
        }
    }
}