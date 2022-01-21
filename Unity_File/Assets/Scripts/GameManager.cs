using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    BeginningState,
    HappinessState,
    CleaningState,
    EndingState
}

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;
    public static GameManager Instance => s_Instance;

    public GameObject Player;

    public GameObject Tutorial;
    public float tutorialTime=5.0f;

    public float happinessStateTotalTime;
    public float happinessStateTime;
    public float cleaningStateTime;

    [HideInInspector] public GameState state;

    float m_currentStateTime;
    public float CurrentStateTime => m_currentStateTime;

    public int HappinessValueForNextStage = 5;

    public int MoodValue_max = 10;
    int currentMoodValue;

    public int CleanValue_max = 12;
    int currentCleanValue;

    List<HideArea> hideAreas = new List<HideArea>();

    public void InitCleanValue()
    {
        currentCleanValue = Mathf.Clamp(currentCleanValue + 1, 0, CleanValue_max);
    }

    public void AddHideAreas(HideArea area)
    {
        hideAreas.Add(area);
    }

    public void ShowHideAreaHint()
    {
        //foreach(HideArea area in hideAreas)
        //{
        //    area.ShowHint();
        //}
    }

    public void DisableHideAreaHint()
    {
        foreach (HideArea area in hideAreas)
        {
            area.DisableHint();
        }
    }

    public void ChangeMood(int amount)
    {
        currentMoodValue = Mathf.Clamp(currentMoodValue + amount, 0, MoodValue_max);
        UI_MoodBar.Instance.SetValue(currentMoodValue / (float)MoodValue_max);

        if (state == GameState.BeginningState || state == GameState.HappinessState)
        {
            MusicManager.Instance.PlayBrake();
        }
    }

    public void ChangeClean(int amount)
    {
        currentCleanValue = Mathf.Clamp(currentCleanValue + amount, 0, CleanValue_max);
        UI_CleanBar.Instance.SetValue(currentCleanValue / (float)CleanValue_max);
    }

    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }

        s_Instance = this;
    }

    private void Start()
    {
        currentMoodValue = 0;
        Tutorial.SetActive(true);

        UI_MoodBar.Instance.SetValue(currentMoodValue / (float)MoodValue_max);

        UI_CleanBar.Instance.SetActive(false);

        state = GameState.BeginningState;

        m_currentStateTime = happinessStateTotalTime;
        ProcessHappinessState();
    }

    void Update()
    {
        m_currentStateTime -= Time.deltaTime;
        if(Time.time > tutorialTime)
        {
            Tutorial.SetActive(false);
        }

        //happiness stage with reached Goal
        if(currentMoodValue == HappinessValueForNextStage && state == GameState.BeginningState)
        {
            m_currentStateTime = happinessStateTime;
            state = GameState.HappinessState;
        }

        //happiness stage with un-reached Goal
        if (m_currentStateTime < 0 && state == GameState.BeginningState)
        {
            m_currentStateTime = cleaningStateTime;

            state = GameState.CleaningState;
            ProcessCleaningState();
        }

        //buffer time for reach the hapiness goal
        if (m_currentStateTime < 0 && state == GameState.HappinessState)
        {
            m_currentStateTime = cleaningStateTime;

            state = GameState.CleaningState;
            ProcessCleaningState();          
        }

        //cleaning stage time
        if(m_currentStateTime < 0 && state == GameState.CleaningState)
        {
            state = GameState.EndingState;
            ProcessEndingState();
        }
    }

    void ProcessHappinessState()
    {
        MusicManager.Instance.PlayHappinessStateClip();

        UI_MoodBar.Instance.SetActive(true);
    }

    void ProcessCleaningState()
    {
        UI_CleanBar.Instance.SetValue(currentCleanValue / (float)CleanValue_max);
        StartCoroutine(MusicManager.Instance.PlayCleaningStateClip());      
    }

    void ProcessEndingState()
    {
        SceneManager.LoadScene("EndScene");
    }
}
