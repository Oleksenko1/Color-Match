using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagnetZoneScript : MonoBehaviour
{
    [Inject]
    private PlayerBehaviour player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shapes"))
        {
            Debug.Log("Shape collision with magnet zone");
            ShapeBehaiviour shapeBeh = collision.GetComponent<ShapeBehaiviour>();
            if (shapeBeh.GetColorSO() == player.GetColor())
            {
                shapeBeh.SetTarget(player.GetTransform());
            }
        }
    }
}
