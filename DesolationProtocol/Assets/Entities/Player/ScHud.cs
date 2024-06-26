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

    public int contador = 300;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("Contador",1);

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

    public void Contador()
    {
        contador--;
        Wave_text.text = "La ayuda llegara en " + contador + " segundos"; 
        
        if (contador <= 0)
        {
            //manda al pibe a una pantalla de victoria
        }
        else
        {
            Invoke("Contador",1);    
        }
    }
}
