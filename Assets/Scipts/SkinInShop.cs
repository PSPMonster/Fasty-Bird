using UnityEngine;
using UnityEngine.UI;

public class SkinInShop : MonoBehaviour
{
    public SSkinInfo skinInfo;

    public Image skinImage;
    public Text buttonText;
    public bool isSkinUnlocked;

    private void Awake() {
        skinImage.sprite = skinInfo.skinSprite;
        IsSkinUnlocked();
    }

    private void IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo.skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            buttonText.text = "Equip";
        }
    }

    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            FindObjectOfType<SkinManager>().EquipSkin(skinInfo);
        }
        else
        {
            if (FindObjectOfType<PlayerMoney>().TryRemoveMoney(skinInfo.skinPrice))
            {
                PlayerPrefs.SetInt(skinInfo.skinID.ToString(), 1);
                IsSkinUnlocked();
            }

        }
    }
}
