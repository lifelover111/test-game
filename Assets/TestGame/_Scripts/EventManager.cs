using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    [SerializeField] Player player;
    private void Awake()
    {
        instance = this;
    }

    public void AddOnEnemyClickListener(ref System.Action action)
    {
        action += player.OnEnemyClick;
    }

    public void AddOnShootEventListener(System.Action action)
    {
        player.sAnim.AnimationState.Event += (Spine.TrackEntry trackEntry, Spine.Event e) => { action?.Invoke(); };
    }
}
