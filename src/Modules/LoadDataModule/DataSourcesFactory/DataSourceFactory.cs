namespace LoadDataModule.DataSourcesFactory
{
    /// <summary>
    /// it is implemented with Abstract Factory pattern
    /// There are several ways to implement this scenario
    /// </summary>
    public class DataSourceFactory : IDataSourceFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IFileDataSource GetJsonDataSource()
        {
            return new JsonFileDataSource();
        }
    }
}
