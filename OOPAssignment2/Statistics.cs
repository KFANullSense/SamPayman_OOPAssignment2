using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOPAssignment2
{
    internal class Statistics
    {
        /*
         * File format:
         * [GameType],[HighScore],[NumberOfPlays]
         */

        private static string _filePath = Path.Combine(Environment.CurrentDirectory, @"Data\stats.txt");

        public static void ValidateStatsFile()
        {
            if (File.Exists(_filePath))
            {
                return;
            } else
            {
                StreamWriter localSW = File.AppendText(_filePath);

                localSW.WriteLine("SevensOut,0,0");
                localSW.WriteLine("ThreeOrMore,0,0");

                localSW.Close();
            }
        }

        static List<string> ReadFileContents()
        {
            string fileContents = "";

            StreamReader localSR = new StreamReader(_filePath);

            try
            {
                fileContents = localSR.ReadToEnd();
            }
            catch
            {
                Console.WriteLine("ERROR: Stats file is empty.");
            }
            finally
            {
                localSR.Close();
            }

            List<string> finalContents = fileContents.Split("\n").ToList();

            return finalContents;
        }

        static void WriteFileContents(List<string> contentsToWrite)
        {
            string finalContents = string.Join("\n", contentsToWrite);

            StreamWriter localSW = new StreamWriter(_filePath);

            try
            {
                localSW.Write(finalContents);
            }
            catch
            {
                Console.WriteLine("ERROR: Stats file not found.");
            }
            finally
            {
                localSW.Close();
            }
        }

        public static void AddPlays(string gameType, int playsToAdd)
        {
            List<string> fileContents = ReadFileContents();

            for (int i = 0; i < fileContents.Count; i++)
            {
                string[] lineValue = fileContents[i].Split(",");
                if (lineValue[0] == gameType)
                {
                    lineValue[2] = (int.Parse(lineValue[2]) + playsToAdd).ToString();
                }

                fileContents[i] = string.Join(",", lineValue);
            }

            WriteFileContents(fileContents);
        }

        public static void UpdateHighScore(string gameType, int newHighScore)
        {
            List<string> fileContents = ReadFileContents();

            for (int i = 0; i < fileContents.Count; i++)
            {
                string[] lineValue = fileContents[i].Split(",");
                if (lineValue[0] == gameType)
                {
                    lineValue[1] = newHighScore.ToString();
                }

                fileContents[i] = string.Join(",", lineValue);
            }

            WriteFileContents(fileContents);
        }

        public static int ReturnHighScore(string gameType)
        {
            List<string> fileContents = ReadFileContents();

            for (int i = 0; i < fileContents.Count; i++)
            {
                string[] lineValue = fileContents[i].Split(",");
                if (lineValue[0] == gameType)
                {
                    return int.Parse(lineValue[1]);
                }
            }

            return -1;
        }

        public static int ReturnPlays(string gameType)
        {
            List<string> fileContents = ReadFileContents();

            for (int i = 0; i < fileContents.Count; i++)
            {
                string[] lineValue = fileContents[i].Split(",");
                if (lineValue[0] == gameType)
                {
                    return int.Parse(lineValue[2]);
                }
            }

            return -1;
        }

        public static void PrintStatistics()
        {
            Console.Clear();
            Console.WriteLine("Game Statistics: ");
            Console.WriteLine($"\nSevens Out:\nHigh Score: {ReturnHighScore("SevensOut")}\nTimes Played: {ReturnPlays("SevensOut")}");
            Console.WriteLine($"\nThree Or More:\nHigh Score: {ReturnHighScore("ThreeOrMore")}\nTimes Played: {ReturnPlays("ThreeOrMore")}");
            Console.WriteLine($"Press Enter to return to the menu.");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
