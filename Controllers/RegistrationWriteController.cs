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
    public class RegistrationWriteController : Controller
    {
        ApplicationDbContext db;
        public RegistrationWriteController(ApplicationDbContext context)
        {
            db = context;
        }


        [HttpGet]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<IEnumerable<RegistrationWrite>>> Index()
        {
            var model = await db.RegistrationWrite.ToListAsync();
            var RegistrationWriteType = await db.RegistrationWriteType.ToListAsync();
            var Warehouses = await db.Warehouses.ToListAsync();
            var Product = await db.Product.ToListAsync();
            var Units = await db.Units.ToListAsync();

            foreach (var m in model)
            {
                if (m.Quantity==null)
                {
                    db.RegistrationWrite.Remove(m);
                    await db.SaveChangesAsync();
                    return RedirectToActionPermanent("Index");
                }
            }

            return View(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<IEnumerable<RegistrationWrite>>> Create()
        {
            SelectList RegistrationWriteType = new SelectList(db.RegistrationWriteType, "Id", "Name");
            ViewBag.RegistrationWriteType = RegistrationWriteType;

            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<RegistrationWrite>> Create(RegistrationWrite RegistrationWrite)
        {
            if (RegistrationWrite == null)
            {
                return BadRequest();
            }
            RegistrationWrite.Data = DateTime.Now;
            RegistrationWrite.WarehousesId = 1;
            RegistrationWrite.ProductId = 1;
            RegistrationWrite.UnitsId = 1;
            db.RegistrationWrite.Add(RegistrationWrite);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Update", new { id = RegistrationWrite.Id });
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<RegistrationWrite>> Delete(int id)
        {
            var RegistrationWrite = await db.RegistrationWrite.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (RegistrationWrite == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                var RegistrationWriteType = await db.RegistrationWriteType.Where(x => x.Id == RegistrationWrite.RegistrationWriteTypeId).FirstOrDefaultAsync();
                var Product = await db.Product.Where(x => x.Id == RegistrationWrite.ProductId).FirstOrDefaultAsync();
                var Units = await db.Units.Where(x => x.Id == RegistrationWrite.UnitsId).FirstOrDefaultAsync();
                var Warehouses = await db.Warehouses.Where(x => x.Id == RegistrationWrite.WarehousesId).FirstOrDefaultAsync();
                return View(RegistrationWrite);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<RegistrationWrite>> Delete1(int id)
        {
            RegistrationWrite RegistrationWrite = db.RegistrationWrite.FirstOrDefault(x => x.Id == id);
            if (RegistrationWrite == null)
            {
                return BadRequest();
            }
            db.RegistrationWrite.Remove(RegistrationWrite);
            var prod = await db.Product.Where(x => x.Id == RegistrationWrite.ProductId).FirstOrDefaultAsync();
            prod.Status = false;
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<RegistrationWrite>> Update(int id)
        {
            var RegistrationWrite = await db.RegistrationWrite.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (RegistrationWrite == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                SelectList Product = new SelectList(db.Product, "Id", "Name");
                ViewBag.Product = Product;

                SelectList Warehouses = new SelectList(db.Warehouses, "Id", "Name");
                ViewBag.Warehouses = Warehouses;

                SelectList RegistrationWriteType = new SelectList(db.RegistrationWriteType, "Id", "Name");
                ViewBag.RegistrationWriteType = RegistrationWriteType;
                
                await db.SaveChangesAsync();
                return View(RegistrationWrite);
            }
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Storekeeper")]
        public async Task<ActionResult<RegistrationWrite>> Update(RegistrationWrite RegistrationWrite)
        {
           if (RegistrationWrite == null)
            {
                return BadRequest();
            }
            if (!db.RegistrationWrite.Any(x => x.Id == RegistrationWrite.Id))
            {
                return BadRequest();
            }
            
            RegistrationWrite.Data = DateTime.Now;
            var prod = await db.Product.Where(x => x.Id == RegistrationWrite.ProductId).FirstOrDefaultAsync();
            prod.Status = true;
            RegistrationWrite.UnitsId = prod.UnitsId;
            db.RegistrationWrite.Update(RegistrationWrite);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }
    }
}