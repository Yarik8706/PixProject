using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float attackSpeed;
    [SerializeField] private AudioSource shotMusic;
    private float _attackSpeedNow;

    private void Start()
    {
        _attackSpeedNow = attackSpeed;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _attackSpeedNow -= Time.deltaTime;
        var attackVector = InputController.Instance.GetAttackVector();
        _playerMovement.enabled = true;
        _playerMovement.rigidbody2d.drag = 0;
        if (attackVector == Vector2.zero) return;
        _playerMovement.rigidbody2d.drag = 1;
        _playerMovement.enabled = false;
        var angle = Mathf.Atan2(-attackVector.y, attackVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        if (!(_attackSpeedNow <= 0)) return;
        shotMusic.Play();
        _attackSpeedNow = attackSpeed;
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.rotation);
    }
}