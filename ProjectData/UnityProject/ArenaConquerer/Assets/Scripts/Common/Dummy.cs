using System.Collections;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Health healthRef = new Health();
    private const float HP = 100f;
    private float currentHP;
    private bool isAttackStopped = true;
    private bool isGettingHealed = false;
    [SerializeField]
    private TextMeshProUGUI logText;

    private float delayTime = 1f;

    private void Start()
    {
        logText.text = healthRef.SetTotalHealth(HP).ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delayTime = 1f;
            if (isGettingHealed)
            {
                ResetDummyHP();
            }
            isAttackStopped = false;
            currentHP = healthRef.DamageHealth(HP * 0.15f);
            logText.text = currentHP.ToString();
        }

        if (currentHP < HP && !isAttackStopped)
        {
            if (delayTime > 0)
            {
                delayTime -= Time.deltaTime;
            }
            else
            {
                isAttackStopped = true;
                StopCoroutine(StartHealthRecovery());
                StartCoroutine(StartHealthRecovery());
            }
        }
    }

    public IEnumerator StartHealthRecovery()
    {
        if (!isAttackStopped)
            yield return null;

        isGettingHealed = true;

        while (currentHP < HP && isGettingHealed)
        {
            if (isAttackStopped && isGettingHealed)
            {
                currentHP++;
                logText.text = currentHP.ToString();
                yield return new WaitForEndOfFrame();
            }
            else if (!isAttackStopped)
            {
                ResetDummyHP();
            }
        }

        healthRef.SetTotalHealth(HP);
        logText.text = currentHP.ToString();
        isGettingHealed = false;
    }

    private void ResetDummyHP()
    {
        StopCoroutine(StartHealthRecovery());
        currentHP = HP;
        healthRef.SetTotalHealth(HP);
        logText.text = currentHP.ToString();
        isGettingHealed = false;
    }
}
