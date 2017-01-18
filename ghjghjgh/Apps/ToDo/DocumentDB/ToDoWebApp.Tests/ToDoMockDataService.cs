using System;
using BusinessLogic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ToDoWebApp.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class ToDoMockDataService : IDisposable, IToDoBL
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Domain.Models.ToDoItem> Add(Domain.Models.ToDoItem item)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Domain.Models.ToDoItem item)
        {
            return false;
        }

        public string GetItemCount()
{
    throw new NotImplementedException();
}
public bool RemoveItem()
{
    throw new NotImplementedException();
}

        /// <summary>
        /// This is to get all the items
        /// </summary>
        /// <returns>list of items as object</returns>
        public async Task<List<Domain.Models.ToDoItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //Nothing to do
        }

        
    }
}
