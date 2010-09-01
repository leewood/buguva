using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

namespace HomeunixMangaClient
{
    public class VolumeInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Downloader
    {
        public Downloader(Form1 form)
        {
            Form = form;
        }

        public string VolumesListUrl { get; set; }
        public VolumeInfo[] VolumesList { get; set; }
        public List<string> VolumeNames { get { return (from item in VolumesList select item.Name).ToList(); } }

        public string VolumeUrl { get; set; }
        public string[] ImageUrls { get; set; }
        public Form1 Form { get; private set; } 


        private string _mangaName = null;
        public string MangaName 
        {
            get
            {
                return _mangaName;
            }
            set
            {
                if (_mangaName != value)
                {
                    VolumesListUrl = null;
                    VolumesList = null;
                    VolumeUrl = null;
                    ImageUrls = null;
                    _mangaDirObject = null;
                    _mangaName = value;
                }
            }
        }

        public string StartVolume { get; set; }
        public int StartVolumeIndex { get; set; }
        public string EndVolume { get; set; }
        public int EndVolumeIndex { get; set; }
        public string CurrentVolume { get; set; }
        public int CurrentVolumeIndex { get; set; }
        public int CurrentPage { get; set; }
        private WebClient client = new WebClient();

        private string _mangaDir;
        public string MangaDir 
        {
            get
            {
                return _mangaDir;
            }
            set
            {
                if (_mangaDir != value)
                {
                    _mangaDir = value;
                    _mangasDirObject = null;
                    _mangaDirObject = null;
                    _volumeDir = null;
                }
            }
        }


        private void TryDownloadFile(string url, string fileName)
        {
            bool ok = false;
            while (!ok)
            {
                try
                {
                    client.DownloadFile(url, fileName);
                    ok = true;
                }
                catch (Exception)
                {
                }
            }
        }

        private byte[] TryDownloadData(string url)
        {            
            while (true)
            {
                try
                {
                    return client.DownloadData(url);
                }
                catch (Exception)
                {                   
                }
            }
        }

        private DirectoryInfo _mangasDirObject = null;
        public DirectoryInfo MangasDirObject
        {
            get
            {
                if (_mangasDirObject == null)
                {
                    _mangasDirObject = new DirectoryInfo(MangaDir);
                    if (!_mangasDirObject.Exists) _mangasDirObject.Create();
                }
                return _mangasDirObject;
            }
        }

        private DirectoryInfo _mangaDirObject = null;
        public DirectoryInfo MangaDirObject
        {
            get
            {
                if (_mangaDirObject == null)
                {
                    string fName = MangasDirObject.FullName + "\\" + MangaName;
                    _mangaDirObject = new DirectoryInfo(fName);
                    if (!_mangaDirObject.Exists) _mangaDirObject.Create();
                }
                return _mangaDirObject;
            }
        }

        private DirectoryInfo _volumeDir = null;
        public DirectoryInfo VolumeDirObject
        {
            get
            {
                if (_volumeDir == null)
                {
                    string fName = MangaDirObject.FullName + "\\" + CurrentVolume;
                    _volumeDir = new DirectoryInfo(fName);
                    if (!_volumeDir.Exists) _volumeDir.Create();
                }
                return _volumeDir;
            }
        }

        public string CurrentPageUrl
        {
            get
            {
                return String.Format("http://image.read.homeunix.com/onlinereading/{0}/{0} {1}/{2}", MangaName, CurrentVolume, ImageUrls[CurrentPage]);
            }
        }

        public string CurrentFileName
        {
            get
            {
                return String.Format("{0}\\{1}", VolumeDirObject.FullName, ImageUrls[CurrentPage]);
            }
        }

        public void DetectVolumesListUrl()
        {            
            var data = TryDownloadData("http://unixmanga.com/onlinereading/manga-lists.html");
            var s = Encoding.ASCII.GetString(data);
            var index = s.IndexOf(String.Format(">{0}</a>", MangaName));
            if (index < 0) throw new Exception("Error loading manga info");            
            s = s.Substring(0, index);
            index = s.LastIndexOf("<a href=");
            if (index < 0) throw new Exception("Error loading manga info");
            s = s.Substring(index + 9);
            index = s.IndexOf("\"");
            if (index < 0) throw new Exception("Error loading manga info");
            s = s.Substring(0, index);
            VolumesListUrl = s;
        }

