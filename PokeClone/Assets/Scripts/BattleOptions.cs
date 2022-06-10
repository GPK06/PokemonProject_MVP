using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // to change scenes
using UnityEngine.UI; // to change the ui elements in unity

// battle options class that deals with all of the options that can be done during a battle
public class BattleOptions : MonoBehaviour
{
    // make not of previous route
    string previousRoute;

    // Make new pokemons
    PokemonStats mossamr;
    PokemonStats volthesis;
    PokemonStats wargo;

    // when the script is first run fill in all of the information to do with the pokemon
    public void Awake()
    {
        updateEnemyPokemon(); // called to upoate enemy pokemon info
        previousRoute = PokemonParty.getPrevRoute();
    }

    // method to update the enemy pokemon displayed
    public static void updateEnemyPokemon()
    {
        // get array of the enmy pokemon
        PokemonStats[] enemyPokemons = PokemonParty.getEnemyPokemon();

        Debug.Log("trainer battle " + PokemonParty.getTrainerBattle());
        Debug.Log("all Dead " + PokemonParty.getAllDead());

        if (PokemonParty.getTrainerBattle() && !PokemonParty.getAllDeadParty())
        {
            inBetween("The trainer has switched to " + enemyPokemons[0].getName());
        } else if (!PokemonParty.getAllDeadParty())
        {
            inBetween("You Encountered " + enemyPokemons[0].getName());
        }

        GameObject Canvas = GameObject.FindWithTag("Canvas");

        Image pokemonImage = Canvas.transform.Find("EnemyPokemon").GetComponent<Image>(); // gets the image name
        Text pokemonName = Canvas.transform.Find("EnemyName").GetComponent<Text>(); // gets the text
        Text totalHealth = Canvas.transform.Find("EnemyTotalHealth").GetComponent<Text>(); // gets the text
        Text currentHealth = Canvas.transform.Find("EnemyCurrentHealth").GetComponent<Text>(); // gets the text

        // current pokemon is at index 0;
        pokemonImage.sprite = Resources.Load<Sprite>(enemyPokemons[0].getName()); // changes image
        pokemonName.text = enemyPokemons[0].getName(); // changes text
        currentHealth.text = enemyPokemons[0].getHealth() + "";
        totalHealth.text = enemyPokemons[0].maxHealth() + "";
    }

    // method to send messages to the player when they are in the battle
    public static void inBetween(string text)
    {
        // get panel and send message
        GameObject panel = GameObject.Find("Canvas/BattleOptions/InBetween");
        panel.SetActive(true);
        Text message = GameObject.Find("Canvas/BattleOptions/InBetween/moveOnButton/Text").GetComponent<Text>();
        message.text = text;

        GameObject[] selectionButtons = GameObject.FindGameObjectsWithTag("Button");

        // turn off buttons so that it can be seen
        foreach (GameObject button in selectionButtons)
        {
            button.SetActive(false);
        }
    }

    // constructor to initialize all of the fields
    public BattleOptions()
    {
        PokemonStats pokemon = PokemonParty.getVolthesis();
        volthesis = pokemon;
        pokemon = PokemonParty.getWargo();
        wargo = pokemon;
        pokemon = PokemonParty.getMossamr();
        mossamr = pokemon;
    }

    // changes to the previous route if the player ran away
    public void runAway()
    {
        if (!PokemonParty.getTrainerBattle() && !PokemonParty.getAllDead())
        {
            // all trainers have more than 1 pokemon
            SceneManager.LoadScene(previousRoute);
        } else
        {
            inBetween("You cannot run from a trainer battle!");
        }
    }

    // so that I can leave when the battle is over
    public void leave()
    {
        SceneManager.LoadScene(previousRoute);
    }

