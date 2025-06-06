#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Provides a custom property drawer for the <see cref="InfoAttribute"/>.
    /// </summary>
    /// <remarks>This class is used to render a help box in the Unity Inspector for properties annotated with
    /// the <see cref="InfoAttribute"/>. The help box displays a message and an optional icon, with the height
    /// dynamically adjusted based on the content.</remarks>
    [CustomPropertyDrawer(typeof(InfoAttribute))]
    public class InfoDrawer : PropertyDrawer
    {
        private const float Padding = 34f;
        private const float VerticalPadding = 8f;

        private float _iconSize;
        private MessageType _messageType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null)
                return;

            _iconSize = infoAttribute.IconSize;

            if (property.propertyType == SerializedPropertyType.String)
            {
                infoAttribute.Message = property.stringValue;

                if (property.stringValue.StartsWith("Error"))
                    _messageType = MessageType.Error;
                else if (property.stringValue.StartsWith("Warning"))
                    _messageType = MessageType.Warning;
                else _messageType = infoAttribute.Type;
            }

            // Check if Message is a string before using it
            EditorGUI.HelpBox(position, infoAttribute.Message, (UnityEditor.MessageType)_messageType);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var infoAttribute = attribute as InfoAttribute;

            var textHeight = EditorStyles.helpBox.CalcHeight(
                new GUIContent(infoAttribute.Message),
                EditorGUIUtility.currentViewWidth - Padding);

            return Mathf.Max(textHeight + VerticalPadding, _iconSize + VerticalPadding);
        }
    }
}
#endif