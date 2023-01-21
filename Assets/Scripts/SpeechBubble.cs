using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpeechBubble : MonoBehaviour
{
	public float charactersPerSecond = 20f;
	public float timeDelayAfterTextComplete = 1f;
	public UnityEvent onTextFilled;
	public UnityEvent onDialogueComplete;

	private TMP_Text textContainer;
	private string targetText = string.Empty;
	private float startTime;
	private bool isSpeaking = false;
	private bool complete = false;
	private bool interrupted = false;

	private void Awake() {
		textContainer = GetComponentInChildren<TMP_Text>();
	}

	private void Update() {
		if (isSpeaking) {
			float timeSinceStart = Time.time - startTime;
			int numCharacters = GetNumVisibleCharacters();
			if (numCharacters < targetText.Length) {
				textContainer.text = targetText.Remove(numCharacters);
			} else {
				textContainer.text = targetText;
				if (!complete && timeSinceStart - (1 / charactersPerSecond * targetText.Length) > timeDelayAfterTextComplete) {
					complete = true;
					onTextFilled.Invoke();
				} else if (complete && timeSinceStart - (1 / charactersPerSecond * targetText.Length) > timeDelayAfterTextComplete*2) {
					isSpeaking = false;
					if (!interrupted) onDialogueComplete.Invoke();
				}
			}
		}
	}

	public void Speak(string text) {
		textContainer.text = string.Empty;
		startTime = Time.time;
		targetText = text.Trim();
		isSpeaking = true;
		complete = false;
		interrupted = false;
	}

	public void InterruptSpeaking() {
		interrupted = true;
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