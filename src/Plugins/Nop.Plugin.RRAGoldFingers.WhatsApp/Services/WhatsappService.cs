using System.Threading.Tasks;
using Nop.Plugin.RRAGoldFingers.WhatsApp.Models;
using Nop.Plugin.RRAGoldFingers.WhatsApp.Services;

namespace Nop.Plugin.Misc.Zettle.Services
{
    /// <summary>
    /// Represents the plugin service
    /// </summary>
    public class WhatsappService
    {
        protected readonly WhatsappHttpClient _whatsappHttpClient;

        public WhatsappService(WhatsappHttpClient whatsappHttpClient)
        {
            _whatsappHttpClient = whatsappHttpClient;
        }

        public async Task SendMessage(ChatMessage msg)
        {
            await _whatsappHttpClient.Post(msg);
        }
    }
}