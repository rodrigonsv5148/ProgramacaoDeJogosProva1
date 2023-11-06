using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : MonoBehaviour
{

    public Transform player;
    public float velInimigo = 5.0f;
    public float vidaInimigo = 3f;
    public bool visao = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && visao) 
        {
            
            Vector3 direcaoDoJogador = player.position - transform.position;

            Debug.DrawRay(transform.position, direcaoDoJogador, Color.red);

            Vector3 normalDirecao = direcaoDoJogador.normalized;

            Vector3 novaPosicao = transform.position + normalDirecao * velInimigo * Time.deltaTime;

            transform.position = novaPosicao;
        }

        if (vidaInimigo <= 0)
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
             visao = true;
        }
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             visao = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             visao = false;
        }
    }
}
