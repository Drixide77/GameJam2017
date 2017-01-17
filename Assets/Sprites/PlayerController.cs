using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public float maxSpeed = 5.0f;

    private float speed = 5.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;
    private Vector2 moveDirection = Vector2.zero;

    private bool grounded = false;
    private Animator anim;
    private CharacterController cc;


    // Use this for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (cc.isGrounded)
        {
            moveDirection = new Vector2(h, 0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            //anim.SetFloat("Speed", Mathf.Abs(h));
            if (Input.GetButton("Jump"))
            {
                //anim.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
            }
            moveDirection.y = 0.0f;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        cc.Move(moveDirection * Time.deltaTime);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
