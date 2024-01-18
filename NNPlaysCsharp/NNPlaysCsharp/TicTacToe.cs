using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPlaysCsharp
{
    public  class TicTacToe
    {
        int[] board;
        Player playerOne;
        Player playerTwo;
        public TicTacToe(Player _playerOne, Player _playerTwo)
        {
            board = new int[9];
            playerOne = _playerOne;
            playerTwo = _playerTwo;
        }

        public void StartGame()
        {
            Console.WriteLine("Game started");

            int moves = 0;
            Player player = playerOne;
            while (moves < 9)
            {
                Console.Clear();
                DrawGameboard(board);

                Console.WriteLine("Player {0}'s turn:", player.id);
                while(!player.MakeMove(ref board, GetAvailableMoves(player.id, board)))
                {
                    Console.WriteLine("Illegal move.");
                };

                int gameState = CheckGameState(board);

                if(gameState == -1)
                {
                    Console.WriteLine("Illegal move.");
                    continue;
                }
                else if(gameState != 0)
                {
                    Console.Clear();
                    DrawGameboard(board);
                    Console.WriteLine("{0} wins!", ConvertGameSymbol(player.id));
                    return;
                }

                player = player.id == playerOne.id ? playerTwo : playerOne;
                moves++;
            }

            Console.Clear();
            DrawGameboard(board);
            Console.WriteLine("It's a draw!");
        }

        public static bool CheckLegalMove(int pos, int[] board)
        {
            return pos >= 0 && pos <= 8 && board[pos] == 0;
        }

        public static int[][] GetAvailableMoves(int player, int[] board)
        {
            List<int[]> availableMoves = new List<int[]>();
            for(int i = 0; i < 9; i++)
            {
                if (board[i] == 0)
                {
                    int[] move = board.ToArray();
                    move[i] = player;
                    availableMoves.Add(move);
                }
            }
            return availableMoves.ToArray();
        }

        /// <summary>
        /// Return the player's number if the player wins; 0 if it's a draw;
        /// </summary>
        public static int CheckGameState(int[] board)
        {
            //check vertical and horizontal lines
            for(int i = 0; i <= 6; i = i+3)
            {
                if (board[i] == board[i+1] && board[i+1] == board[i+2] && board[i] != 0)
                    return board[i];
                else if (board[i/3] == board[i/3+3] && board[i/3+3] == board[i/3+6] && board[i/3] != 0)
                    return board[i];
            }

            //check diagonals
            if (board[0] != 0 && board[0] == board[4] && board[4] == board[8])
                return board[0];
            else if(board[2] != 0 && board[2] == board[4] && board[4] == board[6])
                return board[2];

            return 0;
        }

        public static void DrawGameboard(int[] board)
        {
            for(int i = 0; i <= 6; i = i+3)
            {
                Console.WriteLine("{0} | {1} | {2}         {3} | {4} | {5}", i, i + 1, i + 2, ConvertGameSymbol(board[i]), ConvertGameSymbol(board[i+1]), ConvertGameSymbol(board[i+2]));
                if(i != 6)
                    Console.WriteLine("----------        ----------");
            }
        }

        public static string ConvertGameSymbol(int symbol)
        {
            if (symbol == 0)
                return " ";
            if (symbol == 1)
                return "X";
            if (symbol == 2)
                return "O";

            return "";
        }
    }
}
