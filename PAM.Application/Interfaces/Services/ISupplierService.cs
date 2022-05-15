using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<Supplier> CreateSupplier(CreateSupplier supplier);
        Task<Supplier> UpdateSupplier(Supplier supplier);
        Task<bool> DeleteSupplier(Guid supplierId);
        Task<Supplier> GetSupplier(Guid supplierId);
        Task<IEnumerable<Supplier>> GetAllSuppliers();
    }
}