    // goes into battle and depending on the number sent then that is the move done by the pokemon
    public void battle(int n)
    {
        // initializes all of the variables that will need to be used
        //string stat;

        PokemonStats[] enemyPokemons = PokemonParty.getEnemyPokemon();
        PokemonStats enemyPokemon = enemyPokemons[0];

        PokemonStats[] party = PokemonParty.getParty();
        PokemonStats currentPokemon = party[0];

        // as long as the pokemon is still alive
        if (currentPokemon.getHealth() > 1)
        {

            double enemyDamage;
            double currentDamage;
            double enemyFrac;
            double currentFrac;

            // makes the effectiveness fractions for volthesis and gets the damage done for both different type attacks
            currentFrac = currentPokemon.effectiveness(enemyPokemon.getPrimaryType());
            double enemyDamagePrimary = enemyPokemon.damageDone(currentPokemon, enemyPokemon.getPrimaryType(), currentFrac);
            currentFrac = currentPokemon.effectiveness(enemyPokemon.getSecondaryType());
            double enemyDamageSecondary = enemyPokemon.damageDone(volthesis, enemyPokemon.getSecondaryType(), currentFrac);

            // the better dame is done to the players pokemon
            if (enemyDamagePrimary > enemyDamageSecondary)
            {
                enemyDamage = enemyDamagePrimary;
            }
            else
            {
                enemyDamage = enemyDamageSecondary;
            }

            if (n == 1)
            {
                // if int n is a 1 then we are using the primary stat
                enemyFrac = enemyPokemon.effectiveness(currentPokemon.getPrimaryType());
                currentDamage = currentPokemon.damageDone(enemyPokemon, currentPokemon.getPrimaryType(), enemyFrac);
            }
            else
            {
                // if int n is a 2 then we are using the secondary stat
                enemyFrac = enemyPokemon.effectiveness(currentPokemon.getSecondaryType());
                currentDamage = currentPokemon.damageDone(enemyPokemon, currentPokemon.getSecondaryType(), enemyFrac);
            }

            // whichever pokemon is faster attacks first
            if (currentPokemon.getSpeed() > enemyPokemon.getSpeed())
            {
                inBetween(currentPokemon.getName() + " did " + currentDamage + " health points of damage! " + enemyPokemon.getName() + " did " + enemyDamage + " health points of damage!");

                enemyPokemon.takeDamageEnemy(currentDamage, currentPokemon);
                currentPokemon.takeDamage(enemyDamage, enemyPokemon);
            }
            else
            {
                inBetween(currentPokemon.getName() + " did " + currentDamage + " health points of damage! " + enemyPokemon.getName() + " did " + enemyDamage + " health points of damage!");

                currentPokemon.takeDamage(enemyDamage, enemyPokemon);
                enemyPokemon.takeDamageEnemy(currentDamage, currentPokemon);
            }
        } else
        {
            inBetween("Your pokemon died. Switch!"); // messasge if tried battling with dead pokemon
        }
    }

    // if the player wants to battle then change the butons
    public void moveSelection()
    {
        GameObject[] selectionButtons = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject button in selectionButtons)
        {
            button.SetActive(false);
        }

        // activate battling buttons
        GameObject battleButton = GameObject.Find("Canvas/BattleOptions/SecondaryButton");
        battleButton.SetActive(true);
        
        battleButton = GameObject.Find("Canvas/BattleOptions/PrimaryButton");
        battleButton.SetActive(true);
        
