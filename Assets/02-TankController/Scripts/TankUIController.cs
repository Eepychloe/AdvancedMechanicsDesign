using UnityEngine;
using UnityEngine.UIElements;

public class TankUIController : MonoBehaviour
{
    private UIDocument m_uiDocument;
    private VisualElement m_uiRoot;
    private Rigidbody m_rb;
    private Label m_speedLabel;
    private Label m_ammoLabel;
    
        //Weapon Data
        private int m_currentAmmoCount;
        private int m_clipSize;
        
        //TankSpeed
        private float m_tankSpeed;

        public void Awake()
        {
            m_uiDocument = GetComponent<UIDocument>();
            m_rb = GetComponent<Rigidbody>();
            
            if (m_uiDocument == null)
                throw new UnityException("No UIDocument found");

            m_uiRoot = m_uiDocument.rootVisualElement;
            m_speedLabel = m_uiRoot.Q<Label>(name: "speed-label");
            m_ammoLabel = m_uiRoot.Q<Label>(name: "ammo-label");
        }

        private void FixedUpdate()
        {
            m_tankSpeed = m_rb.linearVelocity.magnitude;
            m_speedLabel.text = $"speed: {m_tankSpeed}";
            m_ammoLabel.text = $"ammo: {m_currentAmmoCount}";
        }
}
