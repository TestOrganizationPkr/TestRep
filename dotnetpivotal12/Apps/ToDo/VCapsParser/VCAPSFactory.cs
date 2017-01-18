using System.Diagnostics.CodeAnalysis;

namespace VCapsParser
{
    [ExcludeFromCodeCoverage]
    public static class VCAPSFactory
    {
        /// <summary>
        /// This is the factory which is resposible to create the object of the class based in ServiceType
        /// </summary>
        public static IVCAPSDatabase GetInstance(ServiceType option)
        {
            switch (option)
            {
                case ServiceType.MySql:
                    return new MySql();
                case ServiceType.Postgre:
                    return new Postgre();
                case ServiceType.Redis:
                    return new Redis();
                default:
                    return new MySql();
            }
        }
    }
}
