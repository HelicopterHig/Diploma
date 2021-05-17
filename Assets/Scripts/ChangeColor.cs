using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private new Renderer renderer;

    

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }


    private void OnMouseEnter()
    {
        Color c = new Color(0.5f, 0.0f, 0.5f);
        renderer.material.color = c;
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}