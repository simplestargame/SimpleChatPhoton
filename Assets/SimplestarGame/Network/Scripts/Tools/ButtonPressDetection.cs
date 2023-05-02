using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace SimplestarGame
{
    public class ButtonPressDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        internal Action onPressed;
        internal Action onReleased;
        internal bool IsPressed { get; private set; } = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            this.IsPressed = true;
            this.onPressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.IsPressed = false;
            this.onReleased?.Invoke();
        }
    }
}