using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Tutoriais usados: 
Animacao:
        https://www.youtube.com/watch?v=9jgcEWnbHZA
*/

public class Personagem : MonoBehaviour
{
    // Variaveis relacionadas ao status do personagem
    public float vidaPersonagem = 100f;
    [SerializeField]private float velocidadePersonagem = 5f;
    [SerializeField]private float forcaPulo = 600f;

    // Circulo da base do pe do personagem, serve para identificar colisor com o chao.
    [SerializeField]private float tamanhoCirculoBase = 2.5f;

    public bool machinima = false; 
    private Animator animator;

    private Vector2 Input = new Vector2();
    private float horizontalInput;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool permitirPulo = false;

    [SerializeField] private Transform basePersonagem;
    [SerializeField] private LayerMask chaoLayer;
    
    //A unity Recomenda usar um hash como "variavel do animator" p melhorar desempenho
    private int correndoHash = Animator.StringToHash("Correndo");
    private int saltandoHash = Animator.StringToHash("Saltando");

    //Método de quando se inicia o jogo
    private void Awake ()
    {
        // Pega o componente rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Controle das animacoes
        animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
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

       // Codigo que controla as animacoes
       animator.SetBool(correndoHash, horizontalInput != 0);
       animator.SetBool(saltandoHash, !permitirPulo);

       if(horizontalInput > 0)
       {
        sr.flipX = false;
       }else if (horizontalInput < 0)
       {
        sr.flipX = true;
       }
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
