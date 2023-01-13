using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bird))]
public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.Space;

    private Bird bird;

    private void Awake()
    {
        bird = GetComponent<Bird>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(key))
        {
            bird.Jump();
        }
    }
}
