using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTUzd.Models
{
    public class CyclotomicCoset
    {
        public List<int> Items { get; set; }
        public int N { get; private set; }
        public int Q { get; private set; }
        public int S { get; private set; }
        public int I { get; private set; }

        public static CyclotomicCoset Calculate(int n, int q, int i)
        {
            int s = 1;
            var result = new CyclotomicCoset {N = n, Q = q, I = i, Items = new List<int> {i}};

            int qGrade = q;            
            while ((i * qGrade) % n != i)
            {
                if ((i * qGrade) % n == 0)
                {
                    result.Items.Clear();
                    result.S = 0;
                    return result;
                }
                result.Items.Add((qGrade*i)%n);
                qGrade *= q;
                s++;                
            }
            result.S = s;
            return result;
        }
    }

    

}
