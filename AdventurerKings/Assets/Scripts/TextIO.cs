using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class TextIO : MonoBehaviour
{
    public static TextIO TxtIO;
    public double[,] TerrainTable;
    private void Awake()
    {
        TxtIO = this;

        TerrainTable = new double[15, 29];
        Load("E:\\Users\\Jakob\\UnityProjects\\AdventurerKings\\TerrainDemands.txt");
    }
    private void Start()
    {
    }



    private void Load(string fileName)
    {
        // Handle any problems that might arise when reading the text

        string line;
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as

        StreamReader theReader = new StreamReader(fileName, Encoding.Default);

        // Immediately clean up the reader after this block of code is done.
        // You generally use the "using" statement for potentially memory-intensive objects
        // instead of relying on garbage collection.
        // (Do not confuse this with the using directive for namespace at the 
        // beginning of a class!)
        string s;
        string[] YAxis = new string[29];
        int i = 0;
        while ((s = theReader.ReadLine()) != null)
        {

            YAxis[i] = s;
            i++;

        }
        theReader.Close();

        TerrainTable = new double[15, 29];


        for (int y = 0; y < 29; y++)
        {
            string[] these = YAxis[y].Split('\t');
            for (int x = 0; x < 15; x++)
            {
                TerrainTable[x, y] = double.Parse(these[x]);
            }
        }
        for (int ih = 0; ih < 15; ih++)
        {
    //        Debug.Log(table[ih, 0]);

        }

        //double ix = TerrainTable[0, 0];

    }

    public double[] GetTerrainDemands(int terrain) {
        double[] res = new double[29];
        for (int y = 0; y < 29; y++)
        {
            res[y] = TerrainTable[terrain,y];
        }

        return res;
    }



}


            
            /*
            if (s!= null)
            {

            }



            using (theReader)
        {
            // While there's lines left in the text file, do this:
            do
            {
                line = theReader.ReadLine();

                if (line != null)
                {
                    // Do whatever you need to do with the text line, it's a string now
                    // In this example, I split it into arguments based on comma
                    // deliniators, then send that array to DoStuff()
                    string[] entries = line.Split('\t');
                    if (entries.Length > 0)
                        {
                            foreach (string item in entries)
                            {
                                Debug.Log(item);
                            }
                        }
                        
                }
            }
            while (line != null);
            // Done reading, close the reader and return true to broadcast success    
            theReader.Close();
            return true;
        }
    }
    // If anything broke in the try block, we throw an exception with information
    // on what didn't work
    catch (Exception e)
    {
        //Console.WriteLine("{0}\n", e.Message);
        return false;
    }
    
}

    */


 



