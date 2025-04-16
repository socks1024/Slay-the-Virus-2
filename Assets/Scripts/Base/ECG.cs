using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECG : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public Transform endpos;
    public Transform startpos;
    public float speed=0.5f;

   
    private void Update()
    {
        image1.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        image2.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if (image1.transform.position.x > endpos.position.x)
        {
            image1.transform.position = startpos.position;
        }

        if (image2.transform.position.x > endpos.position.x)
        {
            image2.transform.position = startpos.position;
        }
    }

    
}
