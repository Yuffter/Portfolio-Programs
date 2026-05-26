using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MinutesGame.Common.Explanation
{
    public class ExplanationView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _explanationTextObject;
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _strength = 5f;
        private TMP_Text _explanationText;
        private float _currentTime = 0f;

        private void Awake()
        {
            _explanationText = _explanationTextObject.GetComponent<TMP_Text>();
            _explanationTextObject.SetActive(false);
        }

        public async UniTask ShowExplanationTextAsync()
        {
            _explanationTextObject.SetActive(true);
            _explanationText.alpha = 0f;
            _explanationText.DOFade(1, 0.5f);
            _explanationTextObject.transform.DOScale(1.0f, 0.5f).From(2f).SetEase(Ease.OutElastic);
            _currentTime = 0f;
            _explanationText.ForceMeshUpdate();

            while (_currentTime < 3f)
            {
                _currentTime += Time.deltaTime;

                _explanationText.ForceMeshUpdate();
                var mesh = _explanationText.mesh;
                var vertices = mesh.vertices;

                for (int i = 0;i < _explanationText.textInfo.characterCount;i++)
                {
                    var charInfo = _explanationText.textInfo.characterInfo[i];
                    if (!charInfo.isVisible) continue;

                    int vertexIndex = charInfo.vertexIndex;
                    float x = (Mathf.PerlinNoise(Time.time * _speed, i) - 0.5f) * _strength;
                    float y = (Mathf.PerlinNoise(i, Time.time * _speed) - 0.5f) * _strength;
                    Vector3 offset = new Vector3(x, y, 0);

                    vertices[vertexIndex + 0] += offset;
                    vertices[vertexIndex + 1] += offset;
                    vertices[vertexIndex + 2] += offset;
                    vertices[vertexIndex + 3] += offset;
                }
                mesh.vertices = vertices;
                _explanationText.canvasRenderer.SetMesh(mesh);
                await UniTask.Yield();
            }

            _explanationTextObject.GetComponent<RectTransform>().DOAnchorPosY(Screen.height + 100, 1f).SetEase(Ease.InBack);
            _explanationText.DOFade(0, 1f);

            _currentTime = 0f;
            while (_currentTime < 1f)
            {
                _currentTime += Time.deltaTime;

                _explanationText.ForceMeshUpdate();
                var mesh = _explanationText.mesh;
                var vertices = mesh.vertices;

                for (int i = 0;i < _explanationText.textInfo.characterCount;i++)
                {
                    var charInfo = _explanationText.textInfo.characterInfo[i];
                    if (!charInfo.isVisible) continue;

                    int vertexIndex = charInfo.vertexIndex;
                    float x = (Mathf.PerlinNoise(Time.time * _speed, i) - 0.5f) * _strength;
                    float y = (Mathf.PerlinNoise(i, Time.time * _speed) - 0.5f) * _strength;
                    Vector3 offset = new Vector3(x, y, 0);

                    vertices[vertexIndex + 0] += offset;
                    vertices[vertexIndex + 1] += offset;
                    vertices[vertexIndex + 2] += offset;
                    vertices[vertexIndex + 3] += offset;
                }
                mesh.vertices = vertices;
                _explanationText.canvasRenderer.SetMesh(mesh);
                await UniTask.Yield();
            }
            // _explanationText.DOFade(0, 0.5f);
        }
    }
}