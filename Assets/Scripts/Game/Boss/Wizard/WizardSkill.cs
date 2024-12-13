using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkill : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject enemySpawn;

    private bool isSkillAvailable = true;
    [SerializeField] private float cooldownTime = 180f;

    public void ActivateSkill()
    {
        if (!isSkillAvailable)
        {
            Debug.LogWarning("Skill is on cooldown. Please wait.");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Instantiate(enemySpawn, spawnPoint.position, Quaternion.identity);
            }
        }

        StartCoroutine(SkillCooldown());
    }

    private IEnumerator SkillCooldown()
    {
        isSkillAvailable = false;
        yield return new WaitForSeconds(cooldownTime);
        isSkillAvailable = true;
    }
}
