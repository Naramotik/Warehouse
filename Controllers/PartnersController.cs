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
    public class PartnersController : Controller
    {
    ApplicationDbContext db;
    public PartnersController(ApplicationDbContext context)
    {
        db = context;
    }


    [HttpGet]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Partners>>> Index()
        {
            var model = await db.Partners.ToListAsync();
            return View(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Partners>>> Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Partners>> Create(Partners Partners)
        {
            if (Partners == null)
            {
                return BadRequest();
            }
            db.Partners.Add(Partners);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Partners>> Delete(int id)
        {
            var Partners = await db.Partners.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Partners == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Partners);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Partners>> Delete1(int id)
        {
            Partners Partners = db.Partners.FirstOrDefault(x => x.Id == id);
            if (Partners == null)
            {
                return BadRequest();
            }
            db.Partners.Remove(Partners);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Partners>> Update(int id)
        {
            var Partners = await db.Partners.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Partners == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Partners);
            }
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Partners>> Update(Partners Partners)
        {
            if (Partners == null)
            {
                return BadRequest();
            }
            if (!db.Partners.Any(x => x.Id == Partners.Id))
            {
                return BadRequest();
            }
            db.Partners.Update(Partners);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }
    }
}
