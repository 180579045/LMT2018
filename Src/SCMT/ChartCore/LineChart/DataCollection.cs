using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LineChart
{

    /// <summary>
    /// 折线图数据集;
    /// </summary>
    public class DataCollection
    {
        private List<DataSeries> dataList;

        public DataCollection()
        {
            dataList = new List<DataSeries>();
        }

        // 一张图中可支持多条线;
        public List<DataSeries> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public void AddLines(ChartStyle cs)
        {
            int j = 0;
            foreach (DataSeries ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                {
                    ds.SeriesName = "DataSeries" + j.ToString();
                }
                ds.AddLinePattern();
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    ds.LineSeries.Points[i] = cs.NormalizePoint(ds.LineSeries.Points[i]);
                    ds.Symbols.AddSymbol(cs.ChartCanvas, ds.LineSeries.Points[i]);
                }
                cs.ChartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }

        public void AddPoint(ChartStyleGridlines cs, DataSeries ds2)
        {
            ds2.AddLinePattern();

            // 转化为屏幕坐标;
            for (int i = 0; i < ds2.LineSeries.Points.Count; i++)
            {
                ds2.Symbols.AddSymbol(cs.ChartCanvas, ds2.LineSeries.Points[i]);
            }
            cs.ChartCanvas.Children.Clear();

            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle2();
            cs.ChartCanvas.Children.Add(ds2.LineSeries);
        }
        
    }
}
