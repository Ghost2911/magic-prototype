using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform shield;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            shield.gameObject.SetActive(true);
        if (Input.GetKeyUp(KeyCode.Space))
            shield.gameObject.SetActive(false);
    }

}
