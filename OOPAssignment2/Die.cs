//NOTE: This class was taken from my code for Assignment 1, as specified in the brief.

using System;

namespace OOPAssignment2
{
    internal class Die
    {
        //By making the Random instance static, it ensures that all rolls of the dice will continue along the same seed.
        public static Random Rng = new Random();

        //Provide a public way to get the current value of the dice. Set is private, as the dice will not need to be set outside of the roll function.
        public int CurrentValue { get; private set; }

        //This method returns a random number between 1 and 6.
        public int Roll()
        {
            int rolledValue = Rng.Next(1, 7); //Using the static Random instance, generate a random integer between 1 and 6.

            //Set the current value of the dice to the random value, and return it.
            CurrentValue = rolledValue;
            return rolledValue;
        }

    }
}