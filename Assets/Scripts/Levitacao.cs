using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levitacao : MonoBehaviour
{

    public float velLevitacao = 1.0f; 
    public float altLevitacao = 0.5f;
    private Vector3 posicaoInicial;
    [SerializeField]private string proxCena = "Ato2";

    // Start is called before the first frame update
    void Start()
    {
        posicaoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float novoY = posicaoInicial.y + Mathf.Sin(Time.time * velLevitacao) * altLevitacao;

        transform.position = new Vector3(transform.position.x, novoY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(proxCena);
        }
    }
}

