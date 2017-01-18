namespace VCapsParser
{
    /// <summary>
    /// This is IFactory interface which need to implement by the Factory class
    /// </summary>
    public interface IVCAPSDatabase
    {
        /// <summary>
        /// This is the BrockerName
        /// </summary>
        string brokerName { get; }

        /// <summary>
        /// This is the user defined name, this name we are using for identify and filtering the json data
        /// </summary>
        string udfName { get; }

        /// <summary>
        /// This is to get the connection string based on raw json passed
        /// </summary>
        /// <param name="rawJson"></param>
        /// <returns></returns>
        string ConstructConnectionString(string rawJson);
    }
}
