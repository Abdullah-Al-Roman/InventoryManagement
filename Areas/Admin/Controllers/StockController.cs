using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
    public class StockController : Controller
    {
        public IActionResult AddStock()
        {
            return View();
        }
    }
}
