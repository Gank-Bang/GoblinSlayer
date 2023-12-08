using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    // Update is called once per frame

    void FixedUpdate(){
        if(movementInput != Vector2.zero && movementInput.x != 0){
            bool sucess = TryMove(movementInput);
            if(!sucess){
                sucess = TryMove(new Vector2(movementInput.x,0));

                if(!sucess){
                sucess = TryMove(new Vector2(0,movementInput.y));
                }
            }
            animator.SetBool("isMoving",sucess);
        }
        else{
            animator.SetBool("isMoving",false);
        }

        if(movementInput.x < 0 ){
            spriteRenderer.flipX = true;
        }
        else if(movementInput.x > 0){
           spriteRenderer.flipX = false; 
        }
          
    }

    private bool TryMove(Vector2 direction){
        direction.y = 0;
        int count = rb.Cast(direction,movementFilter,castCollisions,moveSpeed*Time.fixedDeltaTime + colisionOffset);
            
        if (count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else{
            return false;
        }
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }
}
