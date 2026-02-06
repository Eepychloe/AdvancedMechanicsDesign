using UnityEngine;
using UnityEngine.UIElements;

public class TankUIController : MonoBehaviour
{
    private UIDocument m_uiDocument;
    private VisualElement m_uiRoot;
    private Rigidbody m_rb;
    private TankAmmo m_tankAmmo;
    private Label m_speedLabel;
    private Label m_ammoLabel;
    private Label m_clipLabel;
    
        //Weapon Data
        [SerializeField, HideInInspector] private int m_currentAmmoCount;
        [SerializeField, HideInInspector] private int m_clipSize;
        
        //TankSpeed
        [SerializeField, HideInInspector] private float m_tankSpeed;

        public void Awake()
        {
            m_uiDocument = GetComponent<UIDocument>();
            m_rb = GetComponent<Rigidbody>();
            m_tankAmmo = GetComponent<TankAmmo>();

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
                    return $"Ammo: {m_currentAmmoCount} /";
                });
                
                m_clipLabel = m_uiRoot.Q<Label>(name: "clip-label");
                var clipBinding = new DataBinding
                {
                    dataSource = this,
                    dataSourcePath = new Unity.Properties.PropertyPath("m_clipSize"),
                    bindingMode = BindingMode.ToTarget,
                    updateTrigger = BindingUpdateTrigger.OnSourceChanged
                };
                m_clipLabel.SetBinding("text", clipBinding);
        }

        private void FixedUpdate()
        {
            m_tankSpeed = m_rb.linearVelocity.magnitude;
            m_currentAmmoCount = m_tankAmmo.GetCurrentAmmo();
            m_clipSize = m_tankAmmo.GetClipSize();
        }
}
