using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, InterfactKitchenObjectParent
{
    public static event EventHandler OnPlaced;

    [SerializeField] protected Transform counterTopPoint;
    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player) { }
    public virtual void InteractAlternate(Player player) { }

    public virtual void InteractPlate(Player player) { }

    public static void ResetStaticData()
    {
        OnPlaced = null;
    }

    public Transform GetTransformKitchenObjectPoint()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            if (OnPlaced != null)
            {
                OnPlaced(this, EventArgs.Empty);
            }
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void DeleteKitchenObject()
    {
        kitchenObject = null;

    }
}
