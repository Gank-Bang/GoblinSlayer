using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MikaScript : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject bombPrefab;
    public Transform playerTransform;

    public float LifePoints = 20;
    Animator animator;

    private SpriteRenderer spriteRenderer;

    public float bombSpeed = 4;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("LaunchBombTowardsPlayer", 0f, 3f);
    }

    void Update()
    {
        /*if (playerTransform != null)
        {
            // Calculate direction towards the player
            Vector2 direction = playerTransform.position - transform.position;

            // Face the player by adjusting the local scale
            if (direction.x > 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }*/
        if (playerTransform != null)
        {
            // Calculate direction towards the player
            Vector2 direction = playerTransform.position - transform.position;

            // Flip the sprite renderer to face the player
            spriteRenderer.flipX = direction.x < 0;
        }
        checkDie();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            Dialogue dialogueScript = dialogueBox.GetComponent<Dialogue>();
            dialogueScript.LanceDialogue();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
        }
    }

    void LaunchBombTowardsPlayer()
    {
        if (playerTransform != null)
        {
            animator.SetTrigger("MikaAttack");
            // Calculate direction towards the player
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            // Instantiate the bomb prefab and set its position and rotation
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

            // Set the velocity of the bomb in the calculated direction
            Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();
            bombRigidbody.velocity = direction * bombSpeed;
            
        }
    }

       void checkDie(){
            if(LifePoints <= 0){
                Destroy(this.gameObject);
            }
        }

    public void takeDammage(GameObject sword){
        animator.SetTrigger("MikaHit");
        LifePoints -= 1;
        print(LifePoints);
    }
}
