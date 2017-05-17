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
        /*
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
        */
        private void button2_Click(object sender, EventArgs e)
        {
            Flow flow = new Flow(@"D:/data_DEA_new.csv");
            List<DataRecord> LDR = flow.getList();

            foreach (DataRecord aa in LDR)
            {
                foreach (DataRecord aaa in aa.get_After())
                {
                    //MessageBox.Show(aa.work + " after is " + aaa.work);
                }
            }

            flow.Combination(17); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<List<List<string>>> source = new List<List<List<string>>>();
            List<string> target = new List<string>();
            List<string> a1 = new List<string>(new string[] { "8" });
            List<string> a2 = new List<string>(new string[] { "7" });
            List<string> a3 = new List<string>(new string[] { "6" });
            List<string> a4 = new List<string>(new string[] { "5" });
            List<string> a5 = new List<string>(new string[] { "4" });
            List<string> a6 = new List<string>(new string[] { "3" });
            List<string> a7 = new List<string>(new string[] { "2" });
            List<string> a8 = new List<string>(new string[] { "1" });
            List<List<string>> source1 = new List<List<string>>();
            source1.Add(a1);
            source1.Add(a2);
            source1.Add(a3);
            source1.Add(a4);
            source1.Add(a5);
            source1.Add(a6);
            source1.Add(a7);
            source1.Add(a8);
            //source.Add(source1);
            target.Add("7");
            target.Add("8");
            Flow f = new Flow();
            List<List<string>> buffer = new List<List<string>>();
            if (f.compare_List(source1, target))
            {
                
               // buffer = lls;
                /*
                foreach (string t in target)
                {
                    int i = 0;
                    foreach (List<List<string>> lls in source)
                    {
                        foreach (List<string> ls in lls)
                        {
                            foreach (string d in ls)
                            {
                                if (d == t)
                                {
                                    buffer = lls;
                                }

                            }
                        }
                        i++;
                    }

                }*/
            }
            foreach(List<string> ls in buffer)
            {
                Debug.Write("{");
                foreach (string s in ls)
                {
                    Debug.Write("a");
                }
                Debug.Write("}");
            }
            //Debug.Write(f.compare_List(source, target));
        }
    }
}

