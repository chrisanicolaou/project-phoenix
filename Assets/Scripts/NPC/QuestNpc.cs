using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.Questing;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC
{
    public class QuestNpc : Npc
    {
        protected override string[] GetDialog()
        {
            foreach (var questActivation in _npcData.QuestsToActivate)
            {
                if (questActivation.Quest.State == QuestState.Unlocked)
                {
                    var dialogue = questActivation.ActivationDialogs;
                    questActivation.Quest.Activate();
                    return dialogue;
                }
            }
            foreach (var questDialog in _npcData.SubscribedQuests)
            {
                if (questDialog.Quest.State != QuestState.Active) continue;
                
                switch (questDialog.Quest.Steps[questDialog.Quest.CurrentStepIndex].State)
                {
                    case QuestState.Locked:
                        return base.GetDialog();
                    case QuestState.Unlocked:
                        return questDialog.Steps[questDialog.Quest.CurrentStepIndex].StepStartingDialog;
                    case QuestState.Active:
                        return questDialog.Steps[questDialog.Quest.CurrentStepIndex].StepIncompleteDialog;
                    case QuestState.Complete:
                        return questDialog.Steps[questDialog.Quest.CurrentStepIndex].StepCompleteDialog;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return base.GetDialog();
            // if (CurrentQuest == null)
            // {
            //     Debug.LogWarning($"Requesting GetDialogue but _currentQuest is null.");
            //     return base.GetDialog();
            // }
            //
            // var questDialog = Dialog.QuestDialogs.FirstOrDefault(q => q.QuestId == CurrentQuest.Id);
            //
            // if (questDialog == null)
            // {
            //     throw new ArgumentException(
            //         $"Quest of ID {CurrentQuest.Id} does not exist in npc {Name}'s dialogue json!");
            // }

            // var questStepDialog =
            //     questDialog.Steps.FirstOrDefault(s => s.StepIndex == CurrentQuest.CurrentStepIndex);

            // if (questStepDialog == null)
            // {
            //     Debug.LogWarning(
            //         $"Requesting GetDialogue(questID: {CurrentQuest.Id}, stepIndex: {CurrentQuest.CurrentStepIndex}) but stepIndex does not exist.");
            //     return base.GetDialog();
            // }

            // switch (CurrentQuest.Steps[CurrentQuest.CurrentStepIndex].State)
            // {
            //     case QuestState.Locked:
            //         return base.GetDialog();
            //     case QuestState.Unlocked:
            //         return questStepDialog.StepStartingDialog;
            //     case QuestState.Active:
            //         return questStepDialog.StepIncompleteDialog;
            //     case QuestState.Complete:
            //         return questStepDialog.StepCompleteDialog;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        // private void OnQuestUnlocked(Dictionary<string, object> msg)
        // {
        //     if (!GameEventsManager.Instance.TryValidateKey<int>(msg, "questId", out var questId)) return;
        //
        //     var i = Array.IndexOf(_questIds, questId);
        //
        //     if (i == -1) return;
        //
        //     if (CurrentQuest != null)
        //     {
        //         Debug.LogWarning(
        //             $"Unlocked quest found - overriding current quest {CurrentQuest.Name}. This is safe for as long" +
        //             "as the intended design is for 1 quest at a time per NPC.");
        //     }
        //
        //     CurrentQuest = QuestSystem.Instance.QuestPool
        //         .FirstOrDefault(q => q.Id == questId && q.State == QuestState.Unlocked);
        //
        //     if (CurrentQuest == null)
        //     {
        //         Debug.LogError(
        //             $"Quest not found in QuestPool. Please make sure the state is set to unlocked before triggering QuestUnlocked events." +
        //             $"QuestID: {questId}");
        //         return;
        //     }
        //     
        //     Debug.Log("Hey, I have a new quest now! Come see me!");
        // }
    }
}