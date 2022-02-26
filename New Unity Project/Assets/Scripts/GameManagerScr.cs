using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public PLayer Player, Enemy;
    public List<Card> EnemyDeck, PlayerDeck;
    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        Player = new PLayer();
        Enemy = new PLayer();
    }
    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        if (Kolods.ReadyColod)
        {
            list = Kolods.MyCards;
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
            }
        }
        return list;
    }
}

public class GameManagerScr : MonoBehaviour
{
    public static GameManagerScr Instance;

    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand,
                     EnemyField, PlayerField;

    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public Button EndTurnBtn;

    public GameObject ResultGO;
    public Text ResultTxt;

    public AttackeHero EnemyHero, PlayerHero;

    public List<CardController> PlayerHandCards = new List<CardController>(),
                             PlayerFieldCards = new List<CardController>(),
                             EnemyHandCards = new List<CardController>(),
                             EnemyFieldCards = new List<CardController>();

    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }


    private void Awake()
    {
        if ( Instance == null)
        Instance = this; 
    }

    public void RestartGame()
    {
        StopAllCoroutines();
        foreach (var card in PlayerHandCards) Destroy(card.gameObject);
        foreach (var card in PlayerFieldCards) Destroy(card.gameObject);
        foreach (var card in EnemyHandCards) Destroy(card.gameObject);
        foreach (var card in EnemyFieldCards) Destroy(card.gameObject);

        PlayerHandCards.Clear();
        PlayerFieldCards.Clear();
        EnemyHandCards.Clear();
        EnemyFieldCards.Clear();

        StartGame();
    }

    public void StartGame()
    {
        Turn = 0;

        CurrentGame = new Game();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        UIController.Instance.StartGame();
        StartCoroutine(TurnFunc());
    }


    public void GiveCard()
    {
        StartGame();
    }

    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 3)
        {
            GiveCardToHand(deck, hand);
            
        }
    }
    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        
        if (deck.Count == 0) return;
        CreateCardPref(deck[0], hand);
        deck.RemoveAt(0);
       
    }

    void CreateCardPref(Card card, Transform hand)
    {
        GameObject cardGO = Instantiate(CardPref, hand, false);
        CardController cardC = cardGO.GetComponent<CardController>();

        cardC.Init(card, hand == PlayerHand);
        if (cardC.IsPlayerCard)
            PlayerHandCards.Add(cardC);
        else EnemyHandCards.Add(cardC);
    }


    IEnumerator TurnFunc()
    {
        TurnTime = 30;

        UIController.Instance.UpdateTurnTime(TurnTime);

        foreach (var card in PlayerFieldCards)
            card.Info.HighlightCard(false);

        CheckCardsForManaAvaliability();

        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card.Card.CanAttack = true;
                card.Info.HighlightCard(true);
                card.Ability.OnNewTurn();

            }

            while (TurnTime-- > 0)
            {
                UIController.Instance.UpdateTurnTime(TurnTime);
                yield return new WaitForSeconds(1);
            }
            
            ChangeTurn();
        }
        else
        {
            foreach (var card in EnemyFieldCards)
            {
                card.Card.CanAttack = true;
                card.Ability.OnNewTurn();
            }
            
            StartCoroutine( EnemyTurn(EnemyHandCards));
        }
        
    }
    IEnumerator EnemyTurn(List<CardController> cards)
    {
        yield return new WaitForSeconds(1);
        int count = cards.Count == 1 ? 1 : Random.Range(0, cards.Count);
        for (int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5 || 
                //EnemyMana==0 || 
                EnemyHandCards.Count ==0) break;

            //  List<CardController> cardsList = cards.FindAll(x => EnemyMana >= x.Card.Manacost);
            List<CardController> cardsList = cards.FindAll(x => CurrentGame.Enemy.Mana >= x.Card.Manacost);
            if (cardsList.Count == 0) break;

            cardsList[0].GetComponent<CardMovementScr>().MoveToField(EnemyField);
            
            yield return new WaitForSeconds(.75f);
           
            cardsList[0].transform.SetParent(EnemyField);

            cardsList[0].OnCast();
        }
        yield return new WaitForSeconds(1);
        while (EnemyFieldCards.Exists(x=> x.Card.CanAttack))
        {
            var activeCard = EnemyFieldCards.FindAll(x => x.Card.CanAttack)[0];
            bool hasProvocation = PlayerFieldCards.Exists(x => x.Card.IsProvocation);
            
            if (hasProvocation||
                Random.Range(0, 2) == 0 && PlayerFieldCards.Count > 0)
            {
                CardController enemy;
                if (hasProvocation)
                    enemy = PlayerFieldCards.Find(x => x.Card.IsProvocation);
                else
                    enemy = PlayerFieldCards[Random.Range(0, PlayerFieldCards.Count)];
                if (activeCard.Card.CanAttack)
                {
                    Debug.Log(activeCard.Card.Attack + ";" + activeCard.Card.Defense + "-->" + enemy.Card.Attack + ";" + enemy.Card.Defense);


                    activeCard.Movement.MoveToTarget(enemy.transform);
                    yield return new WaitForSeconds(.75f);

                    CardsFight(enemy, activeCard);
                    activeCard.Card.CanAttack = false;
                }
            }
            else
            {
                if (activeCard.Card.CanAttack)
                {
                    Debug.Log(activeCard.Card.Attack + ";" + activeCard.Card.Defense + "-->" + "AttackHero");

                    activeCard.GetComponent<CardMovementScr>().MoveToTarget(PlayerHero.transform);
                    yield return new WaitForSeconds(.75f);

                    DamageHero(activeCard, false);
                    activeCard.Card.CanAttack = false;
                }
            }

            yield return new WaitForSeconds(.2f);

        }
        yield return new WaitForSeconds(1);
        ChangeTurn();

    }

    

    public void ChangeTurn()
    {
        TurnTime = 30;
        StopAllCoroutines();
        Turn++;
        UIController.Instance.DisableTurnBtn(); 
        if (IsPlayerTurn)
        {
            GiveNewCards();

            
            CurrentGame.Player.RestoreRoundMana();

            UIController.Instance.UpdateHPAndMana();
        }
        else
        {
            CurrentGame.Player.IncreaseManapool();
            CurrentGame.Enemy.IncreaseManapool();
            CurrentGame.Enemy.RestoreRoundMana();
            UIController.Instance.UpdateHPAndMana();
        }
        StartCoroutine(TurnFunc());
        TurnTime = 30;

    }

    void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }

    public void CardsFight(CardController attacker, CardController defender)
    {
        defender.Card.GetDamage(attacker.Card.Attack);
        attacker.OnDamageDeal();
        defender.OnTakeDamage(attacker);

        attacker.Card.GetDamage(defender.Card.Attack);
        attacker.OnTakeDamage();

        attacker.CheckForAlive();
        defender.CheckForAlive();
    }

   
    public void ReduceMana (bool playerMana, int manacost)
    {
        if (playerMana)
        {
            CurrentGame.Player.Mana -= manacost;
        }
        else
            CurrentGame.Enemy.Mana -= manacost;
        UIController.Instance.UpdateHPAndMana();
    }
  
    
    public void DamageHero(CardController card, bool isEnemyAttacked)
    {
        if (isEnemyAttacked)
        {
            CurrentGame.Enemy.GetDamage(card.Card.Attack);
        }
        else
            CurrentGame.Player.GetDamage(card.Card.Attack);
        UIController.Instance.UpdateHPAndMana();
        card.OnDamageDeal();
        CheckForResult();
    }
    public void CheckForResult()
    {
        if (CurrentGame.Enemy.HP == 0 || CurrentGame.Player.HP == 0)
        {
            StopAllCoroutines();
            UIController.Instance.ShowResult();
        }
    }
    public void CheckCardsForManaAvaliability()
    {
        foreach (var card in PlayerHandCards)
            card.Info.HighlightManaAvaliability(CurrentGame.Player.Mana);
    }

   public void HighlightTargets(bool highlight)
    {
        List<CardController> targets = new List<CardController>();
        if (EnemyFieldCards.Exists(x => x.Card.IsProvocation))
            targets = EnemyFieldCards.FindAll(x => x.Card.IsProvocation);
        else
        {
            targets = EnemyFieldCards;
            EnemyHero.HighlightAsTarget(highlight);
        }
        foreach (var card in targets)
            card.Info.HighlightAsTarget(highlight);
        EnemyHero.HighlightAsTarget(highlight);
    }
   
}