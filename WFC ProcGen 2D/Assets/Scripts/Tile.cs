using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/New Tile")]

public class Tile : ScriptableObject
{
    public float size;
    public Vector2Int coordinates;
    public GameObject sprite;
    public List<Edge> edges;

    public void RotateRight() 
    {
        var last = edges[edges.Count - 1];
        edges.RemoveAt(edges.Count - 1);
        edges.Insert(0, last);
        sprite.transform.Rotate(0.0f, 0.0f, -90.0f);
    }
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
