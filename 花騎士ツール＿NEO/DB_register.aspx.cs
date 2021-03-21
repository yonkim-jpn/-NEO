using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Net.Http;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;

namespace 花騎士ツール＿NEO
{
    public partial class DB_register : System.Web.UI.Page
    {
        private SqlConnection cn = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader rd;
        private string cnstr =
            //@"Data Source=(LocalDB)\MSSQLLocalDB;" +
            //@"AttachDbFilename=""C:\Users\yonki\OneDrive\database\fkgdata.mdf"";" +
            //@"Integrated Security = True; Connect Timeout = 30";
            @"Server=tcp:fkg-data-yonkim.database.windows.net,1433;Initial Catalog=花騎士データ;Persist Security Info=False;User ID=yonkim;Password=Hornet600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private SqlConnection cn1 = new SqlConnection();
        private SqlCommand cmd1 = new SqlCommand();
        private SqlDataReader rd1;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button50003.Click += new EventHandler(Button50002_Click);
            //Button3_Click += new EventHandler(Button2_Click);
        }

        protected void Button50002_Click(object sender, EventArgs e)
        {
            //入力クリア処理
            this.wikiURL.Text = "";
            this.TextBox50001.Text = "";
            this.TextBox50002.Text = "";
            this.DropDownList50001.SelectedIndex = 0;
            this.DropDownList50005.SelectedIndex = 0;
            this.DropDownList50006.SelectedIndex = 0;

            this.TextBox50003.Text = "";
            this.TextBox50004.Text = "";
            this.TextBox50005.Text = "";
            this.TextBox50006.Text = "";

            this.TextBox50007.Text = "";
            this.TextBox50016.Text = "";
            this.DropDownList50016.SelectedIndex = 0;

            this.DropDownList50017.SelectedIndex = 0;
            this.TextBox50017.Text = "";

            this.DropDownList50002.SelectedIndex = 0;
            this.DropDownList50003.SelectedIndex = 0;
            this.DropDownList50004.SelectedIndex = 0;
            this.TextBox50008.Text = "";
            this.TextBox50009.Text = "";
            this.DropDownList50021.SelectedIndex = 0;

            this.DropDownList50007.SelectedIndex = 0;
            this.DropDownList50008.SelectedIndex = 0;
            this.DropDownList50009.SelectedIndex = 0;
            this.TextBox50010.Text = "";
            this.TextBox50011.Text = "";
            this.DropDownList50022.SelectedIndex = 0;

            this.DropDownList50010.SelectedIndex = 0;
            this.DropDownList50011.SelectedIndex = 0;
            this.DropDownList50012.SelectedIndex = 0;
            this.TextBox50012.Text = "";
            this.TextBox50013.Text = "";
            this.DropDownList50023.SelectedIndex = 0;

            this.DropDownList50013.SelectedIndex = 0;
            this.DropDownList50014.SelectedIndex = 0;
            this.DropDownList50015.SelectedIndex = 0;
            this.TextBox50014.Text = "";
            this.TextBox50015.Text = "";
            this.DropDownList50024.SelectedIndex = 0;

            this.A1NO2.SelectedIndex = 0;
            this.A1NO3.SelectedIndex = 0;
            this.A2NO2.SelectedIndex = 0;
            this.A2NO3.SelectedIndex = 0;
            this.A3NO2.SelectedIndex = 0;
            this.A3NO3.SelectedIndex = 0;
            this.A4NO2.SelectedIndex = 0;
            this.A4NO3.SelectedIndex = 0;
        }

