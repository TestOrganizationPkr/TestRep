namespace Cache
{
    public interface IDataCache
    {
        /// <summary>
        /// This is to get the value based on key from redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// This is to remove the item base on key from redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// This is to increment the value
        /// </summary>
        /// <param name="key"></param>
        void Increment(string key);
    }
}
