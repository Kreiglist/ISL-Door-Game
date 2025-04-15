using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "QuestionsData", order = 1)]

public class QuestionsScriptable : ScriptableObject
{
    public string quizName;
    public List<Question> questions;
}
