namespace RadVanFortuin.Domain.Model
{
    /// <summary>
    /// Stelt een woord voor dat de speler moet raden. 
    /// De tekst wordt genormaliseerd naar kleine letters.
    /// </summary>
    public sealed class Word
    {
        public string Value { get; }

        public int Length => Value.Length;

        public Word(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentException("Woord mag niet leeg zijn.", nameof(word));

            var wordNormalized = word.Trim().ToLower();
            if (wordNormalized.Length < 5)
                throw new ArgumentException("Woordlengte moet ≥ 5 zijn.", nameof(word));

            if (!wordNormalized.All(char.IsLetter))
                throw new ArgumentException("Woord mag enkel letters bevatten.", nameof(word));

            Value = wordNormalized;
        }

        public override string ToString() => Value;
    }
}