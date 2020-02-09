using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
            Debug.Log("Cambio de escena");
            SceneManager.LoadScene("Scene1");
     
    }
}
