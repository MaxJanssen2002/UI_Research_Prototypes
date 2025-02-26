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
        SimpleCustomEditor wnd = GetWindow<SimpleCustomEditor>();
        wnd.titleContent = new GUIContent("Een eigen editor");
    }


    private int counter = 0;

    private const string buttonPrefix = "button";

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

// Option 1:
        // Instantiate UXML (from the UI Builder). This brings in the button1.
        // The UI Builder can be found in the Unity Editor: Window > UI Toolkit > UI Builder
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);


// Option 2:
        // Import UXML created manually (The SimpleCustomEditor_UXML.uxml in the same directory as this file)
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Handwritten_UXML.uxml");
        VisualElement myLabelFromUXML = visualTree.Instantiate();
        root.Add(myLabelFromUXML);


// Option 3:
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

        // Make my own VisualElement (in this case a red block with text and a textfield in it)
        var myElement = new VisualElement();
        myElement.Add(new Label("A label in a visualElement"));
        myElement.Add(new Label("A second label in the same visualElement"));
        TextField myTextfield = new TextField();
        myTextfield.name = "textfield2";
        myTextfield.label = "Insert something";
        myElement.Add(myTextfield);
        myElement.style.backgroundColor = Color.red;
        myElement.style.width = 300;
        myElement.style.height = 150;
        root.Add(myElement);


        //Call the event handler
        SetupButtonHandler();
        SetupTextHandler();
        SetupKeyHandler();
        SetupSliderHandler();
    }



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

        // Option 2: Ask for the target of the event
        TextField field = evt.currentTarget as TextField;


        Debug.Log("Value of '" + field.name + "' set to '" + evt.newValue + "'");
    }


    private void SetupKeyHandler()
    {
        VisualElement root = rootVisualElement;
        root.Q<TextField>().Focus();
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


    private void SetupSliderHandler() 
    {
        VisualElement root = rootVisualElement;

        Slider slider = root.Q<Slider>("slider1");

        slider.RegisterCallback<ChangeEvent<int>>(PrintIntSliderMessage);
    }


    private void PrintIntSliderMessage(ChangeEvent<int> evt) 
    {
        VisualElement root = rootVisualElement;
        Slider slider = evt.currentTarget as Slider;

        Debug.Log("Value of '" + slider.name + "' set to '" + evt.newValue + "'");
    }
}
