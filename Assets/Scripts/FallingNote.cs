using System.Collections;
using TMPro;
using UnityEngine;

public class FallingNote : MonoBehaviour
{
    public int expectedTapIndex;
    public float speed;
    private RectTransform rectTransform;
    public float hitX;
    private BermainMode bermainMode;
    private bool wasHit = false;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        bermainMode = FindObjectOfType<BermainMode>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);

        if (rectTransform.anchoredPosition.x > hitX + 1200f)
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
