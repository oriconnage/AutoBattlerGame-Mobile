using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtn : Singleton<BuyBtn>
{
    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceText;






 
    public int Price { get => price;  }

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = Price+"";
    }

}
