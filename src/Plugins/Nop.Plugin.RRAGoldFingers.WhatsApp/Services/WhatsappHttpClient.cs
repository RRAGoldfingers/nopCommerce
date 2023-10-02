using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.RRAGoldFingers.WhatsApp.Models;

namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Services;

/// <summary>
/// Represents HTTP client to request third-party services
/// </summary>
public class WhatsappHttpClient
{
    protected readonly HttpClient _httpClient;
    protected readonly WhatsappSettings _whatsappSettings;

    public WhatsappHttpClient(HttpClient httpClient, WhatsappSettings whatsappSettings)
    {
        _httpClient = httpClient;
        _whatsappSettings = whatsappSettings;
    }

    public async Task Post(object request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_whatsappSettings.WhatsappUrl, content);
    }
}