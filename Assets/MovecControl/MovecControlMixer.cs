using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
class MovecControlMixer : PlayableBehaviour
{
    #region Private state

    int _delayCount;
    SkinnedMeshRenderer[] _targetCache;
    MaterialPropertyBlock _sheet;

    #endregion

    #region PlayableBehaviour overrides

    public override void OnGraphStart(Playable playable)
    {
        _delayCount = 0;
        _targetCache = null;
        _sheet = new MaterialPropertyBlock();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Retrieve the target root transform from the track-given data.
        var root = playerData as Transform;
        if (root == null) return;

        // Update the cache that contains the target renderer list.
        if (_targetCache == null)
            _targetCache = root.GetComponentsInChildren<SkinnedMeshRenderer>();

        // Scan the track clips to determine the current movec mode.
        var mode = MotionVectorGenerationMode.Camera;
        var inputCount = playable.GetInputCount();
        for (var i = 0; i < inputCount; i++)
        {
            if (playable.GetInputWeight(i) > 0)
            {
                var clip = (ScriptPlayable<MovecControlPlayable>)playable.GetInput(i);
                mode = clip.GetBehaviour().mode;
                break;
            }
        }

        // We know that the per-object movec mode makes an undesirable spike in
        // the first frame, so delay actual changes for two frames.
        if (mode == MotionVectorGenerationMode.Object)
        {
            if (_delayCount++ < 2) mode = MotionVectorGenerationMode.Camera;
        }
        else
        {
            _delayCount = 0;
        }

        // Update the movec mode of the renderers.
        foreach (var smr in _targetCache)
            smr.motionVectorGenerationMode = mode;

        // Cancel the depth bias with the per-object movec mode.
        if (mode == MotionVectorGenerationMode.Object)
        {
            foreach (var smr in _targetCache)
            {
                smr.GetPropertyBlock(_sheet);
                _sheet.SetFloat("_MotionVectorDepthBias", 0);
                smr.SetPropertyBlock(_sheet);
            }
        }
    }

    #endregion
}
