Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraMap

Namespace CustomColorizer
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Const powerAttrName As String = "PowerValue"
		Private Const maxPower As Double = 1000
		Private Const rectNumber As Integer = 50

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

			' Create a map control, set its dock style and add it to the form.
			Dim map As New MapControl()
			map.Dock = DockStyle.Fill
			Me.Controls.Add(map)

			' Create a vector items layer and add it to the map.
			Dim itemsLayer As New VectorItemsLayer()
			map.Layers.Add(itemsLayer)

			' Generate map polygons.
			GenerateVectorItems(itemsLayer.Items)

			' Specify the tooltip content.            
			itemsLayer.ToolTipPattern = "{" & powerAttrName & "}"

			' Create a custom colorizer.
			map.Colorizer = New CustomColorizer()

		End Sub

		Private Sub GenerateVectorItems(ByVal col As LayerMapItemCollection)
			Dim width As Integer = 5
			Dim singlePower As Double = maxPower / rectNumber

			For i As Integer = 0 To rectNumber - 1
				Dim polygon As MapPolygon = CreatePolygon(i * singlePower, New GeoPoint() { New GeoPoint(0, width * i), New GeoPoint(0, width * (i + 1)), New GeoPoint(40, width * (i + 1)), New GeoPoint(40, width * i), New GeoPoint(0, width * i) })
				col.Add(polygon)
			Next i
		End Sub


		Private Function CreatePolygon(ByVal power As Double, ByVal points() As GeoPoint) As MapPolygon
			Dim item As New MapPolygon()

			For Each point As GeoPoint In points
				item.Points.Add(point)
			Next point

			item.Attributes.Add(New MapItemAttribute() With {.Name = powerAttrName, .Type = GetType(Double), .Value = power})

			Return item
		End Function


		Private Class CustomColorizer
			Inherits MapColorizer

			Private colors() As Color = {Color.Violet, Color.Blue, Color.LightBlue, Color.Green,Color.Yellow, Color.Orange, Color.Red }

			Public Overrides Sub ColorizeElement(ByVal element As IColorizerElement)

				Dim polygon As MapPolygon = TryCast(element, MapPolygon)
				If polygon IsNot Nothing Then
					Dim power As Double = CDbl(polygon.Attributes(powerAttrName).Value)

					Dim linearizedPower As Integer = CInt(Fix(Math.Truncate(power * colors.Length / maxPower)))

					element.ColorizerColor = colors(linearizedPower)
				End If
			End Sub

		End Class
	End Class
End Namespace

