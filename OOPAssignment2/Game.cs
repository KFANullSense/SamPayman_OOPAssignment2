namespace OOPAssignment2
{
    internal class Game
    {
        IPlayableGame currentGame = new SevensOut();

        static void Main(string[] args)
        {
            Statistics.ValidateStatsFile();

            Game localGame = new Game();
            localGame.MainMenu();
        }

        void MainMenu()
        {
            /* Menu Options:
             * 1) Play Game
             * 2) Switch Mode
             * 3) Check Stats
             * 4) Perform Tests
             * 5) Exit Program
             */

            bool programShouldRun = true;

            while (programShouldRun)
            {
                Console.WriteLine($"Menu Options:\n1) Play Game\n2) Switch Mode (Current Mode: {currentGame.ReturnGameType()})\n3) Check Stats\n4) Perform Tests\n5) Exit Program\n");

                var userInput = Console.ReadLine();
                int i = 0;

                if (int.TryParse(userInput, out i))
                {
                    if (i >= 1 && i <= 5)
                    {
                        switch (i)
                        {
                            case 1:
                                currentGame.PlayGame();
                                Statistics.AddPlays(currentGame.ReturnGameType(), 1);

                                if (currentGame.ReturnGameType() == "ThreeOrMore")
                                {
                                    currentGame = new ThreeOrMore();
                                }
                                else
                                {
                                    currentGame = new SevensOut();
                                }

                                break;
                            case 2:
                                if (currentGame.ReturnGameType() == "ThreeOrMore")
                                {
                                    currentGame = new SevensOut();
                                } else
                                {
                                    currentGame = new ThreeOrMore();
                                }
                                break;
                            case 3:
                                Statistics.PrintStatistics();
                                break;
                            case 4:
                                //perform tests
                                break;
                            case 5:
                                Console.WriteLine("Exiting program...");
                                programShouldRun = false;
                                break;
                        }
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.\n");
                }
            }
        }
    }
}