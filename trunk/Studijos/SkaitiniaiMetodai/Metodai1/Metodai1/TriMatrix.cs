using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    struct Coefficents
    {
        public double C, D;
    }

    class TriMatrix
    {
        private List<double> _matrixCol1 = new List<double>();
        private List<double> _matrixCol2 = new List<double>();
        private List<double> _matrixCol3 = new List<double>();
        private List<double> _resultMatrix = new List<double>();
        private List<double> _coefficientD = new List<double>();
        private List<double> _coefficientC = new List<double>();

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

        public double this[int j]
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

        public double this[int i, int j]
        {
            get
            {
                if ((i >= 0) && (i <= 2))
                {
                    List<double> list = new List<double>();
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
                    List<double> list = new List<double>();
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
                    if ((j >= 0) && (j <= RowCount))
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

        public string ErrorText
        {
            get;
            set;
        }

        public bool CheckForCorrectness()
        {
            int numberOfStrict = 0;            
            for (int i = 0; i < RowCount; i++)
            {
                if (Math.Abs(this[0, i]) + Math.Abs(this[2, i]) > Math.Abs(this[1, i]))
                {
                    ErrorText = "Eilutėje " + i.ToString() + " nepatenkinta įstrižainės vyravimo sąlyga";
                    return false;
                }
                else
                {
                    if (Math.Abs(this[1, i]) > Math.Abs(this[0, i]) + Math.Abs(this[2, i]))
                    {
                        numberOfStrict++;
                    }
                }
            }
            if (numberOfStrict == 0)
            {
                ErrorText = "Nėra nė vienos eilutės, kuri patenkintų griežtą nelygybę";
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
                    double bottom = this[0, i - 1] * tempCoeff.C + this[1, i - 1];
                    result.C = -this[2, i - 1] / bottom;
                    result.D = (this[i - 1] - this[0, i - 1] * tempCoeff.D) / bottom;
                }
                for (int j = _coefficientC.Count; j < i - 1; j++)
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
                    result[0, currentLine] = double.Parse(columns[0]);
                    result[1, currentLine] = double.Parse(columns[1]);
                    result[2, currentLine] = double.Parse(columns[2]);
                }
                else
                {
                    if (currentLine > 0)
                    {
                        result[0, currentLine] = double.Parse(columns[currentLine - 1]);
                    }
                    else
                    {
                        result[0, currentLine] = 0;
                    }
                    result[1, currentLine] = double.Parse(columns[currentLine]);
                    result[2, currentLine] = double.Parse(columns[currentLine + 1]);
                }
                currentLine++;
            }
            return result;
        }

        public void LoadResults(string data)
        {
            string[] lines = data.Split(new char[] { '\n' });
            _resultMatrix.Clear();
            for (int i = 0; i < lines.Length; i++)
            {
                _resultMatrix.Add(double.Parse(lines[i]));
            }
        }

        public List<double> Solve()
        {
            List<double> tempResult = new List<double>();
            int start = RowCount - 1;
            CalculateLevel(RowCount);
            tempResult.Add(_coefficientD[RowCount - 1]);
            for (int i = start; i >= 1; i--)
            {
                double current = tempResult[tempResult.Count - 1] * _coefficientC[i - 1] + _coefficientD[i - 1];
                tempResult.Add(current);
            }
            tempResult.Reverse();
            return tempResult;
        }
    }
}
