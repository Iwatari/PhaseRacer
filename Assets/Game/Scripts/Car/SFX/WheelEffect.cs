using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private ParticleSystem[] wheelSmoke;

    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sidewaySlipLimit;

    [SerializeField] private new AudioSource audio;

    [SerializeField] private GameObject skidPrefab;

    private WheelHit wheelHit;
    private Transform[] skidTrail;

    private void Start()
    {
        skidTrail = new Transform[wheels.Length];
    }
    private void Update()
    {
        bool isSlip = false;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetGroundHit(out wheelHit);

            if(wheels[i].isGrounded == true)
            {
                if(Mathf.Abs(wheelHit.forwardSlip) > forwardSlipLimit ||
                   Mathf.Abs(wheelHit.sidewaysSlip) > sidewaySlipLimit)
                {
                    if(skidTrail[i] == null)
                    {
                        skidTrail[i] = Instantiate(skidPrefab).transform;
                    }

                  //  if(audio.isPlaying == false)
                   //     audio.Play();

                    if(skidTrail[i] != null)
                    {
                        skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * wheels[i].radius;
                        skidTrail[i].forward = -wheelHit.normal;

                        wheelSmoke[i].transform.position = skidTrail[i].position;
                        wheelSmoke[i].Emit(1);
                    }

                    isSlip = true;

                    continue;
                }
            }

            skidTrail[i] = null;
            wheelSmoke[i].Stop();
        }

      //  if(isSlip == false)
          //  audio.Stop();
    }
}
