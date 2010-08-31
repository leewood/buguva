using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class SWAP : Process
    {
        #region Variables

        private ElementList returnArraySwapBlock = new ElementList();
        private ElementList returnArrayHardDisk = new ElementList();
        private ElementList returnArrayRAM = new ElementList();
        private ElementList returnArrayReadStatus = new ElementList();
        private ElementList returnArray;

        private List<int> freeSwap = new List<int>();

        /*
         * 0 - nurodyta ir swap ir ram adresai
         * 1 - nurodyta tik ram adresas
         * 2 - nurodyta tik swap adresas ir yra laisvo ram
         * 3 - nurodyta tik swap adresas ir nera laisvo ram
         */
        private int situation;

        #endregion

        public SWAP()
        {
            step.Add(stepBlockOnSwapBlock);
            step.Add(stepBlockOnHardDisk);
            step.Add(stepBlockOnRAM);
            step.Add(stepExchangeData);
            step.Add(stepBlockOnReadStatus);
            step.Add(stepReturnRAMToSender);
            step.Add(stepReleaseHardDisk);
            step.Add(stepDestroySwapBlock);
            step.Add(stepCreateSwapOK);

            for (int i = 2; i < 65536; i++)
            {
                freeSwap.Add(i);
            }
        }

        #region STEPS

        private int stepBlockOnSwapBlock()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("SWAPBlock", query, returnArraySwapBlock);

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
            if (((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress > -1 &&
                ((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress > -1)
            {
                #region Nurodyta ir RAM ir RAM adresai
                element.blockAddress = ((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress;
                situation = 0;
                #endregion
            }
            else
            {
                if (((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress > -1 &&
                ((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress < 0)
                {
                    #region Nurodytas tik RAM adresas
                    situation = 1;
                    #endregion
                }
                else
                {
                    #region Nurodytas tik SWAP adresas
                    if (kernel.getResourcePointer("RAMResource").elementList.Count > 0)
                    {
                        #region Yra laisvo RAM
                        element.blockAddress = -1;
                        situation = 2;
                        #endregion
                    }
                    else
                    {
                        #region Nera laisvo RAM
                        element.blockAddress = -2;
                        situation = 3;
                        #endregion
                    }
                    #endregion
                }
            }
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArrayRAM);

            return 3;
        }

        private int stepExchangeData()
        {
            if (freeSwap.Count == 0)
            {
                situation = 4;
            }


            switch (situation)
            {    
                case 0:
                    #region Nurodyta ir RAM ir SWAP adresai
                    Register2Bytes p_reg2bytesC = new Register2Bytes();
                    Register3Bytes p_reg3bytesDA = new Register3Bytes();
                    Register3Bytes p_reg3bytesSA = new Register3Bytes();            
                    p_reg2bytesC = new Register2Bytes();
                    p_reg2bytesC[0] = (byte)(0);
                    p_reg2bytesC[1] = (byte)(0);

                    p_reg3bytesDA = new Register3Bytes();
                    p_reg3bytesDA[0] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress / 65536);
                    p_reg3bytesDA[1] = (byte)((((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress % 65536) / 256);
                    p_reg3bytesDA[2] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress % 256);

                    p_reg3bytesSA = new Register3Bytes();
                    p_reg3bytesSA[0] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress / 65536);
                    p_reg3bytesSA[1] = (byte)((((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 65536) / 256);
                    p_reg3bytesSA[2] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 256);

                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)1);
                    }
                    else
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)0);
                    }

                    freeSwap.Add(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress);
                    
                    break;
                    #endregion

                case 1:
                    #region Nurodytas tik RAM adresas
                    int freeSwapAdress = freeSwap[0];
                    freeSwap.RemoveAt(0);

                    p_reg2bytesC = new Register2Bytes();
                    p_reg2bytesC[0] = (byte)(0);
                    p_reg2bytesC[1] = (byte)(0);

                    p_reg3bytesDA = new Register3Bytes();
                    p_reg3bytesDA[0] = (byte)(freeSwapAdress / 65536);
                    p_reg3bytesDA[1] = (byte)((freeSwapAdress % 65536) / 256);
                    p_reg3bytesDA[2] = (byte)(freeSwapAdress % 256);

                    p_reg3bytesSA = new Register3Bytes();
                    p_reg3bytesSA[0] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress / 65536);
                    p_reg3bytesSA[1] = (byte)((((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress % 65536) / 256);
                    p_reg3bytesSA[2] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).memoryAddress % 256);

                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)1, (byte)2);
                    }
                    else
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)0, (byte)2);
                    }
                    
                    break;
                    #endregion

                case 2:
                    #region Nurodytas tik SWAP adresas ir yra laisvo RAM
                    p_reg2bytesC = new Register2Bytes();
                    p_reg2bytesC[0] = (byte)(0);
                    p_reg2bytesC[1] = (byte)(0);

                    p_reg3bytesDA = new Register3Bytes();
                    p_reg3bytesDA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                    p_reg3bytesDA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                    p_reg3bytesDA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                    p_reg3bytesSA = new Register3Bytes();
                    p_reg3bytesSA[0] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress / 65536);
                    p_reg3bytesSA[1] = (byte)((((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 65536) / 256);
                    p_reg3bytesSA[2] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 256);

                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)1);
                    }
                    else
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)0);
                    }

                    if (((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress > 1)
                    {
                        freeSwap.Add(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress);
                    }

                    break;
                    #endregion

                case 3:
                    #region Nurodytas tik SWAP adresas ir nera laisvo RAM

                    #region Is RAM i laikina SWAP
                    p_reg2bytesC = new Register2Bytes();
                    p_reg2bytesC[0] = (byte)(0);
                    p_reg2bytesC[1] = (byte)(0);

                    p_reg3bytesDA = new Register3Bytes();
                    p_reg3bytesDA[0] = (byte)0;
                    p_reg3bytesDA[1] = (byte)0;
                    p_reg3bytesDA[2] = (byte)0;

                    p_reg3bytesSA = new Register3Bytes();
                    p_reg3bytesSA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                    p_reg3bytesSA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                    p_reg3bytesSA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)1, (byte)2);
                    }
                    else
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)0, (byte)2);
                    }
                    #endregion

                    #region Is SWAP i RAM
                    p_reg2bytesC[0] = (byte)(0);
                    p_reg2bytesC[1] = (byte)(0);

                    p_reg3bytesDA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
                    p_reg3bytesDA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
                    p_reg3bytesDA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

                    p_reg3bytesSA[0] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress / 65536);
                    p_reg3bytesSA[1] = (byte)((((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 65536) / 256);
                    p_reg3bytesSA[2] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 256);

                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)1);
                    }
                    else
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)0);
                    }
                    #endregion

                    #region Is laikino SWAP i SWAP
                    p_reg2bytesC[0] = (byte)(0);
                    p_reg2bytesC[1] = (byte)(0);

                    p_reg3bytesSA[0] = (byte)0;
                    p_reg3bytesSA[1] = (byte)0;
                    p_reg3bytesSA[2] = (byte)0;

                    if (((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress == 1)
                    {
                        freeSwapAdress = freeSwap[0];
                        freeSwap.RemoveAt(0);

                        p_reg3bytesDA[0] = (byte)(freeSwapAdress / 65536);
                        p_reg3bytesDA[1] = (byte)((freeSwapAdress % 65536) / 256);
                        p_reg3bytesDA[2] = (byte)(freeSwapAdress % 256);
                    }
                    else
                    {
                        p_reg3bytesDA[0] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress / 65536);
                        p_reg3bytesDA[1] = (byte)((((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 65536) / 256);
                        p_reg3bytesDA[2] = (byte)(((SWAPBlockElement)returnArraySwapBlock[0]).hddBlockAddress % 256);
                    }

                    if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)2);
                    }
                    else
                    {
                        kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)2, (byte)2);
                    }
                    #endregion

                    break;
                    #endregion

                case 4:
                    #region Nera laisvo SWAP
                    ElementList elem = new ElementList(null);
                    ResourceElement shutdown = new ResourceElement();

                    kernel.createResource("Shutdown", this);
                    shutdown.sender = this;
                    elem.add(shutdown);
                    kernel.freeResouce("Shutdown", elem);
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
            ((RAMResourceElement)returnArrayRAM[0]).receiver = ((SWAPBlockElement)returnArraySwapBlock[0]).sender;
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

        private int stepDestroySwapBlock()
        {
            
            return 8;
        }

        private int stepCreateSwapOK()
        {
            ElementList elem = new ElementList(null);
            ResourceElement readok = new ResourceElement();

            kernel.createResource("SwapOK", this);
            readok.sender = this;
            elem.add(readok);
            kernel.freeResouce("SwapOK", elem);

            return 0;
        }

        #endregion
    }
}
