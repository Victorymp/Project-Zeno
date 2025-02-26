using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public void pvpButtonHandler()
    {
        SceneManager.LoadSceneAsync("Terrain");
    }

    public void pvcButtonHandler()
    {
        SceneManager.LoadSceneAsync("Terrain");
    }

    public void settingsButtonHandler()
    {
        SceneManager.LoadSceneAsync("Settings");
    }

    public void quitButtonHandler()
    {
        Application.Quit();
    }

}
