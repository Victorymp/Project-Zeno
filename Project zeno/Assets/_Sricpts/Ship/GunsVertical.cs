using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunsVertical : MonoBehaviour
{
    public float rotationSpeed = 2f;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;
     public GameObject bullet;

    public float spread, bulletSpeed;
    public bool isFiring;
    public Transform firePoint;
    public float fireRate;

    void update()
    {
        // if (isFiring)
        // {
        //     Fire();
        // }
    }

    public void rotateGun(Transform ori)
    {
        // Rotate the object in the x direction and y direction
        // transform.Rotate(ori.transform.rotation.x, ori.transform.rotation.y, 0);
        
    }
    
    public void Fire()
    {
        // raycast to detect if the bullet hit something
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        // raycast hit object 
        RaycastHit hit;
        // target point of the raycast
        Vector3 targetPoint;
        // if the raycast hits an object
        if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
        // if the raycast does not hit an object
        else targetPoint = ray.GetPoint(1000);
        // direction of the bullet
        Vector3 direction = targetPoint - firePoint.position;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        direction.z += Random.Range(-spread, spread);
        direction.Normalize();
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));
        newBullet.transform.forward = direction;
        // add force to the bullet
        newBullet.GetComponent<Rigidbody>().AddForce(direction * 200f, ForceMode.Impulse);
        
        Destroy(newBullet, 5f);
    }

    public void setBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }
}
