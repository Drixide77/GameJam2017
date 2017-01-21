using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    Vector2 directionalInput;
    int wallDirX;

	public Bullet bulletPrefab;
	public Vector3 bulletOffset;
	private float fireCurrentCooldown;
	public float maxRateOfFire;

	//public Animations anim;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
		fireCurrentCooldown = 0.0f;
    }

    void Update()
    {
        CalculateVelocity();
        //HandleWallSliding();
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
		if (fireCurrentCooldown > 0.0f) fireCurrentCooldown -= Time.deltaTime;

		//Animations
		/*
		if (velocity.y > 0.05f || velocity.y < 0.05f) {
			anim.startJump ();
		} else anim.stopJump ();
		*/

		if (velocity.x > 0.05f) {
			//anim.flipX (false);
		} else if (velocity.x < 0.05f) {
			//anim.flipX (true);
		}
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

	public void Fire(){
		if (fireCurrentCooldown <= 0.0f) {
			Bullet myBullet = (Bullet)Instantiate (bulletPrefab, transform.position + bulletOffset*controller.collisions.faceDir, Quaternion.identity); //as Gameobject;
			myBullet.SetDirection (controller.collisions.faceDir);
			fireCurrentCooldown = maxRateOfFire;
		}
	}
		
    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

	public void getDestroyed(){
		print ("Player got destroyed");
	}

	public void InvertGravity(){
		gravity = gravity * -1.0f;
	}
}
