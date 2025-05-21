#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(InfoAttribute))]
    public class InfoDrawer : DecoratorDrawer
    {
        private const float Padding = 34f;
        private const float VerticalPadding = 8f;

        private float _iconSize;

        public override void OnGUI(Rect position)
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null) 
                return;

            _iconSize = infoAttribute.IconSize;

            EditorGUI.HelpBox(position, infoAttribute.Message, infoAttribute.Type);
        }

        public override float GetHeight()
        {
            var infoAttribute = attribute as InfoAttribute;
            if (infoAttribute == null) return base.GetHeight();

            float textHeight = EditorStyles.helpBox.CalcHeight(
                new GUIContent(infoAttribute.Message),
                EditorGUIUtility.currentViewWidth - Padding);

            return Mathf.Max(textHeight + VerticalPadding, _iconSize + VerticalPadding);
        }
    }
}
#endif