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
    // -----------------------------------------Variaveis-----------------------------------------
    
    // Variaveis relacionadas ao status do personagem
    public float vidaPersonagem = 100f;
    [SerializeField]private float velocidadePersonagem = 5f;
    [SerializeField]private float forcaPulo = 600f;
    // Velocidade vertical a qual considero que o personagem esta caindo e nao so pulando
    [SerializeField]private float queda = 5f;

    // Variaveis dos componentes do personagem
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Variavel para saber se o player pode jogar ou e machinima
    public bool machinima = false;

    // Variaveis auxiliares para realizar a movimentacao horizontal do personagem
    private Vector2 Input = new Vector2();
    private float horizontalInput;    

    // Variaveis auxiliares para limitar pulo do personagem
    private bool permitirPulo = false;
    [SerializeField] private Transform basePersonagem;
    [SerializeField] private LayerMask chaoLayer;
        // Circulo da base do pe do personagem, serve para identificar colisor com o chao.
        [SerializeField]private float tamanhoCirculoBase = 2.5f;

    // Pegar o box collider e sempre deixar de frente para o player
    [SerializeField]private Transform frente;
    [SerializeField]private Transform atras;
    [SerializeField]private Transform colisor;
    [SerializeField]private float offset = 1f;
    private Vector3 offsetCodigo;

    // A unity Recomenda usar um hash como "variavel do animator" p melhorar desempenho
    private int correndoHash = Animator.StringToHash("Correndo");
    private int saltandoHash = Animator.StringToHash("Saltando");
    private int caindoHash = Animator.StringToHash("Caindo");

    // -----------------------------------------Metodos-----------------------------------------
    // Metodo de quando se inicia o jogo
    private void Awake ()
    {
        // Pega os componentes
        // RigidBody2D
        rb = GetComponent<Rigidbody2D>();
        // Animacoes
        animator = GetComponent<Animator>();
        // Sprite Render
        sr = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        if (machinima == false)
        {
            //Linha que faz o personagem andar
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       permitirPulo = Physics2D.OverlapCircle(basePersonagem.position, tamanhoCirculoBase, chaoLayer);

       // Codigo que controla as animacoes
       // Correndo
       animator.SetBool(correndoHash, horizontalInput != 0);
       // Saltando
       animator.SetBool(saltandoHash, !permitirPulo);
       // Caindo
       animator.SetBool(caindoHash, rb.velocity.y < -queda && !permitirPulo);
       // Atacando
       //animator.SetBool(atacandoHash);
       

       // Controla o lado das animacoes, se estao para frente o para tras;
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

        rb.velocity = new Vector2(horizontalInput * velocidadePersonagem, rb.velocity.y);

        
    }

    //----------------Aqui é para pular, Onfire é só temporário, trocar--------------------------
    void OnFire()
    {
        if(permitirPulo == true)
        {
            rb.AddForce(Vector2.up * forcaPulo);
        }
        
   
    }

    void OnAttack()
    {
        

        //Flipa o colisor do ataque o mantendo sempre a frente do personagem
        offsetCodigo = new Vector3(offset, 0.0f, 0.0f);

        if(sr.flipX == false)
        {
            colisor.position = frente.position + offsetCodigo;
        }else
        {
            colisor.position = atras.position - offsetCodigo;
        }
    }
}
