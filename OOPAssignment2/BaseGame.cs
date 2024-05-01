using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    interface IPlayableGame
    {
        public string ReturnGameType();
        public void PlayGame();
        public int[] GameTurn(int numToRoll);
        public void SetTestState(bool testState);
    }

    abstract class BaseGame : IPlayableGame
    {
        protected string _gameType = "BaseGame";
        protected int _localHighScore = 0;

        protected int _currentPlayerNum = 1;
        protected int _player1Score = 0;
        protected int _player2Score = 0;

        protected bool IsTesting = false;

        public virtual string ReturnGameType()
        {
            return _gameType;
        }

        public void SetTestState(bool testState)
        {
            IsTesting = testState;
        }

        public void PlayGame()
        {
            if (IsTesting)
            {
                PlayCPU();
                return;
            }

            Console.Clear();

            bool shouldLoop = true;

            while (shouldLoop)
            {
                shouldLoop = false;

                Console.WriteLine("Would you like to:\n1) Play against the computer\n2) Play against another player");

                var userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    PlayCPU();
                }
                else if (userInput == "2")
                {
                    PlayMultiplayer();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid input. Please enter a number between 1 and 2.");
                    shouldLoop = true;
                }
            }
        }

        public abstract void PlayCPU();
        public abstract void PlayMultiplayer();
        public abstract int[] GameTurn(int numToRoll);

        public void ToggleCurrentPlayer()
        {
            if (_currentPlayerNum == 1)
            {
                _currentPlayerNum = 2;
            } else
            {
                _currentPlayerNum = 1;
            }
        }

        public void AddToCurrentPlayerScore(int scoreToAdd)
        {
            if (_currentPlayerNum == 1)
            {
                _player1Score += scoreToAdd;
            } else
            {
                _player2Score += scoreToAdd;
            }
        }

        public int ReturnCurrentPlayerScore ()
        {
            return (_currentPlayerNum == 1) ? _player1Score : _player2Score;
        }

        public void ExportHighScore(int[] scoresToCheck)
        {
            int currentHighScore = Statistics.ReturnHighScore(_gameType);

            foreach (int score in scoresToCheck)
            {
                if (score > currentHighScore)
                {
                    Console.WriteLine($"You got a new High Score of {score}!");
                    Statistics.UpdateHighScore(_gameType, score);
                }
            }
        }
    }
}
