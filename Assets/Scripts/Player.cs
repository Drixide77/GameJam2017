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

	Animator anim;
	SpriteRenderer sr;
	public bool running = false;
	public bool jumping = false;
	public bool aiming = false;
	bool flipedX = false;
	float aim2idleCD = 0.5f;

	AudioSource audiosource;
	public AudioClip laser;
	public AudioClip tele;
	public AudioClip jump;
	public AudioClip wave;

	public GameObject teleport;

	Vector3 respawnPoint;
	[HideInInspector] public bool died = false;

    void Start()
    {
        controller = GetComponent<Controller2D>();
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		audiosource = GetComponent<AudioSource> ();

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

		// ------------- Animations ----------------------
		if (!jumping) {
			if (velocity.x > 0.5f || velocity.x < -0.5f) {
				if (!running) {
					print ("running!");
					anim.CrossFade ("running", 0.0f);
					running = true;
				}
			} else {
				running = false;
				if (aiming) {
					print ("aim");
					anim.CrossFade ("aim", 0.0f);
				} else {
					print ("idle");
					anim.CrossFade ("idle", 0.0f);
				}
			}
		}


		if (velocity.x > 0.05f) {
			sr.flipX = false;
			flipedX = false;
		} else if (velocity.x < -0.05f) {
			sr.flipX = true;
			flipedX = true;
		} else
			sr.flipX = flipedX; 

		if (Mathf.Sign(gravity) > 0.0f) {
			sr.flipY = true;
		} else sr.flipY = false;

		if (aiming) {
			aim2idleCD -= Time.deltaTime;
			if (aim2idleCD < 0.0f) {
				aim2idleCD = 0.5f;
				aiming = false;
			}
		}

		if (jumping) {
			if (controller.collisions.below && Mathf.Sign (gravity) < 0.0f) {
				jumping = false;
			}
			if (controller.collisions.above && Mathf.Sign (gravity) > 0.0f) {
				jumping = false;
			}

			if (aiming) {
				print ("jumpaim");
				anim.CrossFade ("jumpaim", 0.0f);
			} else {
				print ("jump");
				anim.CrossFade ("jump", 0.0f);
			}
		}
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
		jumping = true;
		running = false;
		print ("jump");
		audiosource.PlayOneShot (jump);
		anim.CrossFade ("jump", 0.0f);
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope && Mathf.Sign(gravity) < 0) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		} else if (controller.collisions.above && Mathf.Sign(gravity) > 0) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = (maxJumpVelocity * controller.collisions.slopeNormal.y);//*-1.0f;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = -maxJumpVelocity;
			}	
		}
    }

    public void OnJumpInputUp()
    {
		if (velocity.y > minJumpVelocity*-Mathf.Sign(gravity) && Mathf.Sign(gravity) < 0.0f)
        {
            velocity.y = minJumpVelocity;
		} else if (velocity.y < minJumpVelocity*-Mathf.Sign(gravity) && Mathf.Sign(gravity) > 0.0f)
		{
			velocity.y = -minJumpVelocity;
		}

    }

	public void Fire(){
		if (fireCurrentCooldown <= 0.0f) {
			audiosource.PlayOneShot (laser);
			aiming = true;
			Bullet myBullet = (Bullet)Instantiate (bulletPrefab, transform.position + new Vector3(bulletOffset.x*Mathf.Sign(controller.collisions.faceDir), bulletOffset.y*-(Mathf.Sign(gravity)), bulletOffset.z), Quaternion.identity); //as Gameobject;
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
		audiosource.PlayOneShot (tele);
		Instantiate (teleport, transform.position, Quaternion.identity);
		//Respawn!
		transform.position = respawnPoint;
		died = true;
		gravity = -(gravity * Mathf.Sign(gravity));
	}

	public void InvertGravity(){
		gravity = gravity * -1.0f;
	}

	public void SoundGravity(){
		audiosource.PlayOneShot (wave);
	}

	public void SetRespawn(Vector3 spawnPos) {
		respawnPoint = spawnPos;
	}
}
