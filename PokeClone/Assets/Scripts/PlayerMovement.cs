using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // makes a float for the speed of the character
    public float moveSpeed = 5f;

    // makes a rigid body object so the players position can be changed
    public Rigidbody2D rb;
    public Animator animator; // animator to control the player animation

    // to see update the x and y positions
    Vector2 movement;

    // Update is called once per frame to see if the player input 'W' 'A' 'S' 'D' or the arrow keys
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
    }

    // updates the position at a fixed frame rate regardless of specs of the PC
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // if the player triggered, a trigger, then a roll is made to see if to encounter 
    private void OnTriggerStay2D(Collider2D collision)
    {
        int num = Random.Range(0, 101);
        
        if (num == 1)
        {   
            // different encounter boxes for each pokemon (like some routes in pokemon)
            if (collision.name.Equals("EncounterBoxWargo"))
            {
                SceneManager.LoadScene("BattleWargo");
            }
            else
            {
                SceneManager.LoadScene("BATTLE");
            }
        }

    }
}
