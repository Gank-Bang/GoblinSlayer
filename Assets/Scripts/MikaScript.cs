using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MikaScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dialogueBox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player")){
            dialogueBox.SetActive(true);
            Dialogue dialogueScript = dialogueBox.GetComponent<Dialogue>();
            dialogueScript.LanceDialogue();
        }

    }
    
    void OnCollisionExit2D(Collision2D collision){

        if(collision.gameObject.CompareTag("Player")){
            dialogueBox.SetActive(false);
        }

    }

    
    
}
