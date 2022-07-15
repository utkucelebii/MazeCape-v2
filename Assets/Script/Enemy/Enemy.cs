using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _animator;
    public Vector3 startingPos;
    public Vector3 transformPos;
    private Transform player;

    public SpriteRenderer Flashlight;
    private float rotationCounter;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        player = LevelManager.Instance.player;
    }

    private void Start()
    {
        transformPos = transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        float distanceBetweenPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceBetweenPlayer < 4.5f)
        {
            CatchPlayer();
            _animator.SetBool("Run", true);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", false);
        }
        else if(distanceBetweenPlayer < 6f)
        {
            ReturnToOriginalPosition();
            StayInAlert();
        }
        else
        {
            ReturnToOriginalPosition();
            BeNuts();
        }
    }

    private void ReturnToOriginalPosition()
    {
        if (Vector3.Distance(transform.position, startingPos) < 0.15f)
        {
            _animator.SetBool("Run", false);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
            return;
        }

        _animator.SetBool("Run", false);
        _animator.SetBool("Walk", true);
        _animator.SetBool("Idle", false);

        transform.position = Vector3.Lerp(transform.position, startingPos, Time.deltaTime);
        transform.LookAt(startingPos);
    }

    private void CatchPlayer()
    {
        if (LevelManager.Instance.isGameOn)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * 1.5f);
            transform.LookAt(player);
        }
    }

    private void StayInAlert()
    {
        rotationCounter += Time.deltaTime;
        if (rotationCounter > 4)
            rotationCounter = 0;

        if(rotationCounter > 2f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transformPos.y + 45, 0), Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transformPos.y - 45, 0), Time.deltaTime);
        }
        Flashlight.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.40f);
    }

    private void BeNuts()
    {
        rotationCounter += Time.deltaTime;
        if (rotationCounter > 4)
            rotationCounter = 0;

        if (rotationCounter > 2f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transformPos.y + 45, 0), Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transformPos.y - 45, 0), Time.deltaTime);
        }
        Flashlight.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.40f);
    }
}
