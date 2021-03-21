using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 花騎士ツール＿NEO
{
    public partial class FKG_register : System.Web.UI.Page
    {
        private SqlConnection cn = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader rd;
        private string cnstr =
            @"Server=tcp:fkg-data-yonkim.database.windows.net,1433;Initial Catalog=花騎士データ;Persist Security Info=False;User ID=yonkim;Password=Hornet600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //ページ読み込み完了後の処理
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            DisplayFKG();
        }

        //protected void Page_UnLoad(object sender, EventArgs e)
        //{
        //    //ページを閉じるときにクッキー削除
        //    HttpContext.Current.Response.Cookies["FkgRegister"].Value = "";
        //    HttpContext.Current.Response.Cookies["FkgRegister"].Expires = DateTime.Now.AddDays(-1);//有効期限
        //}

        //メインルーチン構想
        //
        //クッキーに団長情報を登録し、自分のDBidを得る仕組み
        //次ログイン時には、クッキーより自動的にユーザー情報取得する
        //
        //PCで登録した場合、モバイル版のブラウザのクッキーに、DBidを登録する必要あり。逆もしかり
        //団長名、もしくはDBidにより自分の登録情報をDBから検索し、登録する仕組みが必要


        //DBへの登録
        //所持キャラのidと、スキルレベルを登録する
        //
        //おまけ要素
        //登録したキャラの数、昇華キャラの人数等の情報をおまけ的に表示し、他団長と比較する
        //団長LVも登録、花騎士の登録データにロリ度なんかあると面白いんだが、、、それはまた別の話？w
        //ロリ団長No1とか決められるよねw

        //DBへのキャラ登録方法
        //星6の花騎士をDBより引っ張り出し表示する部分
        //どのような形がいいか？？？一度に5人くらいまとめて登録できる感じがいい？
        //その場合は選択項目のリセットボタン必須
        //
        //登録とはいいながらも、DBのカラムへの書き込み方が問題
        //花騎士1人あたり、idが最大5文字、スキルレベルが1文字、デリミタを2か所使うので、8文字必要
        //nvarcharの最大が4000文字なので、1カラムに最大500人登録可能
        //余裕をもって1カラム450人分登録出来るとして、3カラム分くらい用意しておくか？恐らく充分だろう
        //（現在のキャラ全員昇華したとして、全体で約500人程度　月に20人ずつ追加すると、年240人追加される
        //  今から5年後にだいたい500人+240人x5年=1540人！！！）
        //まぁ実際5年後までこのアプリを継続するつもりは全くない。管理費用払うの俺だしね。やだもん

        //DBへの書き込み、読出し用インターフェース
        //基本的にデータ登録文字へのエンコード、デコード関数を作成
        //新規登録はそんなに考えなくてもいいとしても、
        //登録されているデータの読出し、スキルレベルの再修正等のインターフェースは慎重に作らないとヤバい
        //



        ////////必要な機能考察
        //DBへのアカウント創設関数
        //花騎士登録関数（DBアカへ追加登録）
        //登録した花騎士データの修正
        //他ブラウザへの登録補助関数



        /////////////////////////////
        //オリジナル関数
        /////////////////////////////
        protected DataSet GetData(string queryString)
        {

            // Retrieve the connection string stored in the Web.config file.
            //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
            //SqlDataAdapter dAdapter = null;
            DataSet ds = new DataSet();

            //try
            //{aqqq
            // Connect to the database and run the query.
            cn.ConnectionString = cnstr;
            cn.Open();

            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = cnstr;
            SqlDataAdapter dAdapter = new SqlDataAdapter(queryString, cn);
            dAdapter.Fill(ds);

            cn.Close();

            //}
            //catch (Exception ex)
            //{

            //    // The connection failed. Display an error message.
            //  //  Message.Text = "Unable to connect to the database.";

            //}

            return ds;

        }

        //花騎士名をDBより取得し、ダイアログBOXに入れる(一回だけ行えばいいモジュール)
        protected void DisplayFKG()
        {
            ////クッキーがあって、なおかつOnに設定されている場合はこの処理は行わない
            //if (HttpContext.Current.Request.Cookies["FkgRegister"] != null && HttpContext.Current.Request.Cookies["FkgRegister"].ToString() == "On")
            //{
            //    return ;
            //}
            ////クッキーがない、もしくはOnになっていない場合はクッキーをOnにセット
            //HttpContext.Current.Response.Cookies["FkgRegister"].Value = "On";
            //HttpContext.Current.Response.Cookies["FkgRegister"].Expires = DateTime.Now.AddDays(1);//有効期限

            if (HiddenField0.Value == "On")
            {
                return;
            }
            HiddenField0.Value = "On";

            string list_querry = "SELECT Id, Name,A1Ex1,A1V1,A1V2,A2Ex1,A2V1,A2V2,A3Ex1,A3V1,A3V2,A4Ex1,A4V1,A4V2 FROM [dbo].[Fkgmbr]";


            //GetDataによりdataset入手する

            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);
            DataTable dt_out = ds_fkg.Tables[0];

            //花騎士の登録人数を書き出し
            Label2101.Text = dt_out.Rows.Count.ToString() + "人";

            ////クッキーに登録されているデータを取得
            //string[,] cookieFkg = DecodeFkgName();
            //string cookieNumStr = "0";
            //if (cookieFkg != null)
            //{
            //    cookieNumStr = cookieFkg.GetLength(0).ToString();
            //}
            //Label2.Text = cookieNumStr + "人";
            
            ////次にクッキー登録データと、現在取得したDBを比較し、クッキーに登録されているデータを省く処理実効
            //DataTable dt_out1 = ExpireFkgName(dt_out, cookieFkg);
            //if (dt_out1 == null)
            //{
            //    dt_out1 = dt_out;
            //}

            //レコードのソートを行う
            DataView view = new DataView(dt_out);
            view.Sort = "NAME";
            DataTable dt_out2 = view.ToTable();

            //datasetの値をDropDownList1に入力する
            this.DropDownList2001.DataSource = view.ToTable();
            this.DropDownList2001.DataTextField = "Name";
            this.DropDownList2001.DataValueField = "Id";
            this.DropDownList2001.DataBind();

            //DropDownList2からは一番上に登録しないを加えるための処理
            DataRow datarow = dt_out2.NewRow();
            datarow["id"] = 0;
            datarow["Name"] = "登録しない";
            dt_out2.Rows.InsertAt(datarow, 0);

            this.DropDownList2002.DataSource = dt_out2;
            this.DropDownList2002.DataTextField = "Name";
            this.DropDownList2002.DataValueField = "Id";
            this.DropDownList2002.DataBind();

            this.DropDownList2003.DataSource = dt_out2;
            this.DropDownList2003.DataTextField = "Name";
            this.DropDownList2003.DataValueField = "Id";
            this.DropDownList2003.DataBind();

            this.DropDownList2004.DataSource = dt_out2;
            this.DropDownList2004.DataTextField = "Name";
            this.DropDownList2004.DataValueField = "Id";
            this.DropDownList2004.DataBind();

            this.DropDownList2005.DataSource = dt_out2;
            this.DropDownList2005.DataTextField = "Name";
            this.DropDownList2005.DataValueField = "Id";
            this.DropDownList2005.DataBind();


            //編集用のドロップダウンリストに花騎士名入力

            //まず、クッキーデータから、DBを呼び出して、キャラ名とスキルレベルの情報を付加する処理
            
            //クッキーがもし無ければ処理終了
            if (HttpContext.Current.Request.Cookies["FkgName"] == null)
            {
                return;
            }
            //クッキーがある場合はデータ取得
            string readString = HttpContext.Current.Request.Cookies["FkgName"].Value;
            DataTable dt_display = GetFkgFromCookie(readString);


            //表示ルーチン
            this.DropDownList2011.Items.Clear();
            //レコードのソートを行う
            DataView view2 = new DataView(dt_display);
            view2.Sort = "NAME";
            DataTable dt_display2 = view2.ToTable();

            datarow = dt_display2.NewRow();
            datarow["id"] = 0;
            datarow["Name"] = "花騎士を選択して下さい";
            datarow["Slv"] = 0;
            datarow["Slv+id"] = 0;
            dt_display2.Rows.InsertAt(datarow, 0);

            //datasetの値をDropDownListに入力する
            this.DropDownList2011.DataSource = dt_display2;
            this.DropDownList2011.DataTextField = "Name";
            this.DropDownList2011.DataValueField = "Slv+id";
            this.DropDownList2011.DataBind();

            this.DropDownList2012.SelectedIndex = Convert.ToInt32(dt_display2.Rows[0]["Slv"]) - 1;

            

        }

        //
        //花騎士登録ボタン
        //
        protected void Button101_Click(object sender, EventArgs e)
        {
            //ドロップダウンリストより花騎士名入手
            //スキルレベル入手
            //デリミタは#
            //this.DropDownList1.SelectedValue
            string[] fkgName = new string[5];
            string[] fkgSlv = new string[5];

            fkgName[0] = this.DropDownList2001.SelectedValue;
            fkgName[1] = this.DropDownList2002.SelectedValue;
            fkgName[2] = this.DropDownList2003.SelectedValue;
            fkgName[3] = this.DropDownList2004.SelectedValue;
            fkgName[4] = this.DropDownList2005.SelectedValue;

            string[] fkgName2 = new string[5];
            fkgName2[0] = this.DropDownList2001.SelectedItem.ToString();
            fkgName2[1] = this.DropDownList2002.SelectedItem.ToString();
            fkgName2[2] = this.DropDownList2003.SelectedItem.ToString();
            fkgName2[3] = this.DropDownList2004.SelectedItem.ToString();
            fkgName2[4] = this.DropDownList2005.SelectedItem.ToString();

            int count = 0;

            switch (RadioButtonList2001.SelectedItem.Value)
            {
                case "1":
                    {
                        fkgSlv[count] = "1";
                        break;
                    }
                case "2":
                    {
                        fkgSlv[count] = "2";
                        break;
                    }
                case "3":
                    {
                        fkgSlv[count] = "3";
                        break;
                    }
                case "4":
                    {
                        fkgSlv[count] = "4";
                        break;
                    }
                case "5":
                    {
                        fkgSlv[count] = "5";
                        break;
                    }
            }
            count++;

            switch (RadioButtonList2002.SelectedItem.Value)
            {
                case "1":
                    {
                        fkgSlv[count] = "1";
                        break;
                    }
                case "2":
                    {
                        fkgSlv[count] = "2";
                        break;
                    }
                case "3":
                    {
                        fkgSlv[count] = "3";
                        break;
                    }
                case "4":
                    {
                        fkgSlv[count] = "4";
                        break;
                    }
                case "5":
                    {
                        fkgSlv[count] = "5";
                        break;
                    }
            }
            count++;

            switch (RadioButtonList2003.SelectedItem.Value)
            {
                case "1":
                    {
                        fkgSlv[count] = "1";
                        break;
                    }
                case "2":
                    {
                        fkgSlv[count] = "2";
                        break;
                    }
                case "3":
                    {
                        fkgSlv[count] = "3";
                        break;
                    }
                case "4":
                    {
                        fkgSlv[count] = "4";
                        break;
                    }
                case "5":
                    {
                        fkgSlv[count] = "5";
                        break;
                    }
            }
            count++;

            switch (RadioButtonList2004.SelectedItem.Value)
            {
                case "1":
                    {
                        fkgSlv[count] = "1";
                        break;
                    }
                case "2":
                    {
                        fkgSlv[count] = "2";
                        break;
                    }
                case "3":
                    {
                        fkgSlv[count] = "3";
                        break;
                    }
                case "4":
                    {
                        fkgSlv[count] = "4";
                        break;
                    }
                case "5":
                    {
                        fkgSlv[count] = "5";
                        break;
                    }
            }
            count++;

            switch (RadioButtonList2005.SelectedItem.Value)
            {
                case "1":
                    {
                        fkgSlv[count] = "1";
                        break;
                    }
                case "2":
                    {
                        fkgSlv[count] = "2";
                        break;
                    }
                case "3":
                    {
                        fkgSlv[count] = "3";
                        break;
                    }
                case "4":
                    {
                        fkgSlv[count] = "4";
                        break;
                    }
                case "5":
                    {
                        fkgSlv[count] = "5";
                        break;
                    }
            }
            //
            //同じ番号が選択されていないか確認する処理
            //
            string writeString = EncodeFkgName(ref fkgName, ref fkgSlv, out int addNum);
            //nullが返って来ている場合
            if (writeString == null)
            {
                //もしダブりあれば、エラーメッセージ出して処理終了
                string script = "<script language=javascript>" + "window.alert('選択した花騎士名がダブっています。同じ花騎士を登録する事は出来ません。')" + "</script>";
                Response.Write(script);
                return;
            }

            //クッキー読出し
            string readString = "";
            //存在確認
            if (HttpContext.Current.Request.Cookies["FkgName"] != null)
            {
                //クッキーがあれば文字列取得
                readString = HttpContext.Current.Request.Cookies["FkgName"].Value;

            }
            //クッキー内に同じ花騎士名の登録があるか確認
            string result = CompareFkgName(ref fkgName);
            //既にクッキー内に登録がある場合
            if(result != "")
            {
                string[] dabuli = result.Substring(1).Split('#');
                string output = "";
                for (int i = 0; i < dabuli.Length; i++)
                {
                    if(output !="")
                    {
                        output += "と";
                    }
                    output += fkgName2[Convert.ToInt32(dabuli[i])];
                }
                //エラーメッセージ出力
                string script = "<script language=javascript>" + "window.alert('既に" + output + "は登録されています。同じ花騎士を登録する事は出来ません。')" + "</script>";
                Response.Write(script);
                return;
            }

            //クッキーに登録（既にクッキーがある場合は上書きになる）
            HttpContext.Current.Response.Cookies["FkgName"].Value = readString + writeString;
            HttpContext.Current.Response.Cookies["FkgName"].Expires = DateTime.Now.AddDays(180);//有効期限

            

            //編集用のドロップダウンリストに花騎士名入力

            //まず、クッキーデータから、DBを呼び出して、キャラ名とスキルレベルの情報を付加する処理
            DataTable dt_display = GetFkgFromCookie(readString + writeString);

            //表示ルーチン
            this.DropDownList2011.Items.Clear();
            //レコードのソートを行う
            DataView view = new DataView(dt_display);
            view.Sort = "NAME";
            DataTable dt_display2 = view.ToTable();

            //テーブル一番上に文字列表示
            DataRow datarow = dt_display2.NewRow();
            datarow["id"] = 0;
            datarow["Name"] = "花騎士を選択して下さい";
            datarow["Slv"] = 0;
            datarow["Slv+id"] = 0;
            dt_display2.Rows.InsertAt(datarow, 0);

            //datasetの値をDropDownList1に入力する
            this.DropDownList2011.DataSource = dt_display2;
            this.DropDownList2011.DataTextField = "Name";
            this.DropDownList2011.DataValueField = "Slv+id";
            this.DropDownList2011.DataBind();

            this.DropDownList2012.SelectedIndex = Convert.ToInt32(dt_display2.Rows[0]["Slv"]) - 1;

            //登録用ドロップダウンリスト初期化
            this.DropDownList2001.SelectedIndex = 0;
            this.DropDownList2002.SelectedIndex = 0;
            this.DropDownList2003.SelectedIndex = 0;
            this.DropDownList2004.SelectedIndex = 0;
            this.DropDownList2005.SelectedIndex = 0;

            this.RadioButtonList2001.SelectedIndex = 0;
            this.RadioButtonList2002.SelectedIndex = 0;
            this.RadioButtonList2003.SelectedIndex = 0;
            this.RadioButtonList2004.SelectedIndex = 0;
            this.RadioButtonList2005.SelectedIndex = 0;

            this.RadioButtonList2006.SelectedIndex = 0;
            this.RadioButtonList2007.SelectedIndex = 0;
            this.RadioButtonList2008.SelectedIndex = 0;
            this.RadioButtonList2009.SelectedIndex = 0;
            this.RadioButtonList2010.SelectedIndex = 0;

            //デバッグ用登録成功確認窓　必要？
            if (readString != null)
            {
                //デバッグ用表示
                string script = "<script language=javascript>" + "window.alert('今回は" + addNum + "名の花騎士を登録しました。')" + "</script>";
                Response.Write(script);
            }

        }


        //クッキー消去ボタン
        protected void Button102_Click(object sender, EventArgs e)
        {
            if(!CheckBox1.Checked)
            {
                string script1 = "<script language=javascript>" + "window.alert('本当に消去したい場合は消去用チェックボックスをオンにして下さい')" + "</script>";
                Response.Write(script1);
                return;
            }
            //クッキーに登録（既にクッキーがある場合は上書きになる）
            HttpContext.Current.Response.Cookies["FkgName"].Value = "";
            HttpContext.Current.Response.Cookies["FkgName"].Expires = DateTime.Now.AddDays(-1);//有効期限

            DropDownList2011.Items.Clear();

            string script = "<script language=javascript>" + "window.alert('現在クッキーに登録されている花騎士を全消去しました。')" + "</script>";
            Response.Write(script);
            return;
        }


        ///////////////////////////////////////////////
        //花騎士登録時の文字列内にダブりがあるか判定。
        //あればnull、無ければ書き込み用文字列を返す
        //////////////////////////////////////////////

        protected string EncodeFkgName(ref string[] Name, ref string[] Slv, out int Num)
        {
            string returnString = "";
            Num = 0;

            //Name[0]は必ず値が入っている
            //他の要素がダブっている場合はエラーを返す
            if (Array.IndexOf(Name, Name[0], 1) > 0)
            {
                return null;
            }
            Num++;
            returnString += "#" + Name[0] + "#" + Slv[0];

            //Name[1]が0じゃない場合は、同様に他の要素とのダブりをチェック
            if (Name[1] != "0")
            {
                if (Array.IndexOf(Name, Name[1], 2) > 0)
                {
                    return null;
                }
                Num++;
                returnString += "#" + Name[1] + "#" + Slv[1];
            }

            //Name[2]が0じゃない場合は、同様に他の要素とのダブりをチェック
            if (Name[2] != "0")
            {
                if (Array.IndexOf(Name, Name[2], 3) > 0)
                {
                    return null;
                }
                Num++;
                returnString += "#" + Name[2] + "#" + Slv[2];
            }

            //Name[3]が0じゃない場合は、同様に他の要素とのダブりをチェック
            if (Name[3] != "0")
            {
                if (Array.IndexOf(Name, Name[3], 4) > 0)
                {
                    return null;
                }
                Num++;
                returnString += "#" + Name[3] + "#" + Slv[3];
            }

            //Name[4]が0じゃない場合
            if (Name[4] != "0")
            {
                Num++;
                returnString += "#" + Name[4] + "#" + Slv[4];
            }

            return returnString;
        }

        //クッキーに登録されているデータを取得する処理
        //戻り値は二次元配列
        public string[,] DecodeFkgName()
        {
            //クッキーがもし無ければnullを返す
            if (HttpContext.Current.Request.Cookies["FkgName"] == null)
            {
                return null;
            }
            //クッキーがある場合はデータ取得
            string readString = HttpContext.Current.Request.Cookies["FkgName"].Value;
            char[] delimita = { '#', '&' };
            string[] origen = readString.Split(delimita,StringSplitOptions.RemoveEmptyEntries);
            int length = origen.Length;
            length = length / 2;
            string[,] returnString = new string[length, 2];

            //forループにて、デコードする
            int count = 0;
            for (int i = 0; i < length * 2; i++)
            {
                //iが偶数
                if (i % 2 == 0)
                {
                    returnString[count, 0] = origen[i];
                }


                //iが奇数
                else
                {
                    returnString[count, 1] = origen[i];
                    count++;
                }

            }
            return returnString;
            
        }




        //クッキー内のデータに登録ストリングのデータが入っているか確認する処理
        //ダブっている花騎士が何個目なのかを返す。何も入ってなければ""
        protected string CompareFkgName(ref string[] FkgName)
        {
            string[,] cookieFkg = DecodeFkgName();
            //クッキーがもし無ければこれ以降の処理はない
            if(cookieFkg == null)
            {
                return "";
            }
            string returnString = "";

            for(int i = 0; i < cookieFkg.GetLength(0); i++)
            {
                for(int j = 0; j < FkgName.Length; j++)
                {
                    if(cookieFkg[i, 0] == FkgName[j])
                    {
                        //クッキー内のデータと一致する場合
                        returnString += "#" + j;
                    }
                }
            }

            return returnString;
        }


        //
        //超重要関数
        //
        //クッキーデータによりフィルタされた花騎士名前データを呼び出す。
        //スキルレベル情報付き
        protected DataTable GetFkgFromCookie(string cookieData)
        {
            string[] origen = (cookieData).Substring(1).Split('#');
            int length = origen.Length;
            length = length / 2;
            string[,] registeredString = new string[length, 2];

            //forループにて、出コードする
            int count2 = 0;
            for (int i = 0; i < length * 2; i++)
            {
                //iが偶数
                if (i % 2 == 0)
                {
                    registeredString[count2, 0] = origen[i];
                }
                //iが奇数
                else
                {
                    registeredString[count2, 1] = origen[i];
                    count2++;
                }
            }//解読終了

            //Where句の材料作成
            string whereString = " WHERE ";
            for(int i = 0; i < registeredString.GetLength(0); i++)
            {
                if(i != 0)
                {
                    whereString += " OR ";
                }
                whereString += "Id = " + registeredString[i, 0];
            }

            //DBへの問い合わせ準備
            string list_querry = "SELECT Id, Name FROM [dbo].[Fkgmbr]" + whereString;

            //GetDataによりdataset入手する
            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);
            DataTable dt_fkg = ds_fkg.Tables[0];

            dt_fkg.Columns.Add("Slv", typeof(int));
            dt_fkg.Columns.Add("Slv+id", typeof(string));

            for (int i = 0; i < dt_fkg.Rows.Count; i++)
            {
                
                for (int j = 0; j < registeredString.GetLength(0); j++)
                {
                    if(dt_fkg.Rows[i]["Id"].ToString() == registeredString[j,0])
                    {
                        dt_fkg.Rows[i]["Slv"] = registeredString[j, 1];
                    }
                }
                dt_fkg.Rows[i]["Slv+id"] = dt_fkg.Rows[i]["Slv"].ToString() + "#" + dt_fkg.Rows[i]["id"].ToString();
            }

            return dt_fkg;
        }



        //
        //このモジュール内では使わないけど、このアイデアは使える
        //
        //現在のDBから、配列に登録されているデータを省く処理
        protected DataTable ExpireFkgName (DataTable dt_in, string[,] cookie)
        {
            if(cookie == null)
            {
                return dt_in;
            }
            
            //DBデータでループ
            for(int i = dt_in.Rows.Count - 1; i > -1; i--)
            {
                //クッキーデータでループ
                for (int j = 0; j < cookie.GetLength(0); j++)
                {
                    //クッキーデータと一致するか確認
                    if(cookie[j, 0].IndexOf(dt_in.Rows[i]["id"].ToString()) != -1)
                    {
                        //一致したのでDBからデータ削除
                        dt_in.Rows.RemoveAt(i);

                    }
                }
            }
            return dt_in;








        }


    }//クラス作成領域の終わり



    /// <summary>
    /// string 型の拡張メソッドを管理するクラス
    /// </summary>
    public static partial class StringExtensions
    {

        /// <summary>
        /// 指定した文字列がいくつあるか
        /// </summary>
        public static int CountOf(this string self, params string[] strArray)
        {
            int count = 0;

            foreach (string str in strArray)
            {
                int index = self.IndexOf(str, 0);
                while (index != -1)
                {
                    count++;
                    index = self.IndexOf(str, index + str.Length);
                }
            }

            return count;
        }

    }
}
