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
        [SerializeField, HideInInspector] private int m_currentAmmoCount;
        private int m_clipSize;
        
        //TankSpeed
        [SerializeField, HideInInspector] private float m_tankSpeed;

        public void Awake()
        {
            m_uiDocument = GetComponent<UIDocument>();
            m_rb = GetComponent<Rigidbody>();

            if (m_uiDocument == null)
                throw new UnityException("No UIDocument found");

            m_uiRoot = m_uiDocument.rootVisualElement;
            m_speedLabel = m_uiRoot.Q<Label>(name: "speed-label");

            var speedBinding = new DataBinding
            {
                dataSource = this,
                dataSourcePath = new Unity.Properties.PropertyPath("m_tankSpeed"),
                bindingMode = BindingMode.ToTarget,
                updateTrigger = BindingUpdateTrigger.OnSourceChanged
            };
            
            m_speedLabel.SetBinding("text", speedBinding);
            
            speedBinding.sourceToUiConverters.AddConverter((ref float speed) =>
            {
                float mphSpeed = speed * 2.2369362912f;
                float processedSpeed = Mathf.Floor(mphSpeed);
                return $"Speed (mph): {processedSpeed}";
            });
            
            
            m_ammoLabel = m_uiRoot.Q<Label>(name: "ammo-label");
            var ammoBinding = new DataBinding
            {
                dataSource = this,
                dataSourcePath = new Unity.Properties.PropertyPath("m_currentAmmoCount"),
                bindingMode = BindingMode.ToTarget,
                updateTrigger = BindingUpdateTrigger.OnSourceChanged
            };
                
                m_ammoLabel.SetBinding("text", ammoBinding);
                
                ammoBinding.sourceToUiConverters.AddConverter((ref int currentAmmoCount) =>
                {
                    return $"Ammo: {m_currentAmmoCount} / {m_clipSize}";
                });
        }

        private void FixedUpdate()
        {
            m_tankSpeed = m_rb.linearVelocity.magnitude;
            //m_speedLabel.text = $"speed: {m_tankSpeed}";
            //m_ammoLabel.text = $"ammo: {m_currentAmmoCount}";
        }
}
