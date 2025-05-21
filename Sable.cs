using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Sable : Terrain
    {
        public Sable() : base("Sable", 1.2, 40.0) { }
        // Capacité en eau plus faible, fertilité plus pauvre
    }
}