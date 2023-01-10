using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.MVC.Data;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Controllers
{
    [Route("[controller]")]
    public class InventoryController : Controller
    {
        ApplicationDbContext db;
        public InventoryController(ApplicationDbContext context)
        {
            db = context;
        }
        [HttpGet]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<IEnumerable<Inventory>>> Index()
        {

            var model = await db.Inventory.ToListAsync();
            var Product = await db.Product.ToListAsync();
            var Units = await db.Units.ToListAsync();
            var Warehouses = await db.Warehouses.ToListAsync();
            return View(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<IEnumerable<Inventory>>> Create()
        {
            SelectList Product = new SelectList(db.Product, "Id", "Name");
            ViewBag.Product = Product;

            SelectList Warehouses = new SelectList(db.Warehouses, "Id", "Name");
            ViewBag.Warehouses = Warehouses;

            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Inventory>> Create(Inventory Inventory)
        {
            var Product = await db.Product.Where(x => x.Id == Inventory.ProductId).FirstOrDefaultAsync();
            if (Inventory == null)
            {
                return BadRequest();
            }
            Inventory.UnitsId = Product.UnitsId;
            Inventory.Data = DateTime.Now;
            Product.Status = true;
            Inventory.UnitsId = Product.UnitsId;
            db.Inventory.Add(Inventory);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Inventory>> Delete(int id)
        {
            var Inventory = await db.Inventory.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Inventory == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                var Product = await db.Product.Where(x => x.Id == Inventory.ProductId).FirstOrDefaultAsync();
                var Units = await db.Units.Where(x => x.Id == Inventory.UnitsId).FirstOrDefaultAsync();
                var Warehouses = await db.Warehouses.Where(x => x.Id == Inventory.WarehousesId).FirstOrDefaultAsync();
                return View(Inventory);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Inventory>> Delete1(int id)
        {
            Inventory Inventory = db.Inventory.FirstOrDefault(x => x.Id == id);
            if (Inventory == null)
            {
                return BadRequest();
            }
            db.Inventory.Remove(Inventory);
            var prod = await db.Product.Where(x => x.Id == Inventory.ProductId).FirstOrDefaultAsync();
            prod.Status = false;
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Inventory>> Update(int id)
        {
            var Inventory = await db.Inventory.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Inventory == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                SelectList Product = new SelectList(db.Product, "Id", "Name");
                ViewBag.Product = Product;
                var prod = await db.Product.Where(x => x.Id == Inventory.ProductId).FirstOrDefaultAsync();
                prod.Status = false;
                SelectList Warehouses = new SelectList(db.Warehouses, "Id", "Name");
                ViewBag.Warehouses = Warehouses;
                return View(Inventory);
            }
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Inventory>> Update(Inventory Inventory)
        {
            if (Inventory == null)
            {
                return BadRequest();
            }
            if (!db.Inventory.Any(x => x.Id == Inventory.Id))
            {
                return BadRequest();
            }
            Inventory.Data = DateTime.Now;
            var prod = await db.Product.Where(x => x.Id == Inventory.ProductId).FirstOrDefaultAsync();
            prod.Status = true;
            Inventory.UnitsId = prod.UnitsId;
            db.Inventory.Update(Inventory);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }
    }
}
