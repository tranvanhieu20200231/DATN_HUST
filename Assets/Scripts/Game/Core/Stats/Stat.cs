using System;
using System.Collections;
using TMPro;
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

        [SerializeField] private GameObject damagePopup;

        [SerializeField] private ParticleManager particleManager;

        [SerializeField] private SpriteRenderer SR;

        public void Init() => CurrentValue = MaxValue;

        public void InitPlayer()
        {
            MaxValue = PlayerData.health;

            CurrentValue = MaxValue;
        }

        public void Increase(float amount)
        {
            CurrentValue += amount;

            TextMeshProUGUI textPopup = damagePopup.GetComponentInChildren<TextMeshProUGUI>();
            if (textPopup != null)
            {
                textPopup.text = "+" + ((int)amount).ToString();
            }

            particleManager.StartParticles(damagePopup);
        }

        public void Decrease(float amount)
        {
            CurrentValue -= amount;

            TextMeshProUGUI textPopup = damagePopup.GetComponentInChildren<TextMeshProUGUI>();
            if (textPopup != null)
            {
                textPopup.text = (-(int)amount).ToString();
            }

            particleManager.StartParticles(damagePopup);

            if (SR != null)
            {
                particleManager.StartCoroutine(Blink(SR, 0.1f, 5));
            }
        }

        private IEnumerator Blink(SpriteRenderer spriteRenderer, float blinkDuration, int blinkCount)
        {
            for (int i = 0; i < blinkCount; i++)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
                yield return new WaitForSeconds(blinkDuration);

                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                yield return new WaitForSeconds(blinkDuration);
            }
        }
    }
}