using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NicoScript : MonoBehaviour
{

    public GameObject dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.CompareTag("Player")){
            dialogueBox.SetActive(true);
            Dialogue dialogueScript = dialogueBox.GetComponent<Dialogue>();
            dialogueScript.LanceDialogue();
        }

    }
    
    void OnTriggerExit2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player")){
            dialogueBox.SetActive(false);
        }

    }
    
}
