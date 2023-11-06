using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;
    public float velBoss = 1.5f;
    public float vidaBoss = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            
            Vector3 direcaoDoJogador = player.position - transform.position;

            Debug.DrawRay(transform.position, direcaoDoJogador, Color.red);

            Vector3 normalDirecao = direcaoDoJogador.normalized;

            Vector3 novaPosicao = transform.position + normalDirecao * velBoss * Time.deltaTime;

            transform.position = novaPosicao;
        }

        if (vidaBoss <= 0)
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             
        }
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }
}
