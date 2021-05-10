//Controls the loading and deletion of levels

using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public Level StarterLevel;
    public List<Tier> Tiers;
    
    [System.Serializable]
    public class Tier
    {
        public List<Level> levels;
    }

    private int tier = -1;
    private List<int> selectedLevelIDs = new List<int>();
    private Level currentLevel;

    private void Awake()
    {
        Instance = this;
    }

    public void EndCurrentLevel()
    {
        if (currentLevel != null) currentLevel.TransOut();
    }

    public void LoadNextLevel()
    {
        currentLevel = Instantiate(GetNextLevel());
        currentLevel.TransIn();
    }

    public void Restart()
    {
        if (currentLevel != null)
        {
            currentLevel.TransOut();
            currentLevel = null;
        }

        tier = -1;
        selectedLevelIDs.Clear();
    }
    
    public Level GetNextLevel()
    {
        if (tier < 0)
        {
            tier = 0;
            return StarterLevel;
        }

        if (ShouldAdvanceToNextTier())
        {
            AdvanceToNextTier();
        }
        else if (ShouldGoToPreviousTier())
        {
            GoToPreviousTier();
        }

        List<Level> tierLevels = Tiers[tier].levels;

        if (selectedLevelIDs.Count >= tierLevels.Count)
        {
            Debug.Log("All levels from this tier have already been played");
            selectedLevelIDs.Clear();
        }

        int levelID;
        while (true)
        {
            levelID = Random.Range(0, tierLevels.Count);
            if (!selectedLevelIDs.Contains(levelID))
            {
                break;
            }
        }

        selectedLevelIDs.Add(levelID);

        return tierLevels[levelID];

    }

    private bool ShouldAdvanceToNextTier()
    {
        if (selectedLevelIDs.Count >= 3 && tier < Tiers.Count - 1)
        {
            return true;
        }
        return false;
    }

    private void AdvanceToNextTier()
    {
        tier++;
        selectedLevelIDs.Clear();
    }

    private bool ShouldGoToPreviousTier()
    {
        if (selectedLevelIDs.Count >= 3 && tier == Tiers.Count - 1)
        {
            return true;
        }
        return false;
    }

    private void GoToPreviousTier()
    {
        tier--;
        selectedLevelIDs.Clear();
    }
}
