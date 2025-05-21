#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(InfoAttribute))]
    public class InfoDrawer : DecoratorDrawer
    {
        private const float Padding = 34f;
        private const float IconSize = 34f;
        private const float VerticalPadding = 8f;

        public override void OnGUI(Rect position)
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null) 
                return;

            // Calculate heights
            float textHeight = EditorStyles.helpBox.CalcHeight(
                new GUIContent(infoAttribute.Message),
                position.width - Padding
            );

            float boxHeight = Mathf.Max(textHeight + VerticalPadding, IconSize + VerticalPadding);

            // Draw background box
            position.height = boxHeight;
            GUI.Label(position, GUIContent.none, EditorStyles.helpBox);

            // Calculate icon position (vertically centered)
            Rect iconPosition = new Rect(
                position.x + 4f,
                position.y + (boxHeight - IconSize) * 0.5f,
                IconSize,
                IconSize
            );

            // Draw icon
            GUI.Label(iconPosition, EditorGUIUtility.TrIconContent("console.infoicon"));

            // Calculate text area
            Rect textPosition = new Rect(position)
            {
                x = position.x + IconSize + 8f,
                y = position.y + 4f,
                width = position.width - IconSize - 12f,
                height = textHeight
            };

            // Draw text
            EditorGUI.LabelField(textPosition, infoAttribute.Message, EditorStyles.wordWrappedMiniLabel);
        }

        public override float GetHeight()
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null) return base.GetHeight();

            float textHeight = EditorStyles.helpBox.CalcHeight(
                new GUIContent(infoAttribute.Message),
                EditorGUIUtility.currentViewWidth - Padding);

            // Ensure minimum height accommodates icon with padding
            return Mathf.Max(textHeight + VerticalPadding, IconSize + VerticalPadding);
        }
    }
}
#endif