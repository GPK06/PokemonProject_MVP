using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// viewParty class is the class that controls all of the functions that deals with viewing controls and party
// viewParty class based off of: https://www.youtube.com/watch?v=JivuXdrIHK0
public class viewParty : MonoBehaviour
{
    // variable to see whether I am viewing the party/controls or not
    public static bool viewingParty = false;
    public static bool viewingControls = false;

    // GameObject so that I have access to the controlMenu menu and the controls ui
    public GameObject party;
    public GameObject controlUI;

    // checking every frame to see if the player pressed the letter p or c to view the party/controls or to stop viewing them.
    // Then runs method accordingly
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !viewingControls)
        {
            if (viewingParty)
            {
                Resume();
            } else
            {
                Pause();
            }
        } else if (Input.GetKeyDown(KeyCode.C) && !viewingParty) {
            if (viewingControls)
            {
                resumeControls();
            } else
            {
                pauseControls();
            }
        }
    }

    // Resumes the game, so disables the panel
    void Resume()
    {
        party.SetActive(false);
        Time.timeScale = 1f;
        viewingParty = false;
    }

    // paused the game, so acrtivates the panel and fills in all necesary information
    void Pause()
    { 
        fillInValues(PokemonParty.getParty());
        party.SetActive(true);
        Time.timeScale = 0f;
        viewingParty = true;
    }

    // Resumes the game, so disables the panel
    void resumeControls()
    {
        controlUI.SetActive(false);
        viewingControls = false;
    }

    // paused the game, so activates the panel and shows controls
    void pauseControls()
    {
        controlUI.SetActive(true);
        viewingControls = true;
    }

    // Traverses through the pokemon Party and if there is a value inside then the image is changed and the same with the name
    void fillInValues(PokemonStats[] pokemonArray)
    {
        // variable names
        string imageName;
        string textName;
        // for=loop to go through the pokemon array
        for (int i = 0; i < 6; i++)
        {
            // assign names for the images/text that needs to be found
            imageName = "Image" + i;
            textName = "Text" + i;
            Image pokemonImage = party.transform.Find(imageName).GetComponent<Image>(); // gets the image name
            Text pokemonName = party.transform.Find(textName).GetComponent<Text>(); // gets the text

            // as long as there is a pokemon in that slot then fill in the information
            if (pokemonArray[i] != null)
            {
                PokemonStats pokemon = pokemonArray[i];
                pokemonImage.sprite = Resources.Load<Sprite>(pokemonArray[i].getName()); // changes image
                pokemonName.text = pokemon.getName(); // changes text
            }
        }
    }
}
