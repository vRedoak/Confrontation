using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardInfoScr : MonoBehaviour
{
   
    public Image Logo;
    public Text Attack, Defense, Manacost;
    public GameObject HideObj, HighlitghedObj;
    public Color NormalCol, TargetCol;
    public CardController CC;

    public void HideCardInfo()
    {
        HideObj.SetActive(true);
        Manacost.text = "";
    }
     

    public void ShowCardInfo()
    {
        HideObj.SetActive(false);
        Logo.sprite = CC.Card.Logo;
        Logo.preserveAspect = true;
        Attack.text = CC.Card.Attack.ToString();
        Defense.text = CC.Card.Defense.ToString();

    }
    public Sprite GetLogo()
    {
        return Logo.sprite;
    }

    public int GetAttack()
    {
        return int.Parse(Attack.text);  
    }

    public int GetDefense()
    {
        return int.Parse(Defense.text);
    }
    public int GetManacost()
    {
        return int.Parse(Manacost.text);
    }
    public void RefreshData()
    {
        Attack.text = CC.Card.Attack.ToString();
        Defense.text = CC.Card.Defense.ToString();
        Manacost.text = CC.Card.Manacost.ToString();
    }

    public void HighlightCard(bool highlight)
    {
        HighlitghedObj.SetActive(highlight);

    }

    public void DeHighlightCard()
    {
        HighlitghedObj.SetActive(false);
    }

    public void HighlightManaAvaliability(int currentMana)
    {
        GetComponent<CanvasGroup>().alpha = currentMana >= CC.Card.Manacost ? 1 : .5f;
    }

    public void HighlightAsTarget(bool highlight)
    {
        //GetComponent<Image>().color = highlight ?
        //                            TargetCol :
        //                            NormalCol;
       Logo.color = highlight ?
                                  TargetCol :
                                  NormalCol;

    }
}
