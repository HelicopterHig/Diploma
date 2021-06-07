using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllScene : MonoBehaviour
{

    public GameObject buttonsMenu;
    public GameObject buttonsSelect;
    public GameObject buttonSingle;
    public GameObject buttonAll;
    //public GameObject buttonBack;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShiwSelectButt()
    {
        buttonsMenu.SetActive(false);
        buttonsSelect.SetActive(true);
        //buttonBack.SetActive(true);
    }
    public void BackButt()
    {
        buttonsMenu.SetActive(true);
        buttonsSelect.SetActive(false);
        //buttonBack.SetActive(true);
    }
    public void OpenScene()
    {
        buttonSingle.SetActive(false);
    }
    public void SingleGenerateLoad()
    {
        Application.LoadLevel("work!");
    }
    public void AllGenLoad()
    {
        Application.LoadLevel("all_gen");
    }
}
    
    

