#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials.Repositories.Unity.Editor.Attributes.Info.Editor
{
    /// <summary>
    /// Provides a custom property drawer for the <see cref="InfoAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Draws a HelpBox above the default field drawing for non-string properties. The message is taken from the
    /// attribute constructor if provided; if not provided and the property is a string, the current string value is
    /// used as the message. For string properties, the default field is hidden unless the attribute constructor message
    /// is explicitly set; in that case we draw HelpBox + the string field.
    /// </remarks>
    [CustomPropertyDrawer(typeof(InfoAttribute))]
    public class InfoDrawer : PropertyDrawer
    {
        private const float Padding = 34f;
        private const float VerticalPadding = 8f;
        private const float FieldSpacing = 4f;

        private float _iconSize;
        private MessageType _messageType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null)
                return;

            _iconSize = infoAttribute.IconSize;

            // Resolve message: prefer attribute message; otherwise, if string property, use its value
            string message = infoAttribute.Message;
            bool isString = property.propertyType == SerializedPropertyType.String;
            bool attributeHasMessage = !string.IsNullOrWhiteSpace(message);
            if (!attributeHasMessage && isString)
            {
                message = (property.stringValue ?? string.Empty);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    if (message.StartsWith("Error")) _messageType = MessageType.Error;
                    else if (message.StartsWith("Warning")) _messageType = MessageType.Warning;
                    else _messageType = infoAttribute.Type;
                }
            }
            else
            {
                _messageType = infoAttribute.Type;
            }

            // Compute rectangles
            var indented = EditorGUI.IndentedRect(position);

            float y = position.y;
            bool hasMessage = !string.IsNullOrWhiteSpace(message);
            if (hasMessage)
            {
                float helpHeight = Mathf.Max(
                    EditorStyles.helpBox.CalcHeight(new GUIContent(message), indented.width) + VerticalPadding,
                    _iconSize + VerticalPadding);

                var helpRect = new Rect(indented.x, y, indented.width, helpHeight);
                EditorGUI.HelpBox(helpRect, message, (UnityEditor.MessageType)_messageType);
                y += helpHeight + FieldSpacing;
            }

            // Draw the default property field
            // - Non-string: always draw the default field
            // - String: draw only if attribute message was explicitly set
            bool shouldDrawField = !isString || attributeHasMessage;
            if (shouldDrawField)
            {
                float fieldHeight = EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
                var fieldRect = new Rect(position.x, y, position.width, fieldHeight);
                EditorGUI.PropertyField(fieldRect, property, label, includeChildren: true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null) return base.GetPropertyHeight(property, label);

            bool isString = property.propertyType == SerializedPropertyType.String;

            // Resolve message for height calculation (trimmed)
            string msg = infoAttribute.Message;
            bool attrHasMessage = !string.IsNullOrWhiteSpace(msg);
            if (!attrHasMessage && isString)
            {
                msg = (property.stringValue ?? string.Empty).Trim();
            }

            bool hasMessage = !string.IsNullOrWhiteSpace(msg);

            // HelpBox height (if any)
            float helpHeight = 0f;
            if (hasMessage)
            {
                float viewWidth = EditorGUIUtility.currentViewWidth - Padding;
                float textHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(msg), viewWidth);
                helpHeight = Mathf.Max(textHeight + VerticalPadding, infoAttribute.IconSize + VerticalPadding) + FieldSpacing;
            }

            // For string without explicit attribute message, we hide the field; if no message, hide everything
            if (isString && !attrHasMessage)
            {
                return hasMessage ? helpHeight : 0f;
            }

            float fieldHeight = EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
            return helpHeight + fieldHeight;
        }
    }
}
#endif