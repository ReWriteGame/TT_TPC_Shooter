using Modules.Score.Visual;
using UnityEngine;

public class BetaBotVisual : MonoBehaviour, IInitialize
{
    [SerializeField] private BetaBot betaBot;
    [SerializeField] private ScoreCounterVisualText healthVisual;

    public void Init() => healthVisual.SetScoreCounter(betaBot.Health);
}