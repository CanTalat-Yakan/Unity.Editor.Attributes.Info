using UnityEngine;

namespace UnityEssentials
{
    public class InfoAttribute : PropertyAttribute
    {
        public string Message;

        public InfoAttribute(string message)
        {
            Message = message;
        }
    }
}
