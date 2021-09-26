using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicFader : MonoBehaviour
{
    private SpriteRenderer spriteToFade;
    [SerializeField, Range(0, 10)]
    private float duration; //duration of fade
    [SerializeField, Range(0, 10)]
    private float timer; //timer until fade starts
    private void Awake()
    {
        spriteToFade = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(FadeTimer(spriteToFade, duration));
    }

    public void FadeOut(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        }
    }

    IEnumerator FadeTimer(SpriteRenderer MyRenderer, float duration) 
    {
        yield return new WaitForSeconds(timer);
        FadeOut(spriteToFade, duration);
    }
}
