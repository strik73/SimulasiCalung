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
        public Color glowColor = Color.yellow; // Glow color
        public float glowIntensity = 5f; // Intensity of the glow
    }

    public CalungPart[] calungParts = new CalungPart[14];
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
                    StartCoroutine(GlowPart(calungParts[j].part, calungParts[j].glowColor, calungParts[j].glowIntensity));
                    break;
                }
            }
        }
    }

    IEnumerator GlowPart(GameObject part, Color glowColor, float intensity)
    {
        Renderer rend = part.GetComponent<Renderer>();
        if (rend != null)
        {
            Material mat = rend.material;

            // Enable emission and set glow color
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", glowColor * intensity);

            // Wait for a short duration
            yield return new WaitForSeconds(0.2f);

            // Disable emission
            mat.DisableKeyword("_EMISSION");
        }
    }
}