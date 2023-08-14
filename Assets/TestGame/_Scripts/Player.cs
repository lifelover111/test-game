using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject muzzlePrefab;
    public float speed = 2f;
    AudioSource audioSource;
    SkeletonAnimation _sAnim;
    public SkeletonAnimation sAnim { get { return _sAnim; } private set { _sAnim = value; } }

    bool _isLoose = false;
    public bool isLoose {get { return _isLoose; } private set { _isLoose = value; } }
    bool _isFinished = false;
    public bool isFinished { get { return _isFinished; } private set { _isFinished = value; } }

    private void Awake()
    {
        sAnim = GetComponentInChildren<SkeletonAnimation>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        float tmpSpeed = speed;
        sAnim.AnimationName = "shoot";
        sAnim.AnimationState.Event += (Spine.TrackEntry trackEntry, Spine.Event e) => {
            GameObject go = Instantiate(muzzlePrefab);
            go.transform.position = transform.position + 3 * Vector3.right + 1.5f * Vector3.up;
        };
        sAnim.AnimationState.Complete += (Spine.TrackEntry trackEntry) => {
            if (sAnim.AnimationName != "shoot")
                return;
            sAnim.loop = true;
            sAnim.AnimationName = "run";
            speed = tmpSpeed;
        };

        sAnim.AnimationName = "run";
    }

    void Update()
    {
        gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            isFinished = true;
            return;
        }
        else if(collision.gameObject.tag == "Win")
        {
            speed = 0;
            GameManager.instance.Invoke("Win", 1f);
            return;
        }
        isLoose = true;
        sAnim.loop = false;
        sAnim.AnimationName = "loose";
        speed = 0;
        GameManager.instance.Invoke("Defeat", 2f);
    }

    public void OnEnemyClick()
    {
        if (isLoose)
            return;
        speed = 0;
        sAnim.loop = false;
        sAnim.AnimationName = "shoot";
        if (sAnim.AnimationName == "shoot")
            audioSource.Play();
    }
}
