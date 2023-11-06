using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApenasCimaBaixo : MonoBehaviour
{

    public float velLevitacao = 1.0f; 
    public float altLevitacao = 0.5f;
    private Vector3 posicaoInicial;

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
        }
    }
}

