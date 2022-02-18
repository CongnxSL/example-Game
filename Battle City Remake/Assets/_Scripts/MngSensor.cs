using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngSensor : MonoBehaviour
{
    public bool sensor;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player 1") ||
            collision.gameObject.CompareTag("Player 2") ||
            collision.gameObject.CompareTag("Enemy 1") ||
            collision.gameObject.CompareTag("Enemy 2") ||
            collision.gameObject.CompareTag("Enemy 3") ||
            collision.gameObject.CompareTag("Enemy 4") ||
            collision.gameObject.CompareTag("Map"))
        {
            sensor = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player 1") ||
            collision.gameObject.CompareTag("Player 2") ||
            collision.gameObject.CompareTag("Enemy 1") ||
            collision.gameObject.CompareTag("Enemy 2") ||
            collision.gameObject.CompareTag("Enemy 3") ||
            collision.gameObject.CompareTag("Enemy 4") ||
            collision.gameObject.CompareTag("Map"))
        {
            sensor = true;
        }
    }
}
