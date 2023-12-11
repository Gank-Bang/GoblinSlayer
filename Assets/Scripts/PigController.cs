using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Transform transform;
    Vector2 positionDebut;
    Vector2 positionFin;

    float direction = 1f;
    public float LifePoints = 6;
    public float moveSpeed = 100f;

    bool changeDirection = false;
    bool aller = true; 


    void Start()
    {
     rb = GetComponent<Rigidbody2D>();  
     transform = GetComponent<Transform>(); 
     positionDebut = new Vector2(transform.position.x,transform.position.y);
     positionFin = new Vector2(positionDebut.x + 1,transform.position.y);
     transform.localScale *= new Vector2(-1,1);

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        rb.velocity = new Vector2( direction * moveSpeed * Time.fixedDeltaTime,rb.velocity.y);

        if(transform.position.x <= positionFin.x){
            direction = 1f;
        }
        if(transform.position.x >= positionFin.x){
            direction = -1f;
        }

        if(aller && transform.position.x >= positionFin.x){
            changeDirection = true;
        }
        else if(!aller && transform.position.x <= positionFin.x){
            changeDirection = true;
        }

        if(changeDirection){
            if(aller){
                positionFin.x -= 1;
                aller = false;
            }
            else{
                positionFin.x += 1;
                aller = true;
            }
            transform.localScale *= new Vector2(-1,1);
            changeDirection = false;
        }

        checkDie();
    }

    void checkDie(){
        if(LifePoints <= 0){
            Destroy(this.gameObject);
        }
    }
}
