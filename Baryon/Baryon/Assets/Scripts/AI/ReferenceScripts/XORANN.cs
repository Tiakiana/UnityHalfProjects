using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XORANN : MonoBehaviour {

    void Start()
    {
        Program.Go();
    }


    public  class Program
        {
            private static Neuron OutPutNeuron1;
            private static Neuron HiddenNeuron1;
            private static Neuron HiddenNeuron2;




public            static void Go()
            {
                Debug.Log("How many times should I train?");
                int epo = 2000;

                train(epo);
                Debug.Log("Write the first number (1 or 0)");
                double X = 1;
                Debug.Log("Write the Second number (1 or 0)");

                double Y = 1;

                HiddenNeuron1.inputs = new double[] { X, Y};
                HiddenNeuron2.inputs = new double[] { X,Y};

                OutPutNeuron1.inputs = new double[] { HiddenNeuron1.output, HiddenNeuron2.output };

                Debug.Log("Hmm let's see. Based on my training I'd give it exactly " + OutPutNeuron1.output * 100 + "% chance of being a True!");


                if (OutPutNeuron1.output>0.5)
                {
                    Debug.Log("So my guess is that "+ X + " + " + Y +" Should be True");
                }
                else
                {
                    Debug.Log("So my guess is that "+X + " + " + Y + " Should be False");

                }



             //   Debug.Log("lol");
                
            }

            class sigmoid
            {
                public static double output(double x)
                {
                    return 1.0 / (1.0 + Mathf.Exp((float)-x));
                }

                public static double derivative(double x)
                {
                    return x * (1 - x);
                }
            }

            class  Neuron
            {
                public double[] inputs = new double[2];
                public double[] weights = new double[2];
                public double error;

                private double biasWeight;

                private System.Random r = new System.Random();

                public double output
                {
                    get { return sigmoid.output(weights[0] * inputs[0] + weights[1] * inputs[1] + biasWeight); }
                }

                public void randomizeWeights()
                {
                    weights[0] = r.NextDouble();
                    weights[1] = r.NextDouble();
                    biasWeight = r.NextDouble();
                }

                public void adjustWeights()
                {
                    weights[0] += error * inputs[0];
                    weights[1] += error * inputs[1];
                    biasWeight += error;
                }
            }

            private static void train( int ep)
            {
                // the input values
                double[,] inputs =
                {
                    { 0, 0},
                    { 0, 1},
                    { 1, 0},
                    { 1, 1}
                };

                // desired results
                double[] results = { 0, 1, 1, 0 };

                // creating the neurons
                Neuron hiddenNeuron1 = new Neuron();
                Neuron hiddenNeuron2 = new Neuron();
                Neuron outputNeuron = new Neuron();

                // random weights
                hiddenNeuron1.randomizeWeights();
                hiddenNeuron2.randomizeWeights();
                outputNeuron.randomizeWeights();

                int epoch = 0;

                Retry:
                epoch++;
                for (int i = 0; i < 4; i++)  // very important, do NOT train for only one example
                {
                    // 1) forward propagation (calculates output)
                    hiddenNeuron1.inputs = new double[] { inputs[i, 0], inputs[i, 1] };
                    hiddenNeuron2.inputs = new double[] { inputs[i, 0], inputs[i, 1] };

                    outputNeuron.inputs = new double[] { hiddenNeuron1.output, hiddenNeuron2.output };

                    Debug.Log("Thinking");

                    // 2) back propagation (adjusts weights)

                    // adjusts the weight of the output neuron, based on its error
                    outputNeuron.error = sigmoid.derivative(outputNeuron.output) * (results[i] - outputNeuron.output);
                    outputNeuron.adjustWeights();

                    // then adjusts the hidden neurons' weights, based on their errors
                    hiddenNeuron1.error = sigmoid.derivative(hiddenNeuron1.output) * outputNeuron.error * outputNeuron.weights[0];
                    hiddenNeuron2.error = sigmoid.derivative(hiddenNeuron2.output) * outputNeuron.error * outputNeuron.weights[1];

                    hiddenNeuron1.adjustWeights();
                    hiddenNeuron2.adjustWeights();
                }

                if (epoch < ep)
                    goto Retry;
                OutPutNeuron1 = outputNeuron;
                HiddenNeuron1 = hiddenNeuron1;
                HiddenNeuron2 = hiddenNeuron2;
               
            }
        }


    }




        

