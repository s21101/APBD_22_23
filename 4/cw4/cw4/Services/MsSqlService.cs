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

            if (!OrderByCategories.Contains(orderBy))
            {
                throw new ArgumentException("Invalid orderBy value");
            }

            string sql = "SELECT * FROM Animal order by name asc";  //"SELECT * FROM Animal order by " + orderBy; //
 
            var res = new List<Animal>();
            using (SqlConnection con = new(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = $"SELECT * FROM Animal order by {orderBy} asc";
                //com.Parameters.AddWithValue("order_by", name);
                //com.Parameters.Add("@order_by", System.Data.SqlDbType.NVarChar);
                //com.Parameters["@order_by"].Value = orderBy;
                //SqlParameter p1 = new("@order_by", "name");
                //p1.SqlDbType = System.Data.SqlDbType.NVarChar;
                //com.Parameters.Add(p1);


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

        private async Task<int> GetNextAnimalIdAsync(SqlConnection con)
        {
            int nextAnimalId = -1;
            try
            { 
                await con.OpenAsync();
            }
            catch{ }

            using (SqlCommand cmd = new SqlCommand("SELECT MAX(idAnimal) FROM Animal"))
            {
                nextAnimalId = (int) await cmd.ExecuteScalarAsync();
            }

            return nextAnimalId;

        }

        public async Task<Animal> AddAnimalAsync(Animal newAnimal)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                //int nextAnimalId;

                //string sql = "SELECT MAX(idAnimal) FROM Animal";
                //using (SqlCommand cmd = new SqlCommand(sql, connection))
                //{
                //    nextAnimalId = (int)await cmd.ExecuteScalarAsync();
                //}
                int insertedID;

                await connection.OpenAsync();
                string sql = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@param1,@param2,@param3,@param4)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = newAnimal.Name;
                    cmd.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = newAnimal.Description;
                    cmd.Parameters.Add("@param3", SqlDbType.VarChar, 50).Value = newAnimal.Category;
                    cmd.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = newAnimal.Area;
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();
                    //insertedID = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }

            return newAnimal;




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
                    int numberOfRows = await cmd.ExecuteNonQueryAsync();

                    if (numberOfRows == 0)
                    {
                        throw new ArgumentException("Invalid animal id");
                    }
                }
            }
        }
    }
}
