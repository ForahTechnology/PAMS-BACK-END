using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class CreateEquipmentInventory
    {
        public string EquipmentName { get; set; }
        public string Description { get; set; }
        public string IDTag { get; set; }
        public string Location { get; set; }
        public string Supplier { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyExpiration { get; set; }
        public string Price { get; set; }
        public string Condition { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PhotoLink { get; set; }
        public bool Discountinued { get; set; }
        public Guid SupplierId { get; set; }
    }
}
