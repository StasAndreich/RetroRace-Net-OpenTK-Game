using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGEngine.Physics
{
    public class ColliderBatch
    {
        private List<Collider> colliders = new List<Collider>();

        public int Quantity => colliders.Count;

        public Collider this[int index]
        {
            get => colliders[index];
            set => colliders[index] = value;
        }

        public void AddCollider(Collider collider)
        {
            colliders.Add(collider);
        }

        public static ColliderBatch CreateColliderBatch(params Collider[] colliders)
        {
            if (colliders.Length == 0)
                return null;

            var batch = new ColliderBatch();
            for (int i = 0; i < colliders.Length; i++)
            {
                batch.AddCollider(colliders[i]);
            }

            return batch;
        }
    }
}
