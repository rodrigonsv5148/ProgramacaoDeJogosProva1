using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 5f;
    public int dano = 10;
    public Vector2 direcao = Vector2.right; // Direção padrão (direita)

    void Update()
    {
        // Movimento contínuo do projétil
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta colisões com objetos
        if (other.CompareTag("Inimigo"))
        {

            Destroy(gameObject); // Destroi o projétil após a colisão
        }
    }
}
