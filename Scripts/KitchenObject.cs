using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;

    private InterfactKitchenObjectParent kitchenObjectParent;

    public KitchenObjectScriptableObject GetKitchenObjectScriptableObject()
    {
        return kitchenObjectScriptableObject;
    }

    public void SetKitchenObjectParent(InterfactKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.DeleteKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        this.transform.parent = kitchenObjectParent.GetTransformKitchenObjectPoint();

        this.transform.localPosition = Vector3.zero;
    }

    public static void SpawnKitchenObject(KitchenObjectScriptableObject kitchenObjectScriptableObject, InterfactKitchenObjectParent interfactKitchenObjectParent)
    {
        GameObject spawned = Instantiate(kitchenObjectScriptableObject.prefab);

        spawned.GetComponent<KitchenObject>().SetKitchenObjectParent(interfactKitchenObjectParent);
    }

    public void DestroyKitchenObject()
    {
        kitchenObjectParent.DeleteKitchenObject();

        Destroy(this.gameObject);
    }
}
