using UnityEngine;
using UnityEngine.UI;

public class DisplayCountry : MonoBehaviour
{
    [SerializeField]
    private Text txt;


    public async void RunText(string countryName)
    {
        txt.text = countryName;

        Vector3 randRot = new Vector3(0f, 0f, Random.Range(-40f, 40f));
        transform.localEulerAngles = randRot;

        float curTime = Time.time;
        float duration = 1f;
        Vector3 startPos = transform.forward;
        Vector3 targetPos = transform.forward + Vector3.forward;
        Color startColor = txt.color;
        Color newColor = txt.color;
        while (Time.time - curTime < duration)
        {
            float t = (Time.time - curTime) / duration;
            transform.position += transform.up * 1f * Time.deltaTime;

            newColor.a = Mathf.Lerp(startColor.a, 0f, t);
            txt.color = newColor;

            await System.Threading.Tasks.Task.Yield();
        }

        Destroy(gameObject);
    }
}
