using UnityEngine;
using UnityEngine.UI;
using FMODUnity;


public class DSCollactableBehavor : MonoBehaviour
{

    public float points = 1f; // Points awarded for collecting this item
    public Collider playerCollider; // Reference to the player's collider
    public DSPlayerCollectableBehavor dsPlayerCollectableBehavor; // Reference to the player's collectable behavior script
    public float rotateSpeed = 50f; // Rotation speed in degrees per second
    public float upDownDistance = 0.5f; // Distance to move up and down
    public Image icon; // Icon to represent the collectable on the map
    [Header("Audio Effects")]
    //public AudioClip collectAudioClip; // Sound to play upon collection

    //public AudioSource audioSource; // Audio source to play the sound

    FMOD.Studio.EventInstance Coinsfx;
    
    [Header("Visual Effects")] // TODO : Later on add particle effects
    public GameObject particleEffect;// Particle effect object to play upon collection

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Coinsfx = RuntimeManager.CreateInstance("event:/Master/SFX/CoinCollectSFX");
        RuntimeManager.AttachInstanceToGameObject(Coinsfx, this.transform, false);
        // Find the collectables manager in the scene
        m_collectablesManagerObject = GameObject.FindWithTag("CollectableManagerTag");
        if (m_collectablesManagerObject != null)
        {
            m_collectablesManagerScript = m_collectablesManagerObject.GetComponent<DSCollectablesManager>();
            if (m_collectablesManagerScript != null)
            {
                m_collectablesManagerScript.addCollectable(this.gameObject , icon );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Coinsfx.set3DAttributes(RuntimeUtils.To3DAttributes(this.transform));
        // rotate object around y and z axis
        transform.Rotate(0, rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime);
       

        // move object up and down using sine wave
        float upDownY = Mathf.Sin(Time.time) * upDownDistance * Time.deltaTime; // Amplitude of 0.5 units

        
        
        this.transform.position = new Vector3(transform.position.x, upDownY + transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == playerCollider)
        {
            if (dsPlayerCollectableBehavor != null)
            {
                dsPlayerCollectableBehavor.AddPoints(points);
                /*
                if (audioSource != null && collectAudioClip != null)
                    audioSource.PlayOneShot(collectAudioClip); // Play collection sound
                    */
                Coinsfx.start();
                m_collectablesManagerScript.removeCollectable(this.gameObject); // Remove from manager's list
                Destroy(gameObject); // Destroy the collectable item after collection
                
                // TODO : Instantiate particle effect at the position of the collectable
            }
        }
    }

    private GameObject m_collectablesManagerObject;
    private DSCollectablesManager m_collectablesManagerScript;

}
