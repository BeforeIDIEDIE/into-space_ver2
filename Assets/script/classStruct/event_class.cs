using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public string description; 
    public List<EventChoice> choices;
    public List<EventAnswer> answer;
}

[System.Serializable]
public class EventChoice
{
    public string choiceText;
}

public class EventAnswer
{
    public string answerText;
}