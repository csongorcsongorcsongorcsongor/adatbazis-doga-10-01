using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        databaseHandler db;
        public Form1()
        {
            InitializeComponent();
            db = new databaseHandler();

            start();
        }
        void start()
        {
            updlistbox();
            label4.AutoSize = true;
            label4.Visible = false;
            button1.Click += (s, e) =>
            {
                if (textBox1.Text.Length >= 3 && numericUpDown1.Value != null && numericUpDown2.Value != null)
                {
                    aru onearu = new aru();
                    onearu.nev = textBox1.Text;
                    onearu.mennyiseg = Convert.ToInt32(numericUpDown1.Value);
                    onearu.ar = Convert.ToInt32(numericUpDown2.Value);
                    db.aruk.Add(onearu);
                    db.addone(textBox1.Text, Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
                    updlistbox();
                }
                else
                {
                    errmsg("Nem elég hosszu");
                }
            };
            button2.Click += (s, e) =>
            {
                int currentindex = listBox1.SelectedIndex;
                try
                {
                    db.delone(db.aruk[currentindex]);

                }
                catch (Exception)
                {
                    errmsg("Nincs kiválasztva");
                }
                updlistbox();
            };
        }
        void updlistbox()
        {
            db.aruk.Clear();
            db.readAll();
            
            listBox1.Items.Clear();
            foreach (aru item in db.aruk)
            {
                listBox1.Items.Add($"ID:{item.id}; NEV: {item.nev}; MENNYISEG: {item.mennyiseg}; AR: {item.ar}");
            }
        }
        void errmsg(string szoveg)
        {
            Timer delay = new Timer();
            delay.Interval = 1200;
            label4.Visible = true;
            label4.ForeColor = Color.Red;
            label4.Text = szoveg;
            delay.Start();
            delay.Tick += (ss, ee) =>
            {
                label4.Visible = false;
                delay.Stop();

            };
        }
    }
}
