using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public string description; 
    public List<EventChoice> choices;
    public List<EventAnswer> answers;
}

[System.Serializable]
public class EventChoice
{
    public string choiceText;
}
[System.Serializable]
public class EventAnswer
{
    public string answerText;
}