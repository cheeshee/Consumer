using System;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DraggablePoint : PropertyAttribute { }


#if UNITY_EDITOR
[CustomEditor(typeof(MonoBehaviour), true)]
public class DraggablePointDrawer : Editor
{

    readonly GUIStyle style = new GUIStyle();

    void OnEnable()
    {
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;
    }

    public void OnSceneGUI()
    {
        SerializedProperty property = serializedObject.GetIterator();
        while (property.Next(true))
        {
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                handleVectorProperty(property);
            }
            else if (property.isArray)
            {
                for (int x = 0; x < property.arraySize; x++)
                {
                    SerializedProperty element = property.GetArrayElementAtIndex(x);
                    if (element.propertyType != SerializedPropertyType.Vector2)
                    {
                        //Break early if we're not an array of Vector2
                        break;
                    }
                    handleVectorPropertyInArray(element, property, x);
                }
            }
        }
    }

    void handleVectorProperty(SerializedProperty property)
    {
        FieldInfo field = serializedObject.targetObject.GetType().GetField(property.name);
        if (field == null)
        {
            return;
        }
        var draggablePoints = field.GetCustomAttributes(typeof(DraggablePoint), false);
        if (draggablePoints.Length > 0)
        {
            Handles.Label(property.vector2Value, property.name);
            property.vector2Value = Handles.PositionHandle(property.vector2Value, Quaternion.identity);
            serializedObject.ApplyModifiedProperties();
        }
    }

    void handleVectorPropertyInArray(SerializedProperty property, SerializedProperty parent, int index)
    {
        FieldInfo parentfield = serializedObject.targetObject.GetType().GetField(parent.name);
        if (parentfield == null) //Check parent is not null
        {
            return;
        }
        var draggablePoints = parentfield.GetCustomAttributes(typeof(DraggablePoint), false);
        if (draggablePoints.Length > 0)
        {
            Handles.Label(property.vector2Value, parent.name + "[" + index + "]"); //Print the Array field name and its index
            property.vector2Value = Handles.PositionHandle(property.vector2Value, Quaternion.identity);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif