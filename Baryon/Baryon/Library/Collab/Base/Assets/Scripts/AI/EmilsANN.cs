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
                hidden.Inputs.Add(Input.Neurons[inputneuron].Inputs);
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
        Output.Neurons[mentorMove].Error = 1 - Output.Neurons[mentorMove].Output;

        for (int hidden = 0; hidden < Hidden.Neurons.Count; hidden++)
        {
            Hidden.Neurons[hidden].Error = Output.Neurons[mentorMove].Weights[hidden] * Output.Neurons[mentorMove].Error * sigmoid.derivative(Hidden.Neurons[hidden].Output);
        }
    }
    public void AdjustWeights()
    {
        Output.Neurons[mentorMove].DeltaOutputSum = Output.Neurons[mentorMove].Output * (1 - Output.Neurons[mentorMove].Output) * Output.Neurons[mentorMove].Error;
        
        foreach (Neuron item in Hidden.Neurons)
        {
            for (int i = 0; i < item.Weights.Count; i++)
            {
                item.Weights[i] = LearningRate * Output.Neurons[mentorMove].DeltaOutputSum; //item.Error * item.Output 
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
            AdjustWeights();


        }
        else
        {

            Debug.Log("Mentor knew shit. Cannot train");
        }


        // Beregn Error og justér
        Debug.Log("This is the neuron that should be 1: and it's error is " + Output.Neurons[mentorMove].Error);
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
        
        public double Threshold;
        public double DeltaOutputSum;
        public double Error;
        public double Output
        {
            get
            {
                double res = 0;
                for (int i = 0; i < Inputs.Count; i++)
                {
                    res += Inputs[i] * Weights[i];
                }
                return sigmoid.output(res);
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
