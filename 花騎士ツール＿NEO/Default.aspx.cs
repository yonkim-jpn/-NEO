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
    public partial class _Default : Page
    {
        private SqlConnection cn = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader rd;
        private string cnstr =
            @"Server=tcp:fkg-data-yonkim.database.windows.net,1433;Initial Catalog=花騎士データ;Persist Security Info=False;User ID=yonkim;Password=Hornet600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



        protected void Page_Load(object sender, EventArgs e)
        {
            Check_DB();
        }

        protected void Check_DB()
        {
            string list_querry = "SELECT Id, Name, Date FROM [dbo].[Fkgmbr]";

            //GetDataによりdataset入手する
            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);
            DataTable dt_out = ds_fkg.Tables[0];

            
            //レコードのソートを行う
            DataView view = new DataView(dt_out);
            view.Sort = "Date DESC";
            DataTable dt_out2 = view.ToTable();
            DateTime diaRenova = (DateTime)dt_out2.Rows[0]["Date"];
            string diaRenovaStr = diaRenova.ToString("yyyy/MM/dd");

            //花騎士名記述用str
            string nombreFkg = "";
            int countFkg = 0;

            //最終更新日と同日のみを抜き出す処理
            for (int i = 0; i < dt_out2.Rows.Count; i++)
            {
                DateTime dia = (DateTime)dt_out2.Rows[i]["Date"];
                string diaStr = dia.ToString("yyyy/MM/dd");
                //最終更新日と日付を比較
                switch (diaStr.CompareTo(diaRenovaStr))
                {
                    case -1://前の日時の場合はループを抜ける
                    case 1://後の日時の場合　DBは日時で降順にしてあるので、ここには来ない
                        goto ExitLoop;
                    case 0://同日の場合
                        {
                            if(countFkg > 0)
                            {
                                nombreFkg += " ,　";
                            }
                            nombreFkg += dt_out2.Rows[i]["NAME"];
                            countFkg++;
                            break;
                        }

                }
            }
            ExitLoop: ;

            //花騎士の登録人数を書き出し
            Label1001.Text = "データ最終更新日　" + diaRenova.ToString("yyyy/MM/dd");
            Label1002.Text = "更新した花騎士名　" + nombreFkg;
            Label1003.Text = "現在のDB登録花騎士数　" + dt_out.Rows.Count.ToString() + "人";
        }

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
    }
}