using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class AllSceneManager : MonoBehaviour
{
    void Update()
    {
        /** if (Input.GetKeyDown(KeyCode.R))
         {
             Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("all_gen");
             Debug.Log("scene reloaded = " );
         }**/
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
