using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject iconTemplate;
    private List<GameObject> iconList;

    private void Awake()
    {
        iconTemplate.SetActive(false);
        iconList = new List<GameObject>();
    }

    private void Start()
    {
        plateKitchenObject.OnKitchenObjectAdded += PlateKitchenObject_OnKitchenObjectAdded;
        plateKitchenObject.OnKitchenObjectTakenOut += PlateKitchenObject_OnKitchenObjectTakenOut;
    }

    private void PlateKitchenObject_OnKitchenObjectTakenOut(object sender, PlateKitchenObject.OnKitchenObjectTakenOutEventArgs e)
    {
        //foreach(Transform child in this.transform)
        //{
        //    if (child == iconTemplate.transform) continue;
        //    Destroy(child);
        //}

        //foreach(KitchenObjectScriptableObject kitchenObjectScriptableObject in plateKitchenObject.GetKitchenObjectScriptableObjectsList())
        //{
        //    GameObject spawnedIcon = Instantiate(iconTemplate, this.transform);

        //    iconTemplate.SetActive(true);

        //    spawnedIcon.GetComponent<PlateIconSingleUI>().SetSpriteKitchenObjectScriptableObject(kitchenObjectScriptableObject);
        //}

        if(iconList.Count > 0)
        {
            GameObject lastIcon = iconList[iconList.Count - 1];

            iconList.RemoveAt(iconList.Count - 1);

            Destroy(lastIcon);
        }
    }

    private void PlateKitchenObject_OnKitchenObjectAdded(object sender, PlateKitchenObject.OnKitchenObjectAddedEventArgs e)
    {
        for (int i = 0; i < plateKitchenObject.GetKitchenObjectScriptableObjectsList().Count; i++)
        {
            KitchenObjectScriptableObject kitchenObjectScriptableObject = plateKitchenObject.GetKitchenObjectScriptableObjectsList()[i];

            if (i < iconList.Count)
            {
                // C?p nh?t icon ?ã t?n t?i
                iconList[i].GetComponent<PlateIconSingleUI>().SetSpriteKitchenObjectScriptableObject(kitchenObjectScriptableObject);
            }
            else
            {
                // T?o icon m?i
                GameObject spawnedIcon = Instantiate(iconTemplate, this.transform);
                spawnedIcon.SetActive(true);
                spawnedIcon.GetComponent<PlateIconSingleUI>().SetSpriteKitchenObjectScriptableObject(kitchenObjectScriptableObject);
                iconList.Add(spawnedIcon);
            }
        }
    }
}
