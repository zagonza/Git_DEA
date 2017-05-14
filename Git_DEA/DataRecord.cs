using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git_DEA
{
    class DataRecord
    {
        public String work { get; set; }
        public String work_before { get; set; }
        public int time { get; set; }
        private DataRecord prev= null;
        private DataRecord after = null;
        public void set_Before(DataRecord before)
        {
            this.prev = before;
        }
        public void set_After(DataRecord after)
        {
            this.after = after;
        }
        public DataRecord get_Before()
        {
            return this.prev;
        }
        public DataRecord get_After()
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
    }
}
