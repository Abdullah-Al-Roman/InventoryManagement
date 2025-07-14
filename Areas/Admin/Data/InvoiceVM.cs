using InventoryManagement.DataModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Areas.Admin.Data
{
    
    public class InvoiceVM
    {
        
        public Guid InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public Guid ClientID { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }
        public float? Subtotal { get; set; }
        public float? GrandTotal { get; set; }
        public float Pay { get; set; }
        public float Due { get; set; }
        public string? PaymentType { get; set; }
        public int Slip { get; set; }
        public List<InvoiceItemsVM> InvoiceItems { get; set; }
        public Clients Client { get; set; }
        public string? ClientName { get; set; }

    }

    public class InvoiceItemsVM
    {
     
        public Guid ProductId { get; set; }
        public Guid InvoiceItemsId { get; set; }
        public Guid InvoiceId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Total { get; set; }

    }
}
