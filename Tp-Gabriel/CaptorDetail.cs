using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Gabriel
{
    public class CaptorDetail
    {

        public DateTime dateHour { get; set; }
        public string idsalle { get; set; }
        public double temperatur { get; set; }

        public CaptorDetail(DateTime dateHour, string idsalle, double temperatur)
        {
            this.dateHour = dateHour;
            this.idsalle = idsalle;
            this.temperatur = temperatur;
        }

    }
}
