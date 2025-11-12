using RadVanFortuin.Domain.Model;
using RadVanFortuin.Domain.Repository;

namespace RadVanFortuin.Domain
{
    /// <summary>
    /// Application-coördinator tussen Presentation en Domain Model.
    /// Orkestreert het spelverloop volgens de SD's, zonder UI/IO.
    /// </summary>
    public class DomainController
    {
        private readonly Game _game = new();

        /// <summary>Alle rondes van het huidige spel.</summary>
        public IReadOnlyList<Round> Rounds => _game.Rounds;
        /// <summary>Alle spelers van het huidige spel.</summary>
        public IReadOnlyList<Player> Players => _game.Players;

        /// <summary>SD1: Start het spel met spelers en een gekozen categorie.</summary>
        public void StartGame(List<Player> players, Category category)
        {
            ArgumentNullException.ThrowIfNull(players);
            if (players.Count == 0)
                throw new ArgumentException("Minstens één speler vereist.", nameof(players));
            ArgumentNullException.ThrowIfNull(category);

            _game.Start(players, category);
        }

        /// <summary>SD2: Letter raden in een ronde; geeft posities terug. Winst bij solved.</summary>
        public IReadOnlyList<int> GuessLetter(Round round, Player player, char letter)
        {
            ArgumentNullException.ThrowIfNull(round);
            ArgumentNullException.ThrowIfNull(player);

            var positions = round.Guess(letter);
            if (round.Solved) player.AddWin();
            return positions;
        }

        /// <summary>SD2: Volledig woord raden; true bij succes. Winst bij solved.</summary>
        public bool SolveWord(Round round, Player player, string guess)
        {
            ArgumentNullException.ThrowIfNull(round);
            ArgumentNullException.ThrowIfNull(player);

            var ok = round.Solve(guess);
            if (ok) player.AddWin();
            return ok;
        }

        /// <summary>Winnaars (één of meerdere) met de hoogste score.</summary>
        public IReadOnlyList<Player> Winners() => _game.Winners();        
    }
}