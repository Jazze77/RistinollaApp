using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RistinollaApp
{
    public class Player
    {
        public string name;
        public int number;

        Player() { }

        public Player(string name, int number)
        {

            this.name = name;
            this.number = number;
        }
    }


    class Game
    {

        Player player1;
        Player player2;
        int[,] map;

        public Game()
        {
            // initiate players
            player1 = new Player("Player 1", 1);
            player2 = new Player("Player 2", 2);

            // fill array with 9 empty slots

            map = new int[3, 3]
            {
                { 0,0,0 },
                { 0,0,0 },
                { 0,0,0 }
            };
        }

        public void PlayGame()
        {

            // run game loop
            bool didWin = false;

            do
            {

                // player1
                DrawMap(player1);
                ChoosePosition(player1);
                DrawMap(player1);
                didWin = DidWin(player1.number);

                if (didWin)
                {
                    Console.WriteLine(player1.name + " WON!");
                    break;
                }

                // player2
                DrawMap(player2);
                ChoosePosition(player2);
                DrawMap(player2);
                didWin = DidWin(player2.number);

                if (didWin)
                {
                    Console.WriteLine(player2.name + " WON!");
                    break;
                }

                bool checkmate = true;
                if (!didWin)
                {
                   
                    for (int iterA = 0; iterA < 3; iterA++)
                    {
                        for (int iterB = 0; iterB < 3; iterB++)
                        {
                            if (map[iterA, iterB] == 0)
                            {
                                Console.WriteLine("NO WINNER");
                                checkmate = false;
                                break;
                            }
                        }
                    }
                    didWin = checkmate;
                }

            } while (!didWin);
        }

        void DrawMap(Player player)
        {
            Console.Clear();

            int baseX = 2, baseY = 1;
            Console.Write("Player is " + player.name + ".");
            baseX = 2; baseY = 3;

            // draw fixed length map table
            for (int iterA = 0; iterA < 3; iterA++)
            {
                for (int iterB = 0; iterB < 3; iterB++)
                {
                    Console.SetCursorPosition(baseX + iterB * 2, baseY + iterA * 2);
                    // draw sign
                    switch (map[iterA, iterB])
                    {
                        case 0:
                            Console.Write(".");
                            break;
                        case 1:
                            Console.Write("X");
                            break;
                        case 2:
                            Console.Write("O");
                            break;
                    }

                    // draw background
                    if (iterB < 2)
                    {
                        Console.Write("|");
                    }
                    else if (iterA < 2)
                    {
                        Console.SetCursorPosition(baseX, baseY + iterA * 2 + 1);
                        Console.Write("-+-+-");
                    }
                }
            }

            baseX = 2; baseY = 10;
            Console.SetCursorPosition(baseX, baseY);
        }


        void ChoosePosition(Player player)
        {
            Console.WriteLine("Select Position!");

            bool validResponse = false;
            int responseX, responseY;
            do
            {
                Console.WriteLine("X...");
                string guessInput = Console.ReadLine();

                // convert string to number
                validResponse = int.TryParse(guessInput, out responseX);

                if (validResponse)
                {
                    if (responseX < 0 || responseX > 2) { validResponse = false; }
                }

            } while (!validResponse);

            do
            {
                Console.WriteLine("Y...");
                string guessInput = Console.ReadLine();

                // convert string to number
                validResponse = int.TryParse(guessInput, out responseY);

                if (validResponse)
                {
                    if (responseY < 0 || responseY > 2) { validResponse = false; }
                }

            } while (!validResponse);

            map[responseY, responseX] = (player == player1) ? 1 : 2;

            Console.WriteLine("<Return> to Change Player...");
            Console.ReadLine();
        }
        bool DidWin(int player)
        {
            for (int iter = 0; iter < 3; iter++)
            {
               if (
                    (map[iter, 0] == player) &&
                    (map[iter, 1] == player) &&
                    (map[iter, 2] == player)
                    )

                { return true; }

                if (
                    (map[0, iter] == player) &&
                    (map[1, iter] == player) &&
                    (map[2, iter] == player)
                    )
                { return true; }
            }

            if (
                (map[0, 0] == player) &&
                (map[1, 1] == player) &&
                (map[2, 2] == player)
                )

            { return true; }

            if (
                (map[0, 2] == player) &&
                (map[1, 1] == player) &&
                (map[2, 0] == player)
                )

            { return true; }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game();
            game.PlayGame();

            Console.ReadLine();
        }
    }
}
