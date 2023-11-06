using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*Tutoriais usados: 
Animacao:
        UNITY TUTORIAL 2D | Como Movimentar e Animar um Personagem 2D - https://www.youtube.com/watch?v=9jgcEWnbHZA
        Chamar Método Repetidas Vezes A Cada Intervalo De Tempo - Invoke Repeating - Dicas de C# e Unity !! - https://www.youtube.com/watch?v=6JNYHttVdMA
        Tutorial Unity | Plataforma 2D - https://www.youtube.com/watch?v=XcdRuE8KlKg&list=PLgTmU6kuSLtz1GCoybobrln0nZilckt-Z&index=6 
        Como fazer ANIMAÇÃO na UNITY? - UNITY TUTORIAL - https://www.youtube.com/watch?v=VsFlnlHRau8

*/

public class Personagem : MonoBehaviour
{
    // -----------------------------------------Variaveis-----------------------------------------
    
    // Variaveis relacionadas ao status do personagem
    public float vidaPersonagem = 100f;
    public int personagem = 2;
    [SerializeField]private float velocidadePersonagem = 5f;
    [SerializeField]private float forcaPulo = 600f;
    [SerializeField]private float forcaDash = 600f;
    [SerializeField]private float cargaDash = 3f;
    [SerializeField]private float tempoCargaDash = 3f;
    [SerializeField]private float limiteCargaDash = 3f;
    [SerializeField]private float decrescimoVida = 1f;
    // Velocidade vertical a qual considero que o personagem esta caindo e nao so pulando
    [SerializeField]private float quedaVelocidade = 5f; 
    
    // Variaveis dos componentes do personagem
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Variaveis auxiliares 
    // Diz se o player esta em momento de gameplay ou machinima
    [SerializeField] private bool machinima = false;
    [SerializeField] private float danoDeQueda = 5f;
    [SerializeField] private string nomeDaTag;
    [SerializeField] private string nomeDaTagDeAtaque;
    public GameObject novoProjetil;
    public GameObject novoProjetil1;

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

    // Variaveis auxiliares para a animacao do ataque
    [SerializeField]private float intervaloAtaque;
    private float proximoAtaque;

    // Variaveis auxiliares para a animacao de hit
    [SerializeField]private float tempoDeHit = 0.43f;

    // Variaveis auxiliares para a animacao de Shield
    [SerializeField]private float tempoDeShield = 0.43f;

    // Variaveis auxiliares para a animacao de Special
    [SerializeField]private float tempoDeSpecial = 0.417f; 

    // A unity Recomenda usar um hash como "variavel do animator" p melhorar desempenho
    private int correndoHash = Animator.StringToHash("Correndo");
    private int saltandoHash = Animator.StringToHash("Saltando");
    private int caindoHash = Animator.StringToHash("Caindo");
    private int atacandoHash = Animator.StringToHash("Atacando");
    private int dashingHash = Animator.StringToHash("Dashing");
    private int deadingHash = Animator.StringToHash("Deading");
    private int hittedHash = Animator.StringToHash("Hitted");
    private int barrierHash = Animator.StringToHash("Barrier");
    private int specialHash = Animator.StringToHash("Special");

    // Variaveis relacionadas aos audios do personagem
    [SerializeField]private AudioSource correndoAudio;
    [SerializeField]private AudioSource saltandoAudio;
    [SerializeField]private AudioSource atacandoAudio;
    [SerializeField]private AudioSource dashingAudio;
    [SerializeField]private AudioSource deadingAudio;
    [SerializeField]private AudioSource hittedAudio;
    [SerializeField]private AudioSource barrierAudio;
    [SerializeField]private AudioSource specialAudio;

    // Variaveis de contagem de tempo
    private float tempo = 0f;

    // -----------------------------------------Metodos-----------------------------------------
    // Metodo de quando se inicia o jogo
    private void Awake ()
    {
        // ------------ Pega os componentes
        // RigidBody2D
        rb = GetComponent<Rigidbody2D>();
        // Animacoes
        animator = GetComponent<Animator>();
        // Sprite Render
        sr = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
    }

