using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

public partial class 花騎士登録フォーム : System.Web.UI.Page
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
        Button3.Click += new EventHandler(this.Button2_Click);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //入力クリア処理
        this.TextBox1.Text = "";
        this.TextBox2.Text = "";
        this.DropDownList1.SelectedIndex = 0;
        this.DropDownList5.SelectedIndex = 0;
        this.DropDownList6.SelectedIndex = 0;

        this.TextBox3.Text = "";
        this.TextBox4.Text = "";
        this.TextBox5.Text = "";
        this.TextBox6.Text = "";

        this.TextBox7.Text = "";
        this.TextBox16.Text = "";
        this.DropDownList16.SelectedIndex = 0;

        this.DropDownList17.SelectedIndex = 0;
        this.TextBox17.Text = "";

        this.DropDownList2.SelectedIndex = 0;
        this.DropDownList3.SelectedIndex = 0;
        this.DropDownList4.SelectedIndex = 0;
        this.TextBox8.Text = "";
        this.TextBox9.Text = "";
        this.DropDownList21.SelectedIndex = 0;

        this.DropDownList7.SelectedIndex = 0;
        this.DropDownList8.SelectedIndex = 0;
        this.DropDownList9.SelectedIndex = 0;
        this.TextBox10.Text = "";
        this.TextBox11.Text = "";
        this.DropDownList22.SelectedIndex = 0;

        this.DropDownList10.SelectedIndex = 0;
        this.DropDownList11.SelectedIndex = 0;
        this.DropDownList12.SelectedIndex = 0;
        this.TextBox12.Text = "";
        this.TextBox13.Text = "";
        this.DropDownList23.SelectedIndex = 0;

        this.DropDownList13.SelectedIndex = 0;
        this.DropDownList14.SelectedIndex = 0;
        this.DropDownList15.SelectedIndex = 0;
        this.TextBox14.Text = "";
        this.TextBox15.Text = "";
        this.DropDownList24.SelectedIndex = 0;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (!CheckBox1.Checked)
        {
            //
            //通常登録処理
            //

            //特殊処理 防御選択時、小数がＤＢ内に入力されるため、値を十倍して入力する
            double diffValue = 0;
            string diff = "防御ダメ軽減率上昇";
            if ((this.DropDownList4.Text == diff) | (this.DropDownList9.Text == diff) | (this.DropDownList12.Text == diff) | (this.DropDownList15.Text == diff))
            {
                if (this.DropDownList4.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox9.Text);
                    diffValue *= 10;
                    TextBox9.Text = diffValue.ToString();
                }

                if (this.DropDownList9.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox11.Text);
                    diffValue *= 10;
                    TextBox11.Text = diffValue.ToString();
                }

                if (this.DropDownList12.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox13.Text);
                    diffValue *= 10;
                    TextBox13.Text = diffValue.ToString();
                }

                if (this.DropDownList15.Text == diff)
                {
                    diffValue = Convert.ToDouble(TextBox15.Text);
                    diffValue *= 10;
                    TextBox15.Text = diffValue.ToString();
                }
            }

            //特殊処理2 反撃選択時、小数がＤＢ内に入力されるため、値を百倍して入力する
            double diffValue2 = 0;
            string diff2 = "反撃";
            if ((this.DropDownList4.Text == diff2) | (this.DropDownList9.Text == diff2) | (this.DropDownList12.Text == diff2) | (this.DropDownList15.Text == diff2))
            {
                if (this.DropDownList4.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox9.Text);
                    diffValue2 *= 100;
                    TextBox9.Text = diffValue2.ToString();
                }

                if (this.DropDownList9.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox11.Text);
                    diffValue2 *= 100;
                    TextBox11.Text = diffValue2.ToString();
                }

                if (this.DropDownList12.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox13.Text);
                    diffValue2 *= 100;
                    TextBox13.Text = diffValue2.ToString();
                }

                if (this.DropDownList15.Text == diff2)
                {
                    diffValue2 = Convert.ToDouble(TextBox15.Text);
                    diffValue2 *= 100;
                    TextBox15.Text = diffValue2.ToString();
                }
            }

            //特殊処理 スキル倍率が小数で入力されるため、10倍して格納する
            double skillRatio = 0;
            skillRatio = Convert.ToDouble(TextBox17.Text);
            skillRatio *= 10;
            TextBox17.Text = skillRatio.ToString();

            //登録処理
            try
            {
                //入力されたIdの行があるか確認する処理
                cn1.ConnectionString = cnstr;
                cn1.Open();
                cmd1.Connection = cn1;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT Id FROM [dbo].[Fkgmbr] WHERE Id = '" + TextBox1.Text + "'";
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
                                   "'" + TextBox1.Text + "'," + "N'" + TextBox2.Text + "'," +
                                   "'" + DropDownList1.Text + "'," + "N'" + DropDownList5.Text + "'," + "N'" + DropDownList6.Text + "'," +
                                   "'" + TextBox3.Text + "'," + "'" + TextBox4.Text + "'," + "'" + TextBox5.Text + "'," + "'" + TextBox6.Text + "'," +
                                   "'" + TextBox7.Text + "'," + "'" + TextBox16.Text + "'," + "'" + DropDownList16.Text + "'," +
                                   "N'" + DropDownList17.Text + "'," + "'" + TextBox17.Text + "'," +
                                   "'" + DropDownList2.SelectedValue + "'," + "'" + DropDownList3.SelectedValue + "'," + "N'" + DropDownList4.Text + "'," + "'" + TextBox8.Text + "'," + "'" + TextBox9.Text + "'," + "N'" + DropDownList21.Text + "'," +
                                   "'" + DropDownList7.SelectedValue + "'," + "'" + DropDownList8.SelectedValue + "'," + "N'" + DropDownList9.Text + "'," + "'" + TextBox10.Text + "'," + "'" + TextBox11.Text + "'," + "N'" + DropDownList22.Text + "'," +
                                   "'" + DropDownList10.SelectedValue + "'," + "'" + DropDownList11.SelectedValue + "'," + "N'" + DropDownList12.Text + "'," + "'" + TextBox12.Text + "'," + "'" + TextBox13.Text + "'," + "N'" + DropDownList23.Text + "'," +
                                   "'" + DropDownList13.SelectedValue + "'," + "'" + DropDownList14.SelectedValue + "'," + "N'" + DropDownList15.Text + "'," + "'" + TextBox14.Text + "'," + "'" + TextBox15.Text + "'," + "N'" + DropDownList24.Text + "'," +
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
        else if(CheckBox1.Checked)
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
                cmd1.CommandText = "SELECT Id FROM [dbo].[Fkgmbr] WHERE Id = '" + TextBox1.Text + "'";
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

                //編集処理
                cn.ConnectionString = cnstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;                
                cmd.CommandText = "UPDATE [dbo].[Fkgmbr] " +
                                   "SET Id = '" + TextBox1.Text + "',Name = N'" + TextBox2.Text +
                                   "', Rarity = '" + DropDownList1.Text + "', ATT = N'" + DropDownList5.Text + "', Unit = N'" + DropDownList6.Text +
                                   "', HP = '" + TextBox3.Text + "', ATK = '" + TextBox4.Text + "', DEF = '" + TextBox5.Text + "', MOV = '" + TextBox6.Text +
                                   "', STP = '" + TextBox7.Text + "', STPMax = '" + TextBox16.Text + "', ANum = '" + DropDownList16.Text +
                                   "', SType = N'" + DropDownList17.Text + "', SRatio = '" + TextBox17.Text +
                                   "', A1st1 = '" + DropDownList2.SelectedValue + "', A1NO = '" + DropDownList3.SelectedValue + "', A1Ex1 = N'" + DropDownList4.Text + "', A1V1 = '" + TextBox8.Text + "', A1V2 = '" + TextBox9.Text + "', A1Ex2 = N'" + DropDownList21.Text +
                                   "', A2st1 = '" + DropDownList7.SelectedValue + "', A2NO = '" + DropDownList8.SelectedValue + "', A2Ex1 = N'" + DropDownList9.Text + "', A2V1 = '" + TextBox10.Text + "', A2V2 = '" + TextBox11.Text + "', A2Ex2 = N'" + DropDownList22.Text +
                                   "', A3st1 = '" + DropDownList10.SelectedValue + "', A3NO = '" + DropDownList11.SelectedValue + "', A3Ex1 = N'" + DropDownList12.Text + "', A3V1 = '" + TextBox12.Text + "', A3V2 = '" + TextBox13.Text + "', A3Ex2 = N'" + DropDownList23.Text +
                                   "', A4st1 = '" + DropDownList13.SelectedValue + "', A4NO = '" + DropDownList14.SelectedValue + "', A4Ex1 = N'" + DropDownList15.Text + "', A4V1 = '" + TextBox14.Text + "', A4V2 = '" + TextBox15.Text + "', A4Ex2 = N'" + DropDownList24.Text +
                                   "', Date = '" + DateTime.Now + "' " +
                                   "WHERE Id = '" + TextBox1.Text + "'";
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
        string script ="<script language=javascript>" +"window.alert('処理終了')" +"</script>";
        Response.Write(script);

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //ダミー
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
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
        if(ds.Tables[0].Rows.Count == 0)
        {
            return null;
        }

        return ds;

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //データ呼出しボタン
        //string partQuery = "Id = " + Microsoft.VisualBasic.Strings.StrConv(TextBox1.Text, VbStrConv.Narrow);
        //TextBox1.Text = Microsoft.VisualBasic.Strings.StrConv(TextBox1.Text, VbStrConv.Narrow);

        string partQuery = "Id = " + TextBox1.Text;

        if (TextBox1.Text == "")
        {
            partQuery = "Name = N'" + TextBox2.Text + "'";
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

        if (TextBox1.Text != "")
        {
            TextBox2.Text = dt_row["Name"].ToString();
        }
        else
        {
            TextBox1.Text = dt_row["Id"].ToString();
        }


        DropDownList1.Text = dt_row["Rarity"].ToString();
        DropDownList5.Text = dt_row["ATT"].ToString();
        DropDownList6.Text = dt_row["Unit"].ToString();

        TextBox3.Text = dt_row["HP"].ToString();
        TextBox4.Text = dt_row["ATK"].ToString();
        TextBox5.Text = dt_row["DEF"].ToString();
        TextBox6.Text = dt_row["MOV"].ToString();

        TextBox7.Text = dt_row["STP"].ToString();
        TextBox16.Text = dt_row["STPMax"].ToString();
        DropDownList16.Text = dt_row["Anum"].ToString();

        DropDownList17.Text = dt_row["SType"].ToString();
        TextBox17.Text = dt_row["SRatio"].ToString();

        DropDownList2.Text = dt_row["A1st1"].ToString();
        DropDownList3.Text = dt_row["A1NO"].ToString();
        DropDownList4.Text = dt_row["A1Ex1"].ToString();
        TextBox8.Text = dt_row["A1V1"].ToString();
        TextBox9.Text = dt_row["A1V2"].ToString();
        DropDownList21.Text = dt_row["A1Ex2"].ToString();

        DropDownList7.Text = dt_row["A2st1"].ToString();
        DropDownList8.Text = dt_row["A2NO"].ToString();
        DropDownList9.Text = dt_row["A2Ex1"].ToString();
        TextBox10.Text = dt_row["A2V1"].ToString();
        TextBox11.Text = dt_row["A2V2"].ToString();
        DropDownList22.Text = dt_row["A2Ex2"].ToString();

        DropDownList10.Text = dt_row["A3st1"].ToString();
        DropDownList11.Text = dt_row["A3NO"].ToString();
        DropDownList12.Text = dt_row["A3Ex1"].ToString();
        TextBox12.Text = dt_row["A3V1"].ToString();
        TextBox13.Text = dt_row["A3V2"].ToString();
        DropDownList23.Text = dt_row["A3Ex2"].ToString();

        DropDownList13.Text = dt_row["A4st1"].ToString();
        DropDownList14.Text = dt_row["A4NO"].ToString();
        DropDownList15.Text = dt_row["A4Ex1"].ToString();
        TextBox14.Text = dt_row["A4V1"].ToString();
        TextBox15.Text = dt_row["A4V2"].ToString();
        DropDownList24.Text = dt_row["A4Ex2"].ToString();

    }
}