using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class ScHud : MonoBehaviour
{
    [SerializeField] GameObject MenuPausa;
    [SerializeField] ScEntity _entity;
    [SerializeField] private Slider HpBar;
    [SerializeField] private Text Wave_text;
    public int timer = 300;

    [SerializeField] private GameObject silencedHud;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("Countdown",1);

    }

    public void TogglePause()
    {
        if (MenuPausa.activeSelf)
        {
            MenuPausa.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            MenuPausa.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level_MilitaryBase");
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void CountHP()
    {
        HpBar.value = _entity.health / _entity.Stats.maxHealth;
    }

    public void Countdown()
    {
        timer--;
        if (timer <= 0)
        {
            Wave_text.text = "The help has arrived!\nGo outside to escape";
        }
        else
        {
            Wave_text.text = "Help is comming!\nETA " + timer + " seconds";
            Invoke("Countdown",1);    
        }
    }

    public void Silencers(bool isSilenced)
    {   
        if (isSilenced)
        {
            silencedHud.SetActive(true);
        }
        else
        {
            silencedHud.SetActive(false);
        }
    }

}
