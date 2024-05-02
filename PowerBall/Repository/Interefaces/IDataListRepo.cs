using PowerBall.Models;

namespace PowerBall.Repository.Interefaces
{
    public interface IDataListRepo
    {
        public Task<List<DataList>> GetAllData();
        public  Task<List<DataList>> GetDataByPage(int pageIndex, int pageSize);
        public Task<List<DataList>> GetDataBY_SP();
        public  Task<List<DataList>> GetDataByPageSP(int pageIndex, int pageSize);
    }
}
