using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision){
            if (collision.CompareTag("Ennemi") || collision.CompareTag("EnnemiCanon") || collision.CompareTag("Diamond") ){
                //print("rien");
            }
            else if(collision.CompareTag("Player")){
                PlayerController playerScript = collision.GetComponent<PlayerController>();
                playerScript.onHit(collision);
            }


    }
    
}
