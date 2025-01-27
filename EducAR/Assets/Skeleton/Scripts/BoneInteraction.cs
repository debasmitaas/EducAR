using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneInteraction : MonoBehaviour
{
    public Material highlightMaterial;
    public Material originalMaterial;
    public GameObject descriptionBox;

    void OnSelect()
    {
        //Highlight hobe
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            renderer.material = highlightMaterial;
        }

        descriptionBox.SetActive(true);
    }

    void OnDeselect()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial;
        } 

        descriptionBox.SetActive(false);
    }
}
