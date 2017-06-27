using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ArtificialNeuralNetwork : MonoBehaviour
{
    public QLearn Mentor;
    private int mentorMove = 0;
    public Layer Input, Hidden, Output;

    public double ThreshAdjust, BiasAdjust, LearningRate;

    //    public List<double> MentorMove = new List<double>();
    // Use this for initialization
    void Start()
    {
        Input = new Layer();
        Hidden = new Layer();
        Output = new Layer();

        List<Neuron> inputneurons = new List<Neuron>();
        for (int i = 0; i < 27; i++)
        {
            Neuron n = new Neuron();

            n.Weights.Add(1);
            n.Threshold = 0;
            n.BiasWeight = 1;
            inputneurons.Add(n);
        }
        Input.Neurons.AddRange(inputneurons);

        List<Neuron> hiddenNeurons = new List<Neuron>();
        for (int i = 0; i < 19; i++)
        {
            Neuron n = new Neuron();
            for (int x = 0; x < 27; x++)
            {
                n.Weights.Add(UnityEngine.Random.value);

            }
            n.Threshold = ThreshAdjust;
            n.BiasWeight = BiasAdjust;
            hiddenNeurons.Add(n);
        }
        Hidden.Neurons.AddRange(hiddenNeurons);

        List<Neuron> outputNeurons = new List<Neuron>();
        for (int i = 0; i < 15; i++)
        {
            Neuron n = new Neuron();
            for (int x = 0; x < 19; x++)
            {
                n.Weights.Add(UnityEngine.Random.value);

            }
            n.Threshold = ThreshAdjust;
            n.BiasWeight = BiasAdjust;
            outputNeurons.Add(n);
        }
        Output.Neurons.AddRange(outputNeurons);

        // Alle vægtene er gjort tilfældige



        StartCoroutine("WaitForEverAThing");

    }
    

    public void GetMentorMove()
    {

        QLearn.Action action = Mentor.MentorMove();
        if (action == null)
        {
            Debug.Log("No Known Action");
        }
        else
        {
            mentorMove = action.Pawn * 5 + action.Move;

        }
        // skal give et tal mellem 0 og 14;


        // Dette er det move vores belærte mentor Qlearn ville have gjort.

    }

    public void TakeInput()
    {
        foreach (Neuron inputneuron in Input.Neurons)
        {
            inputneuron.Inputs.Clear();
        }
        int[,] boardState = Board.BoardInst.ConvertBoardToANNInput();
        int ix = 0;
        

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                
                Input.Neurons[ix].Inputs.Add(boardState[x, y]);
                ix++;
            }
        }
        Input.Neurons[25].Inputs.Add((double)GameManager.GmInst.Player1Points);
        Input.Neurons[26].Inputs.Add((double)GameManager.GmInst.Player2Points);
        int i = 0;
        foreach (Neuron item in Input.Neurons)
        {

          
            Debug.Log("Input = "+ item.Inputs[0] + "\n" + "Weight = " + item.Weights[0] + "\n" + "Bias = " + item.BiasWeight + "\n" + "Output " + item.Output);
            
            //    Debug.Log("Weight = "+ item.Weights[0]);
            //    Debug.Log("Bias = " + item.BiasWeight);
            //    Debug.Log("error " + item.Error);
            //

            i++;
        }

    }
    public void SendToHiddenLayer()
    {
        // Husk at tage højde for threshhold

        foreach (Neuron hidden in Hidden.Neurons)
        {
           
                hidden.Inputs.Clear();



            for (int inputneuron = 0; inputneuron < Input.Neurons.Count; inputneuron++)
            {
             
                hidden.Inputs.Add(Input.Neurons[inputneuron].Output);
            }

        


        }


    }
    public void SendToOutputLayer()
    {
        // Husk at tage højde for threshhold

        foreach (Neuron item in Output.Neurons)
        {
            item.Inputs.Clear();

            foreach (Neuron hiddenneuron in Hidden.Neurons)
            {
                Debug.Log("This neuron has output: " + hiddenneuron.Output);
                Debug.Log("This neuron has Weights " + hiddenneuron.Weights[0]);
                Debug.Log("This neuron has BiasWeight " + hiddenneuron.BiasWeight);
                if (hiddenneuron.Output >= hiddenneuron.Threshold)
                {

                    item.Inputs.Add(hiddenneuron.Output);
                }
                else
                {
                    item.Inputs.Add(0);
                }

            }
        }

    }

    public void CalculateError()
    {
        // Skal gøre det der står på æsken.
        //Output.Neurons[mentorMove].Error = 1 -Output.Neurons[mentorMove].Output);
        //globalError = f’(output) * (desiredOutput - actualOutput)
        double outputErr = 0;
        Output.Neurons[mentorMove].Error = sigmoid.derivative(Output.Neurons[mentorMove].Output) * (1 - Output.Neurons[mentorMove].Output);


        outputErr += sigmoid.derivative(Output.Neurons[mentorMove].Output) * (1 - Output.Neurons[mentorMove].Output);
        Debug.Log("This here is my error " +outputErr);
        for (int i = 0; i < 15; i++)
        {
            if (i == mentorMove)
            {
                continue;
            }
            else
            {
                Output.Neurons[i].Error = sigmoid.derivative(Output.Neurons[i].Output) * (Output.Neurons[i].Output-0);
                outputErr += Output.Neurons[i].Error;
            }

        }
        // evt tag gennemsnittet af Erroren.

        // Calculate error på hiddenlayer
        foreach (Neuron item in Hidden.Neurons)
        {
            //hiddenNeuron1.error = sigmoid.derivative(hiddenNeuron1.output) * outputNeuron.error * outputNeuron.weights[0];

            item.Error = sigmoid.derivative(item.Output) * outputErr * Output.GetSummedWeights();
        }


        //outputNeuron.error = sigmoid.derivative(outputNeuron.output) * (results[i] - outputNeuron.output);

        //outputNeuron.adjustWeights();

    }

    public void AdjustWeights()
    {


        // Skal gøre det der står på æsken.

        foreach (Neuron item in Output.Neurons)
        {
            /*
             *                 weights[0] += error * inputs[0];
                weights[1] += error * inputs[1];
                biasWeight += error;
             * 
             * */
            for (int i = 0; i < item.Weights.Count; i++)
            {
                item.Weights[i] = item.Error * item.Inputs[i];

            }
            item.BiasWeight += item.Error;
        }

        foreach (Neuron item in Hidden.Neurons)
        {
            for (int i = 0; i < item.Weights.Count; i++)
            {
                item.Weights[i] = item.Error * item.Inputs[i];

            }
            item.BiasWeight += item.Error;
        }


    }


    public void Epoch()
    {

        // if (Mentor.MentorMove() != null)
        // {
        //   GetMentorMove();
        //Er den rigtige, men for testing purposes laver vi lige noget andet.

        TakeInput();
        SendToHiddenLayer();
        SendToOutputLayer();
        CalculateError();
        AdjustWeights();
        //   }
        // else
        // {

        // Debug.Log("Mentor knew shit. Cannot train");
        //}


        // Beregn Error og justér

    }

    IEnumerator WaitForEverAThing()
    {
        Debug.Log("Loading ANN");
        yield return new WaitForSeconds(2);
        Train(1);
    }

    public void Train(int epochs)
    {
        for (int i = 0; i < epochs; i++)
        {
            Epoch();
            foreach (Neuron item in Output.Neurons)
            {
                Debug.Log(item.Error);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyUp("i"))
        {
            Train(200);
        }
        if (UnityEngine.Input.GetKeyUp("p"))
        {
            //TakeInput();
        Train(3);
        }
    }

    public double ConvertActionToSingleInt(QLearn.Action action)
    {
        Debug.Log("Not yet implemented");
        return 0;
    }


    // Her smider vi lige en bunke classes til senere brug.

    public class Layer
    {


        public List<Neuron> Neurons = new List<Neuron>();
        public double GetSummedWeights()
        {
            double res = 0;
            foreach (Neuron item in Neurons)
            {
                res += item.Weights[0];
            }
            return res;
        }
    }

    public class Neuron
    {
        public List<double> Inputs = new List<double>();
        public List<double> Weights = new List<double>();
        public double BiasWeight;
        public double Error;
        public double Threshold;


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
