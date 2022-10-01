using System;

namespace OnceTwoTree
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameTest())
                game.Run();
        }
    }
}
