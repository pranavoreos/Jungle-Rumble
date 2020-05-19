using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public enum FoodType
    {
        Speed,
        Jump
    }

    public FoodType foodtype;
    public int foodModAmount = 0;

    private float floatingTimer = 0f;
    private float floatingMax = 1f;
    private float floatingDir = 0.01f;

    private void FixedUpdate()
    {
        if(floatingTimer < floatingMax)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + floatingDir);
            floatingTimer += Time.fixedDeltaTime;
        }
        else
        {
            floatingDir *= -1;
            floatingTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if(foodtype == FoodType.Jump)
            {
                collision.gameObject.GetComponent<PlayerMovement>().hasJumpFood = true;
            }
            else if(foodtype == FoodType.Speed)
            {
                collision.gameObject.GetComponent<PlayerMovement>().hasSpeedFood = true;
            }

            collision.gameObject.GetComponent<PlayerMovement>().foodModAmount = foodModAmount;

            Destroy(this.gameObject);
        }

    }

}
