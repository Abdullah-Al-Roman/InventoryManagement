using InventoryManagement.DataModel;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data;

public class ApplicationDbContext : IdentityDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<CompanyProfile> CompanyProfile { get; set; }
    public DbSet<Vendors> Vendors { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Categorys> Categorys { get; set; }
    public DbSet<Clients> Clients { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
  //  public DbSet<Productdetails> Productdetails { get; set; }
    public DbSet<InvoiceItems> InvoiceItems { get; set; }
    
}
