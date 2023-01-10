using System;
namespace UserManagement.MVC.Models

{
    public class RegistrationWrite : BaseEntity//оприходование/списание
    {
        public int RegistrationWriteTypeId { get; set; }
        public RegistrationWriteType RegistrationWriteType { get; set; } 
        public int WarehousesId { get; set; }
        public Warehouses Warehouses { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Quantity { get; set; }
        public int UnitsId { get; set; }
        public Units Units { get; set; }
        public DateTime Data { get; set; }
    }
}
