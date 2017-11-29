using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class MovecControlPlayable : PlayableBehaviour
{
    public MotionVectorGenerationMode mode = MotionVectorGenerationMode.Object;
}
