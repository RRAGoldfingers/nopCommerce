using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.Zettle.Services;
using Nop.Plugin.RRAGoldFingers.WhatsApp.Models;
using Nop.Services.Catalog;

namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Controllers;

public class WhatsappWebhookController : Controller
{
    protected readonly WhatsappService _whatsapp;
    protected readonly IProductService _productService;

    public WhatsappWebhookController(WhatsappService whatsapp, IProductService productService)
    {
        _whatsapp = whatsapp;
        _productService = productService;
    }    

    [HttpPost]
    public async Task<IActionResult> Search([FromBody]ChatMessage msg)
    {
        var products = await _productService.SearchProductsAsync(keywords: msg.MessageBody);
        var resp = products.Select(o => new { o.Name, o.Price, Available = o.StockQuantity > 0 }).ToArray();
        var from = "NopCommerce Whatsapp plugin";
        var to = msg.FromNumber;

        await Task.Delay(1000);
        await _whatsapp.SendMessage(new ChatMessage("I am searching...", from, to));
        await Task.Delay(5000);

        if (products.Any())
        {
            foreach (var product in resp)
            {
                await _whatsapp.SendMessage(new ChatMessage($"{product.Name}: {product.Price} available: {product.Available}", from, to));
            }
        }
        else
        {
            await _whatsapp.SendMessage(new ChatMessage("Nothing found", from, to));
        }

        return Json(resp);
    }
}


