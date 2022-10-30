using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public static GridBuilder instance;
    [HideInInspector]
    public bool solving;
    [Header("Can be changed at runtime")]
    public bool autoRestart;
    [HideInInspector]
    public bool autoCollapse;
    public bool useWeighting;
    [Space(16)]
    [Tooltip("Upping this to 2 greatly reduces performance")]
    [Range(0, 2)]
    public int propagationDistance;
    [Space(16)]
    public bool useDelayedCollapse;
    [Range(0, 100)]
    public float delay;
    [Space(16)]
    public int width;
    public int height;

    [Header("Set Before runtime")]
    public bool usingComplexEdges;
    [Space(16)]
    public ModuleSet set;
    public List<Module> modules;

    public Cell[,] cells;
    [HideInInspector]
    public List<Cell> orderedCells;

    [Header("Tile")]
    [Tooltip("Default Sprite if desired")]
    public Sprite defaultSprite;
    [HideInInspector]
    public Vector2 spriteSize;
    public GameObject tile;

    private float camSize;

    private void Awake()
    {
        //Static instance isn't used at the moment but is kept to remind me once I get around to refactoring.
        if(!instance)
            instance = this;

        camSize = Camera.main.orthographicSize;
    }

    void Start()
    {
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

    public void GenerateGrid() 
    {
        GridReset();

        cells = new Cell[width, height];

        var bottomLeft = new Vector2
            (
                transform.position.x - width * spriteSize.x / 2f + spriteSize.x / 2,
                transform.position.y - height * spriteSize.y / 2f + spriteSize.y / 2
            );

        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var curPos = new Vector2(bottomLeft.x + x * spriteSize.x, bottomLeft.y + y * spriteSize.y);

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
        //SetScale has to happen after the cells are generated which is why it cant be a part of reset.
        SetScale();
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

    public void GridReset()
    {
        spriteSize = set.GetSetDimensions();
        modules = new List<Module>();
        modules.AddRange(set.modules);
        foreach (Transform child in gameObject.transform) Destroy(child.gameObject);
        gameObject.transform.localScale = new Vector2(1, 1);
    }

    public void SetScale()
    {
        float ratio = width > height ? (10f / width) * camSize : (10f / height) * camSize;
        ratio *= (0.16f / spriteSize.x);
        gameObject.transform.localScale = new Vector2(ratio, ratio);
    }

    public void next() 
    {
        if (orderedCells.Count == 0)
        {
            Debug.Log("Done!");
            solving = false;
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
