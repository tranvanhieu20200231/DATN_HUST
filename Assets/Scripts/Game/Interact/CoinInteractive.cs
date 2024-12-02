using UnityEngine;

public class CoinInteractive : MonoBehaviour
{
    private Transform playerTransform;
    private bool isFollowingPlayer = false;
    private float moveSpeed = 20f;

    private float checkRadius = 0.25f;

    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Player"));

        if (playerCollider != null && !isFollowingPlayer)
        {
            playerTransform = playerCollider.transform;
            isFollowingPlayer = true;
        }

        if (isFollowingPlayer && playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerTransform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        CoinUI.currentCoin++;
    }
}
