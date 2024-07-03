using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class ChickenAI : MonoBehaviour
{
    [SerializeField] internal AnimancerComponent animancer;
    [SerializeField] internal AnimationClip idling1;
    [SerializeField] internal AnimationClip idling2;
    [SerializeField] internal AnimationClip startFlying;
    [SerializeField] internal AnimationClip stopFlying;
    [SerializeField] internal AnimationClip flying;
    [SerializeField] internal AnimationClip eating;
    [SerializeField] internal AnimationClip sittingIlding1;
    [SerializeField] internal AnimationClip sittingIlding2;
    [SerializeField] internal AnimationClip sittingDown;
    [SerializeField] internal AnimationClip sittingUp;
    [SerializeField] private int hunger = 10000;
    [SerializeField] private int sleepiness = 10000;

    enum State {
        Idling,
        Sleeping,
        Eating,
        Wandering,
    }

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idling;
        animancer.Play(idling1);
        StartCoroutine(IdlingAction());
    }

    private IEnumerator IdlingAction() {
        while (true) {
            int action = Random.Range(0, 2);
            if (state == State.Idling) {
                if (action == 0) {
                    animancer.Play(idling1).Time = 0;
                    yield return new WaitForSeconds(idling1.length);
                } else if (action == 1) {
                    animancer.Play(idling2).Time = 0;
                    yield return new WaitForSeconds(idling2.length);
                }
            }
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
