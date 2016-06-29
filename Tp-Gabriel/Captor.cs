using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Gabriel
{
    public class Captor
    {
        

         public string id { get; set; }
         public string description { get; set; }
         public string nom { get; set; }
         public string unite { get; set; }
         public string abreviation { get; set; }
         public string box { get; set; }
         public string lieu { get; set; }
         public List<CaptorDetail> detailList { get; set; } 



        public Captor(string id, string description, string nom, string unite, string abreviation, string box, string lieu)
        {
            this.id = id;
            this.description = description;
            this.nom = nom;
            this.unite = unite;
            this.abreviation = abreviation;
            this.box = box;
            this.lieu = lieu;
            detailList = new List<CaptorDetail>();
        }

        
    }
}
