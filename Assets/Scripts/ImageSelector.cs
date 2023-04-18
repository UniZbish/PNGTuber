using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{

    [SerializeField] GameObject imageBox;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private CustomImageImporter customAvatarScript;
    [SerializeField] private Pop avatarImage;

    private void Start()
    {
        int x = 0;
        foreach (CustomAvatar avatar in customAvatarScript.AvatarList) 
        {
            GameObject selectImageButton = Instantiate(buttonPrefab, imageBox.transform, false);
            selectImageButton.name = x.ToString();
            Image kidImg = selectImageButton.GetComponentsInChildren<Image>()[1];
            kidImg.sprite = customAvatarScript.AvatarList[int.Parse(selectImageButton.name)].close;
            selectImageButton.GetComponent<Button>().onClick.AddListener(delegate { imageSelect(selectImageButton); });
            x++;
        }

        avatarImage.Open = customAvatarScript.AvatarList[0].open;

        avatarImage.Close = customAvatarScript.AvatarList[0].close;

        ToggleVisabillity(true);
    }

    public void ToggleVisabillity(bool close)
    {
        if (!close)
        {
            imageBox.SetActive(false);
        }
        else
        {
            imageBox.SetActive(!imageBox.activeSelf);
        }
    }

    public void imageSelect(GameObject button)
    {
        avatarImage.Open = customAvatarScript.AvatarList[int.Parse(button.name)].open;

        avatarImage.Close = customAvatarScript.AvatarList[int.Parse(button.name)].close;
    }
}
