using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpFood = false;
    public bool hasSpeedFood = false;
    public int foodModAmount = 0;

    private float foodTimeMax = 10f;
    private float foodTimeCurr = 0f;

    float horizontalMove = 0f;
    bool jumpFlag = false;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate()
    {
        if(hasJumpFood && foodTimeCurr < foodTimeMax)
        {
            controller.m_JumpForceMod = foodModAmount;
            foodTimeCurr += Time.fixedDeltaTime;
        }
        else
        {
            foodTimeCurr = 0f;
            controller.m_JumpForceMod = 0;
            hasJumpFood = false;

        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }
}
