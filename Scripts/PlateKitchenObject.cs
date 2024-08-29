using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] List<KitchenObjectScriptableObject> validKitchenObjectsScriptableObjectArray;
    private List<KitchenObjectScriptableObject> kitchenObjectScriptableObjectsList;

    public event EventHandler<OnKitchenObjectAddedEventArgs> OnKitchenObjectAdded;
    public event EventHandler<OnKitchenObjectTakenOutEventArgs> OnKitchenObjectTakenOut;

    public class OnKitchenObjectAddedEventArgs
    {
        public KitchenObjectScriptableObject kitchenObjectScriptableObject;
    }

    public class OnKitchenObjectTakenOutEventArgs
    {
        public KitchenObjectScriptableObject kitchenObjectScriptableObject;
    }

    private void Awake()
    {
        kitchenObjectScriptableObjectsList = new List<KitchenObjectScriptableObject>();
    }

    public bool PutKitchenObjectOnPlate(KitchenObjectScriptableObject kitchenObjectScriptableObject)
    {
        if(kitchenObjectScriptableObject != this.GetKitchenObjectScriptableObject())
        {
            if (validKitchenObjectsScriptableObjectArray.Contains(kitchenObjectScriptableObject))
            {
                if (!kitchenObjectScriptableObjectsList.Contains(kitchenObjectScriptableObject))
                {
                    kitchenObjectScriptableObjectsList.Add(kitchenObjectScriptableObject);

                    if(OnKitchenObjectAdded != null)
                    {
                        OnKitchenObjectAdded(this, new OnKitchenObjectAddedEventArgs
                        {
                            kitchenObjectScriptableObject = kitchenObjectScriptableObject,
                        });
                    }

                    return true;
                }

                return false;
            }

            return false;
        }

        return false;
    }

    public void TakeKitchenObjectOutOfPlate(InterfactKitchenObjectParent kitchenObjectParent)
    {
        if(kitchenObjectScriptableObjectsList.Count != 0)
        {
            KitchenObjectScriptableObject k = kitchenObjectScriptableObjectsList[kitchenObjectScriptableObjectsList.Count - 1];

            KitchenObject.SpawnKitchenObject(k, kitchenObjectParent);

            if (OnKitchenObjectTakenOut != null)
            {
                OnKitchenObjectTakenOut(this, new OnKitchenObjectTakenOutEventArgs
                {
                    kitchenObjectScriptableObject = k,
                });
            }

            kitchenObjectScriptableObjectsList.Remove(k);
        }
    }

    public void RemoveKitchenObjectsOutOfPlate()
    {
        kitchenObjectScriptableObjectsList.Clear();
    }

    public List<KitchenObjectScriptableObject> GetKitchenObjectScriptableObjectsList()
    {
        return kitchenObjectScriptableObjectsList;
    }
}
