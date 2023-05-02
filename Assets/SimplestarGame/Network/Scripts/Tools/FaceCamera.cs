using UnityEngine;

namespace SimplestarGame
{
    public class FaceCamera : MonoBehaviour
    {
        void Start()
        {
            this.mainCameraTransform = Camera.main.transform;
        }

        void LateUpdate()
        {
            this.transform.LookAt(this.transform.position + this.mainCameraTransform.rotation * Vector3.forward, this.mainCameraTransform.rotation * Vector3.up);
        }

        Transform mainCameraTransform;
    }
}