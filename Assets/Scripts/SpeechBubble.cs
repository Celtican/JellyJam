using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SpeechBubble : MonoBehaviour
{
	public float charactersPerSecond = 40f;
	public float timeDelayAfterTextComplete = 1f;
	public UnityEvent onComplete;

	private TMP_Text textContainer;
	private string targetText = string.Empty;
	private float startTime;
	private bool isSpeaking = false;

	private void Awake() {
		textContainer = GetComponentInChildren<TMP_Text>();
	}

	private void Update()
	{
		if (!isSpeaking) return;
		
		float timeSinceStart = Time.time - startTime;
		int numCharacters = GetNumVisibleCharacters();
		if (numCharacters < targetText.Length) {
			textContainer.text = targetText.Remove(numCharacters);
		} else {
			textContainer.text = targetText;
			if (timeSinceStart - (1 / charactersPerSecond * targetText.Length) > timeDelayAfterTextComplete) {
				isSpeaking = false;
				onComplete.Invoke();
			}
		}
	}

	public void Speak(string text) {
		textContainer.text = string.Empty;
		startTime = Time.time;
		targetText = text.Trim();
		isSpeaking = true;
	}

	public void InterruptSpeaking()
	{
		if (!isSpeaking) return;
		timeDelayAfterTextComplete = 0;
		if (isSpeaking && GetNumVisibleCharacters() < targetText.Length) {
			targetText = textContainer.text.TrimEnd() + "-";
		}
	}

	public void HideBubble() {
		GetComponent<Animator>().SetTrigger("Disappear");
	}
	public void OnBubbleEnd() {
		Destroy(gameObject);
	}

	private int GetNumVisibleCharacters() {
		return Mathf.CeilToInt((Time.time - startTime) * charactersPerSecond);
	}
}