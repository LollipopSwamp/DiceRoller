using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class AudioHandler : MonoBehaviour
{
    public List<AudioSource> collisionSounds = new List<AudioSource>();
    public AudioSource collisionSound;
    public DateTime lastPlayed;
    public int timesHit = 0;
    public List<string> gameObjectsHit = new List<string>();

    public void Reset()
    {
        gameObjectsHit.Clear();
    }

    void OnCollisionEnter(Collision collision)
    {
        return;
        Debug.Log(collision.gameObject.name);
        timesHit++;
        if (!gameObjectsHit.Contains(collision.gameObject.name))
        {
            gameObjectsHit.Add(collision.gameObject.name);
            collisionSound.Play();
        }
    }
}
