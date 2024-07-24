using System.Collections;
using UnityEngine;
using Animancer;


public class MobAI : MonoBehaviour
{
    [SerializeField] internal AnimancerComponent animancer;
    [SerializeField] internal AnimationClip standingAndWavingTail;
    [SerializeField] internal AnimationClip standingAndBlinking;
    [SerializeField] internal AnimationClip eating;
    [SerializeField] internal AnimationClip walking;
    [SerializeField] internal AnimationClip sleeping;
    [SerializeField] internal AnimationClip chewing;

    [SerializeField] private int hunger = 10000;
    [SerializeField] private int sleepiness = 10000;

    enum State {
        Idling,
        Sleeping,
        Eating,
        Wandering,
        Chewing
    }

    private State state;

    void Start()
    {
        state = State.Idling;
        StartCoroutine(IdlingAction());
    }

    private IEnumerator FindFood() {
        state = State.Eating;

        animancer.Play(walking);
        yield return new WaitForSeconds(Random.Range(5, 10));

        animancer.Play(eating);
        yield return new WaitForSeconds(eating.length);
        animancer.Play(chewing);
        yield return new WaitForSeconds(chewing.length);

        state = State.Idling;
    }

    private IEnumerator IdlingAction() {
        while (true) {
            int action = Random.Range(0, 3);
            if (state == State.Idling) {
                if (action == 0) {
                    animancer.Play(standingAndBlinking).Time = 0;
                } else if (action == 1) {
                    animancer.Play(standingAndWavingTail).Time = 0;
                } else if (action == 2) {
                    StartCoroutine(Wandering());
                }
            }
            yield return new WaitForSeconds(Random.Range(2, 6));
        }
    }

    private IEnumerator Wandering() {
        state = State.Wandering;
        animancer.Play(walking);
        yield return new WaitForSeconds(Random.Range(2, 5));
        state = State.Idling;
    }

    private IEnumerator Sleeping() {
        state = State.Sleeping;
        animancer.Play(sleeping);
        yield return new WaitForSeconds(Random.Range(10, 20));
        sleepiness = 10000;
        state = State.Idling;
    }

    void Update() {
        if (hunger > 0 && state == State.Wandering) {
            hunger -= Random.Range(3, 7);
        } else if (hunger > 0) {
            hunger -= Random.Range(1, 3);
        }
        if (sleepiness > 0) {
            sleepiness -= Random.Range(1, 3);
        }

        if (hunger <= 0 && state == State.Idling) {
            StartCoroutine(FindFood());
        }

        if (sleepiness <= 0 && state == State.Idling) {
            StartCoroutine(Sleeping());
        }
    }
}