        public void DetectVolumeUrl()
        {
            var data = TryDownloadData(VolumesListUrl);
            var s = Encoding.ASCII.GetString(data);
            var index = s.IndexOf("[ Goto Main ]</a>");
            if (index < 0) throw new Exception("Error loading manga info");
            s = s.Substring(index + 16);
            index = s.IndexOf("<tr align=");
            if (index < 0) throw new Exception("Error loading manga info");
            s = s.Substring(index);
            index = s.IndexOf("<tr class=\"snHeading\">");
            if (index < 0) throw new Exception("Error loading manga info");
            s = s.Substring(0, index);
            s = "<list>" + s + "</list>";
            s = s.Replace("\"title=", "\" title=");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            List<VolumeInfo> list = new List<VolumeInfo>();
            var prefix = MangaName + " ";
            foreach (var node in doc.DocumentElement.ChildNodes.OfType<XmlNode>())
            {
                var tdNode = node.ChildNodes[1];
                var aNode = tdNode.ChildNodes[0];
                var hrefAttr = aNode.Attributes[0];
                var href = hrefAttr.Value;
                var name = aNode.ChildNodes[0].Value;
                name = name.Replace(prefix, "");
                list.Add(new VolumeInfo() { Name = name, Url = href });
            }
            VolumesList = list.ToArray();
            var names = (from item in list select item.Name).ToList();
            CurrentVolume = StartVolume;
            CurrentVolumeIndex = names.IndexOf(CurrentVolume);
            VolumeUrl = list[CurrentVolumeIndex].Url;
        }

        public void DetectVolumePages()
        {
            var data = TryDownloadData(VolumeUrl);
            var s = Encoding.ASCII.GetString(data);
            var index = s.IndexOf("<legend>Select A Link To Start Reading</legend>");
            if (index < 0) throw new Exception("Error loading manga info");
            index = s.IndexOf("<A", index);
            s = s.Substring(index);
            index = s.IndexOf("<br><CENTER>");
            s = s.Substring(0, index);
            s = s.Replace("SIZE=1", "size=\"1\"").Replace("<BR>", "").Replace("HREF=", "href=").Replace("<A ", "<a ").Replace("</A>", "</a>").Replace("<FONT ", "<font ").Replace("</FONT>", "</font>").Replace("&server=nas.html", "");
            s = "<list>" + s + "</list>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            var namesList = new List<string>();
            foreach (var node in doc.DocumentElement.ChildNodes.OfType<XmlNode>().Where(n => n.Name.ToUpper() == "A"))
            {
                var iNode = node.ChildNodes[0];
                var nNode = iNode.ChildNodes[0].Value;
                namesList.Add(nNode);
            }
            ImageUrls = namesList.ToArray();
            CurrentPage = 0;
        }

        public void PerformVolumeDownload()
        {
            Form.Invoke(new Form1.InitProgress(Form.InitSubProgress), new object[] { 0, ImageUrls.Length - 1 });
            for (CurrentPage = 0; CurrentPage < ImageUrls.Length; CurrentPage++)
            {
                TryDownloadFile(CurrentPageUrl, CurrentFileName);
                Form.Invoke(new Form1.UpdateProgressDelegate(Form.UpdateSubProgress));
            }
        }

        public void PerformMangaDownload()
        {
            _volumeDir = null;
            if (VolumesList == null)
            {
                DetectVolumeUrl();
            }
            StartVolumeIndex = VolumeNames.IndexOf(StartVolume);
            EndVolumeIndex = VolumeNames.IndexOf(EndVolume);
            if (StartVolumeIndex > EndVolumeIndex)
            {
                int temp = StartVolumeIndex;
                StartVolumeIndex = EndVolumeIndex;
                EndVolumeIndex = temp;
            }
            Form.Invoke(new Form1.InitProgress(Form.InitMainProgress), new object[] { StartVolumeIndex, EndVolumeIndex });
            for (CurrentVolumeIndex = StartVolumeIndex; CurrentVolumeIndex <= EndVolumeIndex; CurrentVolumeIndex++)
            {
                _volumeDir = null;
                CurrentVolume = VolumesList[CurrentVolumeIndex].Name;
                VolumeUrl = VolumesList[CurrentVolumeIndex].Url;
                DetectVolumePages();
                PerformVolumeDownload();
                Form.Invoke(new Form1.UpdateProgressDelegate(Form.UpdateMainProgress));
            }
            Form.Invoke(new Form1.UpdateProgressDelegate(Form.CompleteProgress));
        }

        public void PerformDownload(string mangaName, string startVolume, string endVolume)
        {
            MangaName = mangaName;
            StartVolume = startVolume;
            EndVolume = endVolume;
            if (VolumesListUrl == null)
            {
                DetectVolumesListUrl();
            }
            PerformMangaDownload();
        }
    }
}
