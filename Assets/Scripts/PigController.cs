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
    Animator animator;

    float direction = 1f;
    public float reculForce = 5f;

    public float LifePoints = 6;
    public float moveSpeed = 100f;

    bool changeDirection = false;
    bool aller = true; 


    void Start()
    {
        animator = GetComponent<Animator>();
     rb = GetComponent<Rigidbody2D>();  
     transform = GetComponent<Transform>(); 
     positionDebut = new Vector2(transform.position.x,transform.position.y);
     positionFin = new Vector2(positionDebut.x + 1,transform.position.y);
     transform.localScale *= new Vector2(-1,1);
     if(this.CompareTag("EnnemiCanon")){
        canonPig();
     }

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(this.CompareTag("Ennemi")){
            normalPig();
        }
        checkDie();
    }

    void checkDie(){
        if(LifePoints <= 0){
            Destroy(this.gameObject);
        }
    }

    public void takeDammage(GameObject sword){
        SoundManager.instance.PlayPigDamage();
        animator.SetTrigger("isHit");
        LifePoints -= 1;
        Vector2 directionKnock = (transform.position - sword.transform.position).normalized;
        Vector2 newPosition = rb.position + directionKnock * reculForce * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void normalPig(){
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
    }

    public void canonPig(){
        StartCoroutine(Fire());  
    }

    IEnumerator Fire()
    {
        while (true)
        {
            animator.SetTrigger("isFire");
            yield return new WaitForSeconds(3f);
        }
    }
}
