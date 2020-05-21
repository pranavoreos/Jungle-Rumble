using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float velx;
    public float vely;
    private Rigidbody2D rb;
    public int damage = 30;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velx, vely);

    }

    // Update is called once per frame
    void Update()
    {
        timer += 1.0F * Time.deltaTime;
        if (timer >= 4)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.TakeDamge(damage);
        }
        Destroy(this.gameObject);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(collision.gameObject);
        }
    }
}
