using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBuff : AbstractBuff
{
    public override void PickUp(PlayerBehaviour player)
    {
        duration = 4f;
        timeLeft = duration;

        ShapeSpawner.Instance.SpawnRainbowShapes(true);

        SoundsHandler.PlaySFX(player.buffAtributesHolder.rainbowPickUpSFX, 0.4f);

        player.buffAtributesHolder.SpawnTextVFX(this);

        player.buffAtributesHolder.rainbowVFX.Play();
    }
    public override void UpdateBuff(PlayerBehaviour player)
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            TimesUp(player);
        }
    }
    public override void TimesUp(PlayerBehaviour player)
    {
        player.currentBuff = null;

        ShapeSpawner.Instance.SpawnRainbowShapes(false);

        player.buffAtributesHolder.rainbowVFX.Stop();
    }

    
}
