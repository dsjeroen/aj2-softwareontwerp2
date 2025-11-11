using RadVanFortuin.Domain.Model;

namespace RadVanFortuin.Domain.Repository
{
    public interface ICategoryRepository
    {
        IReadOnlyList<Category> GetAll();
    }
}
