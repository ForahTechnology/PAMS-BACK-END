using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public DateTime DateOfLastOrder { get; set; }
        public string Weight { get; set; }
        public string Quantity { get; set; }
        public string Location { get; set; }
        public decimal UnitCost { get; set; }
        public int StockQty { get; set; }
        public decimal TotalValue { get; set; }
        public string ReorderLevel { get; set; }
        public decimal QuantityUsed { get; set; }
        public decimal QuantityLeft { get; set; }
        public bool ItemDiscontinued { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid SupplierId { get; set; }

    }
}
