using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 5f;
    public int dano = 10;
    public Vector2 direcao = Vector2.right; // Direção padrão (direita)
    public string alvo;

    void Update()
    {
        // Movimento contínuo do projétil
        transform.Translate(direcao * velocidade * Time.deltaTime);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(alvo))
        {
                        Debug.Log("acertou2"); 

            Destroy(this.gameObject, 0.4f);    
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(alvo))
        {
            Debug.Log("acertou2"); 
            Destroy(this.gameObject, 0.4f);    
        }   
    }
}
