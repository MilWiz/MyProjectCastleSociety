using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Services.CloudSave.Models;
public class IconPhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    private Texture2D screenCapture;
    private RenderTexture renderTexture;
    public Camera Cam;
    //public GameObject ObjectPosition;
    public ItemsInfo Item;
    public TMP_Text Name;
    public TMP_Text Description;
    public GameObject GameObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Item.Name = Name;
        Item.Description = Description;
        //Item.item = GameObject;
        StartCoroutine(CapturePhoto());
    }


    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(false);
        /*
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CapturePhoto());
        }
        */
    }

    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
        /*
                renderTexture = new RenderTexture(Cam.pixelWidth, Cam.pixelHeight, 24);
                Cam.targetTexture = renderTexture;
                screenCapture = new Texture2D(Cam.pixelWidth, Cam.pixelHeight, TextureFormat.RGB24, false);

                RenderTexture CurrentRT = RenderTexture.active;
                RenderTexture.active = renderTexture;

                Rect regionToRead = new Rect(0, 0, Cam.pixelWidth, Cam.pixelHeight);
                screenCapture.ReadPixels(regionToRead, 0, 0, false);
                screenCapture.Apply();

                RenderTexture.active = CurrentRT;

                Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f));
                //photoDisplayArea.sprite = photoSprite;
                Items[i].icon = photoSprite;
        */

        RenderTexture CurrentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Cam.Render();

        Rect regionToRead = new Rect(0, 0, Cam.pixelWidth, Cam.pixelHeight);

        screenCapture = new Texture2D(Cam.pixelWidth, Cam.pixelHeight, TextureFormat.RGB24, false);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        RenderTexture.active = CurrentRT;

        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f));
        //Item.icon = photoSprite;

        //ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
    }
}
