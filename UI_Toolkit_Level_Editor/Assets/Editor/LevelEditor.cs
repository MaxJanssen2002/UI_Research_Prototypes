using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    public GameObject sphereInstance;
    private Vector3 spawnPosition;
    public Material transparent;
    private GameObject transparentObject;
    private GameObject selectedObject;



    [MenuItem("Window/UI Toolkit/Level Editor")]

    public static void ShowWindow()
    {
        LevelEditor window = GetWindow<LevelEditor>();
        window.titleContent = new GUIContent("Mijn level editor");
    }


    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);

        //Call the event handlers
        SetupButtonHandler();
        SetupToggleHandler();
        SetupVector3Handler();
    }


    public void OnDisable()
    {
        if (transparentObject != null) {
            DestroyTransparentObject();
        }
    }


    void DestroyTransparentObject()
    {
        DestroyImmediate(transparentObject);
    }



//-----------------------------------Event Handlers for button clicks-----------------------------------------------------

    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterButton);
    }

    private void RegisterButton(Button button)
    {
        button.RegisterCallback<ClickEvent>(HandleButton);
    }

    private void HandleButton(ClickEvent evt)
    {
        VisualElement root = rootVisualElement;

        Button button = evt.currentTarget as Button;

        if (button.name == "SphereSpawner") {
            SpawnSphere();
        }
    }


    private void SpawnSphere() 
    {
        VisualElement root = rootVisualElement;
        Toggle toggle = root.Q<Toggle>("SphereSpawnToggle");

        if (toggle.value == true) 
        {
            GameObject newSphere = Instantiate(sphereInstance, spawnPosition, Quaternion.identity);
            newSphere.name = "Sphere";
            Renderer sphereRenderer = newSphere.GetComponent<Renderer>();
        }
    }



//-----------------------------------Event Handlers for Toggle fields-----------------------------------------------------


    void SetupToggleHandler() 
    {
        VisualElement root = rootVisualElement;
        Toggle toggle = root.Q<Toggle>("SphereSpawnToggle");

        toggle.RegisterCallback<ChangeEvent<bool>>(ChangeSphereToggle);
    }


    void ChangeSphereToggle(ChangeEvent<bool> evt)
    {
        VisualElement root = rootVisualElement;
        Toggle toggle = evt.currentTarget as Toggle;

        if (toggle.value) {
            CreateTransparentSphere();
        }
        else {
            DestroyTransparentObject();
        }
    }


    void CreateTransparentSphere()
    {
        transparentObject = Instantiate(sphereInstance, spawnPosition, Quaternion.identity);
        transparentObject.name = "Transparent Sphere";
        transparentObject.GetComponent<Renderer>().material = transparent;
    }



//-----------------------------------Event Handlers for Vector3 fields----------------------------------------------------

    private void SetupVector3Handler() 
    {
        VisualElement root = rootVisualElement;
        Vector3Field field = root.Q<Vector3Field>("SpherePosition");

        field.RegisterCallback<ChangeEvent<float>>(ChangeSpherePosition);
    }


    private void ChangeSpherePosition(ChangeEvent<float> evt) 
    {
        VisualElement root = rootVisualElement;
        Vector3Field field = evt.currentTarget as Vector3Field;

        spawnPosition = field.value;

        if (transparentObject != null) {
            transparentObject.transform.position = spawnPosition;
        }
    }
}
