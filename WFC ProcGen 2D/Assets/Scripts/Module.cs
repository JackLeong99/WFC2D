using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/New Tile")]

public class Module : ScriptableObject
{
    public GameObject GO;
    public Edge[] edges = new Edge[4];
}

[Serializable]

public struct Edge
{
    public List<char> code;

    public bool Compatible(Edge e) 
    {
        List<char> temp = e.code;
        temp.Reverse();
        switch (true) 
        {
            case bool x when temp == this.code:
                return true;
            default:
                return false;
        }
    }
}
