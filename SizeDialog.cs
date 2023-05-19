using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect_4
{
    public partial class SizeDialog : Form
    {
        public SizeDialog()
        {
            InitializeComponent();
            radioButton2.Checked = true;   

        }

        public int [,] BoardSize
        {
            get 
            {
                int [,] size;
                size= new int[0,0];

                if (radioButton1.Checked)
                {
                    size = new int [6, 5];
                }
                if (radioButton2.Checked)
                {
                    size =  new int[6, 7];
                }
                if (radioButton3.Checked)
                {
                    size = new int[7, 8];
                }


                return  size;
            }
        }


        //OK Button
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.OK;
            this.Close();
        }

        //CANCEL Button
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
