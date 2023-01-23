using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SpeechBubble : MonoBehaviour
{
	public float charactersPerSecond = 40f;
	public float timeDelayAfterTextComplete = 1f;
	public UnityEvent onComplete;
	public UnityEvent onStopTalking;

	private TMP_Text textContainer;
	private string targetText = string.Empty;
	private float startTime;
	private bool complete;
	private bool talking;

	private void Awake() {
		textContainer = GetComponentInChildren<TMP_Text>();
	}

	private void Update()
	{
		if (complete) return;
		
		float timeSinceStart = Time.time - startTime;
		int numCharacters = GetNumVisibleCharacters();
		if (numCharacters < targetText.Length) {
			textContainer.text = targetText.Remove(numCharacters);
		} else {
			textContainer.text = targetText;
			if (talking)
			{
				talking = false;
				onStopTalking.Invoke();
			}
			if (timeSinceStart - (1 / charactersPerSecond * targetText.Length) > timeDelayAfterTextComplete) {
				complete = true;
				onComplete.Invoke();
			}
		}
	}

	public void Speak(string text) {
		textContainer.text = string.Empty;
		startTime = Time.time;
		targetText = text.Replace("%name%", DialogueController.Instance.npcAnimator.randomName).Trim();
		complete = false;
		talking = true;
	}

	public void InterruptSpeaking()
	{
		if (complete) return;
		timeDelayAfterTextComplete = 0;
		if (complete && GetNumVisibleCharacters() < targetText.Length) {
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