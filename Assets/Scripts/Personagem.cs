using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
    // Variaveis relacionadas ao status do personagem
    public float vidaPersonagem = 100f;
    [SerializeField]private float velocidadePersonagem = 5f;
    [SerializeField]private float forcaPulo = 600f;
    // Circulo da base do pe do personagem, serve para identificar colisor com o chao.
    [SerializeField]private float tamanhoCirculoBase = 2.5f;

    public bool machinima = false; 

    private Vector2 Input = new Vector2();
    private float horizontalInput;
    private Rigidbody2D rb;
    private bool permitirPulo = false;

    [SerializeField] private Transform basePersonagem;
    [SerializeField] private LayerMask chaoLayer;

    //Método de quando se inicia o jogo
    private void Awake ()
    {
        // Pega o componente rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (machinima == false)
        {
            //Linha que faz o personagem andar
            rb.velocity = new Vector2(horizontalInput * velocidadePersonagem, rb.velocity.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       permitirPulo = Physics2D.OverlapCircle(basePersonagem.position, tamanhoCirculoBase, chaoLayer);
    }
    
    void OnMove(InputValue value)
    {
        Input = value.Get<Vector2>();
        horizontalInput = Input.x;
    }

    //----------------Aqui é para pular, Onfire é só temporário, trocar--------------------------
    void OnFire()
    {
        if(permitirPulo == true)
        {
            rb.AddForce(Vector2.up * forcaPulo);
        }
        
    }
}
