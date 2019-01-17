using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    public Animator anim; //Reference to animation component

    public Transform target; //Target to get close to
    public float minDist; //Distance to maintain with target
    float moveSpeed = 3; //Speed at which to move towards target

    float idleTimer = 0.0f; //Track how long in initial idle state
    float attackTimer = 0.0f; //Track how long in attacking state
    public float idleWait; //Time waiting before seeking
    public float attackWait; //Time between the attacks
    float attackDuration = 2.0f; //Attack animation time limit
    float attackingTime = 0.0f; //Timer for attacking
    bool attacking = false; //Flag for attacking animation state
    bool firstTime = true; //Flag for first time seek
    bool firstTimeCloseEnough = true; //Flag for first time attack
    bool playAttackSound = true; //Flag for attack wind up sound playing

    private Vector3 targetPoint;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("PlayerCube").transform; //Set target to seek to

        anim = GetComponent<Animator>(); //Reference to animation component

        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttacking", false);
    }
	
	// Update is called once per frame
	void Update () {
        seek();
	}

    // Moves toward the given target location, maintains the minimum distance
    void seek()
    {
        if (idleTimer < idleWait)//Initial wait idle
        {
            idleAnim();
            idleTimer += Time.deltaTime;
        }
        else
        {
            //too far away, go towards target
            if (Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z)) >= minDist)
            {
                if (firstTime)
                {
                    //Walk towards target
                    stopAnims();
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z)); //only moves in x and z
                    firstTime = false;
                }
                walkAnim();

                transform.position += transform.forward * moveSpeed * Time.deltaTime; //Move forward
            }
            else //close enough to attack
            {
                if (firstTimeCloseEnough)
                {
                    stopAnims();
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                    firstTimeCloseEnough = false;
                }
                if (attackTimer > attackWait) //it has been x seconds since last attack
                {
                    attacking = true;
                    //stopAnims();
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }
                else //too soon to attack again
                {
                    attacking = false;
                }

                if (attacking) //currently attacking
                {
                    if (playAttackSound)
                    {
                        playAttackSound = false;
                        if (!GetComponent<AudioSource>().isPlaying)
                        {
                            GetComponent<AudioSource>().Play();
                        }
                    }

                    attackAnim();

                    if(attackingTime < attackDuration) //Still attacking
                    {
                        attackingTime += Time.deltaTime;
                    } else //stop attacking reset variables
                    {
                        attackTimer = 0.0f;
                        attackingTime = 0.0f;
                        playAttackSound = true;
                    }
                }
                else //waiting to attack
                {
                    attackTimer += Time.deltaTime;
                    idleAnim();
                }
            }
        }
    }

    //Stop attack animations, called when blocked
    public void stopAttack()
    {
        attackTimer = 0.0f;
        attackingTime = 0.0f;
        playAttackSound = true;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    //Animation functions
    void stopAnims()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttacking", false);
    }

    void idleAnim()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttacking", false);
    }

    void walkAnim()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttacking", false);
    }

    void attackAnim()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttacking", true);
    }
}
