using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DirectionSignalChangeForECS : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            var emm = World.DefaultGameObjectInjectionWorld.EntityManager;

            var query = emm.CreateEntityQuery(typeof(DirectionChangeSignal));

            if (query.IsEmpty)
                return;

            var entity = query.GetSingletonEntity();
            var signal = emm.GetComponentData<DirectionChangeSignal>(entity);

            if (signal.ChangeCount == -1)
            {
                signal.ChangeCount += 3;
            }
            if (signal.ChangeCount == 0)
            {
                signal.ChangeCount += 1;
            }

            emm.SetComponentData(entity, signal);
        }
    }
}
