using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HeadChange : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public UnityEngine.UI.Image image;
    
    public void ChangeImage()
    {
        if (image.sprite == sprite1)
        {
            image.sprite = sprite2;
        }
        else
        {
            image.sprite = sprite1;
        }
        
    }
}
