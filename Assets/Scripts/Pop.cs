using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public bool iSpeak;

    public Sprite Open;
    public Sprite Close;
    public float delayClose;

    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    IEnumerator OpenMouth()
    {
        yield return new WaitForSeconds(delayClose);
        iSpeak = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (iSpeak)
        {
            spriteRend.sprite = Open;
        }
        else 
        {
            spriteRend.sprite = Close;
        }
    }
}
