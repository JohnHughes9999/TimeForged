using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = respawnPoint.position;
    }
}
