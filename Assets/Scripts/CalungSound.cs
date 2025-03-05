using UnityEngine;
using System.Collections;

public class CalungSound : MonoBehaviour
{
    [System.Serializable]
    public class CalungPart
    {
        public string name;
        public AudioClip sound;
        public GameObject part;
        public Renderer partRenderer;
        public Material originalMaterial;
    }

    public CalungPart[] calungParts = new CalungPart[14];
    private AudioSource audioSource;
    public Color glowColor = Color.white;
    public float glowDuration = 0.2f;
    public float brightnessMultiplier = 2f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Ensure each part has a Renderer component
        foreach (var part in calungParts)
        {
            if (part.part != null)
            {
                part.partRenderer = part.part.GetComponent<Renderer>();
                if (part.partRenderer != null)
                {
                    part.originalMaterial = part.partRenderer.material;
                }
            }
        }
    }

    void Update()
    {
        // Handle touch input (mobile)
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    ProcessTap(touch.position);
                }
            }
        }

        // Handle mouse input (PC)
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            ProcessTap(Input.mousePosition);
        }
    }

    void ProcessTap(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int j = 0; j < calungParts.Length; j++)
            {
                if (hit.collider.gameObject == calungParts[j].part && calungParts[j].sound != null)
                {
                    audioSource.PlayOneShot(calungParts[j].sound);
                    StartCoroutine(BrightnessEffect(calungParts[j]));
                    break;
                }
            }
        }
    }

    IEnumerator BrightnessEffect(CalungPart calungPart)
    {
        if (calungPart.partRenderer != null && calungPart.originalMaterial != null)
        {
            Material material = calungPart.partRenderer.material;
            Color originalColor = material.color;

            // Increase brightness by multiplying RGB values
            Color brightColor = originalColor * brightnessMultiplier;
            brightColor.a = originalColor.a; // Keep the original alpha

            material.color = brightColor;
            yield return new WaitForSeconds(glowDuration);
            material.color = originalColor;
        }
    }
}
