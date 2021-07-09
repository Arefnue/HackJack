using UnityEngine;

namespace DeckBuilding
{
    public class PanelAnim : MonoBehaviour
    {
        public Transform root;
        public float rate = 1f;
        private float _timer = 0f;
        private float _timer2 = 0f;
        private float _timer3 = 0f;
        public float scaleDiff = 0.1f;
        public Vector3 scaleVector;

        private Vector3 initalScale;
        private Vector3 minScale;
        private Vector3 maxScale;
        private void Start()
        {
            root = transform;
            initalScale = root.localPosition;
            minScale = initalScale - scaleVector * scaleDiff;
            maxScale = initalScale + scaleVector * scaleDiff;
        }

        private void LateUpdate()
        {
           
            
            if (_timer>=1f)
            {
                _timer2 += Time.deltaTime*rate;
                root.localPosition = Vector3.Lerp(maxScale,minScale, _timer2);
                if (_timer2>=1f)
                {
                    _timer = 0;
                    _timer2 = 0;
                }
            }
            else
            {
                _timer += Time.deltaTime*rate;

                root.localPosition = Vector3.Lerp(minScale, maxScale, _timer);
            }
            
        }
    }
}
