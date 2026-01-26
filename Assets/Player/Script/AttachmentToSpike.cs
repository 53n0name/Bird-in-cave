using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentToSpike : MonoBehaviour
{

    [SerializeField] GameObject Canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftWallEnemy" || collision.gameObject.tag == "RightWallEnemy")
        {
            Canvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