        protected void Button50001_Click(object sender, EventArgs e)
        {

            if (!CheckBox50001.Checked)
            {
                //
                //通常登録処理
                //

                //特殊処理 防御選択時、小数がＤＢ内に入力されるため、値を十倍して入力する
                double diffValue = 0;
                string diff = "防御ダメ軽減率上昇";
                string diff_add = "攻撃力上昇し、防御ダメ軽減率上昇";
                if ((this.DropDownList50004.Text == diff) | (this.DropDownList50009.Text == diff) | (this.DropDownList50012.Text == diff) | (this.DropDownList50015.Text == diff)
                    |(this.DropDownList50004.Text == diff_add) | (this.DropDownList50009.Text == diff_add) | (this.DropDownList50012.Text == diff_add) | (this.DropDownList50015.Text == diff_add)
                    )
                {
                    if ((this.DropDownList50004.Text == diff)| (this.DropDownList50004.Text == diff_add))
                    {
                        diffValue = Convert.ToDouble(TextBox50009.Text);
                        diffValue *= 10;
                        TextBox50009.Text = diffValue.ToString();
                    }

                    if ((this.DropDownList50009.Text == diff)| (this.DropDownList50009.Text == diff_add))
                    {
                        diffValue = Convert.ToDouble(TextBox50011.Text);
                        diffValue *= 10;
                        TextBox50011.Text = diffValue.ToString();
                    }

                    if ((this.DropDownList50012.Text == diff)| (this.DropDownList50012.Text == diff_add))
                    {
                        diffValue = Convert.ToDouble(TextBox50013.Text);
                        diffValue *= 10;
                        TextBox50013.Text = diffValue.ToString();
                    }

                    if ((this.DropDownList50015.Text == diff)| (this.DropDownList50015.Text == diff_add))
                    {
                        diffValue = Convert.ToDouble(TextBox50015.Text);
                        diffValue *= 10;
                        TextBox50015.Text = diffValue.ToString();
                    }
                }

                //特殊処理2 反撃選択時、小数がＤＢ内に入力されるため、値を百倍して入力する
                double diffValue2 = 0;
                string diff2 = "反撃";
                if ((this.DropDownList50004.Text.IndexOf(diff2) != -1) | (this.DropDownList50009.Text.IndexOf(diff2) != -1) | (this.DropDownList50012.Text.IndexOf(diff2) != -1) | (this.DropDownList50015.Text.IndexOf(diff2) != -1))
                {
                    if (this.DropDownList50004.Text.IndexOf(diff2) != -1)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50009.Text);
                        diffValue2 *= 100;
                        TextBox50009.Text = diffValue2.ToString();
                    }

                    if (this.DropDownList50009.Text.IndexOf(diff2) != -1)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50011.Text);
                        diffValue2 *= 100;
                        TextBox50011.Text = diffValue2.ToString();
                    }

                    if (this.DropDownList50012.Text.IndexOf(diff2) != -1)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50013.Text);
                        diffValue2 *= 100;
                        TextBox50013.Text = diffValue2.ToString();
                    }

                    if (this.DropDownList50015.Text.IndexOf(diff2) != -1)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50015.Text);
                        diffValue2 *= 100;
                        TextBox50015.Text = diffValue2.ToString();
                    }
                }

                //特殊処理 スキル倍率が小数で入力されるため、10倍して格納する
                double skillRatio = 0;
                skillRatio = Convert.ToDouble(TextBox50017.Text);
                skillRatio *= 10;
                TextBox50017.Text = skillRatio.ToString();

                //登録処理
                try
                {
                    //入力されたIdの行があるか確認する処理
                    cn1.ConnectionString = cnstr;
                    cn1.Open();
                    cmd1.Connection = cn1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT Id FROM [dbo].[Fkgmbr] WHERE Id = '" + TextBox50001.Text + "'";
                    rd1 = cmd1.ExecuteReader();
                    if (rd1.HasRows == true)
                    {
                        //メッセージボックス表示
                        string script1 = "<script language=javascript>" + "window.alert('既にレコードがある。DB編集から実行して下さい。')" + "</script>";
                        Response.Write(script1);
                        return;
                    }
                    rd1.Close();
                    cn1.Close();
                    //確認完了

                    cn.ConnectionString = cnstr;
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [dbo].[Fkgmbr] (Id, Name, Rarity, ATT, Unit, HP, ATK, DEF, MOV, STP, STPMax, ANum, SType, SRatio, A1st1, A1NO, A1NO2, A1NO3, A1Ex1, A1V1, A1V2, A1Ex2, A2st1, A2NO, A2NO2, A2NO3, A2Ex1, A2V1, A2V2, A2Ex2, A3st1, A3NO, A3NO2, A3NO3, A3Ex1, A3V1, A3V2, A3Ex2, A4st1, A4NO, A4NO2, A4NO3, A4Ex1, A4V1, A4V2, A4Ex2, Date) VALUES(" +
                                       "'" + TextBox50001.Text + "'," + "N'" + TextBox50002.Text + "'," +
                                       "'" + DropDownList50001.Text + "'," + "N'" + DropDownList50005.Text + "'," + "N'" + DropDownList50006.Text + "'," +
                                       "'" + TextBox50003.Text + "'," + "'" + TextBox50004.Text + "'," + "'" + TextBox50005.Text + "'," + "'" + TextBox50006.Text + "'," +
                                       "'" + TextBox50007.Text + "'," + "'" + TextBox50016.Text + "'," + "'" + DropDownList50016.Text + "'," +
                                       "N'" + DropDownList50017.Text + "'," + "'" + TextBox50017.Text + "'," +
                                       "'" + DropDownList50002.SelectedValue + "'," + "'" + DropDownList50003.SelectedValue + "'," + "'" + A1NO2.SelectedValue + "'," + "'" + A1NO3.SelectedValue + "'," + "N'" + DropDownList50004.Text + "'," + "'" + TextBox50008.Text + "'," + "'" + TextBox50009.Text + "'," + "N'" + DropDownList50021.Text + "'," +
                                       "'" + DropDownList50007.SelectedValue + "'," + "'" + DropDownList50008.SelectedValue + "'," + "'" + A2NO2.SelectedValue + "'," + "'" + A2NO3.SelectedValue + "'," + "N'" + DropDownList50009.Text + "'," + "'" + TextBox50010.Text + "'," + "'" + TextBox50011.Text + "'," + "N'" + DropDownList50022.Text + "'," +
                                       "'" + DropDownList50010.SelectedValue + "'," + "'" + DropDownList50011.SelectedValue + "'," + "'" + A3NO2.SelectedValue + "'," + "'" + A3NO3.SelectedValue + "'," + "N'" + DropDownList50012.Text + "'," + "'" + TextBox50012.Text + "'," + "'" + TextBox50013.Text + "'," + "N'" + DropDownList50023.Text + "'," +
                                       "'" + DropDownList50013.SelectedValue + "'," + "'" + DropDownList50014.SelectedValue + "'," + "'" + A4NO2.SelectedValue + "'," + "'" + A4NO3.SelectedValue + "'," + "N'" + DropDownList50015.Text + "'," + "'" + TextBox50014.Text + "'," + "'" + TextBox50015.Text + "'," + "N'" + DropDownList50024.Text + "'," +
                                       "'" + DateTime.Now + "')";
                    rd = cmd.ExecuteReader();
                    rd.Close();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    //メッセージボックス表示
                    string script1 = "<script language=javascript>" + "window.alert('DB登録処理失敗')" + "</script>";
                    Response.Write(script1);
                    return;

                }
            }
            else if (CheckBox50001.Checked)
            {
                //
                //編集登録処理
                //
                //注意点　小数は入らない。もともと表示された値に準拠して編集する
                //
                try
                {
                    //ダミーを開いて入力されたIdの行があるか確認する処理
                    cn1.ConnectionString = cnstr;
                    cn1.Open();
                    cmd1.Connection = cn1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT Id FROM [dbo].[Fkgmbr] WHERE Id = '" + TextBox50001.Text + "'";
                    rd1 = cmd1.ExecuteReader();
                    if (rd1.HasRows != true)
                    {
                        //メッセージボックス表示
                        string script1 = "<script language=javascript>" + "window.alert('そのIdにはレコードがない。DB編集処理失敗')" + "</script>";
                        Response.Write(script1);
                        return;
                    }
                    rd1.Close();
                    cn1.Close();

                    //特殊処理 防御選択時、小数がＤＢ内に入力されるため、値を十倍して入力する
                    double diffValue = 0;
                    string diff = "防御ダメ軽減率上昇";
                    string diff_add = "攻撃力上昇し、防御ダメ軽減率上昇";
                    string text50009 = TextBox50009.Text;
                    string text50011 = TextBox50011.Text;
                    string text50013 = TextBox50013.Text;
                    string text50015 = TextBox50015.Text;

                    if ((this.DropDownList50004.Text == diff) | (this.DropDownList50009.Text == diff) | (this.DropDownList50012.Text == diff) | (this.DropDownList50015.Text == diff)
                        | (this.DropDownList50004.Text == diff_add) | (this.DropDownList50009.Text == diff_add) | (this.DropDownList50012.Text == diff_add) | (this.DropDownList50015.Text == diff_add)
                        )
                    {
                        if ((this.DropDownList50004.Text == diff)| (this.DropDownList50004.Text == diff_add))
                        {
                            diffValue = Convert.ToDouble(text50009);
                            diffValue *= 10;
                            text50009 = diffValue.ToString();
                        }

                        if ((this.DropDownList50009.Text == diff)| (this.DropDownList50009.Text == diff_add))
                        {
                            diffValue = Convert.ToDouble(text50011);
                            diffValue *= 10;
                            text50011 = diffValue.ToString();
                        }

                        if ((this.DropDownList50012.Text == diff)| (this.DropDownList50012.Text == diff_add))
                        {
                            diffValue = Convert.ToDouble(text50013);
                            diffValue *= 10;
                            text50013 = diffValue.ToString();
                        }

                        if ((this.DropDownList50015.Text == diff)| (this.DropDownList50015.Text == diff_add))
                        {
                            diffValue = Convert.ToDouble(text50015);
                            diffValue *= 10;
                            text50015 = diffValue.ToString();
                        }
                    }

                    //特殊処理2 反撃選択時、小数がＤＢ内に入力されるため、値を百倍して入力する
                    double diffValue2 = 0;
                    string diff2 = "反撃";
                    if ((this.DropDownList50004.Text.IndexOf(diff2) != -1) | (this.DropDownList50009.Text.IndexOf(diff2) != -1) | (this.DropDownList50012.Text.IndexOf(diff2) != -1) | (this.DropDownList50015.Text.IndexOf(diff2) != -1))
                    {
                        if (this.DropDownList50004.Text.IndexOf(diff2) != -1)
                        {
                            diffValue2 = Convert.ToDouble(text50009);
                            diffValue2 *= 100;
                            text50009 = diffValue2.ToString();
                        }

                        if (this.DropDownList50009.Text.IndexOf(diff2) != -1)
                        {
                            diffValue2 = Convert.ToDouble(text50011);
                            diffValue2 *= 100;
                            text50011 = diffValue2.ToString();
                        }

                        if (this.DropDownList50012.Text.IndexOf(diff2) != -1)
                        {
                            diffValue2 = Convert.ToDouble(text50013);
                            diffValue2 *= 100;
                            text50013 = diffValue2.ToString();
                        }

                        if (this.DropDownList50015.Text.IndexOf(diff2) != -1)
                        {
                            diffValue2 = Convert.ToDouble(text50015);
                            diffValue2 *= 100;
                            text50015 = diffValue2.ToString();
                        }
                    }

                    //特殊処理 スキル倍率が小数で入力されるため、10倍して格納する
                    double skillRatio = 0;
                    string text50017 = TextBox50017.Text;
                    skillRatio = Convert.ToDouble(text50017);
                    skillRatio *= 10;
                    text50017 = skillRatio.ToString();

                    //編集処理
                    cn.ConnectionString = cnstr;
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [dbo].[Fkgmbr] " +
                                       "SET Id = '" + TextBox50001.Text + "',Name = N'" + TextBox50002.Text +
                                       "', Rarity = '" + DropDownList50001.Text + "', ATT = N'" + DropDownList50005.Text + "', Unit = N'" + DropDownList50006.Text +
                                       "', HP = '" + TextBox50003.Text + "', ATK = '" + TextBox50004.Text + "', DEF = '" + TextBox50005.Text + "', MOV = '" + TextBox50006.Text +
                                       "', STP = '" + TextBox50007.Text + "', STPMax = '" + TextBox50016.Text + "', ANum = '" + DropDownList50016.Text +
                                       "', SType = N'" + DropDownList50017.Text + "', SRatio = '" + text50017 +
                                       "', A1st1 = '" + DropDownList50002.SelectedValue + "', A1NO = '" + DropDownList50003.SelectedValue + "', A1NO2 = '" + A1NO2.SelectedValue + "', A1NO3 = '" + A1NO3.SelectedValue + "', A1Ex1 = N'" + DropDownList50004.Text + "', A1V1 = '" + TextBox50008.Text + "', A1V2 = '" + text50009 + "', A1Ex2 = N'" + DropDownList50021.Text +
                                       "', A2st1 = '" + DropDownList50007.SelectedValue + "', A2NO = '" + DropDownList50008.SelectedValue + "', A2NO2 = '" + A2NO2.SelectedValue + "', A2NO3 = '" + A2NO3.SelectedValue + "', A2Ex1 = N'" + DropDownList50009.Text + "', A2V1 = '" + TextBox50010.Text + "', A2V2 = '" + text50011 + "', A2Ex2 = N'" + DropDownList50022.Text +
                                       "', A3st1 = '" + DropDownList50010.SelectedValue + "', A3NO = '" + DropDownList50011.SelectedValue + "', A3NO2 = '" + A3NO2.SelectedValue + "', A3NO3 = '" + A3NO3.SelectedValue + "', A3Ex1 = N'" + DropDownList50012.Text + "', A3V1 = '" + TextBox50012.Text + "', A3V2 = '" + text50013 + "', A3Ex2 = N'" + DropDownList50023.Text +
                                       "', A4st1 = '" + DropDownList50013.SelectedValue + "', A4NO = '" + DropDownList50014.SelectedValue + "', A4NO2 = '" + A4NO2.SelectedValue + "', A4NO3 = '" + A4NO3.SelectedValue + "', A4Ex1 = N'" + DropDownList50015.Text + "', A4V1 = '" + TextBox50014.Text + "', A4V2 = '" + text50015 + "', A4Ex2 = N'" + DropDownList50024.Text +
                                       "', Date = '" + DateTime.Now + "' " +
                                       "WHERE Id = '" + TextBox50001.Text + "'";
                    rd = cmd.ExecuteReader();
                    rd.Close();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    //メッセージボックス表示
                    string script1 = "<script language=javascript>" + "window.alert('DB編集処理失敗')" + "</script>";
                    Response.Write(script1);
                    return;

                }
            }

            //メッセージボックス表示
            string script = "<script language=javascript>" + "window.alert('処理終了')" + "</script>";
            Response.Write(script);

        }

        protected void Button50003_Click(object sender, EventArgs e)
        {
            //ダミー
        }

        protected void DropDownList50004_SelectedIndexChanged(object sender, EventArgs e)
        {


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

            //中身を取得出来ているか確認処理
            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return ds;

        }

        protected void Button50004_Click(object sender, EventArgs e)
        {
            //データ呼出しボタン
            //string partQuery = "Id = " + Microsoft.VisualBasic.Strings.StrConv(TextBox1.Text, VbStrConv.Narrow);
            //TextBox1.Text = Microsoft.VisualBasic.Strings.StrConv(TextBox1.Text, VbStrConv.Narrow);

            string partQuery = "Id = " + TextBox50001.Text;

            if (TextBox50001.Text == "")
            {
                partQuery = "Name = N'" + TextBox50002.Text + "'";
            }

            string get_query = "";
            get_query = "SELECT Id, Name, Rarity, ATT, Unit, HP, ATK, DEF, MOV, STP, STPMax, SType, SRatio, ANum, A1st1, A1NO, A1NO2, A1NO3, A1Ex1, A1V1, A1V2, A1Ex2, A2st1, A2NO, A2NO2, A2NO3, A2Ex1, A2V1, A2V2, A2Ex2, A3st1, A3NO, A3NO2, A3NO3, A3Ex1, A3V1, A3V2, A3Ex2, A4st1, A4NO, A4NO2, A4NO3, A4Ex1, A4V1, A4V2, A4Ex2 " +
            //"FROM [dbo].[Fkgmbr] WHERE Id =" + mbrid[0] + ", Id =" + mbrid[1] + ", Id =" + mbrid[2] + ", Id =" + mbrid[3] + ", Id =" + mbrid[4];
            "FROM [dbo].[Fkgmbr] WHERE " + partQuery;

            DataSet ds_fkg = GetData(get_query);

            if (ds_fkg == null)
            {
                //メッセージボックス表示
                string script = "<script language=javascript>" + "window.alert('見つかりません')" + "</script>";
                Response.Write(script);
                return;
            }

            DataRow dt_row = ds_fkg.Tables[0].Rows[0];

            if (TextBox50001.Text != "")
            {
                TextBox50002.Text = dt_row["Name"].ToString();
            }
            else
            {
                TextBox50001.Text = dt_row["Id"].ToString();
            }


            DropDownList50001.Text = dt_row["Rarity"].ToString();
            DropDownList50005.Text = dt_row["ATT"].ToString();
            DropDownList50006.Text = dt_row["Unit"].ToString();

            TextBox50003.Text = dt_row["HP"].ToString();
            TextBox50004.Text = dt_row["ATK"].ToString();
            TextBox50005.Text = dt_row["DEF"].ToString();
            TextBox50006.Text = dt_row["MOV"].ToString();

            TextBox50007.Text = dt_row["STP"].ToString();
            TextBox50016.Text = dt_row["STPMax"].ToString();
            DropDownList50016.Text = dt_row["Anum"].ToString();

            DropDownList50017.Text = dt_row["SType"].ToString();
            //スキル倍率の小数表示処理
            double skillRatio = Convert.ToDouble(dt_row["SRatio"]);
            skillRatio /= 10;
            TextBox50017.Text = skillRatio.ToString();

            DropDownList50002.Text = dt_row["A1st1"].ToString();
            DropDownList50003.Text = dt_row["A1NO"].ToString();
            A1NO2.Text = dt_row["A1NO2"].ToString();
            A1NO3.Text = dt_row["A1NO3"].ToString();
            DropDownList50004.Text = dt_row["A1Ex1"].ToString();
            TextBox50008.Text = dt_row["A1V1"].ToString();
            TextBox50009.Text = dt_row["A1V2"].ToString();
            DropDownList50021.Text = dt_row["A1Ex2"].ToString();

            DropDownList50007.Text = dt_row["A2st1"].ToString();
            DropDownList50008.Text = dt_row["A2NO"].ToString();
            A2NO2.Text = dt_row["A2NO2"].ToString();
            A2NO3.Text = dt_row["A2NO3"].ToString();
            DropDownList50009.Text = dt_row["A2Ex1"].ToString();
            TextBox50010.Text = dt_row["A2V1"].ToString();
            TextBox50011.Text = dt_row["A2V2"].ToString();
            DropDownList50022.Text = dt_row["A2Ex2"].ToString();

            DropDownList50010.Text = dt_row["A3st1"].ToString();
            DropDownList50011.Text = dt_row["A3NO"].ToString();
            A3NO2.Text = dt_row["A3NO2"].ToString();
            A3NO3.Text = dt_row["A3NO3"].ToString();
            DropDownList50012.Text = dt_row["A3Ex1"].ToString();
            TextBox50012.Text = dt_row["A3V1"].ToString();
            TextBox50013.Text = dt_row["A3V2"].ToString();
            DropDownList50023.Text = dt_row["A3Ex2"].ToString();

            DropDownList50013.Text = dt_row["A4st1"].ToString();
            DropDownList50014.Text = dt_row["A4NO"].ToString();
            A4NO2.Text = dt_row["A4NO2"].ToString();
            A4NO3.Text = dt_row["A4NO3"].ToString();
            DropDownList50015.Text = dt_row["A4Ex1"].ToString();
            TextBox50014.Text = dt_row["A4V1"].ToString();
            TextBox50015.Text = dt_row["A4V2"].ToString();
            DropDownList50024.Text = dt_row["A4Ex2"].ToString();

            //反撃、防御の時の値を小数に変換する処理追加
            //特殊処理 防御選択時、ＤＢ内では小数ではないので、値を1/10にして入力する
            double diffValue = 0;
            string diff = "防御ダメ軽減率上昇";
            string diff_add = "攻撃力上昇し、防御ダメ軽減率上昇";
            if ((this.DropDownList50004.Text == diff) | (this.DropDownList50009.Text == diff) | (this.DropDownList50012.Text == diff) | (this.DropDownList50015.Text == diff)
                | (this.DropDownList50004.Text == diff_add) | (this.DropDownList50009.Text == diff_add) | (this.DropDownList50012.Text == diff_add) | (this.DropDownList50015.Text == diff_add)
                )
            {
                if ((this.DropDownList50004.Text == diff)| (this.DropDownList50004.Text == diff_add))
                {
                    diffValue = Convert.ToDouble(TextBox50009.Text);
                    diffValue /= 10;
                    TextBox50009.Text = diffValue.ToString();
                }

                if ((this.DropDownList50009.Text == diff)| (this.DropDownList50009.Text == diff_add))
                {
                    diffValue = Convert.ToDouble(TextBox50011.Text);
                    diffValue /= 10;
                    TextBox50011.Text = diffValue.ToString();
                }

                if ((this.DropDownList50012.Text == diff)| (this.DropDownList50012.Text == diff_add))
                {
                    diffValue = Convert.ToDouble(TextBox50013.Text);
                    diffValue /= 10;
                    TextBox50013.Text = diffValue.ToString();
                }

                if ((this.DropDownList50015.Text == diff)|(this.DropDownList50015.Text == diff_add))
                {
                    diffValue = Convert.ToDouble(TextBox50015.Text);
                    diffValue /= 10;
                    TextBox50015.Text = diffValue.ToString();
                }
            }

            //特殊処理2 反撃選択時、ＤＢ内は小数ではないため、値を1/100して入力する
            double diffValue2 = 0;
            string diff2 = "反撃";
            if ((this.DropDownList50004.Text == diff2) | (this.DropDownList50009.Text == diff2) | (this.DropDownList50012.Text == diff2) | (this.DropDownList50015.Text == diff2))
            {
                if (this.DropDownList50004.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox50009.Text);
                    diffValue2 /= 100;
                    TextBox50009.Text = diffValue2.ToString();
                }

                if (this.DropDownList50009.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox50011.Text);
                    diffValue2 /= 100;
                    TextBox50011.Text = diffValue2.ToString();
                }

                if (this.DropDownList50012.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox50013.Text);
                    diffValue2 /= 100;
                    TextBox50013.Text = diffValue2.ToString();
                }

                if (this.DropDownList50015.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox50015.Text);
                    diffValue2 /= 100;
                    TextBox50015.Text = diffValue2.ToString();
                }
            }


        }

        private HttpClient httpClient;
        private HtmlParser htmlParser;

        protected async void Db_Button_Click(object sender, EventArgs e)
        {


            // タイトルを取得したいサイトのURL
            string urlstring = wikiURL.Text;

            // 指定したサイトのHTMLをストリームで取得する
            var doc = default(IHtmlDocument);
            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
            {
                // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                var parser = new HtmlParser();
                doc = await parser.ParseDocumentAsync(stream);
            }

            string[] urlElements = new string[6];

            //HTMLから取得
            //id
            var urlElements0 = doc.QuerySelector("#content_1_0");
            urlElements[0] = urlElements0.InnerHtml;
            int statusFlag = 0;
            int movCount = 0;
            string[] status = new string[4];
            int skillFlag = 0;
            string skill = "";
            string skillType = "";

            var s = doc.QuerySelectorAll("#body > div.ie5 > table > tbody > tr");
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].QuerySelector("th") != null)
                {
                    switch (s[i].QuerySelector("th").TextContent)
                    {
                        case "名前":
                            {
                                urlElements[1] = s[i].QuerySelector("td:nth-child(3)").TextContent;
                                break;
                            }
                        case "レアリティ":
                            {
                                urlElements[2] = s[i].QuerySelector("td").TextContent;
                                break;
                            }
                        case "衣装":
                            {
                                urlElements[3] = s[i].QuerySelector("td").TextContent;
                                break;
                            }
                        case "属性":
                            {
                                urlElements[4] = s[i].QuerySelector("td").TextContent;
                                break;
                            }
                        case "所属国家":
                            {
                                urlElements[5] = s[i].QuerySelector("td").TextContent;
                                break;
                            }
                        case "開花・咲":
                            {
                                if (urlElements[2] == "★★★★★★")
                                {
                                    if (statusFlag == 1)
                                        statusFlag++;
                                }
                                break;
                            }
                        case "昇華・咲":
                        case "昇華・咲(開花)":
                            {
                                if(statusFlag == 1)
                                    statusFlag++;
                                continue;
                            }
                        case "スキル":
                        case "スキル＆アビリティ":
                            {
                                skillFlag = 1;
                                continue;
                            }
                    }
                    if (s[i].QuerySelector("th:nth-child(9)") != null)
                    {
                        if (s[i].QuerySelector("th:nth-child(9)").InnerHtml == "移動力")
                        {
                            statusFlag = 1;
                            continue;
                        }
                    }

                }
                //ステータス取得

                //HP,ATK,DEF
                if(statusFlag == 2)
                {
                    var statusElements = s[i].QuerySelectorAll("strong");
                    status[0] = statusElements[0].InnerHtml;
                    status[1] = statusElements[1].InnerHtml;
                    status[2] = statusElements[2].InnerHtml;
                    statusFlag++;
                }
                //MOV
                if(statusFlag == 1)
                {
                    if (movCount == 0)
                    {
                        status[3] = s[i].QuerySelector("td:nth-child(12)").InnerHtml;
                        movCount = 1;
                    }
                }

                //スキル取得
                if(skillFlag > 0)
                {
                    if(urlElements[2] == "★★★★★★")
                    {
                        switch(skillFlag)
                        {
                            case 1:
                                {
                                    if((s[i].QuerySelector("th:nth-child(2)") != null)&&(s[i].QuerySelector("th:nth-child(2)").InnerHtml == "通常"))
                                        skill = s[i].QuerySelector("td:nth-child(4)").InnerHtml;
                                    else
                                        skill = s[i].QuerySelector("td:nth-child(3)").InnerHtml;
                                    break;
                                }
                            case 2:
                                {
                                    skillType = s[i].QuerySelector("td").InnerHtml;
                                    goto roopEnd;
                                }
                        }
                    }
                    else
                    {
                        switch (skillFlag)
                        {
                            case 3:
                                {
                                    skill = s[i].QuerySelector("td").InnerHtml;
                                    break;
                                }
                            case 4:
                                {
                                    skillType = s[i].QuerySelector("td").InnerHtml;
                                    goto roopEnd;
                                }
                        }
                    }

                    skillFlag++;
                }
            }
            roopEnd:

            //アビリティ部スクレーピング
            string[] ability = new string[4];
            int count = 0;
            int abilityFlag = 0;

            var t = doc.QuerySelectorAll("#body > div.ie5 > table > tbody > tr");
            for (int i = 0; i < t.Length; i++)
            {
                if (count != 0)
                {
                    ability[count] = t[i].QuerySelector("td").TextContent;
                    count++;
                    if (count == 4)
                        break;
                }

                if (t[i].QuerySelector("th") != null)
                {
                    if (t[i].QuerySelector("th").TextContent == "アビリティ")
                    {
                        abilityFlag = 1;
                        continue;
                    }
                    if ((abilityFlag == 1)&&((t[i].QuerySelector("th").TextContent == "昇華アビリティ") || (t[i].QuerySelector("th").TextContent == "昇華アビリティ(開花)") || (t[i].QuerySelector("th").TextContent == "昇華(開花)") || (t[i].QuerySelector("th").TextContent == "昇華") || ((urlElements[2] == "★★★★★★") && ((t[i].QuerySelector("th").TextContent == "開花アビリティ") || (t[i].QuerySelector("th").TextContent == "開花")))))
                    {

                        ability[0] = t[i].QuerySelector("td").TextContent;
                        count++;
                    }
                }
               
            }

            //画面に入力していく
            //id
            string shouka = "";
            if (urlElements[2] == "★★★★★★")
            {
                this.TextBox50001.Text = urlElements0.InnerHtml.Substring(urlElements0.InnerHtml.IndexOf(".") + 1, 3);
            }
            else
            {//昇華の場合の処理
                //IDNoが正確ではない場合(???標記の場合)かどうか判定
                int i = 0;
                if(int.TryParse(urlElements0.InnerHtml.Substring(urlElements0.InnerHtml.IndexOf(".") + 1, 3), out i))
                    this.TextBox50001.Text = (Convert.ToInt32(urlElements0.InnerHtml.Substring(urlElements0.InnerHtml.IndexOf(".") + 1, 3)) + 10000).ToString();
                else//正確ではない場合の処理、そのまま入力
                    this.TextBox50001.Text = urlElements0.InnerHtml.Substring(urlElements0.InnerHtml.IndexOf(".") + 1, 3);

                shouka = "　昇華";
            }
            //name
            //衣装、属性確認
            string ishou = "";
            string attribute = "";
            string country = "";

            if(urlElements[3] != null)
            {
                if (urlElements[3] != "-")
                {
                    ishou = "（" + urlElements[3] + "）";
                }
            }

            attribute = urlElements[4];
            country = urlElements[5];


            if(urlElements[1].IndexOf("/")==-1)
                this.TextBox50002.Text = urlElements[1] + ishou + shouka;
            else
                this.TextBox50002.Text = urlElements[1].Substring(0, urlElements[1].IndexOf("/")) + ishou + shouka;
            //属性
            int index = 0;
            foreach (ListItem item in DropDownList50005.Items)
            {
                if (item.Value == attribute)
                {
                    DropDownList50005.SelectedIndex = index;
                    break;
                }
                index++;
            }
            //所属国家
            index = 0;
            foreach (ListItem item in DropDownList50006.Items)
            {
                if (country == "ウインターローズ")
                    country = "ウィンターローズ";
                if (item.Value == country)
                {
                    DropDownList50006.SelectedIndex = index;
                    break;
                }
                index++;
            }

            //HP
            this.TextBox50003.Text = status[0];
            this.TextBox50004.Text = status[1];
            this.TextBox50005.Text = status[2];
            this.TextBox50006.Text = status[3];

            //スキル発動率
            this.TextBox50007.Text = skill.Substring(0, 2);
            //LV5側が??の場合
            if (skill.Substring(skill.LastIndexOf("%") - 2, 2) != "??")
                this.TextBox50016.Text = skill.Substring(skill.LastIndexOf("%") - 2, 2);
            else
                this.TextBox50016.Text = (Convert.ToInt32(skill.Substring(0, 2)) + 10).ToString();
            //スキルタイプ
            string Stype = "";
            //記述があるか確認処理
            if(skillType.IndexOf("倍") == -1)
            {
                //記述無い場合、敵全体に280倍ダメージとする
                skillType = "敵全体に280倍ダメージ";
            }
            switch (skillType.Substring(0,skillType.IndexOf("倍")-3))
            {
                case "敵全体に":
                    {
                        Stype = "全体";
                        break;
                    }
                case "敵2体に":
                    {
                        Stype = "2体";
                        break;
                    }
                case "敵に3回":
                    {
                            Stype = "複数回";
                        break;
                    }
                case "敵単体に":
                    {
                        //吸収チェック
                        if (skillType.IndexOf("吸収") != -1)
                            Stype = "吸収";
                        else
                            Stype = "単体";
                        break;
                    }
                default:
                    {
                        //変則チェック
                        if (skillType.IndexOf("場合") != -1)
                        {
                            if (skillType.IndexOf("吸収") != -1)
                                Stype = "変則吸収";
                            else
                                Stype = "変則";
                        }
                        break;
                    }
            }
            index = 0;
            foreach (ListItem item in DropDownList50017.Items)
            {
                if (item.Text == Stype)
                {
                    DropDownList50017.SelectedIndex = index;
                    break;
                }
                index++;
            }
            //スキル倍率
            this.TextBox50017.Text = skillType.Substring(skillType.IndexOf("倍")-3,3);

            //アビリティ
            for(int j = 0;j<4;j++)
            {
                if(ability[j]!=null)
                {
                    WriteAbility(j,ability[j]);
                }
            }

        }

        protected void WriteAbility(int abiNo,string ability)
        {
            string[,] pattern = new string[35,2];
            pattern[0,0] = @"^戦闘中、\w*(パーティメンバー|パーティーメンバー|自身)の攻撃力が\d{2}%上昇$"; pattern[0, 1] = "1";
            pattern[1,0] = @"戦闘中、(パーティメンバーが.*それぞれ|パーティーメンバーが.*それぞれ|自身は)\d回ダメージを無効化する\w*?"; pattern[1, 1] = "1";
            pattern[2,0] = @"攻撃力を\d{2}%低下させる\w*?"; pattern[2, 1] = "1";
            pattern[3,0] = @"^戦闘中、\w*(?!自身が攻撃を受けた次ターン時に)(パーティーメンバー|パーティメンバー|自身)の\w*スキル発動率が\w*\d\.*\d{0,2}倍\w*"; pattern[3, 1] = "1";
            pattern[4,0] = @"戦闘中、ソーラードライブの効果が\d{2}%上昇"; pattern[4, 1] = "1";
            pattern[5,0] = @"戦闘中、(パーティメンバー|パーティーメンバー|自身)のスキルダメージが\d{2}%上昇$"; pattern[5, 1] = "1";
            pattern[6,0] = @"(パーティメンバー|パーティーメンバー|自身)がボスに対して与えるダメージが\d{2}%増加する"; pattern[6, 1] = "1";
            pattern[7,0] = @"戦闘中、(パーティ|パーティー)メンバーの与えるダメージがターン経過に応じ\d{2}%ずつ上昇\w*?"; pattern[7, 1] = "1";
            pattern[8,0] = @"(パーティメンバー|パーティーメンバー|自身)の防御力が\d{2}%、防御時のダメージ軽減率が\d\.?\d?%上昇、自身は確率で3回まで戦闘不能にならずHP1で耐える\w*?"; pattern[8, 1] = "2";
            pattern[9,0] = @"攻撃を受けた時、100%の確率で防御力の\d\.*\d*倍を攻撃力に変換し反撃\w*?"; pattern[9, 1] = "2";
            pattern[10,0] = @"戦闘中、自身は\dターンまで\d{2}%、以降は\d{2}%の確率で敵の攻撃を回避する"; pattern[10, 1] = "2";
            pattern[11,0] = @"自身が敵に攻撃を与えた後.*\d{2,3}%の確率で(自身は再行動|再行動)する"; pattern[11, 1] = "1";
            pattern[12,0] = @"毎ターン\d{2}%の確率で\w*最大HPの\d{1,2}%回復する\w*?"; pattern[12, 1] = "2";
            pattern[13,0] = @"光GAUGEが\d+%溜まった状態から討伐開始"; pattern[13, 1] = "1";
            pattern[14,0] = @"戦闘中、(パーティメンバー|パーティーメンバー|自身)のクリティカル攻撃発生率が\d{2}%\w*?、クリティカルダメージが\d{2,3}%上昇\w*?"; pattern[14, 1] = "2";
            pattern[15,0] = @"ボス敵との戦闘中、(パーティメンバー|パーティーメンバー|自身)の攻撃力が\d{2}%上昇$"; pattern[15, 1] = "1";
            pattern[16,0] = @"ボス敵との戦闘中、(パーティメンバー|パーティーメンバー|自身)の攻撃力が\d{2}%上昇し、ボスに対して与えるダメージが\d{2,3}%増加する"; pattern[16, 1] = "2";
            pattern[17,0] = "に応じて、パーティメンバーのスキル発動率が上昇"; pattern[17, 1] = "2";
            //自身のスキルレベル(1～5)でマッチ出来ない。やる方法ある？
            pattern[18,0] = @"戦闘中、(パーティメンバー|パーティーメンバー|自身)のスキルダメージが\d{2}%上昇し、"; pattern[18, 1] = "2";
            pattern[19, 0] = @"^戦闘中、(パーティ|パーティー)メンバーの攻撃力が\d{2}%上昇し、さらに自身の攻撃力が\d{2}%上昇"; pattern[19, 1] = "2";
            pattern[20, 0] = @"戦闘中、自身が攻撃を行った後、自身はパーティ総合力の\d{2}%を攻撃力に変換し、(攻撃を行った敵|攻撃を与えた敵|敵全体)に追撃する"; pattern[20, 1] = "1";
            pattern[21, 0] = @"(斬|打|突|魔)属性弱点";pattern[21, 1] = "1";
            pattern[22, 0] = @"戦闘中、(パーティメンバー|パーティーメンバー|自身)の攻撃力が\d{2}%上昇し、(パーティ|パーティー)メンバーのスキル発動率がそれぞれの好感度に応じて最大\d\.\d{1,2}倍上昇"; pattern[22, 1] = "2";
            pattern[23, 0] = @"(敵3体|敵全体)が\d{2}%の確率で攻撃をミスするようになる"; pattern[23, 1] = "1";
            pattern[24, 0] = @"自身が攻撃を受けた次ターン時に(パーティメンバー|パーティーメンバー|自身)のスキル発動率が\d\.*\d{0,2}倍になる"; pattern[24, 1] = "1";
            pattern[25, 0] = @"迎撃";pattern[25, 1] = "2";
            pattern[26, 0] = @"戦闘中、(パーティメンバー|パーティーメンバー|自身)のクリティカル攻撃発生率が\d{2}%上昇(?!上昇し)"; pattern[26, 1] = "1";
            pattern[27, 0] = @"戦闘中、(パーティメンバー|パーティーメンバー|自身)のクリティカルダメージが\d{2}%上昇$";
            pattern[28, 0] = @"スキル発動率を\d{2}%低下させる";pattern[28, 1] = "1";
            pattern[29, 0] = @"自身が攻撃を受けた次ターン時に(パーティ|パーティー)メンバーの攻撃力が\d\{2,3}%上昇"; pattern[29, 1] = "1";
            pattern[30, 0] = @"戦闘中、\w*(パーティメンバー|パーティーメンバー|自身)の与えるダメージを\d{2,3}%上昇";
            pattern[31, 0] = @"防御力を\d{2}%低下させる\w*?"; pattern[31, 1] = "1";
            pattern[32, 0] = @"ボス敵との戦闘中、(パーティメンバー|パーティーメンバー|自身)の攻撃力が\d{2}%上昇し、さらに自身の攻撃力が\d{2,3}%上昇";pattern[32, 1] = "2";
            pattern[33, 0] = @"戦闘中、(パーティ|パーティー)メンバーの攻撃力が\d{2}%上昇し、スキル発動率がそれぞれの好感度に応じて最大\d\.\d{1,2}倍上昇"; pattern[33, 1] = "2";
            pattern[34, 0] = @"戦闘中、(パーティ|パーティー)メンバーの弱点属性の敵に対するダメージが\d{2}%上昇する"; pattern[34, 1] = "1";

            for (int i = 0; i < pattern.GetLength(0);i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(ability, pattern[i, 0]))
                {
                    //アビ選択
                    SelectAbi(abiNo, i, ability);


                    //パターンに合わせて値の取得
                    switch (i)
                    {
                        case 21://属性付与
                            {
                                MatchCollection results = Regex.Matches(ability, @"(斬|打|突|魔)");
                                WriteSub(abiNo, "AV1", (results.Count / 2).ToString(),"属性付与");
                                return;
                            }

                        default:
                            {
                                MatchCollection results = Regex.Matches(ability, @"\d+\.*\d*(?!ターン|体)");
                                if (results.Count != 0)
                                {
                                    if (results[0].Value != null)
                                        WriteSub(abiNo, "AV1", results[0].Value.ToString(),"");
                                    if (results.Count >= 2)
                                    {
                                        if (pattern[i, 1] == "2")
                                        {
                                            if (results[1].Value != null)
                                                WriteSub(abiNo, "AV2", results[1].Value.ToString(),"");
                                        }
                                    }
                                }
                                    return;
                            }
                    }
                }
            }
        }

        protected void SelectAbi(int abiNo, int patternNo,string ability)
        {
            //選択項目確定
            string abiText = "";
            string ex2Text = "";
            string objNo = "5";//対象人数 
            switch (patternNo)
            {
                case 0:
                    {
                        abiText = "攻撃力上昇";
                        break;
                    }
                case 1:
                    {
                        abiText = "ダメ無効";
                        break;
                    }
                case 2:
                    {
                        abiText = "攻撃力低下";
                        //敵の数取得
                        MatchCollection results = Regex.Matches(ability, @"敵(?<敵数>\d)体");
                        if (results.Count != 0)
                        {
                            objNo = results[0].Groups["敵数"].Value;
                        }

                        break;
                    }
                case 3:
                    {
                        abiText = "スキル発動率上昇";
                        MatchCollection results = Regex.Matches(ability, @"自身の");
                        if (results.Count != 0)
                            objNo = "1";
                        break;
                    }
                case 4:
                    {
                        abiText = "ソラ効果上昇";
                        break;
                    }
                case 5:
                    {
                        abiText = "スキルダメ上昇";
                        break;
                    }
                case 6:
                    {
                        abiText = "対ボスダメ上昇";
                        break;
                    }
                case 7:
                    {
                        abiText = "ターン毎ダメージ上昇";
                        break;
                    }
                case 8:
                    {
                        abiText = "防御ダメ軽減率上昇";
                        break;
                    }
                case 9:
                    {
                        abiText = "反撃";
                        //超反撃取得
                        MatchCollection results = Regex.Matches(ability, @"超反撃");
                        if (results.Count != 0)
                        {
                            if (results[0].Value == "超反撃")
                                ex2Text = "超反撃";
                        }
                        objNo = "1";
                        break;
                    }
                case 10:
                    {
                        abiText = "回避";
                        objNo = "1";
                        break;
                    }
                case 11:
                    {
                        abiText = "自身が再行動";
                        objNo = "1";
                        break;
                    }
                case 12:
                    {
                        abiText = "HP回復";
                        break;
                    }
                case 13:
                    {
                        abiText = "光ゲージ充填";
                        break;
                    }
                case 14:
                    {
                        abiText = "クリ率クリダメ上昇";
                        //対象人数取得
                        MatchCollection results = Regex.Matches(ability, @"(パーティメンバー|パーティーメンバー|自身)");
                        if (results.Count != 0)
                        {
                            switch(results[0].Value)
                            {
                                case "自身":
                                    {
                                        objNo = "1";
                                        break;
                                    }
                            }
                        }
                        
                        break;
                    }
                case 15:
                    {
                        abiText = "対ボス攻撃力上昇";
                        break;
                    }
                //外れ枠。複合アビなので自分で入力
                case 16:
                    {
                        abiText = "対ボス攻撃力ダメ上昇";
                        break;
                    }
                case 17:
                    {
                        abiText = "スキルLVでスキル発動率上昇";
                        break;
                    }
                case 18:
                    {
                        abiText = "その他(MAP画面スキル)";
                        break;
                    }
                case 19:
                    {
                        abiText = "攻撃力上昇し自身がさらに上昇";
                        break;
                    }
                case 20:
                    {
                        abiText = "追撃";
                        //対象の取得
                        MatchCollection results = Regex.Matches(ability, @"(敵全体に|攻撃を与えた敵に|攻撃を行った敵に)追撃");
                        switch(results[0].Value)
                        {
                            case "攻撃を与えた敵に追撃":
                            case "攻撃を行った敵に追撃":
                                {
                                    ex2Text = "追撃1:単体";
                                    break;
                                }
                            case "敵全体に追撃":
                                {
                                    ex2Text = "追撃2:全体";
                                    break;
                                }
                        }
                        break;
                    }
                case 21:
                    {
                        abiText = "属性付与";
                        //属性取得
                        MatchCollection results = Regex.Matches(ability, @"(斬|打|突|魔)");
                        for (int i = 0; i < results.Count/2; i++)
                            ex2Text += results[i].Value;
                        objNo = "3";
                        break;
                    }
                case 22:
                case 33:
                    {
                        abiText = "攻撃力上昇し、スキル発動率上昇";
                        break;
                    }
                case 23:
                    {
                        abiText = "攻撃ミス";
                        //敵の数取得
                        MatchCollection results = Regex.Matches(ability, @"敵(?<敵数>\d)体");
                        if (results.Count != 0)
                        {
                            objNo = results[0].Groups["敵数"].Value;
                        }
                        break;
                    }
                case 24:
                    {
                        abiText = "自身が攻撃を受けた次Tにスキル発動率上昇";
                        break;
                    }
                case 25:
                    {
                        abiText = "迎撃";
                        objNo = "1";
                        break;
                    }
                case 26:
                    {
                        abiText = "クリ率上昇";
                        break;
                    }
                case 27:
                    {
                        abiText = "クリダメ上昇";
                        break;
                    }
                case 28:
                    {
                        abiText = "スキル発動率低下";
                        break;
                    }
                case 29:
                    {
                        abiText = "自身が攻撃を受けた次Tに攻撃力上昇";
                        break;
                    }
                case 30:
                    {
                        abiText = "ダメージ上昇";
                        break;
                    }
                case 31:
                    {
                        abiText = "防御力低下";
                        //敵の数取得
                        MatchCollection results = Regex.Matches(ability, @"敵(?<敵数>\d)体");
                        if (results.Count != 0)
                        {
                            objNo = results[0].Groups["敵数"].Value;
                        }

                        break;
                    }
                case 32:
                    {
                        abiText = "対ボス攻撃力上昇し、自身が更に上昇";
                        break;
                    }
                case 34:
                    {
                        abiText = "弱点属性の敵に対するダメージ増加";
                        break;
                    }
            }



            int index = 0;
            string turn = "";
            if (System.Text.RegularExpressions.Regex.IsMatch(ability, "1ターン目の|１ターン目の|1ターン目に|１ターン目に"))
                turn = "1ターン目";

            switch (abiNo)
            {
                case 0:
                    {
                        foreach (ListItem item in DropDownList50004.Items)
                        {
                            if (item.Text == abiText)
                            {
                                DropDownList50004.SelectedIndex = index;
                                break;
                            }
                            index++;
                        }
                        if (ex2Text != "")
                        {
                            index = 0;
                            foreach (ListItem ex2Item in DropDownList50021.Items)
                            {
                                if (ex2Item.Text == ex2Text)
                                {
                                    DropDownList50021.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //対象人数
                        if(objNo != "全")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50003.Items)
                            {
                                if (item.Text == objNo)
                                {
                                    DropDownList50003.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //ターン
                        if (turn != "")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50002.Items)
                            {
                                if (item.Text == turn)
                                {
                                    DropDownList50002.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        foreach (ListItem item in DropDownList50009.Items)
                        {
                            if (item.Text == abiText)
                            {
                                DropDownList50009.SelectedIndex = index;
                                break;
                            }
                            index++;
                        }
                        if (ex2Text != "")
                        {
                            index = 0;
                            foreach (ListItem ex2Item in DropDownList50022.Items)
                            {
                                if (ex2Item.Text == ex2Text)
                                {
                                    DropDownList50022.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //対象人数
                        if (objNo != "5")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50008.Items)
                            {
                                if (item.Text == objNo)
                                {
                                    DropDownList50008.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //ターン
                        if (turn != "")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50007.Items)
                            {
                                if (item.Text == turn)
                                {
                                    DropDownList50007.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (ListItem item in DropDownList50012.Items)
                        {
                            if (item.Text == abiText)
                            {
                                DropDownList50012.SelectedIndex = index;
                                break;
                            }
                            index++;
                        }
                        if (ex2Text != "")
                        {
                            index = 0;
                            foreach (ListItem ex2Item in DropDownList50023.Items)
                            {
                                if (ex2Item.Text == ex2Text)
                                {
                                    DropDownList50023.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //対象人数
                        if (objNo != "5")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50011.Items)
                            {
                                if (item.Text == objNo)
                                {
                                    DropDownList50011.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //ターン
                        if (turn != "")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50010.Items)
                            {
                                if (item.Text == turn)
                                {
                                    DropDownList50010.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (ListItem item in DropDownList50015.Items)
                        {
                            if (item.Text == abiText)
                            {
                                DropDownList50015.SelectedIndex = index;
                                break;
                            }
                            index++;
                        }
                        if (ex2Text != "")
                        {
                            index = 0;
                            foreach (ListItem ex2Item in DropDownList50024.Items)
                            {
                                if (ex2Item.Text == ex2Text)
                                {
                                    DropDownList50024.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //対象人数
                        if (objNo != "5")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50014.Items)
                            {
                                if (item.Text == objNo)
                                {
                                    DropDownList50014.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        //ターン
                        if (turn != "")
                        {
                            index = 0;
                            foreach (ListItem item in DropDownList50013.Items)
                            {
                                if (item.Text == turn)
                                {
                                    DropDownList50013.SelectedIndex = index;
                                    break;
                                }
                                index++;
                            }
                        }
                        break;
                    }
            }

            return;
        }


        protected void WriteSub(int abiNo,string type,string value, string abiType)
        {
            switch(value)
            {
                case "1.2":
                    {
                        value = "20";
                        break;
                    }
                case "1.65":
                    {
                        value = "65";
                        break;
                    }
                case "2":
                    {
                        if(abiType != "属性付与")
                            value = "100";
                        break;
                    }
            }


            switch(type)
            {
                case "AV1":
                    {
                        switch(abiNo)
                        {
                            case 0:
                                {
                                    this.TextBox50008.Text = value;
                                    break;
                                }
                            case 1:
                                {
                                    this.TextBox50010.Text = value;
                                    break;
                                }
                            case 2:
                                {
                                    this.TextBox50012.Text = value;
                                    break;
                                }
                            case 3:
                                {
                                    this.TextBox50014.Text = value;
                                    break;
                                }
                        }
                        break;
                    }
                case "AV2":
                    {
                        switch (abiNo)
                        {
                            case 0:
                                {
                                    this.TextBox50009.Text = value;
                                    break;
                                }
                            case 1:
                                {
                                    this.TextBox50011.Text = value;
                                    break;
                                }
                            case 2:
                                {
                                    this.TextBox50013.Text = value;
                                    break;
                                }
                            case 3:
                                {
                                    this.TextBox50015.Text = value;
                                    break;
                                }
                        }
                        break;
                    }

            }
            return;
        }

    }

        
}