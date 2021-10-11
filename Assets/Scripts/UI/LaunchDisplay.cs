using KnifeGame.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.UI
{
    public class LaunchDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _start;
        [SerializeField] private List<GameObject> _dots;
        [SerializeField] private GameObject _end;
        [SerializeField] private Camera _uiCamera;

        private Vector3 _swipeStart;

        private void Start()
        {
            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
            SwipeManager.Inst.OnSwipeDrag += OnSwipeDragHandler;
            //SwipeManager.Inst.OnSwipeEnd += (pos) => Disable() ;
        }

        //private Vector3 ConvertPos(Vector2 pos)
        //{
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, pos, _canvas.worldCamera, out pos);
        //    return _canvas.transform.TransformPoint(pos);
        //}

        private void Enable()
        {
            _start.SetActive(true);
            _end.SetActive(true);
            _dots.ForEach((item) => item.SetActive(true));
        }
        private void Disable()
        {
            _start.SetActive(false);
            _end.SetActive(false);
            _dots.ForEach((item) => item.SetActive(false));
        }

        private void OnSwipeStartHandler(Vector2 start)
        {
            Enable();
            
            //transform.position = Camera.main.ScreenToWorldPoint(start);
            //_swipeStart = ConvertPos(start);
            //((RectTransform)_start.transform).position = _swipeStart;
            //OnSwipeDragHandler(start);
        }

        private void OnSwipeDragHandler(Vector2 pos)
        {
            var canvasPos = _uiCamera.ScreenToWorldPoint(pos);
            canvasPos.z = 10f;
            ((RectTransform)_start.transform).position = canvasPos;
            //((RectTransform)_end.transform).position = ConvertPos(pos);
        }
    }
}
