using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelResult : MonoBehaviour
{
    public GameObject ResultPanel;
    public TextMeshProUGUI ResultText;

    [SerializeField] private TextAsset LevelTextsJson;
    private List<LevelRankData> m_LevelRankList;

    [Serializable]
    public class LevelRankData
    {
        public int Level;
        public string S;
        public string A;
        public string B;
        public string C;
    }

    private void Awake()
    {
        m_LevelRankList = new List<LevelRankData>(JsonUtility.FromJson<LevelRankList>(LevelTextsJson.text).Levels);
    }

    [Serializable]
    private class LevelRankList
    {
        public LevelRankData[] Levels;
    }
    private void Start()
    {
        //ResultPanel.SetActive(false);

        EventManager.Instance.RegisterEvent(LevelResultEventArgs.EventId, OnLevelResult);
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnRegisterEvent(LevelResultEventArgs.EventId, OnLevelResult);
    }

    private void OnLevelResult(object sender, EventArgs e)
    {
        var args = e as LevelResultEventArgs;
        if (args == null) return;

        if (args.Show)
            ShowResult(args.Data);
        else
            HideResult();
    }

    public void ShowResult(LevelResultData data)
{
    ResultPanel.SetActive(true);

        string rank = data.Rank;

        int levelIndex = Mathf.Clamp(data.Level - 1, 0, m_LevelRankList.Count - 1);
    LevelRankData levelData = m_LevelRankList[levelIndex];

    string textToShow = rank switch
    {
        "S" => levelData.S,
        "A" => levelData.A,
        "B" => levelData.B,
        "C" => levelData.C,
        _ => levelData.C
    };

    ResultText.text = textToShow;
    LayoutRebuilder.ForceRebuildLayoutImmediate(ResultPanel.GetComponent<RectTransform>());
}

    public void HideResult()
    {
        ResultPanel.SetActive(false);
        ResultText.text = string.Empty;
    }
}
