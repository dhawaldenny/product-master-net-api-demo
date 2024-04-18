using System;
using System.Collections.Generic;

namespace ProductMaster.Entities.Models
{
    public partial class Product
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
