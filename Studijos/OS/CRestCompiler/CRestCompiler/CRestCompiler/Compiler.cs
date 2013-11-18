using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CRestCompiler
{
    public class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }


	/*Block description*/
    struct BlockDesc
    {
        public const int MAX_VARS = 10;
        public int lastCount;
        public int lastBlock;
        public int lastPlace;
    }

	/* Class for holding variables*/
    class Variables
    {
        public const int TYPE_INT = 0;
        public const int TYPE_STRING = 1;
        List<string> names = new List<string>();
        List<int> addresses = new List<int>();
        List<int> types = new List<int>();        
        List<BlockDesc> blocks = new List<BlockDesc>();
        int count = 0;
        int block = 0;
        int place = 0;
        int returnCount = 0;

        public Variables(int block)
        {
            this.block = block;
        }
        public void add(string name, int type)
        {
            add(name, type, place + block * 256);
        }

        public void newVarBlock(int blockAddr, int startPlace)
        {
            BlockDesc desc = new BlockDesc();
            desc.lastBlock = block;
            desc.lastCount = count;
            desc.lastPlace = place;
            block = blockAddr;
            place = startPlace;
            blocks.Add(desc);
            returnCount++;
        }

        public void removeVarBlock()
        {
            block = blocks[returnCount -  1].lastBlock;
            place = blocks[returnCount -  1].lastPlace;
            count = blocks[returnCount - 1].lastCount;
            blocks.Remove(blocks[returnCount - 1]);
            returnCount--;
        }

        public void add(string name, int type, int address)
        {
            if (count > names.Count - 1)
            {
                names.Add(name);
                addresses.Add(address);
                types.Add(type);
            }
            else
            {
                names[count] = name;
                addresses[count] = address;
                this.types[count] = type;
            }

            count++;
            place++;
            if (place > 255)
            {
                place = 0;
                block++;
            }
        }

        public int getAddress(string name)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (names[i] == name)
                {
                    return addresses[i];
                }
            }
            return -1;
        }
        public int getType(string name)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (names[i] == name)
                {
                    return types[i];
                }
            }
            return 0;
            
        }
    }


    class CodeArea
    {
        string[] code = new string[65536];
        public string this[int block, int word]
        {
            get
            {
                if ((block > 255) || (block < 0))
                {
                    block = 0;
                }
                if ((word > 255) || (word < 0))
                {
                    word = 0;
                }
                return code[block * 256 + word];
            }
            set
            {
                if ((block > 255) || (block < 0))
                {
                    block = 0;
                }
                if ((word > 255) || (word < 0))
                {
                    word = 0;
                }
                code[block * 256 + word] = value;
            }
        }
        private char toChar(int i)
        {
            switch(i)
            {
                case 1: return '1'; 
                case 2: return '2'; 
                case 3: return '3'; 
                case 4: return '4'; 
                case 5: return '5'; 
                case 6: return '6'; 
                case 7: return '7'; 
                case 8: return '8'; 
                case 9: return '9'; 
                case 10: return 'A'; 
                case 11: return 'B'; 
                case 12: return 'C'; 
                case 13: return 'D'; 
                case 14: return 'E'; 
                case 15: return 'F'; 
                default: return '0'; 
                
            }
            
        }


        public string addressToString(int block, int word)
        {
            string s = "";
            s += toChar(block / 16);
            s += toChar(block % 16);
            s += toChar(word / 16);
            s += toChar(word % 16);
            return s;
        }
    }


    public delegate void ProcessStep();

    public class Step
    {
        public ProcessStep run;
        public Step nextStep;
        public Step()
        {
            nextStep = null;
            run = emptyStep;
        }

        public Step(ProcessStep method, Step nextStep)
        {
            this.run = method;
            this.nextStep = nextStep;
        }

        public void emptyStep()
        {
        }
    }

    public class StepsArray
    {
        List<Step> steps = new List<Step>();
        public void emptyStep()
        {
        }

        public Step this[int i]
        {
            get
            {
                if (i < steps.Count)
                {
                    return steps[i];

                }
                else
                {
                    Step tempStep = new Step();
                    tempStep.nextStep = null;
                    tempStep.run = emptyStep;
                    return tempStep;
                }
            }
            set
            {
                
                if (i < steps.Count)
                {
                    steps[i] = value;
                }
                else if (i == steps.Count)
                {
                    steps.Add(value);
                }
            }
        }
    }

    public class Reader
    {
        
        public ProcessStep currentStep;
        System.Windows.Forms.RichTextBox inputer;
        System.Windows.Forms.RichTextBox output;
        System.Windows.Forms.RichTextBox errors;
        int curLine = 0;
        int errCount = 0;

        public Reader(System.Windows.Forms.RichTextBox inputer, System.Windows.Forms.RichTextBox output, System.Windows.Forms.RichTextBox errors)
        {
            this.inputer = inputer;
            this.output = output;
            this.errors = errors;
            this.output.Text = "";
        }

        public void test()
        {
            currentStep = new ProcessStep(test2);
            currentStep();
        }

        public void test2()
        {
        }

        public string nextLine()
        {
            if (curLine + 1 > inputer.Lines.Length)
            {
                error("No code lines found");
                return "";
            }
            else
            {

                curLine++;
                return inputer.Lines[curLine - 1];
            }
        }

        public void writeLn(string line)
        {
            this.output.AppendText(line + "\n");
        }


        public void error(string line)
        {
            error(line, true);
        }

        public void error(string line, bool useError)
        {
            string output = line;
            if (useError)
            {
                errCount++;
                output = "Error " + errCount.ToString() + ": Line " + curLine.ToString() + " - " + line;
            }
            this.errors.AppendText(output + "\n");
        }
    }

    class Compiler
    {
        int curCodeLine;
        int curVarBlock;
        int curAsmBlock;
        int curAsmWord;
        int tempAddress;
        Variables vars;
        Variables pointers;
        Variables functions;
        CodeArea code;
        string nextSent = "";
        bool reload = false;
        Reader input;

        public Compiler(Reader rdr)
        {
            curCodeLine = 0;
            input = rdr;
            curVarBlock = 100;
            curAsmBlock = 150;
            curAsmWord = 0;
            tempAddress = 0x8F00;
            vars = new Variables(curVarBlock);
            pointers = new Variables(curVarBlock);
            functions = new Variables(curVarBlock);
            code = new CodeArea();
            //Win32.AllocConsole();
            input.error("", false);
            input.error("---------------------------", false);
            input.error(DateTime.Now.ToLongTimeString() + ": Compilation started", false);
            analize(nextWord());
            code[curCodeLine / 256, curCodeLine % 256] = "HALT    ";
            curCodeLine++;
            input.error("", false);
            input.error(DateTime.Now.ToLongTimeString() + ": Compilation ended", false);
        }



        public int cleverFind(int start, char chr, string str)
        {
            int i = 0;
            bool mode = true;
            for (i = start; i < str.Length; i++)
            {
                if (str[i] == '\'')
                {
                    mode = !mode;
                }
                else if ((str[i] == chr) && (mode))
                {
                    return i;
                }
            }
            return -1;
        }

        public string nextWord()
        {
            string result = "";
            if (!reload)
            {
                result = input.nextLine();
                nextSent = result;
            }
            else
            {
                reload = false;
                result = nextSent;
            }
            return result;
        }

        public bool isFunctionDef(string s)
        {
            if (cleverFind(0, '(', s) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void functionDef(string s)
        {
            int i2 = cleverFind(0, '(', s);
            int j = cleverFind(0, ')', s);
            if (j < 0)
            {
                input.error("Wrong function definition");
                j = s.Length;
            }
            string parameters = s.Substring(i2 + 1, j - i2 - 1);
            int i = nextNonSpace(0, s);
            j = nextSpace(i, s);
            string ftype = s.Substring(i, j - i);
            i = nextNonSpace(j, s);
            string name = s.Substring(i, i2 - i);
            if (name.IndexOf(" ") > -1)
            {
                i = name.IndexOf(" ");
                name = name.Substring(0, i);
            }
            int type = (ftype == "int") ? Variables.TYPE_INT : Variables.TYPE_STRING;

            functions.add(name, type, curCodeLine + 1);
            i = 0;
            vars.newVarBlock((curCodeLine + 2) / 256, (curCodeLine + 2) % 256);
            int varCount = 0;
            while (i < parameters.Length)
            {
                j = parameters.IndexOf(',', i);
                if (j < 0)
                {
                    j = parameters.Length;
                }
                string exp = parameters.Substring(i, j - i);
                newVariable(exp.Substring(nextNonSpace(0, exp)));
                varCount++;
                i = j + 1;
            }
            code[(curCodeLine + 1) / 256, (curCodeLine + 1) % 256] = "JUMP" + code.addressToString((curCodeLine + varCount + 1) / 256, (curCodeLine + varCount + 1) % 256);
            int lastCodeLine = curCodeLine;
            curCodeLine += varCount + 2;
            for (i = varCount - 1; i >= 0; i--)
            {
                code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((lastCodeLine + 2 + i) / 256, (lastCodeLine + 2 + i) % 256);
                curCodeLine++;
            }
            analize(nextWord());
            code[lastCodeLine / 256, lastCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine - 1) / 256, (curCodeLine - 1) % 256);
            vars.removeVarBlock();
        }

        public void outputAsm()
        {
            string line;
            line = nextWord();
            while (line.IndexOf("asm_end") < 0)
            {
                string label = "";
                if (line[0] == ':')
                {
                    int end = line.IndexOf(':', 1);
                    label = line.Substring(1, end - 1);
                    line = line.Substring(end + 1);
                    pointers.add(label, Variables.TYPE_STRING, curAsmBlock * 256 + curAsmWord);
                }
                code[curAsmBlock, curAsmWord] = line;
                curAsmWord++;
                if (curAsmWord > 255)
                {
                    curAsmWord = 0;
                    curAsmBlock++;
                }
                line = nextWord();
            }
        }

        private char toChar(int i)
        {
            switch (i)
            {
                case 1: return '1'; 
                case 2: return '2'; 
                case 3: return '3'; 
                case 4: return '4'; 
                case 5: return '5'; 
                case 6: return '6'; 
                case 7: return '7'; 
                case 8: return '8'; 
                case 9: return '9'; 
                case 10: return 'A'; 
                case 11: return 'B'; 
                case 12: return 'C'; 
                case 13: return 'D'; 
                case 14: return 'E'; 
                case 15: return 'F'; 
                default: return '0'; 

            }
            
        }

        public string removeSpace(string curLine)
        {
            string s;
            int i = 0;
            int n = curLine.Length - 1;
            while (curLine[i] == ' ')
            {
                i++;
            }
            while (curLine[n] == ' ')
            {
                n--;
            }
            s = curLine.Substring(i, n - i + 1);
            return s;
        }

        public int nextNonSpace(int start, string s)
        {
            int i = start;
            while ((i < s.Length) && (s[i] == ' '))
            {
                i++;
            }
            return i;
        }

        public int nextSpace(int start, string s)
        {
            int i = start;
            while (s[i] != ' ')
            {
                i++;
            }
            return i;
        }
        
        public int firstNonLetter(int start, string s)
        {
            int i = start;

            while ((i < s.Length) && (char.IsLetterOrDigit(s[i])))
            {
                i++;
            }
            return i++;
        }

        public UInt32 IntToUint(int value)
        {
            UInt32 value2 = (UInt32)value;
            if (value < 0)
            {
                value2 = (UInt32.MaxValue + (UInt32)value + 1);
            }
            else
            {

            }
            return value2;
        }


        public bool isDecimal(string line)        
        {
            bool isTrue = true;
            for (int i = 0; i < line.Length; i++)
            {
                if (!((char.IsDigit(line[i])) || (line[i] == '-') || (line[i] == ' ')))
                {
                    isTrue = false;
                }
            }
            return isTrue;
        }


        public string cleverSpaceRemover(string input)
        {
            string result = "";
            bool mode = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\'')
                {
                    mode = !mode;
                }
                if ((input[i] != ' ') || ((input[i] == ' ') && (mode)))
                {
                    result += input[i];
                }
            }
            int j = result.Length - 1;
            while ((result[j] == ' ') || (result[j] == ';'))
            {
               j--;
            }
            if (j + 1 < result.Length)
            {
                result = result.Remove(j + 1);
            }
            return result;
        }

        public bool isHexDecimal(string line)
        {
            bool isTrue = true;
            int start = nextNonSpace(0, line);
            isTrue = ((start + 1 < line.Length) && ((line[start] == '0') && (line[start + 1] == 'x')));
            for (int i = start + 2; i < line.Length; i++)
            {
                isTrue = (isTrue && ((char.IsDigit(line[i])) || (line[i] == 'A') || (line[i] == 'B') || (line[i] == 'C') || (line[i] == 'D') || (line[i] == 'E') || (line[i] == 'F')));
            }
            return isTrue;
        }

        public bool isText(string line)
        {
            return ((line.IndexOf('\'') >= 0) && (line.IndexOf('\'', line.IndexOf('\'') + 1) > 0));
        }

        public bool isConst(string line)
        {
            return (isText(line) || isDecimal(line) || isHexDecimal(line));
        }

        public string convConst(string line)
        {
            string result = "";
            bool normal = false;
            if (line[0] == '\'')
            {
                result = line.Substring(1, line.IndexOf('\'', 1) - 1);
                while (result.Length < 8)
                {
                    result += ' ';
                }
            }
            else if (line.Length >= 2)
            {
                if ((line[0] == '0') && (line[1] == 'x'))
                {
                    result = "";

                    for (int i = line.Length - 8; i < line.Length; i++)
                    {
                        if (i < 2)
                        {
                            result += '0';
                        }
                        else
                        {
                            result += line[i];
                        }

                    }
                }
                else
                {
                    normal = true;
                }
            }
            else
            {
                normal = true;
            }

            if (normal)
            {
                int end = line.IndexOf(';');
                if (end < 0)
                {
                    end = line.Length;
                }
                int st = 0;
                if (line[0] == '-')
                {
                    st++;
                }
                int i = int.Parse(line.Substring(st, end - st));

                if (line[0] == '-')
                {
                    i = -i;
                }

                result = "";
                string result2 = "";
                UInt32 i2 = IntToUint(i);
                int j = 7;
                while (j >= 0)
                {
                    result2 += toChar((int)(i2 % 16));
                    i2 /= 16;
                    j--;
                }
                for (j = result2.Length - 1; j >= 0; j--)
                {
                    result += result2[j];
                }
            }
            return result;
        }

        string lastWord = "";
       

        public int nextCBaseWord(string line, int start)
        {
            int i = start;
            i = nextNonSpace(i, line);
            int j = i;
            if (char.IsLetter(line[j]))
            {
                while ((j < line.Length) && (char.IsLetterOrDigit(line[j])))
                {
                    j++;
                }
            }
            else if (line[j] == '\'')
            {
                j++;
                while (line[j] != '\'')
                {
                    j++;
                }
            }
            else
            {
                j++;
            }
            lastWord = line.Substring(i, j - i);
            return j;
        }


        public int findConstEnd(int start, string data)
        {
            int i = start;
            while ((i < data.Length) && ((char.IsDigit(data[i])) || (data[i] == 'A')|| (data[i] == 'B')|| (data[i] == 'C')|| (data[i] == 'D')|| (data[i] == 'E')|| (data[i] == 'F') || (data[i] == 'x')))
            {
                i++;
            }
            return i;
        }

        public int findEnd(int start, string data, char opener, char closer)
        {
            int opened = 1;
            int i = start + 1;
            bool mode = true;
            while (opened > 0)
            {
                if ((data[i] == opener) && (mode))
                {
                    opened++;
                }
                if ((data[i] == closer) && (mode))
                {
                    opened--;
                }
                if (data[i] == '\'')
                {
                    mode = !mode;
                }
                i++;
            }
            return i;
        }


        public void sysCall(string name)
        {
            string s = "";
            switch (name)
            {
                case "fileOpen":
                    code[curCodeLine / 256, curCodeLine % 256] = "CALL0000";
                    break;
                case "read":
                    code[curCodeLine / 256, curCodeLine % 256] = "CALL0001";
                    break;
                case "write":
                    code[curCodeLine / 256, curCodeLine % 256] = "CALL0002";
                    break;
                case "getParam":
                    code[curCodeLine / 256, curCodeLine % 256] = "CALL0003";
                    break;
                case "printf":
                    code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256);
                    curCodeLine++;
                    s = code.addressToString(tempAddress / 256, tempAddress % 256);
                    s = s.Substring(0, 2) + "01";
                    code[curCodeLine / 256, curCodeLine % 256] = "OUT " + s;
                    break;
                case "scanf":
                    s = code.addressToString(tempAddress / 256, tempAddress % 256);
                    s = s.Substring(0, 2) + "01";
                    code[curCodeLine / 256, curCodeLine % 256] = "IN  " + s;
                    curCodeLine++;
                    code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString(tempAddress / 256, tempAddress % 256);
                    break;
                case "start":
                    code[curCodeLine / 256, curCodeLine % 256] = "CALL0004";
                    break;
                default:
                    code[curCodeLine / 256, curCodeLine % 256] = "CALL0005";
                    input.error("Unknown system or function call");
                    break;

            }
        }

        public void functionCall(string fname, string arguments)
        {
            int lastCodeLine = curCodeLine;
            code[curCodeLine / 256, curCodeLine % 256] = "SRET    ";
            curCodeLine++;
            int i = 0;
            while (i < arguments.Length)
            {
                int j = arguments.IndexOf(',', i);
                if (j < 0)
                {
                    j = arguments.Length;
                }
                string exp = arguments.Substring(i, j - i);
                evalExpression(exp);
                i = j + 1;
            }
            int addr = functions.getAddress(fname) - 1;
            if (addr >= -1)
            {
                code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString(addr / 256, addr % 256);
            }
            else
            {
                sysCall(fname);
            }
            code[lastCodeLine / 256, lastCodeLine % 256] = "PUSH" + code.addressToString(curCodeLine / 256, curCodeLine % 256);
            curCodeLine++;
        }

        public void assigmentSentence(string line)
        {
            int i = nextNonSpace(0, line);
            if (line[i] == '[')
            {
                int j = findEnd(i, line, '[', ']');
                string exp = line.Substring(i + 1, j - i - 2);
                j = nextNonSpace(j, line);
                string exp2 = "";
                if (line[j] == '[')
                {
                    int k = findEnd(j, line, '[', ']');
                    exp2 = line.Substring(j + 1, k - j - 2);
                    j = k;
                }
                int z = line.IndexOf('=', j);
                string expVal = line.Substring(z + 1);
                evalExpression(expVal);
                if (exp2 != "")
                {
                    evalExpression(exp);
                    code[curCodeLine / 256, curCodeLine % 256] = "PUTS    ";
                    curCodeLine++;
                    evalExpression(exp2);
                    code[curCodeLine / 256, curCodeLine % 256] = "CHGB    ";
                    curCodeLine++;
                }
                evalExpression(exp);
                code[curCodeLine / 256, curCodeLine % 256] = "POPS    ";
                curCodeLine++;

            }
            else
            {
                int j = nextCBaseWord(line, i);
                string name = lastWord;
                j = nextNonSpace(j, line);
                string exp = "";
                if (line[j] == '[')
                {
                    int k = findEnd(j, line, '[', ']');
                    exp = line.Substring(j + 1, k - j - 2);
                    j = k;

                }
                else if (line[j] == '(')
                {
                    int k = findEnd(j, line, '(', ')');
                    if (k < 0)
                    {
                        input.error("No end for (");
                        k = line.Length;
                    }
                        string exp2 = line.Substring(j + 1, k - j - 2);
                        functionCall(name, exp2);
                        j = k;
                 
                }
                int z = line.IndexOf('=', j);
                if (z > -1)
                {
                    string expVal = line.Substring(z + 1);
                    int addr = vars.getAddress(name);
                    if (addr < 0)
                    {
                        input.error("Symbol not found");
                    }
                    evalExpression(expVal);
                    if (exp != "")
                    {
                        code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString(addr / 256, addr % 256);
                        curCodeLine++;
                        evalExpression(exp);
                        code[curCodeLine / 256, curCodeLine % 256] = "CHGB    ";
                        curCodeLine++;
                    }
                    code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(addr / 256, addr % 256);
                    curCodeLine++;
                }
            }
        }

        public bool isOperationSign(char c)
        {
            return ((c == '=') || (c == '>') || (c == '<') || (c == '@'));
        }

        public void genOperationAsm(string oper)
        {
            switch (oper)
            {
                case "+":
                  code[curCodeLine / 256, curCodeLine % 256] = "ADDE    ";
                  curCodeLine++;
                  break;
                case "-":
                  code[curCodeLine / 256, curCodeLine % 256] = "SUBE    ";
                  curCodeLine++;
                  break;
                case "*":
                  code[curCodeLine / 256, curCodeLine % 256] = "MULE    ";
                  curCodeLine++;
                  break;
                case "&":
                  code[curCodeLine / 256, curCodeLine % 256] = "ANDE    ";
                  curCodeLine++;
                  break;
                case "|":
                  code[curCodeLine / 256, curCodeLine % 256] = "ORE     ";
                  curCodeLine++;
                  break;
                case "^":
                  code[curCodeLine / 256, curCodeLine % 256] = "XORE    ";
                  curCodeLine++;
                  break;
                case "/":
                  code[curCodeLine / 256, curCodeLine % 256] = "DIVE    ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POPM0001";
                  curCodeLine++;

                  break;
                case "%":
                  code[curCodeLine / 256, curCodeLine % 256] = "DIVE    ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POPM0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POPM0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM0001";

                  curCodeLine++;

                  break;
                case "=@":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMPS    ";
                  curCodeLine++;
                  break;
                case "+@":
                  code[curCodeLine / 256, curCodeLine % 256] = "ADDS    ";
                  curCodeLine++;
                  break;
                case "==":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256); ;
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JE  " + code.addressToString((curCodeLine + 5) / 256, (curCodeLine + 5) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 4) / 256, (curCodeLine + 4) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;

                  break;
                case "!=":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256); ;
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JE  " + code.addressToString((curCodeLine + 5) / 256, (curCodeLine + 5) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 4) / 256, (curCodeLine + 4) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;

                  break;
                case ">":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256); ;
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JE  " + code.addressToString((curCodeLine + 5) / 256, (curCodeLine + 5) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 4) / 256, (curCodeLine + 4) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;

                  break;
                case "<":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256); ;
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSHFFFF";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JE  " + code.addressToString((curCodeLine + 5) / 256, (curCodeLine + 5) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 4) / 256, (curCodeLine + 4) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;

                  break;
                case ">=":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256); ;
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSHFFFF";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JE  " + code.addressToString((curCodeLine + 5) / 256, (curCodeLine + 5) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 4) / 256, (curCodeLine + 4) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;

                  break;
                case "<=":
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString(tempAddress / 256, tempAddress % 256); ;
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress + 1) / 256, (tempAddress + 1) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUTM" + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "CMP     ";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JE  " + code.addressToString((curCodeLine + 5) / 256, (curCodeLine + 5) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;

                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0001";
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 4) / 256, (curCodeLine + 4) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "POP " + code.addressToString((tempAddress) / 256, (tempAddress) % 256);
                  curCodeLine++;
                  code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                  curCodeLine++;

                  break;
                  
                default:
                  input.error("Wrong operation");
                  break;
            }
        }

        public string textConstSpecialChars(string rawString)
        {
            string result = "";
            for (int i = 0; i < rawString.Length; i++)
            {
                if (rawString[i] == '\\')
                {
                    i++;
                    switch (rawString[i])
                    {
                        case 'n':
                            result += '\n';
                            break;
                        case '0':
                            result += '\0';
                            break;
                        case '\\':
                            result += '\\';
                            break;
                        case '1':
                            result += '\'';
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    result += rawString[i];
                }
            }
            return result;
        }

        public void evalExpression(string exp)
        {
            string oper = cleverSpaceRemover(exp);
            if (oper[0] == '(')
            {
                int i = findEnd(0, oper, '(', ')');
                string arg1 = oper.Substring(1, i - 2);
                evalExpression(arg1);
                if (i < oper.Length)
                {
                    string operation = ""+oper[i];
                    i++;
                    if (isOperationSign(oper[i]))
                    {
                        operation += oper[i];
                        i++;
                    }
                    evalExpression(oper.Substring(i));
                    genOperationAsm(operation);
                }
            }
            else if (oper[0] == '[')
            {
                int i = findEnd(0, oper, '[', ']');
                string arg1 = oper.Substring(1, i - 2);
                evalExpression(arg1);
                code[curCodeLine / 256, curCodeLine % 256] = "PUTS    ";
                curCodeLine++;
                if (i < oper.Length)
                {

                    if (oper[i] == '[')
                    {
                        int i2 = findEnd(i, oper, '[', ']');
                        string arg2 = oper.Substring(i, i2 - i - 1);
                        i = i2;
                        evalExpression(arg2);
                        code[curCodeLine / 256, curCodeLine % 256] = "EXTR    ";
                        curCodeLine++;
                    }
                    if (i < oper.Length)
                    {
                        string operation = ""+oper[i];
                        i++;
                        if (isOperationSign(oper[i]))
                        {
                            operation += oper[i];
                            i++;
                        }

                        evalExpression(oper.Substring(i));
                        genOperationAsm(operation);
                    }
                }
            }
            else if ((oper[0] == '\'') || (char.IsDigit(oper[0])))
            {
                string arg1 = "";
                int i = 0;
                if (oper[0] == '\'')
                {
                    i = oper.IndexOf('\'', 1) + 1;
                    arg1 = oper.Substring(0, i);
                    arg1 = textConstSpecialChars(arg1);
                    i++;

                } else 
                {

                    i = findConstEnd(0, oper);
                    arg1 = oper.Substring(0, i);
                }

                string conv = convConst(arg1);
                string conv2 = conv.Substring(0, conv.Length - 4);
                string left = conv2.Substring(0, conv2.Length - 4);
                conv2 = conv2.Substring(conv2.Length - 4);
                conv = conv.Substring(conv.Length - 4);
                if (left.Length > 0)
                {
                    code[curCodeLine / 256, curCodeLine % 256] = "PUSH0000";
                    curCodeLine++;
                    code[curCodeLine / 256, curCodeLine % 256] = "PUSL0000";
                    curCodeLine++;
                }

                while (left.Length > 0)
                {
                    if (left.Length % 8 != 0)
                    {
                        while (left.Length % 8 != 0)
                        {
                            left = " " + left;
                        }

                    }
                    string leftHight = left.Substring(4, 4);
                    string leftLow = left.Substring(0, 4);
                    left = left.Substring(8);
                    code[curCodeLine / 256, curCodeLine % 256] = "PUSH" + leftHight;
                    curCodeLine++;
                    code[curCodeLine / 256, curCodeLine % 256] = "PUSL" + leftLow;
                    curCodeLine++;

                }
                code[curCodeLine / 256, curCodeLine % 256] = "PUSH" + conv;
                curCodeLine++;
                code[curCodeLine / 256, curCodeLine % 256] = "PUSL" + conv2;
                curCodeLine++;
                while (left.Length > 0)
                {

                }
                if (i < oper.Length)
                {
                    string operation = ""+oper[i];
                    i++;
                    if (isOperationSign(oper[i]))
                    {
                        operation += oper[i];
                        i++;
                    }

                    evalExpression(oper.Substring(i));
                    genOperationAsm(operation);
                }

            } else 
            {
                bool readMem = true;
                int i = nextCBaseWord(oper, 0);
                int type = 0;
                string arg1 = lastWord;
                if (((i < oper.Length) && (oper[i] != '(')) || (i >= oper.Length))
                {
                    int addr = vars.getAddress(arg1);
                    type = vars.getType(arg1);
                    if (addr < 0)
                    {
                        addr = pointers.getAddress(arg1);
                        readMem = false;
                    }
                    if (addr < 0)
                    {
                        input.error("Symbol not found");
                    }
                    if (readMem)
                    {
                        string command = "PUTM" + code.addressToString(addr / 256, addr % 256);
                        code[curCodeLine / 256, curCodeLine % 256] = command;
                        curCodeLine++;
                        if (i < oper.Length)
                        {

                            if (oper[i] == '[')
                            {
                                int i2 = findEnd(i, oper, '[', ']');
                                string arg2 = oper.Substring(i + 1, i2 - i - 2);
                                i = i2;
                                evalExpression(arg2);
                                code[curCodeLine / 256, curCodeLine % 256] = "EXTR    ";
                                curCodeLine++;
                            }
                            if (i < oper.Length)
                            {
                                string operation = "" + oper[i];
                                i++;
                                if (isOperationSign(oper[i]))
                                {
                                    operation += oper[i];
                                    i++;
                                }

                                evalExpression(oper.Substring(i));
                                genOperationAsm(operation);
                            }
                        }
                    }
                    else
                    {
                        string command = "PUSH" + code.addressToString(addr / 256, addr % 256);
                        code[curCodeLine / 256, curCodeLine % 256] = command;
                        curCodeLine++;
                        if (i < oper.Length)
                        {
                            string operation = "" + oper[i];
                            i++;
                            if (isOperationSign(oper[i]))
                            {
                                operation += oper[i];
                                i++;
                            }
                            evalExpression(oper.Substring(i));
                            genOperationAsm(operation);
                        }


                    }
                }
                else
                {
                    int j = findEnd(i, oper, '(', ')');
                    string args = oper.Substring(i + 1, j - i - 2);
                    functionCall(arg1, args);
                    i = j;
                    if (i < oper.Length)
                    {
                        string operation = "" + oper[i];
                        i++;
                        if (isOperationSign(oper[i]))
                        {
                            operation += oper[i];
                            i++;
                        }

                        evalExpression(oper.Substring(i));
                        genOperationAsm(operation);
                    }

                }
            }

        }

        public void ifSentence(string line)
        {
            int i = nextNonSpace(0, line);
            i = nextNonSpace(i + 2, line);
            int j = findEnd(i, line, '(', ')');
            string exp = line.Substring(i + 1, j - i - 2);
            evalExpression(exp);
            int lastLine = curCodeLine;
            code[curCodeLine / 256, curCodeLine % 256] = "JE  ";
            curCodeLine++;
            analize(nextWord());
            nextWord();
            if (nextSent.IndexOf("else") >= 0)
            {
                code[lastLine / 256, lastLine % 256] += code.addressToString(curCodeLine / 256, curCodeLine % 256);
                lastLine = curCodeLine;
                code[curCodeLine / 256, curCodeLine % 256] = "JUMP";
                curCodeLine++;
                analize(nextWord());
            }
            else
            {
                reload = true;
            }
            code[lastLine / 256, lastLine % 256] += code.addressToString((curCodeLine - 1) / 256, (curCodeLine - 1) % 256);
        }

        public void blockSentence(string line)
        {
            int i = nextNonSpace(0, line);
            if (line[i] == '{')
            {
                code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((curCodeLine + 100) / 256, (curCodeLine + BlockDesc.MAX_VARS) % 256);
                curCodeLine++;
                vars.newVarBlock(curCodeLine / 256, curCodeLine % 256);
                curCodeLine += BlockDesc.MAX_VARS;
                string s = nextWord();
                int i2 = nextNonSpace(0, s);
                while ((i2 < s.Length) && (s[i2] != '}'))
                {
                    analize(s);
                    s = nextWord();
                    i2 = nextNonSpace(i2, s);
                }
                vars.removeVarBlock();
            }
        }

        public void whileSentence(string line)
        {
            int i = line.IndexOf("while");
            i += 5;
            i = nextNonSpace(i, line);
            int j = findEnd(i, line, '(', ')');
            string exp = line.Substring(i + 1, j - i - 2);
            int lastLine = curCodeLine;
            evalExpression(exp);
            code[curCodeLine / 256, curCodeLine % 256] = "JE  ";
            int lastLine2 = curCodeLine;
            curCodeLine++;
            analize(nextWord());
            code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((lastLine - 1)/ 256, (lastLine - 1) % 256);
            code[lastLine2 / 256, lastLine2 % 256] += code.addressToString(curCodeLine / 256, curCodeLine % 256);
            curCodeLine++;
        }

        public void forSentence(string line)
        {
            int i = line.IndexOf("for");
            i += 3;
            i = nextNonSpace(i, line);
            int j1 = line.IndexOf(';', i);
            int j2 = line.IndexOf(';', j1 + 1);
            int j3 = line.IndexOf(')', j2);
            string exp1 = line.Substring(i + 1, j1 - i - 1);
            string exp2 = line.Substring(j1 + 1, j2 - j1 - 1);
            string exp3 = line.Substring(j2 + 1, j3 - j2 - 1);
            evalExpression(exp1);
            int lastLine = curCodeLine;
            evalExpression(exp2);
            code[curCodeLine / 256, curCodeLine % 256] = "JE  ";
            int lastLine2 = curCodeLine;
            curCodeLine++;
            analize(nextWord());
            evalExpression(exp3);
            code[curCodeLine / 256, curCodeLine % 256] = "JUMP" + code.addressToString((lastLine - 1) / 256, (lastLine - 1) % 256);
            code[lastLine2 / 256, lastLine2 % 256] += curCodeLine;
            curCodeLine++;

        }


        public void varOrFunctionDefinition(string s)
        {
            if (isFunctionDef(s))
            {
                functionDef(s);
            }
            else
            {
                newVariable(s.Substring(nextNonSpace(0, s)));
            }
        }

        public void returnSentence(string line)
        {
            int i = line.IndexOf("return");
            string exp = line.Substring(i + 6);
            evalExpression(exp);
            code[curCodeLine / 256, curCodeLine % 256] = "RET     ";
            curCodeLine++;
        }

        public void analize(string line)
        {
            int pos = 0;
            while (pos < line.Length)
            {
                pos = nextCBaseWord(line, pos);
                switch (lastWord)
                {
                    case "int":
                        varOrFunctionDefinition(line);
                        break;
                    case "return":
                        returnSentence(line);
                        break;
                    case "string":
                        varOrFunctionDefinition(line);
                        break;
                    case "if":
                        ifSentence(line);
                        break;
                    case "while":
                        whileSentence(line);
                        break;
                    case "for":
                        forSentence(line);
                        break;
                    case "[":
                        assigmentSentence(line);
                        break;
                    case "{":
                        blockSentence(line);
                        break;
                    case "asm_code":
                        outputAsm();
                        break;
                    default:
                        assigmentSentence(line);
                        break;
                }
                pos = line.Length;
            }
        }

        public void newVariable(string curLine)
        {
            int type = 0;
            if (curLine.Length > 0)
            {
                if (curLine[0] == 'i')
                {
                    type = Variables.TYPE_INT;
                }
                else
                {
                    type = Variables.TYPE_STRING;
                }
                int start = nextNonSpace(nextSpace(0, curLine), curLine);
                int end = firstNonLetter(start, curLine);
                string name = curLine.Substring(start, end - start);
                vars.add(name, type);
                string value = "00000000";
                if ((end + 1 < curLine.Length) && (curLine.IndexOf('=', end + 1) >= 0))
                {
                    int st = nextNonSpace(curLine.IndexOf('=', end + 1) + 1, curLine);
                    int en;
                    for (en = curLine.Length - 1; ((curLine[en] == ' ') || (curLine[en] == ';')); en--)
                    {
                    }
                    string tempVal = curLine.Substring(st, en - st + 1);
                    if (isConst(tempVal))
                    {
                        value = convConst(tempVal);
                    }

                }
                int addr = vars.getAddress(name);
                code[addr / 256, addr % 256] = value;
            }
        }


        private bool hasData(int block)
        {
            for (int i = 0; i < 256; i++)
            {
                if (code[block, i] != null)
                {
                    return true;
                }
            }
            return false;
        }

        private bool isMore(int block, int word)
        {
            for (int i = word; i < 256; i++)
            {
                if (code[block, i] != null)
                {
                    return true;
                }
            }
            return false;
        }

        private string addrConv(int block)
        {
            string result = block.ToString("x");
            if (result.Length < 2)
            {
                result = "0" + result;
            }
            return result;
        }

        public void outputResult()
        {
            int curBlock = 0;
            while (curBlock < 256)
            {
                int curWord = 0;
                if (hasData(curBlock))
                {
                    input.writeLn("$" + addrConv(curBlock));
                    while (curWord < 256)
                    {
                        if (code[curBlock, curWord] == null)
                        {
                            if (isMore(curBlock, curWord))
                            {
                                input.writeLn("");
                            }
                            else
                            {
                                curWord = 255;
                            }
                        }
                        else
                        {
                            input.writeLn(code[curBlock, curWord]);
                        }
                        curWord++;
                    }
                }
                curBlock++;
            }
            input.writeLn("$END");
        }
    }
}
