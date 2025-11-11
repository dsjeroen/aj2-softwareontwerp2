namespace RadVanFortuin.Domain.Model
{
    /// <summary>
    /// Spelcoördinatie: initieert rondes op basis van een categorie
    /// en bepaalt nadien de winnaar. Geen UI of data-toegang.
    /// </summary>
    public class Game
    {
        /// <summary>Max. aantal rondes (0..5).</summary>
        public int MaxRounds { get; private set; } 

        private List<Player> _players = new();
        private readonly List<Round> _rounds = new();

        /// <summary>Alle aangemaakte rondes en players (read-only) voor de controller/UI.</summary>
        public IReadOnlyList<Round> Rounds => _rounds;
        public IReadOnlyList<Player> Players => _players;

        /// <summary>
        /// Initialiseert het spel: bewaart spelers, kiest aantal rondes,
        /// en maakt voor elke ronde een Round met een woord uit de categorie.
        /// </summary>
        public void Start(List<Player> players, Category category)
        {
            if (players is null || players.Count == 0)
                throw new ArgumentException("Minstens één speler vereist.", nameof(players));
            ArgumentNullException.ThrowIfNull(category);
            
            // nieuwe lijst als beschermlaag tegen onbedoelde of ongewenste mutaties van buiten je klasse.
            _players = new List<Player>(players); 

            var words = category.GetWords();
            MaxRounds = Math.Min(5, Math.Max(0, words.Count));

            _rounds.Clear();
            CreateRounds(words);
        }

        /// <summary>
        /// Geeft alle winnaars (één of meerdere) met de hoogste score terug.
        /// </summary>
        public IReadOnlyList<Player> Winners()
        {
            if (_players.Count == 0)
                throw new InvalidOperationException("Game is niet geïnitialiseerd (geen spelers).");

            var max = _players.Max(p => p.Wins);

            return _players
                .Where(p => p.Wins == max)
                .OrderBy(p => p.Name, StringComparer.Ordinal)
                .ToList();
        }

        // ====== PRIVATE HULPFUNCTIES (geen publieke operaties volgens DCD) ======

        private void CreateRounds(IReadOnlyList<Word> words)
        {
            for (int i = 0; i < Math.Min(MaxRounds, words.Count); i++)
            {
                var round = new Round();
                round.SetWord(words[i]);
                _rounds.Add(round);
            }
        }
    }
}