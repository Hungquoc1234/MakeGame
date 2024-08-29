using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager: MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private List<DishScriptableObject> dishScriptableObjectsList;

    private List<DishScriptableObject> waitingDishScriptableObjectsList;
    private float spawnDishRequestTimer;
    private float spawnDishRequestTimerMax = 7f;
    private int waitingDishMax = 4;
    private int deliveryCompletedNumber;

    public event EventHandler OnNewDelivery;
    public event EventHandler OnRemoveDelivery;
    public event EventHandler OnDeliverSuccessfully;
    public event EventHandler OnDeliverFailed;         

    private void Awake()
    {
        Instance = this;

        waitingDishScriptableObjectsList = new List<DishScriptableObject>();
    }

    private void Start()
    {
        spawnDishRequestTimer = 4f;
    }

    private void Update()
    {
        spawnDishRequestTimer -= Time.deltaTime;
        if (spawnDishRequestTimer <= 0f)
        {
            spawnDishRequestTimer = spawnDishRequestTimerMax;

            if (waitingDishScriptableObjectsList.Count < waitingDishMax)
            {
                DishScriptableObject waitingDishScriptableObject = dishScriptableObjectsList[UnityEngine.Random.Range(0, dishScriptableObjectsList.Count)];

                Debug.Log(waitingDishScriptableObject);

                waitingDishScriptableObjectsList.Add(waitingDishScriptableObject);

                if (OnNewDelivery != null)
                {
                    OnNewDelivery(this, EventArgs.Empty);
                }
            }
        }
    }

    public void CheckDelivery(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingDishScriptableObjectsList.Count; i++)
        {
            DishScriptableObject dishScriptableObject = waitingDishScriptableObjectsList[i];

            if (dishScriptableObject.kitchenObjectScriptableObjectsList.Count == plateKitchenObject.GetKitchenObjectScriptableObjectsList().Count)
            {
                bool correctDelivery = true;

                foreach (KitchenObjectScriptableObject waitingIngredient in dishScriptableObject.kitchenObjectScriptableObjectsList)
                {
                    bool matchIngredient = false;

                    foreach (KitchenObjectScriptableObject plateIngredient in plateKitchenObject.GetKitchenObjectScriptableObjectsList())
                    {
                        if (waitingIngredient == plateIngredient)
                        {
                            matchIngredient = true;

                            break;
                        }
                    }

                    if (matchIngredient == false)
                    {
                        correctDelivery = false;

                        break;
                    }
                }

                if (correctDelivery == true)
                {
                    Debug.Log("correct delivery");

                    waitingDishScriptableObjectsList.RemoveAt(i);

                    deliveryCompletedNumber++;

                    if (OnRemoveDelivery != null)
                    {
                        OnRemoveDelivery(this, EventArgs.Empty);
                    }

                    if (OnDeliverSuccessfully != null)
                    {
                        OnDeliverSuccessfully(this, EventArgs.Empty);
                    }

                    return;
                }
            }
        }

        Debug.Log("incorrect delivery");

        if (OnDeliverFailed != null)
        {
            OnDeliverFailed(this, EventArgs.Empty);
        }
    }

    public List<DishScriptableObject> GetWaitingDishScriptableObjectsList()
    {
        return waitingDishScriptableObjectsList;
    }

    public int GetDeliveryCompletedNumber()
    {
        return deliveryCompletedNumber;
    }
}