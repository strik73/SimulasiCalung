using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BermainManger : MonoBehaviour
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
    public SongManager songManager;
    public BermainMode bermainMode;
    private HashSet<GameObject> touchedParts = new HashSet<GameObject>();
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                TapInput(touch.position);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchedParts.Clear();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            TapInput(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchedParts.Clear();
        }
    }

    void TapInput(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int j = 0; j < calungParts.Length; j++)
            {
                GameObject partObject = calungParts[j].part;

                if (hit.collider.gameObject == partObject && calungParts[j].sound != null && !touchedParts.Contains(partObject))
                {
                    audioSource.PlayOneShot(calungParts[j].sound);
                    StartCoroutine(BrightnessEffect(calungParts[j]));
                    touchedParts.Add(partObject);

                    if (bermainMode != null)
                    {
                        bermainMode.OnTap(j);
                    }

                    break;
                }
            }
        }
    }

    IEnumerator BrightnessEffect(CalungPart calungPart)
    {
        if (calungPart.partRenderer != null)
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            Renderer renderer = calungPart.partRenderer;

            renderer.GetPropertyBlock(propertyBlock);
            Color originalColor = calungPart.originalMaterial.color;

            Color brightColor = originalColor * brightnessMultiplier;
            brightColor.a = originalColor.a;

            propertyBlock.SetColor("_Color", brightColor);
            renderer.SetPropertyBlock(propertyBlock);

            yield return new WaitForSeconds(glowDuration);

            propertyBlock.SetColor("_Color", originalColor);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
