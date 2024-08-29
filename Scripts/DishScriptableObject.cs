using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DishScriptableObject : ScriptableObject
{
    public List<KitchenObjectScriptableObject> kitchenObjectScriptableObjectsList;
    public string dish;
    public float expiryTime;
}
