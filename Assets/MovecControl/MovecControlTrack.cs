using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.4f, 0.4f, 0.4f)]
[TrackClipType(typeof(MovecControl))]
[TrackBindingType(typeof(Transform))]
public class MovecControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MovecControlMixer>.Create(graph, inputCount);
    }

    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
        var transform = director.GetGenericBinding(this) as Transform;
        if (transform == null) return;

        foreach (var smr in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
            driver.AddFromName<SkinnedMeshRenderer>(smr.gameObject, "m_MotionVectors");
    }
}
