using UnityEngine;

namespace Volorf.VRLogger
{
    [CreateAssetMenu(fileName = "Logger Settings", menuName = "Create Logger Settings")]
    public class LoggerSettings : ScriptableObject
    {
        public bool clearLogForNewEntry;
        public bool startNewEntryWithNewLine;
        
        [Space(8)]
        [Header("Style")]
        public Color backgroundColor;
        public Color textColor;
    }
}
