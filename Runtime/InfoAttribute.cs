using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    public class InfoAttribute : PropertyAttribute
    {
        public MessageType Type;
        public string Message;
        public int IconSize;

        public InfoAttribute(string message, MessageType type = MessageType.Info, int iconSize = 32)
        {
            Type = type;
            Message = message;
            IconSize =iconSize;
        }
    }
}
