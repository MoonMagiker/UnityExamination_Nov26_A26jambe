using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [Tooltip("0 = snap instantly. Higher values = more lag.")]
    [SerializeField, Range(0f, 1f)] private float lag = 0.15f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desired = target.position + offset;
        Vector3 current = transform.position;

        if (!followX)
        {
            desired.x = current.x;
        }

        if (!followY)
        {
            desired.y = current.y;
        }

        if (lag <= 0f)
        {
            transform.position = desired;
            return;
        }

        // Higher lag = slower catch-up. Roughly maps 0–1 into a useful smooth factor.
        float smoothTime = Mathf.Lerp(0.01f, 0.5f, lag);
        transform.position = Vector3.Lerp(current, desired, 1f - Mathf.Exp(-Time.deltaTime / smoothTime));
    }
}
