using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            Loader.LoaderCallBack();
            Debug.Log("da kich hoat LoaderCallBack");
        }
    }
}
