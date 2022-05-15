using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IStoreManager<Inventory> inventoryStoreManager;
        private readonly IStoreManager<EquipmentInventory> equipInventoryStoreManager;
        private readonly ISupplierService supplierService;

        public InventoryService(
            IStoreManager<Inventory> inventoryStoreManager,
            IStoreManager<EquipmentInventory> equipInventoryStoreManager,
            ISupplierService supplierService
            )
        {
            this.inventoryStoreManager = inventoryStoreManager;
            this.equipInventoryStoreManager = equipInventoryStoreManager;
            this.supplierService = supplierService;
        }

        public async Task<Inventory> CreateStock(CreateStockInventory inventory)
        {
            await supplierService.GetSupplier(inventory.SupplierId);
            var newInventory = new Inventory
            {
                DateOfLastOrder = inventory.DateOfLastOrder,
                ExpiryDate = inventory.ExpiryDate,
                ItemDiscontinued = inventory.ItemDiscontinued,
                ItemName = inventory.ItemName,
                ItemNumber = inventory.ItemNumber,
                Location = inventory.Location,
                Quantity = inventory.Quantity,
                ReorderLevel = inventory.ReorderLevel,
                StockQty = inventory.StockQty,
                TotalValue = inventory.TotalValue,
                Weight = inventory.Weight,
                UnitCost = inventory.UnitCost,
                QuantityLeft = inventory.QuantityLeft,
                QuantityUsed = inventory.QuantityUsed,
                SupplierId = inventory.SupplierId
            };

            await inventoryStoreManager.DataStore.Add(newInventory);
            await inventoryStoreManager.Save();
            return newInventory;
        }

        public async Task<bool> DeleteStock(Guid id)
        {
            var response = await inventoryStoreManager.DataStore.Delete(id);
            await inventoryStoreManager.Save();
            return response;
        }

        public async Task<List<Inventory>> GetStock()
        {
            var inventories = await inventoryStoreManager.DataStore.GetAll();
            return (List<Inventory>)inventories;
        }

        public async Task<Inventory> GetStock(Guid id)
        {
            return await inventoryStoreManager.DataStore.GetById(id);
        }

        public async Task<Inventory> UpdateStock(Inventory inventory)
        {
            var inventoryUpdate = await inventoryStoreManager.DataStore.GetById(inventory.ID);
            if (inventoryUpdate!=null)
            {
                inventoryUpdate.DateOfLastOrder = inventory.DateOfLastOrder;
                inventoryUpdate.ExpiryDate = inventory.ExpiryDate;
                inventoryUpdate.ItemDiscontinued = inventory.ItemDiscontinued;
                inventoryUpdate.ItemName = inventory.ItemName;
                inventoryUpdate.ItemNumber = inventory.ItemNumber;
                inventoryUpdate.Location = inventory.Location;
                inventoryUpdate.Quantity = inventory.Quantity;
                inventoryUpdate.QuantityLeft = inventory.QuantityLeft;
                inventoryUpdate.QuantityUsed = inventory.QuantityUsed;
                inventoryUpdate.ReorderLevel = inventory.ReorderLevel;
                inventoryUpdate.StockQty = inventory.StockQty;
                inventoryUpdate.TotalValue = inventory.TotalValue;
                inventoryUpdate.UnitCost = inventory.UnitCost;
                inventoryUpdate.Weight = inventory.Weight;
                inventoryUpdate.ReorderLevel = inventory.ReorderLevel;
                inventoryUpdate.SupplierId = inventory.SupplierId;
                inventoryStoreManager.DataStore.Update(inventoryUpdate);
                await inventoryStoreManager.Save();
            }
            return inventoryUpdate;
        }

        public async Task<EquipmentInventory> CreateEquip(CreateEquipmentInventory inventory)
        {
            var newInventory = new EquipmentInventory
            {
                Condition = inventory.Condition,
                Description = inventory.Description,
                Discountinued = inventory.Discountinued,
                EquipmentName = inventory.EquipmentName,
                IDTag = inventory.IDTag,
                Location = inventory.Location,
                ModelNumber = inventory.ModelNumber,
                PhotoLink = inventory.PhotoLink,
                Price = inventory.Price,
                PurchaseDate = inventory.PurchaseDate.ToShortDateString(),
                Quantity = inventory.Quantity,
                SerialNumber = inventory.SerialNumber,
                Supplier = inventory.Supplier,
                UnitPrice = inventory.UnitPrice,
                WarrantyExpiration = inventory.WarrantyExpiration.ToShortDateString(),
                SupplierId = inventory.SupplierId
            };

            await equipInventoryStoreManager.DataStore.Add(newInventory);
            await equipInventoryStoreManager.Save();
            return newInventory;
        }

        public async Task<bool> DeleteEquip(Guid id)
        {
            var response = await equipInventoryStoreManager.DataStore.Delete(id);
            await equipInventoryStoreManager.Save();
            return response;
        }

        public async Task<List<EquipmentInventory>> GetEquip()
        {
            var inventories = await equipInventoryStoreManager.DataStore.GetAll();
            return (List<EquipmentInventory>)inventories;
        }

        public async Task<EquipmentInventory> GetEquip(Guid id)
        {
            return await equipInventoryStoreManager.DataStore.GetById(id);
        }

        public async Task<EquipmentInventory> UpdateEquip(EquipmentInventory inventory)
        {
            var inventoryUpdate = await equipInventoryStoreManager.DataStore.GetById(inventory.ID);
            if (inventoryUpdate!=null)
            {
                inventoryUpdate.Condition = inventory.Condition;
                inventoryUpdate.Description = inventory.Description;
                inventoryUpdate.Discountinued = inventory.Discountinued;
                inventoryUpdate.EquipmentName = inventory.EquipmentName;
                inventoryUpdate.IDTag = inventory.IDTag;
                inventoryUpdate.Location = inventory.Location;
                inventoryUpdate.ModelNumber = inventory.ModelNumber;
                inventoryUpdate.PhotoLink = inventory.PhotoLink;
                inventoryUpdate.Price = inventory.Price;
                inventoryUpdate.Quantity = inventory.Quantity;
                inventoryUpdate.SerialNumber = inventory.SerialNumber;
                inventoryUpdate.Supplier = inventory.Supplier;
                inventoryUpdate.UnitPrice = inventory.UnitPrice;
                inventoryUpdate.WarrantyExpiration = inventory.WarrantyExpiration;
                inventoryUpdate.PurchaseDate = inventory.PurchaseDate;
                equipInventoryStoreManager.DataStore.Update(inventoryUpdate);
                await equipInventoryStoreManager.Save();
            }
            return inventoryUpdate;
        }
    }
}
