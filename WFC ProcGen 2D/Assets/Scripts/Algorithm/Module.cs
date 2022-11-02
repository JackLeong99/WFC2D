using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Module/New Module")]

public class Module : ScriptableObject
{
    [Header("Weighting for this tile in core algorithm")]
    [Range(0.0f, 100.0f)]
    public float weighting;
    [Header("Default Sprite")]
    public Sprite tileSprite;
    [Header("If tile can have multiple sprites (Multiple tiles with same edges)")]
    public List<Sprite> spritePool;
    [Tooltip("This list must be the same length as Sprite Pool or it will use the default sprite")]
    public List<float> spritePoolWeighting;
    [Tooltip("First Code corresponds to bottom edge, going counter clockwise")]
    public Edge[] edges = new Edge[4];

    public Sprite GetRandomSprite()
    {
        if (spritePool.Count == 0 || spritePool.Count != spritePoolWeighting.Count) return tileSprite;
        float sum = 0;
        foreach (float f in spritePoolWeighting) sum += f;
        float rnd = Random.Range(0.0f, sum);

        for (int i = 0; i < spritePool.Count; i++) 
        {
            if (rnd < spritePoolWeighting[i]) return spritePool[i];
            rnd -= spritePoolWeighting[i];
        }
        return tileSprite;
    }
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
