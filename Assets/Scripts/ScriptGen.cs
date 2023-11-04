using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGen : MonoBehaviour
{
    [SerializeField]private Transform forma0;
    [SerializeField]private Transform forma1;
    [SerializeField]private Transform forma2;

    private int formaAtual = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnChange()
    {
        switch (formaAtual)
        {
            case 0:
                formaAtual = 1;
                print(formaAtual);
                //forma1.position = forma0.position;
                break;

            case 1:
                formaAtual = 2;
                print(formaAtual);
                //forma2.position = forma1.position;
                break;

            case 2:
                formaAtual = 0;
                print(formaAtual);
                //forma0.position = forma2.position;
                break;
        }
    }
}
