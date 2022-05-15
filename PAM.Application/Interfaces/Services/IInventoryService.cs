using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IInventoryService
    {
        /// <summary>
        /// Create Inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Task<Inventory> CreateStock(CreateStockInventory inventory);
        /// <summary>
        /// Returns all inventory
        /// </summary>
        /// <returns></returns>
        Task<List<Inventory>> GetStock();
        /// <summary>
        /// Return an inventory whose id passed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Inventory> GetStock(Guid id);
        /// <summary>
        /// Update an inventory object.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Task<Inventory> UpdateStock(Inventory inventory);
        /// <summary>
        /// Deletes the inventory object whose id is passed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteStock(Guid id);
        Task<EquipmentInventory> CreateEquip(CreateEquipmentInventory inventory);
        Task<bool> DeleteEquip(Guid id);
        Task<List<EquipmentInventory>> GetEquip();
        Task<EquipmentInventory> GetEquip(Guid id);
        Task<EquipmentInventory> UpdateEquip(EquipmentInventory inventory);
    }
}
