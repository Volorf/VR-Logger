using System.Collections;
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
        [SerializeField] private bool useCaretAnimation = true;

        [Space(8)] [Header("Style")] [SerializeField]
        private Color backgroundColor;

        [SerializeField] private Color textColor;

        [Space(8)] [Header("UI Elements")] [SerializeField]
        private TextMeshProUGUI label;

        [SerializeField] private Image background;

        private string _message = "";
        
        // Caret Stuff
        private string _caret = "|";
        private bool _canAddCaret = true;

        public static Logger Instance
        {
            get { return _logger; }
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

            _message = label.text;
            if (useCaretAnimation) StartCoroutine(CaretAnimation());
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
            _canAddCaret = false;
            if (clearLogForNewEntry) Clear();

            string tempMessage = "";
            if (lineAsEntrySeparator)
            {
                if (useCaretAnimation)
                {
                    _message = RemoveCaretAtStartIfItThere(_message, _caret);
                    tempMessage = "\n" + text;
                }
                else
                {
                    tempMessage = text + "\n";
                }
                _message = tempMessage + _message;
            }
            else
            {
                _message += text;
            }

            label.text = _message;
            _canAddCaret = true;
        }

        public void Clear() => _message = "";

        public void SetClearLogForNewEntry(bool b) => clearLogForNewEntry = b;
        

        private IEnumerator CaretAnimation()
        {
            while (true)
            {
                if (lineAsEntrySeparator)
                {
                    string processedStr = RemoveCaretAtStartIfItThere(_message, _caret);

                    if (_message.Length == processedStr.Length)
                    {
                        string tempMessage = _message;
                        
                        if (_canAddCaret) _message = _caret  + tempMessage;
                        
                    }
                    else
                    {
                        _message = processedStr;
                    }
                }
                else
                {
                    string processedStr = RemoveCaretAtEndIfItThere(_message, _caret);

                    if (_message.Length == processedStr.Length)
                    {
                        if (_canAddCaret) _message += _caret;
                    }
                    else
                    {
                        _message = processedStr;
                    }
                }
                
                label.text = _message;

                yield return new WaitForSeconds(0.5f);
            }
        }

        // TODO: Unify the methods
        private string RemoveCaretAtEndIfItThere(string message, string caret)
        {
            string str = message;
            int l = str.Length;
            
            if (l == 0) return str;
            
            string lastLetter = str.Substring(l - 1, 1);

            if (lastLetter == caret)
            {
                str = str.Substring(0, l - 1);
            }

            return str;
        }

        private string RemoveCaretAtStartIfItThere(string message, string caret)
        {
            string str = message;
            int l = str.Length;

            if (l == 0) return str;

            string firstLetter = str.Substring(0, 1);

            if (firstLetter == caret)
            {
                str = str.Substring(1, l - 1);
            }

            return str;
        }
    }
}

