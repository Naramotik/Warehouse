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
    public class UnitsController : Controller
    {
        ApplicationDbContext db;
        public UnitsController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Units>>> Index()
        {
            var model = await db.Units.ToListAsync();
            return View(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<IEnumerable<Units>>> Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Units>> Create(Units Units)
        {
            if (Units == null)
            {
                return BadRequest();
            }
            db.Units.Add(Units);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Units>> Delete(int id)
        {
            var Units = await db.Units.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Units == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Units);
            }
        }
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Units>> Delete1(int id)
        {
            Units Units = db.Units.FirstOrDefault(x => x.Id == id);
            if (Units == null)
            {
                return BadRequest();
            }
            db.Units.Remove(Units);
            await db.SaveChangesAsync();
            //return prod;
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Units>> Update(int id)
        {
            var Units = await db.Units.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Units == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Units);
            }
        }

        [HttpPost("Update/{id}")]
        [Authorize(Roles = "Supply")]
        public async Task<ActionResult<Units>> Update(Units Units)
        {
            if (Units == null)
            {
                return BadRequest();
            }
            if (!db.Units.Any(x => x.Id == Units.Id))
            {
                return BadRequest();
            }
            db.Units.Update(Units);
            await db.SaveChangesAsync();
            //return Ok(prod);
            return RedirectToActionPermanent("Index");
        }
    }
}