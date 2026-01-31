using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private bool isChecking;
    private Coroutine checkCoroutine;


    void Update()
    {
        if (rb.linearVelocity == Vector2.zero)
        {
            if (isChecking == false)
            {
                isChecking = true;
                checkCoroutine = StartCoroutine(CountTime());
            }
        }
        else if (rb.linearVelocity != Vector2.zero)
        {
            if (checkCoroutine != null)
            {
                StopCoroutine(checkCoroutine);
                checkCoroutine = null;
            }
            isChecking = false;
        }
    }
    private IEnumerator CountTime()
    {
        yield return new WaitForSeconds(4f);
        if (rb.linearVelocity == Vector2.zero)
        {
            rb.position = new Vector2(0f, 4f);
        }
        isChecking = false;
    }
}
