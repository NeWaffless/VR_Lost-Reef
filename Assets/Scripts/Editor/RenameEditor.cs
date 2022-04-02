using UnityEngine;
using UnityEditor;

// script borrowed from https://answers.unity.com/questions/1487864/change-a-variable-name-only-on-the-inspector.html
[CustomPropertyDrawer(typeof(RenameAttribute))]
public class RenameEditor : PropertyDrawer
{
    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
    {
        EditorGUI.PropertyField(position, property, new GUIContent( (attribute as RenameAttribute).NewName ));
    }
}