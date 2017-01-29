using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;

namespace StockManage
{
    public class YahooFinanceAPI
    {
        public static async Task getStockData(EntityRecommendation Symbol)
        {
            try
            {
                string URL = $"http://finance.yahoo.com/d/quotes.csv?s={Symbol}&f=nxsac";
                string CSV;
                using (WebClient client = new WebClient())
                {
                    CSV = await client.DownloadStringTaskAsync(URL).ConfigureAwait(false);
                }
                var Line = CSV.Split('\n')[0];
                var Name = Line.Split(',')[0];
                var SC = Line.Split(',')[1];
                var Sym = Line.Split(',')[2];
                var Price = Line.Split(',')[3];
                var Change = Line.Split(',')[4];
                double result=0;
                if (Price != null && Price.Length >= 0)
                    double.TryParse(Price, out result);
                StringBuilder strReplyMessage = new StringBuilder();
                strReplyMessage.Append($"{Name}");
                strReplyMessage.Append($"\n");
                strReplyMessage.Append($"{SC}:{Sym}");
                strReplyMessage.Append($"\n");
                strReplyMessage.Append($"{result}");
                strReplyMessage.Append($"\n\n");
                strReplyMessage.Append($"\t{Change}");
            }
            catch(WebException we)
            {
                throw we;
            }
        }
    }
}