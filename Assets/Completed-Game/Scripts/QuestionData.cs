using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum SubmissionResult
{
    None,
    Correct,
    Partial,
    Incorrect,
    Skipped
}

[CreateAssetMenu(fileName = "New Question", menuName = "Pinball/Question", order = 0)]
public class QuestionData : ScriptableObject
{

    [Serializable]
    public class ResponseOption
    {
        public string text;
        public bool correct = false;
    }

    [SerializeField] private string question = "";
    [SerializeField] private bool isMultiselect = false;
    [SerializeField] private ResponseOption[] choices = {};

    [SerializeField] private UnityEvent onSuccess;
    [SerializeField] private UnityEvent onPartialSuccess;
    [SerializeField] private UnityEvent onFail;
    [SerializeField] private UnityEvent onSkip;
    
    [Min(1)]
    [SerializeField] private int allowedAttempts = 1;

    public string Question => question;
    public bool IsMultiselect => isMultiselect;
    public ResponseOption[] Choices => choices;
    public int AllowedAttempts => allowedAttempts;
    
    private SubmissionResult EvaluateSelection(bool[] selection)
    {
        bool correct = true;
        bool isPartial = false;
        
        for (int i = 0; i < selection.Length; i++)
        {
            correct &= selection[i] == choices[i].correct; // Incorrect choice
            isPartial |= selection[i] && choices[i].correct; // Has at least a partially correct answer
        }

        return correct ? SubmissionResult.Correct : (isPartial ? SubmissionResult.Partial : SubmissionResult.Incorrect);
    }

    public SubmissionResult SubmitSelection(bool[] selection)
    {
        // TODO: Record answer statistics here as stretch goal
        return EvaluateSelection(selection);
    }

    public void FinishProblem(SubmissionResult submission)
    {
        switch (submission)
        {
            case SubmissionResult.Correct:
                onSuccess.Invoke();
                break;
            case SubmissionResult.Partial:
                onPartialSuccess.Invoke();
                break;
            case SubmissionResult.Incorrect:
                onFail.Invoke();
                break;
            case SubmissionResult.Skipped:
                onSkip.Invoke();
                break;
        }
    }

}
