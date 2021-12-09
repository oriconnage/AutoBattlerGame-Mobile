using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttons : Singleton<buttons>
{
    
    private GameObject unitprefab;
    private Sprite UnitSprite;
    [SerializeField]
    private Sprite HoverIcon;

    public GameObject Unitprefab
    {
        get
        {
            return unitprefab;
        }
        set
        {
            unitprefab = value;
        }
    }
    public Sprite unitSprite
    {
        get
        {
            return UnitSprite;
        }
        set
        {
            UnitSprite = value;
        }
    }


    public void SetImage()
    {
        Image image =GetComponentsInChildren<Image>()[1];
        image.sprite = UnitSprite;
        Debug.Log("setted the image");
    }

    public Sprite Getsprite()
    { return HoverIcon; }
}
