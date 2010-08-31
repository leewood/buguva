using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Operacine_sistema.RealMachine;
using Operacine_sistema.Utils;

namespace ConsoleApplication1
{

    public class ChannelDevice
    {
        #region <Variables>

        private Register2Bytes C = new Register2Bytes();
        private Register3Bytes SA = new Register3Bytes();
        private Register3Bytes DA = new Register3Bytes();
        private Process caller = new Process();
        private byte SD = 0;
        private byte DD = 0;

        private bool CH1 = false;
        private bool CH2 = false;
        private bool CH3 = false;

        HardDiskDrive hardDisk;
        Memory userMemory;
        Memory supervisorMemory;
        Processor mainProcessor;
        OutputDevice outputDevice;
        InputDevice inputDevice;
        ConsoleDevice console;

        #endregion <Variables>

        #region <Public Methods>

        public void setDevices(HardDiskDrive p_hardDiskMain,
            Memory p_memoryUser,
            Memory p_memorySupervisor,
            Processor p_precessorMain,
            OutputDevice p_outputDeviceMain,
            InputDevice p_intputDeviceMain)
        {
            hardDisk = p_hardDiskMain;
            userMemory = p_memoryUser;
            supervisorMemory = p_memorySupervisor;
            mainProcessor = p_precessorMain;
            outputDevice = p_outputDeviceMain;
            inputDevice = p_intputDeviceMain;
            console = new ConsoleDevice(inputDevice, outputDevice);
        }

        public void XCHG(Register2Bytes p_reg2bytesC,
            Register3Bytes p_reg3bytesSA,
            Register3Bytes p_reg3bytesDA,
            byte p_byteSD,
            byte p_byteDD)
        {
            XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, p_byteSD, p_byteDD, null);
        }

