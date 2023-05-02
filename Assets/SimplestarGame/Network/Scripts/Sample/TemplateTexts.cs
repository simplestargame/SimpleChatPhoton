using UnityEngine;

namespace SimplestarGame
{
    public class TemplateTexts : MonoBehaviour
    {
        [SerializeField] ButtonPressDetection buttonHi;
        [SerializeField] ButtonPressDetection buttonHello;
        [SerializeField] ButtonPressDetection buttonGood;
        [SerializeField] ButtonPressDetection buttonOK;
        [SerializeField] TMPro.TMP_InputField inputField;

        void Start()
        {
            this.buttonHi.onReleased += this.OnClickHi;
            this.buttonHello.onReleased += this.OnClickHello;
            this.buttonGood.onReleased += this.OnClickGood;
            this.buttonOK.onReleased += this.OnClickOK;
        }

        void OnClickOK()
        {
            this.inputField.text = "OK!";
        }

        void OnClickGood()
        {
            this.inputField.text = "Good!";
        }

        void OnClickHello()
        {
            this.inputField.text = "Hello.";
        }

        void OnClickHi()
        {
            this.inputField.text = "Hi.";
        }
    }
}