using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.Utils
{
    // Screen Space Overlay prevents the UI from shaking in game (caused by issues between
    // screen space camera & pixel perfect camera component), but is hard to edit with as it
    // does not show the UI in the context of the space it would occupy on screen.
    // This allows keeping the UI set to ScreenSpace.Camera in the editor.
    public class CanvasSwitchToOverlay : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        private void Awake()
        {
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }
}