using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPickup : MonoBehaviour
{
    public AudioClip pickupClip;
    public int stars = 0;
    private Text scoretext;

    private void Start()
    {
        scoretext = GameObject.Find("StarScoreText").GetComponent<Text>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            AudioSource.PlayClipAtPoint(pickupClip, transform.position);
            Destroy(this.gameObject);
            scoretext.GetComponent<ScoreController>().score += 1;
            scoretext.GetComponent<ScoreController>().UpdateScore();

        }
    }
}
