using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public Text fala1; 
public Text fala2;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        fala1.GameObject.SetActive(false);
        fala2.GameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            fala1.GameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                fala1.GameObject.SetActive(false);
                fala2.GameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            // Hide the text message
            fala.GameObject.SetActive(false);
        }
    }
}
