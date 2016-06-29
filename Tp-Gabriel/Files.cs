using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tp_Gabriel
{
    public class Files
    {



        private string xmlFile;
        private string dataFile;

        public Files()
        {

        }

        public Files(string xml,string data)
        {
            if (xml==null || xml.Equals("") || data==null || data.Equals(""))
            {
                throw new Exception("The files can't be empty");
            }
            xmlFile = xml;
            dataFile = data;
        }

        private List<Captor> xmlParse()
        {
            List<Captor> listData = new List<Captor>();
            System.Xml.Linq.XDocument xdoc = XDocument.Load(xmlFile);
            xdoc.Descendants("capteur").Where(p => p.Element("box").Value.Equals("netatmo")).Select(
               p => new
               {
                   id = p.Element("id").Value,
                   description = p.Element("description").Value,
                   nom = p.Element("grandeur").Attribute("nom").Value,
                   unite = p.Element("grandeur").Attribute("unite").Value,
                   abreviation = p.Element("grandeur").Attribute("abreviation").Value,
                   box = p.Element("box").Value,
                   lieu = p.Element("lieu").Value

               }).ToList().ForEach(p =>
               {
                       // must finish here for create the instance
                      listData.Add(new Captor(p.id,  p.description ,p.nom,  p.unite ,  p.abreviation,  p.box , p.lieu));
                   
               });
            return listData;
        }


        private List<CaptorDetail> parseDt()
        {

            List<CaptorDetail> captorDetail = new List<CaptorDetail>();
            string[] folder = Directory.GetFiles(dataFile);
            for (int i = 0; i < folder.Length; i++)
            {

                using (StreamReader streamReader = new StreamReader(folder[i]))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string s_date = line.Substring(1, 19);
                        DateTime date = DateTime.Parse(s_date);
                        
                        string[] id = line.Split(' ');
                        string idsalle = id[(id.Length - 2)];
                        double val = double.Parse(id[(id.Length - 1)]);
                        captorDetail.Add(new CaptorDetail(date,idsalle,val));
                      
                    }
                }
                
            }
            return captorDetail;
        }

        public List<Captor> getXmlAndDt()
        {
            List<Captor> listeC = xmlParse();
            List<CaptorDetail> listeD = parseDt();

            foreach (Captor capt in listeC)
            {
                var array = from Element
                            in listeD
                            where capt.id == Element.idsalle
                            select Element;

                List<CaptorDetail> liste = array.ToList();
                capt.detailList = liste;
            }
            return listeC;
        }


        public string[] getDateDirecotry()
        {
            string[] datafiles = Directory.GetFiles(dataFile);
            for (int i =0;i< datafiles.Length;i++)
            {
                string var = datafiles[i].ToString().Replace(".dt","");
                string [] tmp_array =  var.Split('\\');
                datafiles[i] = tmp_array[tmp_array.Length-1];
            }
            return datafiles;
        }

        public Dictionary<DateTime,double> getvaluesFromDay(List<Captor> listeCaptor,StringBuilder st,string id)
        {      
            st.Insert(4, '/');
            st.Insert(7, '/');
            DateTime dt = DateTime.Parse(st.ToString());
            bool found = false;
            Dictionary<DateTime, double> dicoValues = new Dictionary<DateTime, double>();
            foreach (Captor capt in listeCaptor)
            {
                if (capt.id == id)
                {
                    foreach (CaptorDetail captDetail in capt.detailList)
                    {
                        if (captDetail.dateHour.Day == dt.Day)
                        {
                            found = true;
                            dicoValues.Add(captDetail.dateHour, captDetail.temperatur);

                        }
                    }
                }
                if (found)
                {
                    break;
                }

            }
            return dicoValues;
        }
        

    }
}
