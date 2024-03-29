﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer
{
    public int HP, Mana, Manapool;
    const int MAX_MANAPOOL = 10;

    public PLayer()
    {
        HP = 30;
        Mana = Manapool = 2;
    }
	
    public void RestoreRoundMana()
    {
        Mana = Manapool;
    }

    public void IncreaseManapool()
    {
        Manapool = Mathf.Clamp(Manapool + 1, 0, MAX_MANAPOOL);

    }

    public void GetDamage(int damage)
    {
        HP = Mathf.Clamp(HP - damage, 0, int.MaxValue);
    }
}
