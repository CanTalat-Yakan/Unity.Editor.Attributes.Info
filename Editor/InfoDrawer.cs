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
    public class InfoDrawer : DecoratorDrawer
    {
        private const float Padding = 34f;
        private const float VerticalPadding = 8f;

        private float _iconSize;

        /// <summary>
        /// Renders a custom GUI element in the specified position.
        /// </summary>
        /// <remarks>This method uses the <see cref="InfoAttribute"/> associated with the field to display
        /// a help box with a message, icon, and type. If the attribute is not present or invalid, the method does
        /// nothing.</remarks>
        public override void OnGUI(Rect position)
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null)
                return;

            _iconSize = infoAttribute.IconSize;

            EditorGUI.HelpBox(position, infoAttribute.Message, infoAttribute.Type);
        }

        /// <summary>
        /// Calculates the height required to display the content, including padding and icon size.
        /// </summary>
        /// <remarks>If the associated attribute is not of type <see cref="InfoAttribute"/>, the base
        /// implementation  is used to determine the height. Otherwise, the height is calculated based on the message 
        /// provided by the <see cref="InfoAttribute"/> and the current editor view width.</remarks>
        /// <returns>The height, in pixels, required to render the content. This value is determined by the  message text height
        /// or the icon size, whichever is greater, plus vertical padding.</returns>
        public override float GetHeight()
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null)
                return base.GetHeight();

            var textHeight = EditorStyles.helpBox.CalcHeight(
                new GUIContent(infoAttribute.Message),
                EditorGUIUtility.currentViewWidth - Padding);

            return Mathf.Max(textHeight + VerticalPadding, _iconSize + VerticalPadding);
        }
    }
}
#endif