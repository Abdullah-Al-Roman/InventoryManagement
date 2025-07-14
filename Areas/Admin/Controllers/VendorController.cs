using InventoryManagement.Data;
using InventoryManagement.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin"), Route("Vendor")]
    [Authorize]
    public class VendorController : Controller
    {

        private readonly ApplicationDbContext _context; //datbase connection

        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("VendorCreate")]
        public IActionResult VendorCreate()
        {
            return View();
        }

        [HttpPost("VendorCreateSubmit")]
        public IActionResult VendorCreateSubmit(Vendors model) //method
        {

            if (model.VendorName!= null)
            {

                _context.Vendors.Add(model);
                _context.SaveChanges();

                return RedirectToAction("VendorList");

            }


             return RedirectToAction("VendorCreate");
        }

        [Route("VendorEdit")] //vendor edit e gele ,controller run
        public IActionResult VendorEdit(Guid id)
        {
            var data = _context.Vendors.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }
        [HttpPost("VendorEdit")]

        public IActionResult VendorEdit(Vendors vendor)
        {
            var datacheck = _context.Vendors.FirstOrDefault(x => x.VendorID == vendor.VendorID);

            if (datacheck != null)
            {
                datacheck.VendorName = vendor.VendorName;
                datacheck.Phone = vendor.Phone;
                datacheck.Email = vendor.Email;
                datacheck.Address = vendor.Address;

                _context.Update(datacheck);
                _context.SaveChanges();
            }

            return RedirectToAction("VendorList");
        }

        [Route("VendorList")]
        public IActionResult VendorList() //method
        {
            var datalist = _context.Vendors.ToList();


            return View(datalist);
        }
        [Route("DeleteVendor")]
        public IActionResult DeleteVendor(Guid id)
        {
            var vendor = _context.Vendors.FirstOrDefault(v => v.VendorID == id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                _context.SaveChanges();
            }

            return RedirectToAction("VendorList");
        }
       

    }
}

