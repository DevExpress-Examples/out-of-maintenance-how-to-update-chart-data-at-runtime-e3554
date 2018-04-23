using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;

namespace DXCharts_Runtime {

    public partial class MainWindow : Window {
        private Random rnd;
        private ChartControl chart;

        public MainWindow () {
            InitializeComponent();
        }

        private void Window_Loaded (object sender, RoutedEventArgs e) {
            rnd = new Random();

            chart = new ChartControl();
            this.Content = chart;

            XYDiagram2D diagram = new XYDiagram2D();
            diagram.ActualAxisX.ActualRange.SideMarginsEnabled = false;
            chart.Diagram = diagram;

            diagram.ActualAxisY.ConstantLinesInFront.Add(new ConstantLine(150, "Average"));
            
            AddSeries(diagram);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            UpdateData(chart);
        }

        private void AddSeries (Diagram diagram) {
            for (int i = 0; i < 3; i++) {
                AreaStackedSeries2D area = new AreaStackedSeries2D();
                area.ArgumentScaleType = ScaleType.Numerical;
                area.ActualLabel.Visible = false;
                area.Transparency = 0.3;
                diagram.Series.Add(area);
            }

            LineSeries2D line = new LineSeries2D();
            line.ArgumentScaleType = ScaleType.Numerical;
            line.ActualLabel.Visible = false;
            diagram.Series.Add(line);
        }

        private void UpdateData (ChartControl chart) {
            chart.BeginInit();

            foreach (Series series in chart.Diagram.Series) {
                series.Points.Clear();

                for (int i = 0; i <= 100; i++) {
                    SeriesPoint p = new SeriesPoint(i, rnd.Next(50, 100));
                    series.Points.Add(p);
                }
            }

            chart.EndInit();
        }

        void timer_Tick (object sender, EventArgs e) {
            UpdateData(chart);
        }

    }
}
