using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleOptions : MonoBehaviour
{
    // Make new pokemons
    PokemonStats mossamr;
    PokemonStats volthesis;
    PokemonStats wargo;

    // make the sets for the constructor for all of the pokemon
    HashSet<string> weaknessMossamr = new HashSet<string>();
    HashSet<string> resistanceMossamr = new HashSet<string>();
    HashSet<string> immunityMossamr = new HashSet<string>();

    HashSet<string> weaknessVolthesis = new HashSet<string>();
    HashSet<string> resistanceVolthesis = new HashSet<string>();
    HashSet<string> immunityVolthesis = new HashSet<string>();

    HashSet<string> weaknessWargo = new HashSet<string>();
    HashSet<string> resistanceWargo = new HashSet<string>();

    public void Awake()
    {
        Debug.Log("In Awake Method");

        PokemonStats enemyPokemon = PokemonParty.getEnemyPokemon();

        GameObject Canvas = GameObject.FindWithTag("Canvas");

        Image pokemonImage = Canvas.transform.Find("EnemyPokemon").GetComponent<Image>(); // gets the image name
        Text pokemonName = Canvas.transform.Find("EnemyName").GetComponent<Text>(); // gets the text
        Text totalHealth = Canvas.transform.Find("EnemyTotalHealth").GetComponent<Text>(); // gets the text
        Text currentHealth = Canvas.transform.Find("EnemyCurrentHealth").GetComponent<Text>(); // gets the text

        pokemonImage.sprite = Resources.Load<Sprite>(enemyPokemon.getName()); // changes image
        pokemonName.text = enemyPokemon.getName(); // changes text
        currentHealth.text = enemyPokemon.getHealth() + "";
        totalHealth.text = enemyPokemon.getHealth() + "";
    }

    // copnctructor to inuitalize all of the fields
    public BattleOptions()
    {
        // methods to add all of the information to the sets without clogging the constrcutor
        addWeaknessMossamr();
        addWeaknessVolthesis();
        addWeaknessWargo();

        addResistanceWargo();
        addResistanceVolthesis();
        addResistanceMossamr();

        addImmunityVolthesis();
        addImmunityMossamr();

        // make the pokemon
        //public PokemonStats(string name, string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP)
        mossamr = new PokemonStats("Mossamr", "grass", "steel", 87, 87, 87, 87, 87, 87, weaknessMossamr, resistanceMossamr, immunityMossamr);
        volthesis = new PokemonStats("Volthesis", "fire", "fairy", 87, 87, 87, 87, 87, 87, weaknessVolthesis, resistanceVolthesis, immunityVolthesis);
        wargo = new PokemonStats("Wargo", "water", "dragon", 87, 87, 87, 87, 87, 87, weaknessWargo, resistanceWargo, null);
    }

    // changes to route 1 if the player ran away
    public void runAway()
    {
        Debug.Log("button was pressed");
        SceneManager.LoadScene("Route 1");
    }

    // catches the pokemon by adding it to the pokemon party array
    public void catchPokemon()
    {
        // gets the name to see which pokemon is being caught
        Text name = transform.Find("EnemyName").GetComponent<Text>();
        if (name.text == "Mossamr")
        {
            PokemonParty.add(mossamr);
        } else
        {
            PokemonParty.add(wargo);
        }
    }

    // goes into battle and depending on the number sent then that is the move done by the pokemon
    public void battle(int n)
    {
        // initializes all of the variables that will need to be used
        //string stat;

        PokemonStats enemyPokemon = PokemonParty.getEnemyPokemon();

        PokemonStats[] party = PokemonParty.getParty();
        PokemonStats currentPokemon = party[0];

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
            enemyPokemon.takeDamageEnemy(currentDamage, currentPokemon);
            currentPokemon.takeDamage(enemyDamage, enemyPokemon);
        }
        else
        {
            currentPokemon.takeDamage(enemyDamage, enemyPokemon);
            enemyPokemon.takeDamageEnemy(currentDamage, currentPokemon);
        }
    }

    // if the player wants to battle then goes to the move selection scene
    public void moveSelection()
    {
        GameObject[] selectionButtons = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject button in selectionButtons)
        {
            button.SetActive(false);
        }

        GameObject battleButton = GameObject.Find("Canvas/BattleOptions/SecondaryButton");
        battleButton.SetActive(true);
        
        battleButton = GameObject.Find("Canvas/BattleOptions/PrimaryButton");
        battleButton.SetActive(true);
        
        battleButton = GameObject.Find("Canvas/BattleOptions/SwitchButton");
        battleButton.SetActive(true);
    }

    // method to add mossamrs weakness'
    public void addWeaknessMossamr()
    {
        weaknessMossamr.Add("fire 2"); //4x weakness
        weaknessMossamr.Add("fighting");
    }

    // method to add mossamrs resistances
    public void addResistanceMossamr()
    {
        resistanceMossamr.Add("normal");
        resistanceMossamr.Add("grass 2"); // 4x weakness
        resistanceMossamr.Add("water");
        resistanceMossamr.Add("electric");
        resistanceMossamr.Add("psychic");
        resistanceMossamr.Add("rock");
        resistanceMossamr.Add("dragon");
        resistanceMossamr.Add("steel");
        resistanceMossamr.Add("fairy");
    }

    // method to add mossamrs immunities
    public void addImmunityMossamr()
    {
        immunityMossamr.Add("poison");
    }

    // method to add volthesis wekaness'
    public void addWeaknessVolthesis()
    {
        weaknessVolthesis.Add("water");
        weaknessVolthesis.Add("ground");
        weaknessVolthesis.Add("poison");
        weaknessVolthesis.Add("rock");
    }

    // method to add volthesis resistance's
    public void addResistanceVolthesis()
    {
        resistanceVolthesis.Add("fire");
        resistanceVolthesis.Add("grass");
        resistanceVolthesis.Add("ice");
        resistanceVolthesis.Add("fighting");
        resistanceVolthesis.Add("fairy");
        resistanceVolthesis.Add("bug 2"); // 4x resistance
        resistanceVolthesis.Add("dark");
    }

    // method to add voltheis immunities
    public void addImmunityVolthesis()
    {
        immunityVolthesis.Add("dragon");
    }

    // method to add wargos weakness'
    public void addWeaknessWargo()
    {
        weaknessWargo.Add("fairy"); 
        weaknessWargo.Add("dragon");
    }

    // method to add wargos resistance
    public void addResistanceWargo()
    {
        resistanceWargo.Add("fire 2");
        resistanceWargo.Add("water 2");
        resistanceWargo.Add("steel");
    }

    // method to catch pokemon with the name of the pokemon given as the parameter
    public void catchPokemon(Text name) 
    {
        PokemonStats[] pokemonParty = PokemonParty.getParty();
        if (pokemonParty.Length > 6)
        {
            Debug.Log("Party is too full");
        }

        string nameOfPokemon = name.text;
        if (nameOfPokemon.Equals("Wargo"))
        {
            PokemonParty.add(wargo);
        } else
        {
            PokemonParty.add(mossamr);
        }
        runAway();
    }

    public void showPokmeon(GameObject SwitchMenu)
    {
        SwitchMenu.SetActive(true);
        fillInValues(PokemonParty.getParty(), SwitchMenu);
    }


    void fillInValues(PokemonStats[] pokemonArray, GameObject SwitchMenu)
    {
        string imageName;
        string textName;
        //string buttonName;
        for (int i = 0; i < 6; i++)
        {
            imageName = "Image" + i;
            textName = "Text" + i;
            //buttonName = "Button" + i;
            Image pokemonImage = SwitchMenu.transform.Find(imageName).GetComponent<Image>(); // gets the image name
            Text pokemonName = SwitchMenu.transform.Find(textName).GetComponent<Text>(); // gets the text
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("SwitchButton");

            if (pokemonArray[i] != null)
            {
                Debug.Log(buttons.Length);
                PokemonStats pokemon = pokemonArray[i];
                pokemonImage.sprite = Resources.Load<Sprite>(pokemonArray[i].getName()); // changes image
                pokemonName.text = pokemon.getName(); // changes text
            } else
            {
                buttons[i].SetActive(false);
            }
        }
    }
}
