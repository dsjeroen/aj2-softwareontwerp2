namespace RadVanFortuin.Domain.Model
{
    /// <summary>
    /// Stelt een speler voor met een naam en het aantal gewonnen rondes.
    /// </summary>
    public class Player
    {
        public string Name { get; }
        public int Wins { get; private set; }

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Naam van speler mag niet leeg zijn.", nameof(name));

            Name = name.Trim();
        }

        /// <summary>
        /// Verhoogt het aantal gewonnen rondes met 1.
        /// </summary>
        public void AddWin() => Wins++;
    }
}
