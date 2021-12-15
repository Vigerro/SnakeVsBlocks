using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;    
    }

    public Vector2 GetDirectionToClick(Vector2 headPosition)
    {
            Vector2 mousePoition = _camera.ScreenToViewportPoint(Input.mousePosition);
            mousePoition.y = 1;
            mousePoition = _camera.ViewportToWorldPoint(mousePoition);
            return new Vector2(mousePoition.x - headPosition.x, mousePoition.y - headPosition.y);        
    }
}
