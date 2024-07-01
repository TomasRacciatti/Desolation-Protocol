using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_MilitaryBase");
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
    public void Prototype()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

}
