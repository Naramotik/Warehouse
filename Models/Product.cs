using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int ArticleNumber { get; set; }
        public int ProviderId { get; set; }
        public Partners Provider { get; set; }
        public int UnitsId { get; set; }
        public Units Units { get; set; }
        public bool Status { get; set; }

    }
}
