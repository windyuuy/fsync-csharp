using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsync.amath
{
    using number = System.Double;

    public class Sphere
    {
        public number x;
        public number y;
        public number z;
        public number radius;

        public virtual Vector3 center()
        {
            return new Vector3(x, y, z);
        }
    }
}
