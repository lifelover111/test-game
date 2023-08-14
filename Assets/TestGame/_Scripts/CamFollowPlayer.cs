using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] Player player;
    
    void Update()
    {
        if(!player.isFinished && gameObject.transform.position.x - player.transform.position.x < Camera.main.orthographicSize)
        {
            transform.position += player.speed * Vector3.right * Time.deltaTime;
        }
    }

}
