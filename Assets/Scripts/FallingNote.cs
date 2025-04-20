using System.Collections;
using TMPro;
using UnityEngine;

public class FallingNote : MonoBehaviour
{
    public int expectedTapIndex;
    public float speed;
    private RectTransform rectTransform;
    public float hitY;
    private BermainMode bermainMode;
    private bool wasHit = false;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        bermainMode = FindObjectOfType<BermainMode>();
    }

    void Update()
    {
        rectTransform.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);

        if (rectTransform.anchoredPosition.y < hitY - 200f)
        {
            if (!wasHit)
            {
                bermainMode.MissedNote();
            }
            Destroy(gameObject);
        }
    }

    public void MarkAsHit()
    {
        wasHit = true;
    }
}
