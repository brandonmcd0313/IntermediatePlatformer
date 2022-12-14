using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Tool : MonoBehaviour {
    [SerializeField] float size;
    [SerializeField] GameObject pop;
    public float startGravityScale = -0.5f;
    public float endGravityScale = 2f;
    public float duration = 2f;

    private Rigidbody2D rb;
    float homeX, homeY; float randDiff;
    bool vibra;  GameObject daddy; bool thro = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //set all values to default
        this.GetComponent<BoxCollider2D>().isTrigger = true;
        daddy = null; thro = false;
        vibra = true;
        randDiff = Random.Range(-100f, 100f);
        homeX = this.transform.position.x;
        homeY = this.transform.position.y;
    }

    public void stopVibration()
    {
        vibra = false;
    }

    public void thrown(GameObject play, float tf)
    {
        //Change the rigidbody to be kinematic
        //this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        daddy = play; //cold = true;
        StartCoroutine(launch(tf));
        thro = true;
        StartCoroutine(ChangeGravityScaleOverTime());
        Invoke("kill", 2.5f);
        
    }
    IEnumerator launch(float throwForce)
    {
        GameObject past = this.gameObject;
      //  running = true;
        past.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      //  past.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        if (daddy.GetComponent<PlayerController>().facingRight)
        {
            past.GetComponent<Rigidbody2D>().velocity += new Vector2(throwForce, 0);
        }
        else
        {
            past.GetComponent<Rigidbody2D>().velocity += new Vector2(-throwForce, 0);
        }
        yield return new WaitForSeconds(0.2f);
        //disable trigger so it can hit stuffs
      
        past.GetComponent<BoxCollider2D>().isTrigger = false;
        
      

    }
    private IEnumerator ChangeGravityScaleOverTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            print("AAAAA");
            float gravityScale = Mathf.Lerp(startGravityScale, endGravityScale, elapsedTime / duration);
            rb.gravityScale = gravityScale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.gravityScale = endGravityScale;
    }
    void kill()
    {
        print("DIE");
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(daddy != null && vibra)
        {
            Destroy(this.gameObject);
        }
        //keep this player on the screen!
        {

            //player bounds are -28 to 28 on x axis
            //-18 to 18 on y axis

            //while player is above the bounds of the screen
            if (this.transform.position.y > 18)
            {
                //increase/decrease their gravity scale exponentially until they are below top of screen
                Destroy(this.gameObject);
            }

            //if player goes below the screen
            if (this.transform.position.y < -18)
            {
                Destroy(this.gameObject);
            }



        }
        //keep at constant angle
        this.transform.eulerAngles = new Vector3(0, 0, 90);
        if (vibra)
        {
            //move up and down slightly
           transform.localPosition = new Vector3(homeX, homeY + (size * Mathf.Sin(Time.time + randDiff)));
        }
    }
  
    void OnCollisionEnter2D(Collision2D colsion)
    {
        if (thro)
        {
            print("Tool col");
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            
            GameObject poppy;
            GameObject col = colsion.collider.gameObject;
            print("OnCOl Tool");
            //print(col.name);
            //if col = player and cold
            if (col.tag == "Player")
            {
                poppy = Instantiate(pop);
                //daddy gets -100 points
                if (col.gameObject.name.Contains("1"))
                {
                    ScoreManager.score1 += -100;
                }
                if (col.gameObject.name.Contains("2"))
                {
                    ScoreManager.score2 += -100;
                }
                poppy.GetComponent<ScorePopUp>().setVal(-100);
                poppy.transform.position = col.transform.position;
                Destroy(this.gameObject);
            }
            //player lose 100 point

            //if col = pipe and cold
            if (col.name == "Pipe")
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
}
