using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBone : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;
    public GameObject descriptionBox;
    public Text descriptionText;
    public Button soundButton; 
    public AudioSource audioSource; 

    private Material originalMaterialSelection;
    private Transform selection;
    private RaycastHit raycastHit;

    private BoneDescription currentBoneDescription; 

    void Start()
    {
        soundButton.gameObject.SetActive(false); 
        soundButton.onClick.AddListener(ToggleBoneSound); 
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && Physics.Raycast(ray, out raycastHit))
            {
                Transform hitTransform = raycastHit.transform;

                if (hitTransform.CompareTag("Bone"))
                {
                    SelectBoneObject(hitTransform);
                }
                else
                {
                    DeselectBoneObject();
                }
            }
        }
    }

    void SelectBoneObject(Transform bone)
    {
        if (selection != null)
        {
            selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
        }

        
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        selection = bone;

        if (selection != null)
        {
            MeshRenderer renderer = selection.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                originalMaterialSelection = renderer.sharedMaterial;
                renderer.material = selectionMaterial;
            }
            ShowDescription(selection);
        }
    }

    void DeselectBoneObject()
    {
        if (selection != null)
        {
            selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
            selection = null;
        }

        descriptionBox.SetActive(false);
        soundButton.gameObject.SetActive(false);

        
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void ShowDescription(Transform bone)
    {
        BoneDescription boneDescription = bone.GetComponent<BoneDescription>();
        if (boneDescription != null)
        {
            descriptionText.text = boneDescription.description;
            descriptionBox.SetActive(true);
            currentBoneDescription = boneDescription;

           
            if (boneDescription.hasAudio && boneDescription.audioClip != null)
            {
                soundButton.gameObject.SetActive(true);
            }
            else
            {
                soundButton.gameObject.SetActive(false);
            }
        }
        else
        {
            descriptionBox.SetActive(false);
            soundButton.gameObject.SetActive(false);
        }
    }

    void ToggleBoneSound()
    {
        if (currentBoneDescription != null && currentBoneDescription.hasAudio && currentBoneDescription.audioClip != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.clip = currentBoneDescription.audioClip;
                audioSource.Play();
            }
        }
    }
}
