using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : Singleton<UIResult>
{
    [SerializeField]
    private GameObject panelResult;
    [SerializeField]
    private TextMeshProUGUI txtCountryName;
    [SerializeField]
    private Image imgCountry;
    [SerializeField]
    private GameObject panelNext;
    [SerializeField]
    private TextMeshProUGUI txtStage;

    private Coroutine scaleCoroutine;


    public void ShowResult(SpriteRenderer render, bool isGameOver = false)
    {
        if (imgCountry.color.a > 0)
            return;

        string sprName = render.sprite.name.Substring(0, render.sprite.name.Length - 2);

        txtCountryName.text = $"{GameManager.Instance.MapIndex}. {sprName}";
        imgCountry.sprite = render.sprite;
        imgCountry.color = Color.white;

        ScaleResultEffect(isGameOver);
    }
    private void ScaleResultEffect(bool isGameOver)
    {
        panelResult.transform.localScale = Vector3.one;

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }

        scaleCoroutine = StartCoroutine(ScaleCoroutine(1f, 1.2f, 3f, isGameOver));
    }
    private IEnumerator ScaleCoroutine(float from, float to, float duration, bool isGameOver)
    { 
        Transform t = panelResult.transform;
        float elapsed = 0f;
        t.localScale = Vector3.one * from;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            float s = Mathf.Lerp(from, to, progress);
            t.localScale = Vector3.one * s;
            yield return null;
        }

        t.localScale = Vector3.one * to;
        scaleCoroutine = null;

        yield return new WaitForSecondsRealtime(1f);

        if (!isGameOver)
            NextMap_Clicked();
    }
    private void EnablePanelNext()
    {
        //panelNext.SetActive(true);
    }
    public void NextMap_Clicked()
    {
        //panelNext.SetActive(false);

        txtCountryName.text = "";
        imgCountry.color = Color.clear;

        BallSpawner.Instance.BackToPool();

        GameManager.Instance.StartNewMap();

        txtStage.text = $"Stage {GameManager.Instance.CurrentMap}";
    }
    public void ShowWinner(SpriteRenderer render)
    {
        ShowResult(render, true);
        txtStage.text = $"Stage 50";

        BallSpawner.Instance.SpawnBallWinner();
    }
}
