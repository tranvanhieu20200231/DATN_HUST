using Asset.Script.Core.StatsSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : CoreComponent
{
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Poise { get; private set; }

    [SerializeField] private float poiseRecoveryRate;

    protected override void Awake()
    {
        base.Awake();

        if (LayerMask.LayerToName(gameObject.layer) == "Player")
        {
            Health.InitPlayer();
            Poise.Init();
        }
        else
        {
            Health.Init();
            Poise.Init();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializeStats()
    {
        if (LayerMask.LayerToName(gameObject.layer) == "Player")
        {
            Health.InitPlayer();
            Poise.Init();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeStats();
    }

    private void Update()
    {
        if (Poise.CurrentValue.Equals(Poise.MaxValue))
            return;

        Poise.Increase(poiseRecoveryRate * Time.deltaTime);
    }
}
