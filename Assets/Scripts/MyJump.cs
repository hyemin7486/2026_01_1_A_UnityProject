using UnityEngine;
using UnityEngine.UI;
public class MyJump : MonoBehaviour
{
    public Rigidbody rigidbody;                                   //강체 (형태와 크기가 고정된 고체) 물리 현상이 동작하게 해주는 컴포넌트
    public float power = 200f;                                    //점프 힘을 선언 함
    public Text timeUI;
    public float Timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime;
        timeUI.text = Timer.ToString();

        if (Input.GetKeyDown(KeyCode.Space))                       //스페이스 바를 누르면
        {
            power = power + Random.Range(-100, 200);
             rigidbody.AddForce(transform.up * power);             //power의 힘 값으로 위쪽(transform.up)으로 
        }

        if (this.gameObject.transform.position.y > 5 || this.gameObject.transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }
    }
}
