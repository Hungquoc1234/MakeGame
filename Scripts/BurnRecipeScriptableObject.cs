using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurnRecipeScriptableObject : ScriptableObject
{
    public KitchenObjectScriptableObject input;
    public KitchenObjectScriptableObject output;
    public float burningTimerMax;
}
