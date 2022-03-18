using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public CharacterController ch;
    public Animator animator;
    public float moveSpeed = 3;
    public float rotateSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        ch = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(vertical != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.Mouse1)) animator.SetBool("Atack", true);
        else animator.SetBool("Atack", false);
        ch.SimpleMove(moveSpeed * vertical* transform.forward );
        transform.Rotate(0, rotateSpeed*horizontal * Time.deltaTime, 0);
    }
}
