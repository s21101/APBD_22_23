using cw4.Model;

namespace cw4.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Animal>> GetAnimals(string orderBy);
        Task<Animal> AddAnimalAsync(Animal newAnimal);
        Task DeleteAnimalAsync(int idAnimal);
    }
}
