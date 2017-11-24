using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitBaseEvent 
{
    bool TakeDamage(float damage);

    void Dead();
}
