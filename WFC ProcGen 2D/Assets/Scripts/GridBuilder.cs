using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tile;
    public List<Module> modules;
    public Cell[,] cells;
    
    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    public void GenerateGrid() 
    {
        Reset();

        cells = new Cell[width, height];

        var scale = tile.transform.localScale;
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
    }

    public void Solve()
    {



    }

    public void Reset()
    {
        foreach (Transform child in gameObject.transform) 
        {
            Destroy(child.gameObject);
        }
    }
}