        battleButton = GameObject.Find("Canvas/BattleOptions/SwitchButton");
        battleButton.SetActive(true);
    }

    // method to catch pokemon with the name of the pokemon given as the parameter
    public void catchPokemon(Text name) 
    {
        if (!PokemonParty.getTrainerBattle())
        {
            // turn on next button
            GameObject nextButton = GameObject.Find("Canvas/BattleOptions/NextButton");
            nextButton.SetActive(true);

            Text endText = nextButton.transform.Find("Text").GetComponent<Text>();

            // if the party is too full then can't add a pokemon
            PokemonStats[] pokemonParty = PokemonParty.getParty();
            int totalPokemon = 0;
            
            foreach (PokemonStats pokemon in pokemonParty) // conuts how many pokemon is in the party
            {
                if (pokemon != null)
                {
                    totalPokemon++;
                }
            }
            
            // either party is too full or adds pokemon to the party
            if (totalPokemon >= 6)
            {
                endText.text = "Your party is too full.";
            } else
            {
                // adds the pokemon to the party
                string nameOfPokemon = name.text;
                if (nameOfPokemon.Equals("Wargo"))
                {
                    PokemonParty.add(wargo);
                }
                else
                {
                    PokemonParty.add(mossamr);
                }

                endText.text = "You caught, " + nameOfPokemon; 
            }

            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
            // deactivate all battle buttons
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }

        } else
        {
            inBetween("You cannot catch a trainers pokemon!"); /// message if in trainer battle
        }
    }

    // activates the panel to see the switch menu and then fill in all of the pokemon information
    public void showPokmeon(GameObject SwitchMenu)
    {
        SwitchMenu.SetActive(true);
        fillInValues(PokemonParty.getParty(), SwitchMenu);
    }

    // Very similiar method as the one in view party. and disables the button for pokemon that are not in your party
    void fillInValues(PokemonStats[] pokemonArray, GameObject SwitchMenu)
    {
        string imageName;
        string textName;
        string buttonName;
        //string buttonName;
        for (int i = 0; i < 6; i++)
        {
            imageName = "Image" + i;
            textName = "Text" + i;
            buttonName = "Button" + i;
            Image pokemonImage = SwitchMenu.transform.Find(imageName).GetComponent<Image>(); // gets the image name
            Text pokemonName = SwitchMenu.transform.Find(textName).GetComponent<Text>(); // gets the text
            GameObject button = GameObject.Find("SwitchMenu/Panel/" + buttonName);

            // checks if the pokemon is not null and now dead, then allows the button to be pressed.
            if (pokemonArray[i] != null)
            {
                PokemonStats pokemon = pokemonArray[i];
                pokemonImage.sprite = Resources.Load<Sprite>(pokemonArray[i].getName()); // changes image
                pokemonName.text = pokemon.getName() + " " + pokemon.getHealth() + "/" + pokemon.maxHealth(); // changes text
                
                // deactivate button if the pokemon is dead
                if (pokemonArray[i].getHealth() < 1)
                {
                    Debug.Log("button deactivated");
                    button.SetActive(false);
                }
            } else 
            {
                button.SetActive(false);
            }
        }
    }

    // switches the pokemon in the array so that the pokemon you selected is first.
    public void switchPokemon(int n)
    {
        //Debug.Log("I am being run");
        PokemonStats[] party = PokemonParty.getParty();
        PokemonStats volthesis = party[0]; // because the first slot will always be volthesis
        party[0] = party[n]; // party of n is the pokemon wanted
        party[n] = volthesis;

        // change the visual pokemon
        GameObject Canvas = GameObject.FindWithTag("Canvas");

        Image pokemonImage = Canvas.transform.Find("CurrentPokemon").GetComponent<Image>(); // gets the image name
        Text pokemonName = Canvas.transform.Find("CurrentName").GetComponent<Text>(); // gets the text
        Text totalHealth = Canvas.transform.Find("TotalHealth").GetComponent<Text>(); // gets the text
        Text currentHealth = Canvas.transform.Find("CurrentHealth").GetComponent<Text>(); // gets the text
        
        Image primaryImage = GameObject.Find("Canvas/BattleOptions/PrimaryButton").GetComponent<Image>(); // gets button image
        Image secondaryImage = GameObject.Find("Canvas/BattleOptions/SecondaryButton").GetComponent<Image>(); // gets button image

        // updates all of the values
        pokemonImage.sprite = Resources.Load<Sprite>(party[0].getName() + "BackSprite"); // changes image
        pokemonName.text = party[0].getName(); // changes text
        currentHealth.text = party[0].getHealth() + "";
        totalHealth.text = party[0].maxHealth() + "";

        primaryImage.sprite = Resources.Load<Sprite>(party[0].getPrimaryType() + "Button");
        secondaryImage.sprite = Resources.Load<Sprite>(party[0].getSecondaryType() + "Button");

        GameObject Panel = GameObject.FindWithTag("Panel");
        Panel.SetActive(false);
    }

    // to close the switch menu if you cannot switch pokemon
    public static void close(GameObject panel)
    {
        panel.SetActive(false);

        // activate battling buttons
        GameObject battleButton = GameObject.Find("Canvas/BattleOptions/RunButton");
        battleButton.SetActive(true);

        battleButton = GameObject.Find("Canvas/BattleOptions/FightButton");
        battleButton.SetActive(true);

        battleButton = GameObject.Find("Canvas/BattleOptions/CatchButton");
        battleButton.SetActive(true);
    }
}
