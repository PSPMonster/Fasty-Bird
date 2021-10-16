using UnityEngine;

public class SkinManager : MonoBehaviour 
{
    public static Sprite equippedSkin;

    public void EquipSkin(SSkinInfo skinInfo)
    {
        equippedSkin = skinInfo.skinSprite;
    }
}