using System.Linq;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float followSpeed = 1.0f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public float followRange = 30.0f; // Max range at which item will follow the player

    void Update()
    {
        var item = ItemDatabase.Instance.lastLoadedItems.FirstOrDefault(i => i.name == "Bomb");
        if (item != null)
        {
            // Find the closest player within follow range each frame
            Vector3? closestPlayerPosition = FindClosestPlayerPositionWithinRange();
            if (closestPlayerPosition.HasValue)
            {
                Vector3 targetPosition = closestPlayerPosition.Value + offset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }

        }

    }
    public void OnGUI()
    {
        Update();
    }

    private Vector3? FindClosestPlayerPositionWithinRange()
    {
        float closestDistance = Mathf.Infinity;
        Vector3? closestPlayerPosition = null;
        foreach (Player player in FindObjectsOfType<Player>())
        {
            float distance = Vector3.Distance(transform.position, player.HeadPosition());
            if (distance < closestDistance && distance <= followRange)
            {
                closestDistance = distance;
                closestPlayerPosition = player.HeadPosition();
            }
        }
        return closestPlayerPosition;
    }


}
