using UnityEngine;

public class ResponsiveCamera3D : MonoBehaviour
{
    public float referenceAspect = 16f / 9f; 
    public float referenceFOV = 62f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraFOV();
    }

    void UpdateCameraFOV()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float targetFOV = referenceFOV * (referenceAspect / currentAspect);
        cam.fieldOfView = targetFOV;
    }

    void Update()
    {
        if (Screen.width != Screen.height)
        {
            UpdateCameraFOV();
        }
    }
}