using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject speechBubblePrefab;

    public float charactersPerSecond = 40;
    public float timeBetweenDialogue = 2;

    private List<Step> steps = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddSteps(new Step[]
        {
            new StepDialogue("Hi! It's so nice to meet you! And on such a lovely day, too!"),
            new StepDialogue("I'm a vampire!"),
            new StepDialogue("Oh! Erm, that's... nice? I think?"),
        });
    }

    public void Update()
    {
        if (steps.Count > 0) steps[0].Update();
    }

    public void AddStep(Step step)
    {
        steps.Add(step);
        if (steps.Count == 1) step.Start();
    }

    public void AddSteps(IEnumerable<Step> steps)
    {
        Step[] newSteps = steps as Step[] ?? steps.ToArray();
        this.steps.AddRange(newSteps);
        if (this.steps.Count == newSteps.Length) this.steps[0].Start();
    }

    public void NextStep()
    {
        if (steps.Count == 0) return;
        
        steps[0].End();
        steps.RemoveAt(0);
        
        if (steps.Count == 0) return;
        
        steps[0].Start();
    }

    public void NextStepAndSetStep(Step step)
    {
        if (steps.Count > 1) steps.Insert(1, step);
        else steps.Add(step);
        
        NextStep();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    [Serializable]
    public abstract class Step
    {
        public abstract void Start();
        public abstract void Update();
        public abstract void End();
    }

    private class StepDialogue : Step
    {
        public string targetText;

        public StepDialogue(string targetText)
        {
            this.targetText = targetText;
        }

        public override void Start()
        {
            SpeechBubble bubble = Instantiate(Instance.speechBubblePrefab, Instance.transform).GetComponent<SpeechBubble>();
            bubble.Speak(targetText);
            bubble.onDialogueComplete.AddListener(() => Instance.NextStep());
        }

        public override void Update()
        {
            
        }

        public override void End()
        {
            
        }
    }

    private class StepWait : Step
    {
        private float timeToWait;

        public StepWait() : this(Instance.timeBetweenDialogue)
        {
        }
        
        public StepWait(float timeToWait)
        {
            this.timeToWait = timeToWait;
        }
        
        public override void Start()
        {
            
        }

        public override void Update()
        {
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0) Instance.NextStep();
        }

        public override void End()
        {
            
        }
    }
}
