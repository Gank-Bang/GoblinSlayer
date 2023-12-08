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

    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;

    //JUMP
    public float jumpForce = 2f;
    bool isGrounded;
    bool isMoving;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        rb.velocity = new Vector2(movementInput.x * moveSpeed * Time.fixedDeltaTime,rb.velocity.y);

        

        if(movementInput.x < 0 ){
            spriteRenderer.flipX = true;
        }
        else if(movementInput.x > 0){
           spriteRenderer.flipX = false; 
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
}

    void OnCollisionExit2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
        isGrounded = false;
    }
    }
    
}
