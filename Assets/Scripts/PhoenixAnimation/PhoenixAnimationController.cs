// using System;
// using System.Collections;
// using System.Linq;
// using UnityEngine;
//
// namespace ChiciStudios.ProjectPhoenix.PhoenixAnimation
// {
//     public class PhoenixAnimationController : MonoBehaviour
//     {
//         [field: SerializeField]
//         public PhoenixAnimationClip[] Animations { get; set; }
//
//         public PhoenixAnimationClip CurrentlyPlaying => _currentAnimation;
//
//         private IEnumerator _currentAnimationCoroutine;
//
//         private SpriteRenderer _targetRenderer;
//
//         private PhoenixAnimationClip _currentAnimation;
//
//         private PhoenixAnimationClip _previousAnimation;
//
//         private void Awake()
//         {
//             _targetRenderer = GetComponent<SpriteRenderer>();
//             if (_targetRenderer != null) return;
//             
//             Debug.LogError("AnimationController must have a SpriteRenderer attached!");
//             enabled = false;
//         }
//
//         private void Start()
//         {
//             var defaultAnim = Animations.FirstOrDefault(a => a.PlayOnAwake);
//             
//             if (defaultAnim == null) return;
//             
//             _currentAnimationCoroutine = StartAnimation(defaultAnim);
//             StartCoroutine(_currentAnimationCoroutine);
//         }
//
//         public void Play(PhoenixAnimationType type, int startFrame = 0)
//         {
//             var phoenixAnimationClip = Animations.FirstOrDefault(a => a.Type == type);
//             
//             if (phoenixAnimationClip == null) return;
//             
//             if (_currentAnimationCoroutine != null) StopCoroutine(_currentAnimationCoroutine);
//             _currentAnimationCoroutine = StartAnimation(phoenixAnimationClip, startFrame);
//             StartCoroutine(_currentAnimationCoroutine);
//         }
//
//         private IEnumerator StartAnimation(PhoenixAnimationClip clip, int startFrame = 0)
//         {
//             _previousAnimation = _currentAnimation;
//             _currentAnimation = clip;
//             var frameDuration = clip.Duration / clip.Frames.Length;
//
//             do
//             {
//                 for (var i = startFrame; i < clip.Frames.Length; i++)
//                 {
//                     _targetRenderer.sprite = clip.Frames[i].Frame;
//                     yield return null;
//                     clip.Frames[i].Event?.Invoke();
//                     yield return new WaitForSeconds(frameDuration);
//                 }
//             } while (clip.Loop);
//
//             if (_previousAnimation != null)
//             {
//                 Play(_previousAnimation.Type);
//             }
//         }
//     }
// }