using AForge.Neuro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPlaysCsharp
{
    public abstract class Player
    {
        public int id;
        public Player(int _id) { 
            id = _id;
        }

        public abstract bool MakeMove(ref int[] board, int[][] availableMoves);
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(int _id) : base(_id)
        {
        }

        public override bool MakeMove(ref int[] board, int[][] availableMoves)
        {
            int pos;
            if(!int.TryParse(Console.ReadLine(), out pos))
            {
                return false;
            }
            board[pos] = id;
            return true;
        }
    }

    public class NNPlayer : Player
    {
        Network network;
        public NNPlayer(int _id, string filename) : base(_id)
        {
            network = Network.Load(filename);
        }

        public override bool MakeMove(ref int[] board, int[][] availableMoves)
        {
            double[] output = network.Compute(board.Append(id).Select(i => (double)i).ToArray());
            
            int pos = Array.IndexOf(output, output.Max());
            board[pos] = id;
            return true;
        }
    }
}
