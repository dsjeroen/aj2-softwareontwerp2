namespace RadVanFortuin.Domain.Model
{
    /// <summary>
    /// Stelt een categorie voor, zoals 'Dieren' of 'Landen'.
    /// Elke categorie bevat een lijst van woorden die kunnen worden geraden.
    /// </summary>
    public class Category
    {
        public string Name { get; }

        private readonly List<Word> _words = new();

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Naam mag niet leeg zijn.", nameof(name));
            Name = name.Trim();
        }

        public void AddWord(Word word)
        {
            if (word is null) throw new ArgumentNullException(nameof(word));
            _words.Add(word);
        }

        public IReadOnlyList<Word> GetWords() => _words;
    }
}