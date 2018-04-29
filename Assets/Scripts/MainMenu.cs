using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   //library that gives Unity access to multiple scenes

public class MainMenu : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //allows the start menu to access the game scene

    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);  //allows the start menu to access the game scene

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
