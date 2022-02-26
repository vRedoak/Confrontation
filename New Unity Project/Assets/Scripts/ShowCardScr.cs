using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardScr : MonoBehaviour {

    

    public Transform All, My;
    public GameObject CardPref;
    public Text CardsCount;

    public Card SelectedCard; 

    public Card Card;
    public CardInfoScr Info;

    public int number=0;
    void Start ()
    {
        InitializationCard();
        Kolods.ReadyColod = false;
    }
	
    public void Update()
    {
        //  InitializationCard();
        //Debug.Log("hyi tebe0");
        ShowCount();
    }
    public void InitializationCard()
    {
        if (Kolods.AllCards.Count == 0)
        {
            Kolods.AllCards = CardManager.AllCards;
           
        }
        if (Kolods.MyCards.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                Kolods.MyCards.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
            }
         
        }
        
        GiveHandCards(Kolods.AllCards, All);
        GiveHandCards(Kolods.MyCards, My);
        Debug.Log(Kolods.AllCards.Count + " ;"+ CardManager.AllCards.Count+ ";"+Kolods.MyCards.Count);
        
       

    }


     void GiveHandCards(List<Card> deck, Transform hand)
      {
        int i = 0;
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            number++;
            CreateCardPref(deck[i], hand);
        }
    }

    void CreateCardPref(Card card, Transform hand)
    {
        GameObject cardGO = Instantiate(CardPref, hand, false);
        string s = number + "";
        cardGO.name = "Card"+number;
        CardController cardC = cardGO.GetComponent<CardController>();

        cardC.InitCards(card);
        Info.ShowCardInfo();
    }
    
    public void SelectCard()
    {
      //  SelectedCard = 
    }

    public void SaveMyCards()
    {
        if (My.childCount == 20)
        {
            Kolods.MyCards.Clear();

            for (int i = 0; i < My.childCount; i++)
            {
                CardController cardC = My.GetChild(i).GetComponent<CardController>();
                Kolods.ReadyColod = true;
                Kolods.MyCards.Add(cardC.Card);

            }
        }
       
    }
   public void ShowCount()
    {
        CardsCount.text = My.childCount.ToString();

    }
}

//   string nameCard = .GetChild(8).ToString();
            // var yu = GameObject.Find().transform;
            //   Debug.Log( "; " + nameCard);
            //Kolods.MyCards.Add();