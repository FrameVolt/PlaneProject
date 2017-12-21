using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth{
    int Health { get;}
    void Damage(int val, GameObject initiator);
}
