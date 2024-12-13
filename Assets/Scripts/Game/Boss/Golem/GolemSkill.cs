using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSkill : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject skillObject;

    private bool isSkillAvailable = true;
    [SerializeField] private float cooldownTime = 20f;

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
                Instantiate(skillObject, spawnPoint.position, Quaternion.identity);
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
