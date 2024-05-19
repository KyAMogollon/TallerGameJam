using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator anim;
    private void Awake() => anim = GetComponent<Animator>();
    private void OnEnable()
    {
        StartCoroutine(FadeOver());
    }

    IEnumerator FadeOver()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
