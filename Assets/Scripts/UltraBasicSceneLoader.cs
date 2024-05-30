using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UltraBasicSceneLoader : MonoBehaviour
{
    int scenetoload;

    public void Teleporters(int teleportToLocation)
    {
        SceneManager.LoadScene(teleportToLocation);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit(3);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

}
