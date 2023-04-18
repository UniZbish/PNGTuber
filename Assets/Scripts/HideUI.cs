using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
