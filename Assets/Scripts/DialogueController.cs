using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject speechBubblePrefab;
    public GameObject speechBubbleContainer;

    public GameObject choicePrefab;
    public GameObject choiceContainer;

    private List<Step> steps = new();
    private SpeechBubble latestSpeechBubble;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddSteps(new Step[]
        {
            new StepDialogue("Hi! It's so nice to meet you!"),
            new StepDialogue("And on such a lovely day, too!"),
        });
        
        AddChoice("Vampire!", new Step[]
        {
            new StepDialogue("Me: I'm a vampire!"),
            new StepDialogue("Oh! Erm... that's... nice...?"),
            new StepDialogue("I think...?"),
            new StepDialogue("If you're a vampire..."),
            new StepDialogue("That means you must be on the hunt..."),
            new StepDialogue("Please, spare me!"),
            new StepDialogue("Take my sister instead! She deserves it!"),
            new StepDialogue("I'm too young to die!"),
        });
        
        AddChoice("Cats?", new Step[]
        {
            new StepDialogue("Me: What's your opinion of cats?"),
            new StepDialogue("BIG fan. Seriously, all the way."),
            new StepDialogue("You will NOT believe how many cats I have."),
            new StepDialogue("There's Daeg, then there's Scout, Jonesy..."),
            new StepDialogue("...Parker, Buča, Squee, Boots... you get the idea."),
        });
        
        AddChoice("Weather?", new Step[]
        {
            new StepDialogue("Me: So how about that weather?"),
            new StepDialogue("Uh, it's fine, I guess."),
        });
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

    public void AddSteps(IEnumerable<Step> steps)
    {
        foreach (Step step in steps)
        {
            AddStep(step);
        }
    }

    public void NextStep()
    {
        print("Called NextStep() at " + Time.time);
        
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

    public void AddChoice(string choiceText, Step[] stepsOnClick)
    {
        Choice choice = Instantiate(choicePrefab, choiceContainer.transform).GetComponent<Choice>();
        choice.SetText(choiceText);
        choice.AddListenerOnClick(() =>
        {
            Interrupt();
            AddSteps(stepsOnClick);
        });
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
        public bool started = false;
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
            print("Start called at " + Time.time);
            SpeechBubble bubble = Instantiate(Instance.speechBubblePrefab, Instance.speechBubbleContainer.transform).GetComponent<SpeechBubble>();
            bubble.Speak(targetText);
            bubble.onComplete.AddListener(() =>
            {
                Instance.NextStep();
            });
            Instance.latestSpeechBubble = bubble;
        }

        public override void Update()
        {
            // we wait for the speech bubble's animator to complete
        }

        public override void End()
        {
            
        }
    }

    private class StepWait : Step
    {
        private float timeToWait;

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
