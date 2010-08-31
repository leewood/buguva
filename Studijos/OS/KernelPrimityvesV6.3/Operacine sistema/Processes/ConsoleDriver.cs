using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ConsoleDriver : Process
    {
        #region Variables

        private ElementList returnArrayConsoleData = new ElementList();
        private ElementList returnArrayInputDevice = new ElementList();
        private ElementList returnArrayOutputDevice = new ElementList();
        private ElementList returnArrayRAM = new ElementList();
        private ElementList returnArrayReadStatus = new ElementList();

        private Process ConsoleDataOwner = null;

        #endregion

        public ConsoleDriver()
        {
            step.Add(stepBlockOnConsoleData);
            step.Add(stepBlockOnRAM);
            step.Add(stepWhatToDoNext);
            step.Add(stepBlockOnInputDevice);
            step.Add(stepDataExchangeRead);
            step.Add(stepReleaseInputDevice);
            step.Add(stepFormatChangeRead);
            step.Add(stepFormatChangeWrite);
            step.Add(stepBlockOnOutputDevice);
            step.Add(stepDataExchangeWrite);
            step.Add(stepReleaseOutputDevice);
            step.Add(stepBlockOnReadStatus);
            step.Add(stepReturnRAMToSender);
            step.Add(stepCreateReadOK);
        }

        #region STEPS

        private int stepBlockOnConsoleData()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("ConsoleData", query, returnArrayConsoleData);

            return 1;
        }

        private int stepBlockOnRAM()
        {

            ElementList asking = new ElementList();
            returnArrayRAM = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = ((ConsoleDataElement)returnArrayConsoleData[0]).memoryAddress;
            element.useSupervisorMemory = ((ConsoleDataElement)returnArrayConsoleData[0]).useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArrayRAM);

            return 2;
        }

        private int stepWhatToDoNext()
        {
            if (((ConsoleDataElement)returnArrayConsoleData[0]).mode == 1)
            {
                return 3;
            }
            else
            {
                return 7;
            }
        }

        #region Read

        private int stepBlockOnInputDevice()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("InputDevice", query, returnArrayInputDevice);

            return 4;
        }

        private int stepDataExchangeRead()
        {
            Register2Bytes p_reg2bytesC = new Register2Bytes();
            p_reg2bytesC[0] = (byte)(0);
            p_reg2bytesC[1] = (byte)(0);

            Register3Bytes p_reg3bytesDA = new Register3Bytes();
            p_reg3bytesDA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
            p_reg3bytesDA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
            p_reg3bytesDA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

            Register3Bytes p_reg3bytesSA = new Register3Bytes();
            p_reg3bytesSA[0] = (byte)0;
            p_reg3bytesSA[1] = (byte)0;
            p_reg3bytesSA[2] = (byte)0;

            if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
            {
                kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)3, (byte)1, returnArrayConsoleData[0].sender);
            }
            else
            {
                kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)3, (byte)0, returnArrayConsoleData[0].sender);
            }

            return 11;
        }

        private int stepReleaseInputDevice()
        {
            ElementList elem = new ElementList(null);
            elem.add((returnArrayInputDevice[0]));
            kernel.freeResouce("InputDevice", elem);

            return 6;
        }

        private int stepFormatChangeRead()
        {
            if (((ConsoleDataElement)returnArrayConsoleData[0]).type == 2)
            {
                Word tempWord = new Word();
                string tempStr;
                char[] space = { ' ' };

                for (int i = 0; i < 256; i++)
                {
                    tempStr = ((RAMResourceElement)returnArrayRAM[0]).block[i].toString();
                    tempStr.TrimEnd(space);
                    tempStr.TrimStart(space);
                    tempWord = int.Parse(tempStr);
                    ((RAMResourceElement)returnArrayRAM[0]).block[i] = tempWord;
                }
            }

            return 12;
        }

        #endregion

        #region Write

        private int stepFormatChangeWrite()
        {
            if (((ConsoleDataElement)returnArrayConsoleData[0]).type == ConsoleDataElement.TYPE_INT)
            {
                Word tempWord = new Word();
                string tempStr;
                char[] space = { ' ' };

                for (int i = 0; i < 256; i++)
                {
                    tempStr = ((RAMResourceElement)returnArrayRAM[0]).block[i].toString();
                    tempStr.TrimEnd(space);
                    tempStr.TrimStart(space);
                    tempWord = int.Parse(tempStr);
                    ((RAMResourceElement)returnArrayRAM[0]).block[i] = tempWord;
                }
            }

            return 8;
        }

        private int stepBlockOnOutputDevice()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("OutputDevice", query, returnArrayInputDevice);

            return 9;
        }

        private int stepDataExchangeWrite()
        {
            Register2Bytes p_reg2bytesC = new Register2Bytes();
            p_reg2bytesC[0] = (byte)(0);
            p_reg2bytesC[1] = (byte)(0);

            Register3Bytes p_reg3bytesSA = new Register3Bytes();
            p_reg3bytesSA[0] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress / 65536);
            p_reg3bytesSA[1] = (byte)((((RAMResourceElement)returnArrayRAM[0]).blockAddress % 65536) / 256);
            p_reg3bytesSA[2] = (byte)(((RAMResourceElement)returnArrayRAM[0]).blockAddress % 256);

            Register3Bytes p_reg3bytesDA = new Register3Bytes();
            p_reg3bytesDA[0] = (byte)0;
            p_reg3bytesDA[1] = (byte)0;
            p_reg3bytesDA[2] = (byte)0;

            if (((RAMResourceElement)returnArrayRAM[0]).useSupervisorMemory)
            {
                kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)1, (byte)3, returnArrayConsoleData[0].sender);
            }
            else
            {
                kernel.processor.ioi_devices.XCHG(p_reg2bytesC, p_reg3bytesSA, p_reg3bytesDA, (byte)0, (byte)3, returnArrayConsoleData[0].sender);
            }

            return 11;
        }

        private int stepReleaseOutputDevice()
        {
            ElementList elem = new ElementList(null);
            elem.add((returnArrayInputDevice[0]));
            kernel.freeResouce("OutputDevice", elem);

            return 12;
        }

        #endregion

        private int stepBlockOnReadStatus()
        {
            ElementList query = new ElementList();
            readStatus queryElem = new readStatus();
            queryElem.receiver = this;
            queryElem.sender = null;
            queryElem.onChannel = (((ConsoleDataElement)returnArrayConsoleData[0]).mode == ConsoleDataElement.MODE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE;

            query[0] = queryElem;
            kernel.askForResource("readStatus", query, returnArrayReadStatus);

            if (((ConsoleDataElement)returnArrayConsoleData[0]).mode == 1)
            {
                return 5;
            }
            else
            {
                return 10;
            };
        }

        private int stepReturnRAMToSender()
        {
            ElementList elem = new ElementList(null);
            ((RAMResourceElement)returnArrayRAM[0]).receiver = ((ConsoleDataElement)returnArrayConsoleData[0]).sender;
            ((RAMResourceElement)returnArrayRAM[0]).sender = this;
            elem.add(((RAMResourceElement)returnArrayRAM[0]));
            kernel.freeResouce("RAMResource", elem);

            return 13;
        }

        private int stepCreateReadOK()
        {
            ElementList elem = new ElementList(null);
            readOKElement readok = new readOKElement();

            kernel.createResource("readOK", this);
            readok.sender = this;
            readok.receiver = returnArrayConsoleData[0].sender;
            readok.onChannel = (((ConsoleDataElement)returnArrayConsoleData[0]).mode == 1) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE;
            
            elem.add(readok);
            kernel.freeResouce("readOK", elem);

            return 0;
        }

        #endregion
    }
}
