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
        public TicTacToe()
        {
            board = new int[9];
        }

        public void StartGame()
        {
            Console.WriteLine("Game started");

            int moves = 0;
            int player = 1;
            while (moves < 9)
            {
                Console.Clear();
                DrawGameboard();
                int pos;
                Console.WriteLine("Player {0}'s turn:", player);
                if(!int.TryParse(Console.ReadLine(), out pos))
                {
                    Console.WriteLine("Illegal move.");
                    continue;
                }

                int gameState = MakePlay(pos, player);
                if(gameState == -1)
                {
                    Console.WriteLine("Illegal move.");
                    continue;
                }
                else if(gameState != 0)
                {
                    Console.Clear();
                    DrawGameboard();
                    Console.WriteLine("{0} wins!", ConvertGameSymbol(player));
                    return;
                }

                player = player == 1 ? 2 : 1;
                moves++;
            }

            Console.Clear();
            DrawGameboard();
            Console.WriteLine("It's a draw!");
        }

        public int MakePlay(int pos, int player)
        {
            if (!CheckLegalMove(pos))
                return -1;

            board[pos] = player;

            return CheckGameState();
        }

        bool CheckLegalMove(int pos)
        {
            return pos >= 0 && pos <= 8 && board[pos] == 0;
        }

        int CheckGameState()
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

        void DrawGameboard()
        {
            for(int i = 0; i <= 6; i = i+3)
            {
                Console.WriteLine("{0} | {1} | {2}         {3} | {4} | {5}", i, i + 1, i + 2, ConvertGameSymbol(board[i]), ConvertGameSymbol(board[i+1]), ConvertGameSymbol(board[i+2]));
                if(i != 6)
                    Console.WriteLine("----------        ----------");
            }
        }

        string ConvertGameSymbol(int symbol)
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
