using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    class ThreeOrMore : BaseGame
    {
        public override string ReturnGameType()
        {
            _gameType = "ThreeOrMore";
            return base.ReturnGameType();
        }

        public override void PlayCPU()
        {
            bool gameShouldPlay = true;

            while (gameShouldPlay)
            {
                if (!IsTesting)
                {
                    Console.Clear();
                    if (_currentPlayerNum == 1)
                    {
                        Console.WriteLine($"Player {_currentPlayerNum}, it's your turn. Press Enter to roll the dice!");
                    }
                    else
                    {
                        Console.WriteLine($"It's now the CPU's turn. Press Enter to continue.");
                    }
                    Console.ReadLine();
                    Console.Clear();
                }

                RollAllDice(true);

                if (!IsTesting)
                {
                    Console.WriteLine($"Current Scores:\nPlayer 1: {_player1Score} | CPU: {_player2Score}");
                }

                if (_player1Score >= 20 || _player2Score >= 20)
                {
                    if (IsTesting)
                    {
                        Testing.PublishTestResults($"Game successfully terminated above 20 points scored. Final scores: {_player1Score} | {_player2Score}");
                        gameShouldPlay = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Final Scores:\nPlayer 1: {_player1Score} | CPU: {_player2Score}\n");
                        ExportHighScore(new int[] { _player1Score });

                        Console.ReadLine();
                        Console.Clear();

                        if (_player1Score > _player2Score)
                        {
                            Console.WriteLine("Player 1 wins! Great job!");
                        }
                        else
                        {
                            Console.WriteLine("The CPU wins. Better luck next time!");
                        }

                        Console.ReadLine();

                        gameShouldPlay = false;
                    }
                }

                ToggleCurrentPlayer();
            }
        }

        public override void PlayMultiplayer()
        {
            bool gameShouldPlay = true;

            while (gameShouldPlay)
            {
                Console.Clear();
                Console.WriteLine($"Player {_currentPlayerNum}, it's your turn. Press Enter to roll the dice!");
                Console.ReadLine();
                Console.Clear();

                RollAllDice(false);

                Console.WriteLine($"Current Scores:\nPlayer 1: {_player1Score} | Player 2: {_player2Score}");

                if (_player1Score >= 20 || _player2Score >= 20)
                {
                    Console.Clear();
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

                ToggleCurrentPlayer();
            }
        }

        public override int[] GameTurn(int numToRoll)
        {
            Console.Clear();

            List<Die> diceList = new List<Die>();

            int[] returnValues = new int[numToRoll + 2];

            for (int i = 0; i < numToRoll; i++)
            {
                Die dieToAdd = new Die();
                dieToAdd.Roll();

                returnValues[i + 2] = dieToAdd.CurrentValue;
                diceList.Add(dieToAdd);
            }

            for (int j = 1; j <= 6; j++)
            {
                int numOfDupes = diceList.Count(d => d.CurrentValue == j);

                if (numOfDupes > returnValues[0])
                {
                    returnValues[0] = numOfDupes;
                    returnValues[1] = j;
                }
            }

            return returnValues;
        }

        public void RollAllDice(bool isCPUGame, bool isEmbeddedRoll = false)
        {
            int[] values = GameTurn(5);

            if (!IsTesting)
            {
                string diceValues = "Dice Rolled:\n";

                for (int i = 2; i < values.Length; i++)
                {
                    diceValues += $"Die {i - 1}: {values[i]}\n";
                }

                Console.WriteLine(diceValues);
            }

            DicesetResult(values, isCPUGame, isEmbeddedRoll);
        }

        public void DicesetResult(int[] diceSet, bool isCPUGame, bool isEmbeddedRoll = false)
        {
            string playerPrefix = "You";
            if (isCPUGame && _currentPlayerNum == 2)
            {
                playerPrefix = "The CPU";
            }

            if (IsTesting)
            {
                Testing.ValidateDuplicates(diceSet[0], diceSet.Skip(2).ToArray());
                Testing.PublishTestResults($"Duplicates calculated correctly: {diceSet[0]} found");
                AddToCurrentPlayerScore(6);
            }
            else
            {

                switch (diceSet[0])
                {
                    case 5:
                        Console.WriteLine($"{playerPrefix} rolled a 5-of-a-kind!\nScore Gained: 12 points");
                        AddToCurrentPlayerScore(12);
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine($"{playerPrefix} rolled a 4-of-a-kind!\nScore Gained: 6 points");
                        AddToCurrentPlayerScore(6);
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine($"{playerPrefix} rolled a 3-of-a-kind!\nScore Gained: 3 points");
                        AddToCurrentPlayerScore(3);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine($"{playerPrefix} rolled a 2-of-a-kind.");
                        if (isCPUGame && _currentPlayerNum == 2)
                        {
                            if (new Random().Next(2) == 0)
                            {
                                Console.WriteLine("The CPU has decided to reroll all dice.");
                                Console.ReadLine();
                                RollAllDice(isCPUGame, true);
                            }
                            else
                            {
                                Console.WriteLine("The CPU has decided to reroll the remaining dice.");
                                Console.ReadLine();
                                RerollDice(diceSet[1], isCPUGame);
                            }
                        }
                        else
                        {
                            bool shouldLoop = true;

                            while (shouldLoop)
                            {
                                shouldLoop = false;

                                Console.WriteLine("Would you like to:\n1) Reroll all dice\n2) Reroll the remaining dice");

                                var userInput = Console.ReadLine();

                                if (userInput == "1")
                                {
                                    RollAllDice(isCPUGame);
                                }
                                else if (userInput == "2")
                                {
                                    RerollDice(diceSet[1], isCPUGame);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nInvalid input. Please enter a number between 1 and 2.");
                                    shouldLoop = true;
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("None of the dice were matching. Bad luck!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void RerollDice(int valueToCheck, bool isCPUGame)
        {
            int[] rerolledInput = GameTurn(3);

            string diceValues = "Dice Rolled:\n";

            for (int i = 2; i < rerolledInput.Length; i++)
            {
                diceValues += $"Die {i - 1}: {rerolledInput[i]}\n";
            }

            Console.WriteLine(diceValues);

            int[] rerolledDice = rerolledInput.Skip(2).ToArray();

            if (rerolledDice.Contains(valueToCheck))
            {
                int[] finalValue = new int[] { rerolledDice.Count(d => d == valueToCheck) + 2 };
                DicesetResult(finalValue, isCPUGame);
            }
        }
    }
}
