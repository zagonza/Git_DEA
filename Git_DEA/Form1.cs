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
            using (var sr = new StreamReader(@"D:/data_DEA.csv"))
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

                MessageBox.Show(ListDR.Count.ToString());

                //set Before Node
                for (int i = 0; i <= ListDR.Count - 1; i++)
                {
                    if (ListDR[i].work_before != "-")
                    {
                        for (int j = 0; j <= ListDR.Count - 1; j++)
                        {
                            if (ListDR[i].work_before == ListDR[j].work)
                            {
                                ListDR[i].set_Before(ListDR[j]);
                            }
                        }
                    }
                }
                //set After Node
                for (int i = 0; i <= ListDR.Count - 1; i++)
                {
                    if (ListDR[i].work_before != "-")
                    {
                        for (int j = 0; j <= ListDR.Count - 1; j++)
                        {

                            Find_Node(ListDR, ListDR[i].work_before).set_After(ListDR[i]);
                        }
                    }
                }
                //
                for (int i = 0; i <= ListDR.Count - 1; i++)
                {
                    string before = "";
                    string after = "";
                    if (ListDR[i].hasbefore())
                    {
                        before = ListDR[i].get_Before().work;
                        //Debug.Print(ListDR[i].work + " before is " + ListDR[i].get_Before().work + " after is " + ListDR[i].get_After().work);
                    }
                    else
                    {
                        before = null;
                        //Debug.Print(ListDR[i].work + " before is null");
                    }
                    if (ListDR[i].hasafter())
                    {
                        after = ListDR[i].get_After().work;
                    }
                    else
                    {
                        after = null;
                    }
                    Debug.Print(ListDR[i].work + " before is " + before + " after is " + after);
                }

                //check time
                int time = 17;

                for (int i = 0; i <= ListDR.Count - 1; i++)
                {
                    if (ListDR[i].hasbefore())
                    {
                        if (ListDR[i].time + ListDR[i].get_Before().time > 17)
                        {

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

