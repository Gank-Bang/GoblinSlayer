using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    bool playerInZone = false;
    public GameObject bulle;

    public bool doorOpened = false;

    public int nextScene;

    public int menu = 0;
    private void OnMouseDown(){
        openDoor();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            playerInZone = true;
            if(doorOpened){
                bulle.SetActive(true);
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            playerInZone = false;
            bulle.SetActive(false);
        }
    }


    public void openDoor(){
        if(playerInZone && doorOpened){
            GetComponent<BoxCollider2D>().enabled = false;
            EventSystem.current.SetSelectedGameObject(null); 
            print("porte ouverte");
            SceneManager.LoadScene(menu);
        }
    }
}

