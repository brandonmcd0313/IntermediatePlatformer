using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {
    [SerializeField] float size;
    [SerializeField] GameObject pop;
    float homeX, homeY; float randDiff;
    bool vibra;  GameObject daddy;
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

    public void thrown(GameObject play)
    {
        daddy = play; //cold = true; 
        Invoke("kill", 2.5f);
    }
    void kill()
    {
        print("DIE");
        Destroy(this.gameObject);
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

    void OnCollisionEnter2D(Collision2D colsion)
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        //reassign the polygon collider
        Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
        this.gameObject.AddComponent<PolygonCollider2D>();

        GameObject poppy;
        GameObject col = colsion.collider.gameObject;
        print("OnCOl Tool");
        print(col.name);
        //if col = player and cold
        if(col.tag == "Player")
        {
            poppy = Instantiate(pop);
            //daddy gets -100 points
            if (daddy.name.Contains("1"))
            {
                ScoreManager.score1 += -100;
            }
            if (daddy.name.Contains("2"))
            {
                ScoreManager.score2 += -100;
            }
            poppy.GetComponent<ScorePopUp>().setVal(-100);
            poppy.transform.position = col.transform.position;
            Destroy(this.gameObject);
        }
        //player lose 100 point

        //if col = pipe and cold
        if(col.name == "Pipe")
        {
            poppy = Instantiate(pop);
            //daddy gets 150 points
            if (daddy.name.Contains("1"))
            {
                ScoreManager.score1 += 150;
            }
            if (daddy.name.Contains("2"))
            {
                ScoreManager.score2 += 150;
            }
            poppy.GetComponent<ScorePopUp>().setVal(150);
            poppy.transform.position = daddy.transform.position;
            Destroy(this.gameObject);
        }

    }
}
