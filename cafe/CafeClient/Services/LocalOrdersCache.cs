using CafeClient.DTOs;

namespace CafeClient.Services;

public static class LocalOrdersCache
{
    private static readonly Dictionary<int, OrderResponseDto?> CacheBill = new Dictionary<int, OrderResponseDto?>();

    public static void Save(OrderResponseDto? order)
    {
        if(order == null || order.OrderId == 0) return;
        CacheBill[order.OrderId] = order;
    }

    public static bool TryGet(int orderId, out OrderResponseDto? cacheOrder)
    {
        return CacheBill.TryGetValue(orderId, out cacheOrder);
    }

    public static void Clear(int orderId)
    {
        CacheBill.Remove(orderId);
    }
}