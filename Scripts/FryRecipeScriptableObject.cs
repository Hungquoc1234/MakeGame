using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryRecipeScriptableObject : ScriptableObject
{
    public KitchenObjectScriptableObject input;
    public KitchenObjectScriptableObject output;
    public float fryingTimerMax;
}
