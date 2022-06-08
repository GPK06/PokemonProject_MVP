using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // called so that I can change scenes and get scene information

// PlayerMovement class to control how the character moves and where the character will go (i.e. encounters, next route, etc.)
// Parts of the code was made by following this youtube video: https://www.youtube.com/watch?v=whzomFgjT50
public class PlayerMovement : MonoBehaviour
{
    // makes a float for the speed of the character
    public float moveSpeed = 5f;

    // makes a rigid body object so the players position can be changed
    public Rigidbody2D rbPlayer;
    public Rigidbody2D rbCamera;
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
        rbPlayer.MovePosition(rbPlayer.position + movement * moveSpeed * Time.fixedDeltaTime);
        rbCamera.MovePosition(rbCamera.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // if the player exits this trigger than the pokemon is healed.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("heal"))
        {
            Debug.Log("run in exit method");
            PokemonParty.partyRestore();
        }
    }

    // special method for trainer battles so that they can have more that 1 pokemon
    public void trainerBattle()
    {
        Time.timeScale = 1f; // for the trainer text the game pauses so that you cannot move anymore
        PokemonStats[] party = { PokemonParty.getWargo(), PokemonParty.getWargo(), PokemonParty.getWargo(), null, null, null };
        PokemonParty.assignPokemonInformation(party);
        SceneManager.LoadScene("BATTLE");
    }

    // if the player triggered, a trigger, then a roll is made to see if to encounter, if it is the route collision then go to the route 
    private void OnTriggerStay2D(Collider2D collision)
    {
        // assigns this scene info
        PokemonParty.assignPreviousRoute();

        PokemonStats[] party = new PokemonStats[6]; // will be needed for any battle

        // checks trigger boxes where there is immediate action done
        if (collision.name.Equals("Route2EncounterBox"))
        {
            SceneManager.LoadScene("Route 2");
        } if (collision.name.Equals("Route 1"))
        {
            SceneManager.LoadScene("Route 1");
        } if (collision.name.Equals("Route 3"))
        {
            SceneManager.LoadScene("Route 3");
        } if (collision.name.Equals("TrainerEncounter"))
        {
            GameObject trainerDialogue = GameObject.Find("Canvas/TrainerText/Panel");
            Time.timeScale = 0f; // for the trainer text the game pauses so that you cannot move anymore
            trainerDialogue.SetActive(true);
        }

        // if it rolls a 1 in a 100, then you encounter, while you are in the grass
        int num = Random.Range(0, 101);
        
        if (num == 1)
        {

            // different encounter boxes for each pokemon (like some routes in pokemon)
            if (collision.name.Equals("EncounterBoxWargo"))
            {
                party[0] = PokemonParty.getWargo();

                PokemonParty.assignPokemonInformation(party);
                SceneManager.LoadScene("BATTLE"); // cannot call at the bottom other wise the heal box triggers an encounter
            }
            else if (collision.name.Equals("EncounterBox")) // if not wargo then has to be mossamr
            {
                party[0] = PokemonParty.getMossamr();

                PokemonParty.assignPokemonInformation(party);
                SceneManager.LoadScene("BATTLE");
            } 
        }
    }
}
