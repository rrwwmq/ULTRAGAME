using UnityEngine;
using System.Collections.Generic;

public class ShooterController : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform shootPoint; // Точка, из которой будет вылетать пуля
    public float maxShootingDistance = 100f; // Максимальная дальность полета пули
    public float chargeTime = 2f; // Время зарядки

    private float chargeDuration = 0f; // Время, на которое была зажата кнопка
    private bool isCharging = false; // Флаг, указывающий, что происходит зарядка
    private List<GameObject> bullets = new List<GameObject>(); // Список созданных пуль

    void Update()
    {
        // Проверяем, зажата ли левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeDuration = 0f;
        }

        // Если кнопка мыши зажата, увеличиваем время зарядки
        if (isCharging)
        {
            chargeDuration += Time.deltaTime;
            if (chargeDuration > chargeTime)
            {
                chargeDuration = chargeTime; // Ограничиваем максимальное время зарядки
            }
        }

        // Проверяем, отпущена ли левая кнопка мыши
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            FireBullet();
            isCharging = false;
        }

        // Удаляем старые пули, если их больше 5
        if (bullets.Count > 5)
        {
            Destroy(bullets[0]); // Уничтожаем первую пулю в списке
            bullets.RemoveAt(0); // Удаляем ссылку на уничтоженную пулю из списка
        }
    }

    private void FireBullet()
    {
        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Рассчитываем силу выстрела в зависимости от времени зарядки
        float shootingForce = Mathf.Lerp(0, maxShootingDistance, chargeDuration / chargeTime);

        // Применяем силу к пуле
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(shootPoint.forward * shootingForce, ForceMode.Impulse);

        // Добавляем пулю в список
        bullets.Add(bullet);
    }
}
