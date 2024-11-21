using System;
using UnityEngine;

namespace Asset.Script.Core.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;

        [field: SerializeField] public float MaxValue;

        public float CurrentValue
        {
            get => currentValue;
            private set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }

        private float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void InitPlayer()
        {
            MaxValue = PlayerData.health;

            CurrentValue = MaxValue;
        }

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;
    }
}