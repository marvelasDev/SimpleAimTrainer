using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnMouseDown()
    {
        ShootingGameController.score += 10;
        ShootingGameController.targetsHit += 1;

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
