using cw4.Model;

namespace cw4.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Animal>> GetAnimals(string orderBy);
        Task AddAnimalAsync(Animal newAnimal);
        Task UpdateAnimalAsync(Animal newAnimal, int idAnimal);
        Task DeleteAnimalAsync(int idAnimal);
    }
}
