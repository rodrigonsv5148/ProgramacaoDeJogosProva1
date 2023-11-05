using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : MonoBehaviour
{

    public Transform player;
    public float velInimigo = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
         Vector3 direcaoDoJogador = player.position - transform.position;

         Vector3 normalDirecao = direcaoDoJogador.normalized;

         Vector3 novaPosicao = transform.position + normalDirecao * velInimigo * Time.deltaTime;

         transform.position = novaPosicao;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
