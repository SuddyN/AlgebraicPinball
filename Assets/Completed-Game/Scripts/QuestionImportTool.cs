using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class QuestionImportTool : MonoBehaviour
{
    [MenuItem("Pinball Game/Import/Questions")]
    public static void ImportQuestions()
    {
        string path = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
        string text = File.ReadAllText(path);
        
        string[] lines = text.Split('\n');
        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            string title = data[0];

            string[] found = AssetDatabase.FindAssets($"{title}", new [] {"Assets/Completed-GAme/DAta/Questions/" });
            if (found.Length > 0) continue; // Already exists, skip
            
            string questionText = data[1];
            string[] responses = data[2].Split(';');
            string[] correctAnswers = data[3].Split(';');
            
            QuestionData.ResponseOption[] responseOptions = new QuestionData.ResponseOption[responses.Length];
            for (int i = 0; i < responses.Length; i++)
            {
                responseOptions[i] = new QuestionData.ResponseOption();
                responseOptions[i].text = responses[i];
                responseOptions[i].correct = false;
            }

            bool isMultiselect = correctAnswers.Length > 1;
            
            for (int i = 0; i < correctAnswers.Length; i++)
            {
                int index = Int32.Parse(correctAnswers[i]);
                responseOptions[index].correct = true;
            }

            QuestionData question = QuestionData.Create(questionText, isMultiselect, responseOptions);
            AssetDatabase.CreateAsset(question, $"Assets/Completed-Game/Data/Questions/{title}.asset");
        }
        
        Debug.LogFormat("Created {0} question assets", lines.Length);
    }
}
