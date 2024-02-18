using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

   public void jogar()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void creditos()
    {
        SceneManager.LoadScene("Dungeon");
    }
 
    public void sair()
    {
        Debug.Log("Saindo");
        Application.Quit();
    }
}
