using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBoxs.Core
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "{\"app\":\"com.tencent.structmsg\",\"config\":{\"autosize\":true,\"ctime\":1617340542,\"forward\":true,\"token\":\"e488161bf14c8e3020bdfde2ddbd336e\",\"type\":\"normal\"},\"desc\":\"音乐\",\"extra\":{\"app_type\":1,\"appid\":205141,\"msg_seq\":6946029831377966539,\"uin\":2812695303},\"meta\":{\"music\":{\"action\":\"\",\"android_pkg_name\":\"\",\"app_type\":1,\"appid\":205141,\"desc\":\"赵雷\",\"jumpUrl\":\"https://t1.kugou.com/song.html?id=4r4a5axWV2\",\"musicUrl\":\"http://m.kugou.com/api/v1/wechat/index?uuid=f6b54e55b7aeaa965f40c985175985a0&album_audio_id=53532136&ext=m4a&apiver=2&cmd=101&album_id=1946722&hash=cbf8fa6c9a4bc799d2a55c64f419d86c&plat=0&version=10559&share_chl=qq_client&mid=188729877832146169449386693706424247651&key=8940a409ae7558142985f942d27f242d&_t=1617340542&user_id=451688086&sign=97fdaec15618392a2572f7aedea016fa\",\"preview\":\"http://imge.kugou.com/stdmusic/120/20160830/20160830162246581077.jpg\",\"sourceMsgId\":\"0\",\"source_icon\":\"\",\"source_url\":\"\",\"tag\":\"酷狗音乐\",\"title\":\"成都\"}},\"prompt\":\"[分享]成都\",\"ver\":\"0.0.0.1\",\"view\":\"music\"}";
            App.Common.CqApi.SendPrivateMsgJson(msg);
        }
    }
}
