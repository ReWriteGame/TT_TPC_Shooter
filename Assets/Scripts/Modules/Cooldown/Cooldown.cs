using System;
using Modules.Score;
using UnityEngine;

namespace Class.Cooldown
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private ScoreCounter counter;
        [SerializeField] private bool reloaded = true;
        [SerializeField] private bool isRunning = false;
        [SerializeField] private float timeScale = 1;

        public event Action OnEndCooldown;

        public bool Reloaded => reloaded;
        public float ReloadTime => counter.MaxValue;
        public float CurrentTime => counter.Value;

        public Cooldown(float duration, bool startStateReloaded = true, bool startState = false) :
            this(startStateReloaded, startState)
        {
            counter.SetData(new ScoreCounterData(0, 0, duration));
        }

        public Cooldown(bool startStateReload = true, bool startState = false)
        {
            reloaded = startStateReload;
            isRunning = startState;
            counter = new ScoreCounter();
            counter.OnReachMaxValue += ResetAndUse;
            counter.OnReachMaxValue += EndCooldown;
            GlobalCooldown.AddToList(this);
        }



        ~Cooldown()
        {
            counter.OnReachMaxValue -= ResetAndUse;
            counter.OnReachMaxValue -= EndCooldown;
            GlobalCooldown.RemoveFromList(this);
        }

        public void SpendTime(float time, bool useTimeScale = true)
        {
            time = useTimeScale ? time * timeScale : time;
            if (isRunning && !reloaded) counter.IncreaseValue(time);
        }


        public void SetDuration(float duration)
        {
            counter.SetData(new ScoreCounterData(0, 0, duration));
            ResetAndUse();
        }

        public void Play()
        {
            if (!reloaded) isRunning = true;
        }

        public void Pause() => isRunning = false;

        public void ResetAndUse() => Reset(true);
        public void ResetAndUnuse() => Reset(false);

        public void Reset(bool useState)
        {
            reloaded = useState;
            isRunning = false;
            counter.SetData(new ScoreCounterData(0, 0, counter.MaxValue));
        }

        //////////////////////// wrapper methods //////////////////////// 

        private void EndCooldown() => OnEndCooldown?.Invoke();


        public bool Activate()
        {
            if (reloaded)
            {
                ResetAndUnuse();
                Play();
                return true;
            }

            return false;
        }

        private void OnEndTime(Action action = null)
        {
        }
    }
}