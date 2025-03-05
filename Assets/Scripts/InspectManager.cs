using System;
using UnityEngine;
using TMPro;

public class RotateObject : MonoBehaviour
{
    public float Speed = 10f;
    private bool isRotating = false;
    private Vector2 touchStartPosition;
    private bool rotateOnXAxis = false;
    public TextMeshProUGUI axis;

    void Start()
    {
        UpdateAxisText();
    }

    public void AxisToggle()
    {
        rotateOnXAxis = !rotateOnXAxis;
        UpdateAxisText();
    }

    public void ZoomIn()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void ZoomOut()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        if (Input.touchCount > 0) // Mobile touch input
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

                if (rotateOnXAxis)
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
        else if (Input.GetMouseButtonDown(0)) // PC mouse input
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

            if (rotateOnXAxis)
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
        axis.text = rotateOnXAxis ? "Vertical" : "Horizontal";
    }
}
