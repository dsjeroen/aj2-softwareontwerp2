using RadVanFortuin.Domain.Model;
using RadVanFortuin.Domain.Repository;

namespace RadVanFortuin.Persistence.InMemory
{
    /// <summary>
    /// In-memory implementatie van ICategoryRepository.
    /// Doel: opzetten van vaste data voor de applicatie (geen UI-wijzigingen, geen persistente opslag).
    /// </summary>
    public sealed class InMemoryCategoryRepository :ICategoryRepository
    {
        private readonly List<Category> _categories = new();

        public InMemoryCategoryRepository()
        {
            var dieren = new Category("Dieren");
            dieren.AddWord(new Word("olifant"));   // 7
            dieren.AddWord(new Word("leeuw"));     // 5
            dieren.AddWord(new Word("panter"));    // 6
            _categories.Add(dieren);

            var landen = new Category("Landen");
            landen.AddWord(new Word("belgie"));    // 6
            landen.AddWord(new Word("italie"));    // 6
            landen.AddWord(new Word("frankrijk")); // 9
            _categories.Add(landen);

            var wetenschap = new Category("Wetenschap");
            wetenschap.AddWord(new Word("proton"));   // 6
            wetenschap.AddWord(new Word("neutron"));  // 7
            wetenschap.AddWord(new Word("fotonen"));  // 7
            _categories.Add(wetenschap);
        }

        public IReadOnlyList<Category> GetAll() => _categories;
    }
}