using System.Collections.Generic;

namespace Class.Cooldown
{
    static class GlobalCooldown
    {
        private static List<Cooldown> cooldowns = new List<Cooldown>();

        public static void UpdateTime(float time)
        {
            if (time <= 0 || cooldowns.Count <= 0) return;
            cooldowns.ForEach(x => x.SpendTime(time));
        }

        public static void AddToList(Cooldown cooldown)
        {
            cooldowns.Add(cooldown);
        }

        public static void RemoveFromList(Cooldown cooldown)
        {
            cooldowns.Remove(cooldown);
        }
    }
}