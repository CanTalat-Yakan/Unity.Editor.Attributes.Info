using UnityEngine;

namespace UnityEssentials
{
    public enum MessageType { None, Info, Warning, Error }

    /// <summary>
    /// Specifies metadata to display an informational message in the Unity Inspector.
    /// </summary>
    /// <remarks>This attribute is used to annotate fields in Unity scripts, allowing developers to display
    /// custom messages in the Inspector. The message can include an optional icon and can be styled based on the
    /// specified message type (e.g., Info, Warning, or Error).</remarks>
    public class InfoAttribute : PropertyAttribute
    {
        public MessageType Type;
        public string Message;
        public int IconSize;

        public InfoAttribute(MessageType type = MessageType.Info, int iconSize = 32)
        {
            Type = type;
            IconSize = iconSize;
        }

        public InfoAttribute(string message, MessageType type = MessageType.Info, int iconSize = 32)
        {
            Type = type;
            Message = message;
            IconSize = iconSize;
        }
    }
}
