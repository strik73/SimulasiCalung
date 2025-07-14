using System;
using UnityEngine;
using TMPro;

public class RotateObject : MonoBehaviour
{
    public float Speed = 10f;
    private bool isRotating = false;
    private Vector2 touchStartPosition;
    private bool rotateOnYAxis = false;
    public TextMeshProUGUI axis;
    [SerializeField] private GameObject rotateSprite;
    [SerializeField] private GameObject rotateVertSprite;

    void Start()
    {
        UpdateAxisText();
        rotateSprite.SetActive(true);
    }

    public void AxisToggle()
    {
        rotateOnYAxis = !rotateOnYAxis;
        UpdateAxisText();
        if (rotateOnYAxis)
        {
            rotateSprite.SetActive(false);
            rotateVertSprite.SetActive(true);
        }
        else
        {
            rotateSprite.SetActive(true);
            rotateVertSprite.SetActive(false);
        }
    }

    public void ZoomIn()
    {
        Vector3 newScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
        float maxScale = 2f;
        newScale.x = Mathf.Min(newScale.x, maxScale);
        newScale.y = Mathf.Min(newScale.y, maxScale);
        newScale.z = Mathf.Min(newScale.z, maxScale);
        transform.localScale = newScale;
    }

    public void ZoomOut()
    {
        Vector3 newScale = transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
        float minScale = 0.1f;
        newScale.x = Mathf.Max(newScale.x, minScale);
        newScale.y = Mathf.Max(newScale.y, minScale);
        newScale.z = Mathf.Max(newScale.z, minScale);
        transform.localScale = newScale;
    }



    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isRotating = true;
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isRotating = false;
            }

            if (isRotating && touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - touchStartPosition;

                if (rotateOnYAxis)
                {
                    transform.Rotate(Vector3.right, -touchDelta.y * Speed * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(Vector3.up, -touchDelta.x * Speed * Time.deltaTime);
                }

                touchStartPosition = touch.position;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            touchStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating && Input.GetMouseButton(0))
        {
            Vector2 mouseCurrentPosition = Input.mousePosition;
            Vector2 mouseDelta = mouseCurrentPosition - touchStartPosition;

            if (rotateOnYAxis)
            {
                transform.Rotate(Vector3.right, -mouseDelta.y * Speed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up, -mouseDelta.x * Speed * Time.deltaTime);
            }

            touchStartPosition = mouseCurrentPosition;
        }
    }

    private void UpdateAxisText()
    {
        axis.text = rotateOnYAxis ? "Vertical" : "Horizontal";
    }
}
