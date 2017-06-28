using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EmilsANN : MonoBehaviour
{
    public QLearn Mentor;
    public int mentorMove = 0;
    public Layer Hidden, Output;
    public InputLayer Input;
    public double LearningRate, ThreshAdjust;
    // Use this for initialization
    void Start()
    {
        Input = new InputLayer();
        Hidden = new Layer();
        Output = new Layer();

        
        for (int i = 0; i < 75; i++)
        {
            Input.Neurons.Add(new InputNeuron());
        }
        for (int i = 0; i < 30; i++)
        {
            Neuron n = new Neuron();
            for (int x = 0; x < Input.Neurons.Count; x++)
            {
                n.Weights.Add(UnityEngine.Random.value);
            }
            n.Threshold = ThreshAdjust;
            Hidden.Neurons.Add(n);
        }
        for (int i = 0; i < 15; i++)
        {
            Neuron n = new Neuron();
            for (int x = 0; x < Hidden.Neurons.Count; x++)
            {
                n.Weights.Add(UnityEngine.Random.value);

            }
            n.Threshold = ThreshAdjust;
            Output.Neurons.Add(n);
        }
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
        int[,] boardState = Board.BoardInst.ConvertBoardToANNInput();
        int ix = 0;
        // Take Boardstate and convert it to 3 bit for each square
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                switch (boardState[x, y])
                {
                    case 1:
                        for (int z = 0; z < 3; z++)
                        {
                            if (z == 0 || z == 1)
                            {
                                Input.Neurons[ix + z].Inputs = 0;
                            }
                            else
                            {
                                Input.Neurons[ix + z].Inputs = 1;

                            }
                        }
                        break;
                    case 2:
                        for (int z = 0; z < 3; z++)
                        {
                            if (z == 0 || z == 2)
                            {
                                Input.Neurons[ix + z].Inputs = 0;
                            }
                            else
                            {
                                Input.Neurons[ix + z].Inputs = 1;

                            }
                        }
                        break;
                    case 3:
                        for (int z = 0; z < 3; z++)
                        {
                            if (z == 0)
                            {
                                Input.Neurons[ix + z].Inputs = 0;
                            }
                            else
                            {
                                Input.Neurons[ix + z].Inputs = 1;

                            }
                        }
                        break;
                    case 4:
                        for (int z = 0; z < 3; z++)
                        {
                            if (z == 1 || z == 2)
                            {
                                Input.Neurons[ix + z].Inputs = 0;
                            }
                            else
                            {
                                Input.Neurons[ix + z].Inputs = 1;

                            }

                        }
                        break;
                    case 5:
                        for (int z = 0; z < 3; z++)
                        {
                            if (z == 1)
                            {
                                Input.Neurons[ix + z].Inputs = 0;
                            }
                            else
                            {
                                Input.Neurons[ix + z].Inputs = 1;

                            }
                        }
                        break;
                    case 6:
                        for (int z = 0; z < 3; z++)
                        {
                            if (z == 2)
                            {
                                Input.Neurons[ix + z].Inputs = 0;
                            }
                            else
                            {
                                Input.Neurons[ix + z].Inputs = 1;

                            }
                        }
                        break;
                    case 7:
                        for (int z = 0; z < 3; z++)
                        {
                            Input.Neurons[ix + z].Inputs = 1;
                        }
                        break;
                    default:
                        break;
                }
                ix = ix + 3;
            }
        }

    }
    public void SendToHiddenLayer()
    {
        foreach (Neuron hidden in Hidden.Neurons)
        {
            hidden.Inputs.Clear();
            for (int inputneuron = 0; inputneuron < Input.Neurons.Count; inputneuron++)
            {
                hidden.Inputs.Add(Input.Neurons[inputneuron].Inputs * hidden.Weights[inputneuron]);
            }
        }
    }
    public void SendToOutputLayer()
    {
        // Husk at tage højde for threshhold
        foreach (Neuron item in Output.Neurons)
        {
            item.Inputs.Clear();
            for (int i = 0; i < Hidden.Neurons.Count; i++)
            {
                if ((Hidden.Neurons[i].Output * item.Weights[i]) >= Hidden.Neurons[i].Threshold)
                {

                    item.Inputs.Add(Hidden.Neurons[i].Output * item.Weights[i]);
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
        
        for (int i = 0; i < 15; i++)
        {
            if (i == mentorMove)
            {
                Output.Neurons[i].Error = 1 - Output.Neurons[i].Output;
                Debug.Log("output error for correct choise " + Output.Neurons[i].Error);
            }
            else
            {
                //      Output.Neurons[i].Error = sigmoid.derivative(Output.Neurons[i].Output) * (Output.Neurons[i].Output-0);
                Output.Neurons[i].Error = 0 - Output.Neurons[i].Output;
                Debug.Log("output error for incorrect choises " + Output.Neurons[i].Error);
                //   outputErr += Output.Neurons[i].Error;
            }

        }
        for (int i = 0; i < Output.Neurons.Count; i++)
        {
            Output.Neurons[i].DeltaWeights.Clear();
        }
        for (int i = 0; i < Hidden.Neurons.Count; i++)
        {
            Hidden.Neurons[i].DeltaWeights.Clear();
        }
        WeightsDiference(mentorMove);
             
        for (int j = 0; j < Output.Neurons.Count; j++)
        {
            for (int i = 0; i < Hidden.Neurons.Count; i++)
            {
                Hidden.Neurons[i].DeltaWeights.Clear();
            }
            if (j == mentorMove)
            {
                continue;
            }
            else if (Output.Neurons[j].Error > -0.75)
            {
                WeightsDiference(j);

            }
        }

    }
    public void WeightsDiference(int outputNr)
    {
        Output.Neurons[outputNr].DeltaOutputSum = sigmoid.derivative(Output.Neurons[outputNr].Output) * Output.Neurons[outputNr].Error;
        for (int i = 0; i < Hidden.Neurons.Count; i++)
        {
            Output.Neurons[outputNr].DeltaWeights.Add(Output.Neurons[outputNr].DeltaOutputSum * Hidden.Neurons[i].Output);
        }
        for (int i = 0; i < Hidden.Neurons.Count; i++)
        {
            //Delta hidden sum = delta output sum * hidden-to-outer weights * S'(hidden sum)
            Hidden.Neurons[i].DeltaOutputSum = Output.Neurons[outputNr].DeltaOutputSum * Output.Neurons[outputNr].Weights[i] * sigmoid.derivative(Hidden.Neurons[i].Output);
        }
        for (int i = 0; i < Hidden.Neurons.Count; i++)
        {
            for (int j = 0; j < Input.Neurons.Count; j++)
            {
                Hidden.Neurons[i].DeltaWeights.Add(Hidden.Neurons[i].DeltaOutputSum * Input.Neurons[j].Inputs);
            }
        }
        for (int i = 0; i < Hidden.Neurons.Count; i++)
        {
            Output.Neurons[outputNr].Weights[i] = Output.Neurons[outputNr].Weights[i] + Output.Neurons[outputNr].DeltaWeights[i];
            for (int j = 0; j < Input.Neurons.Count; j++)
            {
                Hidden.Neurons[i].Weights[j] = Hidden.Neurons[i].Weights[j] + Hidden.Neurons[i].DeltaWeights[j];
            }
        }
        
    }
    public void Epoch()
    {

        if (Mentor.MentorMove() != null)
        {
            GetMentorMove();
            //Er den rigtige, men for testing purposes laver vi lige noget andet.
            TakeInput();
            SendToHiddenLayer();
            SendToOutputLayer();
            CalculateError();


        }
        else
        {

            Debug.Log("Mentor knew shit. Cannot train");
        }
    }
    IEnumerator WaitForEverAThing()
    {
        //     Debug.Log("Loading ANN");
        //       Debug.Log("MentorMove is " + mentorMove);

        yield return new WaitForSeconds(2);
        Train(1);
    }
    public void Train(int epochs)
    {
        for (int i = 0; i < epochs; i++)
        {
            Epoch();
            if (i == 4)
            {
                Debug.Log("Try now");
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyUp("t"))
        {
            Debug.Log("Hidden Summed weights: " + Hidden.GetSummedWeights());
            Debug.Log("Hidden Summed weights: " + Output.GetSummedWeights());
        }

        if (UnityEngine.Input.GetKeyUp("i"))
        {
            Train(1);
        }
        if (UnityEngine.Input.GetKeyUp("p"))
        {
            //TakeInput();
            Train(3);
        }
    }
    // Her smider vi lige en bunke classes til senere brug.
    public class InputLayer
    {
        public List<InputNeuron> Neurons = new List<InputNeuron>();
    }
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
    public class InputNeuron
    {
        public double Inputs;
    }
    public class Neuron
    {
        public List<double> Inputs = new List<double>();
        public List<double> Weights = new List<double>();
        public List<double> DeltaWeights = new List<double>();

        public double Threshold;
        public double DeltaOutputSum;
        public double Error;
        public double Output
        {
            get
            {
                return sigmoid.output(IndputSum);
            }
        }
        public double IndputSum
        {
            get
            {
                double res = 0;
                for (int i = 0; i < Inputs.Count; i++)
                {
                    res += Inputs[i];
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
}
