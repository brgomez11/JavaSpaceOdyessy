using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class Actions : MonoBehaviour {

	private Animator animator;
    public float movementSpeed = 10.0f;
    public float angleChangeSpeed = 25.0f;

    const int countOfDamageAnimations = 3;
	int lastDamageAnimation = -1;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	public void Stay () {
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 0f);
		}

	public void Walk () {
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 0.5f);
        bool Walking = Input.GetKey(KeyCode.W);
        animator.SetBool("Walking", Walking);

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetFloat("Speed", 0.0f);
        }

    }

	public void Run () {
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 1f);
        //bool speed = Input.GetKey(KeyCode.R);
        bool Running = Input.GetKey(KeyCode.R);
        animator.SetBool("Running", Running);

        if(Input.GetKeyUp(KeyCode.R))
        {
            animator.SetFloat("Speed", 0.3f);
        }
	}

	public void Attack () {
		Aiming ();
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Attack");
        }
	}

	public void Death () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
			animator.Play("Idle", 0);
		else
			animator.SetTrigger ("Death");
	}

	public void Damage () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) return;
		int id = Random.Range(0, countOfDamageAnimations);
		if (countOfDamageAnimations > 1)
			while (id == lastDamageAnimation)
				id = Random.Range(0, countOfDamageAnimations);
		lastDamageAnimation = id;
		animator.SetInteger ("DamageID", id);
		animator.SetTrigger ("Damage");
	}

	public void Jump () {
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Aiming", false);
      if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
     
    }

	public void Aiming () {
		animator.SetBool ("Squat", false);
        animator.SetFloat("Speed", 0f);





    }

	public void Sitting () {
		//animator.SetBool ("Squat", !animator.GetBool("Squat"));
		animator.SetBool("Aiming", false);
        bool Squat = Input.GetKey(KeyCode.S);
        animator.SetBool("Squat", Squat);

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Squat", false);
        }
    }

    public void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 finalDirection = new Vector3(horizontal, 0, 1.0f);


        transform.position += direction * movementSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * angleChangeSpeed);
    }
}
