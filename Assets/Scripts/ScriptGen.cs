using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptGen : MonoBehaviour
{
    [SerializeField]private Transform forma0;
    [SerializeField]private Transform forma1;
    [SerializeField]private Transform forma2;
    [SerializeField]private Transform posicaoInicial;
    Transform novaforma;
    private int formaAtual = 0;
    Vector3 posicaoObjeto;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        novaforma = Instantiate(forma0, posicaoInicial.transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        posicaoInicial.transform.position = novaforma.transform.position;
    }

    // tenho que pegar carga do dash, vida e posicao do pesonagem, depois destruir um e spawnar outro
    void OnChangeRight()
    {
        switch (formaAtual)
        {
            case 0:
                posicaoObjeto = novaforma.transform.position;
                Destroy(novaforma.gameObject, 0.0f);
                novaforma = Instantiate(forma1, novaforma.transform.position, Quaternion.identity);
                formaAtual = 1;
                break;

            case 1:
                posicaoObjeto = novaforma.transform.position;
                Destroy(novaforma.gameObject, 0.0f);
                novaforma = Instantiate(forma2, novaforma.transform.position, Quaternion.identity);
                formaAtual = 2;
                print(formaAtual);
                break;

            case 2:
                posicaoObjeto = novaforma.transform.position;
                Destroy(novaforma.gameObject, 0.0f);
                novaforma = Instantiate(forma0, novaforma.transform.position, Quaternion.identity);
                formaAtual = 0;
                print(formaAtual);
                break;
        }
    }
    
    void OnChangeLeft()
    {
        switch (formaAtual)
        {
            case 0:
                posicaoObjeto = novaforma.transform.position;
                Destroy(novaforma.gameObject, 0.0f);
                novaforma = Instantiate(forma2, novaforma.transform.position, Quaternion.identity);
                formaAtual = 2;
                print(formaAtual);
                break;

            case 1:
                posicaoObjeto = novaforma.transform.position;
                Destroy(novaforma.gameObject, 0.0f);
                novaforma = Instantiate(forma0, novaforma.transform.position, Quaternion.identity);
                formaAtual = 0;
                print(formaAtual);
                break;

            case 2:
                posicaoObjeto = novaforma.transform.position;
                Destroy(novaforma.gameObject, 0.0f);
                novaforma = Instantiate(forma1, novaforma.transform.position, Quaternion.identity);
                formaAtual = 1;
                break;
        }
    }

}
