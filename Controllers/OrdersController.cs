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
    public class OrdersController : Controller
    {
        ApplicationDbContext db;
        public OrdersController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> Index()
        {

            var model = await db.Orders.ToListAsync();
            var OrderType = await db.OrderType.ToListAsync();
            var Partners = await db.Partners.ToListAsync();
            var Warehouses = await db.Warehouses.ToListAsync();
            var Product = await db.Product.ToListAsync();
            var Units = await db.Units.ToListAsync();
            var OrderStatus = await db.OrderStatus.ToListAsync();
            return View(model);
        }

        [HttpGet("Create")]
        public async Task<ActionResult<IEnumerable<Orders>>> Create()
        {
            SelectList OrderType = new SelectList(db.OrderType, "Id", "Name");
            ViewBag.OrderType = OrderType;

            return View();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Orders>> Create(Orders Orders)
        {
            if (Orders == null)
            {
                return BadRequest();
            }
            db.Orders.Add(Orders);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{id}")]
        public async Task<ActionResult<Orders>> Delete(int id)
        {
            var Orders = await db.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Orders == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Orders);
            }
        }
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<Orders>> Delete1(int id)
        {
            Orders Orders = db.Orders.FirstOrDefault(x => x.Id == id);
            if (Orders == null)
            {
                return BadRequest();
            }
            db.Orders.Remove(Orders);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Update/{id}")]
        public async Task<ActionResult<Orders>> Update(int id)
        {
            var Orders = await db.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Orders == null)
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(Orders);
            }
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult<Orders>> Update(Orders Orders)
        {
            if (Orders == null)
            {
                return BadRequest();
            }
            if (!db.Orders.Any(x => x.Id == Orders.Id))
            {
                return BadRequest();
            }
            db.Orders.Update(Orders);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Index");
        }

    }
}
