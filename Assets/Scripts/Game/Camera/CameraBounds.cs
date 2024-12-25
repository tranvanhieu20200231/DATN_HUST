using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CinemachineConfiner2D))]
public class CameraBounds : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    private bool isBoundsAssigned = false;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError("No CinemachineConfiner2D component found on this GameObject.");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryAssignCameraBounds();
    }

    private void Update()
    {
        if (!isBoundsAssigned)
        {
            TryAssignCameraBounds();
        }
    }

    private void TryAssignCameraBounds()
    {
        GameObject cameraBoundsObject = GameObject.Find("CameraBounds");

        if (cameraBoundsObject == null)
        {
            return;
        }

        if (!isBoundsAssigned)
        {
            PolygonCollider2D polygonCollider = cameraBoundsObject.GetComponent<PolygonCollider2D>();
            if (polygonCollider == null)
            {
                Debug.LogWarning("The GameObject 'CameraBounds' does not have a PolygonCollider2D component.");
                return;
            }

            confiner.m_BoundingShape2D = polygonCollider;

            Debug.Log("Bounding Shape 2D successfully assigned to CinemachineConfiner2D.");

            isBoundsAssigned = true;
        }
        else
        {
            confiner.m_BoundingShape2D = null;
            confiner.m_BoundingShape2D = cameraBoundsObject.GetComponent<PolygonCollider2D>();
            Debug.Log("Bounding Shape 2D successfully re-assigned.");
        }
    }
}
