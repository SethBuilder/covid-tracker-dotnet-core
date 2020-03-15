using System;

namespace dg
{
    public class StatReport
    {
        public DateTime Date { get; set; }

        public int Confirmed { get; set; }

        public int Recovered { get; set; }

        public int Deaths { get; set; }
    }
}
