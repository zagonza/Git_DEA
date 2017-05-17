using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using System.Diagnostics;
namespace Git_DEA
{
    class Flow
    {
        
        List<DataRecord> ListDR;
        List<int> dup_subindex;
        List<List<string>> buffer = new List<List<string>>();
        public Flow() { }
        public Flow(string pathcsv)
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
                ListDR = b.ToList();

                //
                foreach (DataRecord aa in ListDR)
                {
                    aa.intial_L_perv();
                }

                //set Before Node
                for (int i = 0; i <= ListDR.Count - 1; i++)
                {
                    foreach (string before in ListDR[i].L_prev)
                    {                       
                        ListDR[i].set_Before(Find_Node(before));
                    }

                }
                //test
                foreach (DataRecord aa in ListDR)
                {
                    string dogCsv = string.Join(",", aa.L_prev.ToArray());
                   
                }
                //set After Node
                foreach (DataRecord record in ListDR)
                {
                    foreach (string before in record.L_prev)
                    {
                        if (before != "-")
                        {

                            Find_Node( before).set_After(record);
                            //MessageBox.Show(record.work + " next is " + before);
                        }
                    }
                }
            }
        }
        public DataRecord Find_Node(string name)
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

        public void Combination(int ct)
        {
            List<List<DataRecord>> Resultlist = new List<List<DataRecord>>();
            int counts = 0;
            double count = Math.Pow(2, ListDR.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                int sum = 0;

                List<DataRecord> result = new List<DataRecord>();
                string str = Convert.ToString(i, 2).PadLeft(ListDR.Count, '0');
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        sum += ListDR[j].time;
                        result.Add(ListDR[j]);
                    }
                }
                if (sum <= ct)
                {
                    Resultlist.Add(result);
                    foreach (DataRecord a in result)
                    {
                        //Debug.Write(a.work + ",");

                    }
                    counts++;
                    //Debug.Write("\t" + sum);
                    //Debug.WriteLine("");
                }
            }

            //find max member from Combinating
            int max_member = 0;
            foreach (List<DataRecord> a in Resultlist)
            {
                if (a.Count > max_member)
                {
                    max_member = a.Count;
                }
            }
            Debug.WriteLine("max member is " + max_member);
            //group by member of data
            List<List<List<DataRecord>>> Resultlist2 = new List<List<List<DataRecord>>>();
            List<List<DataRecord>> buffer = new List<List<DataRecord>>();
            for (int i = 1; i <= max_member; i++)
            {
                buffer = new List<List<DataRecord>>();
                foreach (List<DataRecord> a in Resultlist)
                {
                    if (a.Count == i)
                    {
                        buffer.Add(a);
                        Debug.WriteLine("a is " + a.Count);
                    }
                }
                Resultlist2.Add(buffer);
            }
            //test
            Debug.WriteLine("buffer is " + buffer.Count);
            Debug.WriteLine(" Resultlist2 is " + Resultlist2.Count);
            foreach (List<List<DataRecord>> aa in Resultlist2)
            {
                foreach (List<DataRecord> ldr in aa)
                {
                    Debug.Write("{");
                    foreach (DataRecord dr in ldr)
                    {
                        Debug.Write(dr.work);
                    }
                    Debug.Write("}");
                }
                Debug.WriteLine("");
            }

            //
            List<List<List<DataRecord>>> CombineList = new List<List<List<DataRecord>>>();
            for(int i = 0; i<= Resultlist2.Count-1; i++)
            {
                if(i == 0 ) // 1 member
                {
                    CombineList.Add(Resultlist2[i]);
                }
                else
                {
                    
                }
            }
        }


        public bool compare_List(List<List<string>> source, List<string> target)
        {
            List<bool> chk = new List<bool>();

            bool output = false;
            dup_subindex = new List<int>();
            Remove_item ret = new Remove_item();
            
            foreach (string t in target)
            {
                int i = 0;
                
                foreach (List<string> ls in source)
                {

                        foreach (string d in ls)
                        {
                            if (d == t)
                            {
                                Debug.Write("Source is : " + d + " Target is : " + t);
                                output = true;
                                dup_subindex.Add(i);
                                ret.index_remove.Add(i);
                                //Debug.WriteLine(" i : " + i);
                                goto endloop;
                                //break;
                            }
                            else
                            {
                                //Debug.Write("Source is : " + d + " Target is : " + t);
                                output = false;
                            }
                        }
                        i++;
                    
                    
                }
                endloop:
                Debug.WriteLine("");
                chk.Add(output);
                
            }
            
            foreach (bool a in chk)
            {
                Debug.WriteLine(a);
                if (a == false)
                {
                    output = false;
                    break;
                }
            }

            foreach(int i in dup_subindex)
            {
                Debug.WriteLine("dup is "+i);
            }
            return output;
        }


        public List<DataRecord> getList()
        {
            return ListDR;
        }
         public List<int> getDup()
        {
            return dup_subindex;
        }
    }
}



/* backup
public bool compare_List(List<List<string>> source, List<string> target)
{
    List<bool> chk = new List<bool>();

    bool output = false;
    dup_subindex = new List<int>();
    foreach (string t in target)
    {
        int i = 0;

        foreach (List<string> ls in source)
        {

            foreach (string d in ls)
            {
                if (d == t)
                {
                    Debug.Write("Source is : " + d + " Target is : " + t);
                    output = true;
                    dup_subindex.Add(i);
                    //Debug.WriteLine(" i : " + i);
                    goto endloop;
                    //break;
                }
                else
                {
                    //Debug.Write("Source is : " + d + " Target is : " + t);
                    output = false;
                }
            }
            i++;


        }
        endloop:
        Debug.WriteLine("");
        chk.Add(output);

    }*/