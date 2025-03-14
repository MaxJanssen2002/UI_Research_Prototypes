using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleCustomEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;


    [MenuItem("Window/UI Toolkit/SimpleCustomEditor")]


    public static void ShowExample()
    {
        SimpleCustomEditor window = GetWindow<SimpleCustomEditor>();
        window.titleContent = new GUIContent("Een eigen editor");
    }


    private int counter = 0;

    private const string buttonPrefix = "button";

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

// Option 1:
        // Instantiate UXML (from the UI Builder).
        // The UI Builder can be found in the Unity Editor: Window > UI Toolkit > UI Builder
        // The generated UXML file from the UI Toolkit is 'SimpleCustomEditor.uxml' (in the same directory as this file)
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);


// Option 2:
        // Import manually created UXML (The Handwritten_UXML.uxml in the same directory as this file)
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Handwritten_UXML.uxml");
        VisualElement myLabelFromUXML = visualTree.Instantiate();
        root.Add(myLabelFromUXML);


// Option 3:
        // Make my own label in this C# code
        Label label = new Label();
        label.name = "label1";
        label.text = "The elements below were created with C# code";
        root.Add(label);

        // Make my own button in this C# code
        Button button = new Button();
        button.name = "button3";
        button.text = "This is Button3";
        button.style.backgroundColor = Color.blue;
        button.style.width = 200;
        button.style.height = 100;
        root.Add(button);

        // Make my own toggle/checkbox in this C# code
        Toggle toggle = new Toggle();
        toggle.name = "toggle3";
        toggle.label = "Show the number?";
        root.Add(toggle);


        //Call the event handlers
        SetupButtonHandler();
        SetupTextHandler();
        SetupSliderHandler();
        SetupVector2Handler();
    }



//-----------------------------------Event Handlers for button clicks-----------------------------------------------------

    //Functions as the event handlers for your button click and number counts
    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterButton);
    }

    private void RegisterButton(Button button)
    {
        button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
        VisualElement root = rootVisualElement;

        ++counter;

        // Because of the names we gave the buttons and toggles, we can use the button name to find the toggle name.
        // The names of the buttons are button1, button2 and button3.
        // The names of the toggles are toggle1, toggle2 and toggle3.
        Button button = evt.currentTarget as Button;
        string buttonNumber = button.name.Substring(buttonPrefix.Length);
        string toggleName = "toggle" + buttonNumber;
        Toggle toggle = root.Q<Toggle>(toggleName);

        if (toggle.value) {
            Debug.Log("Button with name '" + button.name + "' was clicked!" +  " Count: " + counter.ToString());
        }
        else {
            Debug.Log("Button with name '" + button.name + "' was clicked!");
        }
    }



//-----------------------------------Event Handlers for text fields-------------------------------------------------------

    private void SetupTextHandler()
    {
        VisualElement root = rootVisualElement;
        
        TextField field = root.Q<TextField>("textfield1");


        // Option 1: Make a lambda function for your event
        // field.RegisterCallback<ChangeEvent<string>>((evt) =>
        // {
        //     Debug.Log("Value of '" + field.name + "' set to '" + evt.newValue + "'");
        // });

        // Option 2: Call a function for your event
        field.RegisterCallback<ChangeEvent<string>>(PrintTextMessage);


    }


    private void PrintTextMessage(ChangeEvent<string> evt)
    {
        TextField field = evt.currentTarget as TextField;

        Debug.Log("Value of '" + field.name + "' set to '" + evt.newValue + "'");
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
        Debug.Log("KeyDown, keyCode = " + evt.keyCode + ", character = " + evt.character + ", modifiers = " + evt.modifiers);
    }


    private void OnKeyUp(KeyUpEvent evt)
    {
        Debug.Log("KeyUp, keyCode = " + evt.keyCode + ", character = " + evt.character + ", modifiers = " + evt.modifiers);
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



//-----------------------------------Event Handlers for Vector2 fields----------------------------------------------------

    private void SetupVector2Handler() 
    {
        VisualElement root = rootVisualElement;

        Vector2Field field = root.Q<Vector2Field>("vector2values");

        field.RegisterCallback<ChangeEvent<float>>(PrintVector2Message);
    }


    private void PrintVector2Message(ChangeEvent<float> evt) 
    {
        VisualElement root = rootVisualElement;
        Vector2Field field = evt.currentTarget as Vector2Field;

        Debug.Log("Value of '" + field.name + "' set to '" + field.value.x + "' and '" + field.value.y + "'");
    }
}
