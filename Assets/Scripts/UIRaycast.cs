using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycast : MonoBehaviour
{
    private GraphicRaycaster raycastUI;
    private PointerEventData pointerData;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private CameraControls camControls;

    [SerializeField] private ImageSelector imageSelectorScript;
    [SerializeField] private MicSensitivity micSettingScript;
    [SerializeField] private CustomBackground backgroundSettingScript;

    private void Start() 
    {
        raycastUI = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycastUI.Raycast(pointerData, results);
        if (results.Count != 0)
        {
            camControls.allowMoveBool = false;
        }
        else 
        {
            if (Input.GetMouseButtonDown(0))
            {
                CloseAllMenus();
            }
            camControls.allowMoveBool = true;
        }
    }

    private void CloseAllMenus()
    {
        micSettingScript.ToggleVisabillity(false);
        backgroundSettingScript.ToggleVisabillity(false);
        imageSelectorScript.ToggleVisabillity(false);
    }
}
