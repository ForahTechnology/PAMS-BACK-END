using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Handlers;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class InventoryController : BaseController
    {
        private readonly IInventoryService inventoryService;
        private readonly ISupplierService supplierService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventoryService"></param>
        public InventoryController(
            IInventoryService inventoryService,
            ISupplierService supplierService
            )
        {
            this.inventoryService = inventoryService;
            this.supplierService = supplierService;
        }
        /// <summary>
        /// Add item to stock inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost("stock")]
        public async Task<IActionResult> CreateStock(CreateStockInventory inventory)
        {
            if (inventory != null)
            {
                var response = await inventoryService.CreateStock(inventory);
                return Ok(new ResponseViewModel { ReturnObject = response, Message = "Item added", Status = true });
            }
            return BadRequest(new ResponseViewModel {Message = "All fields are required!" });
        }
        /// <summary>
        /// Get stocked item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("stock/{id}")]
        public async Task<IActionResult> GetStock([FromRoute] Guid id)
        {
            if (id!=Guid.Empty)
            {
                var inventory = await inventoryService.GetStock(id);
                if (inventory!=null)
                {
                    return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = inventory });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }
        /// <summary>
        /// Get all stock inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet("stock/all")]
        public async Task<IActionResult> GetStockAll()
        {
            return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = await inventoryService.GetStock() });
        }
        /// <summary>
        /// Update stock inventory item
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPut("stock")]
        public async Task<IActionResult> UpdateStock([FromBody]Inventory inventory)
        {
            if (inventory !=null)
            {
                var response = await inventoryService.UpdateStock(inventory);
                if (response == null) return NotFound(new ResponseViewModel { Message = "Stock item for update could not be found!" });
                return Ok(new ResponseViewModel { Message = "Updated successfully!", Status = true, ReturnObject = response });
            }
            return BadRequest(new ResponseViewModel { Message = "All fields are required!" });
        }
        /// <summary>
        /// Delete stock item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("stock/{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] Guid id)
        {

            if (id != Guid.Empty)
            {
                var isDeleted = await inventoryService.DeleteStock(id);
                if (isDeleted)
                {
                    return Ok(new ResponseViewModel { Message = "Item deleted!", Status = true});
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }
        
        /// <summary>
        /// Add item to equipment inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost("equipment")]
        public async Task<IActionResult> Createequipment(CreateEquipmentInventory inventory)
        {
            if (inventory != null)
            {
                var response = await inventoryService.CreateEquip(inventory);
                return Ok(new ResponseViewModel { ReturnObject = response, Message = "Item added", Status = true });
            }
            return BadRequest(new ResponseViewModel {Message = "All fields are required!" });
        }
        /// <summary>
        /// Get equipment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("equipment/{id}")]
        public async Task<IActionResult> Getequipment([FromRoute] Guid id)
        {
            if (id!=Guid.Empty)
            {
                var inventory = await inventoryService.GetEquip(id);
                if (inventory!=null)
                {
                    return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = inventory });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }
        /// <summary>
        /// Get all equipment inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet("equipment/all")]
        public async Task<IActionResult> GetequipmentAll()
        {
            return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = await inventoryService.GetEquip() });
        }
        /// <summary>
        /// Update equipment inventory item
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPut("equipment")]
        public async Task<IActionResult> Updateequipment([FromBody]EquipmentInventory inventory)
        {
            if (inventory !=null)
            {
                var response = await inventoryService.UpdateEquip(inventory);
                if (response == null) return NotFound(new ResponseViewModel { Message = "Stock item for update could not be found!" });
                return Ok(new ResponseViewModel { Message = "Updated successfully!", Status = true, ReturnObject = response });
            }
            return BadRequest(new ResponseViewModel { Message = "All fields are required!" });
        }
        /// <summary>
        /// Delete stock item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("equipment/{id}")]
        public async Task<IActionResult> DeleteEquipment([FromRoute] Guid id)
        {

            if (id != Guid.Empty)
            {
                var isDeleted = await inventoryService.DeleteEquip(id);
                if (isDeleted)
                {
                    return Ok(new ResponseViewModel { Message = "Item deleted!", Status = true});
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }
        /// <summary>
        /// Create supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPost("suppliers")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Address)
                || string.IsNullOrWhiteSpace(supplier.Name)
                || string.IsNullOrWhiteSpace(supplier.Email)
                || string.IsNullOrWhiteSpace(supplier.ContactPerson)
                || string.IsNullOrWhiteSpace(supplier.PhoneNumber)
                ) throw new ApiException("All fields are required!");
            return Ok(new ResponseViewModel { ReturnObject = await supplierService.CreateSupplier(supplier), Message = "Supplier created", Status = true });
        }

        /// <summary>
        /// Updated supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPut("suppliers")]
        public async Task<IActionResult> UpdateSupplier([FromBody] Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Address)
                || string.IsNullOrWhiteSpace(supplier.Name)
                || string.IsNullOrWhiteSpace(supplier.Email)
                || string.IsNullOrWhiteSpace(supplier.ContactPerson)
                || string.IsNullOrWhiteSpace(supplier.PhoneNumber)
                || supplier.ID == Guid.Empty
                ) throw new ApiException("All fields are required!");
            return Ok(new ResponseViewModel { ReturnObject = await supplierService.UpdateSupplier(supplier), Message = "Supplier updated", Status = true });
        }
        /// <summary>
        /// Get supplier by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("suppliers/{Id}")]
        public async Task<IActionResult> GetSupplierById([FromRoute] Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiException("Invalid supplier Id!");
            return Ok(new ResponseViewModel { ReturnObject = await supplierService.GetSupplier(Id), Message = "Supplier found", Status = true });
        }
        /// <summary>
        /// Get all suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet("suppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            return Ok(new ResponseViewModel { ReturnObject = await supplierService.GetAllSuppliers(), Message = "Available Suppliers", Status = true });
        }
        /// <summary>
        /// Delete supplier
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("suppliers/{Id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiException("Invalid supplier Id!");
            return Ok(new ResponseViewModel { ReturnObject = await supplierService.DeleteSupplier(Id), Message = "Supplier deleted", Status = true });
        }
    }
}
