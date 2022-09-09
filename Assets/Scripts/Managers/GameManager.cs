using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float points { get; private set; }

    public void AddPoints() => points += 1;
}
