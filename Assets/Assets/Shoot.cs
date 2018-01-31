using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject ShootThing;
    public GameObject ShootFrom;
    public float ShootSpeed;
    public float cooldown;
    public int burst;
    public float spread;
    float timer;
    SteamVR_Controller.Device device;
    public AudioSource gunshot;

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    // Use this for initialization
    public void Bang(SteamVR_Controller.Device _device)
    {
        device = _device;

        if (timer < 0)
        {
            timer = cooldown;
            device.TriggerHapticPulse(1000);
            gunshot.Play();
            for (int i = 0; i < burst; i++)
            {
                ShootAShot();
            }


        }
    }

    private void ShootAShot()
    {
        Vector3 fluffed = Random.insideUnitCircle * spread;

        GameObject shard = Instantiate(ShootThing, ShootFrom.transform.position, ShootFrom.transform.rotation);

        Rigidbody shardRB = shard.GetComponent<Rigidbody>();

        shardRB.AddForce((transform.forward+fluffed) * ShootSpeed);
    }

}
