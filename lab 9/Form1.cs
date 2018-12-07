using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_9
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private double p = 1; 
        private void btnCalc_Click(object sender, EventArgs e)
        {
            // Перетворення текстових рядків, які ввів користувач,  
            // у змінні числового типу 
            double x1min = double.Parse(tbx1min.Text);
            double x1max = double.Parse(tbx1max.Text);
            double x2min = double.Parse(tbx2min.Text);
            double x2max = double.Parse(tbx2max.Text);
            double dx1 = double.Parse(tbdx1.Text);
            double dx2 = double.Parse(tbdx2.Text);

            // Обчислення кількості рядків та стовпчиків таблиці 
            gv.ColumnCount = (int)Math.Truncate((x2max - x2min) / dx2) + 1;
            gv.RowCount = (int)Math.Truncate((x1max - x1min) / dx1) + 1;

            // Вивід заголовків рядків та стовпців таблиці 
            for (int i = 0; i < gv.RowCount; i++)
                gv.Rows[i].HeaderCell.Value = (x1min + i * dx1).ToString("0.000");
            gv.RowHeadersWidth = 80; 

            for (int i = 0; i < gv.ColumnCount; i++)
            {
                gv.Columns[i].HeaderCell.Value = (x2min + i * 
                    dx2).ToString("0.000");
                gv.Columns[i].Width = 60;
            }


            int cl, rw;
            double x1, x2, y;


            // Розрахунок і вивід результатів 
 
            rw = 0;
            x1 = x1min;
            while (x1 <= x1max)
            {
                x2 = x2min;
                cl = 0;
                while (x2 <= x2max)
                {
                   double t = x1 + 2 * x2 + 9 / (0.37);
                    y = (3 * x2 - Math.Pow(x1, 2)) / Math.Pow(Math.Cos(t), 3);
                    gv.Rows[rw].Cells[cl].Value = y.ToString("0.000");
                    x2 += dx2;
                    cl++;
                    if( y >= 0)
                    {
                        p *= y;
                    } 
                }
                x1 += dx1;
                rw++;
            }

            TbDob.Text = p.ToString("0.0000");
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            tbx1min.Text = "";
            tbx1max.Text = "";
            tbx2min.Text = "";
            tbx2max.Text = "";
            tbdx1.Text = "";
            tbdx2.Text = "";
            TbDob.Text = "";

            gv.Rows.Clear();
            for (int Cl = 0; Cl < gv.ColumnCount; Cl++)
                gv.Columns[Cl].HeaderCell.Value = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити програму?", "Вихід з програми", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Application.Exit();
        }

    }
}
