using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float colisionOffset = 0.01f;
    public float reculForce = 5f;

    public FixedJoystick joystick;

    public Transform transform;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    int LifePoints = 3;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;

    //JUMP
    public float jumpForce = 2f;
    bool isGrounded;
    bool isMoving;
    bool isDead = false;

    bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();

        Physics2D.SyncTransforms();
    }
    

    // Update is called once per frame

    void FixedUpdate(){

        /*if(movementInput != Vector2.zero && movementInput.x != 0 ){
            bool sucess = TryMove(movementInput);
            if(!sucess){
                sucess = TryMove(new Vector2(movementInput.x,0));

                if(!sucess){
                sucess = TryMove(new Vector2(0,movementInput.y));
                }
            }
            animator.SetBool("isMoving",sucess);
            isMoving = true;
        }
        else{
            animator.SetBool("isMoving",false);
            isMoving = false;
        }*/

        /*if(Input.GetKey(KeyCode.RightArrow)){
            print("droite");
            Vector2 direction = new Vector2(1,0);
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            print("gauche");
            Vector2 direction = new Vector2(-1,0);
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        }
        if(Input.GetKey(KeyCode.M)){
            if(isGrounded){
                Jump();
            }
        }*/


        if(movementInput != Vector2.zero && movementInput.x != 0 ){
            animator.SetBool("isMoving",true);
            isMoving = true;
        }
        else{
            animator.SetBool("isMoving",false);
            isMoving = false;
        }


        //POUR ACTIVER LE MODE JOYSTICK NICO ET THEO
        //movementInput = new Vector2(joystick.Horizontal,0);
        //

        if(!isDead){
            rb.velocity = new Vector2(movementInput.x * moveSpeed * Time.fixedDeltaTime,rb.velocity.y);

            if(movementInput.x < 0 && facingRight ){
                transform.localScale *= new Vector2(-1,1);
                facingRight = false; 
                //spriteRenderer.flipX = true;
            }
            else if(movementInput.x > 0 && !facingRight){
            //spriteRenderer.flipX = false; 
            transform.localScale *= new Vector2(-1,1); 
                facingRight = true; 

            }
        }
          
    }

    /*private bool TryMove(Vector2 direction){
        direction.y = 0;
        int count = rb.Cast(direction,movementFilter,castCollisions,moveSpeed*Time.fixedDeltaTime + colisionOffset);
            
        if (count == 0 && rb.position.y <= -0.647){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else{
            return false;
        }
    }*/

    
    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        // Ajoutez également une condition pour déclencher l'animation de saut si nécessaire
        // animator.SetTrigger("isJumping");
    }

    void OnJump(){
        if(isGrounded){
            Jump();
        }
    }
    void OnFire(){
        print("prout");
        animator.SetTrigger("isAttacking");
        //animator.SetBool("isDead",true);
    }

    void OnMove(InputValue movementValue){
        
        movementInput = movementValue.Get<Vector2>();
        movementInput.y = 0;
    }

    
    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Ennemi"))
        {   
            animator.SetTrigger("isHit");
            Vector2 directionKnock = (transform.position - collision.transform.position).normalized;
            //rb.velocity = new Vector2(directionKnock.x * reculForce, jumpForce);
            print(directionKnock);
            Vector2 newPosition = rb.position + directionKnock * reculForce * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if(LifePoints == 3){
                Heart3.SetActive(false);
                LifePoints -= 1;
            }
            else if(LifePoints == 2){
                Heart2.SetActive(false);
                LifePoints -= 1;
            }
            else if(LifePoints == 1){
                Heart1.SetActive(false);
                LifePoints -= 1;
                animator.SetBool("isDead",true);
                isDead = true;
            }
        }
    }

    

    void OnCollisionExit2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
        isGrounded = false;
    }
    }
    
}
