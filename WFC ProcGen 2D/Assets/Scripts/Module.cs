using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/New Tile")]

public class Module : ScriptableObject
{
    public Sprite tileSprite;
    [Tooltip("First Code corresponds to bottom edge, going counter clockwise")]
    public Edge[] edges = new Edge[4];
    [Range(0.0f, 1.0f)]
    public float weighting;
}

[Serializable]

public struct Edge
{
    public string code;

    public bool Compatible(Edge e, bool r) 
    {
        if (r) return Reverse(e.code).Equals(this.code);
        else return e.code.Equals(this.code);
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
