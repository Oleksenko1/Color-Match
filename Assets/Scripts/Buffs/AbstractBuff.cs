using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuff
{
    public float duration;
    public float timeLeft;

    public abstract void PickUp(PlayerBehaviour player);

    public abstract void UpdateBuff(PlayerBehaviour player);

    public abstract void TimesUp(PlayerBehaviour player);
}
