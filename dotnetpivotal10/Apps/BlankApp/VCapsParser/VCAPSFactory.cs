namespace VCapsParser
{
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
                default:
                    return new MySql();
            }
        }
    }
}
