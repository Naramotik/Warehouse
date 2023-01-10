using System;

namespace UserManagement.MVC.Models

{
    public class Orders : BaseEntity
    {
        public int OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime Data { get; set; }
        public int PartnersId { get; set; }
        public Partners Partners { get; set; }
        public int WarehousesId { get; set; }
        public Warehouses Warehouses { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Quantity { get; set; }
        public int UnitsId { get; set; }
        public Units Units { get; set; }
        public string Comment { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool Status { get; set; }
    }
}
