using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountDownToStart() == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString();
    }
}
