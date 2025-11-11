using RadVanFortuin.Domain.Repository;
using RadVanFortuin.Persistence.InMemory;
using RadVanFortuin.Presentation;

namespace RadVanFortuin.Startup
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ICategoryRepository repo = new InMemoryCategoryRepository();
            var ui = new ConsolePresentation(repo);
            ui.Run();
        }
    }
}