using InventoryManagement.Areas.Admin.Data;
using InventoryManagement.Data;
using InventoryManagement.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;


namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin"), Route("Invoice")]
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("SaleCreate")]
        public IActionResult SaleCreate()
        {
            return View();
        }

        [HttpGet("SearchClientByPhone")]
        public IActionResult SearchClientByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length < 4)
            {
                return Json(null);
            }
            try
            {
                var references = _context.Clients
                    .Where(x => x.Phone.StartsWith(phone))
                    .Select(x => new
                    {
                        x.ClientID,
                        x.ClientName,
                        x.Phone,
                        x.Address
                    })
                    .Take(5)
                    .ToList();

                return Json(new { success = true, references });
            }
            catch
            {
                return Json(new { success = false, message = "Error"});
            }
        }
        [HttpGet("SearchProductByName")]
        public IActionResult SearchProductByName(string description)
        {
             
            try
            {
                var references = _context.Products
                    .Where(x => x.ProductName.StartsWith(description))
                    .Select(x => new
                    {
                        x.ProductID,
                        x.ProductName,
                        x.Price,
                    })
                    .Take(5)
                    .ToList();

                return Json(new { success = true, references });
            }
            catch
            {
                return Json(new { success = false, message = "Error"});
            }
        }


        [HttpPost("OrderSummarySubmit")]
        public IActionResult OrderSummarySubmit(InvoiceVM model)
        {
            if (model == null || model.InvoiceItems == null || model.Pay < 0)
            {
                return Json(new { success = false, message = "Invalid invoice data." });
            }

            try
            {
                // Step 1: Handle Client
                Clients client = null;
                if (model.Client != null && !string.IsNullOrWhiteSpace(model.Client.Phone))
                {
                    client = _context.Clients.FirstOrDefault(x => x.Phone == model.Client.Phone);
                    if (client != null)
                    {
                        // Update client info
                        client.ClientName = model.Client.ClientName;
                        client.Address = model.Client.Address;
                        _context.Clients.Update(client);
                    }
                    else
                    {
                        client = model.Client;
                        _context.Clients.Add(client);
                        model.Client.ClientID = client.ClientID;
                    }

                    _context.SaveChanges(); // Save client changes
                }

                // Step 2: Check Product Quantities
                foreach (var item in model.InvoiceItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.ProductID == item.ProductId);
                    if (product == null)
                    {
                        return Json(new { success = false, message = $"Product not found: {item.ProductName}" });
                    }

                    if (product.Quantity < item.Quantity)
                    {
                        return Json(new { success = false, message = $"Not enough stock for product: {product.ProductName}" });
                    }
                }

                // Step 3: Save Invoice
                Invoice invoiceData = new Invoice
                {
                    Date = model.Date,
                    ClientID = client.ClientID,
                    UnitPrice = model.UnitPrice,
                    Discount = model.Discount,
                    Subtotal = model.Subtotal,
                    GrandTotal = model.GrandTotal,
                    Pay = model.Pay,
                    Due = model.Due,
                    PaymentType = model.PaymentType,
                    Slip = model.Slip
                };

                _context.Invoice.Add(invoiceData);
                _context.SaveChanges();

                // Step 4: Save Invoice Items & Deduct Quantity
                foreach (var item in model.InvoiceItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.ProductID == item.ProductId);
                    product.Quantity -= item.Quantity;
                    _context.Products.Update(product);

                    var invoiceItem = new InvoiceItems
                    {
                        InvoiceId = invoiceData.InvoiceId,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Total = item.Total
                    };

                    _context.InvoiceItems.Add(invoiceItem);
                }

                _context.SaveChanges();

                return Json(new { success = true, message = "Invoice processed successfully." });
            }
            catch
            {
                return Json(new { success = false, message = "An error occurred while processing the invoice." });
            }
        }



        [Route("SaleList")]
        public IActionResult SaleList()
        {
            var datalist = (from invoice in _context.Invoice
                            join client in _context.Clients
                            on invoice.ClientID equals client.ClientID
                            select new InvoiceVM
                            {
                                InvoiceId = invoice.InvoiceId,
                                Date = invoice.Date,                             
                                Subtotal = invoice.Subtotal,
                                Discount = invoice.Discount,
                                GrandTotal = invoice.GrandTotal,
                                Pay = invoice.Pay,
                                Due = invoice.Due,
                                PaymentType = invoice.PaymentType,
                                Slip = invoice.Slip,
                                ClientName = client.ClientName
                            }).ToList();

            return View(datalist);
        }
        [Route("InvoiceDetails")]
        public IActionResult InvoiceDetails(Guid id)
        {
            var invoice = _context.Invoice.FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice); // pass invoice as model
        }

        [Route("SaleReport")]
        public IActionResult SaleReport(DateTime? fdate, DateTime? tdate)
        {
            var query = from invoice in _context.Invoice
                        join client in _context.Clients on invoice.ClientID equals client.ClientID
                        select new InvoiceVM
                        {
                            InvoiceId = invoice.InvoiceId,
                            Date = invoice.Date,
                            Subtotal = invoice.Subtotal ?? 0,
                            Discount = invoice.Discount,
                            GrandTotal = invoice.GrandTotal ?? 0,
                            Pay = invoice.Pay,
                            Due = invoice.Due,
                            PaymentType = invoice.PaymentType,
                            Slip = invoice.Slip,
                            ClientName = client.ClientName
                        };

            if (fdate.HasValue && tdate.HasValue)
            {
                query = query.Where(x => x.Date >= fdate.Value && x.Date <= tdate.Value);
            }

            var list = query.ToList();

            ViewBag.FromDate = fdate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = tdate?.ToString("yyyy-MM-dd");

            ViewBag.TotalSubtotal = list.Sum(x => x.Subtotal);
            ViewBag.TotalDiscount = list.Sum(x => x.Discount);
            ViewBag.TotalPay = list.Sum(x => x.Pay);
            ViewBag.TotalDue = list.Sum(x => x.Due);

            return View(list);
        }


    }
}

