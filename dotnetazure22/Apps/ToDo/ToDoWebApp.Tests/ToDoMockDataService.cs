using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ToDoWebApp.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class ToDoMockDataService : IDisposable, IToDoBL
    {
        public Domain.Models.ToDoItem Add(Domain.Models.ToDoItem item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Domain.Models.ToDoItem item)
        {
            return false;
        }

        /// <summary>
        /// This is to get all the items
        /// </summary>
        /// <returns>list of items as object</returns>
        public List<Domain.Models.ToDoItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetItemCount()
{
    throw new NotImplementedException();
}
public bool RemoveItem()
{
    throw new NotImplementedException();
}

        public void Dispose()
        {
            //Nothing to do
        }
    }
}
