using System;

namespace UserManagement.MVC.Models

{
    public class Inventory : BaseEntity 
    {
        public DateTime Data { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string QuantityFact { get; set; }
        public string QuantityAcc { get; set; }
        public int UnitsId { get; set; }
        public Units Units { get; set; }
        public int WarehousesId { get; set; }
        public Warehouses Warehouses { get; set; }
        
    }
}
