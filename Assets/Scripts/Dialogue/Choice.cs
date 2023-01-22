using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    public void SetText(string text)
    {
        GetComponentInChildren<TMP_Text>().text = text;
    }

    public void AddListenerOnClick(UnityAction action)
    {
        GetComponent<Button>().onClick.AddListener(action);
    }
}
