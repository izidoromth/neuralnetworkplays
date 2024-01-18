using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPlaysCsharp
{
    public class NeuralNetworkIndividual
    {
        float winRate;

        int[][] inputs = new int[][] { };
        int[][] outputs = new int[][] { };

        ActivationNetwork network;

        public NeuralNetworkIndividual(ActivationNetwork activationNetwork, int games)
        {
            network = activationNetwork;

            GenerateTrainingData(games);
        }

        public void Train(int epochs, int errorThreshold)
        {
            int epoch = 0;
            double error = double.MaxValue;

            BackPropagationLearning teacher = new BackPropagationLearning(network);

            while (epoch < epochs && error > errorThreshold)
            {
                error = teacher.RunEpoch(inputs.Select(i => i.Select(j => (double)j).ToArray()).ToArray(), outputs.Select(i => i.Select(j => (double)j).ToArray()).ToArray());
                Console.WriteLine($"Epoch: {epoch} | Error: {error}");
                epoch++;
            }

            network.Save("tictacnet");
        }

        public double[] Compute(double[] input)
        {
            return network.Compute(input);
        }

        void GenerateTrainingData(int games)
        {
            Console.WriteLine("Generating training data...");
            List<int[]> inputsList = new List<int[]>();
            List<int[]> outputsList = new List<int[]>();

            for (int g = 0; g < games; g++)
            {
                int player = 1;
                int[] board = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                int[][] availableMoves = TicTacToe.GetAvailableMoves(player, board);

                while (availableMoves.Length > 0 && TicTacToe.CheckGameState(board) == 0)
                {
                    inputsList.Add(board.Append(player).ToArray());

                    int[][] winMoves = availableMoves.Where(move => TicTacToe.CheckGameState(move) == player).ToArray();
                    int[][] drawMoves = availableMoves.Where(move => TicTacToe.CheckGameState(move) == 0).ToArray();

                    int[] output = new int[9];

                    if (winMoves.Length > 0)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (board[i] != winMoves[0][i])
                            {
                                output[i] = 1;
                                break;
                            }
                        }
                        board = winMoves[0];
                    }
                    else if (drawMoves.Length > 0)
                    {
                        int[] move = drawMoves[new Random().Next(drawMoves.Length)];
                        for (int i = 0; i <9; i++)
                        {
                            if (board[i] != move[i])
                            {
                                output[i] = 1;
                                break;
                            }
                        }
                        board = move;
                    }

                    outputsList.Add(output);

                    player = player == 1 ? 2 : 1;

                    availableMoves = TicTacToe.GetAvailableMoves(player, board);
                }
            }

            inputs = inputsList.ToArray();
            outputs = outputsList.ToArray();
        }
    }
}
