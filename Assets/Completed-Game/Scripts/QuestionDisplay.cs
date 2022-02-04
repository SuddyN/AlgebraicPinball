using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDisplay : MonoBehaviour
{

    [SerializeField] private Text questionTextObject;
    [SerializeField] private Text remainingAttemptsText;
    [SerializeField] private QuestionResponse optionPrefab;
    [SerializeField] private ToggleGroup toggleGroup;

    [SerializeField] private GameObject submitButton;
    [SerializeField] private GameObject retryButton;
    
    private int remainingAttempts = 0;

    private QuestionData currentQuestion;
    private SubmissionResult latestSubmissionResult = SubmissionResult.None;
    private List<QuestionResponse> options = new List<QuestionResponse>();

    public void Display(QuestionData question)
    {

        if (gameObject.activeSelf) return; // Do not redisplay if already active
        
        remainingAttempts = question.AllowedAttempts;
        currentQuestion = question;

        questionTextObject.text = question.Question;

        for (int i = 0; i < question.Choices.Length; i++)
        {
            QuestionResponse inst = Instantiate(optionPrefab, transform);
            inst.Initialize(question.Choices[i].text, question.IsMultiselect ? null : toggleGroup);
            inst.transform.position = transform.position + new Vector3(0, i * -36 + 24, 0);
            options.Add(inst);
        }
        
        UpdateRemainingCount();
        
        gameObject.SetActive(true);
    }

    public void Submit()
    {
        bool[] selection = new bool[options.Count];
        for (int i = 0; i < selection.Length; i++)
        {
            selection[i] = options[i].IsSelected();
            options[i].Hide();
        }
        
        latestSubmissionResult = currentQuestion.SubmitSelection(selection);
        Debug.Log(latestSubmissionResult);
        
        remainingAttempts--;
        UpdateRemainingCount();
        
        if (remainingAttempts < 1 || latestSubmissionResult == SubmissionResult.Correct)
        {
            // Finish the problem display
            currentQuestion.FinishProblem(latestSubmissionResult);
            Clear();
        }
        else
        {
            // Notify the user they were not correct, prompt to retry or skip
            retryButton.SetActive(true);
            submitButton.SetActive(false);
        }
    }

    public void Retry()
    {

        foreach (var option in options)
        {
            option.Reset();
            option.Show();
        }
        retryButton.SetActive(false);
        submitButton.SetActive(true);
    }

    public void Skip()
    {
        currentQuestion.FinishProblem(SubmissionResult.Skipped);
        Clear();
    }

    private void UpdateRemainingCount()
    {
        remainingAttemptsText.text = "Attempts Remaining " + remainingAttempts;
    }

    private void Clear()
    {
        currentQuestion = null;
        latestSubmissionResult = SubmissionResult.None;

        foreach (var option in options)
        {
            Destroy(option.gameObject);
        }
            
        options.Clear();

        remainingAttempts = 0;
        retryButton.SetActive(false);
        submitButton.SetActive(true);
        
        gameObject.SetActive(false);
    }
}