        public void XCHG(Register2Bytes p_reg2bytesC,
            Register3Bytes p_reg3bytesSA,
            Register3Bytes p_reg3bytesDA,
            byte p_byteSD,
            byte p_byteDD,
            Process caller)
        {
            C = p_reg2bytesC.clone();
            SA = p_reg3bytesSA.clone();
            DA = p_reg3bytesDA.clone();
            SD = p_byteSD;
            DD = p_byteDD;

                switch (SD)
                {
                    case 0:
                        #region SD0
                        {
                            switch (DD)
                            {
                                case 1:
                                    {
                                        Thread t = new Thread(xchgFromUserRamToSupervisorRam);
                                        t.Start();
                                        break;
                                    }
                                case 2:
                                    {
                                        Thread t = new Thread(xchgFromUserRamToHard);
                                        t.Start();
                                        break;
                                    }
                                case 3:
                                    {

                                        Thread t = new Thread(xchgFromUserRamToOutput);
                                        t.Start();
                                        break;
                                    }
                                default:
                                    mainProcessor.IOI = 2;
                                    break;
                            }
                            break;


                        }
                        #endregion
                    case 1:
                        #region SD1
                        {
                            switch (DD)
                            {
                                case 0:
                                    {
                                        Thread t = new Thread(xchgFromSupervisorRamToUserRam);
                                        t.Start();

                                        break;
                                    }
                                case 2:
                                    {


                                        Thread t = new Thread(xchgFromUserRamToHard);
                                        t.Start();

                                        break;
                                    }
                                case 3:
                                    {

                                        Thread t = new Thread(xchgFromUserRamToOutput);
                                        t.Start();
                                        break;
                                    }
                                default:
                                    mainProcessor.IOI = 2;
                                    break;
                            }
                            break;
                        }
                        #endregion
                    case 2:
                        #region SD2
                        {
                           
                                switch (DD)
                                {
                                    case 0:
                                        {
                                            

                                            Thread t = new Thread(xchgFromHardToUserRam);
                                            t.Start();

                                            break;
                                        }
                                    case 1:
                                        {
                                            

                                            Thread t = new Thread(xchgFromHardToSupervisorRam);
                                            t.Start();

                                            break;
                                        }
                                    case 3:
                                        {

                                            Thread t = new Thread(xchgFromHardToOutput);
                                            t.Start();


                                            break;
                                        }
                                    default:
                                        mainProcessor.IOI = 2;
                                        break;
                                }
                            break;
                        }
                        #endregion
                    case 3:
                        #region SD3
                        {
                            
                                switch (DD)
                                {
                                    case 0:
                                        {


                                            Thread t = new Thread(() => { xchgFromInputToUserRam(caller); });
                                            t.Start();

                                            break;
                                        }
                                    case 1:
                                        {


                                            Thread t = new Thread(() => { xchgFromInputToSupervisorRam(caller); });
                                            t.Start();

                                            break;
                                        }
                                    case 2:
                                        {

                                            Thread t = new Thread(() => { xchgFromInputToHard(caller); });
                                            t.Start();


                                            break;
                                        }
                                    default:
                                        mainProcessor.IOI = 2;
                                        break;
                                }
                            break;

                        }
                        #endregion
                    default:
                        mainProcessor.IOI = 2;
                        break;
                }

            mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
            mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
            mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);            
        }

        #endregion <Public Methods>

        #region <UserRam>
        private void xchgFromUserRamToHard()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();

            while (CH3)
            {
            }
            CH3 = true;
            hardDisk.writeBlock(Utils.extractSector(reg3bytesLocalDA),
                Utils.extractBlockAddress(reg3bytesLocalDA),
                getUserMemoryBlock());


            CH3 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }
        }

        private void safeConsoleWriting(int id, Block memBlock, byte count)
        {
            var adapter = console[id];
            if (caller != null)
            {
                lock (caller)
                {
                    var process = this.caller;
                    caller = null;
                }
            }
            adapter.ConsoleReady.WaitOne();
            adapter.WriteBlock(memBlock, count);
            adapter.ConsoleReady.ReleaseMutex();
        }

        private void xchgFromUserRamToOutput()
        {
            Register2Bytes reg2bytesLocalC = C.clone();
            Register3Bytes reg3bytesLocalDA = DA.clone();
            Register3Bytes reg3bytesLocalSA = SA.clone();      
            /*while (CH2)
            {
            }*/

            CH2 = true;
            safeConsoleWriting(reg3bytesLocalSA.Sector, getUserMemoryBlock(), reg2bytesLocalC.ByteValue);

            CH2 = false;            
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }
        }

        private void xchgFromUserRamToSupervisorRam()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();

            supervisorMemory.writeAt(Utils.extractBlockAddress(reg3bytesLocalDA),
                getUserMemoryBlock());
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }

        }
        #endregion <UserRam>

        #region <Hard>
        private void xchgFromHardToUserRam()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            while (CH3)
            {
            }

            CH3 = true;
            userMemory.writeAt(Utils.extractBlockAddress(reg3bytesLocalDA),
                getHardDiskBlock());


            CH3 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }
        }

        private void xchgFromHardToSupervisorRam()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            while (CH3)
            {
            }

            CH3 = true;
            supervisorMemory.writeAt(Utils.extractBlockAddress(reg3bytesLocalDA),
                getHardDiskBlock());

            CH3 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }

        }

        private void xchgFromHardToOutput()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            Register3Bytes reg3bytesLocalSA = SA.clone();
            Register2Bytes reg2bytesLocalC = C.clone();
            while (CH3)
            {
            }

            CH3 = true;
            CH2 = true;
            
            safeConsoleWriting(reg3bytesLocalSA.Sector, getHardDiskBlock(), reg2bytesLocalC.ByteValue);

            CH3 = false;
            CH2 = false;

            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }

        }
        #endregion <Hard>

        #region <SupervisorRam>
        private void xchgFromSupervisorRamToHard()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            while (CH3)
            {
            }

            CH3 = true;
            hardDisk.writeBlock(Utils.extractSector(reg3bytesLocalDA),
                Utils.extractBlockAddress(reg3bytesLocalDA),
                getSupervisorMemoryBlock());

            CH3 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }

        }

        private void xchgFromSupervisorRamToOutput()
        {
            Register2Bytes reg2bytesLocalC = C.clone();
            Register3Bytes reg3bytesLocalDA = DA.clone();
            Register3Bytes reg3bytesLocalSA = SA.clone();
            /*while (CH2)
            {
            }*/

            CH2 = true;
            
            safeConsoleWriting(reg3bytesLocalSA.Sector, getSupervisorMemoryBlock(), reg2bytesLocalC.ByteValue);

            CH2 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }
        }

        private void xchgFromSupervisorRamToUserRam()
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();

            userMemory.writeAt(Utils.extractBlockAddress(reg3bytesLocalDA),
                getSupervisorMemoryBlock());
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }

        }
        #endregion <SupervisorRam>

        #region <Input>
        private void xchgFromInputToHard(Process caller)
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            while (CH3 || CH1)
            {
            }

            CH3 = true;
            CH1 = true;
            hardDisk.writeBlock(Utils.extractSector(reg3bytesLocalDA),
                Utils.extractBlockAddress(reg3bytesLocalDA),
                getInputBlock(caller));

            CH1 = false;
            CH3 = false;
            mainProcessor.IOI = 1;
            mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
            mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
            mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);

        }

        private void xchgFromInputToSupervisorRam(Process caller)
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            while (CH1)
            {
            }

            CH1 = true;
            supervisorMemory.writeAt(Utils.extractBlockAddress(reg3bytesLocalDA), getInputBlock(caller));

            CH1 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }
        }

        private void xchgFromInputToUserRam(Process caller)
        {
            Register3Bytes reg3bytesLocalDA = DA.clone();
            while (CH1)
            {
            }

            CH1 = true;

            userMemory.writeAt(Utils.extractBlockAddress(reg3bytesLocalDA), getInputBlock(caller));

            CH1 = false;
            lock (mainProcessor)
            {
                mainProcessor.IOI = 1;
                mainProcessor.CH1 = (byte)((this.CH1) ? 1 : 0);
                mainProcessor.CH2 = (byte)((this.CH2) ? 1 : 0);
                mainProcessor.CH3 = (byte)((this.CH3) ? 1 : 0);
            }
        }
        #endregion <Input>

        #region <Utils>

        private int wordCopyCount(Register2Bytes p_reg2bytesCregister)
        {
            int intWordCount = p_reg2bytesCregister[0] * 16 + p_reg2bytesCregister[1];

            if ((p_reg2bytesCregister.getByteAt(0) == 0) && (p_reg2bytesCregister.getByteAt(1) == 0))
            {
                intWordCount = 256;
            }
           /* else
            {
                if (intWordCount % 8 != 0)
                {
                    intWordCount = (intWordCount / 8) + 1;
                }
                else
                {
                    intWordCount = (intWordCount / 8);
                }
            }*/

            return intWordCount;
        }

        private Block getUserMemoryBlock()
        {
            return getBlock(0);
        }

        private Block getHardDiskBlock()
        {
            return getBlock(2);
        }

        private Block getSupervisorMemoryBlock()
        {
            return getBlock(1);
        }

        private Block getInputBlock(Process caller)
        {
            Process process = null;
            if (caller != null)
            {
                lock (caller)
                {
                    process = caller;                    
                }
            }
            Register2Bytes reg2bytesLocalC = C.clone();
            Register3Bytes reg3bytesLocalDA = DA.clone();
            var adapter = console[reg3bytesLocalDA.Sector];
            var pointer = ReleasePointer.Create();
            if (process != null)
            {
                process.AddNewSemophore(pointer);
            }
            var block = adapter.ReadBlock((byte)(reg2bytesLocalC[0] * 16 + reg2bytesLocalC[1]), pointer);
            if (process != null)
            {
                process.RemoveSemophore(pointer);
            }
            return block;
        }


        public void TidyThreads()
        {
        }

        private Block getBlock(int p_intMemoryType)
        {
            Register2Bytes reg2bytesLocalC = C.clone();
            Register3Bytes reg3bytesLocalSA = SA.clone();

            int intWordCount = wordCopyCount(reg2bytesLocalC);
            Block blockLocal = new Block();
            Word wordLocal = new Word();

            for (int j = 0; j < intWordCount; j++)
            {
                wordLocal = new Word();
                for (int n = 0; n <= 7; n++)
                {
                    if (p_intMemoryType == 0)
                    {
                        wordLocal.writeAt(n, userMemory.getBlockAt(Utils.extractBlockAddress(reg3bytesLocalSA)).getWordAt(j).getByteAt(n));
                    }
                    else
                        if (p_intMemoryType == 1)
                        {
                            wordLocal.writeAt(n, supervisorMemory.getBlockAt(Utils.extractBlockAddress(reg3bytesLocalSA)).getWordAt(j).getByteAt(n));
                        }
                        else
                            if (p_intMemoryType == 2)
                            {
                                wordLocal.writeAt(n, hardDisk.readBlock(Utils.extractSector(reg3bytesLocalSA),
                                    Utils.extractBlockAddress(reg3bytesLocalSA)).getWordAt(j).getByteAt(n));
                            }
                }
                blockLocal.writeAt(j, wordLocal);
            }

            return blockLocal;
        }
        #endregion <Utils>
    }

}
