using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Job : ThreadedJob
{
    public Vector3[] InData;  // arbitary job data
    public Vector3[] OutData; // arbitary job data

    private StreamWriter _writer;
    private StreamReader _reader;
    private string _serverMessage;
    private bool _running = false;

   

    public void Connect()
    {
        TcpClient server = new TcpClient("localHost", 8000);
        //Den strøm i hvilken vores data sendes
        NetworkStream stream = server.GetStream();
        //instantialiesering af reader og writer
        _reader = new StreamReader(stream);
        _writer = new StreamWriter(stream);
        StartRecieveing();
        Debug.Log("All systems Nominal");
    }

    public void StartRecieveing()
    {
        //Starter en tråd der sørger for at klienten får beskeder tilbage fra serveren.
        Thread recieveThread = new Thread(ThreadFunction);
        //Starter tråden
        recieveThread.Start();

     //   Thread startRunning = new Thread(Run);
      //  startRunning.Start();

    }

    public void ReadFromServer()
    {
        _running = true;
        while (_running)
        {
            _serverMessage = _reader.ReadLine();
            //Printer til konsollen hvad vi har fået
            Debug.Log(_serverMessage);
        }


    }

    public override void Start()
    {
        Connect();
        base.Start();
    }

    public void SendRequest(string msg)
    {

        _writer.WriteLine(msg);
        _writer.Flush();
    }



    protected override void ThreadFunction()
    {
       // Debug.Log("LKistenen");
        _running = true;
        while (_running)
        {
            _serverMessage = _reader.ReadLine();
            //Printer til konsollen hvad vi har fået
            Debug.Log(_serverMessage.ToString());
        }
    }
    protected override void OnFinished()
    {
        // This is executed by the Unity main thread when the job is finished
    Debug.Log("Job's Done");
    }
}
