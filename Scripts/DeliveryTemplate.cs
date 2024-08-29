using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{

    public DishScriptableObject Dish { get; private set; }
    public float ExpiryTime { get; private set; }

    public Delivery(DishScriptableObject dish, float expiryTime)
    {
        Dish = dish;
        ExpiryTime = expiryTime;
    }
}
