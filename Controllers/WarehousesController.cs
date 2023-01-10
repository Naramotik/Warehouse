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
    public class WarehousesController : Controller
    {
        ApplicationDbContext db;
        public WarehousesController(ApplicationDbContext context)
        {
            db = context;
        }


        [HttpGet]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Warehouses>>> Index()
        {
            var model = await db.Warehouses.ToListAsync();
            return View(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Warehouses>>> Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Warehouses>> Create(Warehouses Warehouses)
        {
            if (Warehouses == null)
            {
                return BadRequest();
            }
            db.Warehouses.Add(Warehouses);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Warehouses>> Delete(int id)
        {
            var Warehouses = await db.Warehouses.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Warehouses == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Warehouses);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Warehouses>> Delete1(int id)
        {
            Warehouses Warehouses = db.Warehouses.FirstOrDefault(x => x.Id == id);
            if (Warehouses == null)
            {
                return BadRequest();
            }
            db.Warehouses.Remove(Warehouses);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Warehouses>> Update(int id)
        {
            var Warehouses = await db.Warehouses.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Warehouses == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Warehouses);
            }
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Warehouses>> Update(Warehouses Warehouses)
        {
            if (Warehouses == null)
            {
                return BadRequest();
            }
            if (!db.Warehouses.Any(x => x.Id == Warehouses.Id))
            {
                return BadRequest();
            }
            db.Warehouses.Update(Warehouses);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }
    }
}
