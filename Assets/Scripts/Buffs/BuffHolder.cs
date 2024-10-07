using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHolder : MonoBehaviour
{
    enum BuffType { 
        Magnet,
        Rainbow,
        ScoreMultiplier
    }

    [SerializeField] private BuffType buffType;

    public AbstractBuff GetBuff()
    {
        switch (buffType)
        {
            case BuffType.Magnet:
                return new MagnetBuff();
            case BuffType.Rainbow:
                return new RainbowBuff();
            case BuffType.ScoreMultiplier:
                return new MultiplierBuff();
            default:
                Debug.Log("Error in BuffHolder");
                return null;
        }

    }
}
