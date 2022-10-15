using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Tile/New Tile")]

public class Module : ScriptableObject
{
    public Sprite tileSprite;
    public Edge[] edges = new Edge[4];
}

[Serializable]

public struct Edge
{
    public string code;

    public bool Compatible(Edge e) 
    {
        string temp = e.code;
        temp.Reverse();
        return temp.Equals(this.code);
    }
}
