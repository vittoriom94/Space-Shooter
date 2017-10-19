using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour {
    public float speed;
    public Boundary boundary;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire = 0.0f;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            GetComponent<AudioSource>().Play();
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(new Vector3(0, shotSpawn.rotation.y, shotSpawn.rotation.z)));

            // create code here that animates the newProjectile
        }
       
    }

    private void FixedUpdate()
    {
        
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler( rigidbody.velocity.z* tilt, 0, rigidbody.velocity.x* -tilt);
    }
}
