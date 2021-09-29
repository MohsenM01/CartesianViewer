using Common.Models;

namespace LoadDataModule.DataSourcesFactory
{

    /// <summary>
    /// 
    /// </summary>
    public interface IFileDataSource {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        ShapesReadModel GetData(string filePath);
    }


}
