using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GridBuilder grid;

    public TextMeshProUGUI delayLv;
    void Start()
    {
        grid = GridBuilder.instance;
    }

    void Update()
    {
        delayLv.text = grid.GetDelayAsString();
    }
}
