using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using SimpleFeedReader; 

namespace NewsPlugin
{
    public class NewsPlugin
    {
        [KernelFunction("get_news")]
        [Description("Get news for today's date")]
        [return: Description("A list of current news stories")]
        public async Task<List<FeedItem>> GetNewsAsync(string category)
        {
            var reader = new FeedReader();
            var feedItems = await reader.RetrieveFeedAsync(
                $"https://rss.nytimes.com/services/xml/rss/nyt/{category}.xml"
            );

            return feedItems.Take(5).ToList();
        }
    }
}
