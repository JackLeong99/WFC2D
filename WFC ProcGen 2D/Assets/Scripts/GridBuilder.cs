using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public static GridBuilder instance;
    [Header("Can be changed at runtime")]
    public bool autoRestart;
    [HideInInspector]
    public bool autoCollapse;
    public bool useWeighting;
    [Tooltip("Upping this to 2 greatly reduces performance")]
    [Range(0, 2)]
    public int propagationDistance;
    public bool useDelayedCollapse;
    public float delay;

    [Header("Set Before runtime")]
    public bool usingComplexEdges;
    public int width;
    public int height;
    [Space(16)]
    public List<Module> modules;

    [Header("Default tile object based on tileset resolution")]
    public GameObject tile;

    public Cell[,] cells;
    [HideInInspector]
    public List<Cell> orderedCells;
    private Vector2 spriteSize;
    private Camera _camera;

    private void Awake()
    {
        if(!instance)
            instance = this;
    }

    void Start()
    {
        if (useWeighting) modules.Sort((a, b) => b.weighting.CompareTo(a.weighting));
        //Automatically get the dimensions of the first modules sprite and convert it to a Vector2 for spacing the cells
        spriteSize = new Vector2(modules[0].tileSprite.rect.size.x / 100f, modules[0].tileSprite.rect.size.y / 100f);
        Debug.Log("x: " + modules[0].tileSprite.rect.size.x / 100f + ". y: " + modules[0].tileSprite.rect.size.y / 100f);
        SetCamera();
        GenerateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown("r")) 
        {
            autoCollapse = true;
            GenerateGrid();
        }
    }

    public void SetCamera() 
    {
        _camera = Camera.main;
        _camera.orthographicSize = (height * (spriteSize.x/2)) + 0.2f;
    }

    public void GenerateGrid() 
    {
        Reset();

        cells = new Cell[width, height];

        var scale = spriteSize;
        var origin = transform.position;
        var bottomLeft = new Vector2
            (
                origin.x - width * scale.x / 2f + scale.x / 2,
                origin.y - height * scale.y / 2f + scale.y / 2
            );

        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var curPos = new Vector2(bottomLeft.x + x * scale.x, bottomLeft.y + y * scale.y);

                var cellObj = Instantiate(tile, curPos, Quaternion.identity, gameObject.transform);
                cellObj.name = $"({x}, {y})";
                var cell = cellObj.GetComponent<Cell>();
                cell.grid = this;
                cell.SetPossibleModules();
                cells[x, y] = cell;

                if (x > 0)
                {
                    var leftCell = cells[x - 1, y];
                    cell.neighbours[3] = leftCell;
                    leftCell.neighbours[1] = cell;
                }

                if (y > 0)
                {
                    var bottomCell = cells[x, y - 1];
                    cell.neighbours[0] = bottomCell;
                    bottomCell.neighbours[2] = cell;
                }
            }
        FlattenCells();
        if (autoCollapse) orderedCells[Random.Range(0, orderedCells.Count)].Collapse(); 
    }

    public void FlattenCells()
    {
        orderedCells = new List<Cell>(cells.GetLength(0) * cells.GetLength(1));
        for (var i = 0; i < cells.GetLength(0); i++)
            for (var j = 0; j < cells.GetLength(1); j++)
            {
                orderedCells.Add(cells[i, j]);
            }
    }

    public void Reset()
    {
        foreach (Transform child in gameObject.transform) 
        {
            Destroy(child.gameObject);
        }
    }

    public void next() 
    {
        if (orderedCells.Count == 0)
        {
            Debug.Log("Done!");
            autoCollapse = false;
            return;
        }
        //This sorting method is pretty inefficient and causes stackoverflow errors when solving larger grids.
        //For example attempting to solve a 100x100 grid without the use of "delayed collapse" aka coroutines always leads to a stack overflow error.
        //its possible that this sorting method should only be used for first sort then once everything is relatively in order a different method should be used???
        orderedCells.Sort((a, b) => a.entropy.CompareTo(b.entropy));
        orderedCells[Random.Range(0, getLow(orderedCells))].Collapse();
    }

    public int getLow(List<Cell> cl) 
    {
        int l = 1;
        foreach (Cell c in cl) 
        {
            if (c.entropy > l) return cl.IndexOf(c) - 1;
        }
        return 0;
    }
}
