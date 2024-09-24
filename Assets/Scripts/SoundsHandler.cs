using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsHandler : MonoBehaviour
{
    public static void PlaySFX(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);
    }

}
