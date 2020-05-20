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
