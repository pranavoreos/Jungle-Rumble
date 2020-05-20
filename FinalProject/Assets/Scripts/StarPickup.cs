using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour
{
    public AudioClip pickupClup;
    public int stars = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            AudioSource.PlayClipAtPoint(pickupClup, transform.position);
            Destroy(this.gameObject);
            stars += 1;
        }
    }
}
