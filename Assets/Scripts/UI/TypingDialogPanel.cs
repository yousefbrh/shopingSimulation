using TMPro;
using UnityEngine;

namespace UI
{
    public class TypingDialogPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TypeWriterUI typeWriterUI;

        public void Initialize(string dialogText)
        {
            text.text = dialogText;
        }
    }
}
