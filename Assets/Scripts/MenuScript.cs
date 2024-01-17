using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
       EventSystem.current.SetSelectedGameObject(null); 
    }
    
    // Update is called once per frame
    void Update()
    {
        EventSystem.current.SetSelectedGameObject(null); 
    }

    public void onPlay(){
        SceneManager.LoadSceneAsync(1);
    }

    public void onPlayLevel1(){
        EventSystem.current.SetSelectedGameObject(null); 
        SceneManager.LoadScene(1);
    }
    public void onPlayLevel2(){
        EventSystem.current.SetSelectedGameObject(null); 
        SceneManager.LoadScene(2);
    }
    public void onPlayLevel3(){
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(3);
    }
    public void onPlayLevel4(){
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(4);
    }

}
