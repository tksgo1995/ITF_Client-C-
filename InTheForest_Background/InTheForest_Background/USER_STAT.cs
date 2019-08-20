using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest_Background
{
    public class USER_STAT
    {
        public int mask { get; set; }
        public string KUser { get; set; }
        public int FolderPolicyCount { get; set; }
        public Dictionary<string, string> Folder { get; set; }
        public USER_STAT(string beforebuf)
        {
            Folder = new Dictionary<string, string>();
            char[] seq = { '%' };
            string[] afterbuf = beforebuf.Split(seq);
            mask = int.Parse(afterbuf[0]);
            KUser = afterbuf[1];
            FolderPolicyCount = int.Parse(afterbuf[2]);

            for(int i = 3; i < FolderPolicyCount + 4; i = i + 2)
            {
                Folder.Add(afterbuf[i], afterbuf[i + 1]);
            }
            /*
            MessageBox.Show("마스크값: " + mask +
                "\n유저키값: " + KUser +
                "\n정책개수: " + FolderPolicyCount);
            foreach(KeyValuePair<string, string> item in Folder)
            {
                MessageBox.Show("폴더이름: " + item.Key + 
                    "\n폴더키값: " + item.Value);
            }
            */
        }
    }
}
