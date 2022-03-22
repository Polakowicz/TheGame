using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{ 
    [SerializeField] protected int damage;

    public abstract void PerformeBasicAttack();
}
