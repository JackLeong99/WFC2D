using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class Cell : MonoBehaviour
{
    public bool collapsed;

    public bool entropyUpToDate;

    public int entropy;

    public int entropyLastUpdate;

    public List<Module> possibleModules;
    [HideInInspector]
    public List<Module> availableModules;

    public Module activeModule;

    public Cell[] neighbours = new Cell[4];

    public List<Cell> unsolvedNeighbours;

    public GridBuilder grid;

    private SpriteRenderer sRend;

    private void Awake()
    {
        possibleModules = new List<Module>();
        availableModules = new List<Module>();
        unsolvedNeighbours = new List<Cell>();
        sRend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        grid.resetCertainty.AddListener(Uncertain);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !collapsed) 
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) 
            {
                //UpdateEntropy();
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
        }
        entropy = grid.modules.Count;
        entropyLastUpdate = entropy;
    }

    public void SetModule() 
    {
        activeModule = entropy == 1 ? availableModules[0] : grid.useWeighting ? RandomWithWeight(availableModules) : availableModules[Random.Range(0, availableModules.Count)];
        availableModules = new List<Module>();
        availableModules.Add(activeModule);
        sRend.sprite = activeModule.tileSprite;
        grid.orderedCells.Remove(this);
    }

    public Module RandomWithWeight(List<Module> options) 
    {
        //generic weighting method from stackexchange
        float sum = 0;
        foreach (Module m in options) sum += m.weighting;
        float rnd = Random.Range(0.0f, sum);

        foreach (Module m in options)
        {
            if (rnd < m.weighting) return m;
            rnd -= m.weighting;
        }
        //Safety net error handling, should never be reachable.
        Debug.LogWarning("Uh Oh spagettios. Failed to pick at: " + this.name);
        if (grid.autoRestart && sum != 0)
        {
            grid.autoCollapse = true;
            grid.GenerateGrid();
        }
        return null;
    }

    //public void UpdateEntropy()
    //{
    //    for (int i = 0; i < neighbours.Length; i++)
    //    {
    //        if (neighbours[i] == null || !neighbours[i].collapsed) continue;
    //        for (int m = 0; m < possibleModules.Count; m++)
    //        {
    //            if (!possibleModules[m].edges[i].Compatible(neighbours[i].activeModule.edges[(i + 2) % 4], grid.usingComplexEdges))
    //            {
    //                availableModules.Remove(possibleModules[m]);
    //            }
    //        }
    //    }
    //    entropy = availableModules.Count;
    //}

    public void UpdateEntropy()
    {
        entropyUpToDate = true;
        entropyLastUpdate = availableModules.Count;
        int incompatabilityCount = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] == null) continue;
            for (int m = 0; m < possibleModules.Count; m++)
            {
                incompatabilityCount = 0;
                for (int n = 0; n < neighbours[i].availableModules.Count; n++)
                {
                    if (!possibleModules[m].edges[i].Compatible(neighbours[i].availableModules[n].edges[(i + 2) % 4], grid.usingComplexEdges))
                    {
                        incompatabilityCount++;
                    }
                }
                if (incompatabilityCount == neighbours[i].availableModules.Count)
                    availableModules.Remove(possibleModules[m]);
            }
        }
        entropy = availableModules.Count;
        if (entropy < entropyLastUpdate)
            for (int c = 0; c < neighbours.Length; c++) 
            {
                if (!neighbours[c]) continue;
                if (!neighbours[c].entropyUpToDate && !neighbours[c].collapsed)
                    neighbours[c].UpdateEntropy();
            }
    }

    public void Uncertain() 
    {
        entropyUpToDate = false;
    }

    public void Collapse()
    {
        if (entropy == 0)
        {
            Debug.LogWarning("Uh Oh spagettios. Failed to solve: " + this.name + ". Resetting!");
            if (grid.autoRestart)
            {
                grid.autoCollapse = true;
                grid.GenerateGrid();
            }
            return;
        }
        if (grid.useDelayedCollapse)
        {
            StartCoroutine(DelayedCollapse()); 
            return;
        }
        SetModule();
        collapsed = true;
        Propagate();
    }

    public IEnumerator DelayedCollapse() 
    {
        yield return new WaitForSeconds(grid.delay/100);
        SetModule();
        collapsed = true;
        Propagate();
    }

    public void Propagate() 
    {
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] == null || neighbours[i].collapsed) continue;
            unsolvedNeighbours.Add(neighbours[i]);
        }
        foreach (Cell c in unsolvedNeighbours) 
        {
            c.UpdateEntropy();
        }
        grid.next();
    }
}
