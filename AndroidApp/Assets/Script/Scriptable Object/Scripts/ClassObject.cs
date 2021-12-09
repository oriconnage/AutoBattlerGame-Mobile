using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Archer,
    Melee,
    Tank,
    Mage
}
public  abstract class ClassObject : ScriptableObject
{
    // Set card name
    public new string name;
    // sprite and prefab
    public Sprite sprite;
    public GameObject prefab;

    public CardType type;
    //description
    [TextArea(15, 20)]
    public string description;

  /**  public ClassObject(int Id, string name, CardType type, string description,Sprite sprite,GameObject prefab)
    {
        this.Id = Id;
        this.name = name;
        this.type = type;
        this.description = description;
        this.sprite = sprite;
        this.prefab = prefab;
    }**/

}
