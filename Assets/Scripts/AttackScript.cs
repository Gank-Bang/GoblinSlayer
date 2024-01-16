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
        MikaScript MikaScript = collision.gameObject.GetComponent<MikaScript>();
        if(Pigscript != null){
            Pigscript.takeDammage(this.gameObject);
        }
        else if(BoxScript != null){
            BoxScript.DestroyBox();
        }
        else if(MikaScript != null){
            MikaScript.takeDammage(this.gameObject);
        }

    }
}
