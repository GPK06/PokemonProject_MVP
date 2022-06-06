using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        Debug.Log(movement.y);

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

    // if the player triggered, a trigger, then a roll is made to see if to encounter, if it is the route collision then go to the route 
    private void OnTriggerStay2D(Collider2D collision)
    {
        // assigns this scene info
        PokemonParty.assignPreviousRoute();

        // goes to route 2
        if (collision.name.Equals("Route2EncounterBox"))
        {
            SceneManager.LoadScene("Route 2");
        }

        // if it rolls a 1 in a 100, then you encounter, while you are in the grass
        int num = Random.Range(0, 101);
        
        if (num == 1)
        {
            // different encounter boxes for each pokemon (like some routes in pokemon)
            if (collision.name.Equals("EncounterBoxWargo"))
            {
                // make info about the enemy pokemon and send to battle method
                HashSet<string> weaknessWargo = new HashSet<string>();
                HashSet<string> resistanceWargo = new HashSet<string>();

                resistanceWargo.Add("fire 2");
                resistanceWargo.Add("water 2");
                resistanceWargo.Add("steel");

                weaknessWargo.Add("fairy");
                weaknessWargo.Add("dragon");

                PokemonStats wargo = new PokemonStats("Wargo", "water", "dragon", 87, 87, 87, 87, 87, 87, weaknessWargo, resistanceWargo, null);

                PokemonParty.assignPokemonInformation(wargo);
                SceneManager.LoadScene("BATTLE"); // cannot call at the bottom other wise the heal box triggers an encounter
            }
            else if (collision.name.Equals("EncounterBox")) // if not wargo then has to be mossamr
            {                
                // sends info about enemy pokemon to battle nethod.
                HashSet<string> weaknessMossamr = new HashSet<string>();
                HashSet<string> resistanceMossamr = new HashSet<string>();
                HashSet<string> immunityMossamr = new HashSet<string>();

                immunityMossamr.Add("poison");

                resistanceMossamr.Add("normal");
                resistanceMossamr.Add("grass 2"); // 4x weakness
                resistanceMossamr.Add("water");
                resistanceMossamr.Add("electric");
                resistanceMossamr.Add("psychic");
                resistanceMossamr.Add("rock");
                resistanceMossamr.Add("dragon");
                resistanceMossamr.Add("steel");
                resistanceMossamr.Add("fairy");

                weaknessMossamr.Add("fire 2"); //4x weakness
                weaknessMossamr.Add("fighting");

                PokemonStats mossamr = new PokemonStats("Mossamr", "grass", "steel", 87, 87, 87, 87, 87, 87, weaknessMossamr, resistanceMossamr, immunityMossamr);

                PokemonParty.assignPokemonInformation(mossamr);
                SceneManager.LoadScene("BATTLE");
            } else if (collision.name.Equals("heal"))
            {
                PokemonParty.partyRestore();
            }
        }

    }
}
