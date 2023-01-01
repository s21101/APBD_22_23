using cw4.Model;
using System.Data;
using System.Data.SqlClient;

namespace cw4.Services
{
    public class MsSqlService : IDbService
    {
        private IConfiguration _configuration;

        private static HashSet<string> OrderByCategories = new HashSet<string> {
            "name","description","category","area"
        };

        public MsSqlService(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public async Task<IEnumerable<Animal>> GetAnimals(string orderBy)
        {
            if (orderBy == null)
            {
                orderBy = "name";
            }

            if (!OrderByCategories.Contains(orderBy.ToLower()))
            {
                throw new ArgumentException("Invalid orderBy value");
            }
 
            var res = new List<Animal>();
            using (SqlConnection con = new(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = $"SELECT * FROM Animal order by {orderBy} asc";

                await con.OpenAsync();
                SqlDataReader dr = await com.ExecuteReaderAsync();
                while ( await dr.ReadAsync())
                {
                    res.Add(new Animal
                    {
                        IdAnimal = (int) dr["IdAnimal"],
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    });
                }
                await con.CloseAsync();
            }

            return res;
        }

        public async Task AddAnimalAsync(Animal newAnimal)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                string sql = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@param1,@param2,@param3,@param4)";
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = newAnimal.Name;
                    cmd.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = newAnimal.Description;
                    cmd.Parameters.Add("@param3", SqlDbType.VarChar, 50).Value = newAnimal.Category;
                    cmd.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = newAnimal.Area;
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task DeleteAnimalAsync(int idAnimal)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                await connection.OpenAsync();
                string sql = "Delete Animal WHERE idAnimal = @param1";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = idAnimal;
                    cmd.CommandType = CommandType.Text;
                    int count = await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    if (count < 1)
                    {
                        throw new ArgumentException("Not found");
                    }
                }
            }
        }

        public async Task UpdateAnimalAsync(Animal newAnimal, int idAnimal)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                await connection.OpenAsync();
                string sql = "UPDATE Animal " +
                             "SET    name        = @name, " +
                                    "description = @description, " +
                                    "category    = @category, " +
                                    "area        = @area " +
                             "WHERE IdAnimal     = @idAnimal";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = newAnimal.Name;
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = newAnimal.Description;
                    cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = newAnimal.Category;
                    cmd.Parameters.Add("@area", SqlDbType.NVarChar).Value = newAnimal.Area;
                    cmd.Parameters.Add("@idAnimal", SqlDbType.Int).Value = idAnimal;
                    cmd.CommandType = CommandType.Text;
                    int numberOfRows = await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();

                    if (numberOfRows == 0)
                    {
                        throw new ArgumentException("Could not find id "+ idAnimal);
                    }
                }
            }
        }
    }
}
