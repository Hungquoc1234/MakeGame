using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    public static event EventHandler OnWalking;

    public static void ResetStaticData()
    {
        OnWalking = null;
    }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        if(footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if(OnWalking != null)
            {
                OnWalking(player, EventArgs.Empty);
            }
        }
    }
}
