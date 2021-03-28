using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;

namespace DieUnausstehlichenWeb.Helper
{
    public static class Text
    {
        public static IEnumerable<string> LoremIpsum(int minWords, int maxWords,
            int minSentences, int maxSentences,
            int numParagraphs)
        {
            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var rand = new Random();
            var result = new List<string>();

            for (var p = 0; p < numParagraphs; p++)
            {
                var sb = new StringBuilder();
                var numSentences = rand.Next(maxSentences - minSentences)
                                   + minSentences + 1;
                for (var s = 0; s < numSentences; s++)
                {
                    var numWords = rand.Next(maxWords - minWords) + minWords + 1;
                    for (var w = 0; w < numWords; w++)
                    {
                        if (w > 0)
                        {
                            sb.Append(' ');
                        }

                        sb.Append(words[rand.Next(words.Length)]);
                    }

                    sb.Append(". ");
                }

                result.Add(sb.ToString());
            }

            return result;
        }
    }
}