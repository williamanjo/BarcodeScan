using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BarcodeScan
{
    public partial class BarcodeScan : Form
    {
        public BarcodeScan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SelectArea selectArea = new SelectArea(this);
            this.Hide();
            selectArea.ShowDialog();
            this.Show();

        }
        public void Save(Int32 x, Int32 y, Int32 w, Int32 h, Size s)
        {
            textBox1.Text = "";
            Rectangle rect = new Rectangle(x, y, w, h);
            using (var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);
                }
                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(bmp);
                Clipboard.Clear();
                if (result != null)
                {
                    textBox1.Text = result.ToString();
                    Clipboard.SetText(result.ToString());
                }
                else
                {
                    Clipboard.Clear();
                }
            }
        }
    }
}
