using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScWinTrigger : MonoBehaviour
{
    [SerializeField] private ScHud win;

    private void OnTriggerStay(Collider other)
    {
        if (win.timer <= 0)
        {
            SceneManager.LoadScene("VictoryScreen");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
