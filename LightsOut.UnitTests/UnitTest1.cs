using NUnit.Framework;

namespace LightsOut.UnitTests
{
    public class LightsOutTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // Test to see if initially while starting the game, atleast one light is on.
        [Test]
        public void StartGame_OneLightOn()
        {
            //create new game
            var newGame = new Form1();

            bool actual = false;

            for (int i = 0; i < newGame.Grid.GetLength(1); i++)
            {
                for (int j = 0; j < newGame.Grid.GetLength(0); j++)
                {
                    // Check if light is on
                    if (newGame.Grid[i, j])
                    {
                        actual = true;
                    }
                }
            }

            Assert.AreEqual(true,actual);
        }

        //Test to see if when a square is clicked, then its surrounding squares state is changed correctly.
        [Test]
        public void BtnClick_SurroundingLightsOn()
        {
            //create new game
            var newGame = new Form1();

            //Set all lights to be off
            for (int i = 0; i < newGame.Grid.GetLength(1); i++)
            {
                for (int j = 0; j < newGame.Grid.GetLength(0); j++)
                {
                    newGame.Grid[i, j] = false;
                }
            }

            //change state of clicked square and its surrounding squares.
            newGame.changeState(newGame.Grid[2, 2], 2, 2);

            Assert.AreEqual(true, newGame.Grid[2, 2]);
            Assert.AreEqual(true, newGame.Grid[2, 3]);
            Assert.AreEqual(true, newGame.Grid[2, 1]);
            Assert.AreEqual(true, newGame.Grid[1, 2]);
            Assert.AreEqual(true, newGame.Grid[3, 2]);
        }

        //Test to see if the won function finds that all lights are off.
        [Test]
        public void CheckWon_AllLightsOff()
        {
            //create new game
            var newGame = new Form1();

            //Set all lights to be off
            for (int i = 0; i < newGame.Grid.GetLength(1); i++)
            {
                for (int j = 0; j < newGame.Grid.GetLength(0); j++)
                {
                    newGame.Grid[i, j] = false;
                }
            }

            // Get actual result
            bool actual = newGame.Won();

            Assert.AreEqual(true, actual);
        }
    }
}