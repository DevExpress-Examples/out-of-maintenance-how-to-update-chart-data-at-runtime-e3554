Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Xpf.Charts

Namespace DXCharts_Runtime

	Partial Public Class MainWindow
		Inherits Window
		Private rnd As Random
		Private chart As ChartControl

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			rnd = New Random()

			chart = New ChartControl()
			Me.Content = chart

			Dim diagram As New XYDiagram2D()
			diagram.ActualAxisX.ActualRange.SideMarginsEnabled = False
			chart.Diagram = diagram

			diagram.ActualAxisY.ConstantLinesInFront.Add(New ConstantLine(150, "Average"))

			AddSeries(diagram)

			Dim timer As New DispatcherTimer()
			timer.Interval = TimeSpan.FromSeconds(5)
			AddHandler timer.Tick, AddressOf timer_Tick
			timer.Start()

			UpdateData(chart)
		End Sub

		Private Sub AddSeries(ByVal diagram As Diagram)
			For i As Integer = 0 To 2
				Dim area As New AreaStackedSeries2D()
				area.ArgumentScaleType = ScaleType.Numerical
				area.ActualLabel.Visible = False
				area.Transparency = 0.3
				diagram.Series.Add(area)
			Next i

			Dim line As New LineSeries2D()
			line.ArgumentScaleType = ScaleType.Numerical
			line.ActualLabel.Visible = False
			diagram.Series.Add(line)
		End Sub

		Private Sub UpdateData(ByVal chart As ChartControl)
			chart.BeginInit()

			For Each series As Series In chart.Diagram.Series
				series.Points.Clear()

				For i As Integer = 0 To 100
					Dim p As New SeriesPoint(i, rnd.Next(50, 100))
					series.Points.Add(p)
				Next i
			Next series

			chart.EndInit()
		End Sub

		Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
			UpdateData(chart)
		End Sub

	End Class
End Namespace
