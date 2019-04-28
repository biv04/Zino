
using UnityEngine;

public class Fade : MonoBehaviour {

    public Animator animator;

    public void FadeToLevel1 (int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
}
