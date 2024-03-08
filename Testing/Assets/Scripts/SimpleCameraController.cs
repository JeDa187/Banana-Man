using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityTemplateProjects
{
    public class SimpleCameraController : MonoBehaviour
    {
        public Transform target; // Target to follow
        public Vector3 offset; // Offset from the target

        [Header("Movement Settings")]
        public float positionLerpTime = 0.2f;

        [Header("Rotation Settings")]
        public float rotationLerpTime = 0.01f;
        public float rotationSpeed = 5.0f;

        private CameraState m_TargetCameraState = new CameraState();
        private CameraState m_InterpolatingCameraState = new CameraState();

        private class CameraState
        {
            public float yaw;
            public float pitch;
            public Vector3 position;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                position = t.position;
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                position = Vector3.Lerp(position, target.position, positionLerpPct);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, 0.0f);
                t.position = position;
            }
        }

        private void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);
        }

        private void Update()
        {
            // Translation
            Vector3 targetPosition = target.position + offset;
            m_TargetCameraState.position = targetPosition;

            // Rotation - removed the condition, now it always updates
            Vector2 input = GetInputLookRotation() * rotationSpeed;
            m_TargetCameraState.yaw += input.x;
            m_TargetCameraState.pitch -= input.y; // Inverted pitch for natural mouse-look
            m_TargetCameraState.pitch = Mathf.Clamp(m_TargetCameraState.pitch, -30f, 45f); // Keep the pitch within reasonable bounds

            // Framerate-independent interpolation
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

            m_InterpolatingCameraState.UpdateTransform(transform);
        }

        private Vector2 GetInputLookRotation()
        {
            // Always return the mouse movement regardless of any buttons pressed
            return Mouse.current != null ? Mouse.current.delta.ReadValue() : new Vector2();
        }
    }
}
