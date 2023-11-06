using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeguir : MonoBehaviour
{
    public Transform centro; 
    public float suavizacao = 0.125f; 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(centro.position.x, centro.position.y, (centro.position.z - 10) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (centro != null)
        {
             transform.position = new Vector3(centro.position.x, centro.position.y, (centro.position.z - 10) );
        }
    }
}
