using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // imports this to change scenes

// menu class controls the features in the main menu scene such as viewing the controls and playing the game
// Menu class based off of: https://www.youtube.com/watch?v=JivuXdrIHK0
public class Menu : MonoBehaviour
{
    public static bool viewingControls = true;

    // GameObject so that I have access to the control screen
    public GameObject controlUI;

    // checks each from if the letter c is pressed, and then accordingly shows them or disbales it.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (viewingControls)
            {
                showControls();
            }
            else
            {
                stopControls();
            }
        }
    }
    
    // shows the control panel
    void showControls()
    {
        controlUI.SetActive(true);
        viewingControls = false;
    }

    // stops showing the controls
    void stopControls()
    {
        controlUI.SetActive(false);
        viewingControls = true;
    }

    public void play()
    {
        SceneManager.LoadScene("Route 1");
    }

}
