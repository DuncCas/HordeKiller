using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HordeKiller {
    public interface ISpawnable {
        Vector3 newLocation();
        void Spawn(Vector3 pos);
    }
}
