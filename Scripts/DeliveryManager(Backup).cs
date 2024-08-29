//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class DeliveryManager : MonoBehaviour
//{
//    public static DeliveryManager Instance { get; private set; }

//    [SerializeField] private List<DishScriptableObject> dishScriptableObjectsList;
//    [SerializeField] private float deliveryExpiryTime = 10f; // Th?i gian h?t h?n

//    private List<Delivery> waitingDeliveries;
//    private float spawnDishRequestTimer;
//    private float spawnDishRequestTimerMax = 7f;
//    private int waitingDishMax = 4;

//    public event EventHandler OnNewDelivery;
//    public event EventHandler OnRemoveDelivery;

//    private void Awake()
//    {
//        Instance = this;
//        waitingDeliveries = new List<Delivery>();
//    }

//    private void Update()
//    {
//        spawnDishRequestTimer -= Time.deltaTime;
//        if (spawnDishRequestTimer <= 0f)
//        {
//            spawnDishRequestTimer = spawnDishRequestTimerMax;

//            if (waitingDeliveries.Count < waitingDishMax)
//            {
//                DishScriptableObject waitingDishScriptableObject = dishScriptableObjectsList[UnityEngine.Random.Range(0, dishScriptableObjectsList.Count)];
//                waitingDeliveries.Add(new Delivery(waitingDishScriptableObject, Time.time + deliveryExpiryTime));

//                OnNewDelivery?.Invoke(this, EventArgs.Empty);
//            }
//        }

//        // Ki?m tra và xóa các ??n hàng h?t h?n
//        for (int i = waitingDeliveries.Count - 1; i >= 0; i--)
//        {
//            if (waitingDeliveries[i].ExpiryTime <= Time.time)
//            {
//                waitingDeliveries.RemoveAt(i);
//                OnRemoveDelivery?.Invoke(this, EventArgs.Empty);
//            }
//        }
//    }

//    public void CheckDelivery(PlateKitchenObject plateKitchenObject)
//    {
//        for (int i = 0; i < waitingDeliveries.Count; i++)
//        {
//            var delivery = waitingDeliveries[i];

//            if (delivery.Dish.kitchenObjectScriptableObjectsList.Count == plateKitchenObject.GetKitchenObjectScriptableObjectsList().Count)
//            {
//                bool correctDelivery = true;

//                foreach (KitchenObjectScriptableObject waitingIngredient in delivery.Dish.kitchenObjectScriptableObjectsList)
//                {
//                    bool matchIngredient = false;

//                    foreach (KitchenObjectScriptableObject plateIngredient in plateKitchenObject.GetKitchenObjectScriptableObjectsList())
//                    {
//                        if (waitingIngredient == plateIngredient)
//                        {
//                            matchIngredient = true;
//                            break;
//                        }
//                    }

//                    if (!matchIngredient)
//                    {
//                        correctDelivery = false;
//                        break;
//                    }
//                }

//                if (correctDelivery)
//                {
//                    Debug.Log("correct delivery");
//                    waitingDeliveries.RemoveAt(i);
//                    OnRemoveDelivery?.Invoke(this, EventArgs.Empty);
//                    return;
//                }
//            }
//        }

//        Debug.Log("incorrect delivery");
//    }

//    public List<Delivery> GetWaitingDeliveries()
//    {
//        return waitingDeliveries;
//    }
//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class DeliveryManagerUI : MonoBehaviour
//{
//    [SerializeField] private GameObject container;
//    [SerializeField] private GameObject deliveryTemplate;
//    [SerializeField] private Image iconImage;

//    private void Awake()
//    {
//        deliveryTemplate.SetActive(false);
//        iconImage.gameObject.SetActive(false);
//    }

//    private void Start()
//    {
//        DeliveryManager.Instance.OnNewDelivery += DeliveryManager_OnNewDelivery;
//        DeliveryManager.Instance.OnRemoveDelivery += DeliveryManager_OnRemoveDelivery;
//    }

//    private void DeliveryManager_OnRemoveDelivery(object sender, System.EventArgs e)
//    {
//        UpdateVisual();
//    }

//    private void DeliveryManager_OnNewDelivery(object sender, System.EventArgs e)
//    {
//        UpdateVisual();
//    }

//    private void UpdateVisual()
//    {
//        // Xóa t?t c? các DeliveryTemplate hi?n t?i
//        foreach (Transform child in container.transform)
//        {
//            if (child == deliveryTemplate.transform) continue;
//            Destroy(child.gameObject);
//        }

//        // T?o m?i các DeliveryTemplate
//        foreach (Delivery delivery in DeliveryManager.Instance.GetWaitingDeliveries())
//        {
//            GameObject spawnedDeliveryTemplate = Instantiate(deliveryTemplate, container.transform);
//            spawnedDeliveryTemplate.SetActive(true);

//            Transform iconsContainer = spawnedDeliveryTemplate.transform.Find("Container");
//            TextMeshProUGUI dishName = spawnedDeliveryTemplate.transform.Find("DeliveryNameText").GetComponent<TextMeshProUGUI>();
//            dishName.text = delivery.Dish.dish;

//            IngredientIconHandler(delivery.Dish, spawnedDeliveryTemplate);
//        }
//    }

//    private void IngredientIconHandler(DishScriptableObject dishScriptableObject, GameObject deliveryTemplate)
//    {
//        Transform iconsContainer = deliveryTemplate.transform.Find("Container");

//        foreach (KitchenObjectScriptableObject kitchenScriptableObject in dishScriptableObject.kitchenObjectScriptableObjectsList)
//        {
//            Image spawnedImage = Instantiate(iconImage, iconsContainer);

//            spawnedImage.sprite = kitchenScriptableObject.sprite;

//            spawnedImage.gameObject.SetActive(true);
//        }
//    }
//}
