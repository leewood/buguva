using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace ConsoleApplication1
{
    public abstract class AbstractOS
    {
        abstract public void setProcessor(Processor proc);
        abstract public void call(int callType, int argument);
        abstract public bool canGoIntoUserMode();
        abstract public SavedRegisters getResumeState();
        abstract public void normalMode();
        abstract public void debugMode();
        abstract public void debugRunNextStep();
    }

    public class Processor
    {
        #region <Variables>

        public const int USER_MODE = 0;
        public const int SUPERVISOR_MODE = 1;
        public const int TIMER_MAX = 200;
        public Register4Bytes IC = new Register4Bytes();
        public Register4Bytes SP = new Register4Bytes();
        public Register4Bytes PTR = new Register4Bytes();
        public Register4Bytes C = new Register4Bytes();
        public int N = TIMER_MAX;
        public bool done = false;
        public bool useThread = false;

        private byte priv_PI;
        private byte priv_SI;
        private byte priv_IOI;
        private byte priv_TI;
        private byte priv_MODE;
        private byte priv_CH1;
        private byte priv_CH2;
        private byte priv_CH3;
        public byte PI
        {
            get
            {
                return priv_PI;
            }
            set
            {
                priv_PI = value;
                done = true;
            }
        }
        public byte SI
        {
            get
            {
                return priv_SI;
            }
            set
            {
                priv_SI = value;
                done = true;
            }
        }

        public byte IOI
        {
            get
            {
                return priv_IOI;
            }
            set
            {
                priv_IOI = value;
                done = true;
            }
        }

        public byte TI
        {
            get
            {
                return priv_TI;
            }
            set
            {
                priv_TI = value;
                done = true;
            }
        }

        public byte MODE
        {
            get
            {
                return priv_MODE;
            }
            set
            {
                priv_MODE = value;
                done = true;
            }
        }

        public byte CH1
        {
            get
            {
                return priv_CH1;
            }
            set
            {
                priv_CH1 = value;
                done = true;
            }
        }

        public byte CH2
        {
            get
            {
                return priv_CH2;
            }
            set
            {
                priv_CH2 = value;
                done = true;
            }
        }

        public byte CH3
        {
            get
            {
                return priv_CH3;
            }
            set
            {
                priv_CH3 = value;
                done = true;
            }
        }

        bool debug = false;
        public ChannelDevice ioi_devices = new ChannelDevice();
        public Memory user_memory = new Memory();
        public Memory supervisor_memory = new Memory();
        public HardDiskDrive hdd;
        public InputDevice input = new InputDevice();
        public OutputDevice output = new OutputDevice();
        private AbstractOS OSpointer;
        private HighLevelProcessor HLP;

        #endregion <Variables>

        #region <Nested Types>

        private class HighLevelProcessor
        {
            Thread t;
            volatile bool nextStep = false;
            Processor proc;
            VirtualMachine vm;
            bool needTerminate = false;

            public HighLevelProcessor(Processor proc)
            {
                this.proc = proc;
                this.vm = new VirtualMachine(proc);
                startHLP();
            }


            public void startHLP()
            {
                if (proc.useThread)
                {
                    t = new Thread(Run);
                    t.Start();
                }
            }

            public void sleep()
            {
                t.Suspend();
            }

            public void resume()
            {
                if (t.ThreadState == ThreadState.Suspended)
                {
                    t.Resume();
                }
            }

            public void terminate()
            {
                needTerminate = true; 
            }

            void interpretVirtualMachine()
            {
                Word operation = new Word();
                
                Register4Bytes argument = vm.IC.clone();
                int intSp = vm.SP;
                int temp1;
                int temp2;
                bool boolOK = true;
                boolOK = (proc.testVirtualAddress(vm.IC.clone()));
                if (boolOK)
                {
                    argument = vm.SP.clone();
                    boolOK = (proc.testVirtualAddress(vm.SP.clone()));
                }
                if (boolOK)
                {
                    operation = vm.memory[vm.IC.clone()];
                    string opName = operation[0, true];
                    argument = operation[4, true];
                    Register4Bytes tmp = proc.SP.clone();
                    tmp--;
                    Register4Bytes tmp2 = proc.SP.clone();
                    tmp2++;
                    Word tm = vm.memory[vm.SP];
                    bool incIC = true;
                    #region <ASM Commands switch>
                    switch (opName)
                    {
                        case "HALT":
                            proc.SI = 3;
                            break;
                        case "ADD ":
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() + vm.memory[tmp].clone();
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }
                            break;
                        case "ADDE":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() + vm.memory[tmp].clone();
                            vm.SP--;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "SUB ":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() - vm.memory[tmp].clone();
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "SUBE":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() - vm.memory[tmp].clone();
                            vm.SP--;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "MUL ":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() * vm.memory[tmp].clone();
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "MULE":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() * vm.memory[tmp].clone();
                            vm.SP--;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "DIV ":
                            vm.memory[tmp2] = vm.memory[vm.SP].clone() / vm.memory[tmp].clone();
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() % vm.memory[tmp].clone();
                            vm.SP++;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "DIVE":

                            tm = vm.memory[vm.SP].clone();
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() / vm.memory[tmp].clone();
                            vm.memory[tmp] = tm % vm.memory[tmp].clone();
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "ADDC":
                            tm = argument.clone();
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() + tm;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "SUBC":
                            tm = argument.clone();
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() - tm;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "MULC":
                            tm = argument.clone();
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() * tm;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "DIVC":
                            tm = argument.clone();
                            vm.memory[tmp] = vm.memory[vm.SP].clone() / tm;
                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() % tm;
                            vm.SP++;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;

                        case "LC  ":
                            vm.C = argument.clone();
                            break;
                        case "REP ":
                            if (vm.C[3] + vm.C[2] + vm.C[0] + vm.C[1] - 48 * 4 != 0)
                            {
                                vm.IC = argument.clone();
                                vm.C--;
                            }
                            break;
                        case "JMP ":
                            vm.IC = argument.clone();
                            break;
                        case "JUMP":
                            vm.IC = argument.clone();
                            break;
                        case "AND ":

                            vm.memory[tmp2] = vm.memory[vm.SP].clone() & vm.memory[tmp].clone();
                            vm.SP++;
                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "ANDE":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() & vm.memory[tmp].clone();

                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "OR  ":

                            vm.memory[tmp2] = vm.memory[vm.SP].clone() | vm.memory[tmp].clone();
                            vm.SP++;

                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "ORE ":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() | vm.memory[tmp].clone();

                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "XOR ":

                            vm.memory[tmp2] = vm.memory[vm.SP].clone() ^ vm.memory[tmp].clone();
                            vm.SP++;

                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;
                        case "XORE":

                            vm.memory[vm.SP] = vm.memory[vm.SP].clone() ^ vm.memory[tmp].clone();

                            if (Utils.overflow)
                            {
                                Utils.overflow = false;
                                proc.PI = 3;
                            }

                            break;

                        case "NOT ":
                            vm.memory[tmp2] = ~vm.memory[vm.SP].clone();
                            vm.SP++;
                            break;
                        case "NOTE ":
                            vm.memory[vm.SP] = ~vm.memory[vm.SP].clone();

                            break;
                        case "CMP ":
                            if (vm.memory[vm.SP] > vm.memory[tmp])
                            {
                                vm.memory[tmp2] = 1;
                            }
                            else if (vm.memory[vm.SP] == vm.memory[tmp])
                            {
                                vm.memory[tmp2] = 0;
                            }
                            else
                            {
                                vm.memory[tmp2] = -1;
                            }
                            vm.SP++;
                            break;
                        case "JE  ":
                            if (vm.memory[vm.SP] == 0)
                            {
                                vm.IC = argument;
                                vm.SP--;
                            }
                            break;
                        case "JG  ":
                            if (vm.memory[vm.SP] == 1)
                            {
                                vm.IC = argument;
                                vm.SP--;
                            }
                            break;
                        case "JL  ":
                            if (vm.memory[vm.SP] == -1)
                            {
                                vm.IC = argument;
                                vm.SP--;
                            }
                            break;
                        case "POP ":
                            if (proc.testVirtualAddress(argument.clone()))
                            {
                                vm.memory[argument] = vm.memory[vm.SP].clone();
                                vm.SP--;

                            }
                            else
                            {
                                incIC = false;
                            }
                            break;
                        case "POPM":
                            if (proc.testVirtualAddress(argument.clone()))
                            {
                                vm.memory[argument] = vm.memory[vm.SP].clone();
                                vm.SP--;
                            }
                            else
                            {
                                incIC = false;
                            }
                            break;

                        case "PUTM":
                            vm.SP++;
                            vm.memory[vm.SP.clone()] = vm.memory[argument.clone()].clone();
                            break;
                        case "PUSH":
                            if (proc.testVirtualAddress(argument))
                            {
                                vm.SP++;
                                vm.memory[vm.SP] = argument;
                            }
                            else
                            {
                                incIC = false;
                            }
                            break;
                        case "IN  ":
                            if (proc.testVirtualAddress(argument))
                            {
                                proc.SI = 1;
                            }
                            else
                            {
                                incIC = false;
                            }
                            break;
                        case "OUT ":
                            if (proc.testVirtualAddress(argument))
                            {
                                proc.SI = 2;
                            }
                            else
                            {
                                incIC = false;
                            }
                            break;
                        case "CALL":
                            proc.SI = 5;
                            vm.SP++;
                            vm.memory[vm.SP.clone()] = argument;
                            break;
                        case "POPS":
                            
                            vm.memory[vm.memory[intSp].getRegPart(4)] = vm.memory[intSp - 1].clone();
                            vm.SP--;
                            vm.SP--;
                            break;
                        case "PUTS":
                            vm.memory[intSp] = vm.memory[vm.memory[intSp].getRegPart(4)].clone();
                            break;
                        case "CHGB":
                            temp1 = vm.memory[intSp];
                            temp2 = vm.memory[intSp - 2].getByteAt(7);
                            vm.memory[intSp - 2] = vm.memory[intSp - 1].clone();
                            vm.memory[intSp - 2].writeAt(temp1, (byte)temp2);
                            vm.SP--;
                            vm.SP--;
                            vm.SP--;
                            break;
                        case "PUSL":
                            vm.memory[intSp].writeAt(0, argument[0]);
                            vm.memory[intSp].writeAt(1, argument[1]);
                            vm.memory[intSp].writeAt(2, argument[2]);
                            vm.memory[intSp].writeAt(3, argument[3]);
                            break;
                        case "CMPS":
                            string s1 = vm.memory[intSp].toString();
                            string s2 = vm.memory[intSp - 1].toString();
                            if (s1 == s2)
                            {
                                vm.memory[intSp - 1] = 1;
                            }
                            else
                            {
                                vm.memory[intSp - 1] = 0;
                            }
                            vm.SP--;
                            break;
                        case "ADDS":
                            s1 = vm.memory[intSp].toString();
                            s2 = vm.memory[intSp - 1].toString();
                            s1 = s2 + s1;
                            vm.memory[intSp - 1] = 0;
                            vm.memory[intSp - 1].writeAt(0, (byte)s1[0]);
                            vm.memory[intSp - 1].writeAt(1, (byte)s1[1]);
                            vm.memory[intSp - 1].writeAt(2, (byte)s1[2]);
                            vm.memory[intSp - 1].writeAt(3, (byte)s1[3]);
                            vm.memory[intSp - 1].writeAt(4, (byte)s1[4]);
                            vm.memory[intSp - 1].writeAt(5, (byte)s1[5]);
                            vm.memory[intSp - 1].writeAt(6, (byte)s1[6]);
                            vm.memory[intSp - 1].writeAt(7, (byte)s1[7]);
                            vm.SP--;
                            break;
                        case "EXTR":
                            temp1 = vm.memory[intSp];
                            temp2 = vm.memory[intSp - 1].getByteAt(temp1);
                            vm.memory[intSp - 1] = 0;
                            vm.memory[intSp - 1].writeAt(7, (byte)temp2);
                            vm.SP--;
                            break;
                        case "RET ":
                            vm.IC = vm.memory[intSp].getRegPart(4).clone();
                            vm.SP--;
                            break;
                        case "SRET":
                            vm.memory[intSp + 1] = vm.IC.clone();
                            vm.SP++;
                            break;
                        default:
                            proc.PI = 2;
                            break;

                    }
                    #endregion
                    if (incIC)
                    {
                        vm.IC++;
                    }
                }
                proc.N--;
                if ((proc.SI == 1) || (proc.SI == 2) || (proc.SI == 4) || (proc.SI == 5))
                {
                    proc.N -= 3;
                }
                if (proc.N <= 0)
                {
                    proc.TI = 1;
                    proc.N = Processor.TIMER_MAX;
                }
                if (proc.Test())
                {
                    proc.OSpointer.call(proc.getInterruptType(), argument);
                }

            }

            void openStep()
            {
                if ((proc.debug) && (nextStep) && (proc.MODE == Processor.USER_MODE))
                {
                    nextStep = false;
                    proc.done = true;
                    interpretVirtualMachine();
                }
                else if ((!proc.debug) && (proc.MODE == Processor.USER_MODE))
                {
                    interpretVirtualMachine();
                }
                else if (proc.MODE == Processor.SUPERVISOR_MODE)
                {
                    if (proc.OSpointer != null)
                        if (proc.OSpointer.canGoIntoUserMode())
                        {
                            SavedRegisters regs = proc.OSpointer.getResumeState();
                            proc.MODE = Processor.USER_MODE;
                            //proc.IOI = regs.IOI;
                            //proc.TI = regs.TI;
                            //proc.PI = regs.PI;
                            //proc.SI = regs.SI;
                            //proc.CH1 = regs.CH1;
                            //proc.CH2 = regs.CH2;
                            //proc.CH3 = regs.CH3;
                            proc.C = regs.C.clone();
                            proc.SP = regs.SP.clone();
                            proc.IC = regs.IC.clone();
                            proc.PTR = regs.PTR.clone();
                        }
                        else
                        {
                            proc.OSpointer.call(proc.getInterruptType(), 0);
                        }
                }
            }

            void Run()
            {
                while (!needTerminate)
                {
                    openStep();
                }
            }

            public void next()
            {
                nextStep = true;
                if (!proc.useThread)
                {
                    openStep();
                }
            }
        }

        #endregion

        #region <Constructors>

        public Processor(AbstractOS OS, string FileName)
        {
            clearRegisters();
            OSpointer = OS;
            
            OSpointer.setProcessor(this);
            user_memory = new Memory();
            supervisor_memory = new Memory();
            input = new InputDevice();
            output = new OutputDevice();
            hdd = new HardDiskDrive(FileName);
            ioi_devices = new ChannelDevice();
            ioi_devices.setDevices(hdd, user_memory, supervisor_memory, this, output, input);
            HLP = new HighLevelProcessor(this);

        }

        public Processor(string FileName)
        {
            clearRegisters();


            debug = true;
            user_memory = new Memory();
            supervisor_memory = new Memory();
            input = new InputDevice();
            output = new OutputDevice();
            hdd = new HardDiskDrive(FileName);
            ioi_devices = new ChannelDevice();
            ioi_devices.setDevices(hdd, user_memory, supervisor_memory, this, output, input);
            HLP = new HighLevelProcessor(this);

        }

        #endregion

        #region <Methods>

        public void clearRegisters()
        {
            IOI = 0;
            SI = 0;
            PI = 0;
            TI = 0;
            MODE = SUPERVISOR_MODE;
            Register4Bytes wrd = new Register4Bytes(0);
            PTR = wrd.clone();
            SP = wrd.clone();
            N = TIMER_MAX;
            C = wrd.clone();
            IC = wrd.clone();

        }

        public void terminateHLP()
        {
            if (useThread)
            {
                HLP.terminate();
            }
        }

        public void suspendHLP()
        {
            if (useThread)
            {

                HLP.sleep();
            }
        }

        public void resumeHLP()
        {
            if (useThread)
            {

                HLP.resume();
            }
        }

        public void startHLP()
        {
            HLP.startHLP();
        }

        public void userMode(Register4Bytes IC, Register4Bytes SP, Register4Bytes PTR, Register4Bytes C)
        {
            this.IC = IC.clone();
            this.SP = SP.clone();
            this.PTR = PTR.clone();
            this.C = C.clone();
            MODE = USER_MODE;

        }

        public void setOS(AbstractOS OS)
        {
            this.OSpointer = OS;
        }

        bool Test()
        {
            if (((PI == 0) && (SI == 0)) && ((TI == 0) && (IOI == 0)))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public int getInterruptType()
        {
            int Result = 0;
            if (PI > 0)
            {
                Result = PI; PI = 0;
            }
            else if (SI > 0)
            {
                Result = SI + 3; SI = 0;
            }
            else if (TI > 0)
            {
                Result = 9; TI = 0;
            }
            else if (IOI > 0)
            {
                Result = IOI + 9; IOI = 0;
            }
            else
            {
                Result = 0;
            }
            return Result;
        }

        public void normalMode()
        {
            debug = false;
            OSpointer.normalMode();
        }
        public void debugMode()
        {
            debug = true;
            OSpointer.debugMode();
        }


        public void nextHLP()
        {
            HLP.next();
        }

        public void nextStep()
        {
      
            if (this.MODE == Processor.USER_MODE)
            {
                HLP.next();
            }
            else
            {
                OSpointer.debugRunNextStep();
            }
        }

        public void terminate()
        {
            clearRegisters();
        }


        public bool testVirtualAddress(Register4Bytes virtualAddress)
        {
            virtualToAbsoluteAddress(virtualAddress.clone());
            bool result = !((SI == 4) || (PI == 2));
            return result;
        }

        public Word virtualToAbsoluteAddress(Register4Bytes virtualAddress)
        {
            return virtualToAbsoluteAddress(virtualAddress, this.PTR, true);
        }

        public Word virtualToAbsoluteAddress(Register4Bytes virtualAddress, Register4Bytes ptr, bool createInterrupt)
        {
	     Word result = new Word();
		 Word temp = new Word();
         int mem_block = ptr;
		 int mem_offset = virtualAddress[0, true];
		 temp = supervisor_memory[mem_block, mem_offset].clone();
         result[2, true] = temp[1, true].clone();
         result[true, 6] = virtualAddress[2, true].clone();
		 if (temp[0] == '0')
		 {

		 } 
		 else 
		 {
             if (createInterrupt)
             {
                 SI = 4;
             }
		 }
         return result;
        }

        #endregion
    }

        

}