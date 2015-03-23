using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class GestureDector : MonoBehaviour
{
    public Queue<DetectedGesture> DetectedGestures = new Queue<DetectedGesture>();
    public int HandClosedTime = 1;
    public Material switchHand;
    public Material normalHand;

    private HandTracker _handLeftTracker = new HandTracker();
    private HandTracker _handRightTracker = new HandTracker();
    Dictionary<string, float> restore = new Dictionary<string, float>();

    void Update()
    {

        if (this.DetectedGestures.Count > 0)
        {

            var dg = this.DetectedGestures.Dequeue();
            string id = dg.Body.TrackingId.ToString();
            GameObject.Find("Body:" + id).transform.FindChild("HandRight").GetComponent<Renderer>().material = switchHand;

            if (!restore.ContainsKey(id))
                restore.Add(id, Time.time);
        }

        List<string> removes = new List<string>();
        foreach (var item in restore)
        {
            if (item.Value + 3 < Time.time)
            {
                removes.Add(item.Key);
                GameObject.Find("Body:" + item.Key).transform.FindChild("HandRight").GetComponent<Renderer>().material = normalHand;
            }
        }
        foreach (var item in removes)
            restore.Remove(item);

    }

    public void CheckForGestures(Body body)
    {
        _handLeftTracker.Check(body, this, Gestures.HandLeftOpened);
        _handRightTracker.Check(body, this, Gestures.HandRightOpened);
    }

    private class HandTracker : Dictionary<ulong, float>
    {
        public void Check(Body body, GestureDector gd, Gestures gesture)
        {
            HandState state = gesture == Gestures.HandLeftOpened ? body.HandLeftState : body.HandRightState;

            if (state == HandState.Open)
            {
                if (this.ContainsKey(body.TrackingId))
                {
                    if (this[body.TrackingId] + gd.HandClosedTime < Time.time)
                    {
                        Debug.Log(gesture.ToString());
                        gd.DetectedGestures.Enqueue(new DetectedGesture() { Body = body, Gesture = gesture });
                    }
                    this.Remove(body.TrackingId);
                }
            }
            else
            {
                if (!this.ContainsKey(body.TrackingId))
                    this.Add(body.TrackingId, Time.time);
            }
        }
    }
}

public enum Gestures
{
    HandLeftOpened,
    HandRightOpened,
}

public class DetectedGesture
{
    public Gestures Gesture;
    public Body Body;
    public Material PreviousMaterial;
}