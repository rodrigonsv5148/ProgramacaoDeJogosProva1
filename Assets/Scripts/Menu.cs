using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string nomeCenaPlay;
    public string nomeCenaInstrucoes;
    public string nomeCenaMenu;
    public bool instrucoes;

    void OnPlay()
    {        
        if(instrucoes == false)
        {
            SceneManager.LoadScene(nomeCenaPlay);
        }else
        {
            SceneManager.LoadScene(nomeCenaMenu);
        }

    }

    void OnInstructions()
    {
        if(instrucoes == false)
        {
            SceneManager.LoadScene(nomeCenaInstrucoes);
            instrucoes = true;
        }else
        {
            SceneManager.LoadScene(nomeCenaMenu);
            instrucoes = false;
        }
        
    }

    void OnQuit()
    {
        if(instrucoes == false)
        {
            Application.Quit();
        }
        
    }
}
