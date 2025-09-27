using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DSCollectablesManager : MonoBehaviour
{
    public List<GameObject> collectablesList = new List<GameObject>(); // List to hold all collectable items in the scene
    public List<Image> collectablesImages = new List<Image>();
    public Canvas uiCanvas;

    public void addCollectable(GameObject gobj , Image icon)
    {
        collectablesList.Add(gobj);
        Image newIcon = Instantiate(icon , uiCanvas.transform);
        
        collectablesImages.Add(newIcon);
        newIcon.enabled = true;

    }

    public void removeCollectable(GameObject gobj)
    {
        int index = collectablesList.IndexOf(gobj);
        if (index != -1)
        {
            collectablesList.RemoveAt(index);
            Image imgRef = collectablesImages[index];
            Destroy(imgRef.gameObject);
            collectablesImages.RemoveAt(index);
        }
    }
    private void Awake()
    {
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
            foreach(var item in collectablesList)
            {
                Transform itemTransform = item.transform;
                
                Gizmos.DrawLine(itemTransform.position, this.transform.position);
                
            }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
