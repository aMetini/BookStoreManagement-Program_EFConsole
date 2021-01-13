using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Proposal1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TBMContext())
            {
                if (db.Database.CanConnect())
                {
                    db.Stores
                }
                else Debug.WriteLine("Connection Failed!");
            }
        }
    }
}
