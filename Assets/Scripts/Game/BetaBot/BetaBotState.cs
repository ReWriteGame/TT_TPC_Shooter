using System;
using Modules.Score;

[Serializable]
public class BetaBotState
{
    public ScoreCounter health;
    public float damage;
    public float damageDistance;
    public bool isDied;
}