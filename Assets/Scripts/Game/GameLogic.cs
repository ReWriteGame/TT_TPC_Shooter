using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private List<BetaBot> betaBots;
    [SerializeField] private Character hero;
    [SerializeField] private MouseCursorState mouseCursorState;
    [SerializeField] private CinemachineFreeLook freeLook;

    public UnityEvent OnWinGame;
    public UnityEvent OnLoseGame;

    private void Awake() => Subscribe();
    private void OnDestroy() => Unsubscribe();

    private void Subscribe()
    {
        hero.OnDied += OnLoseGame.Invoke;
        betaBots.ForEach(x => x.OnDied += CheckIsWin);
        OnWinGame.AddListener(EndGame);
        OnLoseGame.AddListener(EndGame);
    }

    private void Unsubscribe()
    {
        hero.OnDied -= OnLoseGame.Invoke;
        betaBots.ForEach(x => x.OnDied -= CheckIsWin);
        OnWinGame.RemoveListener(EndGame);
        OnLoseGame.RemoveListener(EndGame);
    }

    private void CheckIsWin()
    {
        bool enemyIsDied = true;
        foreach (var bot in betaBots)
            if (!bot.IsDied)
                enemyIsDied = false;

        if (enemyIsDied) OnWinGame?.Invoke();
    }

    private void EndGame()
    {
        mouseCursorState.Unfocus();
        freeLook.enabled = false;
    }
}