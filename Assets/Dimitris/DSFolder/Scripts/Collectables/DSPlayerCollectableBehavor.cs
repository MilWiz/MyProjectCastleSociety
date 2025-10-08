using UnityEngine;
using TMPro;
public class DSPlayerCollectableBehavor : MonoBehaviour
{

    [Header("Refernces")]
    public Canvas playerUICanvas  ;
    public TMP_Text pointsTextMesh;

    public float AddPoints(float points)
    {
        if (points <= 0) return m_totalPoints; // Ignore non-positive points
        m_totalPoints += points;
        return m_totalPoints;
    }

    public float TotalPoints { get { return m_totalPoints; } }

    public float getTotalPoints()
    {
        return m_totalPoints;
    }

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_collectablesManagerObject = GameObject.FindWithTag("CollectableManagerTag");

        if (m_collectablesManagerObject != null)
        {
            m_collectablesManagerScript = m_collectablesManagerObject.GetComponent<DSCollectablesManager>();
            if (m_collectablesManagerScript != null)
            {
                foreach (var item in m_collectablesManagerScript.collectablesList)
                {
                    if (item != null)
                    {
                        DSCollactableBehavor collactableBehavor = item.GetComponent<DSCollactableBehavor>();
                        if (collactableBehavor != null)
                        {
                            m_maxPoints += collactableBehavor.points;
                        }
                    }
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUICanvas.isActiveAndEnabled)
        {
            // Update UI elements related to points here if needed
            string pointsText = $" {m_totalPoints} / {m_maxPoints}";
            pointsTextMesh.text = pointsText;
        }
    }

    [Header("Debug")]
    [SerializeField] private float m_maxPoints = 0f;
    [SerializeField] private GameObject m_collectablesManagerObject = null;
    [SerializeField] private DSCollectablesManager m_collectablesManagerScript = null;
    private float m_totalPoints = 0f; // Total points collected by the player
}
