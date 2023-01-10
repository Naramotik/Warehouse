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
    public class ProductController : Controller
    {
        ApplicationDbContext db;
        public ProductController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {

            var model1 = await db.Product.ToListAsync();
            var model2 = await db.Inventory.ToListAsync();
            var model3 = await db.Orders.ToListAsync();
            var model4 = await db.RegistrationWrite.ToListAsync();

            var uni = await db.Units.ToListAsync();
            var prod = await db.Partners.ToListAsync();

            foreach (var m1 in model1)
            {
                foreach (var m2 in model2)
                { 
                    if (m1.Id==m2.ProductId)
                    {
                        m1.Status = true;
                        await db.SaveChangesAsync();
                        break;
                    }
                }
                foreach (var m3 in model3)
                {
                    if (m1.Id == m3.ProductId)
                    {
                        m1.Status = true;
                        await db.SaveChangesAsync();
                        break;
                    }
                }
                foreach (var m4 in model4)
                {
                    if (m1.Id == m4.ProductId)
                    {
                        m1.Status = true;
                        await db.SaveChangesAsync();
                        break;
                    }
                }
            }
            return View(model1);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Product>>> Create()
        {
            SelectList partner = new SelectList(db.Partners, "Id", "Name");
            ViewBag.Partners = partner;

            SelectList Units = new SelectList(db.Units, "Id", "Name");
            ViewBag.Units = Units;

            return View();
        }
 
        [HttpPost("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Product>> Create(Product prod)
        {
            if (prod == null)
            {
                return BadRequest();
            }
            db.Product.Add(prod);
            var uni = await db.Units.Where(x => x.Id == prod.UnitsId).FirstOrDefaultAsync();
            uni.Status = true;
            var prov = await db.Partners.Where(x => x.Id == prod.ProviderId).FirstOrDefaultAsync();
            prov.Status = true;
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var prod = await db.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (prod == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(prod);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Product>> Delete1(int id)
        {
            Product prod = db.Product.FirstOrDefault(x => x.Id == id);
            if (prod == null)
            {
                return BadRequest();
            }
            db.Product.Remove(prod);
            var uni = await db.Units.Where(x => x.Id == prod.UnitsId).FirstOrDefaultAsync();
            uni.Status = false;
            var prov = await db.Partners.Where(x => x.Id == prod.ProviderId).FirstOrDefaultAsync();
            prov.Status = false;
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Product>> Update(int id)
        {
            var prod = await db.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (prod == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                SelectList partner = new SelectList(db.Partners, "Id", "Name");
                ViewBag.Partners = partner;

                SelectList Units = new SelectList(db.Units, "Id", "Name");
                ViewBag.Units = Units;
                var uni = await db.Units.Where(x => x.Id == prod.UnitsId).FirstOrDefaultAsync();
                uni.Status = false;
                var prov = await db.Partners.Where(x => x.Id == prod.ProviderId).FirstOrDefaultAsync();
                prov.Status = false;
                await db.SaveChangesAsync();
                return View(prod);
            }
            
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Product>> Update(Product prod)
        {
            if (prod == null)
            {
                return BadRequest();
            }
            if (!db.Product.Any(x => x.Id == prod.Id))
            {
                return BadRequest();
            }
            db.Product.Update(prod);
            var uni = await db.Units.Where(x => x.Id == prod.UnitsId).FirstOrDefaultAsync();
            uni.Status = true;
            var prov = await db.Partners.Where(x => x.Id == prod.ProviderId).FirstOrDefaultAsync();
            prov.Status = true;
            await db.SaveChangesAsync();
            //return Ok(prod);
            return RedirectToActionPermanent("Index");
        }
    }
}
