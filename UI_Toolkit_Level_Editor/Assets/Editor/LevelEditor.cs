using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    public GameObject sphereInstance;
    public GameObject transparentSpherePrefab;
    private GameObject transparentSphereInstance;
    private GameObject selectedObject;
    private Vector3 spawnPosition;
    public int counter;

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
        createTransparentSphere();

        //Call the event handlers
        SetupButtonHandler();
        SetupVector3Handler();
    }


    void createTransparentSphere()
    {
        transparentSphereInstance = Instantiate(transparentSpherePrefab, spawnPosition, Quaternion.identity);
        transparentSphereInstance.name = "Transparent Sphere";
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


    private void SpawnSphere() {
        ++counter;
        GameObject newSphere = Instantiate(sphereInstance, spawnPosition, Quaternion.identity);
        newSphere.name = "Sphere";
        Renderer sphereRenderer = newSphere.GetComponent<Renderer>();
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

        

        if (transparentSphereInstance != null) {
            transparentSphereInstance.transform.position = spawnPosition;
        }
    }
}
