using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject deliveryTemplate;
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        deliveryTemplate.SetActive(false);

        iconImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnNewDelivery += DeliveryManager_OnNewDelivery; ;

        DeliveryManager.Instance.OnRemoveDelivery += DeliveryManager_OnRemoveDelivery; ;
    }

    private void DeliveryManager_OnRemoveDelivery(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnNewDelivery(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container.transform)
        {
            if (child == deliveryTemplate.transform) continue;

            Destroy(child.gameObject);
        }

        foreach (DishScriptableObject d in DeliveryManager.Instance.GetWaitingDishScriptableObjectsList())
        {
            GameObject spawnedDeliveryTemplate = Instantiate(deliveryTemplate, container.transform);

            spawnedDeliveryTemplate.SetActive(true);

            Transform iconsContainer = spawnedDeliveryTemplate.transform.Find("Container");

            TextMeshProUGUI dishName = spawnedDeliveryTemplate.transform.Find("DeliveryNameText").GetComponent<TextMeshProUGUI>();

            dishName.text = d.dish;

            IngredientIconHandler(d, spawnedDeliveryTemplate);
        }
    }

    private void IngredientIconHandler(DishScriptableObject dishScriptablObject, GameObject deliveryTemplate)
    {
        Transform iconsContanier = deliveryTemplate.transform.Find("Container");

        foreach (KitchenObjectScriptableObject kitchenScriptableObject in dishScriptablObject.kitchenObjectScriptableObjectsList)
        {
            Image spawnedImage = Instantiate(iconImage, iconsContanier);

            spawnedImage.sprite = kitchenScriptableObject.sprite;

            spawnedImage.gameObject.SetActive(true);
        }
    }
}


