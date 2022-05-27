using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewParty : MonoBehaviour
{
    public static bool viewingParty = false;

    public GameObject pauseMenuUI;

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

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        viewingParty = false;
    }

    void Pause()
    { 
        fillInValues(PokemonParty.getParty());
        pauseMenuUI.SetActive(true);
        viewingParty = true;
    }

    void fillInValues(PokemonStats[] pokemonArray)
    {
        string imageName;
        string textName;
        for (int i = 0; i < 6; i++)
        {
            imageName = "Image" + i;
            textName = "Text" + i;
            Image pokemonImage = pauseMenuUI.transform.Find(imageName).GetComponent<Image>();
            Text pokemonName = pauseMenuUI.transform.Find(textName).GetComponent<Text>();

            if (pokemonArray[i] != null)
            {
                PokemonStats pokemon = pokemonArray[i];
                pokemonImage.sprite = Resources.Load<Sprite>(pokemonArray[i].getName());
                pokemonName.text = pokemon.getName();
            }
        }
    }
}
