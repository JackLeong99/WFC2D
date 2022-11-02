using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GridBuilder grid;

    [Header("Delay indicator")]
    public TextMeshProUGUI delayLv;

    [Header("Weighting indicator")]
    public TextMeshProUGUI weightingToggle;
    public Color off;
    public Color on;

    [Header("Propogation distance")]
    public Animator pichu;
    public Animator pikachu;
    public Animator raichu;

    [Header("Dimensions indicator")]
    public TextMeshProUGUI widthText;
    public TextMeshProUGUI heightText;

    void Start()
    {
        grid = GridBuilder.instance;
        SetPropagationDistance(1);
    }

    void Update()
    {

    }

    public void SetDelay(float d)
    {
        grid.delay = d;
        delayLv.text = ("Lv" + (grid.delay + 1).ToString());
    }

    public void ToggleWeighting() 
    {
        grid.useWeighting = !grid.useWeighting;
        switch (grid.useWeighting) 
        {
            case true:
                weightingToggle.color = on;
                weightingToggle.text = "on";
                break;
            case false:
                weightingToggle.color = off;
                weightingToggle.text = "off";
                break;
        }
    }

    public void SetPropagationDistance(int i) 
    {
        grid.propagationDistance = i;
        switch (i) 
        {
            case 0:
                pichu.speed = 1;
                pikachu.speed = 0;
                raichu.speed = 0;
                break;
            case 1:
                pichu.speed = 0;
                pikachu.speed = 1;
                raichu.speed = 0;
                break;
            case 2:
                pichu.speed = 0;
                pikachu.speed = 0;
                raichu.speed = 1;
                break;
        }
    }

    public void SetGridWidth(float w) 
    {
        grid.width = (int)w;
        widthText.text = w.ToString();
    }

    public void SetGridHeight(float h)
    {
        grid.height = (int)h;
        heightText.text = h.ToString();
    }
}
