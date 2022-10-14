using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool collapsed;

    public List<Module> possibleModules;

    public Cell[] neighbours = new Cell[4];

    public GridBuilder grid;

    private void Awake()
    {
        possibleModules = new List<Module>();
    }

    public void SetPossibleModules() 
    {
        for (var i = 0; i < grid.modules.Count; i++)
        {
            possibleModules.Add(grid.modules[i]);
        }
    }
}
