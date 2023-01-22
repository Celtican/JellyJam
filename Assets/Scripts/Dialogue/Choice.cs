using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    private Button button;
    private TMP_Text tmpText;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        tmpText = GetComponentInChildren<TMP_Text>();
        button = GetComponentInChildren<Button>();
    }

    public void SetText(string text)
    {
        tmpText.text = text;
    }

    public void AddListenerOnClick(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void PlayHideAnimation()
    {
        animator.SetBool("Disappear", true);
    }

    public void PlayChooseAnimation()
    {
        animator.SetBool("Chosen", true);
    }
}
