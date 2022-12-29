using ChiciStudios.ProjectPhoenix.Questing;
using ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests;
using ChiciStudios.ProjectPhoenix.VariableSO;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Editor
{
    [CustomEditor(typeof(QuestLookupGenerator))]
    public class QuestLookupGeneratorEditor : UnityEditor.Editor
    {
        private QuestLookupGenerator _target;
        
        private void OnEnable()
        {
            _target = target as QuestLookupGenerator;
            _target.QuestPool = GetAllQuests();
        }

        private Quest[] GetAllQuests()
        {
            var guids = AssetDatabase.FindAssets("t:Quest");  //FindAssets uses tags check documentation for more info
            Quest[] quests = new Quest[guids.Length];
            for(var i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                quests[i] = AssetDatabase.LoadAssetAtPath<Quest>(path);
            }
            
            return quests;
        }
    }
}