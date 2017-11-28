using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
class MovecControlMixer : PlayableBehaviour
{
    #region Private state

    int _enableCount;
    SkinnedMeshRenderer[] _targetCache;
    MaterialPropertyBlock _sheet;

    #endregion

    #region PlayableBehaviour overrides

    public override void OnGraphStart(Playable playable)
    {
        _enableCount = 0;
        _targetCache = null;
        _sheet = new MaterialPropertyBlock();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Target root transform
        var root = playerData as Transform;
        if (root == null) return;

        // Get the target renderers and store them into a cache array.
        if (_targetCache == null)
            _targetCache = root.GetComponentsInChildren<SkinnedMeshRenderer>();

        // Check if the current time is covered by a clip.
        var inputCount = playable.GetInputCount();
        var totalWeight = 0.0f;
        for (var i = 0; i < inputCount; i++)
            totalWeight += playable.GetInputWeight(i);

        // Increment the counter if covered, or reset the counter.
        if (totalWeight > 0) _enableCount++; else _enableCount = 0;

        if (_enableCount > 2)
        {
            // Enabled: Set per-object movec mode and reset the bias value.
            foreach (var smr in _targetCache)
            {
                smr.motionVectorGenerationMode = MotionVectorGenerationMode.Object;

                smr.GetPropertyBlock(_sheet);
                _sheet.SetFloat("_MotionVectorDepthBias", 0);
                smr.SetPropertyBlock(_sheet);
            }
        }
        else
        {
            // Disabled: Set camera movec mode.
            foreach (var smr in _targetCache)
                smr.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
        }
    }

    #endregion
}
