using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class File : ResourceElement
    {
        public string fileName;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }


        public void setFileName(String p_strFileName)
        {
            if ((!p_strFileName.EndsWith(".asm", true, null)) && !(p_strFileName.IndexOf("conRAW") >= 0))
            {
                fileName = p_strFileName + ".asm";
            }
            else
                fileName = p_strFileName;
        }

        public String getFileName()
        {
            return fileName;
        }

        public override bool HasName
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            return (String.IsNullOrEmpty(FileName)) ? "Any File resource element" : String.Format("File {0}", FileName);
        }
    }
}
