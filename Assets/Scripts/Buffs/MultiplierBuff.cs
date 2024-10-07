using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierBuff : AbstractBuff
{
    public override void PickUp(PlayerBehaviour player)
    {
        duration = 15f;
        timeLeft = duration;

        UIScore.Instance.SetMultipliying(true);

        SoundsHandler.PlaySFX(player.buffAtributesHolder.multiplierPickUpSFX, 0.4f);

        player.buffAtributesHolder.SpawnTextVFX(this);

        player.buffAtributesHolder.multiplierVFX.Play();
        player.buffAtributesHolder.txtAnimator.SetTrigger("OnStart");
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

        UIScore.Instance.SetMultipliying(false);

        player.buffAtributesHolder.multiplierVFX.Stop();
        player.buffAtributesHolder.txtAnimator.SetTrigger("OnEnd");
    }

}
