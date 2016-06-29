using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Gabriel
{
    class Graph
    {
        public PlotModel GraphModel { get; set; }
        
        public Graph(List<Captor> list,StringBuilder sb,string id)
        {
            GraphModel = new PlotModel();
            
            string unite =  list.Find(elemet => elemet.id == id).unite;
            configChart(id,unite);
            displayValueOnTheGraph(list,sb,id);
        }

        public Graph()
        {

        }

        private void configChart(string id,string unite)
        {
            GraphModel.Title = "Graphique de "+id+" l'unite est "+unite; // Titre du graphique
            
            
            LinearAxis ordonnee = new LinearAxis();
            ordonnee.Title = "Degres";
            ordonnee.Position = AxisPosition.Left;
            //////////Ajout des axes au PlotModel//////////
            var abscisse = new OxyPlot.Axes.DateTimeAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                StringFormat = "dd/MM/yyyy",
                Title = "Date",
            };
            GraphModel.Axes.Add(abscisse);
            GraphModel.Axes.Add(ordonnee);

        }

        public void displayValueOnTheGraph(List<Captor> list,StringBuilder builder,string id)
        {
            Files files = new Files();
            Dictionary<DateTime,double> dicoValues =  files.getvaluesFromDay(list, builder, id);
            OxyPlot.Series.LineSeries lineseries = new OxyPlot.Series.LineSeries();
            foreach (KeyValuePair<DateTime, double> item in dicoValues)
            {
                lineseries.Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(item.Key), item.Value));
                
            }
            GraphModel.Series.Add(lineseries);
        }

    }
}
