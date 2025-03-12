using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    public GameObject sphereInstance;

    [MenuItem("Window/UI Toolkit/Level Editor")]


    public static void ShowWindow()
    {
        LevelEditor window = GetWindow<LevelEditor>();
        window.titleContent = new GUIContent("Mijn level editors");
    }


    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML (from the UI Builder).
        // The UI Builder can be found in the Unity Editor: Window > UI Toolkit > UI Builder
        // The generated UXML file from the UI Toolkit is 'LevelEditor.uxml' (in the same directory as this file)
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        //Call the event handlers
        SetupButtonHandler();
        // SetupTextHandler();
        // SetupKeyHandler();
        // SetupSliderHandler();
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

        Debug.Log("Button with name '" + button.name + "' was clicked!");

        if (button.name == "SphereSpawner") {
            SpawnSphere();
        }
    }


    private void SpawnSphere() {
        Instantiate(sphereInstance, new Vector3(0, 1, 0), Quaternion.identity);
    }



//-----------------------------------Event Handlers for text fields-------------------------------------------------------

    private void SetupTextHandler()
    {
        VisualElement root = rootVisualElement;
        
        TextField field = root.Q<TextField>("textfield1");


        // Option 1: Make a lambda function for your event
        // field.RegisterCallback<ChangeEvent<string>>((evt) =>
        // {
        //     Debug.Log("Value of '" + field.name + "' set from '" + evt.oldValue + "' to '" + evt.newValue + "'");
        // });

        // Option 2: Call a function for your event
        field.RegisterCallback<ChangeEvent<string>>(PrintTextMessage);


    }


    private void PrintTextMessage(ChangeEvent<string> evt)
    {
        VisualElement root = rootVisualElement;


        // Option 1: Ask for the name of the textfield
        //TextField field = root.Q<TextField>("textfield1");

        // Option 2: Ask for the target of the event (which is also the textfield)
        TextField field = evt.currentTarget as TextField;


        // Option 1: Log the new value given by the event
        //Debug.Log("Value of '" + field.name + "' set to '" + evt.newValue + "'");

        // Option 2: Log the current value of the variable you just changed
        Debug.Log("Value of '" + field.name + "' set to '" + field.value + "'");
    }



//-----------------------------------Event Handlers for keyboard input----------------------------------------------------

    private void SetupKeyHandler()
    {
        VisualElement root = rootVisualElement;
        root.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyUpEvent>(OnKeyUp, TrickleDown.TrickleDown);
    }


    private void OnKeyDown(KeyDownEvent evt)
    {
        //Debug.Log("KeyDown, keyCode = " + evt.keyCode + ", character = " + evt.character + ", modifiers = " + evt.modifiers);
    }


    private void OnKeyUp(KeyUpEvent evt)
    {
        //Debug.Log("KeyUp, keyCode = " + evt.keyCode + ", character = " + evt.character + ", modifiers = " + evt.modifiers);
    }



//-----------------------------------Event Handlers for sliders-----------------------------------------------------------

    private void SetupSliderHandler() 
    {
        VisualElement root = rootVisualElement;

        SliderInt intSlider = root.Q<SliderInt>("slider1");
        Slider floatSlider = root.Q<Slider>("slider2");

        intSlider.RegisterCallback<ChangeEvent<int>>(PrintIntSliderMessage);
        floatSlider.RegisterCallback<ChangeEvent<float>>(PrintFloatSliderMessage);
    }


    private void PrintIntSliderMessage(ChangeEvent<int> evt) 
    {
        VisualElement root = rootVisualElement;
        SliderInt slider = evt.currentTarget as SliderInt;

        Debug.Log("Value of '" + slider.name + "' set to '" + slider.value + "'");
    }


    private void PrintFloatSliderMessage(ChangeEvent<float> evt) 
    {
        VisualElement root = rootVisualElement;
        Slider slider = evt.currentTarget as Slider;

        Debug.Log("Value of '" + slider.name + "' set to '" + slider.value + "'");
    }
}
