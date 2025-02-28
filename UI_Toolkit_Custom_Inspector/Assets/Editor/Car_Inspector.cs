using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(Car))]
public class Car_Inspector : Editor
{
    public VisualTreeAsset m_InspectorXML;


    public override VisualElement CreateInspectorGUI()
    {
        VisualElement myInspector = new VisualElement();

        

        m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Car_Inspector_UXML.uxml");
        myInspector = m_InspectorXML.Instantiate();

        myInspector.Add(new Label("This is a custom Inspector"));

        return myInspector;
    }
}