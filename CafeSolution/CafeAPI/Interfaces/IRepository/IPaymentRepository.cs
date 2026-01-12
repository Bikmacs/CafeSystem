using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface IPaymentRepository
    {
        Task AddPaymantAsync(Payment payment);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<Payment?> GetPaymentByOrderAsync(int numOrder);
        Task<Payment?> GetPaymentsByMethodAsync();
        Task<Payment?> UpdatePaymentAsync(string id, Payment payment);
    }
}
