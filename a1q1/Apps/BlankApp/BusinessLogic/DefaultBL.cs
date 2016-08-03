using DataAccessLayer.Repository;
using System;


namespace BusinessLogic
{
    public sealed class DefaultBL : IDefaultBL
    {
        /// <summary>
        /// This is to create an object for the DefaultRepository
        /// </summary>
        readonly IDefaultRepository _repository;

        /// <summary>
        /// Constructor which accepts the repository as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is inside ServiceLocation folder
        /// </summary>
        /// <param name="repository"></param>
        public DefaultBL(IDefaultRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This is the sample method with no implementation.
        /// Remove this method and add new methods to implement the requied functionality by calling Repository methods.
        /// </summary>
        void IDefaultBL.All()
        {
            throw new NotImplementedException();
        }

       
    }
}
