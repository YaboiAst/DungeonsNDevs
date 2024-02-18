using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

   public void Jogar()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
 
    public void Sair()
    {
        Debug.Log("Saindo");
        Application.Quit();
    }

    public void VoltarAoMenu(){
        SceneManager.LoadScene("Menu");
    }
}
