using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDestroyer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buff"))
        {
            Destroy(collision.gameObject);
        }
    }
}
