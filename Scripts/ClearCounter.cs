using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if(kitchenObject == null)
        {
            if(player.GetKitchenObject() != null)
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            
        }
        else
        {
            if (player.GetKitchenObject() == null)
            {
                kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractPlate(Player player)
    {
        if(player.GetKitchenObject() != null)
        {
            //nguoi choi dang cam dia
            if(player.GetKitchenObject() is PlateKitchenObject)
            {
                //co vat khac tren clearcounter
                if (this.kitchenObject != null)
                {
                    PlateKitchenObject plateKitChenObject = player.GetKitchenObject() as PlateKitchenObject;

                    if (plateKitChenObject.PutKitchenObjectOnPlate(this.GetKitchenObject().GetKitchenObjectScriptableObject()))
                    {
                        this.GetKitchenObject().DestroyKitchenObject();
                    }
                }
                else
                {
                    PlateKitchenObject plateKitChenObject = player.GetKitchenObject() as PlateKitchenObject;

                    plateKitChenObject.TakeKitchenObjectOutOfPlate(this);
                }
            }
            //nguoi choi cam vat khac
            else
            {
                //co dia tren clearcounter
                if (this.kitchenObject is PlateKitchenObject)
                {
                    PlateKitchenObject plateKitChenObject = this.GetKitchenObject() as PlateKitchenObject;

                    if (plateKitChenObject.PutKitchenObjectOnPlate(player.GetKitchenObject().GetKitchenObjectScriptableObject()))
                    {
                        player.GetKitchenObject().DestroyKitchenObject();
                    }
                }
            }
        }
        else
        {
            if(this.kitchenObject is PlateKitchenObject)
            {
                PlateKitchenObject plateKitChenObject = this.GetKitchenObject() as PlateKitchenObject;

                plateKitChenObject.TakeKitchenObjectOutOfPlate(player);
            }
        }
    }
}
