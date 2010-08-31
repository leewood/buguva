using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class HDDDriver : Process
    {
        #region Variables

        private ElementList returnArrayHddBlock = new ElementList();
        private ElementList returnArrayHardDisk = new ElementList();
        private ElementList returnArrayRAM = new ElementList();
        private ElementList returnArrayReadStatus = new ElementList();
        private ElementList returnArray = new ElementList();

        #endregion

        public HDDDriver()
        {
            step.Add(stepBlockOnHDDBlock);
            step.Add(stepBlockOnHardDisk);
            step.Add(stepBlockOnRAM);
            step.Add(stepExchangeData);
            step.Add(stepBlockOnReadStatus);
            step.Add(stepReturnRAMToSender);
            step.Add(stepReleaseHardDisk);
            step.Add(stepDestroyHDDBlock);
            step.Add(stepCreateReadOK);
        }

        #region STEPS

        private int stepBlockOnHDDBlock()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("HDDBlock", query, returnArrayHddBlock);

            return 1;
        }

        private int stepBlockOnHardDisk()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("HardDisk", query, returnArrayHardDisk);

            return 2;
        }

        private int stepBlockOnRAM()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = ((HDDBlockElement)returnArrayHddBlock[0]).memoryAddress;
            element.block = null;
            element.useSupervisorMemory = ((HDDBlockElement)returnArrayHddBlock[0]).useSupervisorMemory;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArrayRAM);

            return 3;
        }

        private int stepExchangeData()
        {
            switch (((HDDBlockElement)returnArrayHddBlock[0]).mode)
            {
                #region Write
                case 1:
                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        #region From supervisory memory
                        Register2Bytes p_reg2bytesC = new Register2Bytes();
                        p_reg2bytesC[0] = (byte)(0);
                        p_reg2bytesC[1] = (byte)(0);

                        Register3Bytes p_reg3bytesDA = new Register3Bytes();
                        p_reg3bytesDA[0] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress / 65536);
                        p_reg3bytesDA[1] = (byte)((((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 65536) / 256);
                        p_reg3bytesDA[2] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 256);

                        Register3Bytes p_reg3bytesSA = new Register3Bytes();
                        p_reg3bytesSA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                        p_reg3bytesSA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                        p_reg3bytesSA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)1, (byte)2);
                        #endregion
                    }
                    else
                    {
                        #region From user memory
                        Register2Bytes p_reg2bytesC = new Register2Bytes();
                        p_reg2bytesC[0] = (byte)(0);
                        p_reg2bytesC[1] = (byte)(0);

                        Register3Bytes p_reg3bytesDA = new Register3Bytes();
                        p_reg3bytesDA[0] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress / 65536);
                        p_reg3bytesDA[1] = (byte)((((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 65536) / 256);
                        p_reg3bytesDA[2] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 256);

                        Register3Bytes p_reg3bytesSA = new Register3Bytes();
                        p_reg3bytesSA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                        p_reg3bytesSA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                        p_reg3bytesSA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)0, (byte)2);
                        #endregion
                    }
                    break;
                #endregion

                #region Read
                case 2:
                    if (((HDDBlockElement)returnArrayHddBlock[0]).useSupervisorMemory)
                    {
                        #region To supervisory memory
                        Register2Bytes p_reg2bytesC = new Register2Bytes();
                        p_reg2bytesC[0] = (byte)(0);
                        p_reg2bytesC[1] = (byte)(0);

                        Register3Bytes p_reg3bytesSA = new Register3Bytes();
                        p_reg3bytesSA[0] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress / 65536);
                        p_reg3bytesSA[1] = (byte)((((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 65536) / 256);
                        p_reg3bytesSA[2] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 256);

                        Register3Bytes p_reg3bytesDA = new Register3Bytes();
                        p_reg3bytesDA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                        p_reg3bytesDA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                        p_reg3bytesDA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)1);
                        #endregion
                    }
                    else
                    {
                        #region To user memory
                        Register2Bytes p_reg2bytesC = new Register2Bytes();
                        p_reg2bytesC[0] = (byte)(0);
                        p_reg2bytesC[1] = (byte)(0);

                        Register3Bytes p_reg3bytesSA = new Register3Bytes();
                        p_reg3bytesSA[0] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress / 65536);
                        p_reg3bytesSA[1] = (byte)((((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 65536) / 256);
                        p_reg3bytesSA[2] = (byte)(((HDDBlockElement)returnArrayHddBlock[0]).hddBlockAddress % 256);

                        Register3Bytes p_reg3bytesDA = new Register3Bytes();
                        p_reg3bytesDA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                        p_reg3bytesDA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                        p_reg3bytesDA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)0);
                        #endregion
                    }
                    break;
                #endregion
            }

            return 4;
        }

        private int stepBlockOnReadStatus()
        {
            ElementList query = new ElementList();
            readStatus queryElem = new readStatus();            
            queryElem.receiver = this;
            queryElem.sender = null;
            queryElem.onChannel = readStatus.CHANNEL_HARDDISK;
            query[0] = queryElem;
            kernel.askForResource("readStatus", query, returnArrayReadStatus);

            return 5;
        }

        private int stepReturnRAMToSender()
        {
            ElementList elem = new ElementList(null);

            ((RAMResourceElement)returnArrayRAM[0]).receiver = ((RAMResourceElement)returnArrayRAM[0]).sender;
            ((RAMResourceElement)returnArrayRAM[0]).sender = this;
            elem.add(((RAMResourceElement)returnArrayRAM[0]));
            kernel.freeResouce("RAMResource", elem);

            return 6;
        }

        private int stepReleaseHardDisk()
        {
            ElementList elem = new ElementList(null);
            (returnArrayHardDisk[0]).receiver = null;
            (returnArrayHardDisk[0]).sender = this;
            elem.add((returnArrayHardDisk[0]));
            kernel.freeResouce("HardDisk", elem);

            return 7;
        }

        private int stepDestroyHDDBlock()
        {
            return 8;
        }

        private int stepCreateReadOK()
        {
            ElementList elem = new ElementList(null);
            readOKElement readok = new readOKElement();

            kernel.createResource("readOK", this);
            readok.sender = this;
            readok.receiver = returnArrayHddBlock[0].sender;
            readok.onChannel = readStatus.CHANNEL_HARDDISK;
            elem.add(readok);
            kernel.freeResouce("readOK", elem);

            return 0;
        }

        #endregion
    }
}
