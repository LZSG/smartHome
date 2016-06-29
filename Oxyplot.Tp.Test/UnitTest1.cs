using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Tp_Gabriel;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Oxyplot.Tp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
        /*
        [TestMethod]
        public void the_data_should_be_in_the_xml()
        {
            Tp_Gabriel.Files files = new Tp_Gabriel.Files(@"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\capteurs.xtim", "");
            System.Collections.Generic.List<Captor> listeValue = files.xmlParse();
            foreach (Captor tmp in listeValue)
            {
                Assert.AreEqual("temperaturearrieremaison", tmp.id);
                break;
            }
        }
        */
        [TestMethod]
        public void should_get_dt_data()
        {
            Tp_Gabriel.Files files = new Tp_Gabriel.Files(@"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\capteurs.xtim", @"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\netatmo");
            string[] folder =  Directory.GetFiles(files.dataFile);
            Assert.AreNotEqual(folder.Length,0);
            
            for(int i =0;i<folder.Length;i++)
            {
 
                using (StreamReader streamReader = new StreamReader(folder[i]) )
                {
                    string line;
                    while ((line = streamReader.ReadLine())!=null)
                    {
                        // it's for check the date
                        string date = line.Substring(1, 19);
                        string tmp = "31/01/2014 00:03:07";
                        Assert.AreEqual(tmp,date);

                        // it's for check the id
                        string[] id =  line.Split(' ');
                        Assert.AreEqual("temperaturesalle", id[id.Length-2]);

                        // it's for check the temperature
                        double val = double.Parse(id[(id.Length - 1)]);
                        double tmp2 = 21.8;
                        Assert.AreEqual(tmp2,val);
                        break;
                    }
                }
                break;
            }
        }
        /*
         *      I comment this  party after the refactoring
                [TestMethod]
                public void should_couple_the_datas()
                {
                    Tp_Gabriel.Files files = new Tp_Gabriel.Files(@"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\capteurs.xtim", @"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\netatmo");
                    System.Collections.Generic.List<Captor> listeC = files.xmlParse();
                    System.Collections.Generic.List<CaptorDetail> listeD = files.parseDt();

                    foreach (Captor capt in listeC)
                    {
                        var array = from Element
                                    in listeD
                                    where capt.id == Element.idsalle
                                    select Element;

                        List<CaptorDetail> liste = array.ToList();
                        capt.detailList = liste;
                    }

                    foreach (Captor capt in listeC)
                    {
                        foreach (CaptorDetail captD in capt.detailList)
                        {
                            Assert.AreEqual("temperaturearrieremaison", capt.id);
                            Assert.AreEqual("temperaturearrieremaison", captD.idsalle);
                            break;
                        }
                        break;
                    }

                }
                */
        [TestMethod]
        public void should_call_the_unic_list()
        {
            Tp_Gabriel.Files files = new Tp_Gabriel.Files(@"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\capteurs.xtim", @"C:\Users\gabriel\Desktop\SmartHome - Datas\ecole\netatmo");
            List<Captor> listeCaptor = files.getXmlAndDt();
            foreach (Captor captor in listeCaptor)
            {
                foreach (var listCaptorDetail in captor.detailList)
                {
                    Assert.AreEqual("temperaturearrieremaison", captor.id);
                    Assert.AreEqual("temperaturearrieremaison",listCaptorDetail.idsalle);
                    break;
                }
                break;
            }

        

        }
        [TestMethod]
        public int average(LinkedList<int> listeInt)
        {
            int total = 0;
            foreach (int value in listeInt)
            {
                total += value;
            }

            return total / listeInt.Count;    
            
        }

        [TestMethod]
        public void should_get_the_average()
        {
            LinkedList<int> listeInt = new LinkedList<int>();
            listeInt.AddLast(20);
            listeInt.AddLast(0);
            Assert.AreEqual(10, average(listeInt));
        }

        [TestMethod]
        public void should_compare_date()
        {
            DateTime dt = DateTime.Now;
            DateTime dt2 = dt.AddSeconds(4);
           
            Assert.AreEqual(dt.Date,dt2.Date);
        }

        [TestMethod]
        public void should_get_values()
        {
            Dictionary<DateTime, double> dicoValues = new Dictionary<DateTime, double>();
            Files files = new Files("C:\\Users\\gabriel\\Desktop\\SmartHome - Datas\\ecole\\capteurs.xtim", "C:\\Users\\gabriel\\Desktop\\SmartHome - Datas\\ecole\\netatmo");
            List<Captor> listeCaptor =files.getXmlAndDt();
            bool found = false;
            string tmpVal = "temperaturesalle";
            DateTime dt = DateTime.Parse("31/01/2014 00:03:07");
            foreach (Captor capt in listeCaptor)
            {
                if (capt.id==tmpVal) {
                    foreach (CaptorDetail captDetail in capt.detailList)
                    {
                        if (captDetail.dateHour.Day == dt.Day)
                        {
                            found = true;
                            dicoValues.Add(captDetail.dateHour,captDetail.temperatur);

                        }
                    }
                }
                if (found)
                {
                    break;
                }
                
            }

            Assert.AreEqual(true,found);
            Assert.AreNotEqual(0, dicoValues.Count);

        }

        [TestMethod]
        public void check_dates()
        {
            Files files = new Files("C:\\Users\\gabriel\\Desktop\\SmartHome - Datas\\ecole\\capteurs.xtim", "C:\\Users\\gabriel\\Desktop\\SmartHome - Datas\\ecole\\netatmo");
            List<Captor> liste = files.getXmlAndDt();
            StringBuilder stringBuilder = new StringBuilder("20140131");
            Dictionary<DateTime,double> dico =  files.getvaluesFromDay(liste, stringBuilder, "temperaturesalle");
            Assert.AreNotEqual(0, dico.Count);
        }


    }
}
