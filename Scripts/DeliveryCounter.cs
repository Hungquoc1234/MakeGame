using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if(player.GetKitchenObject() != null)
        {
            if(player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;

                DeliveryManager.Instance.CheckDelivery(plateKitchenObject);

                player.GetKitchenObject().DestroyKitchenObject();
            }
        }
    }
}
