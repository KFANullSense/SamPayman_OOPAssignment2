using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OOPAssignment2
{
    internal class Testing
    {
        private static string _filePath = Path.Combine(Environment.CurrentDirectory, @"Data\testLog.txt");

        public static void ValidateTestingFile()
        {
            if (File.Exists(_filePath))
            {
                return;
            }
            else
            {
                PublishTestResults("Log file created.");
            }
        }

        public static void PublishTestResults(string resultsToWrite)
        {
            StreamWriter localSW = File.AppendText(_filePath);

            localSW.WriteLine($"{DateTime.Now}: {resultsToWrite}");

            localSW.Close();
        }

        public static void PerformTests()
        {
            Game testingGame = new Game();

            testingGame.CurrentGame = new SevensOut();
            testingGame.CurrentGame.SetTestState(true);
            testingGame.CurrentGame.PlayGame();

            testingGame.CurrentGame = new ThreeOrMore();
            testingGame.CurrentGame.SetTestState(true);
            testingGame.CurrentGame.PlayGame();

            Console.Clear();
            Console.WriteLine("Testing successful. Please check log file for additional details.");
            Console.ReadLine();
        }

        public static void ValidateArraySum(int expectedSum, int[] inputArray)
        {
            Debug.Assert(inputArray.Sum() == expectedSum, "Array sum is not equal to expected sum.");
        }

        public static void ValidateDuplicates (int expectedAmount, int[] inputArray)
        {
            int numOfDupes = 0;

            for (int j = 1; j <= 6; j++)
            {
                int currentDupes = inputArray.Count(d => d == j);

                if (currentDupes > numOfDupes)
                {
                    numOfDupes = currentDupes;
                }
            }

            Debug.Assert(numOfDupes == expectedAmount, "Duplicate amount is not equal to expected amount.");
        }
    }
}
