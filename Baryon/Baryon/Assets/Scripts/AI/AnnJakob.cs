using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class AnnJakob : MonoBehaviour
{
    public Pawn[] AIPawns = new Pawn[3];


    public int NumberOfHidden;
    public bool IsPlayer1;
    public InputLayer InputLaye = new InputLayer();
    public Layer HiddenLaye, OutputLaye;
    public static double LearningRate = 0.9;
    public List<double> HiddenWeigthsAdjustments, OutputWeightsAdjustments;
    public QLearn Mentor;
    public GameManager gm;
    public int MentorMove;
    int saveinterval;

    void Start()
    {
        StartCoroutine("waitforeverathing");
        HiddenLaye = new Layer();
        OutputLaye = new Layer();

        for (int i = 0; i < 25; i++)
        {
            InputLaye.Neurons.Add(new InputNeuron());
        }
        if (File.Exists("Hidden.xml") && File.Exists("Output.xml"))
        {
            LoadANN();
        }
        else
        {

            for (int i = 0; i < NumberOfHidden; i++)
        {
            HiddenLaye.Neurons.Add(new Neuron());

        }


        for (int i = 0; i < 15; i++)
        {

            OutputLaye.Neurons.Add(new Neuron());
        }
        foreach (Neuron item in OutputLaye.Neurons)
        {
            for (int i = 0; i < NumberOfHidden; i++)
            {

                item.Weights.Add(Random.value);
            }
            item.Bias = Random.value;
        }
        foreach (Neuron item in HiddenLaye.Neurons)
        {
            for (int i = 0; i < 25; i++)
            {
                item.Weights.Add(Random.value);
            }
            item.Bias = Random.value;
        }
        }



        

    }

    public IEnumerator waitforeverathing()
    {
        yield return new WaitForSeconds(0.5f);
        //    gm = GameManager.GmInst;

        if (IsPlayer1)
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player1Pawns[i].GetComponent<Pawn>();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                AIPawns[i] = gm.Player2Pawns[i].GetComponent<Pawn>();
            }
        }
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
            MentorMove = action.Pawn * 5 + action.Move;

        }
        // skal give et tal mellem 0 og 14;


        // Dette er det move vores belærte mentor Qlearn ville have gjort.

    }

    /*
     * To prove different weights
    Debug.Log("Weight 1 of hidden 1 " + HiddenLaye.Neurons[0].Weights[0]);
    Debug.Log("Weight 2 of hidden 1 " + HiddenLaye.Neurons[0].Weights[1]);
    Debug.Log("Weight 1 of hidden 2 " + HiddenLaye.Neurons[1].Weights[0]);
    Debug.Log("Weight 2 of hidden 2 " + HiddenLaye.Neurons[1].Weights[1]);
    */


    public void TakeTurn() {
        if (gm.player1sTurn == IsPlayer1)
        {
            GetMentorMove();
            TakeInput();
            FeedForwardInputToHidden();
            FeedForwardHiddenToOutput();
            if (WinnerIs() != MentorMove)
            {
                Train();
            }
            else
            {
                Debug.Log("Hey I know this");

                WinnerTakesAllTakeTurn();
            }
        }
    }



    public void Train() {
        if (Mentor.MentorMove() == null)
        {
            Debug.Log("Oh no my mentor knows nothing");
        }
        else
        {
            Epoch();

            int AnotherNeuron;
            here:
            AnotherNeuron = Random.Range(0, 15);
            if (AnotherNeuron == MentorMove)
            {
                goto here;
            }
           // Debug.Log("Another neuron was " + AnotherNeuron);
            //Debug.Log("Mentor move is " + MentorMove);
                while (WinnerIs() != MentorMove)
                {
                Epoch();
                }



            //   Debug.Log("I now know this move");
            if (saveinterval == 500)
            {
                SaveANN();
                saveinterval = 0;
            }
            else
            {
                saveinterval++;
            }
            WinnerTakesAllTakeTurn();
        }

    }

  


    public int WinnerIs() {
        double mxoutput = 0;
        int index = 999;
        for (int i = 0; i < 15; i++)
        {
            if (OutputLaye.Neurons[i].Result > mxoutput)
            {
                index = i;
                mxoutput = OutputLaye.Neurons[i].Result;
            }
        }
        return index;
    }


    public void WinnerTakesAllTakeTurn() {
        double mxoutput = 0;
        int index = 999;
        for (int i = 0; i < 15; i++)
        {
            if (OutputLaye.Neurons[i].Result> mxoutput)
            {
                index = i;
                mxoutput = OutputLaye.Neurons[i].Result;
            }
        }
      //  Debug.Log("index = " + index + " Mentormove = " + MentorMove);

        if (index!= 999)
        {
            switch (index)
            {
                //Blue
                case 0:
                    AIPawns[0].Move(Pawn.ConvertIntToDir(0));
                    break;
                case 1:
                    AIPawns[0].Move(Pawn.ConvertIntToDir(1));
                    break;
                case 2:
                    AIPawns[0].Move(Pawn.ConvertIntToDir(2));
                    break;
                case 3:
                    AIPawns[0].Move(Pawn.ConvertIntToDir(3));
                    break;
                case 4:
                    AIPawns[0].Move(Pawn.ConvertIntToDir(4));
                    break;

                    //Red
                case 5:
                    AIPawns[1].Move(Pawn.ConvertIntToDir(0));
                    break;
                case 6:
                    AIPawns[1].Move(Pawn.ConvertIntToDir(1));
                    break;
                case 7:
                    AIPawns[1].Move(Pawn.ConvertIntToDir(2));
                    break;
                case 8:
                    AIPawns[1].Move(Pawn.ConvertIntToDir(3));
                    break;
                case 9:
                    AIPawns[1].Move(Pawn.ConvertIntToDir(4));
                    break;
                    //Green
                case 10:
                    AIPawns[2].Move(Pawn.ConvertIntToDir(0));
                    break;
                case 11:
                    AIPawns[2].Move(Pawn.ConvertIntToDir(1));
                    break;
                case 12:
                    AIPawns[2].Move(Pawn.ConvertIntToDir(2));
                    break;
                case 13:
                    AIPawns[2].Move(Pawn.ConvertIntToDir(3));
                    break;
                case 14:
                    AIPawns[2].Move(Pawn.ConvertIntToDir(4));
                    break;
                default:
                    break;
            }
        }
    }

