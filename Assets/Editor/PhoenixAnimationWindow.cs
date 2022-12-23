using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.PhoenixAnimation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Screen;

namespace ChiciStudios.ProjectPhoenix.Editor
{
    public class PhoenixAnimationWindow : EditorWindow
    {
        private List<PhoenixAnimationClip> _animationClips = new List<PhoenixAnimationClip>();

        private PhoenixAnimationClip _currentClip;

        private TwoPaneSplitView _mainSplitView;
        private TwoPaneSplitView _topSplitView;
        private VisualElement _clipBrowserView;
        private VisualElement _mainView;
        private TwoPaneSplitView _bottomSplitView;
        private VisualElement _keyframeEventsView;
        private VisualElement _timelineView;

        private TextField _clipBrowserSearchField;
        private ListView _clipBrowserListView;

        private string _clipSearch;

        [MenuItem("Tools/Phoenix Animation")]
        public static void ShowWindow()
        {
            GetWindow<PhoenixAnimationWindow>("Phoenix Animation");
        }

        public void CreateGUI()
        {
            FetchClips();
            CreateViews();
            CreateClipBrowser();
        }

        public void FetchClips()
        {
            var allObjectGuids = AssetDatabase.FindAssets("t:PhoenixAnimationClip");
            _animationClips = allObjectGuids.Select(guid =>
                    AssetDatabase.LoadAssetAtPath<PhoenixAnimationClip>(AssetDatabase.GUIDToAssetPath(guid)))
                .OrderBy(c => c.name)
                .ToList();
        }

        public void CreateViews()
        {
            _topSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
            _clipBrowserView = new VisualElement();
            _mainView = new VisualElement();
            _topSplitView.Add(_clipBrowserView);
            _topSplitView.Add(_mainView);

            _bottomSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
            _keyframeEventsView = new VisualElement();
            _timelineView = new VisualElement();
            _bottomSplitView.Add(_keyframeEventsView);
            _bottomSplitView.Add(_timelineView);

            _mainSplitView = new TwoPaneSplitView(0, 450, TwoPaneSplitViewOrientation.Vertical);
            _mainSplitView.Add(_topSplitView);
            _mainSplitView.Add(_bottomSplitView);

            rootVisualElement.Add(_mainSplitView);
        }

        private void CreateClipBrowser()
        {
            _clipBrowserSearchField = new TextField();
            _clipBrowserView.Add(_clipBrowserSearchField);

            _clipBrowserListView = new ListView();
            _clipBrowserListView.makeItem = () => new Label();
            _clipBrowserListView.bindItem = (item, index) => { (item as Label).text = _animationClips[index].name; };
            SearchClips();
            _clipBrowserListView.onSelectionChange += OnClipSelectionChange;
            _clipBrowserView.Add(_clipBrowserListView);
        }

        private void OnClipSelectionChange(IEnumerable<object> clips)
        {
            _mainView.Clear();

            var selectedClip = clips.FirstOrDefault() as PhoenixAnimationClip;
            if (selectedClip == null || selectedClip.Frames.Length == 0)
                return;

            var spriteImage = new Image();
            spriteImage.scaleMode = ScaleMode.ScaleToFit;
            spriteImage.sprite = selectedClip.Frames[0];

            _mainView.Add(spriteImage);
        }

        public bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        private void SearchClips()
        {
            var clips = new List<PhoenixAnimationClip>(_clipBrowserSearchField.text == string.Empty
                ? _animationClips
                : _animationClips.Where(c =>
                    Contains(c.name, _clipBrowserSearchField.text, StringComparison.OrdinalIgnoreCase)).ToList());
            _clipBrowserListView.itemsSource = clips;
            _clipSearch = _clipBrowserSearchField.text;
        }

        private void OnGUI()
        {
            if (_clipBrowserSearchField.text != _clipSearch)
            {
                SearchClips();
            }
            // if (_currentClip == null)
            // {
            //     GUILayout.BeginArea(new Rect((width / 2), height / 2, 120, 100));
            //     if (GUILayout.Button("Create a new clip"))
            //     {
            //         Debug.Log("Creating a new clip!");
            //     }
            //     GUILayout.EndArea();
            // }
        }
    }
}