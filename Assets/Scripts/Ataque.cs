using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    [SerializeField] private float vidaInimigo = 2f;
    [SerializeField] private float vidaBoss = 9f;

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
                vidaInimigo = 2f;
            }
        }   

        if (other.gameObject.CompareTag("boss"))
        {
            vidaBoss --;
            if (vidaBoss == 0)
            {
                other.gameObject.SetActive(false);
                vidaBoss = 9f;
            }
        }     
    }
}
