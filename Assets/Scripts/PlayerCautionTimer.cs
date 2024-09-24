using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class PlayerCautionTimer : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    [Inject] private PlayerBehaviour player;

    private void Start()
    {
        player.OnColorChangeCaution += (() => StartCoroutine(CountdownCoroutine()));
    }

    IEnumerator CountdownCoroutine()
    {
        for(int x = 3; x > 0; x--)
        {
            text.SetText(x.ToString());
            yield return new WaitForSeconds(1f);
        }

        text.SetText("");
    }
}
