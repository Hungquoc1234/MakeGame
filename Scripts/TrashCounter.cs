using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;

    new public static void ResetStaticData()
    {
        OnTrash = null;
    }
    public override void Interact(Player player)
    {
        if(player.GetKitchenObject() != null)
        {
            player.GetKitchenObject().DestroyKitchenObject();

            if(OnTrash != null)
            {
                OnTrash(this,EventArgs.Empty);
            }
        }
    }
}
