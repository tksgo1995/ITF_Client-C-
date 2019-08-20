using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest
{
    public class USER_STAT
    {
        public string KUser { get; set; }
        public int FolderPolicyCount { get; set; }
        public Dictionary<string, string> Folder { get; set; }
        public USER_STAT(string beforebuf)
        {
            Folder = new Dictionary<string, string>();
            char[] seq = { '%' };
            string[] afterbuf = beforebuf.Split(seq);
            KUser = afterbuf[0];
            FolderPolicyCount = int.Parse(afterbuf[1]);

            for(int i = 2; i < FolderPolicyCount + 4; i = i + 2)
            {
                Folder.Add(afterbuf[i], afterbuf[i + 1]);
            }
            
            /*
            MessageBox.Show("\n유저키값: " + KUser +
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
