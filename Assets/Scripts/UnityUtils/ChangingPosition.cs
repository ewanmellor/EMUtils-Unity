using UnityEngine;


namespace EMUtils.UnityUtils
{
    public sealed class ChangingPosition
    {
        public Vector3 PrevPosition;
        public Quaternion PrevRotation;
        public Vector3 NextPosition;
        public Quaternion NextRotation;
        public float Fraction;
        public float FractionPerSec;

        public ChangingPosition(Vector3 prevPosition, Quaternion prevRotation, Vector3 nextPosition, Quaternion nextRotation, float fractionPerSec)
        {
            PrevPosition = prevPosition;
            PrevRotation = prevRotation;
            NextPosition = nextPosition;
            NextRotation = nextRotation;
            FractionPerSec = fractionPerSec;
        }

        public ChangingPosition(Vector3 prevPosition, Vector3 nextPosition, float fractionPerSec) :
            this(prevPosition, Quaternion.identity, nextPosition, Quaternion.identity, fractionPerSec)
        {
        }

        public void Reset(Vector3 prevPosition, Quaternion prevRotation, Vector3 nextPosition, Quaternion nextRotation, float fractionPerSec)
        {
            PrevPosition = prevPosition;
            PrevRotation = prevRotation;
            NextPosition = nextPosition;
            NextRotation = nextRotation;
            Fraction = 0;
            FractionPerSec = fractionPerSec;
        }

        public void Reset(Vector3 prevPosition, Vector3 nextPosition, float fractionPerSec)
        {
            PrevPosition = prevPosition;
            PrevRotation = Quaternion.identity;
            NextPosition = nextPosition;
            NextRotation = Quaternion.identity;
            Fraction = 0;
            FractionPerSec = fractionPerSec;
        }

        public Vector3 CurrentPosition => Vector3.Lerp(PrevPosition, NextPosition, Fraction);

        public Quaternion CurrentRotation => Quaternion.Slerp(PrevRotation, NextRotation, Fraction);

        public (Vector3, Quaternion, bool) Lerp(float deltaTime)
        {
            Fraction += FractionPerSec * deltaTime;
            if (Fraction >= 1.0f)
            {
                Fraction = 1.0f;
                return (NextPosition, NextRotation, true);
            }

            return (CurrentPosition, CurrentRotation, false);
        }
    }
}
