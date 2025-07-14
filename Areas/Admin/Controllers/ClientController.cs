using InventoryManagement.Data;
using InventoryManagement.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin"), Route("Client")]
    [Authorize]
    public class ClientController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("AddClient")]
        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost("AddClientSubmit")]
        public IActionResult AddClientSubmit(Clients model)
        {

            if (model.ClientName != null)
            {

                _context.Clients.Add(model);
                _context.SaveChanges();

                return RedirectToAction("ClientList");

            }


            return RedirectToAction("AddClient");
        }
        [Route("EditClient")]
        public IActionResult EditClient(Guid id)
        {
            var data = _context.Clients.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost("EditClient")]
        public IActionResult EditClient(Clients client)
        {
            var existingClient = _context.Clients.FirstOrDefault(x => x.ClientID == client.ClientID);
            if (existingClient != null)
            {
                existingClient.ClientName = client.ClientName;
                existingClient.Phone = client.Phone;
                existingClient.Email = client.Email;
                existingClient.Address = client.Address;

                _context.Update(existingClient);
                _context.SaveChanges();
            }

            return RedirectToAction("ClientList");
        }


        [Route("ClientList")]
        public IActionResult ClientList()
        {
            var datalist = _context.Clients.ToList();

            return View(datalist);
        }

        [Route("DeleteClient")]
        public IActionResult DeleteClient(Guid id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.ClientID == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }

            return RedirectToAction("ClientList");
        }
    }
}



