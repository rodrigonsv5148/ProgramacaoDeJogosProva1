using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ataque : MonoBehaviour
{
    [SerializeField] private int vidaInimigo = 2;
    [SerializeField] private int vidaBoss = 5;
    [SerializeField]private string proxCena = "MenuPrincipal";

    public string nomeDaTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("enemy"))
        {   
            vidaInimigo --;
            if (vidaInimigo == 0)
            {
                other.gameObject.SetActive(false);
                vidaInimigo = 2;
            }
        }   

        if (other.gameObject.CompareTag("boss"))
        {
            vidaBoss --;
            if (vidaBoss == 0)
            {
                other.gameObject.SetActive(false);
                vidaBoss = 5;
                SceneManager.LoadScene(proxCena);
            }
        }     
    }
}
