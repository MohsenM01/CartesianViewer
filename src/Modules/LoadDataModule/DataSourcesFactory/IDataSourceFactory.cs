namespace LoadDataModule.DataSourcesFactory
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSourceFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFileDataSource GetJsonDataSource();
    }

}
