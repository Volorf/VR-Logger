using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Volorf.VRLogger;
using Volorf.FollowHead;

namespace Volorf.Logger
{
    [RequireComponent(typeof(FollowHead.FollowHead))]
    public class Logger : MonoBehaviour
    {
        [SerializeField] private LoggerSettings settings;
        
        [Space(8)]
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image background;
        
        private string _message = "";
        private bool _clearLogForNewEntry;
        private bool _startNewEntryWithNewLine;
        
        
        public static Logger Instance
        {
            get
            {
                return _logger;
            }
        }

        private static Logger _logger;

        private int counter = 0;

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
            if (settings != null)
            {
                background.color = settings.backgroundColor;
                label.color = settings.textColor;
                _clearLogForNewEntry = settings.clearLogForNewEntry;
                _startNewEntryWithNewLine = settings.startNewEntryWithNewLine;
            }
            else
            {
                Debug.LogError("The instance of Logger doesn't have Logger Settings");
                background.color = Color.magenta;
                label.color = Color.yellow;
                label.text = "No Logger Settings";
                _clearLogForNewEntry = false;
                _startNewEntryWithNewLine = true;
            }
        }

        [ContextMenu("Test AddText()")]
        public void TestAddText()
        {
            counter++;
            AddText("It works. " + counter);
        }
        
        public void AddText(string text)
        {
            if (_clearLogForNewEntry) Clear();

            string oldMessage = _message;
            _message = text;
            if (_startNewEntryWithNewLine) _message += "\n";
            _message += oldMessage;
            label.text = _message;
        }

        public void Clear() => _message = "";

        public void SetClearLogForNewEntry(bool b) => _clearLogForNewEntry = b;
    }
}

