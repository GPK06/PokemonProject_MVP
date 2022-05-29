using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewParty : MonoBehaviour
{
    // variable to see whether I am viewing the party or not
    public static bool viewingParty = false;

    // GameObject so that I have access to the pause menu
    public GameObject pauseMenuUI;

    // checking every frame (or another something similiar) to see if the player pressed the letter p
    // Then runs method accordingly
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (viewingParty)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    // Resumes the game, so disables the panel
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        viewingParty = false;
    }

    // paused the game, so acrtivates the panel and fills in all necesary information
    void Pause()
    { 
        fillInValues(PokemonParty.getParty());
        pauseMenuUI.SetActive(true);
        viewingParty = true;
    }

    // Traverses through the pokemon Party and if there is a value inside then the image is changed and the same with the name
    void fillInValues(PokemonStats[] pokemonArray)
    {
        string imageName;
        string textName;
        for (int i = 0; i < 6; i++)
        {
            imageName = "Image" + i;
            textName = "Text" + i;
            Image pokemonImage = pauseMenuUI.transform.Find(imageName).GetComponent<Image>(); // gets the image name
            Text pokemonName = pauseMenuUI.transform.Find(textName).GetComponent<Text>(); // gets the text

            if (pokemonArray[i] != null)
            {
                PokemonStats pokemon = pokemonArray[i];
                pokemonImage.sprite = Resources.Load<Sprite>(pokemonArray[i].getName()); // changes image
                pokemonName.text = pokemon.getName(); // changes text
            }
        }
    }
}
