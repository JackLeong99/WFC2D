using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Module/New ModuleSet")]

public class ModuleSet : ScriptableObject
{
    public float setDimensions;
    [Space(8)]
    public List<Module> modules;

    public Vector2 GetSetDimensions() 
    {
        return new Vector2(setDimensions / 100f, setDimensions / 100f);
    }
}
