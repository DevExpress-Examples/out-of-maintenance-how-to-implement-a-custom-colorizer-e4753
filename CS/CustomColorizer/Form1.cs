using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraMap;

namespace CustomColorizer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        const string powerAttrName = "PowerValue";
        const double maxPower = 1000;
        const int rectNumber = 50;

        private void Form1_Load(object sender, EventArgs e) {

            // Create a map control, set its dock style and add it to the form.
            MapControl map = new MapControl();
            map.Dock = DockStyle.Fill;
            this.Controls.Add(map);

            // Create a vector items layer and add it to the map.
            VectorItemsLayer itemsLayer = new VectorItemsLayer();
            map.Layers.Add(itemsLayer);

            MapItemStorage storage = new MapItemStorage();
            itemsLayer.Data = storage;
            // Generate map polygons.
            GenerateVectorItems(storage.Items);

            // Specify the tooltip content.            
            itemsLayer.ToolTipPattern = "{" + powerAttrName + "}";

            // Create a custom colorizer.
            itemsLayer.Colorizer = new CustomColorizer();

        }

        private void GenerateVectorItems(MapItemCollection col) {
            int width = 5;
            double singlePower = maxPower / rectNumber;

            for (int i = 0; i < rectNumber; i++) {
                MapPolygon polygon = CreatePolygon(i * singlePower,
                new GeoPoint[] { new GeoPoint(0, width * i), 
                                 new GeoPoint(0, width * (i + 1)),
                                 new GeoPoint(40, width * (i + 1)),
                                 new GeoPoint(40, width * i), 
                                 new GeoPoint(0, width * i) });
                col.Add(polygon);
            }
        }


        private MapPolygon CreatePolygon(double power, GeoPoint[] points) {
            MapPolygon item = new MapPolygon();

            foreach (GeoPoint point in points) {
                item.Points.Add(point);
            }

            item.Attributes.Add(new MapItemAttribute() { Name = powerAttrName, Type = typeof(Double), Value = power });

            return item;
        }


        private class CustomColorizer : MapColorizer {

            Color[] colors = {Color.Violet, Color.Blue, Color.LightBlue, Color.Green,Color.Yellow, Color.Orange, Color.Red};

            public override void ColorizeElement(IColorizerElement element) {

                MapPolygon polygon = element as MapPolygon;
                if (polygon != null) {
                    double power = (double)polygon.Attributes[powerAttrName].Value;

                    int linearizedPower = (int)Math.Truncate(power * colors.Length / maxPower);

                    element.ColorizerColor = colors[linearizedPower];
                }
            }


        }
    }
}

