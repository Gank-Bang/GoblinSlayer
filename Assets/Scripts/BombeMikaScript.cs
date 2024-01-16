using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeMikaScript : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("DialogueMika")){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("Exploded",true);
        }
    }

    public void Explose(){
        Destroy(this.gameObject);
    }
}
