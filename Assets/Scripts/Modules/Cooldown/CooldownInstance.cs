using UnityEngine;

namespace Class.Cooldown
{
    public class CooldownInstance : MonoBehaviour, IInitialize
    {
        private static CooldownInstance instance;
        public static CooldownInstance Instance => instance;

        private void Awake() => Init();
        private void Update() => GlobalCooldown.UpdateTime(Time.deltaTime);
        
        public void Init()
        {
            if (instance != null && instance != this)
                Destroy(this);

            instance = this;
        }
    }
}