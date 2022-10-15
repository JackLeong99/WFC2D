using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class Cell : MonoBehaviour
{
    public bool collapsed;

    public int entropy;

    public List<Module> possibleModules;

    public List<Module> availableModules;

    public Module activeModule;

    public Cell[] neighbours = new Cell[4];

    public List<Cell> unsolvedNeighbours;

    public GridBuilder grid;

    private void Awake()
    {
        possibleModules = new List<Module>();
        unsolvedNeighbours = new List<Cell>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !collapsed) 
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) 
            {
                Collapse();
            }
        }
    }

    public void SetPossibleModules() 
    {
        for (var i = 0; i < grid.modules.Count; i++)
        {
            possibleModules.Add(grid.modules[i]);
            availableModules.Add(grid.modules[i]);
            entropy = grid.modules.Count;
        }
    }

    public void SetModule() 
    {
        activeModule = availableModules[Random.Range(0, availableModules.Count)];
        GetComponent<SpriteRenderer>().sprite = activeModule.tileSprite;
        grid.orderedCells.Remove(this);
    }

    public void UpdateEntropy()
    {
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] == null || !neighbours[i].collapsed) continue;
            for (int m = 0; m < possibleModules.Count; m++)
            {
                if (!possibleModules[m].edges[i].Compatible(neighbours[i].activeModule.edges[(i + 2) % 4]))
                {
                    availableModules.Remove(possibleModules[m]);
                }
            }
        }
        if (availableModules.Count == 0)
        {
            Debug.LogError("Uh Oh spagettios. Failed to solve. Resetting!");
            grid.autoCollapse = true;
            grid.GenerateGrid();
        }
        entropy = availableModules.Count;
    }

    public void Collapse() 
    {
        SetModule();
        collapsed = true;
        Propogate();
    }

    public void Propogate() 
    {
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] == null || neighbours[i].collapsed) continue;
            unsolvedNeighbours.Add(neighbours[i]);
        }
        if (unsolvedNeighbours.Count == 0) 
        {
            grid.CheckDone();
            return;
        }
        foreach (Cell c in unsolvedNeighbours) 
        {
            c.UpdateEntropy();
        }
        unsolvedNeighbours.Sort((a, b) => a.entropy.CompareTo(b.entropy));
        unsolvedNeighbours[Random.Range(0, unsolvedNeighbours.Count)].Collapse();
    }
}
