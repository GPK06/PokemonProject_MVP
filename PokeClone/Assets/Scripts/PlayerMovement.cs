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

    public void awake()
    {
        // when the player is again loaded in the scene, change the party order back to volthesis at the front 
        // because that is how it works in a real pokemon game
        PokemonStats[] party = PokemonParty.getParty();
        PokemonStats pokemon = party[0];

        for (int i = 0; i < 6; i++)
        {
            // there will only ever be 1 volthesis in a party
            if (party[i].getName().Equals("Volthesis"))
            {
                party[0] = party[i];
                party[i] = pokemon;
            }
        }
    }

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

    // if the player triggered, a trigger, then a roll is made to see if to encounter, if it is the route collision then go to the route 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Route2EncounterBox"))
        {
            SceneManager.LoadScene("Route 2");
        }

        int num = Random.Range(0, 101);
        
        if (num == 1)
        {
            // different encounter boxes for each pokemon (like some routes in pokemon)
            if (collision.name.Equals("EncounterBoxWargo"))
            {
                Debug.Log("Wargo");

                HashSet<string> weaknessWargo = new HashSet<string>();
                HashSet<string> resistanceWargo = new HashSet<string>();

                resistanceWargo.Add("fire 2");
                resistanceWargo.Add("water 2");
                resistanceWargo.Add("steel");

                weaknessWargo.Add("fairy");
                weaknessWargo.Add("dragon");

                PokemonStats wargo = new PokemonStats("Wargo", "water", "dragon", 87, 87, 87, 87, 87, 87, weaknessWargo, resistanceWargo, null);

                PokemonParty.assignPokemonInformation(wargo);
            }
            else
            {
                Debug.Log("Mossamr");
                
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
            }

            SceneManager.LoadScene("BATTLE");
        }

    }
}
