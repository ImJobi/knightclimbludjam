using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public Rigidbody2D shrb;
    //[SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject dustCloud;
    [SerializeField] private GameObject pfStun;
    [SerializeField] private GameObject knight;
    public Vector2 mousepos;
    private Vector2 shielddir;
    public float shdist = .45f;
    Vector3 chscale = new Vector3(1f, 1f, 1f);//normal character local scale
    //Vector3 strchscale= new Vector3(1.03f, 1.17f, 1f);//stretched local scale
    private float normx = 0f;
    private float normy = 0f;
    private float degree = 0f;
    public int stun = 0;
    public float unstuntime = 0;
    public AudioClip clunk1;
    public AudioClip clunk2;
    public AudioClip clunk3;
    public AudioClip clunk4;
    public SpriteRenderer sprnd;
    public bool playing = false;
    Vector3 stshape=new Vector3(1,1,1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            //may have to replace camera.main with mainCamera testing

            //Vector3 playerPos = new Vector3(rb.transform.position.x, rb.transform.position.y, 0);
            mousepos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - rb.transform.position.x;
            mousepos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rb.transform.position.y;
            float normtot = Mathf.Abs(mousepos.x) + Mathf.Abs(mousepos.y);
            normx = mousepos.x / normtot;
            normy = mousepos.y / normtot;
            shielddir.x = normx;
            shielddir.y = normy;
            //find the angle from arctangent for drawing object in circle around mouse
            float angle = Mathf.Atan(normy / normx);
            //calculate distances from center from angle
            float cosx = Mathf.Cos(angle);
            float siny = Mathf.Sin(angle);
            degree = angle * 180 / Mathf.PI;
            //fixes 2nd half of circle
            if (normx < 0)
            {
                cosx = -cosx;
                siny = -siny;
            }
            else
            {
                degree = degree + 180;
            }

            //character flipping rough
            if (normx>.3f)
            {
                stshape.x = -Mathf.Abs(stshape.x);
            }
            else if (normx<-.3f)
            {
                stshape.x = Mathf.Abs(stshape.x);
            }
            knight.transform.localScale = stshape;
            float chdirection =stshape.x;
            if (stshape.x > 0)
            {
                chscale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                chscale = new Vector3(-1f, 1f, 1f);
            }
            
            //SHitty STRETCH OR NOT
            RaycastHit2D soonup = Physics2D.BoxCast(rb.transform.position, new Vector3(.8f, .8f, 1f), 0f, Vector3.up, .2f);
            RaycastHit2D soondown = Physics2D.BoxCast(rb.transform.position, new Vector3(.8f, .8f, 1f), 0f, Vector3.down, .2f);
            if ((soonup.collider != null && soonup.distance < .50) || (soondown.collider != null && soondown.distance < .50))
            {
                if (chdirection > 0)
                {
                    stshape = chscale + new Vector3(-Mathf.Abs(rb.velocity.y) / 80, Mathf.Abs(rb.velocity.y) / 90, 0);
                }
                else
                {
                    stshape = chscale + new Vector3(Mathf.Abs(rb.velocity.y) / 80, Mathf.Abs(rb.velocity.y) / 90, 0);
                }

            }
            else
            {
                if (chdirection > 0)
                {
                    stshape = chscale + new Vector3(-Mathf.Abs(rb.velocity.y) / 50, Mathf.Abs(rb.velocity.y) / 30, 0);
                }
                else
                {
                    stshape = chscale + new Vector3(Mathf.Abs(rb.velocity.y) / 50, Mathf.Abs(rb.velocity.y) / 30, 0);

                }
                //print(stshape);
            }

            //if (shielddir.x > .3f&&chdirection<0)
            //{
            //    print("flipping first if: " + stshape);
            //    stshape.x = -stshape.x;
            //}
            //else if(shielddir.x < -.3f && chdirection > 0)
            //{
             //   print("flipping: " + stshape);
            //    stshape.x = -stshape.x;
            //}

            //knight.transform.localScale = stshape;
            /*if(rb.velocity.y>=-3&&rb.velocity.y<=3&&stretch!=0)
            {
                stretch = 0;
                knight.transform.localScale = chscale;

            }
            else if((rb.velocity.y>2.6|| rb.velocity.y < -2.6 )&& stretch!=1)
            {
                stretch = 1;
                knight.transform.localScale = strchscale;
            }*/


            //character flipping rough
            /*if (normx>.3f)
            {
                knight.transform.localScale = new Vector3(-1.1f, 1.1f, 1f);
            }
            else if (normx<-.3f)
            {
                knight.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            }*/
            //Debug.Log(normx);
            //converts radian angle to degree angle for shield rotation

            //raycast tests
            //RaycastHit2D ray = Physics2D.BoxCast(shrb.transform.position, new Vector2(0.441936f, 0.6000607f), 0f, shielddir, .4f);// pushCast();
            //Debug.Log(ray.distance);
            //print(ray.collider);
            //ray.collider.gameObject




            //debugging
            //print("Angle: " +angle+ " Cosx: " +cosx+ " Siny: " +siny + " normx: " +normx+ " normy: " +normy);
            //rotate shield around player
            shrb.transform.position = rb.transform.position + new Vector3(cosx * shdist, siny * shdist);
            //rotate shield to right angle as it rotates around
            shrb.transform.eulerAngles = (new Vector3(0, 0, degree));
            //shield pushing
            if (stun == 0)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D ray = Physics2D.BoxCast(shrb.transform.position, new Vector2(.451936f, 0.66307f), 0f, shielddir, .375f);
                    Cursor.lockState = CursorLockMode.Confined;
                    shdist = .7f;
                    if (ray.collider != null)
                    {
                        int sound = Random.Range(0, 4);
                        float apitch = .78f + Random.Range(0, .25f);
                        GetComponent<AudioSource>().pitch = apitch;
                        if (sound == 0)
                        {
                            GetComponent<AudioSource>().clip = clunk1;
                        }
                        else if (sound == 1)
                        {
                            GetComponent<AudioSource>().clip = clunk2;
                        }
                        else if (sound == 2)
                        {
                            GetComponent<AudioSource>().clip = clunk3;
                        }
                        else if (sound == 3)
                        {
                            GetComponent<AudioSource>().clip = clunk4;
                        }
                        GetComponent<AudioSource>().Play();
                        
                        Vector2 fo = new Vector2(-normx, -normy);
                        if (rb.velocity.x > 0 && fo.x < 0 || rb.velocity.x < 0 && fo.x > 0)
                        {
                            rb.velocity = new Vector2(rb.velocity.x * .38f, rb.velocity.y);
                        }
                        if (rb.velocity.y > 0 && fo.y < 0 || rb.velocity.y < 0 && fo.y > 0)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .38f);
                        }
                        float fmult = 1 - (ray.distance * 3);
                        if (fmult < .45f)
                        {
                            fmult = .45f;
                        }
                        Quaternion quat = Quaternion.Euler(new Vector3(0, 0, degree));
                        Instantiate(dustCloud, ray.point, quat);
                        rb.AddForce(fo * fmult * 10, ForceMode2D.Impulse);
                    }
                }
            }
            else
            {
                sprnd.color = new Color(1, .92f, .21f, 1);
                if (Input.GetMouseButtonDown(0))
                {
                    //Find a way to visually print over player head later
                    var status = Instantiate(pfStun, new Vector3(rb.transform.position.x, rb.transform.position.y + .8f, 0f), rb.transform.localRotation);
                    status.transform.SetParent(rb.transform);
                }
                if (stun == 2)
                {
                    if (Time.time > unstuntime)
                    {
                        sprnd.color = new Color(1, 1, 1, 1);
                        stun = 0;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                shdist = .45f;
            }
        }
    }

    private RaycastHit2D pushCast()
    {
        RaycastHit2D box = Physics2D.BoxCast(shrb.transform.position, shrb.transform.localScale, 0f, shielddir, 2.0f);
        
        return box ;

    }

}
