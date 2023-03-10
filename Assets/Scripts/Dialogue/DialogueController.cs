using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    [FormerlySerializedAs("speechBubblePrefab")] public GameObject npcSpeechBubblePrefab;
    public GameObject playerSpeechBubblePrefab;
    public GameObject speechBubbleContainer;

    public GameObject choicePrefab;
    public GameObject choiceContainer;

    public GameObject npcPrefab;
    public Vector3 npcSpawnPosition;
    public NpcAnimator npcAnimator;
    public TraitsList npcTraits;

    public List<DialogueList> npcEnterDialogues;
    public List<DialogueList> npcDepartDialogues;
    public List<DialogueList> npcPassDialogues;
    public List<SpriteRenderer[]> test;

    private List<Step> steps = new();
    private SpeechBubble latestSpeechBubble;

    private List<QuestionObject> questions = new();
    private List<Choice> activeChoices = new();

    private bool gameOver = false;
    public string epilogueScene;

    public Animator fadeOutAnimator;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        questions = new List<QuestionObject>(Resources.LoadAll<QuestionObject>("Dialogue"));
        AttemptNewNpc(false);
    }

    public void Update()
    {
        if (steps.Count > 0)
        {
            if (!steps[0].started)
            {
                steps[0].started = true;
                steps[0].Start();
            }
            steps[0].Update();
        }
    }

    public void AddStep(Step step)
    {
        step.started = false;
        steps.Add(step);
    }

    public void AddSteps(DialogueList dialogue)
    {
        foreach (Dialogue d in dialogue.speech)
        {
            AddStep(new StepDialogue(d));
        }
    }
    public void AddSteps(IEnumerable<Step> steps)
    {
        foreach (Step step in steps)
        {
            AddStep(step);
        }
    }

    public void NextStep()
    {
        if (steps.Count == 0) return;
        
        steps[0].End();
        steps.RemoveAt(0);
    }

    public void NextStepAndSetStep(Step step)
    {
        if (steps.Count > 1) steps.Insert(1, step);
        else steps.Add(step);
        
        NextStep();
    }

    public void ClearSteps()
    {
        if (steps.Count > 1) steps[0].End();
        steps.Clear();
        if (latestSpeechBubble != null) latestSpeechBubble.onComplete.RemoveAllListeners();
    }

    public void Interrupt()
    {
        ClearSteps();
        
        if (latestSpeechBubble != null) latestSpeechBubble.InterruptSpeaking();
    }

    public void ClearChoices()
    {
        foreach (Choice choice in activeChoices)
        {
            choice.PlayHideAnimation();
        }
        activeChoices.Clear();
    }

    public void AddRandomQuestion()
    {
        QuestionObject question = questions[Random.Range(0, questions.Count)];
        AddChoice(question.text, () =>
        {
            List<DialogueObject> potentialDialogue = new List<DialogueObject>(question.potentialDialogue);
            for (int i = potentialDialogue.Count - 1; i >= 0; i--)
            {
                Traits.Type associatedTrait = potentialDialogue[i].associatedTrait;
                if (associatedTrait == Traits.Type.None) continue;

                bool npcEnjoys = npcTraits.GetTraitOfType(associatedTrait).value >= 5.5f;
                if (npcEnjoys != potentialDialogue[i].npcEnjoysTrait) potentialDialogue.RemoveAt(i);
            }

            DialogueObject dialogue;
            if (potentialDialogue.Count == 0)
            {
                // oh no!
                dialogue = question.potentialDialogue[0];
            }
            else
            {
                dialogue = potentialDialogue[Random.Range(0, potentialDialogue.Count)];
            }
            
            HeartController.Instance.AddHearts(dialogue.heartsGained);
            foreach (DialogueObject.Speech speech in dialogue.speech)
            {
                AddStep(new StepDialogue(speech.isPlayer, speech.text));
            }

            if (dialogue.followUps.Count == 0)
            {
                AddRandomQuestions();
            } else {
                foreach (FollowUpObject followUp in dialogue.followUps)
                {
                    AddChoice(followUp.text, () =>
                    {
                        HeartController.Instance.AddHearts(followUp.heartsGained);
                        foreach (DialogueObject.Speech speech in followUp.speech)
                        {
                            AddStep(new StepDialogue(speech.isPlayer, speech.text));
                        }
                        AddRandomQuestions();
                    });
                }
            }
        });
    }

    public void AddRandomQuestions()
    {
        for (int i = 0; i < 3; i++) AddRandomQuestion();
    }

    public void AddChoice(string choiceText, UnityAction onClick)
    {
        Choice choice = Instantiate(choicePrefab, choiceContainer.transform).GetComponent<Choice>();
        choice.SetText(choiceText);
        choice.AddListenerOnClick(() => choice.PlayChooseAnimation());
        choice.AddListenerOnClick(Interrupt);
        choice.AddListenerOnClick(ClearChoices);
        choice.AddListenerOnClick(onClick);
        
        activeChoices.Add(choice);
    }

    public void AttemptNewNpc(bool passed)
    {
        Interrupt();
        ClearChoices();
        if (npcAnimator != null)
        {
            if (passed) AddSteps(npcPassDialogues[Random.Range(0, npcPassDialogues.Count)]);
            else AddSteps(npcDepartDialogues[Random.Range(0, npcDepartDialogues.Count)]);
            AddStep(new StepNpcExit());
        }

        if (gameOver)
        {
            AddStep(new StepEpilogue());
        }
        else
        {
            AddStep(new StepNpcEnter());
            AddSteps(npcEnterDialogues[Random.Range(0, npcEnterDialogues.Count)]);
        }
    }

    public void TimeOver()
    {
        gameOver = true;
        AttemptNewNpc(false);
        EpilogueController.ForeverAlone();
    }

    public void TurnNpc()
    {
        Interrupt();
        ClearSteps();
        AddStep(new StepEpilogue());
        EpilogueController.CalculateScore(npcAnimator.randomName, PlayerTraits.Instance != null ? PlayerTraits.Instance.playerTraitsList : null, npcTraits);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    [Serializable]
    public class Step
    {
        [NonSerialized] public bool started = false;

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void End()
        {
        }
    }

    [Serializable]
    public class StepDialogue : Step
    {
        public bool isPlayer;
        public string targetText;

        public StepDialogue(Dialogue dialogue)
        {
            isPlayer = dialogue.isPlayer;
            targetText = dialogue.text;
        }
        public StepDialogue(bool isPlayer, string targetText)
        {
            this.isPlayer = isPlayer;
            this.targetText = targetText;
        }

        public override void Start()
        {
            if (!isPlayer) Instance.npcAnimator.StartTalking();

            GameObject prefabToUse = isPlayer ? Instance.playerSpeechBubblePrefab : Instance.npcSpeechBubblePrefab;
            
            SpeechBubble bubble = Instantiate(prefabToUse, Instance.speechBubbleContainer.transform).GetComponent<SpeechBubble>();
            bubble.Speak(targetText);
            bubble.onStopTalking.AddListener(() =>
            {
                Instance.npcAnimator.StopTalking();
            });
            bubble.onComplete.AddListener(() =>
            {
                Instance.NextStep();
            });
            Instance.latestSpeechBubble = bubble;
        }
    }

    private class StepNpcEnter : Step
    {
        public override void Start()
        {
            Instance.npcAnimator = Instantiate(Instance.npcPrefab).GetComponent<NpcAnimator>();
            Instance.npcTraits = Instance.npcAnimator.GetComponent<TraitsList>();
            Instance.npcAnimator.transform.position = Instance.npcSpawnPosition;
            HeartController.Instance.ResetHearts();
        }

        public override void End()
        {
            Instance.AddRandomQuestions();
        }
    }
    
    private class StepNpcExit : Step
    {
        public override void Start()
        {
            Instance.npcAnimator.Exit();
            HeartController.Instance.ClearHearts();
        }
    }

    private class StepWait : Step
    {
        private float timeToWait;

        public StepWait(float timeToWait)
        {
            this.timeToWait = timeToWait;
        }
        
        public override void Update()
        {
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0) Instance.NextStep();
        }
    }

    private class StepEpilogue : Step
    {
        public float timeLeft = 2.5f;
        
        public override void Start()
        {
            Instance.fadeOutAnimator.SetBool("FadeOut", true);
        }
        
        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) SceneManager.LoadScene(Instance.epilogueScene);
        }
    }
}
