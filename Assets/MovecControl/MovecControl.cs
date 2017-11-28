using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class MovecControl : PlayableAsset, ITimelineClipAsset
{
    public MovecControlPlayable template = new MovecControlPlayable();

    public ClipCaps clipCaps { get { return ClipCaps.None; } }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<MovecControlPlayable>.Create(graph, template);
    }
}
