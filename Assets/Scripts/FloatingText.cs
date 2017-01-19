using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    private Text damageText;
    public Animator animator;

     void Start()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float timeDestroy = clipInfo[0].clip.length;
        damageText = animator.GetComponent<Text>();
        Destroy(gameObject, timeDestroy);
    }
    
    public void setText(string text){
        animator.GetComponent<Text>().text = text;
    }

}