    void FixedUpdate()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaPersonagem >= 0)
        {
            vidaPersonagem -= (decrescimoVida * 0.001f);
        }
        

        // Animacao de morte do personagem
        if(vidaPersonagem < 0f)
        {
            if (machinima == false)
            {
                deadingAudio.Play();
            }
            animator.SetTrigger(deadingHash);
            machinima = true;
        }

        if(machinima == false)
        {
            // Auxiliar do Dash
            tempo += Time.deltaTime;

            // Loading do Dash
            if (tempo > tempoCargaDash)
            {
                if (cargaDash >= 0 && cargaDash < limiteCargaDash)
                {
                    cargaDash++;
                }
                tempo = 0f; 
            }

            //  ------------ Esses codigos estao aqui por auxiliarem as animacoes
            // Codigo do movimento do personagem em si
            rb.velocity = new Vector2(horizontalInput * velocidadePersonagem, rb.velocity.y);
            // Ver se pode pular
            permitirPulo = Physics2D.OverlapCircle(basePersonagem.position, tamanhoCirculoBase, chaoLayer);

            //  ------------ Codigo que controla as animacoes
            // Correndo
            animator.SetBool(correndoHash, horizontalInput != 0);
            // Saltando
            animator.SetBool(saltandoHash, !permitirPulo);
            // Caindo
            animator.SetBool(caindoHash, rb.velocity.y < -quedaVelocidade && !permitirPulo);
            if (rb.velocity.y < -quedaVelocidade && permitirPulo == false)
            {
                vidaPersonagem = vidaPersonagem - danoDeQueda;
            }

            // Audios do personagem
            // Audio de correndo
            if(horizontalInput == 0 || permitirPulo == false)
            {
                correndoAudio.Stop();
            }

            //  ------------ Controla o lado das animacoes, se estao para frente o para tras;
            if(horizontalInput > 0)
            { 
                sr.flipX = false;
            }else if (horizontalInput < 0)
            {
                sr.flipX = true;
            }
        }
    }
    
    // Controle de movimentacao do personagem
    void OnMove(InputValue value)
    {
        if(machinima == false)
        {
            // Pegar so o componente horizontal do vetor no input
            Input = value.Get<Vector2>();
            horizontalInput = Input.x;    
            correndoAudio.Play();
        }
    }
    
    // Controle de pulo do personagem
    void OnJump()
    {
        if (personagem == 0 || personagem == 2)
        {
            if(machinima == false)
            {
                // Codigo do pulo em si
                if(permitirPulo == true)
                {
                    // ---------------------------------------------Aqui----------------------------
                    //saltandoAudio.Play();
                    rb.AddForce(Vector2.up * forcaPulo);
                }
            }
        }
        
    }   
    
    // Controle de ataque do personagem
    void OnAttack()
    {
        if(machinima == false)
        {
            if(Time.time > proximoAtaque)
            {
                // Contador para proximo ataque
                proximoAtaque = Time.time + intervaloAtaque;
                
                atacandoAudio.Play();

                animator.SetTrigger(atacandoHash);

                //Flipa o colisor do ataque o mantendo sempre a frente do personagem
                // Offset
                offsetCodigo = new Vector3(offset, 0.0f, 0.0f);
                // Codigo do flip
                if(sr.flipX == false)
                {
                    colisor.position = frente.position + offsetCodigo;
                }else
                {
                    colisor.position = atras.position - offsetCodigo;
                }
            }
        } 
    }

    // Controle do Dash do pesonagem
    void OnDash ()
    {
        if(machinima == false)
        {
            // Ve se o personagem nao esta atacando
            if(Time.time > proximoAtaque)
            {
                // Ve se o personagem tem carga para o dash
                if(cargaDash > 0)
                {
                    switch (personagem)
                    {
                        case 0:
                            if(sr.flipX == false)
                            {
                                rb.AddForce(Vector2.right * forcaDash);
                            }else
                            {
                                rb.AddForce(Vector2.left * forcaDash);
                            }
                            break; 
                        case 1:
                            if(sr.flipX == false)
                            {
                                rb.AddForce(Vector2.right * forcaDash * 2);
                            }else
                            {
                                rb.AddForce(Vector2.left * forcaDash * 2);
                            }
                            break;
                        case 2:
                            if(sr.flipX == false)
                            {
                                rb.AddForce(Vector2.right * forcaDash * 2);
                            }else
                            {
                                rb.AddForce(Vector2.left * forcaDash * 2);
                            }
                            break;
                    }

                    dashingAudio.Play();
                    animator.SetTrigger(dashingHash);
                    cargaDash--;
                }
            }
        }
    }

    // Animacao ho hit no personagem
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(machinima == false)
        {
            if(other.gameObject.CompareTag(nomeDaTag))
            {
                hittedAudio.Play();
                animator.SetTrigger(hittedHash);
                
                machinima = true;
                
                StartCoroutine(hittedTime());
            }
            
            if(other.gameObject.CompareTag(nomeDaTagDeAtaque))
            {
                Destroy(other.gameObject);
            }
        }
    }

    // Funcao responsavel por fazer o personagem ficar parado quando sofre um hit
    private IEnumerator hittedTime()
    {
        yield return new WaitForSeconds(tempoDeHit);
        machinima = false;
        vidaPersonagem = -1f;
    }

    // Funcao responsavel por fazer o personagem ficar parado quando ativa o escudo
    private IEnumerator shieldTime()
    {
        yield return new WaitForSeconds(tempoDeShield);
        
        rb.velocity = new Vector2(horizontalInput * velocidadePersonagem, rb.velocity.y);

        machinima = false;
    }

    private IEnumerator specialTime()
    {
        yield return new WaitForSeconds(tempoDeSpecial);
        
        rb.velocity = new Vector2(horizontalInput * velocidadePersonagem, rb.velocity.y);

        machinima = false;
    }

    // Funcao responsavel pelas skills especiais das transformacoes do alien
    void OnSpecial()
    {
        if(machinima == false && personagem == 1)
        {
            if (cargaDash > 0 && Time.time > proximoAtaque)
            {
                
                rb.velocity = Vector2.zero;
                correndoAudio.Stop();
                animator.SetBool(correndoHash, rb.velocity.x == 0);
                cargaDash--;
                StartCoroutine(shieldTime());
                machinima = true;
                animator.SetTrigger(barrierHash);
                barrierAudio.Play();
            }            
        }
        if(machinima == false && personagem == 2)
        {
            if (cargaDash > 0 && Time.time > proximoAtaque)
            {
                rb.velocity = Vector2.zero;
                correndoAudio.Stop();
                animator.SetBool(correndoHash, rb.velocity.x == 0);
                cargaDash--;
                StartCoroutine(specialTime());
                machinima = true;
                animator.SetTrigger(specialHash);    
                specialAudio.Play();
                if(sr.flipX == false)
                {
                    novoProjetil1 = Instantiate(novoProjetil, frente.position, Quaternion.identity);
                    novoProjetil1.GetComponent<Projetil>().direcao = Vector2.right;
                }else
                {
                    novoProjetil1 = Instantiate(novoProjetil, atras.position, Quaternion.identity);
                    novoProjetil1.GetComponent<Projetil>().direcao = Vector2.right;
                } 
            }            
        }
    }
}

// Audios, spawn do golpe, som de fogo do golpe, configurar o golpe