using PAMS.Application.Interfaces.Persistence;
using PAMS.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Persistence.Implementation
{
    public class StoreManager<T> : IStoreManager<T> where T : class
    {
        protected readonly PAMSdbContext context;
        IDataStore<T> store;
        public StoreManager(PAMSdbContext pAMSdbContext)
        {
            this.context = pAMSdbContext;
        }
        public IDataStore<T> DataStore => store ??= new DataStore<T>(context);

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}
