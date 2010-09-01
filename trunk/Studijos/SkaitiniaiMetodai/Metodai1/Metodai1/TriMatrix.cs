using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    struct Coefficents
    {
        public decimal C, D;
    }

    class TriMatrix
    {
        private List<decimal> _matrixCol1 = new List<decimal>();
        private List<decimal> _matrixCol2 = new List<decimal>();
        private List<decimal> _matrixCol3 = new List<decimal>();
        private List<decimal> _resultMatrix = new List<decimal>();
        private List<decimal> _coefficientD = new List<decimal>();
        private List<decimal> _coefficientC = new List<decimal>();

        public int RowCount
        {
            get
            {
                int count1 = _matrixCol1.Count;
                int count2 = _matrixCol2.Count;
                int count3 = _matrixCol3.Count;
                int result = count1;
                if (count2 > result)
                {
                    result = count2;
                }
                if (count3 > result)
                {
                    result = count3;
                }
                return result;
            }
        }

        public decimal this[int j]
        {
            get
            {
                if ((j >= 0) && (j < _resultMatrix.Count))
                {
                    return _resultMatrix[j];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (j >= 0)
                {
                    if (j < _resultMatrix.Count)
                    {
                        _resultMatrix[j] = value;
                    }
                    else
                    {
                        for (int i = _resultMatrix.Count - 1; i < j - 1; i++)
                        {
                            _resultMatrix.Add(0);
                        }
                        _resultMatrix.Add(value);
                    }
                }
            }
        }

        public decimal this[int i, int j]
        {
            get
            {
                if ((i >= 0) && (i <= 2))
                {
                    List<decimal> list = new List<decimal>();
                    switch (i)
                    {
                        case 0:
                            list = _matrixCol1;
                            break;
                        case 1:
                            list = _matrixCol2;
                            break;
                        case 2:
                            list = _matrixCol3;
                            break;
                    }
                    if ((j >= 0) && (j < RowCount))
                    {
                        if (j < list.Count)
                        {
                            return list[j];
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                return 0;
            }
            set
            {
                if ((i >= 0) && (i <= 2))
                {
                    List<decimal> list = new List<decimal>();
                    switch (i)
                    {
                        case 0:
                            list = _matrixCol1;
                            break;
                        case 1:
                            list = _matrixCol2;
                            break;
                        case 2:
                            list = _matrixCol3;
                            break;
                    }
                    if ((j >= 0) && (j < RowCount))
                    {
                        if (j < list.Count)
                        {
                            list[j] = value;
                        }
                        else
                        {
                            for (int k = list.Count - 1; k < j - 1; k++)
                            {
                                list.Add(0);
                            }
                            list.Add(value);
                        }
                    }
                }
            }
        }

        public bool CheckForCorrectness()
        {
            int numberOfStrict = 0;            
            for (int i = 0; i < RowCount; i++)
            {
                if (this[0, i] + this[2, i] > this[1, i])
                {
                    return false;
                }
                else
                {
                    if (this[1, i] > this[0, i] + this[2, i])
                    {
                        numberOfStrict++;
                    }
                }
            }
            return numberOfStrict > 0;
        }

        public Coefficents CalculateLevel(int i)
        {
            Coefficents result = new Coefficents();
            if (_coefficientC.Count - 1 >= i)
            {
                result.C = _coefficientC[i - 1];
                result.D = _coefficientD[i - 1];
            }
            else
            {
                if (i == 1)
                {
                    result.C = -this[2, 0] / this[1, 0];
                    result.D = this[0] / this[1, 0];
                }
                else
                {
                    Coefficents tempCoeff = CalculateLevel(i - 1);
                    decimal bottom = this[0, i] * tempCoeff.C + this[1, i];
                    result.C = -this[2, i] / bottom;
                    result.D = this[i] - this[0, i] * tempCoeff.D;
                }
                for (int j = 0; j < i - 1; j++)
                {
                    _coefficientC.Add(0);
                    _coefficientD.Add(0);
                }
                _coefficientC.Add(result.C);
                _coefficientD.Add(result.D);
            }
            return result;
        }

        public static TriMatrix Load(string data)
        {
            string[] lines = data.Split(new char[] { '\n' });
            TriMatrix result = new TriMatrix();
            int currentLine = 0;
            foreach (string line in lines)
            {
                string[] columns = line.Split(new char[] { ' ' });
                if (columns.Length == 3)
                {
                    result[0, currentLine] = decimal.Parse(columns[0]);
                    result[1, currentLine] = decimal.Parse(columns[1]);
                    result[2, currentLine] = decimal.Parse(columns[2]);
                }
                else
                {
                    if (currentLine > 0)
                    {
                        result[0, currentLine] = decimal.Parse(columns[currentLine - 1]);
                    }
                    else
                    {
                        result[0, currentLine] = 0;
                    }
                    result[1, currentLine] = decimal.Parse(columns[currentLine]);
                    result[2, currentLine] = decimal.Parse(columns[currentLine + 1]);
                }
                currentLine++;
            }
            return result;
        }

        public void LoadResults(string data)
        {
            string[] lines = data.Split(new char[] { '\n' });

        }

        public List<decimal> Solve()
        {
            List<decimal> tempResult = new List<decimal>();
            int start = RowCount - 1;
            tempResult.Add(_coefficientD[RowCount - 1]);
            for (int i = start; i >= 1; i--)
            {
                decimal current = tempResult[tempResult.Count - 1] * _coefficientC[i - 1] + _coefficientD[i - 1];
                tempResult.Add(current);
            }
            return tempResult.Reverse();
        }
    }
}
