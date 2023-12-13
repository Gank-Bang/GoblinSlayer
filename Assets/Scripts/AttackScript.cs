using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        PigController Pigscript = collision.gameObject.GetComponent<PigController>();
        BoxScript BoxScript = collision.gameObject.GetComponent<BoxScript>();
        if(Pigscript != null){
            Pigscript.takeDammage(this.gameObject);
        }
        else if(BoxScript != null){
            BoxScript.DestroyBox();
        }

    }
}
