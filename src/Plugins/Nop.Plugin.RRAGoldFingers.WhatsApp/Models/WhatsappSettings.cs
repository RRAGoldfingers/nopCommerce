using Nop.Core.Configuration;

namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Models;

public class WhatsappSettings : ISettings
{
    public string WhatsappUrl { get; set; }
}
