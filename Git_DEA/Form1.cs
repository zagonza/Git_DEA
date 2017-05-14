using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace Git_DEA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader(@"D:/data_DEA_new.csv"))
            {
                var reader = new CsvReader(sr);

                //CSVReader will now read the whole file into an enumerable
                IEnumerable<DataRecord> records = reader.GetRecords<DataRecord>();

                var b = (from myObj in records
                         select new DataRecord
                         {
                             work = myObj.work,
                             work_before = myObj.work_before,
                             time = myObj.time
                         }).ToArray();
                List<DataRecord> ListDR = b.ToList();

                //
                foreach (DataRecord aa in ListDR)
                {
                    aa.intial_L_perv();
                }
                //
                MessageBox.Show(ListDR.Count.ToString());

                //set Before Node
                for (int i = 0; i <= ListDR.Count - 1; i++)
                {
                    foreach (string before in ListDR[i].L_prev)
                    {
                        //MessageBox.Show(ListDR[i].work + " prev is "+ before);
                        ListDR[i].set_Before(Find_Node(ListDR, before));
                    }

                }
                //test
                foreach (DataRecord aa in ListDR)
                {
                    string dogCsv = string.Join(",", aa.L_prev.ToArray());
                    MessageBox.Show(aa.work + " prev is " + dogCsv);
                    if (aa.L_prev.Count > 1)
                    {
                        MessageBox.Show(aa.work + "Has prev more than 1 it has : " + aa.L_prev.Count);
                    }
                }

                //set After Node
                foreach (DataRecord record in ListDR)
                {
                    foreach (string before in record.L_prev)
                    {
                        if (before != "-")
                        {

                            Find_Node(ListDR, before).set_After(record);
                            //MessageBox.Show(record.work + " next is " + before);
                        }
                    }
                }
                //test
                {
                    foreach (DataRecord aa in ListDR)
                    {
                        foreach (DataRecord aaa in aa.get_After())
                        {
                            MessageBox.Show(aa.work + " after is " + aaa.work);
                        }

                    }
                }
            }
        }

        private DataRecord Find_Node(List<DataRecord> ListDR, string name)
        {
            DataRecord output = null; ;
            for (int i = 0; i <= ListDR.Count - 1; i++)
            {
                if (ListDR[i].work == name)
                {
                    output = ListDR[i];
                    break;
                }
            }
            return output;
        }

    }
}

