﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class QLearn : MonoBehaviour {
    Dictionary<int, float> ListOfStates = new Dictionary<int, float>();
	// Use this for initialization
	void Start () {
	
	}
    public class QNode {


        public QNode Parent;
        public List<QNode> Moves;
        public float Value;
        public int BoardState;
        public QNode(QNode parentnode, int boardState) {
            Parent = parentnode;
            Value = 0.5f;
            BoardState = boardState;

        }
        public QNode()
        {
            Parent = null;
            Value = 0.5f;

        }

    }
    public int HashBoardState() {
        int[,,] ex = Board.BoardInst.ConvertToBoardState();
        string s = "";
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    s += ex[x, y, z];

                }
            }
        }
        int hashedString = s.GetHashCode();
        return hashedString;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("Gemmer stadie");
            int ex = HashBoardState();
         
            // Debug.Log(ListOfStates[Board.BoardInst.ConvertToBoardState()]);

            ListOfStates.Add(ex, .5f);

            Debug.Log(ListOfStates.ContainsKey(HashBoardState()));
        }
        if (Input.GetKeyDown("b"))
        {
            Debug.Log("Henter værdien for dette stadie");
            Debug.Log(ListOfStates[HashBoardState()]);
        }
    }
}
