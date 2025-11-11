namespace RadVanFortuin.Domain.Model
{
    public class Round
    {
        public int AttemptsLeft { get; private set; } = 10;
        public bool Solved { get; private set; }
        public List<char> GuessedLetters { get; } = new();

        private Word? _word;

        public void SetWord(Word word)
        {
            ArgumentNullException.ThrowIfNull(word);
            _word = word;
        }

        public string GetMaskedWord()
        {
            var chars = _word.Value.Select(c => GuessedLetters.Contains(c) ? c : '_');
            return string.Join(' ', chars);
        }

        public List<int> Guess(char letter)
        {
            letter = char.ToLower(letter);
            var positions = new List<int>();

            for (int i = 0; i < _word.Value.Length; i++)
                if (_word.Value[i] == letter)
                    positions.Add(i);

            if (!GuessedLetters.Contains(letter))
                GuessedLetters.Add(letter);

            AttemptsLeft--;

            if (_word.Value.All(c => GuessedLetters.Contains(c)))
                Solved = true;

            return positions;
        }

        public bool Solve(string guessedWord)
        {
            if (string.IsNullOrWhiteSpace(guessedWord))
            {
                AttemptsLeft--;
                return false;
            }

            var normalized = guessedWord.Trim().ToLower();
            if (normalized == _word.Value)
            {
                Solved = true;
                return true;
            }

            AttemptsLeft--;
            return false;
        }

        public string Solution => _word?.Value ?? string.Empty;


        /// <summary>Reset voor de volgende speler, met hetzelfde woord.</summary>
        public void ResetForNextPlayer()
        {
            // zelfde woord behouden
            AttemptsLeft = 10;
            Solved = false;
            GuessedLetters.Clear();
        }
    }
}