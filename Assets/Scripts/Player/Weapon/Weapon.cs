using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{ 
    [SerializeField] protected int damage;

    public virtual void PerformBasicAttack() { }

    public virtual void PerformStrongerAttack() { }
    public virtual void CancelStrongerAttack() { }

    public virtual void StartAlternativeAttack() { }
    public virtual void PerformAlternativeAttack() { }
    public virtual void CancelAlternativeAttack() { }

    public virtual void PerformBeamPullAction(float input) { }
}
