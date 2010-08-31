using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operacine_sistema.Interface;
using ConsoleApplication1;
using System.Threading;
using Operacine_sistema.Utils;

namespace Operacine_sistema.RealMachine
{
    public enum ConsoleType
    {
        Main,
        Additional
    }

    public class ConsoleAdapter
    {
        InputDevice _input;
        OutputDevice _output;
        ConsoleWindow _window;

        private Mutex _consoleReady = null;
        public Mutex ConsoleReady 
        {
            get
            {
                lock (this)
                {
                    if (_consoleReady == null)
                    {
                        if (ConType == ConsoleType.Main)
                        {
                            _consoleReady = new Mutex();
                            _consoleBuffer = new Mutex();
                        }
                        else
                        {
                            _consoleReady = _window.ReadingBufferMutex;
                            _consoleBuffer = _window.CopyBufferMutex;
                        }
                    }
                    return _consoleReady;
                }                
            }
        }

        private Mutex _consoleBuffer = null;
        public Mutex ConsoleBuffer 
        {
            get
            {
                lock (this)
                {
                    if (_consoleBuffer == null)
                    {
                        if (ConType == ConsoleType.Main)
                        {
                            _consoleReady = new Mutex();
                            _consoleBuffer = new Mutex();
                        }
                        else
                        {
                            _consoleReady = _window.ReadingBufferMutex;
                            _consoleBuffer = _window.CopyBufferMutex;
                        }                        
                    }
                    return _consoleBuffer;
                }
            }
        }
        public ConsoleType ConType { get; private set; }
        public int ID { get; private set; }
        private bool stopInterrupt = false;

        public ConsoleAdapter(InputDevice input, OutputDevice output)
        {
            _input = input;
            _output = output;
            ConType = ConsoleType.Main;
            ID = 0;
        }

        public ConsoleAdapter(ConsoleWindow window, int id)
        {
            _window = window;
            ID = id;
            ConType = ConsoleType.Additional;
        }

        public ConsoleAdapter(int id)
        {
            _window = new ConsoleWindow();
            ID = id;
            ConType = ConsoleType.Additional;
        }

        public void WriteBlock(Block p_blockMain, byte p_byteWordsToWrite)
        {
            if (ConType == ConsoleType.Main)
            {
                if (_output != null)
                {
                    _output.writeBlock(p_blockMain, p_byteWordsToWrite);
                }
            }
            else
            {
                if (_window != null)
                {
                    var data = p_blockMain.ToBytesArray();
                    _window.WriteBytes(data, 0, p_byteWordsToWrite);
                    if (_window.Visible == false)
                    {
                        _window.Show();
                    }
                }
            }
        }

        byte[] lastBuffer = new byte[0];

        public Block ReadBlock(byte p_byteWordReadLimit, ReleasePointer  pointer)
        {
            if (ConType == ConsoleType.Main)
            {                
                ConsoleReady.WaitOne();
                Block result = null;
                if (_input != null)
                {                    
                    result = _input.readBlock(p_byteWordReadLimit, pointer);
                }
                ConsoleReady.ReleaseMutex();
                return result;
            }
            else
            {
                if (_window != null)
                {
                    if (_window.Visible == false)
                    {
                        _window.Show();
                    }
                    var mut = _window.StartReading();
                    if (mut.WaitOne())
                    {
                        var buf = _window.GetCopyBufferData();
                        mut.ReleaseMutex();
                        var bufferList = new List<byte>();
                        if (lastBuffer.Length > 0)
                        {
                            bufferList.AddRange(lastBuffer);
                        }
                        bufferList.AddRange(buf);
                        var buffer = bufferList.ToArray();
                        var result = buffer.ToBlock(0, p_byteWordReadLimit * 8);
                        if (p_byteWordReadLimit * 8 < buffer.Length)
                        {
                            lastBuffer = new byte[buffer.Length - p_byteWordReadLimit * 8];
                            buffer.CopyTo(lastBuffer, p_byteWordReadLimit * 8);
                        }
                        else
                        {
                            lastBuffer = new byte[0];
                        }
                        return result;
                    }
                }
            }
            return new Block();
        }

    }

    public class ConsoleDevice
    {
        private List<ConsoleAdapter> adapters = new List<ConsoleAdapter>();

        public ConsoleDevice(InputDevice input, OutputDevice output)
        {
            adapters = new List<ConsoleAdapter>();
            adapters.Add(new ConsoleAdapter(input, output));
        }

        public ConsoleAdapter this[int id]
        {
            get
            {
                var adapter = (from adap in adapters where adap.ID == id select adap).FirstOrDefault();
                if (adapter == null)
                {
                    adapter = new ConsoleAdapter(id);
                    adapters.Add(adapter);
                }
                return adapter;
            }
        }

        public void WriteBlock(int adapterID, Block p_blockMain, byte p_byteWordsToWrite)
        {
            var adapter = this[adapterID];
            adapter.WriteBlock(p_blockMain, p_byteWordsToWrite);
        }

        public Block ReadBlock(int adapterID, byte p_byteWordReadLimit, ReleasePointer pointer)
        {
            var adapter = this[adapterID];
            return adapter.ReadBlock(p_byteWordReadLimit, pointer);
        }
    }
}
