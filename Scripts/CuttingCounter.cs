using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, InterfaceHasProgress
{
    public event EventHandler<InterfaceHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    public event EventHandler OnCutting;
    public static event EventHandler OnCuttingSound;

    [SerializeField] private CuttingRecipeScriptableObject[] cuttingRecipeScriptableObjectArray;
    private int cuttingProgress;

    new public static void ResetStaticData()
    {
        OnCuttingSound = null;
    }

    public override void Interact(Player player)
    {
        if (this.kitchenObject == null)
        {
            if (player.GetKitchenObject() != null)
            {
                cuttingProgress = 0;

                player.GetKitchenObject().SetKitchenObjectParent(this);

                if(HasRecipe(this.GetKitchenObject().GetKitchenObjectScriptableObject()) == true)
                {
                    int cuttingTimesMax = GetCuttingRecipeScriptableObject(this.kitchenObject.GetKitchenObjectScriptableObject()).cuttingTimes;

                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs { progressNormalized = (float)cuttingProgress / cuttingTimesMax });
                    }
                }
            }
        }
        else
        {
            if (player.GetKitchenObject() == null)
            {
                this.kitchenObject.SetKitchenObjectParent(player);
            }
        } 
    }

    public override void InteractAlternate(Player player)
    {
        if (kitchenObject != null)
        {
            if (HasRecipe(this.GetKitchenObject().GetKitchenObjectScriptableObject()) == true)
            {
                cuttingProgress++;

                int cuttingTimesMax = GetCuttingRecipeScriptableObject(this.kitchenObject.GetKitchenObjectScriptableObject()).cuttingTimes;

                if (OnProgressChange != null)
                {
                    OnProgressChange(this, new InterfaceHasProgress.OnProgressChangeEventArgs { progressNormalized = (float)cuttingProgress / cuttingTimesMax });
                }

                if (cuttingProgress >= cuttingTimesMax)
                {
                    KitchenObjectScriptableObject output = GetSlicesAfterCutting(this.kitchenObject.GetKitchenObjectScriptableObject());

                    this.kitchenObject.DestroyKitchenObject();

                    KitchenObject.SpawnKitchenObject(output, this);
                }

                if(OnCutting != null)
                {
                    OnCutting(this, EventArgs.Empty);
                }

                if(OnCuttingSound != null)
                {
                    OnCuttingSound(this, EventArgs.Empty);
                }
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
            if (this.kitchenObject is PlateKitchenObject)
            {
                PlateKitchenObject plateKitChenObject = this.GetKitchenObject() as PlateKitchenObject;

                plateKitChenObject.TakeKitchenObjectOutOfPlate(player);
            }
        }
    }

    private KitchenObjectScriptableObject GetSlicesAfterCutting(KitchenObjectScriptableObject input)
    {
        CuttingRecipeScriptableObject output = GetCuttingRecipeScriptableObject(input);
        if (output != null)
        {
            return GetCuttingRecipeScriptableObject(input).output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipe(KitchenObjectScriptableObject input)
    {
        CuttingRecipeScriptableObject output = GetCuttingRecipeScriptableObject(input);
        if(output != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private CuttingRecipeScriptableObject GetCuttingRecipeScriptableObject(KitchenObjectScriptableObject input)
    {
        foreach (CuttingRecipeScriptableObject cuttingRecipeScriptableObject in cuttingRecipeScriptableObjectArray)
        {
            if (cuttingRecipeScriptableObject.input == input)
            {
                return cuttingRecipeScriptableObject;
            }
        }
        return null;
    }
}
