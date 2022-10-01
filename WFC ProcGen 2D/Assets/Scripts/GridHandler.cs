using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public int dimensions;
    public List<Tile> tilePool;
    //[HideInInspector]
    public List<Tile> grid;
    
    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    public void GenerateGrid() 
    {
        grid = new List<Tile>(dimensions * dimensions);
        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++) 
            {

            }
        }
    }
}
