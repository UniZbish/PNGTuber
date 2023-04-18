using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomImageImporter : MonoBehaviour
{
    [SerializeField] private string importPath;
    public List<CustomAvatar> AvatarList;

    private void Awake()
    {
        AvatarList = new List<CustomAvatar>();

        importPath = $"{Path.Combine(Environment.CurrentDirectory, "AvatarImages")}";
        if (!Directory.Exists(importPath)) 
        {
            Directory.CreateDirectory(importPath);
        }

        foreach (string folder in Directory.GetDirectories(importPath))
        {
            CustomAvatar temp = new CustomAvatar();
            temp.avatarName = Path.GetFileName(folder);

            foreach (string file in Directory.GetFiles(folder))
            {
                if (file.Contains("Open"))
                {
                    temp.open = imageImport(file);
                }
                else if (file.Contains("Close")) 
                {
                    temp.close = imageImport(file);
                }
            }
            AvatarList.Add(temp);
        }
    }

    private Sprite imageImport(string file) 
    {
        byte[] fileData = File.ReadAllBytes(file);
        Texture2D tex2D = new Texture2D(2, 2);
        tex2D.LoadImage(fileData);
        Sprite imageSprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        return imageSprite;
    }
}
