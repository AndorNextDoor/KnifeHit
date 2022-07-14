using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color color;
    public Color BaseColor;
    public void Flick()
    {
        StartCoroutine(ColorFlickering());
    }
   public IEnumerator ColorFlickering()
    {

        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = BaseColor;
    }
}
