using UnityEngine;
using TMPro;

public class ItemsManager : MonoBehaviour
{

    public ItemsInfo[] items;
    public TMP_Text[] Name;
    public TMP_Text[] Description;
    //public RenderTexture[] rndtxt;
    private RenderTexture renderTexture;
    public Camera[] cam;
    public ItemsInfo Silk;
    public TMP_Text silkname;
    public TMP_Text silkdescription;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Silk.Name = silkname;
        Silk.Description = silkdescription;
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Name = Name[i];
            items[i].Description = Description[i];

            /*
            RenderTexture CurrentRT = RenderTexture.active;
            RenderTexture.active = renderTexture;

            Cam.Render();

            Rect regionToRead = new Rect(0, 0, Cam.pixelWidth, Cam.pixelHeight);

            screenCapture = new Texture2D(Cam.pixelWidth, Cam.pixelHeight, TextureFormat.RGB24, false);
            screenCapture.ReadPixels(regionToRead, 0, 0, false);
            screenCapture.Apply();

            RenderTexture.active = CurrentRT;

            Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f));
            */

            renderTexture = new RenderTexture(cam[i].pixelWidth, cam[i].pixelHeight, 24);
            cam[i].targetTexture = renderTexture;
            
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = renderTexture;

            cam[i].Render();

            /*
            Texture2D texture = new Texture2D(rndtxt[i].width, rndtxt[i].height, TextureFormat.RGBA32, false);
            texture.ReadPixels(new Rect(0, 0, rndtxt[i].width, rndtxt[i].height), 0, 0);
            texture.Apply();
            */

            Rect regionToRead = new Rect(0, 0, cam[i].pixelWidth, cam[i].pixelHeight);

            Texture2D screenCapture = new Texture2D(cam[i].pixelWidth, cam[i].pixelHeight, TextureFormat.RGB24, false);
            screenCapture.ReadPixels(regionToRead, 0, 0, false);
            screenCapture.Apply();



            /*texture.ReadPixels(new Rect(0, 0, rndtxt[i].width, rndtxt[i].height), 0, 0);
            texture.Apply();

            RenderTexture.active = null;
            items[i].icon = Sprite.Create(texture,new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        
            */
            /*
            RenderTexture.active = currentRT;
            Rect rec = new Rect(0, 0, texture.width, texture.height);
            items[i].icon = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100f);
            */
            RenderTexture.active = currentRT;
            Rect rec = new Rect(0, 0, screenCapture.width, screenCapture.height);
            items[i].icon = Sprite.Create(screenCapture, rec, new Vector2(0.5f, 0.5f), 100f);
        }
    }

}
