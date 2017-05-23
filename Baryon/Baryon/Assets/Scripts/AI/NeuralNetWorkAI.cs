using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UnityEngine;


    class NeuralNetWorkAI : MonoBehaviour
    {


        /*

        idé:
        Jakob Arndal Så kører vi

            Idéen er at man laver 27 inputnodes
            1 for hvert felt + de to pointscores.

            Output er en pawn og en retning.
            så det er 2 neuroner

            så skal der være følgende neuroner i midterlaget
            ((27+2)*2)/3 = 19,33 neuroner ~ 19 mellemlagsneuroner

            Modtager hele numre fra boardstate af som er en double.
            Derfor skal der vi gå fra at have 2,2 til at være spiller to's blå brik til at bare lave 1 tal der smides ind i.

            

    */
        int[,,] BoardState = new int[5, 5, 5];
        /*
        static void Main(string[] args)
        {






        }
        */



        public class Layer
        {
            public List<Neuron> Neurons = new List<Neuron>();
        }

        public class Neuron
        {
            public List<double> Inputs = new List<double>();
            public List<double> Weights = new List<double>();
            public double BiasWeight;
            public double Error;

            public double Output
            {

                get
                {
                    double res = 0;
                    for (int i = 0; i < Inputs.Count; i++)
                    {
                        res += Inputs[i] * Weights[i] * BiasWeight;

                    }
                    return res;
                }
            }


        }

        class sigmoid
        {
            public static double output(double x)
            {
                return 1.0 / (1.0 + Math.Exp(-x));
            }

            public static double derivative(double x)
            {
                return x * (1 - x);
            }
        }

        // Kig lige en extra gang på denne her:

        private static double[] Softmax(double[] oSums)
        {
            // determine max output sum
            // does all output nodes at once so scale doesn't have to be re-computed each time
            double max = oSums[0];
            for (int i = 0; i < oSums.Length; ++i)
                if (oSums[i] > max) max = oSums[i];

            // determine scaling factor -- sum of exp(each val - max)
            double scale = 0.0;
            for (int i = 0; i < oSums.Length; ++i)
                scale += Math.Exp(oSums[i] - max);

            double[] result = new double[oSums.Length];
            for (int i = 0; i < oSums.Length; ++i)
                result[i] = Math.Exp(oSums[i] - max) / scale;

            return result; // now scaled so that xi sum to 1.0
        }





    }

