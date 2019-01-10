using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float moveSpeed = 0;
    private bool flymode = false;
    private float distanceX;
    private float distanceY;
    private float distanceZ;
    private float propeller;
    private Collider coll;
    private float timecount = 0;
    private int life = 1;
    private float gametime;

    public GameObject ovrcamera;

    public Text timeLabel;
    public Text lifeLabel;
    public Text gametimeLabel;
    public Text gameoverLabel;



    void Start()
    {
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        headControl();
        //moveControl();
        //캐릭터를 움직이는 함수를 프레임마다 호출한다.

    }

    void headControl()
    {
        moveSpeed = 10;

        if (ovrcamera.transform.rotation.x > 0.2)
        {
            if (this.gameObject.transform.rotation.x < 0.3)
            {
                this.gameObject.transform.Rotate(10 * Time.deltaTime, 0, 0);
            }
        }

        if (ovrcamera.transform.rotation.x < -0.2)
        {
            if (this.gameObject.transform.rotation.x > -0.3)
            {
                this.gameObject.transform.Rotate(-10 * Time.deltaTime, 0, 0);
            }
        }

        if (ovrcamera.transform.rotation.y > 0.2)
        {
            if (this.gameObject.transform.rotation.y < 0.3)
            {
                this.gameObject.transform.Rotate(0, 10 * Time.deltaTime, 0);
            }
        }

        if (ovrcamera.transform.rotation.y < -0.2)
        {
            if (this.gameObject.transform.rotation.y > -0.3)
            {
                this.gameObject.transform.Rotate(0, -10 * Time.deltaTime, 0);
            }
        }

        timeLabel.text = ovrcamera.transform.rotation.x.ToString();
        //속도를 텍스트로 출력한다.
        lifeLabel.text = ovrcamera.transform.rotation.y.ToString();

        gametimeLabel.text = transform.rotation.x.ToString();

        this.gameObject.transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        //movespeed에 따라 앞으로 이동한다.
        this.gameObject.transform.GetChild(2).Rotate(0, 20, 0);
        //프로펠러를 회전시킵니다.
    }

    void moveControl()
    {
        gametime += Time.deltaTime;
        if (flymode == false)//비행중이 아닐 때
        {
            if (Input.GetAxis("Vertical") > 0)//비행기는 후진이 안되기 때문에 앞으로 가는 것만 인식한다.
            {
                
                moveSpeed += Time.deltaTime;
                //속도를 높여준다.
                timecount += Time.deltaTime;
                //타임카운트를 올려준다.
            }

            else
            {
                if (timecount > 0)
                {
                    timecount -= Time.deltaTime; //입력중이 아니면 타임카운트를 줄여준다.
                }
            }

            if (timecount > 3)//3초 이상 위키를 눌렀으면
            {
                coll.attachedRigidbody.useGravity = false;//중력을 꺼준다.
                flymode = true;//비행중으로 바꾼다.
            }
        }

        else if (flymode == true)//비행줄일때
        {
            

            if (Input.GetAxis("Vertical") > 0)//위키를 눌렀을 때
            {
                if (moveSpeed < 70)
                {
                    moveSpeed += Time.deltaTime*2;//스피드가 70을 넘지 않으면 속도를 올린다.
                }
            }

            else if (Input.GetAxis("Vertical") < 0)//아래키를 눌렀을때
            {
                if (moveSpeed > 1)
                {
                    moveSpeed -= Time.deltaTime;//스피드가 1보다 크다면 속도를 낮춘다.
                }
            }

            this.gameObject.transform.Rotate(0,Input.GetAxis("Horizontal") * Time.deltaTime * -10,0);
            //왼쪽 조이스틱으로 좌우회전

            //오른쪽 조이스틱으로 기울기를 조절
            if (Input.GetAxis("RVertical") > 0)// 위키를 눌렀을 때
            {
                timecount = 100;
                this.gameObject.transform.Rotate(-10* Time.deltaTime, 0, 0);
            }

            else if (Input.GetAxis("RVertical") < 0)// 아래키를 눌렀을 때
            {
                timecount = 200;
                this.gameObject.transform.Rotate(10 * Time.deltaTime, 0, 0);
            }

            if (Input.GetAxis("RHorizontal") > 0)// 왼쪽키를 눌렀을 때
            {
                timecount = 300;
                this.gameObject.transform.Rotate(0, 0, 10 * Time.deltaTime);
            }

            else if (Input.GetAxis("RHorizontal") < 0)// 오른쪽키를 눌렀을 때
            {
                timecount = 400;
                this.gameObject.transform.Rotate(0, 0, -10 * Time.deltaTime);
            }
        }


        timeLabel.text = moveSpeed.ToString();
        //속도를 텍스트로 출력한다.
        lifeLabel.text = life.ToString();
        
        gametimeLabel.text = gametime.ToString();

        this.gameObject.transform.Translate(transform.forward * moveSpeed * Time.deltaTime,Space.World);
        //movespeed에 따라 앞으로 이동한다.
        this.gameObject.transform.GetChild(2).Rotate(0, 20, 0);
        //프로펠러를 회전시킵니다.

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag=="Cloud")
        {
            life -= 1;
            if(life<=0)
            {
                gameoverLabel.text = "GAME OVER";
            }
        }
    }
}