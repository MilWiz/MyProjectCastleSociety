using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ShowObjectsOnMap : MonoBehaviour
{

    public Camera MapCamera;
    public Canvas MapUICanvas;
    public DSCollectablesManager collectableManager;
    private int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        i = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (MapUICanvas.gameObject.activeSelf)
        {
            if (i < collectableManager.collectablesList.Count)
            {
                
                Vector3 position  = MapCamera.WorldToScreenPoint(collectableManager.collectablesList[i].transform.position);
                collectableManager.collectablesImages[i].transform.position = position;
                //collectablesImages[i].transform.(-Camera.main.transform.forward)
                i++;
            }
        }
        else
        {
            
            i = 0;
        }
    }
}
