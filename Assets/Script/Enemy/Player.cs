using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private InputManager inputManager;

    public float moveSpeed = 7f;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        inputManager = InputManager.Instance;
    }


    private void FixedUpdate()
    {
        if(inputManager.direction != Vector3.zero)
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);

            Vector3 moveDirection = new Vector3(inputManager.direction.x, 0, inputManager.direction.y);
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Run", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            _animator.SetBool("Death", true);
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", false);

            LevelManager.Instance.isGameOn = false;
            LevelManager.Instance.Respawn();
            Debug.Log("You just caught");
        }
        else if(collision.transform.tag == "Finish")
        {
            LevelManager.Instance.Respawn();
            Debug.Log("Congrats!! You've finished the level");
        }
    }
}
