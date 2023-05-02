using UnityEngine;

namespace SimplestarGame
{
    public class FPSCounter : MonoBehaviour
    {
        void Update()
        {
            if (SceneContext.Instance.fpsText == null)
            {
                return;
            }
            this.deltaTime += (Time.unscaledDeltaTime - this.deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            SceneContext.Instance.fpsText.text = "FPS: " + fps.ToString("00");
        }

        float deltaTime = 0.0f;
    }
}