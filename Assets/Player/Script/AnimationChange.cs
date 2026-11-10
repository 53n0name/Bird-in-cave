using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private Rigidbody2D Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool flipped = Rigidbody.linearVelocity.x < 0;

        this.transform.rotation = Quaternion.Euler(new Vector2(0f, flipped ? 180f : 0f));

        if (Rigidbody.linearVelocity.y > 0)
        {
            _animator.SetBool("Flying", true);
        }
        else
        {
            _animator.SetBool("Flying", false);

        }
    }
}
