using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public GameObject canvas;
    public Image img;
    public AnimationCurve curve;
    private void Start()
    {
        StartCoroutine("Fadein");
    }
    IEnumerator Fadein()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        canvas.SetActive(false);
    }

    public void FadeTo(string _name)
    {
        StartCoroutine(Fadeout(_name));
    }
    IEnumerator Fadeout(string name)
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(name);
    }

}
