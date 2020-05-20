using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    private Rigidbody rb;

    public float runSpeed = 25f;
    public bool hasJumpFood = false;
    public bool hasSpeedFood = false;
    public int foodModAmount = 0;

    public AudioClip jumpClip;

    private float foodTimeMax = 10f;
    private float foodTimeCurr = 0f;

    float horizontalMove = 0f;
    bool jumpFlag = false;
    bool jump = false;

    [SerializeField] private float hurtForce = 10f;

    private enum State{ idle, running, jumping, falling}
    private State state = State.idle;

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
            if(animator.GetBool("IsJumping") == false)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                jump = true;
                animator.SetBool("IsJumping", true);
                state = State.falling;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" && state == State.falling)
        {
            if(state == State.falling)
            {
                Destroy(other.gameObject);
            }
            else
            {
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enermy is to my right therefore I should be damaged and move left
                    rb.velocity = new Vector3(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //enemy is to my left therefore I should be damaged and move right
                    rb.velocity = new Vector3(hurtForce, rb.velocity.y);
                }
            }
            
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
