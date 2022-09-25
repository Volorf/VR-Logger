using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Volorf.VRLogger
{
    [RequireComponent(typeof(FollowHead.FollowHead))]
    public class Logger : MonoBehaviour
    {
        [Header("New Entry")]
        [SerializeField] private bool clearLogForNewEntry = false;
        [SerializeField] private bool lineAsEntrySeparator = true;
        
        [Space(8)]
        [Header("Style")]
        [SerializeField] private Color backgroundColor;
        [SerializeField] private Color textColor;

        [Space(8)]
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image background;
        
        private string _message = "";
        
        public static Logger Instance
        {
            get
            {
                return _logger;
            }
        }

        private static Logger _logger;

        private int _counter = 0;

        private void Awake()
        {
            if (_logger != null && _logger != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _logger = this;
            }
        }

        private void Start()
        {
            background.color = backgroundColor;
            label.color = textColor;
        }

        [ContextMenu("Display Test Entry")]
        public void DisplayTestEntry()
        {
            _counter++;
            AddText("Entry #" + _counter);
        }
        
        public void AddText(string text)
        {
            if (clearLogForNewEntry) Clear();

            string tempMessage = _message;
            if (lineAsEntrySeparator)
            {
                tempMessage += "\n";
                _message = text;
                _message += tempMessage;
            }
            else
            {
                tempMessage += text;
                _message = tempMessage;
            }
  
            label.text = _message;
        }

        public void Clear() => _message = "";

        public void SetClearLogForNewEntry(bool b) => clearLogForNewEntry = b;
    }
}

