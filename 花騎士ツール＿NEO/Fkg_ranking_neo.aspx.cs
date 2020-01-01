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
    public partial class Fkg_ranking_neo : System.Web.UI.Page
    {

        private SqlConnection cn = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        //private SqlDataReader rd;
        private string cnstr =
            //@"Data Source=(LocalDB)\MSSQLLocalDB;" +
            //@"AttachDbFilename=""C:\Users\yonki\OneDrive\database\fkgdata.mdf"";" +
            //@"Integrated Security = True; Connect Timeout = 30";
            @"Server=tcp:fkg-data-yonkim.database.windows.net,1433;Initial Catalog=花騎士データ;Persist Security Info=False;User ID=yonkim;Password=Hornet600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ラジオボタンから要素抽出
            string rarity = "";
            string att = "";
            string country = "";
            int selectNum = 0;

            //レアリティ
            if (RadioButtonList1.SelectedItem.Text != "全レア")
            {
                selectNum += 1;
                switch (RadioButtonList1.SelectedItem.Text)
                {

                    case "6":
                        {
                            rarity = "Rarity = 6";
                            break;
                        }

                    case "5":
                        {
                            rarity = "Rarity = 5";
                            break;
                        }

                    case "昇華虹":
                        {
                            rarity = "Id > 10000";
                            break;
                        }
                }
            }

            //属性
            if (RadioButtonList2.SelectedItem.Text != "全属性")
            {
                selectNum += 1;
                switch (RadioButtonList2.SelectedItem.Text)
                {
                    case "斬":
                        {
                            att = "ATT = N'斬'";
                            break;
                        }

                    case "打":
                        {
                            att = "ATT = N'打'";
                            break;
                        }

                    case "突":
                        {
                            att = "ATT = N'突'";
                            break;
                        }
                    case "魔":
                        {
                            att = "ATT = N'魔'";
                            break;
                        }
                }
            }

            //国家
            if (RadioButtonList3.SelectedItem.Text != "全国家")
            {
                selectNum += 1;
                switch (RadioButtonList3.SelectedItem.Text)
                {
                    case "知徳花":
                        {
                            country = "Unit = N'ブロッサムヒル'";
                            break;
                        }

                    case "深緑花":
                        {
                            country = "Unit = N'リリィウッド'";
                            break;
                        }

                    case "常夏花":
                        {
                            country = "Unit = N'バナナオーシャン'";
                            break;
                        }
                    case "風谷花":
                        {
                            country = "Unit = N'ベルガモットバレー'";
                            break;
                        }
                    case "雪原花":
                        {
                            country = "Unit = N'ウィンターローズ'";
                            break;
                        }
                    case "湖畔花":
                        {
                            country = "Unit = N'ロータスレイク'";
                            break;
                        }
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
                        if (rarity != "")
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
                        if ((rarity != "") & (att != ""))
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
                        whereQuerry = GetWhereQuerry(str1, str2);
                        break;
                    }
                case 3:
                    {
                        str1 = rarity;
                        str2 = att;
                        str3 = country;
                        whereQuerry = GetWhereQuerry(str1, str2, str3);
                        break;
                    }

            }


            //クエリ作成
            string get_query = "";
            get_query = "SELECT Id,Name,Rarity,HP,ATK,DEF,MOV,ATT,Unit,Date " +
                "FROM [dbo].[Fkgmbr]" + whereQuerry;
            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(get_query);
            DataTable dt_fkg = new DataTable();

            dt_fkg = ds_fkg.Tables[0];

            //データテーブル加工処理
            //総合力計算
            ds_fkg.Tables[0].Columns.Add("Total", typeof(Int32));
            for (int i = 0; i < ds_fkg.Tables[0].Rows.Count; i++)
            {
                ds_fkg.Tables[0].Rows[i]["Total"] = Convert.ToInt32(ds_fkg.Tables[0].Rows[i]["HP"]) + Convert.ToInt32(ds_fkg.Tables[0].Rows[i]["ATK"]) + Convert.ToInt32(ds_fkg.Tables[0].Rows[i]["DEF"]);
            }
            //ソート処理
            //ソートキー取得
            string sortKey = "";
            string flag = "";//ID,Dateでソート時on

            switch (RadioButtonList4.SelectedItem.Text)
            {
                case "総合力":
                    {
                        sortKey = "Total";
                        break;
                    }
                case "HP":
                    {
                        sortKey = "HP";
                        break;
                    }
                case "攻撃力":
                    {
                        sortKey = "ATK";
                        break;
                    }
                case "防御力":
                    {
                        sortKey = "DEF";
                        break;
                    }
                case "移動力":
                    {
                        sortKey = "MOV";
                        break;
                    }
                case "登録日":
                    {
                        sortKey = "Unit";
                        flag = "登録日";
                        break;
                    }
                case "ID":
                    {
                        sortKey = "Ranking";
                        flag = "ID";
                        break;
                    }

            }



            if(flag != "")
            {
                ds_fkg.Tables[0].Columns.Add("Ranking", typeof(int));
                for (int j = 0; j < ds_fkg.Tables[0].Rows.Count; j++)
                {
                    if (Convert.ToInt32(ds_fkg.Tables[0].Rows[j]["Id"]) > 10000)
                    {
                        ds_fkg.Tables[0].Rows[j]["Ranking"] = Convert.ToInt32(ds_fkg.Tables[0].Rows[j]["Id"]) - 10000;
                    }
                    else
                    {
                        ds_fkg.Tables[0].Rows[j]["Ranking"] = Convert.ToInt32(ds_fkg.Tables[0].Rows[j]["Id"]);
                    }
                    //ds_fkg.Tables[0].Rows[j]["Unit"] = ds_fkg.Tables[0].Rows[j]["Date"].ToString("yyyy/MM/dd/HH,mm",
                    //System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP"));
                    //System.Globalization.CultureInfo ci =
                    //new System.Globalization.CultureInfo("ja-JP", true);
                    //ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                    ds_fkg.Tables[0].Rows[j]["Unit"] = Convert.ToDateTime(ds_fkg.Tables[0].Rows[j]["Date"]).ToString("yyyy年MM月dd日tth時m分");
                    //ds_fkg.Tables[0].Rows[j]["Unit"] = Convert.ToDateTime(ds_fkg.Tables[0].Rows[j]["Date"]).ToString("yyyy/MM/dd/HH:mm");
                }
            }

            //レコードのソートを行う
            DataView view = new DataView(ds_fkg.Tables[0]);
            view.Sort = sortKey + " DESC";//降順ソート
            dt_fkg = view.ToTable();

            //ランキング順位付け処理
            if (flag == "")
            {
                dt_fkg.Columns.Add("Ranking", typeof(Int32));
                for (int i = 0; i < dt_fkg.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        dt_fkg.Rows[i]["Ranking"] = i + 1;
                        continue;
                    }
                    if (Convert.ToInt32(dt_fkg.Rows[i][sortKey]) < Convert.ToInt32(dt_fkg.Rows[i - 1][sortKey]))
                    {//普通のカウント
                        dt_fkg.Rows[i]["Ranking"] = i + 1;
                    }
                    if (Convert.ToInt32(dt_fkg.Rows[i][sortKey]) == Convert.ToInt32(dt_fkg.Rows[i - 1][sortKey]))
                    {//同順位だった場合のカウント
                        dt_fkg.Rows[i]["Ranking"] = dt_fkg.Rows[i - 1]["Ranking"];

                    }
                }
            }

            DataTable dt_fkg2 = new DataTable();
            DataTable dt_fkg2_copyl = new DataTable();
            DataTable dt_final = new DataTable();
            if(flag == "")
            {
                sortKey = "Ranking";
            }

            //昇順処理
            if (RadioButtonList5.SelectedItem.Text == "昇順")
            {
                DataView view1 = new DataView(dt_fkg);
                view1.Sort = sortKey + " ASC";
                //最終出力データテーブルに格納する
                dt_fkg2 = view1.ToTable();
            }
            else if (RadioButtonList5.SelectedItem.Text == "降順")
            {
                dt_fkg2 = dt_fkg;
            }

            

            //表示データ数に合わせてデータをコピーする
            //コピーするデータ数算出
            int displayNumber = 0;

            switch (RadioButtonList6.SelectedItem.Text)
            {
                case "30":
                    {
                        if (dt_fkg2.Rows.Count < 30)
                            displayNumber = dt_fkg2.Rows.Count;
                        else
                            displayNumber = 30;
                        break;
                    }

                case "50":
                    {
                        if (dt_fkg2.Rows.Count < 50)
                            displayNumber = dt_fkg2.Rows.Count;
                        else
                            displayNumber = 50;
                        break;
                    }

                case "100":
                    {
                        if (dt_fkg2.Rows.Count < 100)
                            displayNumber = dt_fkg2.Rows.Count;
                        else
                            displayNumber = 100;
                        break;
                    }

                case "全":
                    {
                        displayNumber = dt_fkg2.Rows.Count;
                        break;
                    }

            }

           

            //データテーブルクローン作成
            dt_final = dt_fkg2.Clone();

            //データインポート
            for (int i = 0; i < displayNumber; i++)
            {
                dt_final.ImportRow(dt_fkg2.Rows[i]);
            }

            

            //ListView1表示
            ListView1.DataSource = dt_final;
            ListView1.DataBind();

        }


        //
        //自作関数
        //
        //
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
            string resultQuerry = " WHERE (" + str1 + ")";
            return resultQuerry;
        }

        string GetWhereQuerry(string str1, string str2)
        {
            string resultQuerry = " WHERE ((" + str1 + ") AND (" + str2 +"))";
            return resultQuerry;
        }

        string GetWhereQuerry(string str1, string str2, string str3)
        {
            string resultQuerry = " WHERE ((" + str1 + ") AND (" + str2 + ") AND (" + str3 + "))";
            return resultQuerry;
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}