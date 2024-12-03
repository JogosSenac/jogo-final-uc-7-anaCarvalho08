using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuConfig : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void Créditos()
    {
        SceneManager.LoadScene("Créditos");
    }

    public void TelaMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Você saiu");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
