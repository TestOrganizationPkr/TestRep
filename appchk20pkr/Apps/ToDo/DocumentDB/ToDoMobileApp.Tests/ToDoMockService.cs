using System;
using BusinessLogic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ToDoMobileApp.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class ToDoMockService : IDisposable, IToDoBL
    {
        /// <summary>
        /// Method to thrown an exception
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Exception</returns>
        public Task<Domain.Models.ToDoItem> Add(Domain.Models.ToDoItem item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to thrown an exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Exception</returns>
        public Task<bool> Delete(string id)
        {
           throw new NotImplementedException();
        }

        /// <summary>
        /// Test method throw an exception
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Exception</returns>
        public Task<bool> Update(Domain.Models.ToDoItem item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is to get all the items
        /// </summary>
        /// <returns>Exception</returns>
        public Task<List<Domain.Models.ToDoItem>> GetAll()
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
