using UnityEngine;

public class 敵人 : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("傷害"), Range(0, 100)]
    public float damage = 20f;

    public Transform checkPoint;

    private Rigidbody2D r2d;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(checkPoint.position, -checkPoint.up * 1.5f);
    }

    //持續觸發
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "e04")
        {
            Track(collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "e04" && collision.transform.position.y < transform.position.y + 2)
        {
            collision.gameObject.GetComponent<e04>().Damage(damage);
        }
    }

    #region  方法
    /// <summary>
    /// 隨機移動 
    /// </summary>
    private void Move()
    {
        //r2d.AddForce(new Vector2(-speed, 0));
        r2d.AddForce(-transform.right * speed);   // 區域座標 2D transform.right 右邊、transform.up 上方

        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, -checkPoint.up, 1.5f, 1 << 8);

        if (hit == false)
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 追蹤玩家
    /// </summary>
    /// <param name="target">玩家座標</param>
    private void Track(Vector3 target)
    {
        //如果 玩家在左邊 角度 = 0
        //如果 玩家在右邊 角度 = 180
        if (target.x < transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;   //new Vector3(0, 0, 0)
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    #endregion
}
