using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagnetBuff : AbstractBuff
{ 
    public override void PickUp(PlayerBehaviour player)
    {
        duration = 12f;
        timeLeft = duration;

        player.buffAtributesHolder.magnetVFXTransform.gameObject.SetActive(true);

        SoundsHandler.PlaySFX(player.buffAtributesHolder.magnetPickUpSFX, 0.4f);

        player.buffAtributesHolder.SpawnTextVFX(this);

        Debug.Log("Magnet picked up");
    }
    public override void UpdateBuff(PlayerBehaviour player)
    {
        timeLeft -= Time.deltaTime;
        player.buffAtributesHolder.magnetVFXTransform.position = player.GetTransform().position;

        if(timeLeft <= 0)
        {
            TimesUp(player);
        }
    }

    public override void TimesUp(PlayerBehaviour player)
    {
        player.buffAtributesHolder.magnetVFXTransform.gameObject.SetActive(false);
        player.currentBuff = null;

        Debug.Log("Magnet time is over");
    }

}
