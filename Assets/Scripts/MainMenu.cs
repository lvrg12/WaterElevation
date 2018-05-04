using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;   //library that gives Unity access to multiple scenes
using System.IO;

public class MainMenu : MonoBehaviour
{
    private string city;

    public void writeCity(string name)
    {
        string path = "Assets/Resources/currentCity.txt";
        StreamWriter writer = new StreamWriter(path,false);
        writer.WriteLine(name);
        writer.Close();

        AssetDatabase.ImportAsset(path); 
        Resources.Load("currentCity");
    }

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
