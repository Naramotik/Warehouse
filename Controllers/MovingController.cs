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
    public class MovingController : Controller
    {
        ApplicationDbContext db;
        public MovingController(ApplicationDbContext context)
        {
            db = context;
        }
            [HttpGet]
            [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<IEnumerable<Moving>>> Index()
        {
            var model = await db.Moving.ToListAsync();
            var Product = await db.Product.ToListAsync();
            var WarehousesFrom = await db.Warehouses.ToListAsync();
            var WarehousesTo = await db.Warehouses.ToListAsync();
            var Units = await db.Units.ToListAsync();
            return View(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<IEnumerable<Moving>>> Create()
        {
            SelectList Product = new SelectList(db.Product, "Id", "Name");
            ViewBag.Product = Product;

            SelectList WarehousesFrom = new SelectList(db.Warehouses, "Id", "Name");
            ViewBag.WarehousesFrom = WarehousesFrom;

            SelectList WarehousesTo = new SelectList(db.Warehouses, "Id", "Name");
            ViewBag.WarehousesTo = WarehousesTo;

            return View();
        }
        [HttpPost("Create")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Moving>> Create(Moving Moving)
        {
            var prod = await db.Product.Where(x => x.Id == Moving.ProductId).FirstOrDefaultAsync();
            if (Moving == null)
            {
                return BadRequest();
            }
            Moving.Data = DateTime.Now;
            Moving.UnitsId = prod.UnitsId;
            prod.Status = true;
            db.Moving.Add(Moving);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Moving>> Delete(int id)
        {
            var Moving = await db.Moving.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Moving == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                var Product = await db.Product.Where(x => x.Id == Moving.ProductId).FirstOrDefaultAsync();
                var Units = await db.Units.Where(x => x.Id == Moving.UnitsId).FirstOrDefaultAsync();
                var WarehousesFrom = await db.Warehouses.Where(x => x.Id == Moving.WarehousesFromId).FirstOrDefaultAsync();
                var WarehousesTo = await db.Warehouses.Where(x => x.Id == Moving.WarehousesToId).FirstOrDefaultAsync();
                return View(Moving);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Moving>> Delete1(int id)
        {
            Moving Moving = db.Moving.FirstOrDefault(x => x.Id == id);
            if (Moving == null)
            {
                return BadRequest();
            }
            db.Moving.Remove(Moving);
            var prod = await db.Product.Where(x => x.Id == Moving.ProductId).FirstOrDefaultAsync();
            prod.Status = false;
            await db.SaveChangesAsync();
            //return prod;
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Moving>> Update(int id)
        {
            var Moving = await db.Moving.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Moving == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                SelectList Product = new SelectList(db.Product, "Id", "Name");
                ViewBag.Product = Product;
                var prod = await db.Product.Where(x => x.Id == Moving.ProductId).FirstOrDefaultAsync();
                prod.Status = false;
                SelectList WarehousesFrom = new SelectList(db.Warehouses, "Id", "Name");
                ViewBag.WarehousesFrom = WarehousesFrom;

                SelectList WarehousesTo = new SelectList(db.Warehouses, "Id", "Name");
                ViewBag.WarehousesTo = WarehousesTo;
                return View(Moving);
            }
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<Moving>> Update(Moving Moving)
        {
            if (Moving == null)
            {
                return BadRequest();
            }
            if (!db.Moving.Any(x => x.Id == Moving.Id))
            {
                return BadRequest();
            }
            Moving.Data = DateTime.Now;
            var prod = await db.Product.Where(x => x.Id == Moving.ProductId).FirstOrDefaultAsync();
            prod.Status = true;
            Moving.UnitsId = prod.UnitsId;
            db.Moving.Update(Moving);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }
    }
}