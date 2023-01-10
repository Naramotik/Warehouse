
using System.Collections.Generic;

namespace UserManagement.MVC.Models

{
    public class Warehouses : BaseEntity
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }

    }
}
