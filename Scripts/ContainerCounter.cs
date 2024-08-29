using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnGrabKitchenObject;
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;

    public override void Interact(Player player)
    {
        if (player.GetKitchenObject() == null)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectScriptableObject, player);

            if (OnGrabKitchenObject != null)
            {
                OnGrabKitchenObject(this, EventArgs.Empty);
            }
        }
    }

    public override void InteractPlate(Player player)
    {
        if (player.GetKitchenObject() != null)
        {
            //nguoi choi dang cam dia
            if (player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject plateKitChenObject = player.GetKitchenObject() as PlateKitchenObject;

                if (plateKitChenObject.PutKitchenObjectOnPlate(this.kitchenObjectScriptableObject))
                {
                    if (OnGrabKitchenObject != null)
                    {
                        OnGrabKitchenObject(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
