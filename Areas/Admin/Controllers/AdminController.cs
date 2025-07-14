using InventoryManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin"),Route("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            ViewBag.TotalClients = _context.Clients.Count();
            ViewBag.TotalVendor = _context.Vendors.Count();
            ViewBag.TotalGrandTotal = _context.Invoice.Sum(x => x.GrandTotal);

            // Today's sales
            ViewBag.TotalSalesToday = _context.Invoice
                .Where(x => x.Date.Date == today)
                .Sum(x => (decimal?)x.GrandTotal) ?? 0;

            // This month's sales
            ViewBag.TotalSalesThisMonth = _context.Invoice
                .Where(x => x.Date >= startOfMonth && x.Date <= today)
                .Sum(x => (decimal?)x.GrandTotal) ?? 0;

            return View();
        }

    }
}
