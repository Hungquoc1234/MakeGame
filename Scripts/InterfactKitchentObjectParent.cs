using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfactKitchenObjectParent
{
    public Transform GetTransformKitchenObjectPoint();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void DeleteKitchenObject();
}
