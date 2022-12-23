using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.NPC.Dialog;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChiciStudios.ProjectPhoenix.NPC
{
    public class Npc : MonoBehaviour, IInteractCommandActor
    {
        [SerializeField]
        private GameEvent _npcInteractionCompleteEvent;
        
        [FormerlySerializedAs("_dialogueBoxPrefab")]
        [SerializeField]
        private GameObject _dialogBoxPrefab;

        [SerializeField]
        protected NpcData _npcData;

        private static DialogBox _dialogBox;

        protected DialogBox DialogBox
        {
            get
            {
                if (_dialogBox == null)
                {
                    CreateDialogBox();
                }
                return _dialogBox;
            }
        }

        private void CreateDialogBox()
        {
            var dialogBoxObj = Instantiate(_dialogBoxPrefab);
            var canvas = dialogBoxObj.GetComponentInParent<Canvas>();
            canvas.worldCamera = Camera.main;
            _dialogBox = dialogBoxObj.GetComponentInParent<DialogBox>();
        }

        public virtual void Interact()
        {
            StartCoroutine(PlayDialog());
        }
        
        protected virtual string[] GetDialog()
        {
            return _npcData.DefaultDialog.VillageLevelDialogs[0].Dialog;
        }

        protected IEnumerator PlayDialog()
        {
            var dialog = GetDialog();
            if (dialog.Length == 0) yield break;
        
            if (_npcData.DialogSprite == null) _npcData.DialogSprite = GetComponent<SpriteRenderer>().sprite;
            yield return DialogBox.StartDialog(_npcData.Name, _npcData.DialogSprite, dialog);
            
            _npcInteractionCompleteEvent.Fire(new Dictionary<string, object>
            {
                { "npcId", _npcData.Id }
            });
        }
    }
}