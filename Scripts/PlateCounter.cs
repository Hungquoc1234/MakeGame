using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{
    [SerializeField] private int maxPlateQuantity = 4;
    [SerializeField] private float timeSpawningPlate = 3f;
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;
    private float spawningPlateTimer;
    private int plateQuantity;

    public event EventHandler OnSpawnPlate;
    public event EventHandler OnRemovePlate;

    private void Update()
    {
        spawningPlateTimer += Time.deltaTime;

        if(spawningPlateTimer > timeSpawningPlate)
        {
            spawningPlateTimer = 0;

            if(plateQuantity < maxPlateQuantity)
            {
                plateQuantity++;

                if (OnSpawnPlate != null)
                {
                    OnSpawnPlate(this, EventArgs.Empty);
                }
            }
           
        }
    }

    public override void Interact(Player player)
    {
        if (plateQuantity > 0)
        {
            if (player.GetKitchenObject() == null)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectScriptableObject, player);

                plateQuantity--;

                if (OnRemovePlate != null)
                {
                    OnRemovePlate(this, EventArgs.Empty);
                }
            }
        }
    }
}
