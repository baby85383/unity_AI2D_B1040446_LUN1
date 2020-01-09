using UnityEngine;

namespace Fish
{
    public class 攝影機 : MonoBehaviour
    {
        [Header("速度"), Range(0, 10)]
        public float speed = 3;

        private Transform target;

        private void Start()
        {
            target = GameObject.Find("e04").transform;
        }

        //延遲更新 : Update 之後執行 - 攝影機追蹤、物件追蹤
        private void LateUpdate()
        {
            Vector3 cam = transform.position;
            Vector3 tar = target.position;
            tar.z = -10;
            // 舉例 hp - 0 ~ 999
            tar.y = Mathf.Clamp(tar.y, -1, 1); //數學.夾住(值，最小，最大)
            transform.position = Vector3.Lerp(cam, tar, 0.3f * Time.deltaTime * speed);
        }
    }
}