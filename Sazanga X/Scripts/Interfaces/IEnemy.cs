using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float hp {get;set;}
    public void Attack();
    public void Dead();
    public void Damage(float damageAmount);
}
