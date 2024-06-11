using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprine : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActived;
    private float alpha;
    [SerializeField]
    private float alphaSet = 1f;
    [SerializeField]
    private float alphaMultiplier = 0.98f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActived = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if (Time.time >= (timeActived + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
