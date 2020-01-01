using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class fkg_ranking : System.Web.UI.Page
{
    private SqlConnection cn = new SqlConnection();
    private SqlCommand cmd = new SqlCommand();
    private SqlDataReader rd;
    private string cnstr =
        //@"Data Source=(LocalDB)\MSSQLLocalDB;" +
        //@"AttachDbFilename=""C:\Users\yonki\OneDrive\database\fkgdata.mdf"";" +
        //@"Integrated Security = True; Connect Timeout = 30";
        @"Server=tcp:fkg-data-yonkim.database.windows.net,1433;Initial Catalog=花騎士データ;Persist Security Info=False;User ID=yonkim;Password=Hornet600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    protected void Page_Load(object sender, EventArgs e)
    {
  
    }

    //表示ボタンクリック
    /*コーディング方針
        クエリのセレクト文に最大4種のＡＮＤ要素が入るので、
        何種入るか算出する。
        算出したその要素を入力して、セレクトクエリを出力する関数作成
        関数は、引数1-3個をもらう。オーバーロード
         */
    protected void Button1_Click(object sender, EventArgs e)
    {
        //リストボックス内アイテムクリア
        this.ListBox1.Items.Clear();


        //ラジオボタンから要素抽出
        string rarity = "";
        string att = "";
        string country = "";
        int selectNum = 0;

        //レアリティ
        if (!RadioButton1.Checked)
        {
            selectNum += 1;
            //クエリパーツ作成
            if (RadioButton2.Checked)
            {
                rarity = "Rarity = 6";
            }
            else if (RadioButton3.Checked)
            { 
                rarity = "Rarity = 5";
            }
            else if (RadioButton4.Checked)
            {
                rarity = "Rarity = 4 AND Rarity = 3 AND Rarity = 2";
            }
            else if (RadioButton27.Checked)
            {
                rarity = "Id > 10000";
            }
        }

        //属性
        if (!RadioButton5.Checked)
        {
            selectNum += 1;
            //クエリパーツ作成
            if (RadioButton6.Checked)
            {
                att = "ATT = N'斬'";
            }
            else if (RadioButton7.Checked)
            {
                att = "ATT = N'打'";
            }
            else if (RadioButton8.Checked)
            {
                att = "ATT = N'突'";
            }
            else if (RadioButton9.Checked)
            {
                att = "ATT = N'魔'";
            }
        }

        //国家
        if (!RadioButton10.Checked)
        {
            selectNum += 1;
            //クエリパーツ作成
            if (RadioButton11.Checked)
            {
                country = "Unit = N'ブロッサムヒル'";
            }
            else if (RadioButton12.Checked)
            {
                country = "Unit = N'リリィウッド'";
            }
            else if (RadioButton13.Checked)
            {
                country = "Unit = N'バナナオーシャン'";
            }
            else if (RadioButton14.Checked)
            {
                country = "Unit = N'ベルガモットバレー'";

            }
            else if (RadioButton15.Checked)
            {
                country = "Unit = N'ウィンターローズ'";

            }
            else if (RadioButton16.Checked)
            {
                country = "Unit = N'ロータスレイク'";

            }
        }

        //Where句生成
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string whereQuerry = "";

        switch (selectNum)
        {
            case 1:
                {
                    if(rarity != "")
                    {
                        str1 = rarity;
                    }
                    else if (att != "")
                    {
                        str1 = att;
                    }
                    else if (country != "")
                    {
                        str1 = country;
                    }
                    whereQuerry = GetWhereQuerry(str1);
                    break;
                }
            case 2:
                {
                    if((rarity !="")&(att != ""))
                    {
                        str1 = rarity;
                        str2 = att;
                    }
                    if ((rarity != "") & (country != ""))
                    {
                        str1 = rarity;
                        str2 = country;
                    }
                    if ((att != "") & (country != ""))
                    {
                        str1 = att;
                        str2 = country;
                    }
                    whereQuerry = GetWhereQuerry(str1,str2);
                    break;
                }
            case 3:
                {
                    str1 = rarity;
                    str2 = att;
                    str3 = country;
                    whereQuerry = GetWhereQuerry(str1,str2,str3);
                    break;
                }

        }


        //クエリ作成
        string get_query = "";
        get_query = "SELECT Id,Name,Rarity,HP,ATK,DEF,MOV,ATT,Unit " +
            "FROM [dbo].[Fkgmbr]"  + whereQuerry;
        DataSet ds_fkg = new DataSet();
        ds_fkg = GetData(get_query);
        DataTable dt_fkg = new DataTable();

        dt_fkg = ds_fkg.Tables[0];

        //データテーブル加工処理
        //総合力計算
        ds_fkg.Tables[0].Columns.Add("Total", typeof(Int32));
        for(int i =0; i < ds_fkg.Tables[0].Rows.Count;i++)
        {
            ds_fkg.Tables[0].Rows[i]["Total"] = Convert.ToInt32(ds_fkg.Tables[0].Rows[i]["HP"]) + Convert.ToInt32(ds_fkg.Tables[0].Rows[i]["ATK"]) + Convert.ToInt32(ds_fkg.Tables[0].Rows[i]["DEF"]);
        }
        //ソート処理
        //ソートキー取得
        string sortKey = "";

        if(RadioButton17.Checked)
        {
            sortKey = "Total";
        }
        else if(RadioButton18.Checked)
        {
            sortKey = "HP";
        }
        else if (RadioButton19.Checked)
        {
            sortKey = "ATK";
        }
        else if (RadioButton20.Checked)
        {
            sortKey = "DEF";
        }
        else if (RadioButton21.Checked)
        {
            sortKey = "MOV";
        }

        //レコードのソートを行う
        DataView view = new DataView(ds_fkg.Tables[0]);
        view.Sort = sortKey　+ " DESC";//降順ソート
        dt_fkg = view.ToTable();

        //ランキング順位付け処理
        dt_fkg.Columns.Add("Ranking", typeof(Int32));
        for (int i = 0; i < dt_fkg.Rows.Count;i++)
        {
            if (i == 0)
            {
                dt_fkg.Rows[i]["Ranking"] = i + 1;
                continue;
            }
            if (Convert.ToInt32(dt_fkg.Rows[i][sortKey]) < Convert.ToInt32(dt_fkg.Rows[i-1][sortKey]))
            {//普通のカウント
                dt_fkg.Rows[i]["Ranking"] = i + 1;
            }
            if (Convert.ToInt32(dt_fkg.Rows[i][sortKey]) == Convert.ToInt32(dt_fkg.Rows[i - 1][sortKey]))
            {//同順位だった場合のカウント
                dt_fkg.Rows[i]["Ranking"] = dt_fkg.Rows[i-1]["Ranking"];

            }


        }

        //
        DataTable dt_fkg2 = new DataTable();
        DataTable dt_fkg2_copyl = new DataTable();
        DataTable dt_final = new DataTable();

        //昇順処理
        if (RadioButton23.Checked)
        {
            DataView view1 = new DataView(dt_fkg);
            view1.Sort = "Ranking DESC";
            //最終出力データテーブルに格納する
            dt_fkg2 = view1.ToTable();
        }
        //降順
        if (RadioButton22.Checked)
        {
            dt_fkg2 = dt_fkg;
        }

        //表示データ数に合わせてデータをコピーする
        //コピーするデータ数算出
        int displayNumber = 0;

        //30個
        if (RadioButton24.Checked)
        {
            if (dt_fkg2.Rows.Count < 30)
                displayNumber = dt_fkg2.Rows.Count;
            else
                displayNumber = 30;
        }

        //50個
        if (RadioButton25.Checked)
        {
            if (dt_fkg2.Rows.Count < 50)
                displayNumber = dt_fkg2.Rows.Count;
            else
                displayNumber = 50;
        }

        //100個
        if (RadioButton26.Checked)
        {
            if (dt_fkg2.Rows.Count < 100)
                displayNumber = dt_fkg2.Rows.Count;
            else
                displayNumber = 100;
        }

        //データテーブルクローン作成
        dt_final = dt_fkg2.Clone();

        //データインポート
        for (int i = 0; i < displayNumber ; i++)
        {
            dt_final.ImportRow(dt_fkg2.Rows[i]);
        }

        //データセットからレコードを取り出す処理
        //for (int i = 0; i < dt_fkg.Rows.Count;i ++)
        //{
        //    int j = i + 1;
        //    //ListBox1.Items.Add(string.Format("{0:000}位 {1,-120} {2,5} {3,5} {4,5} {5,5} {6,5}",j, dt_fkg.Rows[i]["Name"].ToString().PadRight(120- dt_fkg.Rows[i]["Name"].ToString().Length,' '), dt_fkg.Rows[i]["Total"], dt_fkg.Rows[i]["HP"], dt_fkg.Rows[i]["ATK"], dt_fkg.Rows[i]["DEF"], dt_fkg.Rows[i]["MOV"]));
        //    //ListBox1.Items.Add(string.Format("{0:000}位 " + dt_fkg.Rows[i]["Name"].ToString().PadRight(40 - dt_fkg.Rows[i]["Name"].ToString().Length, ' ') +  " {1,5} {2,5} {3,5} {4,5} {5,5}", j, dt_fkg.Rows[i]["Total"], dt_fkg.Rows[i]["HP"], dt_fkg.Rows[i]["ATK"], dt_fkg.Rows[i]["DEF"], dt_fkg.Rows[i]["MOV"]));

        //    string nameString = dt_fkg.Rows[i]["Name"].ToString().PadRight(40 - dt_fkg.Rows[i]["Name"].ToString().Length, ' ');
        //    //ListBox1.Items.Add(string.Format("{0,-3}位 ", j) + nameString + string.Format(" {0,5} {1,5} {2,5} {3,5} {4,5}", j, dt_fkg.Rows[i]["Total"], dt_fkg.Rows[i]["HP"], dt_fkg.Rows[i]["ATK"], dt_fkg.Rows[i]["DEF"], dt_fkg.Rows[i]["MOV"]));
        //    ListBox1.Items.Add(string.Format("{0,3}位 ", j) + nameString);
        //}


        //データ表示
        grvDetail.DataSource = dt_final;
        grvDetail.DataBind();

        //Table1.Rows.Clear();
        //for (int i = 0; i < dt_fkg.Rows.Count; i++)
        //{
        //    HtmlTableRow row = new HtmlTableRow();
        //    for (int k = 0; k <8; k++)
        //    {
        //        HtmlTableCell cell = new HtmlTableCell();

        //        switch (k)
        //        {

        //            case 0:
        //                cell.InnerText = dt_fkg.Rows[i][0].ToString();
        //                break;
        //            case 1:
        //                cell.InnerText = dt_fkg.Rows[i]["Name"].ToString();
        //                break;
        //            case 2:
        //                cell.InnerText = dt_fkg.Rows[i]["Total"].ToString();
        //                break;
        //            case 3:
        //                cell.InnerText = dt_fkg.Rows[i]["Total"].ToString();
        //                break;
        //            case 4:
        //                cell.InnerText = dt_fkg.Rows[i]["HP"].ToString();
        //                break;
        //        }
        //    }
        //}

    }

    DataSet GetData(String queryString)
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

    string GetWhereQuerry(string str1)
    {
        string resultQuerry = " WHERE " + str1;
        return resultQuerry;
    }

    string GetWhereQuerry(string str1, string str2)
    {
        string resultQuerry = " WHERE " + str1 + " AND " + str2;
        return resultQuerry;
    }

    string GetWhereQuerry(string str1, string str2,string str3)
    {
        string resultQuerry = " WHERE " + str1 + " AND " + str2 + " AND " + str3;
        return resultQuerry;
    }

}