void Update()
    {
        if (Input.GetKeyUp("i"))
        {
            Train();
         /*   for (int i = 0; i < 2000; i++)
            {
                Epoch();
            }
            foreach (Neuron item in OutputLaye.Neurons)
            {
                Debug.Log(item.Result);
            }
            */
        }


    }


    public void FeedForwardInputToHidden()
    {
        for (int x = 0; x < HiddenLaye.Neurons.Count; x++)
        {
            HiddenLaye.Neurons[x].Inputs.Clear();

            for (int y = 0; y < InputLaye.Neurons.Count; y++)
            {
                // Mangler evt bias
                HiddenLaye.Neurons[x].Inputs.Add(InputLaye.Neurons[y].Input);
            }
            HiddenLaye.Neurons[x].CalculateInputSum();
            HiddenLaye.Neurons[x].CalculateResult();
        }

    }
    public void FeedForwardHiddenToOutput()
    {
        for (int x = 0; x < OutputLaye.Neurons.Count; x++)
        {
            OutputLaye.Neurons[x].Inputs.Clear();

            for (int y = 0; y < HiddenLaye.Neurons.Count; y++)
            {
                //Mangler evt. Bias
                OutputLaye.Neurons[x].Inputs.Add(HiddenLaye.Neurons[y].Result);
            }

            OutputLaye.Neurons[x].CalculateInputSum();
            OutputLaye.Neurons[x].CalculateResult();
        }
    }

    public void CalculateOutputError()
    {

        for (int i = 0; i < OutputLaye.Neurons.Count; i++)
        {
            if (i == MentorMove)
            {
                OutputLaye.Neurons[i].Error = 1 - OutputLaye.Neurons[i].Result;
            }
            else
            {
                OutputLaye.Neurons[i].Error = 0 - OutputLaye.Neurons[i].Result;

            }
        }




    }

    public void AdjustOutputWeights()
    {

        for (int x = 0; x < OutputLaye.Neurons.Count; x++)
        {
            OutputLaye.Neurons[x].AdjustmentsWeight.Clear();
            double deltaoutputsum = sigmoid.derivative(OutputLaye.Neurons[x].Result) * OutputLaye.Neurons[x].Error;
            for (int y = 0; y < OutputLaye.Neurons[x].Weights.Count; y++)
            {

                OutputLaye.Neurons[x].AdjustmentsWeight.Add(deltaoutputsum * HiddenLaye.Neurons[y].Result);
            }

        }

    }



    public void CalculateHiddenError()
    {
        for (int x = 0; x < HiddenLaye.Neurons.Count; x++)
        {
            double res = 0;
            for (int y = 0; y < OutputLaye.Neurons.Count; y++)
            {
                res += OutputLaye.Neurons[y].Error * OutputLaye.Neurons[y].Weights[x];
            }
            HiddenLaye.Neurons[x].Error = res * sigmoid.derivative(HiddenLaye.Neurons[x].Result);
        }
    }

    public void AdjustHiddenWeights()
    {
        for (int x = 0; x < HiddenLaye.Neurons.Count; x++)
        {
            HiddenLaye.Neurons[x].AdjustmentsWeight.Clear();
            for (int y = 0; y < HiddenLaye.Neurons[x].Weights.Count; y++)
            {
                // HiddenLaye.Neurons[x].Weights[y] += HiddenLaye.Neurons[x].Error * HiddenLaye.Neurons[x].Inputs[y] * LearningRate;
                //hiddenNeuron1.error = sigmoid.derivative(hiddenNeuron1.output) * outputNeuron.error * outputNeuron.weights[0];
                HiddenLaye.Neurons[x].AdjustmentsWeight.Add(HiddenLaye.Neurons[x].Error * HiddenLaye.Neurons[x].Inputs[y]);
            }

        }
    }

    public void ApplyDeltaWeights()
    {
        for (int x = 0; x < OutputLaye.Neurons.Count; x++)
        {
            OutputLaye.Neurons[x].ApplyAdjustments();
            OutputLaye.Neurons[x].Bias += OutputLaye.Neurons[x].Error;
        }
        for (int x = 0; x < HiddenLaye.Neurons.Count; x++)
        {
            HiddenLaye.Neurons[x].ApplyAdjustments();
            HiddenLaye.Neurons[x].Bias += HiddenLaye.Neurons[x].Error;
        }
    }

    public void Epoch()
    {
        //Debug.Log("Input 1 is " + InputLaye.Neurons[0].Input);
        //Debug.Log("Input 2 is " + InputLaye.Neurons[1].Input);
        if (Mentor.MentorMove() != null)
        {
           

            GetMentorMove();
        TakeInput();
        FeedForwardInputToHidden();
        //     Debug.Log("Hidden 1 result is " + HiddenLaye.Neurons[0].Result);
        //   Debug.Log("Hidden 2 result is " + HiddenLaye.Neurons[1].Result);


        FeedForwardHiddenToOutput();

        //     Debug.Log("output 1 result is " + OutputLaye.Neurons[0].Result);
        //   Debug.Log("output 2 result is " + OutputLaye.Neurons[1].Result);


        CalculateOutputError();

        //     Debug.Log("output 1 Error is " + OutputLaye.Neurons[0].Error);
        //    Debug.Log("output 2 Error is " + OutputLaye.Neurons[1].Error);

        AdjustOutputWeights();
        CalculateHiddenError();
        //      Debug.Log("Hidden 1 error is " + HiddenLaye.Neurons[0].Error);
        //    Debug.Log("Hidden 2 error is " + HiddenLaye.Neurons[1].Error);

        AdjustHiddenWeights();
        ApplyDeltaWeights();
      //  Debug.Log("Error of chosen neuron is " + Mathf.Abs((float)OutputLaye.Neurons[MentorMove].Error));
      //  Debug.Log("Error of second neuron is " + OutputLaye.Neurons[9].Error);
        }
        else
        {
            Debug.Log("Le Maître ne sais pas quoi á faire");
        }

    }


    public class Layer
    {
        public List<Neuron> Neurons = new List<Neuron>();

    }
    public class InputLayer
    {
        public List<InputNeuron> Neurons = new List<InputNeuron>();
    }
    public class InputNeuron
    {
        public double Input;
    }
    public class Neuron
    {
        public double InputSum;
        public List<double> Inputs = new List<double>();
        public List<double> Weights = new List<double>();
        public List<double> AdjustmentsWeight = new List<double>();
        public double DeltaOutputSum;
        public double Error;
        public double Result;
        public double Bias;

        public void CalculateInputSum()
        {
            double res = 0;
            for (int i = 0; i < Inputs.Count; i++)
            {
                res += Inputs[i] * Weights[i] + Bias;
            }
            InputSum = res;
        }
        public void CalculateResult()
        {
            double res = 0;
            for (int i = 0; i < Inputs.Count; i++)
            {
                // Burde have bias
                res += Inputs[i] * Weights[i] + Bias;
            }
            Result = sigmoid.output(res);
        }



        public void ApplyAdjustments()
        {

            for (int x = 0; x < Weights.Count; x++)
            {
                Weights[x] += AdjustmentsWeight[x] * LearningRate;
            }
            AdjustmentsWeight.Clear();
        }
    }


    public void TakeInput()
    {
        int[,] boardState = Board.BoardInst.ConvertBoardToANNInput();
        int ix = 0;

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                switch (boardState[x, y])
                {
                    //If empty
                    case 1:
                        InputLaye.Neurons[ix].Input = -1;
                        break;
                    //blå s1
                    case 2:
                        InputLaye.Neurons[ix].Input = -0.66;
                        break;
                    //Rød s1
                    case 3:
                        InputLaye.Neurons[ix].Input = -0.33;
                        break;
                    //grøn s1
                    case 4:
                        InputLaye.Neurons[ix].Input = 0;
                        break;
                    // blå s2
                    case 5:
                        InputLaye.Neurons[ix].Input = 0.33;
                        break;
                    // rød s2
                    case 6:
                        InputLaye.Neurons[ix].Input = 0.66;
                        break;
                    // grøn s2
                    case 7:
                        InputLaye.Neurons[ix].Input = 1;
                        break;
                    default:
                        break;
                }
                ix++;
            }
        }


        foreach (InputNeuron inputneuron in InputLaye.Neurons)
        {
            inputneuron.Input = 9999;
        }


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
    // save the hidden layer and output layer
    public void SaveANN()
    {
        XmlSerializer writer = new XmlSerializer(typeof(List<Neuron>));
        var HiddenPath = "Hidden.xml";
        var OutputPath = "Output.xml";

        FileStream hFile = File.Create(HiddenPath);
        System.Threading.Thread.Sleep(1);

        writer.Serialize(hFile, HiddenLaye.Neurons);
        hFile.Close();
        FileStream oFile = File.Create(OutputPath);
        System.Threading.Thread.Sleep(1);
        writer.Serialize(oFile, OutputLaye.Neurons);
        hFile.Close();
    }
    // load Hidden layer and output layer, remember to give filepath
    public void LoadANN()
    {
        var HiddenPath = "Hidden.xml";
        var OutputPath = "Output.xml";
        XmlSerializer reader = new XmlSerializer(typeof(List<Neuron>));
        StreamReader hFile = new StreamReader(HiddenPath);
        HiddenLaye.Neurons = (List<Neuron>)reader.Deserialize(hFile);
        hFile.Close();
        StreamReader oFile = new StreamReader(OutputPath);
        OutputLaye.Neurons = (List<Neuron>)reader.Deserialize(oFile);
        oFile.Close();

    }


}
