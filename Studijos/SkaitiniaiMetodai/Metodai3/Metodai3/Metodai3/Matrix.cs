using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai3
{
    class Matrix
    {
        public List<List<double>> Values = new List<List<double>>();

        public double this[int i, int j]
        {
            get
            {
                if (Values.Count < j + 1)
                {
                    return 0;
                }
                else
                {
                    if (Values[j] != null)
                    {
                        if (Values[j].Count >= i + 1)
                        {
                            return Values[j][i];
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            set
            {
                if (Values.Count < j + 1)
                {
                    for (int k = Values.Count; k < j + 1; k++)
                    {
                        Values.Add(new List<double>());
                    }
                }
                if (Values[j].Count < i + 1)
                {
                    for (int k = Values[j].Count; k < i + 1; k++)
                    {
                        Values[j].Add(0);
                    }
                }
                Values[j][i] = value;
            }
        }

        public int Width
        {
            get
            {
                int width = 0;
                for (int j = 0; j < Values.Count; j++)
                {
                    if (Values[j].Count > width)
                    {
                        width = Values[j].Count;
                    }
                }
                return width;
            }
        }

        public int Height
        {
            get
            {
                return Values.Count;
            }
        }


        public bool isSimetric()
        {
            bool ats = true;
            for (int i = 0; i < Values.Count; i++)
            {
                for (int j = 0; j < Values.Count; j++)
                {
                    if (Values[j][i] != Values[i][j])
                    {
                        ats = false;
                    }
                }
            }
            return ats;
        }

        public Matrix MulNumber(double numb)
        {
            Matrix result = new Matrix();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    result[i, j] = this[i, j] * numb;
                }
            }
            return result;
        }

        public Matrix Add(Matrix add)
        {
            Matrix result = new Matrix();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    result[i, j] = this[i, j] + add[i, j];
                }
            }
            return result;
        }

        public Matrix Sub(Matrix sub)        
        {
            return Add(sub.MulNumber(-1));
        }

        public Matrix FromRow(int rowNr)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < Width; i++)
            {
                result[i, 0] = this[i, rowNr];
            }
            return result;
        }

        public Matrix Mul(Matrix mul)
        {
            Matrix result = new Matrix();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < mul.Width; i++)
                {                   
                    result[i, j] = ScalarMul(this, mul, i, j);
                }
            }
            return result;
        }

        public static double ScalarMul(Matrix a, Matrix b, int i, int j)
        {
            double result = 0;
            for (int k = 0; k < a.Width; k++)
            {
                result += a[k, j] * b[i, k];
            }
            return result;
        }

        public Matrix Rotate()
        {
            Matrix result = new Matrix();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    result[j, i] = this[i, j];
                }
            }
            return result;
        }

        public static Matrix FromList(List<double> values)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < values.Count; i++)
            {
                result[i, 0] = values[i];
            }
            return result;
        }

        public void AddLine(List<double> values)
        {
            AddLine(FromList(values));
        }

        public void AddLine(Matrix line)
        {
            int j = Height;
            for (int i = 0; i < line.Width; i++)
            {
                this[i, j] = line[i, 0];
            }
        }

        public void Clear()
        {
            Values.Clear();
        }

        public Matrix Clone()
        {
            Matrix result = new Matrix();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    result[i, j] = this[i, j];
                }
            }
            return result;
        }

        public static double ScalarVector(Matrix a, Matrix b)
        {
            return ScalarMul(a, b, 0, 0);
        }
    }
}
