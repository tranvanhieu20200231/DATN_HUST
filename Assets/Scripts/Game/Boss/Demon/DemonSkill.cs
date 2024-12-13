using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSkill : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints_1;
    [SerializeField] private GameObject enemySpawn_1;

    private bool isSkillAvailable_1 = true;
    [SerializeField] private float cooldownTime_1 = 180f;

    [SerializeField] private List<Transform> spawnPoints_2;
    [SerializeField] private GameObject skillObject_2;

    private bool isSkillAvailable_2 = true;
    [SerializeField] private float cooldownTime_2 = 20f;

    public void ActivateSkill_1()
    {
        if (!isSkillAvailable_1)
        {
            Debug.LogWarning("Skill is on cooldown. Please wait.");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints_1)
        {
            if (spawnPoint != null)
            {
                Instantiate(enemySpawn_1, spawnPoint.position, Quaternion.identity);
            }
        }

        StartCoroutine(SkillCooldown_1());
    }

    private IEnumerator SkillCooldown_1()
    {
        isSkillAvailable_1 = false;
        yield return new WaitForSeconds(cooldownTime_1);
        isSkillAvailable_1 = true;
    }

    public void ActivateSkill_2()
    {
        if (!isSkillAvailable_2)
        {
            Debug.LogWarning("Skill is on cooldown. Please wait.");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints_2)
        {
            if (spawnPoint != null)
            {
                Instantiate(skillObject_2, spawnPoint.position, skillObject_2.transform.rotation);
            }
        }

        StartCoroutine(SkillCooldown_2());
    }

    private IEnumerator SkillCooldown_2()
    {
        isSkillAvailable_2 = false;
        yield return new WaitForSeconds(cooldownTime_2);
        isSkillAvailable_2 = true;
    }
}
