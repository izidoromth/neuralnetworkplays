using AForge.Neuro.Learning;
using AForge.Neuro;
using System;

namespace NNPlaysCsharp;
class Program
{
    static void Main(string[] args)
    {
        NeuralNetworkIndividual nn = new NeuralNetworkIndividual(
            new ActivationNetwork(
                new SigmoidFunction(2),
                10,
                10,
                9),
            1000);

        nn.Train(2000, 50);

        TicTacToe game = new TicTacToe(new HumanPlayer(1), new NNPlayer(2, "tictacnet"));
        game.StartGame();

        // create population

        // play N games

        // crossover

        //mutation
    }
}