using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{
    private Camera cam;
    public float scale;
    public float mouseSens;
    private float hor, ver;

    public bool allowMoveBool, lockState;

    [SerializeField] private Button lockButton;
    [SerializeField] private Sprite lockedSprite, unlockedSprite;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        scale = 0.5f;
        mouseSens = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockState && allowMoveBool)
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                cam.orthographicSize += -Input.mouseScrollDelta.y * scale;
            }

            if (Input.GetMouseButton(0))
            {
                ver = Input.GetAxis("Mouse X");
                hor = Input.GetAxis("Mouse Y");
            }

            if (Input.GetMouseButtonUp(0))
            {
                ver = 0f;
                hor = 0f;
            }

            cam.transform.Translate(new Vector2(-ver * Time.deltaTime * mouseSens, -hor * Time.deltaTime * mouseSens));
        }

        if (Input.GetKeyDown(KeyCode.L)) 
        {
            ToggleLockState();
        }
    }

    public void ToggleLockState() 
    {
        lockState = !lockState;
        if (lockState)
        {
            lockButton.image.sprite = unlockedSprite;
        }
        else 
        {
            lockButton.image.sprite = lockedSprite;
        }        
    }
}
