using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.Zettle.Services;

namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Controllers;

public class WhatsappWebhookController : Controller
{
    #region Fields

    protected readonly WhatsappService _whatsapp;

    #endregion

    #region Ctor

    public WhatsappWebhookController(WhatsappService whatsapp)
    {
        _whatsapp = whatsapp;
    }

    #endregion

    #region Methods

    [HttpPost]
    public async Task<IActionResult> Webhook(WebhookRequest request)
    {
        await _whatsapp.PostReply(request);
        return Ok($"You requested total {request.Quantity} of {request.Name} @ {DateTime.Now}");
    }

    #endregion
}

public record WebhookRequest(string Name, int Quantity);