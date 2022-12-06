using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {
    [SerializeField] float size;
    float homeX, homeY; float randDiff;
    bool vibra;
    // Use this for initialization
    void Start()
    {
        vibra = true;
        randDiff = Random.Range(-100f, 100f);
        homeX = this.transform.position.x;
        homeY = this.transform.position.y;
    }

    public void stopVibration()
    {
        vibra = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (vibra)
        {
            //move up and down slightly
            transform.localPosition = new Vector3(homeX, homeY + (size * Mathf.Sin(Time.time + randDiff)));
        }
    }
}
