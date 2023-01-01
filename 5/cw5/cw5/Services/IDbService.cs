using cw5.Model;

namespace cw5.Services
{
    public interface IDbService
    {
        Task<int> AddProductToWarehouse(Warehouse warehouse);
        Task<int> AddProductToWarehouseProcedure(Warehouse warehouse);
    }
}
