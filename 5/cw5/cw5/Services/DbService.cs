using cw5.Model;
using System.Data;
using System.Data.SqlClient;

namespace cw5.Services
{
    public class DbService : IDbService
    {
        private IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        private static int MinAmount = 0;
        public async Task<int> AddProductToWarehouse(Warehouse warehouse)
        {

            if (! await IsIdForProductExists(warehouse.IdProduct))
            {
                throw new ArgumentException("Produkt o danym id nie istnieje.");
            }

            if (! await IsIdForWarehouseExists(warehouse.IdWarehouse))
            {
                throw new ArgumentException("Hurtownia o danym id nie istnieje.");
            }

            if (warehouse.Amount <= MinAmount)
            {
                throw new ArgumentException($"Ilosc nie moze byc mniejsza rowna {MinAmount}.");
            }

            if (! await IsOrderExists(warehouse))
            { 
                throw new ArgumentException("Nie ma odpowiedniego zlecenia");
            }

            int idOrder = await GetIdOrder(warehouse);

            if (await IsOrderCompleted(idOrder))
            {
                throw new ArgumentException("Zamowienie zostalo juz zrealizowane");
            }


            if (!await UpdateFulfilledDate(idOrder))
            {
                throw new ArgumentException("Nie udalo sie zaktualizowac FullfielledAt");
            }

            return await InsertProductWarehouse(idOrder, warehouse);

        }

        public async Task<int> AddProductToWarehouseProcedure(Warehouse warehouse)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand("AddProductToWarehouse", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@IdProduct", warehouse.IdProduct);
            com.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
            com.Parameters.AddWithValue("@Amount",warehouse.Amount);
            com.Parameters.AddWithValue("@CreatedAt", warehouse.CreateAt);

            await con.OpenAsync();

            /*
            var returnParameter = com.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            int result = (Int32)returnParameter.Value;
            */

            return await com.ExecuteNonQueryAsync();



            //return result;

        }

        private async Task<bool> IsIdForProductExists(int idProduct)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand("SELECT count(1) FROM Product WHERE idProduct = @product", con);
            com.Parameters.AddWithValue("@product", idProduct);

            await con.OpenAsync();

            int count = (Int32)await com.ExecuteScalarAsync();


            if (count > 0)
            {
                return true;
            }

            return false;
        }
        private async Task<bool> IsIdForWarehouseExists(int idWarehouse)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand("SELECT count(1) FROM Warehouse where idwarehouse = @warehouse", con);
            com.Parameters.AddWithValue("@warehouse", idWarehouse);

            await con.OpenAsync();

            int count = (Int32)await com.ExecuteScalarAsync();

            if (count > 0)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> IsOrderExists(Warehouse warehouse)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand(
                "SELECT count(1) " +
                "FROM   \"Order\" " +
                "WHERE  idProduct = @product " +
                "AND    amount    = @amount " +
                "AND    CreatedAt < @createdAt", 
                con);
            com.Parameters.AddWithValue("@product", warehouse.IdProduct);
            com.Parameters.AddWithValue("@amount", warehouse.Amount);
            com.Parameters.AddWithValue("@createdAt", warehouse.CreateAt);

            await con.OpenAsync();

            int count = (Int32)await com.ExecuteScalarAsync();

            if (count > 0)
            {
                return true;
            }

            return false;
        }

        private async Task<int> GetIdOrder(Warehouse warehouse)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand(
                "SELECT idOrder " +
                "FROM   \"Order\" " +
                "WHERE  idProduct = @product " +
                "AND    amount    = @amount " +
                "AND    CreatedAt < @createdAt",
                con);
            com.Parameters.AddWithValue("@product", warehouse.IdProduct);
            com.Parameters.AddWithValue("@amount", warehouse.Amount);
            com.Parameters.AddWithValue("@createdAt", warehouse.CreateAt);

            await con.OpenAsync();

            int id = (Int32)await com.ExecuteScalarAsync();

            return id;
        }

        private async Task<bool> UpdateFulfilledDate(int idOrder)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand(
                "UPDATE \"Order\" " +
                "SET FulfilledAt = GETDATE() " +
                "WHERE  idOrder = @order",
                con);
            com.Parameters.AddWithValue("@order", idOrder);

            await con.OpenAsync();

            int id = (Int32)await com.ExecuteNonQueryAsync();

            if (id == 1)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> IsOrderCompleted(int idOrder)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand(
                "SELECT count(1) " +
                "FROM   Product_Warehouse " +
                "WHERE  idOrder     = @order ",
                con);
            com.Parameters.AddWithValue("@order", idOrder);

            await con.OpenAsync();

            int count = (Int32)await com.ExecuteScalarAsync();

            if (count > 0)
            {
                return true;
            }

            return false;
        }

        private async Task<int> InsertProductWarehouse(int idOrder, Warehouse warehouse)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            using var com = new SqlCommand(
                "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)" +
                "VALUES (" +
                "@warehouse," +
                "@product," +
                "@order," +
                "@amount," +
                "@amount * (" +
                " SELECT price" +
                " FROM   product" +
                " WHERE  idProduct = @product)," +
                "GETDATE()" +
                ")",
                con);
            com.Parameters.AddWithValue("@order", idOrder);
            com.Parameters.AddWithValue("@warehouse", warehouse.IdWarehouse);
            com.Parameters.AddWithValue("@product", warehouse.IdProduct);
            com.Parameters.AddWithValue("@amount", warehouse.Amount);

            await con.OpenAsync();

            int id = (Int32)await com.ExecuteNonQueryAsync();

            return id;
        }


    }
}
