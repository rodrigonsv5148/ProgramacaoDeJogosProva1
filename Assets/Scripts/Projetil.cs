using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projetil : MonoBehaviour
{
    public float velocidade = 5f;
    public int dano = 10;
    public Vector2 direcao = Vector2.right; // Direção padrão (direita)
    public string alvo;
    [SerializeField] private int vidaBoss = 5;
    [SerializeField]private string proxCena = "MenuPrincipal";

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
