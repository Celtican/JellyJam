using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public float timeBetweenPowers;
    public Image fillImage;

    private float cooldown;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        fillImage.fillAmount = -(Mathf.Clamp01(cooldown/timeBetweenPowers)-1);
        button.interactable = cooldown <= 0;
    }

    public void HypnotizePower()
    {
        cooldown = timeBetweenPowers;
        HeartController.Instance.Hypnotize(1f);
    }

    public void ReadMindPower()
    {
        cooldown = timeBetweenPowers;
    }

}