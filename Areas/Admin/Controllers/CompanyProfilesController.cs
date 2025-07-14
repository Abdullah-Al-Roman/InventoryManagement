using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.DataModel;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CompanyProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompanyProfile.ToListAsync());
        }

        // GET: Admin/CompanyProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfile = await _context.CompanyProfile
                .FirstOrDefaultAsync(m => m.CompanyProfileId == id);
            if (companyProfile == null)
            {
                return NotFound();
            }

            return View(companyProfile);
        }

        // GET: Admin/CompanyProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyProfileId,CompanyName,Address,Phone,Email,Website,LogoPath,Slogan,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] CompanyProfile companyProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyProfile);
        }

        // GET: Admin/CompanyProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfile = await _context.CompanyProfile.FindAsync(id);
            if (companyProfile == null)
            {
                return NotFound();
            }
            return View(companyProfile);
        }

        // POST: Admin/CompanyProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyProfileId,CompanyName,Address,Phone,Email,Website,LogoPath,Slogan,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] CompanyProfile companyProfile)
        {
            if (id != companyProfile.CompanyProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyProfileExists(companyProfile.CompanyProfileId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(companyProfile);
        }

        // GET: Admin/CompanyProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfile = await _context.CompanyProfile
                .FirstOrDefaultAsync(m => m.CompanyProfileId == id);
            if (companyProfile == null)
            {
                return NotFound();
            }

            return View(companyProfile);
        }

        // POST: Admin/CompanyProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyProfile = await _context.CompanyProfile.FindAsync(id);
            if (companyProfile != null)
            {
                _context.CompanyProfile.Remove(companyProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyProfileExists(int id)
        {
            return _context.CompanyProfile.Any(e => e.CompanyProfileId == id);
        }
    }
}
