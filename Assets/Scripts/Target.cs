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
        GameControl.score += 10;
        GameControl.targetsHit += 1;
        transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
        gameObject.GetComponent<Renderer>().material.color = Color.red;

        StartCoroutine("SpinAndDestroy");
        //Destroy(gameObject);


        
    }

    private IEnumerator SpinAndDestroy()
    {
        //transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
        //gameObject.GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(2f);
    }
}
