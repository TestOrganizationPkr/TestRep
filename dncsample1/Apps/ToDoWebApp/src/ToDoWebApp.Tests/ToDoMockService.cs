using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
namespace ToDoWebApp.Tests 
{
    public class ToDoMockService : IDisposable, IToDoBL
    {
        public ToDoItem Add(ToDoItem item)
        {
            throw new NotImplementedException();
        }
        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        public List<ToDoItem> GetAll()
        {
            throw new NotImplementedException();
        }
        public bool Update(ToDoItem item)
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
    }
}
