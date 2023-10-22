using System.Threading.Tasks;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.Zettle.Services;
using Nop.Plugin.RRAGoldFingers.WhatsApp.Models;
using Nop.Services.Customers;
using Nop.Services.Events;

namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Order.Consumers;

public sealed class OrderStatusChangedEventConsumer : IConsumer<OrderStatusChangedEvent>
{
    private readonly WhatsappService _whatsapp;
    private readonly ICustomerService _customerService;

    public OrderStatusChangedEventConsumer(
        WhatsappService whatsapp,
        ICustomerService customerService)
    {
        _whatsapp = whatsapp;
        _customerService = customerService;
    }

    public Task HandleEventAsync(OrderStatusChangedEvent eventMessage)
    {
        return SendWhatsappMessageToCustomer(eventMessage);
    }

    private async Task SendWhatsappMessageToCustomer(OrderStatusChangedEvent eventMessage)
    {
        var customer = await GetCustomer(eventMessage.Order.CustomerId);
        var msg = BuildWhatsappMessageForCustomer(eventMessage, customer);
        await _whatsapp.SendMessage(new ChatMessage(msg, "", customer.Phone));
    }

    private async Task<Customer> GetCustomer(int customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        return customer;
    }

    private static string BuildWhatsappMessageForCustomer(OrderStatusChangedEvent eventMessage, Customer customer)
    {
        var msg = $"Hi, {customer.FirstName}, your order ID '{eventMessage.Order.OrderGuid}' status '{eventMessage.PreviousOrderStatus}' has changed to '{eventMessage.Order.OrderStatus}'";
        return msg;
    }
}