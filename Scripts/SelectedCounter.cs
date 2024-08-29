using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    // Start is called before the first frame update
    void Start()
    {

        baseCounter = GetComponentInParent<BaseCounter>();

        Player.Instance.OnSelectedCounterChangedColor += Player_OnSelectedCounterChangedColor;
    }

    //h�m n�y d�ng ?? ??i m�u c?a clearCounter khi n� ???c ng??i ch?i ch?m
    private void Player_OnSelectedCounterChangedColor(object sender, Player.SelectedCounterChangedColorArgs e)
    {
        if(e.selectedCounterArgs == baseCounter)
        {
            foreach(GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(true);
            }
            
        }
        else
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(false);
            }
        }
    }
}
