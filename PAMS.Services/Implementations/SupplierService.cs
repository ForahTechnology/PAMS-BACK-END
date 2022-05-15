using PAMS.Application.Handlers;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly IStoreManager<Supplier> supplierStoreManager;

        public SupplierService(
            IStoreManager<Supplier> supplierStoreManager
            )
        {
            this.supplierStoreManager = supplierStoreManager;
        }
        public async Task<Supplier> CreateSupplier(CreateSupplier supplier)
        {
            var existingSupplier = supplierStoreManager.DataStore.GetAllQuery().Where(s => s.Email == supplier.Email && s.Name == supplier.Name);
            if (existingSupplier.Count() > 0) throw new ApiException("Supplier already exist!");
            var newSupplier = new Supplier
            {
                Name = supplier.Name,
                Address = supplier.Address,
                ContactPerson = supplier.ContactPerson,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber
            };
            await supplierStoreManager.DataStore.Add(newSupplier);
            await supplierStoreManager.Save();
            return newSupplier;
        }

        public async Task<bool> DeleteSupplier(Guid supplierId)
        {
            var isDeleted = await supplierStoreManager.DataStore.Delete(supplierId);
            if (!isDeleted) throw new KeyNotFoundException("Supplier not found!");
            await supplierStoreManager.Save();
            return isDeleted;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            var suppliers = await supplierStoreManager.DataStore.GetAll();
            return suppliers;
        }

        public async Task<Supplier> GetSupplier(Guid supplierId)
        {
            var supplier = await supplierStoreManager.DataStore.GetById(supplierId);
            if (supplier == null) throw new KeyNotFoundException("Supplier not found!");
            return supplier;
        }

        public async Task<Supplier> UpdateSupplier(Supplier supplier)
        {
            var supplierUpdate = await supplierStoreManager.DataStore.GetById(supplier.ID);
            if (supplierUpdate == null) throw new KeyNotFoundException("Supplier not found!");
            supplierUpdate.Email = supplier.Email ?? supplierUpdate.Email;
            supplierUpdate.Address = supplier.Address ?? supplierUpdate.Address;
            supplierUpdate.ContactPerson = supplier.ContactPerson ?? supplierUpdate.ContactPerson;
            supplierUpdate.PhoneNumber = supplier.PhoneNumber ?? supplierUpdate.PhoneNumber;
            supplierUpdate.Name = supplier.Name ?? supplierUpdate.Name;
            supplierStoreManager.DataStore.Update(supplierUpdate);
            await supplierStoreManager.Save();
            return supplierUpdate;
        }
    }
}
