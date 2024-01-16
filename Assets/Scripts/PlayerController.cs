using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public GameObject Diamond1;
    public GameObject Diamond2;
    public GameObject Diamond3;

    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    int LifePoints = 3;
    int Diamonds = 0;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject door;

    //JUMP
    public float jumpForce = 2f;
    float joystickX = 0;
    Vector2 Movement ;

    public bool isAttacking = false;
    public bool isGrounded;
    public bool isReallyGrounded;
    bool isMoving;
    bool isDead = false;

    bool facingRight = true;

    bool wallRight = false;
    bool wallLeft = false;

    bool isTouchingWall = false;


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



        if(isTouchingWall && !isReallyGrounded && rb.velocity.y < 0){
            animator.SetBool("isWall",true);
            rb.gravityScale = 0.3f;
        }
        else{
            animator.SetBool("isWall",false);
            rb.gravityScale = 1f;
        }


        if(Movement != Vector2.zero && Movement.x != 0 ){
            animator.SetBool("isMoving",true);
            isMoving = true;
        }
        else{
            animator.SetBool("isMoving",false);
            isMoving = false;
        }
        

        //POUR ACTIVER LE MODE JOYSTICK NICO ET THEO   

        if(joystick.Horizontal>0){
            joystickX = 1;
        }
        else if(joystick.Horizontal<0){
            joystickX = -1;
        }
        else{
            joystickX = 0;
        }
        //print("jostickX : "+joystickX);
        //print("movementInput : " + movementInput);

    
        //

        if(!isDead){
            if(!isAttacking){
                if(movementInput.x == 0 && joystickX == 0){
                    Movement.x = 0;
                }
                Movement.x = movementInput.x + joystickX;
                Movement.Normalize();
                rb.velocity = new Vector2(Movement.x * moveSpeed * Time.fixedDeltaTime,rb.velocity.y);

                if(Movement.x < 0 && facingRight ){
                    transform.localScale *= new Vector2(-1,1);
                    //spriteRenderer.flipX = !spriteRenderer.flipX;
                    facingRight = false; 
                    //spriteRenderer.flipX = true;
                }
                else if(Movement.x > 0 && !facingRight){
                    //spriteRenderer.flipX = false; 
                    transform.localScale *= new Vector2(-1,1); 
                    //spriteRenderer.flipX = !spriteRenderer.flipX;
                    facingRight = true; 

                }
            }
        }
        else{
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
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

    public void OnJump(){
        if(isGrounded){
            Jump();
        }
    }
    public void OnFire(){
        //print("prout");
        isAttacking = true;
        animator.SetTrigger("isAttacking");
        //animator.SetBool("isDead",true);
    }

    void OnMove(InputValue movementValue){
        Vector2 movement = movementValue.Get<Vector2>();
        if(wallRight && movement.x >= 0 || wallLeft && movement.x <= 0){

        }
        else{
            movementInput = movementValue.Get<Vector2>();
        }
        //movementInput.y = 0;
    }

    
    void OnCollisionEnter2D(Collision2D collision) {

        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall")){
            isTouchingWall = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Planche") || collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Box") || collision.collider.CompareTag("TopCollider")  ) {
            isGrounded = true;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") ){
            animator.SetBool("isWall",false);
            isReallyGrounded = true;
        }

        /*if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.collider.CompareTag("Box")  || collision.collider.CompareTag("BoxDiamond") ) {
            if(facingRight){
                wallRight = true;
                wallLeft = false;
                movementInput = Vector2.zero;
            }
            else{
               wallLeft = true;
               wallRight = false;
               movementInput = Vector2.zero; 
            }
        }*/
        if (collision.gameObject.CompareTag("Ennemi") )
        {   
          onHit(collision);
        }
        

        if (collision.gameObject.CompareTag("Diamond"))
        {   

            if(Diamonds == 0){
                Diamond1.SetActive(true);
                Diamonds += 1;
            }
            else if(Diamonds == 1){
                Diamond2.SetActive(true);
                Diamonds += 1;
            }
            else if(Diamonds == 2){
                Diamond3.SetActive(true);
                Diamonds += 1;
                //animator.SetBool("isDead",true);
                //isDead = true;

                ////PROTOTYPE
                GameObject doorObject = GameObject.Find("Door");
                if (doorObject != null)
                {
                    Animator doorAnimator = doorObject.GetComponent<Animator>();
                    doorAnimator.SetBool("isOpen",true);
                    door.GetComponent<DoorScript>().doorOpened = true;
                }
                ////PROTOTYPE
                
            }
            collision.gameObject.SetActive(false);

        }
    }

    

    void OnCollisionExit2D(Collision2D collision) {

        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Box")) {
            wallLeft = false;
            wallRight = false;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")){
            isReallyGrounded = false;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall")){
            isTouchingWall = false;
             animator.SetBool("isWall",false);
             rb.gravityScale = 1f;
        }

    }

    public void onHit(Collision2D collision){
        animator.SetTrigger("isHit");
            Vector2 directionKnock = (transform.position - collision.transform.position).normalized;
            //rb.velocity = new Vector2(directionKnock.x * reculForce, jumpForce);
            //print(directionKnock);
            Vector2 newPosition = rb.position + directionKnock * reculForce * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if(LifePoints == 3){
                Heart3.SetActive(false);
                LifePoints -= 1;
                //Diamond1.SetActive(true);

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

    public void onHit(Collider2D collision){
        animator.SetTrigger("isHit");
            Vector2 directionKnock = (transform.position - collision.transform.position).normalized;
            //rb.velocity = new Vector2(directionKnock.x * reculForce, jumpForce);
            //print(directionKnock);
            Vector2 newPosition = rb.position + directionKnock * reculForce * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if(LifePoints == 3){
                Heart3.SetActive(false);
                LifePoints -= 1;
                //Diamond1.SetActive(true);

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

    public void Die(){
        DoorScript scriptDoor = door.GetComponent<DoorScript>();
        int sceneActuelle = scriptDoor.nextScene-1;
        if(sceneActuelle == -1){
            sceneActuelle = 3;
        }
        print(sceneActuelle);
        SceneManager.LoadSceneAsync(sceneActuelle);
    }

    
}
