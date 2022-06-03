using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool viewingControls = false;

    // GameObject so that I have access to the pause menu
    public GameObject controlUI;
    public GameObject startMenu;

    // Resumes the game, so disables the panel
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

    void showControls()
    {
        controlUI.SetActive(false);
        viewingControls = false;
    }

    // paused the game, so activates the panel and shows controls
    void stopControls()
    {
        controlUI.SetActive(true);
        viewingControls = true;
    }

    public void play()
    {
        SceneManager.LoadScene("Route 1");
    }

}
