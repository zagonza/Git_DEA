using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Git_DEA
{
    class DataRecord
    {
        public String work { get; set; }
        public String work_before { get; set; }
        public int time { get; set; }
        private List<DataRecord> prev = null;
        private List<DataRecord> after = null;
        public List<string> L_prev = new List<string>();

        public DataRecord ()
        {
            prev = new List<DataRecord>();
            after = new List<DataRecord>();
        }

        public void set_Before(DataRecord before)
        {
            this.prev.Add(before);
        }

        public void set_After(DataRecord after)
        {
            this.after.Add(after);
        }

        public List<DataRecord> get_Before()
        {
            return this.prev;
        }

        public List<DataRecord> get_After()
        {
            return this.after;
        }

        public Boolean hasbefore()
        {
            Boolean output = false;
            if(this.prev != null)
            {
                output = true;
            }
            else
            {
                output = false;
            }
            return output;
        }

        public Boolean hasafter()
        {
            Boolean output = false;
            if (this.after != null)
            {
                output = true;
            }
            else
            {
                output = false;
            }
            return output;
        }

        public void intial_L_perv()
        {
            if (work_before.IndexOf("-") != -1)
            {
                //MessageBox.Show("Has -");
                L_prev.Add("-");
            }
            else if (work_before.IndexOf(",") != -1)
            {
                L_prev = work_before.Split(',').ToList();
                /*MessageBox.Show("Has ," + L_prev.Count);
                foreach(string a in L_prev)
                {
                    MessageBox.Show("Has , member is " + a);
                }*/
            }
            else
            {
                L_prev.Add(work_before);
            }
        }
    }
}
