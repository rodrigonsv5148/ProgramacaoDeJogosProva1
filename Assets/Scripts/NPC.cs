using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC : MonoBehaviour
{

    public Text fala1; 

    // Start is called before the first frame update
    void Start()
    {
        fala1.gameObject.SetActive(false);
        fala1.text = "Perdoe-nos por nossa hostilidade extrangeiro, somos escravos de um tirano... O quê? Você vai nos ajudar? Então pegue a relíquia adiante e boa sorte!";
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            fala1.gameObject.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            fala1.gameObject.SetActive(false);
        }
    }
}
