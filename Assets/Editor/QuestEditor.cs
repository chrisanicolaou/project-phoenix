using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Questing;
using ChiciStudios.ProjectPhoenix.Questing.Steps;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Editor
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : UnityEditor.Editor
    {
        private Quest _target;
        private List<QuestStep> _steps;
 
        private void OnEnable()
        {
            Debug.Log("Copying steps");
            _target = target as Quest;
            _steps = new List<QuestStep>(_target.Steps);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            for (var i = 0; i < _target.Steps.Count; i++)
            {
                _target.Steps[i] ??= new InteractStep();
                
                switch (_target.Steps[i].Type)
                {
                    case QuestStepType.Interact:
                        if (_target.Steps[i] is not InteractStep)
                        {
                            _target.Steps[i] = new InteractStep();
                            _target.Steps[i].Type = QuestStepType.Interact;
                        }
                        break;
                    case QuestStepType.Combat:
                        if (_target.Steps[i] is not CombatStep)
                        {
                            _target.Steps[i] = new CombatStep();
                            _target.Steps[i].Type = QuestStepType.Combat;
                        }
                        break;
                    case QuestStepType.Gather:
                        if (_target.Steps[i] is not GatherStep)
                        {
                            _target.Steps[i] = new GatherStep();
                            _target.Steps[i].Type = QuestStepType.Gather;
                        }
                        break;
                    case QuestStepType.InteractAndReturnQuestItem:
                        if (_target.Steps[i] is not InteractAndReturnQuestItemStep)
                        {
                            _target.Steps[i] = new InteractAndReturnQuestItemStep();
                            _target.Steps[i].Type = QuestStepType.InteractAndReturnQuestItem;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("New quest type has not been added to editor script");
                }
            }

            if (GUILayout.Button("Reset"))
            {
                foreach (var questStep in _target.Steps)
                {
                    questStep.Reset();
                }

                _target.State = QuestState.Locked;
                _target.CurrentStepIndex = 0;
            }
        }
    }
}