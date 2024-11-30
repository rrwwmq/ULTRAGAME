using UnityEngine;
using System.Collections.Generic;

public class ShooterController : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform shootPoint; // �����, �� ������� ����� �������� ����
    public float maxShootingDistance = 100f; // ������������ ��������� ������ ����
    public float chargeTime = 2f; // ����� �������

    private float chargeDuration = 0f; // �����, �� ������� ���� ������ ������
    private bool isCharging = false; // ����, �����������, ��� ���������� �������
    private List<GameObject> bullets = new List<GameObject>(); // ������ ��������� ����

    void Update()
    {
        // ���������, ������ �� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeDuration = 0f;
        }

        // ���� ������ ���� ������, ����������� ����� �������
        if (isCharging)
        {
            chargeDuration += Time.deltaTime;
            if (chargeDuration > chargeTime)
            {
                chargeDuration = chargeTime; // ������������ ������������ ����� �������
            }
        }

        // ���������, �������� �� ����� ������ ����
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            FireBullet();
            isCharging = false;
        }

        // ������� ������ ����, ���� �� ������ 5
        if (bullets.Count > 5)
        {
            Destroy(bullets[0]); // ���������� ������ ���� � ������
            bullets.RemoveAt(0); // ������� ������ �� ������������ ���� �� ������
        }
    }

    private void FireBullet()
    {
        // ������� ����
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // ������������ ���� �������� � ����������� �� ������� �������
        float shootingForce = Mathf.Lerp(0, maxShootingDistance, chargeDuration / chargeTime);

        // ��������� ���� � ����
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(shootPoint.forward * shootingForce, ForceMode.Impulse);

        // ��������� ���� � ������
        bullets.Add(bullet);
    }
}
