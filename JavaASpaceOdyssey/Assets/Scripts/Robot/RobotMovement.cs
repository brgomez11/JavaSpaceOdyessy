using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour {

    public float speed = 1.5f;
    private Vector3 movement;
    private Animator robotAnim;
    private Rigidbody robotRigidbody;
    private int floorMask;
    private float camRayLength = 100f;
    private AudioSource audioS;
    public AudioClip walkAudio;
    //public AudioClip jumpAudio;

     void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        robotAnim = GetComponent<Animator>();
        robotRigidbody = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h,v);
        Turn();
        Walk(h, v);
        Jump();
        Attack();

    }

    public void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        robotRigidbody.MovePosition(transform.position + movement);
    }

    public void Turn()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            robotRigidbody.MoveRotation(newRotation);
        }

    }

    public void Walk(float h, float v)
    {
        bool walk = h != 0f || v != 0f;

        robotAnim.SetBool("walk", walk);
        audioS.clip = walkAudio;

        if (walk = h != 0f || v != 0f)
        {
           
            if( !audioS.isPlaying)
            {
                audioS.Play();
            }
        }

        else
        {
            audioS.Stop();
        }
    }

    public void Jump()
    {
        // audioS.clip = jumpAudio;
        if (Input.GetKeyDown(KeyCode.J))
        {
            robotAnim.SetTrigger("jump");

            /*    if (!audioS.isPlaying)
                {
                    audioS.Play();
                }

            }
            else
            {
                audioS.Stop();

            }*/
        }
    }
        public void Aim()
    {

        bool aim = Input.GetKey(KeyCode.N);
        robotAnim.SetBool("aim", aim);
    }

    public void Attack()
    {

        Aim();

        if (Input.GetKeyDown(KeyCode.F))
        {
            robotAnim.SetTrigger("attack");
        }
    }
}
