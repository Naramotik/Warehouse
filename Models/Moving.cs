using System;

namespace UserManagement.MVC.Models

{
    public class Moving : BaseEntity
    {
        public int WarehousesFromId { get; set; }
        public Warehouses WarehousesFrom { get; set; }
        public int WarehousesToId { get; set; }
        public Warehouses WarehousesTo { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Quantity { get; set; }
        public int UnitsId { get; set; }
        public Units Units { get; set; }
        public DateTime Data { get; set; }
    }
}
