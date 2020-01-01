using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

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
                if ((this.DropDownList50004.Text == diff) | (this.DropDownList50009.Text == diff) | (this.DropDownList50012.Text == diff) | (this.DropDownList50015.Text == diff))
                {
                    if (this.DropDownList50004.Text == diff)
                    {
                        diffValue = Convert.ToDouble(TextBox50009.Text);
                        diffValue *= 10;
                        TextBox50009.Text = diffValue.ToString();
                    }

                    if (this.DropDownList50009.Text == diff)
                    {
                        diffValue = Convert.ToDouble(TextBox50011.Text);
                        diffValue *= 10;
                        TextBox50011.Text = diffValue.ToString();
                    }

                    if (this.DropDownList50012.Text == diff)
                    {
                        diffValue = Convert.ToDouble(TextBox50013.Text);
                        diffValue *= 10;
                        TextBox50013.Text = diffValue.ToString();
                    }

                    if (this.DropDownList50015.Text == diff)
                    {
                        diffValue = Convert.ToDouble(TextBox50015.Text);
                        diffValue *= 10;
                        TextBox50015.Text = diffValue.ToString();
                    }
                }

                //特殊処理2 反撃選択時、小数がＤＢ内に入力されるため、値を百倍して入力する
                double diffValue2 = 0;
                string diff2 = "反撃";
                if ((this.DropDownList50004.Text == diff2) | (this.DropDownList50009.Text == diff2) | (this.DropDownList50012.Text == diff2) | (this.DropDownList50015.Text == diff2))
                {
                    if (this.DropDownList50004.Text == diff2)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50009.Text);
                        diffValue2 *= 100;
                        TextBox50009.Text = diffValue2.ToString();
                    }

                    if (this.DropDownList50009.Text == diff2)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50011.Text);
                        diffValue2 *= 100;
                        TextBox50011.Text = diffValue2.ToString();
                    }

                    if (this.DropDownList50012.Text == diff2)
                    {
                        diffValue2 = Convert.ToDouble(TextBox50013.Text);
                        diffValue2 *= 100;
                        TextBox50013.Text = diffValue2.ToString();
                    }

                    if (this.DropDownList50015.Text == diff2)
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
                    cmd.CommandText = "INSERT INTO [dbo].[Fkgmbr] (Id, Name, Rarity, ATT, Unit, HP, ATK, DEF, MOV, STP, STPMax, ANum, SType, SRatio, A1st1, A1NO, A1Ex1, A1V1, A1V2, A1Ex2, A2st1, A2NO, A2Ex1, A2V1, A2V2, A2Ex2, A3st1, A3NO, A3Ex1, A3V1, A3V2, A3Ex2, A4st1, A4NO, A4Ex1, A4V1, A4V2, A4Ex2, Date) VALUES(" +
                                       "'" + TextBox50001.Text + "'," + "N'" + TextBox50002.Text + "'," +
                                       "'" + DropDownList50001.Text + "'," + "N'" + DropDownList50005.Text + "'," + "N'" + DropDownList50006.Text + "'," +
                                       "'" + TextBox50003.Text + "'," + "'" + TextBox50004.Text + "'," + "'" + TextBox50005.Text + "'," + "'" + TextBox50006.Text + "'," +
                                       "'" + TextBox50007.Text + "'," + "'" + TextBox50016.Text + "'," + "'" + DropDownList50016.Text + "'," +
                                       "N'" + DropDownList50017.Text + "'," + "'" + TextBox50017.Text + "'," +
                                       "'" + DropDownList50002.SelectedValue + "'," + "'" + DropDownList50003.SelectedValue + "'," + "N'" + DropDownList50004.Text + "'," + "'" + TextBox50008.Text + "'," + "'" + TextBox50009.Text + "'," + "N'" + DropDownList50021.Text + "'," +
                                       "'" + DropDownList50007.SelectedValue + "'," + "'" + DropDownList50008.SelectedValue + "'," + "N'" + DropDownList50009.Text + "'," + "'" + TextBox50010.Text + "'," + "'" + TextBox50011.Text + "'," + "N'" + DropDownList50022.Text + "'," +
                                       "'" + DropDownList50010.SelectedValue + "'," + "'" + DropDownList50011.SelectedValue + "'," + "N'" + DropDownList50012.Text + "'," + "'" + TextBox50012.Text + "'," + "'" + TextBox50013.Text + "'," + "N'" + DropDownList50023.Text + "'," +
                                       "'" + DropDownList50013.SelectedValue + "'," + "'" + DropDownList50014.SelectedValue + "'," + "N'" + DropDownList50015.Text + "'," + "'" + TextBox50014.Text + "'," + "'" + TextBox50015.Text + "'," + "N'" + DropDownList50024.Text + "'," +
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
                    string text50009 = TextBox50009.Text;
                    string text50011 = TextBox50011.Text;
                    string text50013 = TextBox50013.Text;
                    string text50015 = TextBox50015.Text;

                    if ((this.DropDownList50004.Text == diff) | (this.DropDownList50009.Text == diff) | (this.DropDownList50012.Text == diff) | (this.DropDownList50015.Text == diff))
                    {
                        if (this.DropDownList50004.Text == diff)
                        {
                            diffValue = Convert.ToDouble(text50009);
                            diffValue *= 10;
                            text50009 = diffValue.ToString();
                        }

                        if (this.DropDownList50009.Text == diff)
                        {
                            diffValue = Convert.ToDouble(text50011);
                            diffValue *= 10;
                            text50011 = diffValue.ToString();
                        }

                        if (this.DropDownList50012.Text == diff)
                        {
                            diffValue = Convert.ToDouble(text50013);
                            diffValue *= 10;
                            text50013 = diffValue.ToString();
                        }

                        if (this.DropDownList50015.Text == diff)
                        {
                            diffValue = Convert.ToDouble(text50015);
                            diffValue *= 10;
                            text50015 = diffValue.ToString();
                        }
                    }

                    //特殊処理2 反撃選択時、小数がＤＢ内に入力されるため、値を百倍して入力する
                    double diffValue2 = 0;
                    string diff2 = "反撃";
                    if ((this.DropDownList50004.Text == diff2) | (this.DropDownList50009.Text == diff2) | (this.DropDownList50012.Text == diff2) | (this.DropDownList50015.Text == diff2))
                    {
                        if (this.DropDownList50004.Text == diff2)
                        {
                            diffValue2 = Convert.ToDouble(text50009);
                            diffValue2 *= 100;
                            text50009 = diffValue2.ToString();
                        }

                        if (this.DropDownList50009.Text == diff2)
                        {
                            diffValue2 = Convert.ToDouble(text50011);
                            diffValue2 *= 100;
                            text50011 = diffValue2.ToString();
                        }

                        if (this.DropDownList50012.Text == diff2)
                        {
                            diffValue2 = Convert.ToDouble(text50013);
                            diffValue2 *= 100;
                            text50013 = diffValue2.ToString();
                        }

                        if (this.DropDownList50015.Text == diff2)
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
                                       "', A1st1 = '" + DropDownList50002.SelectedValue + "', A1NO = '" + DropDownList50003.SelectedValue + "', A1Ex1 = N'" + DropDownList50004.Text + "', A1V1 = '" + TextBox50008.Text + "', A1V2 = '" + text50009 + "', A1Ex2 = N'" + DropDownList50021.Text +
                                       "', A2st1 = '" + DropDownList50007.SelectedValue + "', A2NO = '" + DropDownList50008.SelectedValue + "', A2Ex1 = N'" + DropDownList50009.Text + "', A2V1 = '" + TextBox50010.Text + "', A2V2 = '" + text50011 + "', A2Ex2 = N'" + DropDownList50022.Text +
                                       "', A3st1 = '" + DropDownList50010.SelectedValue + "', A3NO = '" + DropDownList50011.SelectedValue + "', A3Ex1 = N'" + DropDownList50012.Text + "', A3V1 = '" + TextBox50012.Text + "', A3V2 = '" + text50013 + "', A3Ex2 = N'" + DropDownList50023.Text +
                                       "', A4st1 = '" + DropDownList50013.SelectedValue + "', A4NO = '" + DropDownList50014.SelectedValue + "', A4Ex1 = N'" + DropDownList50015.Text + "', A4V1 = '" + TextBox50014.Text + "', A4V2 = '" + text50015 + "', A4Ex2 = N'" + DropDownList50024.Text +
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
            get_query = "SELECT Id, Name, Rarity, ATT, Unit, HP, ATK, DEF, MOV, STP, STPMax, SType, SRatio, ANum, A1st1, A1NO, A1Ex1, A1V1, A1V2, A1Ex2, A2st1, A2NO, A2Ex1, A2V1, A2V2, A2Ex2, A3st1, A3NO, A3Ex1, A3V1, A3V2, A3Ex2, A4st1, A4NO, A4Ex1, A4V1, A4V2, A4Ex2 " +
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
            DropDownList50004.Text = dt_row["A1Ex1"].ToString();
            TextBox50008.Text = dt_row["A1V1"].ToString();
            TextBox50009.Text = dt_row["A1V2"].ToString();
            DropDownList50021.Text = dt_row["A1Ex2"].ToString();

            DropDownList50007.Text = dt_row["A2st1"].ToString();
            DropDownList50008.Text = dt_row["A2NO"].ToString();
            DropDownList50009.Text = dt_row["A2Ex1"].ToString();
            TextBox50010.Text = dt_row["A2V1"].ToString();
            TextBox50011.Text = dt_row["A2V2"].ToString();
            DropDownList50022.Text = dt_row["A2Ex2"].ToString();

            DropDownList50010.Text = dt_row["A3st1"].ToString();
            DropDownList50011.Text = dt_row["A3NO"].ToString();
            DropDownList50012.Text = dt_row["A3Ex1"].ToString();
            TextBox50012.Text = dt_row["A3V1"].ToString();
            TextBox50013.Text = dt_row["A3V2"].ToString();
            DropDownList50023.Text = dt_row["A3Ex2"].ToString();

            DropDownList50013.Text = dt_row["A4st1"].ToString();
            DropDownList50014.Text = dt_row["A4NO"].ToString();
            DropDownList50015.Text = dt_row["A4Ex1"].ToString();
            TextBox50014.Text = dt_row["A4V1"].ToString();
            TextBox50015.Text = dt_row["A4V2"].ToString();
            DropDownList50024.Text = dt_row["A4Ex2"].ToString();

            //反撃、防御の時の値を小数に変換する処理追加
            //特殊処理 防御選択時、ＤＢ内では小数ではないので、値を1/10にして入力する
            double diffValue = 0;
            string diff = "防御ダメ軽減率上昇";
            if ((this.DropDownList50004.Text == diff) | (this.DropDownList50009.Text == diff) | (this.DropDownList50012.Text == diff) | (this.DropDownList50015.Text == diff))
            {
                if (this.DropDownList50004.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox50009.Text);
                    diffValue /= 10;
                    TextBox50009.Text = diffValue.ToString();
                }

                if (this.DropDownList50009.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox50011.Text);
                    diffValue /= 10;
                    TextBox50011.Text = diffValue.ToString();
                }

                if (this.DropDownList50012.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox50013.Text);
                    diffValue /= 10;
                    TextBox50013.Text = diffValue.ToString();
                }

                if (this.DropDownList50015.Text == diff)
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
    }
}