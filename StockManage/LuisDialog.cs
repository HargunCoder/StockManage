using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace StockManage
{
    [Serializable]
    [LuisModel ("c1cf7c1b-edc7-4f4a-a2f5-1e2484e60f5e", "906e574bcd124c8d987afa3c71fd59db")]
    public class LuisDialog :LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Sorry, I don't understand {result.Query}.");
            context.Wait(this.MessageReceived);
        }
        [LuisIntent("StockPrice")]
        public async Task StockIntent(IDialogContext context, LuisResult result)
        {
            EntityRecommendation STOCK;
            if(result.TryFindEntity("Equity", out STOCK))
            {
                await YahooFinanceAPI.getStockData(STOCK);
            }
            context.Wait(this.MessageReceived);
        }
        [LuisIntent ("Pleasentries")]
        public async Task HiIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hello");
            context.Wait(this.MessageReceived);
        }
        [LuisIntent("AddNewStock")]
        public async Task AddNewStockIntent(IDialogContext context, LuisResult result)
        {
            EntityRecommendation STOCK;
            if(result.TryFindEntity("Equity", out STOCK))
            {
                STOCK.Type = "Destination";
                await context.PostAsync($"Stock being bought...\n{STOCK.Entity} shares added to profile.");
            }
            context.Wait(this.MessageReceived);
        }
        [LuisIntent("SellStock")]
        public async Task SellStockIntent(IDialogContext context, LuisResult result)
        {
            EntityRecommendation STOCK;
            if(result.TryFindEntity("Equity", out STOCK))
            {
                STOCK.Type = "Destinaiton";
                await context.PostAsync($"Stock being sold...\n{STOCK.Entity} shares removed from profile.");
            }
            context.Wait(this.MessageReceived);
        }
    }
}