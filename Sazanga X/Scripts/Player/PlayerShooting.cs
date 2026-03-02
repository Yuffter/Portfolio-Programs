using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletModel;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletModel, transform.position, Quaternion.identity);
            SEManager.Instance.Play(SEPath.PLAYER_BULLET_1);
        }
    }

    public void UnableToShoot() {
        canShoot = false;
    }
}
