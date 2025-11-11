using RadVanFortuin.Domain;
using RadVanFortuin.Domain.Model;
using RadVanFortuin.Domain.Repository;

namespace RadVanFortuin.Presentation
{
    /// <summary>
    /// Consolegebaseerde presentatie-laag.
    /// Zorgt voor de gebruikersinteractie (input/output)
    /// en gebruikt enkel de DomainController.
    /// </summary>
    public sealed class ConsolePresentation
    {
        private readonly DomainController _dc;
        private readonly ICategoryRepository _repo;

        public ConsolePresentation(ICategoryRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _dc = new DomainController(); // geen custom ctor meer
        }

        /// <summary>Start de gebruikersflow voor het Rad van Fortuin-spel.</summary>
        public void Run()
        {
            Console.Clear();
            Console.WriteLine("=== Rad van Fortuin ===\n");

            // 1) Spelers
            var players = CreatePlayers();

            // 2) Categorie
            var category = ChooseCategory();

            // 3) Start spel
            _dc.StartGame(players, category);

            // 4) Spelverloop
            foreach (var round in _dc.Rounds)
            {
                Console.WriteLine($"\n--- Nieuwe ronde ({category.Name}) ---");

                foreach (var player in _dc.Players)
                {
                    Console.WriteLine($"\nSpeler: {player.Name}");

                    while (!round.Solved && round.AttemptsLeft > 0)
                    {
                        Console.WriteLine($"Woord: {round.GetMaskedWord()}");
                        Console.WriteLine($"Pogingen over: {round.AttemptsLeft}");
                        Console.Write("Letter of woord: ");
                        var input = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(input))
                            continue;

                        if (input.Length == 1)
                        {
                            var pos = _dc.GuessLetter(round, player, input[0]);
                            Console.WriteLine(pos.Count > 0
                                ? $"Goed! Letter op posities: {string.Join(',', pos)}"
                                : "Geen match.");
                        }
                        else
                        {
                            var ok = _dc.SolveWord(round, player, input);
                            Console.WriteLine(ok ? "Juist!" : "Fout!");
                        }
                    }

                    if (round.Solved)
                        Console.WriteLine($"\n{player.Name} heeft het woord geraden: {round.Solution}");
                    else
                        Console.WriteLine("\nRonde voor deze speler voorbij — geen pogingen meer.");

                    round.ResetForNextPlayer();
                }
            }

            // 5) Winnaars
            var winners = _dc.Winners();
            Console.WriteLine("\n=== Spel afgelopen ===");
            Console.WriteLine("Winnaar(s): " + string.Join(", ", winners.Select(w => w.Name)));
            Console.WriteLine("\nDruk op Enter om af te sluiten...");
            Console.ReadLine();
        }

        // -------------------- Helpers --------------------

        private static List<Player> CreatePlayers()
        {
            var players = new List<Player>();
            for (int i = 1; i <= 3; i++)
            {
                Console.Write($"Naam speler {i}: ");
                var name = Console.ReadLine();
                players.Add(new Player(string.IsNullOrWhiteSpace(name) ? $"Speler{i}" : name.Trim()));
            }
            return players;
        }

        private Category ChooseCategory()
        {
            var categories = _repo.GetAll();
            Console.WriteLine("\nKies een categorie:");
            for (int i = 0; i < categories.Count; i++)
                Console.WriteLine($"  {i + 1}. {categories[i].Name}");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) ||
                   choice < 1 || choice > categories.Count)
            {
                Console.Write("Ongeldige keuze, probeer opnieuw: ");
            }

            return categories[choice - 1];
        }
    }
}