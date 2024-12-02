using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int minCoins = 5;
    [SerializeField] private int maxCoins = 10;
    [SerializeField] private float forceMin = 3f;
    [SerializeField] private float forceMax = 6f;
    [SerializeField] private float spreadAngle = 30f;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent<ParticleManager>(ref particleManager);
    private ParticleManager particleManager;

    private Stats Stats => stats ? stats : core.GetCoreComponent<Stats>(ref stats);
    private Stats stats;

    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticles(particle);
            DropCoins();
        }

        core.transform.parent.gameObject.SetActive(false);
    }

    private void DropCoins()
    {
        int coinCount = Random.Range(minCoins, maxCoins);

        for (int i = 0; i < coinCount; i++)
        {
            GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = newCoin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float angle = Random.Range(-spreadAngle, spreadAngle);
                Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

                float force = Random.Range(forceMin, forceMax);

                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    private void OnEnable()
    {
        Stats.Health.OnCurrentValueZero += Die;
    }

    private void OnDisable()
    {
        Stats.Health.OnCurrentValueZero -= Die;
    }
}
