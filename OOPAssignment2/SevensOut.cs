using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    class SevensOut : BaseGame
    {
        public override string ReturnGameType()
        {
            _gameType = "SevensOut";
            return base.ReturnGameType();
        }

        public override void PlayCPU()
        {
            bool gameShouldPlay = true;

            while (gameShouldPlay)
            {
                int[] values = GameTurn(2);

                if (values[0] == 7)
                {
                    Console.WriteLine($"You rolled a 7! Your turn is over.");
                    if (_currentPlayerNum == 1)
                    {
                        Console.WriteLine($"Current Scores:\nPlayer 1: {_player1Score}");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("It is now the CPU's turn. Please press enter.");
                        Console.ReadLine();
                        ToggleCurrentPlayer();
                    } else
                    {
                        Console.WriteLine($"Final Scores:\nPlayer 1: {_player1Score} | CPU: {_player2Score}\n");
                        ExportHighScore(new int[] { _player1Score }); //The CPU cannot set a new high score, only players can.

                        Console.ReadLine();
                        Console.Clear();

                        if (_player1Score > _player2Score)
                        {
                            Console.WriteLine("Player 1 wins! Great job!");
                        } else
                        {
                            Console.WriteLine("The CPU wins. Better luck next time!");
                        }

                        Console.ReadLine();

                        gameShouldPlay = false;
                    }
                } else
                {
                    AddToCurrentPlayerScore(values[0]);

                    if (_currentPlayerNum == 1)
                    {
                        Console.WriteLine($"You gain {values[0]} points! Current total: {ReturnCurrentPlayerScore()}");

                        if (values[1] == values[2])
                        {
                            AddToCurrentPlayerScore(values[0]);

                            Console.WriteLine($"You rolled a double!\nYou gain {values[0] * 2} points! Current total: {ReturnCurrentPlayerScore()}");
                        }

                        Console.WriteLine("Press Enter to roll the dice.");
                        Console.ReadLine();
                    } else
                    {
                        Console.WriteLine($"The CPU gains {values[0]} points! Current total: {ReturnCurrentPlayerScore()}");

                        if (values[1] == values[2])
                        {
                            AddToCurrentPlayerScore(values[0]);

                            Console.WriteLine($"The CPU rolled a double!\nIt gains {values[0] * 2} points! Current total: {ReturnCurrentPlayerScore()}");
                        }

                        Console.WriteLine("Press Enter to continue.");
                        Console.ReadLine();
                    }
                }
            }
        }

        public override void PlayMultiplayer()
        {
            bool gameShouldPlay = true;

            while (gameShouldPlay)
            {
                int[] values = GameTurn(2);

                if (values[0] == 7)
                {
                    Console.WriteLine($"You rolled a 7! Your turn is over.");
                    if (_currentPlayerNum == 1)
                    {
                        Console.WriteLine($"Current Scores:\nPlayer 1: {_player1Score}");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Player 2, it is now your turn. Please press Enter to continue.");
                        Console.ReadLine();
                        ToggleCurrentPlayer();
                    }
                    else
                    {
                        Console.WriteLine($"Final Scores:\nPlayer 1: {_player1Score} | Player 2: {_player2Score}\n");
                        ExportHighScore(new int[] { _player1Score, _player2Score });

                        Console.ReadLine();
                        Console.Clear();

                        if (_player1Score > _player2Score)
                        {
                            Console.WriteLine("Player 1 wins! Great job!");
                        }
                        else
                        {
                            Console.WriteLine("Player 2 wins! Great job!");
                        }

                        Console.ReadLine();

                        gameShouldPlay = false;
                    }
                }
                else
                {
                    AddToCurrentPlayerScore(values[0]);

                    Console.WriteLine($"You gain {values[0]} points! Current total: {ReturnCurrentPlayerScore()}");

                    if (values[1] == values[2])
                    {
                        AddToCurrentPlayerScore(values[0]);

                        Console.WriteLine($"You rolled a double!\nYou gain {values[0] * 2} points! Current total: {ReturnCurrentPlayerScore()}");
                    }

                    Console.WriteLine("Press Enter to roll the dice.");
                    Console.ReadLine();
                }
            }
        }

        public override int[] GameTurn(int numToRoll)
        {
            Console.Clear();

            List<Die> diceList = new List<Die>();
            int[] returnValues = new int[numToRoll + 1];

            for (int i = 0; i < numToRoll; i++)
            {
                Die dieToAdd = new Die();
                dieToAdd.Roll();

                returnValues[i+1] = dieToAdd.CurrentValue;
                diceList.Add(dieToAdd);
            }

            int totalValue = diceList.Sum(d => d.CurrentValue);
            returnValues[0] = totalValue;

            string outputString = "";

            for (int i = 0; i < diceList.Count; i++)
            {
                outputString += $"Die {i}: {diceList[i].CurrentValue} | ";
            }

            outputString += $"Total Value: {totalValue}";

            Console.WriteLine(outputString);

            return returnValues;
        }
    }
}
