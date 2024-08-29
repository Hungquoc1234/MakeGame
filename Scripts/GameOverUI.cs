using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveryNumberText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver() == true)
        {
            gameObject.SetActive(true);

            deliveryNumberText.text = Mathf.Ceil(DeliveryManager.Instance.GetDeliveryCompletedNumber()).ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
