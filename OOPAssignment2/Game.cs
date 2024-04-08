namespace OOPAssignment2
{
    internal class Game
    {
        PlayableGame currentGame = new SevensOut();

        static void Main(string[] args)
        {
            /* Menu Options:
             * 1) Play Game
             * 2) Switch Mode
             * 3) Check Stats
             * 4) Perform Tests
             * 5) Exit Program
             */
        }

        void RunCurrentGame()
        {
            currentGame.PlayGame();
        }
    }

    interface PlayableGame
    {
        public void PlayGame();
    }

    class SevensOut : PlayableGame
    {
        public void PlayGame()
        {
            //play
        }
    }

    class ThreeOrMore : PlayableGame
    {
        public void PlayGame()
        {
            //play
        }
    }
}