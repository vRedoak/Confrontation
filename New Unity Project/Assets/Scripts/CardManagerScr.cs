using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public enum AbilityType
    {
        NO_ABILITY,
        INSTANT_ACTIVE,
        DOUBLE_ATTACK,
        SHIELD,
        PROVOCATION,
        REGENERATION_EACH_TURN,
        COUNTER_ATTACK
    }

    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public bool CanAttack;
    public bool IsPlaced;

    public List<AbilityType> Abilities;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }

    public bool HasAbility
    {
        get
        {
            return Abilities.Count > 0;
        }
    }
     public bool IsProvocation
    {
        get
        {
            return Abilities.Exists(x => x == AbilityType.PROVOCATION);
        }
    }
     
    public int TimesDealedDamage;

    public Card( string name, string logopath, int attack, int defense, int manacost, AbilityType abilityType =0)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logopath);
        Attack = attack;
        Defense = defense;
        CanAttack = false;
        Manacost = manacost;
        IsPlaced = false;
        Abilities = new List<AbilityType>();
        if (abilityType != 0)
            Abilities.Add(abilityType);
       TimesDealedDamage = 0; 
    }
    public Card(string name, Sprite logosp, int attack, int defense, int manacost, AbilityType abilityType = 0)
    {
        Name = name;
        Logo = logosp;
        Attack = attack;
        Defense = defense;
        CanAttack = false;
        Manacost = manacost;
        IsPlaced = false;
        Abilities = new List<AbilityType>();
        if (abilityType != 0)
            Abilities.Add(abilityType);
        TimesDealedDamage = 0;
    }


    public void GetDamage(int dmg)
    {
        if (dmg > 0)
        {
            if (Abilities.Exists(x => x == AbilityType.SHIELD))
                Abilities.Remove(AbilityType.SHIELD);
            else
            Defense -= dmg;
        }
        
    }
}

public static class CardManager
{
   
    
    public static List<Card> AllCards = new List<Card>();
    
}

public class CardManagerScr : MonoBehaviour
{

	public void Awake()
    {
        if (CardManager.AllCards.Count == 0)
        {
            CardManager.AllCards.Add(new Card("Дзимму33", "Image/Cards/Дзимму33", 3, 3, 6, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("Ленин53", "Image/Cards/Ленин53", 5, 3, 5, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("Грозный44", "Image/Cards/Грозный44", 4, 4, 4, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("камикадзе43", "Image/Cards/камикадзе43", 4, 3, 4, Card.AbilityType.INSTANT_ACTIVE));
            CardManager.AllCards.Add(new Card("мицунари77", "Image/Cards/мицунари77", 7, 7, 7, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("миямото33", "Image/Cards/миямото33", 3, 3, 6, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("Бисмарк62", "Image/Cards/Бисмарк62", 6, 2, 4, Card.AbilityType.REGENERATION_EACH_TURN));
            CardManager.AllCards.Add(new Card("небельверфер63", "Image/Cards/небельверфер63", 6, 3, 5, Card.AbilityType.INSTANT_ACTIVE));
            CardManager.AllCards.Add(new Card("камикадзе43", "Image/Cards/Танк_мыш1212", 12, 12, 10, Card.AbilityType.PROVOCATION));
            CardManager.AllCards.Add(new Card("Мотопехота34", "Image/Cards/Мотопехота34", 3, 4, 3, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("партизаны12", "Image/Cards/партизаны12", 1, 2, 1, Card.AbilityType.REGENERATION_EACH_TURN));
            CardManager.AllCards.Add(new Card("Пехота22", "Image/Cards/Пехота22", 2, 2, 1, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("Путин17", "Image/Cards/Путин17", 1, 7, 4, Card.AbilityType.NO_ABILITY));
            CardManager.AllCards.Add(new Card("Солдат31", "Image/Cards/Солдат31", 3, 1, 2, Card.AbilityType.NO_ABILITY));
        }
    }
}

