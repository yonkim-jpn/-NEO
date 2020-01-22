using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 花騎士ツール＿NEO
{
    public partial class Magia_damage_calc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ディスプレイサイズにより、魔法少女タイプのラジオボタンリストを垂直に切り替え
            //if(Request.Browser.IsMobileDevice)
            //{
            //    var a = Request.UserAgent;
            //    if (a.Contains("iPhone") || (a.Contains("Android") && a.Contains("Mobile")))
            //        this.tipoPuella.RepeatColumns = 2;
            //}
        }
    }
}