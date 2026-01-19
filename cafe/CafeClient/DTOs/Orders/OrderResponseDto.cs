using CafeClient.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel; // Важно!
using System.Runtime.CompilerServices; // Важно!

namespace CafeClient.DTOs
{
    public class OrderResponseDto : INotifyPropertyChanged
    {
        public int OrderId { get; set; }
        public string? UserName { get; set; }
        public int UserId { get; set; }
        public int? TableNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public string WaitingTime => (DateTime.Now - CreatedAt).ToString(@"hh\:mm\:ss");
        public void UpdateTimeUI()
        {
            OnPropertyChanged(nameof(WaitingTime));
        }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public ObservableCollection<OrderItemDto> Items { get; set; } = new();
        public ObservableCollection<BillDto> Bills { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}