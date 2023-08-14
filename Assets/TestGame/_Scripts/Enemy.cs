using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    SkeletonAnimation sAnim;
    public float speed = 1f;
    public event System.Action OnClick = new System.Action(() => { });
    bool isIdle = true;
    bool isProvoked = false;

    private void Awake()
    {
        sAnim = GetComponentInChildren<SkeletonAnimation>();
    }
    private void Start()
    {
        EventManager.instance.AddOnEnemyClickListener(ref OnClick);
        sAnim.AnimationName = "idle";
    }

    private void Update()
    {
        if (!isProvoked && (transform.position - GameManager.instance.player.transform.position).magnitude <= Camera.main.orthographicSize * 2.7f)
        {
            isProvoked = true;
            sAnim.loop = false;
            sAnim.AnimationName = "angry";
            sAnim.AnimationState.Complete += (Spine.TrackEntry trackEntry) => {
                if (sAnim.AnimationName != "angry")
                    return;
                sAnim.loop = true;
                sAnim.AnimationName = "run";
                isIdle = false; 
            };
        }
        float currentSpeed = isIdle||GameManager.instance.player.isLoose ? 0 : speed;
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        if (GameManager.instance.player.isLoose)
        {
            sAnim.loop = true;
            sAnim.AnimationName = "win";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = 0;
        sAnim.loop = true;
        sAnim.AnimationName = "win";
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke();
        EventManager.instance.AddOnShootEventListener(() => { if (this != null) Die(); });
    }


    void Die()
    {
        GameObject go = Instantiate(explosionPrefab);
        go.transform.position = transform.position + 2.5f*Vector3.up;
        Destroy(gameObject);
    }
}
