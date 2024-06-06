using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject showSettings;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        GameData.instance.ClearData();
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowOpt()
    {
        showSettings.SetActive(true);
    }
    public void HideOpt()
    {
        showSettings?.SetActive(false);
    }
}
