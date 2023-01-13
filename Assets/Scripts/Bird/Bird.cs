using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float flyPower;
    [SerializeField] private float fallPower;
    [SerializeField] private float maxVelocity;
    [Header("Components")]
    [SerializeField] private new Rigidbody2D rigidbody;

    public float Velocity
    {
        get => rigidbody.velocity.y;
    }
    public bool Disabled { get; private set; }
    public bool Dead { get; private set; }


    public event System.Action<Bird> OnDie;
    public event System.Action<Bird> OnReset;
    public event System.Action<Bird> OnPoint;


    public void Jump()
    {
        if (Disabled)
            return;
        rigidbody.velocity = Vector2.up * flyPower;
    }
    public void Restart()
    {
        Disabled = false;
        Dead = false;

        transform.position = new Vector3(0, Random.Range(-2, 2));

        OnReset?.Invoke(this);
    }

    private void Die()
    {
        if (Dead)
            return;
        rigidbody.velocity = Vector3.zero;
        Disabled = true;
        Dead = true;

        OnDie?.Invoke(this);
    }
    private void Hit()
    {
        if (Disabled)
            return;
        rigidbody.velocity = new Vector2(0, -Mathf.Abs(rigidbody.velocity.y));
        Disabled = true;    
    }
    private void Point()
    {
        OnPoint?.Invoke(this);
    }


    private void FixedUpdate()
    {
        if (Dead)
            return;
        transform.right = new Vector3(1, rigidbody.velocity.y / maxVelocity, 0).normalized;
        rigidbody.velocity += Vector2.down * fallPower * Time.fixedDeltaTime;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Trigger trigger))
        {
            switch (trigger.Type)
            {
                case Trigger.Types.Ground:
                    Die();
                    break;
                case Trigger.Types.Obstacle:
                    Hit();
                    break;
                case Trigger.Types.Point:
                    Point();
                    break;
            }
        }
    }
}
