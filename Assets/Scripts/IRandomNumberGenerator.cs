using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HordeKiller {
   public interface IRandomNumberGenerator {
        float GenerateRandomValue(float min, float max);

    }
}
