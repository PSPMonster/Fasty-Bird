using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName ="New Skin", menuName ="Create New Skin")]
public class SSkinInfo : ScriptableObject
{
    public enum SkinIDs { pink, red, blue }
    public SkinIDs skinID;

    public Sprite skinSprite;

    public int skinPrice;
}
