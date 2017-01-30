using AutoMapper;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Cache;

namespace BusinessLogic
{
    public sealed class ToDoBL : IDisposable, IToDoBL
    {
        /// <summary>
        /// This is to create an object for the ToDoRepository
        /// </summary>
        readonly IToDoRepository _repository;

        /// <summary>
/// This is to create an object for the Repository
/// </summary>
readonly IDataCache _repositoryCache;

private readonly string _cacheKey = "itemcount";

        /// <summary>
        /// Constructor which accepts the repository as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is inside ServiceLocation folder
        /// </summary>
        /// <param name="repository"></param>
        public ToDoBL(IToDoRepository repository, IDataCache repositoryCache)
        {
            _repository = repository;
            _repositoryCache = repositoryCache;
        }

        /// <summary>
        /// This is to add a new todo item 
        /// Here we have used automapper to convert the domain model to DAL model
        /// </summary>
        /// <param name="item"></param>
        /// <returns>After adding todo item it will append the id in todo object and return that object</returns>
        public Domain.Models.ToDoItem Add(Domain.Models.ToDoItem item)
        {
            if (null != item)
            {
                //This is to initilize the automapper
                Mapper.Initialize(cfg => cfg.CreateMap<Domain.Models.ToDoItem, DataAccessLayer.Models.ToDoItem>());
                //This is convert the Domain model to DAL model object, this will take domain model as parameter and return DAL model as response
                DataAccessLayer.Models.ToDoItem itemDAL = Mapper.Map<Domain.Models.ToDoItem, DataAccessLayer.Models.ToDoItem>(item);
                var record = _repository.Create(itemDAL);
                if (null != record)
                {
                    item.Id = record.id;
                    //This is to increment a cache
_repositoryCache.Increment(_cacheKey);
                }
            }
            return item;
        }

        /// <summary>
/// This is to get the count from redis
/// </summary>
/// <returns></returns>
public string GetItemCount()
{
	return _repositoryCache.GetValue(_cacheKey);
}

/// <summary>
/// This is to delete / remove the item from redis based on key;
/// </summary>
/// <returns></returns>
public bool RemoveItem()
{
	return _repositoryCache.Remove(_cacheKey);
}

        /// <summary>
        /// This is to update todo item 
        /// Here we have used automapper to convert the domain model to DAL model
        /// </summary>
        /// <param name="item"></param>
        /// <returns>After updating todo item, it will retrun true if it updated successfully otherwise false</returns>
        public bool Update(Domain.Models.ToDoItem item)
        {
            bool status = false;
            if (null != item)
            {
                //This is to initilize the automapper
                Mapper.Initialize(cfg => cfg.CreateMap<Domain.Models.ToDoItem, DataAccessLayer.Models.ToDoItem>());
                //This is convert the Domain model to DAL model object, this will take domain model as parameter and return DAL model as response
                DataAccessLayer.Models.ToDoItem itemDAL = Mapper.Map<Domain.Models.ToDoItem, DataAccessLayer.Models.ToDoItem>(item);
                //int id = _repository.Update(itemDAL);
                var response = _repository.Update(itemDAL);
                status = response == null ? false : true;
                //if (id > 0)
                //{
                //    status = true;
                //}
            }
            return status;
        }

        /// <summary>
        /// This is to delete an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This will retrun bool. If the todo item is deleted successfully then it will return true otherwise false. </returns>
        public bool Delete(string id)
        {
            bool status = false;
            var item = _repository.Find(id);
            if (null != item)
            {
                _repository.Delete(id);
                status = true;
            }
            return status;
        }

        /// <summary>
        /// This is to get all the todo's items
        /// Here we have used automapper to convert the DAL model list to domain model list
        /// </summary>
        /// <returns>This will retun a list of todo's items as list</returns>
        public List<Domain.Models.ToDoItem> GetAll()
        {
            Console.WriteLine("Get call start");
            try
            {
                List<Domain.Models.ToDoItem> items = new List<Domain.Models.ToDoItem>();
                var itemsDAL = _repository.All().ToList();
                if (null != itemsDAL & itemsDAL.Count > 0)
                {
                    //This is to initilize the automapper
                    Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.ToDoItem, Domain.Models.ToDoItem>());
                    //This is convert the DAL model list to Domain model list, this will take DAL model list as parameter and return Domain model list as response
                    items = Mapper.Map<List<DataAccessLayer.Models.ToDoItem>, List<Domain.Models.ToDoItem>>(itemsDAL);
                }
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get call ex :"+ ex.Message);
                return null;
            }
        }

        /// <summary>
        /// This is to dispose / release the connection 
        /// </summary>
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
