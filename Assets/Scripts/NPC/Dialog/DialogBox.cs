using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.NPC.Dialog
{
    public class DialogBox : MonoBehaviour
    {
        [SerializeField]
        private Image _img;
        
        [SerializeField]
        private CanvasGroup _imgFade;

        [SerializeField]
        private TextMeshProUGUI _nameText;

        [SerializeField]
        private TextMeshProUGUI _bodyText;

        [SerializeField]
        private RectTransform _boxTransform;

        private TweenerCore<string, string, StringOptions> _textTween;

        private void Update()
        {
            if (_textTween == null || _textTween.IsComplete()) return;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _textTween.Complete();
            }
        }

        public IEnumerator StartDialog(string header, Sprite img, string[] sentences)
        {
            if (sentences.Length == 0) yield break;
            _nameText.text = header;
            _img.sprite = img;
            var timeScale = Time.timeScale;
            Time.timeScale = 0f;
            var openDialog = OpenDialog();
            yield return openDialog.WaitForCompletion();

            foreach (var sentence in sentences)
            {
                _textTween = _bodyText.DOText(sentence, 1f);
                yield return _textTween.WaitForCompletion();
                _textTween = null;
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                _bodyText.text = string.Empty;
            }

            var closeDialog = CloseDialog();
            yield return closeDialog.WaitForCompletion();
            
            Time.timeScale = timeScale;
            _textTween = null;

            Clean();
        }

        private void Clean()
        {
            _nameText.text = default;
        }

        private Sequence OpenDialog()
        {
            var seq = DOTween.Sequence();
            seq.Append(_boxTransform.DOScale(1f, 0.3f));
            seq.Append(_imgFade.DOFade(1f, 0.2f));
            return seq;
        }

        private Sequence CloseDialog()
        {
            var seq = DOTween.Sequence();
            seq.Append(_imgFade.DOFade(0f, 0.2f));
            seq.Append(_boxTransform.DOScale(0f, 0.3f));
            return seq;
        }
    }
}