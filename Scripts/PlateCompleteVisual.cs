using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVIsual : MonoBehaviour
{
    [Serializable]
    private struct KitchenScriptableObject_GameObject
    {
        public GameObject gameObject;
        public KitchenObjectScriptableObject kitchenObjectScriptableObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenScriptableObject_GameObject> kitchenScriptableObject_GameObjectsList;

    private void Start()
    {
        plateKitchenObject.OnKitchenObjectAdded += PlateKitchenObject_OnKitchenObjectAdded;
        plateKitchenObject.OnKitchenObjectTakenOut += PlateKitchenObject_OnKitchenObjectTakenOut;

        foreach (KitchenScriptableObject_GameObject kg in kitchenScriptableObject_GameObjectsList)
        {
            kg.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnKitchenObjectTakenOut(object sender, PlateKitchenObject.OnKitchenObjectTakenOutEventArgs e)
    {
        foreach (KitchenScriptableObject_GameObject kg in kitchenScriptableObject_GameObjectsList)
        {
            if (kg.kitchenObjectScriptableObject == e.kitchenObjectScriptableObject)
            {
                kg.gameObject.SetActive(false);
            }
        }
    }

    private void PlateKitchenObject_OnKitchenObjectAdded(object sender, PlateKitchenObject.OnKitchenObjectAddedEventArgs e)
    {
        foreach(KitchenScriptableObject_GameObject kg in kitchenScriptableObject_GameObjectsList)
        {
            if(kg.kitchenObjectScriptableObject == e.kitchenObjectScriptableObject)
            {
                kg.gameObject.SetActive(true);
            }
        }
    }
}
