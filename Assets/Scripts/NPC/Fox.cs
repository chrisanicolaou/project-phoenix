// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using ChiciStudios.ProjectPhoenix.Enums;
// using ChiciStudios.ProjectPhoenix.Globals;
// using UnityEngine;
//
// namespace ChiciStudios.ProjectPhoenix.NPC
// {
//     public class Fox : QuestNpc
//     {
//         public override int Id { get; set; } = 0;
//         public override string Name { get; set; } = "Fox";
//
//         public override void Interact()
//         {
//             IsInteracting = true;
//             StartCoroutine(InteractInternal());
//         }
//
//         public override IEnumerator InteractInternal()
//         {
//             yield return StartCoroutine(PlayDialog());
//             
//             GameEventsManager.Instance.TriggerEvent(GameEvent.DialogCompleted, new Dictionary<string, object>
//             {
//                 { "npcId", Id }
//             });
//             IsInteracting = false;
//         }
//     }
// }