namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Models;

public record ChatMessage
{
    public ChatMessage()
    {
    }

    public ChatMessage(string messageBody, string fromNumber, string toNumber)
    {
        MessageBody = messageBody;
        FromNumber = fromNumber;
        ToNumber = toNumber;
    }

    public string FromNumber { get; set; }
    public string ToNumber { get; set; }
    public string MessageBody { get; set; }
}