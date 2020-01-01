using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Ability_search : System.Web.UI.Page
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



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*
        string inputStr = DropDownList1.SelectedValue;
        if (inputStr == "ソーラードライブ効果上昇")
        {
            TextBox1.Enabled = true;
        }
        else
        {
            TextBox1.Enabled = false;
            TextBox1.Text = "";
        }
        */
    }

    

    //
    //メイン計算処理
    //
    protected void Button1_Click(object sender, EventArgs e)
    {
        //クエリ作成
        string get_query = "";
        get_query = "SELECT Id,Name,Rarity,ATT,A1st1,A1NO,A1Ex1,A1V1,A1V2,A1Ex2,A2st1,A2NO,A2Ex1,A2V1,A2V2,A2Ex2,A3st1,A3NO,A3Ex1,A3V1,A3V2,A3Ex2,A4st1,A4NO,A4Ex1,A4V1,A4V2,A4Ex2,SType,SRatio " +
            "FROM [dbo].[Fkgmbr]";
        DataSet ds_fkg = new DataSet();
        ds_fkg = GetData(get_query);
        DataTable dt_fkg0 = new DataTable();
        DataTable dt_fkg = new DataTable();

        //レアリティフィルタ処理
        dt_fkg0 = GetDataFilterRare(ds_fkg.Tables[0]);
        //スキルタイプフィルタ処理
        dt_fkg0 = GetDataFilterSType(ds_fkg.Tables[0]);
        //データテーブルが空の場合
        if (dt_fkg0 == null)
        {
            //メッセージボックス表示
            string script1 = "<script language=javascript>" + "window.alert('該当する花騎士を見つけることが出来ませんでした。処理を終了します')" + "</script>";
            Response.Write(script1);
            return;
        }

        //属性フィルタ処理
        dt_fkg = GetDataFilterAtt(dt_fkg0);
        //データテーブルが空の場合
        if (dt_fkg == null)
        {
            //メッセージボックス表示
            string script1 = "<script language=javascript>" + "window.alert('該当する花騎士を見つけることが出来ませんでした。処理を終了します')" + "</script>";
            Response.Write(script1);
            return;
        }

        //データテーブルが空の場合
        if (dt_fkg == null)
        {
            //メッセージボックス表示
            string script1 = "<script language=javascript>" + "window.alert('該当する花騎士を見つけることが出来ませんでした。処理を終了します')" + "</script>";
            Response.Write(script1);
            return;
        }

        //LINQ用クエリ生成ルーチン
        //　選択された値からWHERE句のパーツ生成

        string[] requestWord = new string[3];
        string[] fieldStr = new string[3];
        int[] selectValue = new int[3];
        int[] inputValue = new int[3];


        requestWord[0] = this.DropDownList1.SelectedValue;
        requestWord[1] = this.DropDownList2.SelectedValue;
        requestWord[2] = this.DropDownList3.SelectedValue;

        //入力値判定処理
        if (TextBox1.Text != "")
        {
            try
            {
                inputValue[0] = Convert.ToInt32(TextBox1.Text);
                if(inputValue[0] < 0)
                {
                    //メッセージボックス表示
                    string script1 = "<script language=javascript>" + "window.alert('0より小さい数値は入力できません。数値を確認して下さい。処理を終了します')" + "</script>";
                    Response.Write(script1);
                    return;
                }
            }
            catch
            {
                //メッセージボックス表示
                string script1 = "<script language=javascript>" + "window.alert('入力された数値が処理できません。数値を確認して下さい。処理を終了します')" + "</script>";
                Response.Write(script1);
                return;
            }
        }

        if (TextBox2.Text != "")
        {
            try
            {
                inputValue[1] = Convert.ToInt32(TextBox2.Text);
                if (inputValue[1] < 0)
                {
                    //メッセージボックス表示
                    string script1 = "<script language=javascript>" + "window.alert('0より小さい数値は入力できません。数値を確認して下さい。処理を終了します')" + "</script>";
                    Response.Write(script1);
                    return;
                }
            }
            catch
            {
                //メッセージボックス表示
                string script1 = "<script language=javascript>" + "window.alert('入力された数値が処理できません。数値を確認して下さい。処理を終了します')" + "</script>";
                Response.Write(script1);
                return;
            }
        }

        if (TextBox3.Text != "")
        {
            try
            {
                inputValue[2] = Convert.ToInt32(TextBox3.Text);
                if (inputValue[2] < 0)
                {
                    //メッセージボックス表示
                    string script1 = "<script language=javascript>" + "window.alert('0より小さい数値は入力できません。数値を確認して下さい。処理を終了します')" + "</script>";
                    Response.Write(script1);
                    return;
                }
            }
            catch
            {
                //メッセージボックス表示
                string script1 = "<script language=javascript>" + "window.alert('入力された数値が処理できません。数値を確認して下さい。処理を終了します')" + "</script>";
                Response.Write(script1);
                return;
            }
        }

        //

        //未選択の場合の処理
        if ((requestWord[0]=="選択無し")&& (requestWord[1] == "選択無し")&& (requestWord[2] == "選択無し"))
        {
            //メッセージボックス表示
            string script1 = "<script language=javascript>" + "window.alert('アビリティを選択して下さい。処理を終了します')" + "</script>";
            Response.Write(script1);
            return;
        }

        if ((CheckBox9.Checked == false) && (CheckBox10.Checked == false) && (CheckBox11.Checked == false) && (CheckBox12.Checked == false) && (CheckBox13.Checked == false) && (CheckBox14.Checked == false))
        {
            //メッセージボックス表示
            string script1 = "<script language=javascript>" + "window.alert('少なくとも一つはスキルタイプにチェック入れて下さい。処理を終了します')" + "</script>";
            Response.Write(script1);
            return;
        }


        //ストリング分析してパーツ生成
        for (int i = 0; i < 3; i++)
        {
            switch (requestWord[i])
            {
                case "選択無し":
                    {
                        fieldStr[i] = "";
                        break;
                    }
                case "スキル発動率1.2倍上昇"://数値と比較
                    {
                        fieldStr[i] = "スキル発動率上昇";
                        selectValue[i] = 20;
                        break;
                    }
                case "スキル発動率1.65倍上昇"://数値と比較
                    {
                        fieldStr[i] = "スキル発動率上昇";
                        selectValue[i] = 65;
                        break;
                    }
                case "スキル発動率上昇":
                    //"スキルLVでスキル発動率上昇"
                    {
                        fieldStr[i] = "スキル発動率上昇";
                        break;
                    }
                case "スキルダメージ増加":
                    {
                        fieldStr[i] = "スキルダメ上昇";
                        break;
                    }
                case "クリティカル率上昇"://クリタエちゃん問題ある
                    //"クリ率クリダメ上昇"
                    {
                        fieldStr[i] = "クリ率上昇";
                        break;
                    }
                case "クリティカルダメージ増加"://クリタエちゃん問題ある
                    //"クリ率クリダメ上昇"
                    {
                        fieldStr[i] = "クリダメ上昇";
                        break;
                    }
                case "攻撃力上昇"://いっぱいあるからそれを全部引っ掛けるかどうか、数値入力もどうするか
                    //DB登録種類列記
                    //"敵の数で攻撃力上昇"
                    //"攻撃力上昇HP割合ダメ上昇"
                    //"攻撃力上昇し自身がさらに上昇"
                    //"スキル発動毎に攻撃力上昇"fkg_selectでのコーディングはなし
                    //
                    {
                        fieldStr[i] = "攻撃力上昇";
                        break;
                    }
                case "ターン毎攻撃力上昇":
                    //パターン何個かあるけど
                    //"攻撃力上昇1T目さらに上昇"
                    //
                    {
                        fieldStr[i] = "ターンで攻撃力上昇";
                        break;
                    }
                case "ボスに与えるダメージ増加"://ダメ攻撃両方上昇するのはどう処理する？
                    // "対ボス攻撃力ダメ上昇"
                    {
                        fieldStr[i] = "対ボスダメ上昇";
                        break;
                    }
                case "ボスに対して攻撃力上昇"://ダメ攻撃両方上昇するのはどう処理する？
                    // "対ボス攻撃力ダメ上昇"
                    {
                        fieldStr[i] = "対ボス攻撃力上昇";
                        break;
                    }
                case "ダメージ増加":
                    {
                        fieldStr[i] = "ダメージ上昇";
                        break;
                    }
                case "回避":
                    {
                        fieldStr[i] = "回避";
                        break;
                    }
                case "反撃":
                    {
                        fieldStr[i] = "反撃";
                        break;
                    }
                case "再行動":
                    {
                        fieldStr[i] = "再行動";
                        break;
                    }
                case "防御力・ダメージ軽減率上昇":
                    {
                        fieldStr[i] = "防御ダメ軽減率上昇";
                        break;
                    }
                case "攻撃力低下":
                    {
                        fieldStr[i] = "攻撃力低下";
                        break;
                    }
                case "スキル発動率低下":
                    {
                        fieldStr[i] = "スキル発動率低下";
                        break;
                    }
                case "命中率低下":
                    {
                        fieldStr[i] = "攻撃ミス";
                        break;
                    }
                case "追撃":
                    {
                        fieldStr[i] = "追撃";
                        break;
                    }
                case "ダメージ無効化":
                    {
                        fieldStr[i] = "ダメ無効";
                        break;
                    }
                case "属性付与":
                    {
                        fieldStr[i] = "属性付与";
                        break;
                    }
                case "HP回復":
                    {
                        fieldStr[i] = "HP回復";
                        break;
                    }
                case "ソーラードライブ効果上昇":
                    {
                        fieldStr[i] = "ソラ効果上昇";
                        break;
                    }
                case "シャインクリスタルドロップ率上昇":
                    {
                        fieldStr[i] = "シャイクリ泥率上昇";
                        break;
                    }
                case "光ゲージ充填":
                    {
                        fieldStr[i] = "光ゲージ充填";
                        break;
                    }
                case "移動力増加":
                    {
                        fieldStr[i] = "PT移動力増加";
                        break;
                    }
                case "1ターン目系"://特殊。1ターン目のアビ全て抜き出す
                    {
                        fieldStr[i] = "1ターン目系";
                        selectValue[i] = 1;
                        break;
                    }
                case "スキル：全体":
                    {
                        fieldStr[i] = "全体";
                        break;
                    }
                case "スキル：2体":
                    {
                        fieldStr[i] = "2体";
                        break;
                    }
                case "スキル：変則":
                    {
                        fieldStr[i] = "変則";
                        break;
                    }
                case "スキル：複数回":
                    {
                        fieldStr[i] = "複数回";
                        break;
                    }
                case "スキル：吸収":
                    {
                        fieldStr[i] = "吸収";
                        break;
                    }
                case "スキル：単体":
                    {
                        fieldStr[i] = "単体";
                        break;
                    }

            }


        }


        
        //　合体してWHERE句生成

        //データテーブル取得
        DataTable dt_fkg_out = GetDataFilter(ref fieldStr, ref selectValue, ref inputValue, dt_fkg);
        
        //データテーブルが空の場合
        if(dt_fkg_out == null)
        {
            //メッセージボックス表示
            string script1 = "<script language=javascript>" + "window.alert('該当する花騎士を見つけることが出来ませんでした。処理を終了します')" + "</script>";
            Response.Write(script1);
            return;
        }

        

        //結果表示
        //結果表示用カラム追加
        dt_fkg_out.Columns.Add("Abi1", typeof(string));
        dt_fkg_out.Columns.Add("Abi2", typeof(string));
        dt_fkg_out.Columns.Add("Abi3", typeof(string));
        dt_fkg_out.Columns.Add("Abi4", typeof(string));

        dt_fkg_out.Columns.Add("Abi1V", typeof(int));
        dt_fkg_out.Columns.Add("Abi2V", typeof(int));
        dt_fkg_out.Columns.Add("Abi3V", typeof(int));
        dt_fkg_out.Columns.Add("Abi4V", typeof(int));

        dt_fkg_out.Columns.Add("SRatioRev", typeof(string));

        //
        //　結果表示用文字列変換処理
        //　検索キー順にデータセットに格納(これは未実装）
        //

        //  第一検索キー取得
        string[] findKey = new string[3];
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            if (fieldStr[i] != "")
            {
                findKey[count] = fieldStr[i];
                count++;
            }
        }



        //出力DB内の全データに対する処理
        for (int i = 0;i<dt_fkg_out.Rows.Count;i++)
        {
            string outA1 = "";
            string outA2 = "";
            string outA3 = "";
            string outA4 = "";
            int outA1V = 0;
            int outA2V = 0;
            int outA3V = 0;
            int outA4V = 0;

            //出力文字列
            string[] outAOri = new string[4];
            outAOri[0] = OutString(Convert.ToInt32(dt_fkg_out.Rows[i]["A1st1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A1NO"]), dt_fkg_out.Rows[i]["A1Ex1"].ToString(), Convert.ToInt32(dt_fkg_out.Rows[i]["A1V1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A1V2"]), dt_fkg_out.Rows[i]["Name"].ToString(), dt_fkg_out.Rows[i]["A1Ex2"].ToString());
            outAOri[1] = OutString(Convert.ToInt32(dt_fkg_out.Rows[i]["A2st1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A2NO"]), dt_fkg_out.Rows[i]["A2Ex1"].ToString(), Convert.ToInt32(dt_fkg_out.Rows[i]["A2V1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A2V2"]), dt_fkg_out.Rows[i]["Name"].ToString(), dt_fkg_out.Rows[i]["A2Ex2"].ToString());
            outAOri[2] = OutString(Convert.ToInt32(dt_fkg_out.Rows[i]["A3st1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A3NO"]), dt_fkg_out.Rows[i]["A3Ex1"].ToString(), Convert.ToInt32(dt_fkg_out.Rows[i]["A3V1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A3V2"]), dt_fkg_out.Rows[i]["Name"].ToString(), dt_fkg_out.Rows[i]["A3Ex2"].ToString());
            outAOri[3] = OutString(Convert.ToInt32(dt_fkg_out.Rows[i]["A4st1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A4NO"]), dt_fkg_out.Rows[i]["A4Ex1"].ToString(), Convert.ToInt32(dt_fkg_out.Rows[i]["A4V1"]), Convert.ToInt32(dt_fkg_out.Rows[i]["A4V2"]), dt_fkg_out.Rows[i]["Name"].ToString(), dt_fkg_out.Rows[i]["A4Ex2"].ToString());

            int[] outAVOri = new int[4];
            outAVOri[0] = Convert.ToInt32(dt_fkg_out.Rows[i]["A1V1"]);
            outAVOri[1] = Convert.ToInt32(dt_fkg_out.Rows[i]["A2V1"]);
            outAVOri[2] = Convert.ToInt32(dt_fkg_out.Rows[i]["A3V1"]);
            outAVOri[3] = Convert.ToInt32(dt_fkg_out.Rows[i]["A4V1"]);

            int[] outAV2Ori = new int[4];
            outAV2Ori[0] = Convert.ToInt32(dt_fkg_out.Rows[i]["A1V2"]);
            outAV2Ori[1] = Convert.ToInt32(dt_fkg_out.Rows[i]["A2V2"]);
            outAV2Ori[2] = Convert.ToInt32(dt_fkg_out.Rows[i]["A3V2"]);
            outAV2Ori[3] = Convert.ToInt32(dt_fkg_out.Rows[i]["A4V2"]);

            //出力処理前の出力文字列
            string[] outAEx = new string[4];
            outAEx[0] = dt_fkg_out.Rows[i]["A1Ex1"].ToString();
            outAEx[1] = dt_fkg_out.Rows[i]["A2Ex1"].ToString();
            outAEx[2] = dt_fkg_out.Rows[i]["A3Ex1"].ToString();
            outAEx[3] = dt_fkg_out.Rows[i]["A4Ex1"].ToString();

            //出力用ソート事前処理
            switch (count)
            {
                case 1:
                case 2:
                case 3:
                    {
                        int count1T = 0;
                        int index1st = 0;
                        int index2nd = 0;
                        int index3rd = 0;

                        for(int j = 0; j < 4; j++)
                        {
                            //ソートにA1V1を使うタイプ
                            if ((findKey[0] == outAEx[j])
                                 || ((findKey[0] == "スキル発動率上昇") && (outAEx[j] == "スキルLVでスキル発動率上昇"))
                                 || ((findKey[0] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、対ボスダメ上昇"))
                                 || ((findKey[0] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率1T目と3T目"))
                                 || ((findKey[0] == "クリ率上昇") && (outAEx[j] == "クリ率クリダメ上昇"))
                                 || ((findKey[0] == "クリダメ上昇") && (outAEx[j] == "クリダメ上昇し自身がさらに上昇"))
                                 || ((findKey[0] == "対ボス攻撃力上昇") && (outAEx[j] == "対ボス攻撃力ダメ上昇"))
                                 || ((findKey[0] == "対ボス攻撃力上昇") && (outAEx[j] == "対ボス攻撃力上昇し、自身が更に上昇"))
                                 || ((findKey[0] == "対ボス攻撃力上昇") && (outAEx[j] == "対ボス攻撃力上昇し、自身を含む2人がさらに上昇"))
                                 || ((findKey[0] == "ソラ効果上昇") && (outAEx[j] == "ソラ効果シャイクリ泥率上昇"))
                                 || ((findKey[0] == "ソラ効果上昇") && (outAEx[j] == "ソラ効果光ゲージ充填上昇"))
                                 || ((findKey[0] == "攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、自信を含む2人がさらに上昇"))
                                 || ((findKey[0] == "攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、自信を含む3人がさらに上昇"))
                                 || ((findKey[0] == "攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、スキル発動率上昇"))
                                 || ((findKey[0] == "対ボスダメ上昇") && (outAEx[j] == "対ボスダメ上昇自身のボスダメ上昇"))
                                 || ((findKey[0] == "ダメージ上昇") && (outAEx[j] == "ターン毎ダメージ上昇"))
                                 || ((findKey[0] == "ダメージ上昇") && (outAEx[j] == "ソラ発動毎にダメ上昇"))
                                 || ((findKey[0] == "PT移動力増加") && (outAEx[j] == "PT移動力増加し、移動力を攻撃力に追加")))


                            {
                                outA1 = outAOri[j];
                                outA1V = outAVOri[j];
                                switch (j)
                                {
                                    case 0:
                                        {
                                            outA2 = outAOri[1];
                                            outA3 = outAOri[2];
                                            outA4 = outAOri[3];

                                            outA2V = outAVOri[1];
                                            outA3V = outAVOri[2];
                                            outA4V = outAVOri[3];
                                            break;
                                        }
                                    case 1:
                                        {
                                            outA2 = outAOri[0];
                                            outA3 = outAOri[2];
                                            outA4 = outAOri[3];

                                            outA2V = outAVOri[0];
                                            outA3V = outAVOri[2];
                                            outA4V = outAVOri[3];
                                            break;
                                        }
                                    case 2:
                                        {
                                            outA2 = outAOri[0];
                                            outA3 = outAOri[1];
                                            outA4 = outAOri[3];

                                            outA2V = outAVOri[0];
                                            outA3V = outAVOri[1];
                                            outA4V = outAVOri[3];
                                            break;
                                        }
                                    case 3:
                                        {
                                            outA2 = outAOri[0];
                                            outA3 = outAOri[1];
                                            outA4 = outAOri[2];

                                            outA2V = outAVOri[0];
                                            outA3V = outAVOri[1];
                                            outA4V = outAVOri[2];
                                            break;
                                        }

                                }
                            }

                            //ソートにA1V2を使うタイプ//攻撃力上昇1T目さらに上昇
                            else if ((findKey[0] == outAEx[j])
                                 || ((findKey[0] == "スキル発動率上昇") && (outAEx[j] == "攻撃力上昇し、スキル発動率上昇"))
                                 || ((findKey[0] == "スキルダメ上昇") && (outAEx[j] == "攻撃力上昇し、スキルダメージ上昇"))
                                 || ((findKey[0] == "クリダメ上昇") && (outAEx[j] == "クリ率クリダメ上昇"))
                                 || ((findKey[0] == "対ボスダメ上昇") && (outAEx[j] == "攻撃力上昇し、対ボスダメ上昇"))
                                 || ((findKey[0] == "対ボスダメ上昇") && (outAEx[j] == "対ボス攻撃力ダメ上昇"))
                                 || ((findKey[0] == "対ボスダメ上昇") && (outAEx[j] == "スキル発動率上昇し、対ボスダメ上昇"))
                                 || ((findKey[0] == "シャイクリ泥率上昇") && (outAEx[j] == "ソラ効果シャイクリ泥率上昇"))
                                 || ((findKey[0] == "ソラ効果上昇") && (outAEx[j] == "攻撃力上昇し、ソラ効果上昇"))
                                 || ((findKey[0] == "光ゲージ充填") && (outAEx[j] == "ソラ効果光ゲージ充填上昇"))
                                 || ((findKey[0] == "光ゲージ充填") && (outAEx[j] == "攻撃力上昇し、光ゲージ充填"))
                                 || ((findKey[0] == "攻撃力低下") && (outAEx[j] == "攻撃力上昇し、敵全体の攻撃力低下"))
                                 || ((findKey[0] == "攻撃ミス") && (outAEx[j] == "攻撃力上昇し、敵3体が攻撃ミス"))
                                 || ((findKey[0] == "回避") && (outAEx[j] == "攻撃力上昇し、回避"))
                                 || ((findKey[0] == "再行動") && (outAEx[j] == "攻撃力上昇し、再行動"))
                                 || ((findKey[0] == "ターンで攻撃力上昇") && (outAEx[j] == "攻撃力上昇ターンでさらに上昇"))
                                 || ((findKey[0] == "ターンで攻撃力上昇") && (outAEx[j] == "攻撃力上昇1T目さらに上昇"))
                                 || ((findKey[0] == "1ターン目系") && (outAEx[j] == "攻撃力上昇1T目さらに上昇"))
                                 || ((findKey[0] == "PT移動力増加") && (outAEx[j] == "攻撃力上昇し、移動力追加")))


                            {
                                outA1 = outAOri[j];
                                outA1V = outAV2Ori[j];
                                switch (j)
                                {
                                    case 0:
                                        {
                                            outA2 = outAOri[1];
                                            outA3 = outAOri[2];
                                            outA4 = outAOri[3];

                                            outA2V = outAVOri[1];
                                            outA3V = outAVOri[2];
                                            outA4V = outAVOri[3];
                                            break;
                                        }
                                    case 1:
                                        {
                                            outA2 = outAOri[0];
                                            outA3 = outAOri[2];
                                            outA4 = outAOri[3];

                                            outA2V = outAVOri[0];
                                            outA3V = outAVOri[2];
                                            outA4V = outAVOri[3];
                                            break;
                                        }
                                    case 2:
                                        {
                                            outA2 = outAOri[0];
                                            outA3 = outAOri[1];
                                            outA4 = outAOri[3];

                                            outA2V = outAVOri[0];
                                            outA3V = outAVOri[1];
                                            outA4V = outAVOri[3];
                                            break;
                                        }
                                    case 3:
                                        {
                                            outA2 = outAOri[0];
                                            outA3 = outAOri[1];
                                            outA4 = outAOri[2];

                                            outA2V = outAVOri[0];
                                            outA3V = outAVOri[1];
                                            outA4V = outAVOri[2];
                                            break;
                                        }

                                }
                            }
                            //例外　1ターン目系
                            else if (findKey[0] == "1ターン目系" && outAOri[j].Contains("1T目"))
                            {
                                //1T目スキルが何個あるかカウント
                                count1T++;
                                switch (count1T)
                                {//最大1T目スキルが3個まであると想定
                                    case 1:
                                        {
                                            index1st = j;
                                            break;
                                        }
                                    case 2:
                                        {
                                            index2nd = j;
                                            break;
                                        }
                                    case 3:
                                        {
                                            index3rd = j;
                                            break;
                                        }
                                }
                                index1st = j;

                            }

                            

                        }

                        //例外　1ターン目系
                        if (findKey[0] == "1ターン目系")
                        {
                                    outA1 = outAOri[index1st];
                                    outA1V = outAVOri[index1st];
                                        switch (index1st)
                                        {
                                            case 0:
                                                {
                                                    outA2 = outAOri[1];
                                                    outA3 = outAOri[2];
                                                    outA4 = outAOri[3];

                                                    outA2V = outAVOri[1];
                                                    outA3V = outAVOri[2];
                                                    outA4V = outAVOri[3];
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    outA2 = outAOri[0];
                                                    outA3 = outAOri[2];
                                                    outA4 = outAOri[3];

                                                    outA2V = outAVOri[0];
                                                    outA3V = outAVOri[2];
                                                    outA4V = outAVOri[3];
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    outA2 = outAOri[0];
                                                    outA3 = outAOri[1];
                                                    outA4 = outAOri[3];

                                                    outA2V = outAVOri[0];
                                                    outA3V = outAVOri[1];
                                                    outA4V = outAVOri[3];
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    outA2 = outAOri[0];
                                                    outA3 = outAOri[1];
                                                    outA4 = outAOri[2];

                                                    outA2V = outAVOri[0];
                                                    outA3V = outAVOri[1];
                                                    outA4V = outAVOri[2];
                                                    break;
                                                }
                                        }
                        }

                        //スキルタイプの場合
                        if ((findKey[0] == "全体") || (findKey[0] == "2体") || (findKey[0] == "変則") || (findKey[0] == "複数回") || (findKey[0] == "吸収") || (findKey[0] == "単体"))
                        {
                            //特別にアビ欄を入れ替えない
                            outA1 = outAOri[0];
                            outA2 = outAOri[1];
                            outA3 = outAOri[2];
                            outA4 = outAOri[3];

                            outA1V = outAVOri[0];
                            outA2V = outAVOri[1];
                            outA3V = outAVOri[2];
                            outA4V = outAVOri[3];
                        }
                            break;
                    }
                default:
                    {
                        outA1 = outAOri[0];
                        outA2 = outAOri[1];
                        outA3 = outAOri[2];
                        outA4 = outAOri[3];

                        outA1V = outAVOri[0];
                        outA2V = outAVOri[1];
                        outA3V = outAVOri[2];
                        outA4V = outAVOri[3];
                        break;
                    }
            }


            dt_fkg_out.Rows[i]["Abi1"] = outA1;
            dt_fkg_out.Rows[i]["Abi2"] = outA2;
            dt_fkg_out.Rows[i]["Abi3"] = outA3;
            dt_fkg_out.Rows[i]["Abi4"] = outA4;

            dt_fkg_out.Rows[i]["Abi1V"] = outA1V;
            dt_fkg_out.Rows[i]["Abi2V"] = outA2V;
            dt_fkg_out.Rows[i]["Abi3V"] = outA3V;
            dt_fkg_out.Rows[i]["Abi4V"] = outA4V;

            //スキルタイプ追加
            dt_fkg_out.Rows[i]["SRatioRev"] = OutSkillExplain(Convert.ToString(dt_fkg_out.Rows[i]["SType"]),Convert.ToInt32(dt_fkg_out.Rows[i]["SRatio"]),Convert.ToString(dt_fkg_out.Rows[i]["Name"]));

        }

        //結果ソート処理
        DataView view1 = new DataView(dt_fkg_out);
        //スキル検索のみ別ソート
        if ((findKey[0] == "全体") || (findKey[0] == "2体") || (findKey[0] == "変則") || (findKey[0] == "複数回") || (findKey[0] == "吸収") || (findKey[0] == "単体"))
        {
            view1.Sort = "SRatio DESC,Name";
        }
        //スキル検索以外の通常ソート
        else
        {
            view1.Sort = "Abi1V DESC,Name";
        }
        //最終出力データテーブルに格納する
        DataTable dt_fkg_out2 = view1.ToTable();

        //結果を表示
        GridView1.DataSource = dt_fkg_out2;
        GridView1.DataBind();

       

    }

    //
    //与えられたクエリ要素と、AND、ORの別より、データテーブル生成する関数
    //
    private DataTable GetDataFilter(ref string[] fieldStrIn, ref int[] selectValueIn, ref int[] inputValueIn, DataTable dt_input)
    {

        string[] fieldStr = new string[3];
        

        int[] selectValue = new int[3];
        int[] inputValue = { 0, 0, 0 };
        

        //入力文字列の検証
        int countInput = 0;
        //int countSpecial = 0;
        //ノーマルの場合のカウント
        for (int i = 0; i < 3; i++)
        {
            if(fieldStrIn[i]!="")
            {
                fieldStr[countInput] = fieldStrIn[i];
                selectValue[countInput] = selectValueIn[i];
                countInput++;
            }
        }

        for (int i = 0; i < 3 ; i++)
        {
            if(inputValueIn[i] != 0)
            {
                inputValue[i] = inputValueIn[i];
            }
        }


        //LINQ処理
        DataTable[] dt = new DataTable[4];
        dt[0] = dt_input;

        if(countInput == 0)
        {
            return null;
        }

        for(int i = 0;i<3;i++)
        {
            if (selectValue[i] == 0)
            //ノーマル
            {
                switch (fieldStr[i])
                {
                    case "スキル発動率上昇":
                        //"スキルLVでスキル発動率上昇"も引っ掛ける
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルLVでスキル発動率上昇"))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、スキル発動率上昇"))
                                             || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボスダメ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A2Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A3Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A4Ex1") == "スキル発動率1T目と3T目"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }

                            break;
                        }
                    case "クリ率上昇":
                    case "クリダメ上昇":
                    case "クリダメ上昇し自身がさらに上昇":
                        //"クリ率クリダメ上昇"も引っ掛ける
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where (((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "クリ率クリダメ上昇") || (p.Field<string>("A2Ex1") == "クリ率クリダメ上昇") || (p.Field<string>("A3Ex1") == "クリ率クリダメ上昇") || (p.Field<string>("A4Ex1") == "クリ率クリダメ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "クリダメ上昇し自身がさらに上昇") || (p.Field<string>("A2Ex1") == "クリダメ上昇し自身がさらに上昇") || (p.Field<string>("A3Ex1") == "クリダメ上昇し自身がさらに上昇") || (p.Field<string>("A4Ex1") == "クリダメ上昇し自身がさらに上昇")))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }
                    case "ターンで攻撃力上昇":
                        //パターン何個かあるけど
                        //"攻撃力上昇1T目さらに上昇"
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇1T目さらに上昇"))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇ターンでさらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇ターンでさらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇ターンでさらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇ターンでさらに上昇"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }
                    case "対ボスダメ上昇":
                        // "対ボス攻撃力ダメ上昇"も引っ掛ける
                        // "対ボスダメ上昇自身のボスダメ上昇"も引っ掛ける
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、対ボスダメ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "対ボス攻撃力ダメ上昇") || (p.Field<string>("A2Ex1") == "対ボス攻撃力ダメ上昇") || (p.Field<string>("A3Ex1") == "対ボス攻撃力ダメ上昇") || (p.Field<string>("A4Ex1") == "対ボス攻撃力ダメ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "対ボスダメ上昇自身のボスダメ上昇") || (p.Field<string>("A2Ex1") == "対ボスダメ上昇自身のボスダメ上昇") || (p.Field<string>("A3Ex1") == "対ボスダメ上昇自身のボスダメ上昇") || (p.Field<string>("A4Ex1") == "対ボスダメ上昇自身のボスダメ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボスダメ上昇"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "対ボス攻撃力上昇":
                        // "対ボス攻撃力ダメ上昇"も引っ掛ける
                        // "対ボスダメ上昇自身のボスダメ上昇"も引っ掛ける
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "対ボス攻撃力ダメ上昇") || (p.Field<string>("A2Ex1") == "対ボス攻撃力ダメ上昇") || (p.Field<string>("A3Ex1") == "対ボス攻撃力ダメ上昇") || (p.Field<string>("A4Ex1") == "対ボス攻撃力ダメ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "対ボス攻撃力上昇し、自身が更に上昇") || (p.Field<string>("A2Ex1") == "対ボス攻撃力上昇し、自身が更に上昇") || (p.Field<string>("A3Ex1") == "対ボス攻撃力上昇し、自身が更に上昇") || (p.Field<string>("A4Ex1") == "対ボス攻撃力上昇し、自身が更に上昇"))
                                             || ((p.Field<string>("A1Ex1") == "対ボス攻撃力上昇し、自身を含む2人がさらに上昇") || (p.Field<string>("A2Ex1") == "対ボス攻撃力上昇し、自身を含む2人がさらに上昇") || (p.Field<string>("A3Ex1") == "対ボス攻撃力上昇し、自身を含む2人がさらに上昇") || (p.Field<string>("A4Ex1") == "対ボス攻撃力上昇し、自身を含む2人がさらに上昇"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "ソラ効果上昇":
                        // "ソラ効果シャイクリ泥率上昇"も引っ掛ける
                        // "ソラ効果光ゲージ充填上昇"も
                        {

                            var outputData = from p in dt[i].AsEnumerable()
                                             where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= inputValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= inputValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= inputValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i])) && (p.Field<int>("A4V1") >= inputValue[i]))
                                             || (((p.Field<string>("A1Ex1") == "ソラ効果シャイクリ泥率上昇") && (p.Field<int>("A1V1") >= inputValue[i])) || ((p.Field<string>("A2Ex1") == "ソラ効果シャイクリ泥率上昇") && (p.Field<int>("A2V1") >= inputValue[i])) || ((p.Field<string>("A3Ex1") == "ソラ効果シャイクリ泥率上昇") && (p.Field<int>("A3V1") >= inputValue[i])) || ((p.Field<string>("A4Ex1") == "ソラ効果シャイクリ泥率上昇")) && (p.Field<int>("A4V1") >= inputValue[i]))
                                             || (((p.Field<string>("A1Ex1") == "ソラ効果光ゲージ充填上昇") && (p.Field<int>("A1V1") >= inputValue[i])) || ((p.Field<string>("A2Ex1") == "ソラ効果光ゲージ充填上昇") && (p.Field<int>("A2V1") >= inputValue[i])) || ((p.Field<string>("A3Ex1") == "ソラ効果光ゲージ充填上昇") && (p.Field<int>("A3V1") >= inputValue[i])) || ((p.Field<string>("A4Ex1") == "ソラ効果光ゲージ充填上昇")) && (p.Field<int>("A4V1") >= inputValue[i]))
                                             || (((p.Field<string>("A1Ex1") == "攻撃力上昇し、ソラ効果上昇") && (p.Field<int>("A1V1") >= inputValue[i])) || ((p.Field<string>("A2Ex1") == "攻撃力上昇し、ソラ効果上昇") && (p.Field<int>("A2V1") >= inputValue[i])) || ((p.Field<string>("A3Ex1") == "攻撃力上昇し、ソラ効果上昇") && (p.Field<int>("A3V1") >= inputValue[i])) || ((p.Field<string>("A4Ex1") == "攻撃力上昇し、ソラ効果上昇")) && (p.Field<int>("A4V1") >= inputValue[i]))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "シャイクリ泥率上昇":
                        // "対ボス攻撃力ダメ上昇"も引っ掛ける
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "ソラ効果シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "ソラ効果シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "ソラ効果シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "ソラ効果シャイクリ泥率上昇"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "光ゲージ充填":
                        // "ソラ効果光ゲージ充填上昇"も
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "ソラ効果光ゲージ充填上昇") || (p.Field<string>("A2Ex1") == "ソラ効果光ゲージ充填上昇") || (p.Field<string>("A3Ex1") == "ソラ効果光ゲージ充填上昇") || (p.Field<string>("A4Ex1") == "ソラ効果光ゲージ充填上昇"))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、光ゲージ充填") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、光ゲージ充填") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、光ゲージ充填") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、光ゲージ充填"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "攻撃力低下":
                        // "攻撃力上昇し、敵全体が攻撃力低下"も
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、敵全体の攻撃力低下"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "攻撃ミス":
                        // "攻撃力上昇し、敵3体が攻撃ミス"も
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、敵3体が攻撃ミス") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、敵3体が攻撃ミス") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、敵3体が攻撃ミス") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、敵3体が攻撃ミス"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "回避":
                        // "攻撃力上昇し、敵3体が攻撃ミス"も
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、回避") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、回避") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、回避") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、回避"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "スキルダメ上昇":
                        // "攻撃力上昇し、スキルダメージ上昇"も
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキルダメージ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、スキルダメージ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、スキルダメージ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、スキルダメージ上昇"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "再行動":
                        // "攻撃力上昇し、自身が再行動"も
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、再行動") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、再行動") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、再行動") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、再行動"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "PT移動力増加":
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、移動力追加") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、移動力追加") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、移動力追加") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、移動力追加"))
                                             || ((p.Field<string>("A1Ex1") == "PT移動力増加し、移動力を攻撃力に追加") || (p.Field<string>("A2Ex1") == "PT移動力増加し、移動力を攻撃力に追加") || (p.Field<string>("A3Ex1") == "PT移動力増加し、移動力を攻撃力に追加") || (p.Field<string>("A4Ex1") == "PT移動力増加し、移動力を攻撃力に追加"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    case "ダメージ上昇":
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                             || ((p.Field<string>("A1Ex1") == "ターン毎ダメージ上昇") || (p.Field<string>("A2Ex1") == "ターン毎ダメージ上昇") || (p.Field<string>("A3Ex1") == "ターン毎ダメージ上昇") || (p.Field<string>("A4Ex1") == "ターン毎ダメージ上昇"))
                                             || ((p.Field<string>("A1Ex1") == "ソラ発動毎にダメ上昇") || (p.Field<string>("A2Ex1") == "ソラ発動毎にダメ上昇") || (p.Field<string>("A3Ex1") == "ソラ発動毎にダメ上昇") || (p.Field<string>("A4Ex1") == "ソラ発動毎にダメ上昇"))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }
                    /*
                case "攻撃力上昇"://考えてみるとみんな攻撃力上昇持ってるから、このコードはいらないかもしれない。
                    //DB登録種類列記
                    //"敵の数で攻撃力上昇"
                    //"攻撃力上昇HP割合ダメ上昇"
                    //"攻撃力上昇し自身がさらに上昇"
                    //"スキル発動毎に攻撃力上昇"fkg_selectでのコーディングはなし
                    {
                        var outputData = from p in dt[i].AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                         || ((p.Field<string>("A1Ex1") == "敵の数で攻撃力上昇") || (p.Field<string>("A2Ex1") == "敵の数で攻撃力上昇") || (p.Field<string>("A3Ex1") == "敵の数で攻撃力上昇") || (p.Field<string>("A4Ex1") == "敵の数で攻撃力上昇"))
                                         || ((p.Field<string>("A1Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇HP割合ダメ上昇"))
                                         || ((p.Field<string>("A1Ex1") == "攻撃力上昇し自身がさらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し自身がさらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し自身がさらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し自身がさらに上昇"))
                                         || ((p.Field<string>("A1Ex1") == "スキル発動毎に攻撃力上昇") || (p.Field<string>("A2Ex1") == "スキル発動毎に攻撃力上昇") || (p.Field<string>("A3Ex1") == "スキル発動毎に攻撃力上昇") || (p.Field<string>("A4Ex1") == "スキル発動毎に攻撃力上昇"))
                                         select p;

                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }

                        dt[i + 1] = outputData.CopyToDataTable();
                        if (countInput == (i + 1))
                        {
                            return dt[i + 1];
                        }
                        break;
                    }
                    */

                    case "全体":
                    case "2体":
                    case "変則":
                    case "複数回":
                    case "吸収":
                    case "単体":
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("SType") == fieldStr[i]))
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }

                            break;
                        }

                    default:
                        {
                            //var outputData = from p in dt[i].AsEnumerable()
                            //                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                            //                 select p;

                            var outputData = from p in dt[i].AsEnumerable()
                                             where ((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= inputValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= inputValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= inputValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i])) && (p.Field<int>("A4V1") >= inputValue[i])
                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();
                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }
                }
            }
            else
            //スペシャル
            {
                switch (fieldStr[i])
                {
                    case "1ターン目系":
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where (p.Field<int>("A1st1") == selectValue[i]) || (p.Field<int>("A2st1") == selectValue[i]) || (p.Field<int>("A3st1") == selectValue[i]) || (p.Field<int>("A4st1") == selectValue[i])
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇1T目さらに上昇"))

                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();

                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }


                    case "スキル発動率上昇":
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") == selectValue[i]))
                                             || ((p.Field<string>("A1Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A4V1") == selectValue[i]))
                                             || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i])) )


                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();

                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                    default:
                        {
                            var outputData = from p in dt[i].AsEnumerable()
                                             where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") == selectValue[i])))


                                             select p;

                            //検索結果が無い場合
                            if (outputData.Count() == 0)
                            {
                                return null;
                            }

                            dt[i + 1] = outputData.CopyToDataTable();

                            if (countInput == (i + 1))
                            {
                                return dt[i + 1];
                            }
                            break;
                        }

                       
                }
               
                    
            }
        }
        //コンパイラ通すため
        return null;

       /*
        //
        //初期案
        //
        //ノーマル
        switch (countInput)
        {
            case 0:
                {
                    return null;
                }
            case 1:
                {
                    if (selectValue[0] == 0)
                    //ノーマル
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                         select p;

                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }

                        DataTable dt = outputData.CopyToDataTable();
                        return dt;
                    }
                    else
                    //スペシャル
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (((p.Field<string>("A1Ex1") == fieldStr[0]) && (p.Field<int>("A1V1") == selectValue[0])) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0])
                                         || ((p.Field<string>("A1Ex1") == fieldStr[0]) || ((p.Field<string>("A2Ex1") == fieldStr[0]) && (p.Field<int>("A2V1") == selectValue[0])) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                         || ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || ((p.Field<string>("A3Ex1") == fieldStr[0]) && (p.Field<int>("A3V1") == selectValue[0])) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                         || ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || ((p.Field<string>("A4Ex1") == fieldStr[0]) && (p.Field<int>("A4V1") == selectValue[0]))))

                                         select p;

                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }

                        DataTable dt = outputData.CopyToDataTable();
                        return dt;
                    }
                
                   }
            case 2:
                {
                    if (!this.CheckBox4.Checked)
                    {
                        //AND検索
                        var outputData = from p in dt_input.AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                         && ((p.Field<string>("A1Ex1") == fieldStr[1]) || (p.Field<string>("A2Ex1") == fieldStr[1]) || (p.Field<string>("A3Ex1") == fieldStr[1]) || (p.Field<string>("A4Ex1") == fieldStr[1]))
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        DataTable dt = outputData.CopyToDataTable();
                        return dt;
                    }
                    else
                    {
                        //OR検索
                        var outputData = from p in dt_input.AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                                || ((p.Field<string>("A1Ex1") == fieldStr[1]) || (p.Field<string>("A2Ex1") == fieldStr[1]) || (p.Field<string>("A3Ex1") == fieldStr[1]) || (p.Field<string>("A4Ex1") == fieldStr[1]))
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        DataTable dt = outputData.CopyToDataTable();
                        return dt;
                    }
                }
            case 3:
                {
                    if (!this.CheckBox4.Checked)
                    {
                        //AND検索
                        var outputData = from p in dt_input.AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                         && ((p.Field<string>("A1Ex1") == fieldStr[1]) || (p.Field<string>("A2Ex1") == fieldStr[1]) || (p.Field<string>("A3Ex1") == fieldStr[1]) || (p.Field<string>("A4Ex1") == fieldStr[1]))
                                         && ((p.Field<string>("A1Ex1") == fieldStr[2]) || (p.Field<string>("A2Ex1") == fieldStr[2]) || (p.Field<string>("A3Ex1") == fieldStr[2]) || (p.Field<string>("A4Ex1") == fieldStr[2]))
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        DataTable dt = outputData.CopyToDataTable();
                        return dt;
                    }
                    else
                    {
                        //OR検索
                        var outputData = from p in dt_input.AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == fieldStr[0]) || (p.Field<string>("A2Ex1") == fieldStr[0]) || (p.Field<string>("A3Ex1") == fieldStr[0]) || (p.Field<string>("A4Ex1") == fieldStr[0]))
                                                || ((p.Field<string>("A1Ex1") == fieldStr[1]) || (p.Field<string>("A2Ex1") == fieldStr[1]) || (p.Field<string>("A3Ex1") == fieldStr[1]) || (p.Field<string>("A4Ex1") == fieldStr[1]))
                                                || ((p.Field<string>("A1Ex1") == fieldStr[2]) || (p.Field<string>("A2Ex1") == fieldStr[2]) || (p.Field<string>("A3Ex1") == fieldStr[2]) || (p.Field<string>("A4Ex1") == fieldStr[2]))
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        DataTable dt = outputData.CopyToDataTable();
                        return dt;
                    }
                }
            default:
                return null;
        }
        */
    }

    private DataTable GetDataFilterRare(DataTable dt_input)
    {
        DataTable dt_out;

        //全てのレア指定の場合
        if((CheckBox1.Checked)&&(CheckBox2.Checked)&&(CheckBox3.Checked))
            {
                return dt_input;
            }
        //☆6
        else if(CheckBox1.Checked)
        {
            var outputData = from p in dt_input.AsEnumerable()
                             where (p.Field<int>("Rarity") == 6)
                             select p;
            //検索結果が無い場合
            if (outputData.Count() == 0)
            {
                return null;
            }

            dt_out = outputData.CopyToDataTable();
            return dt_out;
        }
        //☆5
        else if (CheckBox2.Checked)
        {
            var outputData = from p in dt_input.AsEnumerable()
                             where (p.Field<int>("Rarity") == 5)
                             select p;
            //検索結果が無い場合
            if (outputData.Count() == 0)
            {
                return null;
            }

            dt_out = outputData.CopyToDataTable();
            return dt_out;
        }
        //その他 4,3,2
        else if (CheckBox3.Checked)
        {
            var outputData = from p in dt_input.AsEnumerable()
                             where (p.Field<int>("Rarity") == 4) || (p.Field<int>("Rarity") == 3) || (p.Field<int>("Rarity") == 2)
                             select p;
            //検索結果が無い場合
            if (outputData.Count() == 0)
            {
                return null;
            }

            dt_out = outputData.CopyToDataTable();
            return dt_out;
        }
        //6と5
        else if (CheckBox1.Checked && CheckBox2.Checked)
        {
            var outputData = from p in dt_input.AsEnumerable()
                             where (p.Field<int>("Rarity") == 6) || (p.Field<int>("Rarity") == 5)
                             select p;
            //検索結果が無い場合
            if (outputData.Count() == 0)
            {
                return null;
            }

            dt_out = outputData.CopyToDataTable();
            return dt_out;
        }
        //6とその他
        else if (CheckBox1.Checked && CheckBox3.Checked)
        {
            var outputData = from p in dt_input.AsEnumerable()
                             where (p.Field<int>("Rarity") == 6) 
                             || ((p.Field<int>("Rarity") == 4) || (p.Field<int>("Rarity") == 3) || (p.Field<int>("Rarity") == 2))
                             select p;
            //検索結果が無い場合
            if (outputData.Count() == 0)
            {
                return null;
            }

            dt_out = outputData.CopyToDataTable();
            return dt_out;
        }
        //5とその他
        else if (CheckBox2.Checked && CheckBox3.Checked)
        {
            var outputData = from p in dt_input.AsEnumerable()
                             where (p.Field<int>("Rarity") == 5)
                             || ((p.Field<int>("Rarity") == 4) || (p.Field<int>("Rarity") == 3) || (p.Field<int>("Rarity") == 2))
                             select p;
            //検索結果が無い場合
            if (outputData.Count() == 0)
            {
                return null;
            }

            dt_out = outputData.CopyToDataTable();
            return dt_out;
        }

        return null;
    }

    //スキルタイプフィルタ処理
    private DataTable GetDataFilterSType(DataTable dt_input)
    {
        DataTable dt_out;

        //全チェック入り指定の場合
        if ((CheckBox9.Checked) && (CheckBox10.Checked) && (CheckBox11.Checked) && (CheckBox12.Checked) && (CheckBox13.Checked))
        {
            return dt_input;
        }
        //Trueの数で場合分け。判定処理
        int countTrue = 0;
        if(CheckBox9.Checked == true)
        {
            countTrue++;
        }
        if (CheckBox10.Checked == true)
        {
            countTrue++;
        }
        if (CheckBox11.Checked == true)
        {
            countTrue++;
        }
        if (CheckBox12.Checked == true)
        {
            countTrue++;
        }
        if (CheckBox13.Checked == true)
        {
            countTrue++;
        }
        if (CheckBox14.Checked == true)
        {
            countTrue++;
        }


        switch (countTrue)
        {
            case 1:
            {
                    string filterValue = "";
                    if (CheckBox9.Checked == true)
                    {
                        filterValue = "全体";
                    }
                    if (CheckBox10.Checked == true)
                    {
                        filterValue = "2体";
                    }
                    if (CheckBox11.Checked == true)
                    {
                        filterValue = "変則";
                    }
                    if (CheckBox12.Checked == true)
                    {
                        filterValue = "複数回";
                    }
                    if (CheckBox13.Checked == true)
                    {
                        filterValue = "吸収";
                    }
                    if (CheckBox14.Checked == true)
                    {
                        filterValue = "単体";
                    }

                    var outputData = from p in dt_input.AsEnumerable()
                                     where (p.Field<string>("SType") == filterValue)
                                     select p;
                    //検索結果が無い場合
                    if (outputData.Count() == 0)
                    {
                        return null;
                    }

                    dt_out = outputData.CopyToDataTable();
                    return dt_out;

            }

            case 2:
                {
                    string filterValue1 = "";
                    string filterValue2 = "";
                    if (CheckBox9.Checked == true)
                    {
                        filterValue1 = "全体";
                    }
                    if (CheckBox10.Checked == true)
                    {
                        if(filterValue1 == "")
                            filterValue1 = "2体";
                        filterValue2 = "2体";
                    }
                    if (CheckBox11.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "変則";
                        else if (filterValue2 == "")
                            filterValue2 = "変則";
                    }
                    if (CheckBox12.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "複数回";
                        else if (filterValue2 == "")
                            filterValue2 = "複数回";
                    }
                    if (CheckBox13.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "吸収";
                        else if (filterValue2 == "")
                            filterValue2 = "吸収";
                    }
                    if (CheckBox14.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "単体";
                        else if (filterValue2 == "")
                            filterValue2 = "単体";
                    }

                    var outputData = from p in dt_input.AsEnumerable()
                                     where (p.Field<string>("SType") == filterValue1) || (p.Field<string>("SType") == filterValue2)
                                     select p;
                    //検索結果が無い場合
                    if (outputData.Count() == 0)
                    {
                        return null;
                    }

                    dt_out = outputData.CopyToDataTable();
                    return dt_out;

                }

            case 3:
                {
                    string filterValue1 = "";
                    string filterValue2 = "";
                    string filterValue3 = "";
                    if (CheckBox9.Checked == true)
                    {
                        filterValue1 = "全体";
                    }
                    if (CheckBox10.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "2体";
                        filterValue2 = "2体";
                    }
                    if (CheckBox11.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "変則";
                        else if (filterValue2 == "")
                            filterValue2 = "変則";
                        else if (filterValue3 == "")
                            filterValue3 = "変則";
                    }
                    if (CheckBox12.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "複数回";
                        else if (filterValue2 == "")
                            filterValue2 = "複数回";
                        else if (filterValue3 == "")
                            filterValue3 = "複数回";
                    }
                    if (CheckBox13.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "吸収";
                        else if (filterValue2 == "")
                            filterValue2 = "吸収";
                        else if (filterValue3 == "")
                            filterValue3 = "吸収";
                    }
                    if (CheckBox14.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "単体";
                        else if (filterValue2 == "")
                            filterValue2 = "単体";
                        else if (filterValue3 == "")
                            filterValue3 = "単体";
                    }

                    var outputData = from p in dt_input.AsEnumerable()
                                     where (p.Field<string>("SType") == filterValue1) || (p.Field<string>("SType") == filterValue2) || (p.Field<string>("SType") == filterValue3)
                                     select p;
                    //検索結果が無い場合
                    if (outputData.Count() == 0)
                    {
                        return null;
                    }

                    dt_out = outputData.CopyToDataTable();
                    return dt_out;
                }

            case 4:
                {
                    string filterValue1 = "";
                    string filterValue2 = "";
                    string filterValue3 = "";
                    string filterValue4 = "";

                    if (CheckBox9.Checked == true)
                    {
                        filterValue1 = "全体";
                    }
                    if (CheckBox10.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "2体";
                        filterValue2 = "2体";
                    }
                    if (CheckBox11.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "変則";
                        else if (filterValue2 == "")
                            filterValue2 = "変則";
                        else if (filterValue3 == "")
                            filterValue3 = "変則";

                    }
                    if (CheckBox12.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "複数回";
                        else if (filterValue2 == "")
                            filterValue2 = "複数回";
                        else if (filterValue3 == "")
                            filterValue3 = "複数回";
                        else if (filterValue4 == "")
                            filterValue4 = "複数回";
                    }
                    if (CheckBox13.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "吸収";
                        else if (filterValue2 == "")
                            filterValue2 = "吸収";
                        else if (filterValue3 == "")
                            filterValue3 = "吸収";
                        else if (filterValue4 == "")
                            filterValue4 = "吸収";
                    }
                    if (CheckBox14.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "単体";
                        else if (filterValue2 == "")
                            filterValue2 = "単体";
                        else if (filterValue3 == "")
                            filterValue3 = "単体";
                        else if (filterValue4 == "")
                            filterValue4 = "単体";
                    }
                    var outputData = from p in dt_input.AsEnumerable()
                                     where (p.Field<string>("SType") == filterValue1) || (p.Field<string>("SType") == filterValue2) || (p.Field<string>("SType") == filterValue3)
                                     select p;
                    //検索結果が無い場合
                    if (outputData.Count() == 0)
                    {
                        return null;
                    }

                    dt_out = outputData.CopyToDataTable();
                    return dt_out;
                }

                case 5:
                {
                    string filterValue1 = "";
                    string filterValue2 = "";
                    string filterValue3 = "";
                    string filterValue4 = "";
                    string filterValue5 = "";

                    if (CheckBox9.Checked == true)
                    {
                        filterValue1 = "全体";
                    }
                    if (CheckBox10.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "2体";
                        filterValue2 = "2体";
                    }
                    if (CheckBox11.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "変則";
                        else if (filterValue2 == "")
                            filterValue2 = "変則";
                        else if (filterValue3 == "")
                            filterValue3 = "変則";

                    }
                    if (CheckBox12.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "複数回";
                        else if (filterValue2 == "")
                            filterValue2 = "複数回";
                        else if (filterValue3 == "")
                            filterValue3 = "複数回";
                        else if (filterValue4 == "")
                            filterValue4 = "複数回";
                    }
                    if (CheckBox13.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "吸収";
                        else if (filterValue2 == "")
                            filterValue2 = "吸収";
                        else if (filterValue3 == "")
                            filterValue3 = "吸収";
                        else if (filterValue4 == "")
                            filterValue4 = "吸収";
                        else if (filterValue5 == "")
                            filterValue5 = "吸収";
                    }
                    if (CheckBox14.Checked == true)
                    {
                        if (filterValue1 == "")
                            filterValue1 = "単体";
                        else if (filterValue2 == "")
                            filterValue2 = "単体";
                        else if (filterValue3 == "")
                            filterValue3 = "単体";
                        else if (filterValue4 == "")
                            filterValue4 = "単体";
                        else if (filterValue5 == "")
                            filterValue5 = "単体";
                    }

                    var outputData = from p in dt_input.AsEnumerable()
                                     where (p.Field<string>("SType") == filterValue1) || (p.Field<string>("SType") == filterValue2) || (p.Field<string>("SType") == filterValue3)
                                     select p;
                    //検索結果が無い場合
                    if (outputData.Count() == 0)
                    {
                        return null;
                    }

                    dt_out = outputData.CopyToDataTable();
                    return dt_out;
                }
        }

        return null;
    }

    private DataTable GetDataFilterAtt(DataTable dt_input)
    {
        DataTable dt_out;

        //全ての属性指定の場合
        if ((CheckBox5.Checked) && (CheckBox6.Checked) && (CheckBox7.Checked) && (CheckBox8.Checked))
        {
            return dt_input;
        }

        //属性数チェック
        string[,] arrayAtt = new string[4, 2];

        arrayAtt[0, 0] = CheckBox5.Checked.ToString();
        arrayAtt[1, 0] = CheckBox6.Checked.ToString();
        arrayAtt[2, 0] = CheckBox7.Checked.ToString();
        arrayAtt[3, 0] = CheckBox8.Checked.ToString();

        arrayAtt[0, 1] = "斬";
        arrayAtt[1, 1] = "打";
        arrayAtt[2, 1] = "突";
        arrayAtt[3, 1] = "魔";

        int attCount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (arrayAtt[i, 0] == "True")
            {
                attCount++;
            }
        }

        switch (attCount)
        {
            case 1://1個の場合
                {
                    string attInput ="";
                    //どれを入れるか選別
                    for (int i = 0; i < 4; i++)
                    {
                        if(arrayAtt[i,0] == "True")
                        {
                            attInput = arrayAtt[i,1];
                        }
                    }
                    dt_out = FilterSub(attInput, dt_input);
                    break;
                    
                }
            case 2://2個の場合
                {
                    string[] attInput = new string[2];
                    int j = 0;
                    //どれを入れるか選別
                    for (int i = 0; i < 4; i++)
                    {
                        if (arrayAtt[i, 0] == "True")
                        {
                            attInput[j] = arrayAtt[i, 1];
                            j++;
                        }
                    }
                    dt_out = FilterSub(attInput[0], attInput[1], dt_input);
                    break;
                }
            case 3://3個の場合
                {
                    string[] attInput = new string[3];
                    int j = 0;
                    //どれを入れるか選別
                    for (int i = 0; i < 4; i++)
                    {
                        if (arrayAtt[i, 0] == "True")
                        {
                            attInput[j] = arrayAtt[i, 1];
                            j++;
                        }
                    }
                    dt_out = FilterSub(attInput[0], attInput[1], attInput[2], dt_input);
                    break;
                }
            default:
                return null;
        }
        return dt_out;
    }

    private DataTable FilterSub(string Att1, DataTable dt_in)
    {
        DataTable dt_out;

        var outputData = from p in dt_in.AsEnumerable()
                         where (p.Field<string>("ATT") == Att1)
                         select p;
        //検索結果が無い場合
        if (outputData.Count() == 0)
        {
            return null;
        }

        dt_out = outputData.CopyToDataTable();
        return dt_out;
    }

    private DataTable FilterSub(string Att1, string Att2, DataTable dt_in)
    {
        DataTable dt_out;

        var outputData = from p in dt_in.AsEnumerable()
                         where (p.Field<string>("ATT") == Att1) || (p.Field<string>("ATT") == Att2)
                         select p;
        //検索結果が無い場合
        if (outputData.Count() == 0)
        {
            return null;
        }

        dt_out = outputData.CopyToDataTable();
        return dt_out;
    }

    private DataTable FilterSub(string Att1, string Att2, string Att3,DataTable dt_in)
    {
        DataTable dt_out;

        var outputData = from p in dt_in.AsEnumerable()
                         where (p.Field<string>("ATT") == Att1) || (p.Field<string>("ATT") == Att2) || (p.Field<string>("ATT") == Att3)
                         select p;
        //検索結果が無い場合
        if (outputData.Count() == 0)
        {
            return null;
        }

        dt_out = outputData.CopyToDataTable();
        return dt_out;
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

    //
    //出力文字生成関数
    //
    string OutString(int turn,int num,string Ex,int v1, int v2, string Name, string Ex2)
    {
        

        string turnStr = "";
        string numStr = "";
        string outStr = "";
        
        switch (Ex)
        {
            case "攻撃力上昇":
                {
                    if(turn == 1)
                    {
                        turnStr = "1T目の";
                    }

                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }

                    outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇";
                    return outStr;
                }

            case "敵の数で攻撃力上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    if (v2 == -1)
                    {//ウキツリボク専用
                        outStr = "敵の数が3体で" + v1 + "％攻撃力増加、敵の数が減るほど"　+ v1 + "％ずつ" + numStr + "攻撃力増加";
                    }
                    else
                    {
                        outStr = "敵の数ｘ" + v1 + "％" + numStr + "攻撃力が上昇";
                    }
                    return outStr;
                }

            case "ターンで攻撃力上昇"://シャクヤクハロ、スノドロ、ヤドリギ(MAX40%)
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }


                    outStr = "ターン毎に" + numStr + "攻撃力" + v1 + "％上昇　最大" + v2 + "％";
                    return outStr;
                }

            case "攻撃力上昇ターンでさらに上昇"://アデニウム昇華
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力が" + v1 + "％上昇し," + turnStr + "ターン毎にPT全体の攻撃力" + v2 + "％上昇";
                    return outStr;

                }

            case "攻撃力上昇1T目さらに上昇"://マンリョウ
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力が" + v1 + "％上昇し、1T目の攻撃力が更に" + v2 + "％上昇";
                    return outStr;
                }

            case "攻撃力上昇HP割合ダメ上昇"://シャボンソウ
                {
                    numStr = "自身の";
                    string v2Sub = "";
                    switch (v2)
                    {
                        case 50:
                            v2Sub = "1/2";
                            break;
                    }
                    outStr = "PT全体の攻撃力が" + v1 + "％上昇し更に" + numStr + "現在のHPの" + v2Sub + "の割合で" + numStr + "与ダメージが増加";
                    return outStr;
                }

            case "攻撃力上昇し自身がさらに上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力が" + v1 + "％上昇し、自身の攻撃力が更に" + v2 + "％上昇";
                    return outStr;
                }

            case "攻撃力上昇し、自信を含む2人がさらに上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力が" + v1 + "％上昇し、自身を含む2人の攻撃力が更に" + v2 + "％上昇";
                    return outStr;
                }

            case "攻撃力上昇し、自信を含む3人がさらに上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力が" + v1 + "％上昇し、自身を含む3人の攻撃力が更に" + v2 + "％上昇";
                    return outStr;
                }
            case "攻撃力上昇し、敵の数でさらに上昇":
                {
                    {
                        if (turn == 1)
                        {
                            turnStr = "1T目の";
                        }

                        switch (num)
                        {
                            case 1:
                                {
                                    numStr = "自身の";
                                    break;
                                }
                            case 2:
                            case 3:
                            case 4:
                                {
                                    numStr = "自身を含む" + num + "人の";
                                    break;
                                }
                            case 5:
                                {
                                    numStr = "PT全体の";
                                    break;
                                }
                        }

                        outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、 敵の数ｘ" + v2 + "％攻撃力が上昇";
                        return outStr;
                    }
                }

            case "攻撃力上昇し、スキル発動率上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }

                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    double v2D = (double)(v2 + 100) / 100;
                    outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、スキル発動率が"　+ v2D + "倍";
                    return outStr;

                }

            case "攻撃力上昇し、スキルダメージ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }

                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    if (num != 5)
                    {
                        outStr = turnStr + "PT全体のスキルダメージが" + v2 + "％増加し、" + numStr + "攻撃力が" + v1 + "％上昇";
                    }
                    else
                    {
                        outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、スキルダメージが" + v2 + "％増加";
                    }
                    return outStr;

                }

            case "攻撃力上昇し、対ボス攻撃力上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }

                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、ボス敵に対して攻撃力が" + v2 + "％上昇";
                    return outStr;
                }

            case "攻撃力上昇し、対ボスダメ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }

                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、ボスに対して与えるダメージが" + v2 + "％増加";
                    return outStr;
                }

            case "攻撃力上昇し、敵全体の攻撃力低下":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }
                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、敵全体の攻撃力が" + v2 + "％低下";
                return outStr;

            case "攻撃力上昇し、敵3体が攻撃ミス":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }
                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、敵3体が" + v2 + "％の確率で攻撃ミス";
                return outStr;

            case "攻撃力上昇し、回避":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }

                int v3 = 0;
                switch (v2)
                {
                    case 80:
                        {
                            v3 = 50;
                            break;
                        }
                }

                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、自身が" + "2T目まで" + v2 + "％、以降は" + v3 + "％の確率で回避";
                return outStr;

            case "攻撃力上昇し、再行動":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }
                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、自身が" + v2 + "％の確率で再行動";
                return outStr;

            case "攻撃力上昇し、移動力追加":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }
                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、パーティの移動力が" + v2 + "増加";
                return outStr;

            case "攻撃力上昇し、ソラ効果上昇":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }
                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、ソーラードライブの効果が" + v2 + "％上昇";
                return outStr;

            case "攻撃力上昇し、光ゲージ充填":
                if (turn == 1)
                {
                    turnStr = "1T目の";
                }

                switch (num)
                {
                    case 1:
                        {
                            numStr = "自身の";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            numStr = "自身を含む" + num + "人の";
                            break;
                        }
                    case 5:
                        {
                            numStr = "PT全体の";
                            break;
                        }
                }
                outStr = turnStr + numStr + "攻撃力が" + v1 + "％上昇し、光GAUGEが" + v2 + "％溜まった状態から討伐開始";
                return outStr;


            case "スキル発動毎に攻撃力上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = "スキル発動毎に" + numStr + "攻撃力が" + v1 + "％上昇";
                    return outStr;
                }

            case "属性種類数により攻撃力上昇":
                {
                    outStr = "PT全体の属性種類が多いほど攻撃力が上昇。1属性で" + v1 + "％、2属性で" + v1 * 2 + "％、3属性以上で" + v1 * 3 + "％";
                    return outStr;
                }

            case "防御力に応じて攻撃力上昇":
                {
                    outStr = "PTメンバーの攻撃力に、それぞれの防御力の" + v1 + "％を追加";
                    return outStr;

                }

            case "PTメンバーの数で攻撃力上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力が戦闘開始時のメンバーの数ｘ" + v1 + "％上昇";
                    return outStr;
                }
            case "ダメージ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "与ダメージが" + v1 + "％増加";
                    return outStr;
                }

            case "HP割合ダメ上昇率"://要データ　50%以外のパターン持ち不明
                {
                    numStr = "自身の";
                    string v1Sub = "";
                    switch (v1)
                    {
                        case 50:
                            v1Sub = "1/2";
                            break;
                    }
                    outStr = numStr + "現在のHPの" + v1Sub + "の割合で" + numStr + "与ダメージが増加";
                    return outStr;
                }

            case "ターン毎ダメージ上昇"://パフィオペディルム
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "与ダメージがターン毎に" + v1 + "％増加　最大" + v2 + "％"; 
                    return outStr;
                }

            case "弱点属性の敵に対するダメージ増加":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "弱点属性の敵に対するダメージが" + v1 + "％増加";
                    return outStr;

                }
                case "移動力を攻撃力に追加":
                {
                    outStr = "PT全体の攻撃力に移動力の" + v1 + "％を追加";
                    return outStr;
                }

            case "防御ダメ軽減率上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    double v2D = (double)v2 / 10;
                    outStr = numStr + "防御力が" + v1 + "％上昇し、防御時のダメージ軽減率を" + v2D + "％軽減、3回GUTS発動";
                    return outStr;
                }

            case "クリ率上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "クリティカル発生率が" + v1 + "％上昇";
                    return outStr;
                }

            case "クリダメ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "クリティカルダメージが" + v1 + "％増加";
                    return outStr;
                }

            case "クリダメ上昇し自身がさらに上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "クリティカルダメージが" + v1 + "％増加し、自信のクリティカルダメージが" + v2 + "％増加";
                    return outStr;
                }

            case "クリ率クリダメ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = turnStr + numStr + "クリティカル率が" + v1 + "％上昇しクリティカルダメージが" + v2 + "％増加";
                    return outStr;
                }

            case "スキル発動率上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    double v1D = (double)(v1 + 100) / 100;
                    outStr = turnStr + numStr + "スキル発動率が" + v1D + "倍";
                    return outStr;
                }

            case "スキル発動率1T目と3T目":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    double v1D = (double)(v1 + 100) / 100;
                    double v2D = (double)(v2 + 100) / 100;
                    outStr = turnStr + numStr + "スキル発動率が" + v1D + "倍、3T目のスキル発動率が" + v2D + "倍";
                    return outStr;
                }

            case "スキル発動率上昇し、対ボスダメ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    double v1D = (double)(v1 + 100) / 100;
                    outStr = turnStr + numStr + "スキル発動率が" + v1D + "倍、ボスに与えるダメージが" + v2 + "％増加";
                    return outStr;
                }

            case "スキルLVでスキル発動率上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    double v1D = (double)(v1 + 100) / 100;
                    double v2D = (double)(v2 + 100) / 100;
                    outStr = numStr + "スキル発動率が自身のスキルレベルに応じ" + v1D + "～" + v2D +"倍";
                    return outStr;
                }

            case "スキルダメ上昇":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目の";
                    }
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "スキルダメージが" + v1 + "％増加";
                    return outStr;
                }

            case "PTと自身スキルダメ上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "スキルダメージが" + v1 + "％増加し、更に自身のスキルダメージが" + v2 + "％増加";
                    return outStr;
                }

            case "対ボスダメ上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "ボスに与えるダメージが" + v1 + "％増加";
                    return outStr;
                }

            case "対ボスダメ上昇自身のボスダメ上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = numStr + "ボスに与えるダメージが" + v1 + "％増加し自身のボスに与えるダメージが" + v2 + "％増加";
                    return outStr;



                }
            case "対ボス攻撃力上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = "ボス敵に対して" + numStr + "攻撃力が" + v1 + "％上昇";
                    return outStr;
                }

            case "対ボス攻撃力上昇し、自身が更に上昇":
                {
                    outStr = "ボス敵に対してPT全体の攻撃力が" + v1 + "％上昇し、自信がさらに" + v2 + "％上昇";
                    return outStr;
                }

            case "対ボス攻撃力上昇し、自身を含む2人がさらに上昇":
                {
                    outStr = "ボス敵に対してPT全体の攻撃力が" + v1 + "％上昇し、自信を含む2人がさらに" + v2 + "％上昇" ;
                    return outStr;
                }


            case "対ボス攻撃力ダメ上昇":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身の";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人の";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体の";
                                break;
                            }
                    }
                    outStr = "ボス敵に対して" + numStr + "攻撃力が" + v1 + "％上昇し、与ダメージが" + v2 + "％増加";
                    return outStr;
                }

            case "再行動":
                {
                    if (turn == 1)
                    {
                        turnStr = "1T目に";
                    }

                    outStr = turnStr + "自身が敵にダメージを与えた後" + v1 + "％の確率で自身が再行動";
                    return outStr;
                }

            case "回避":
                {
                    outStr = "2T目まで" + v1 + "％、以降は" + v2 + "％の確率で回避";
                    return outStr;
                }

            case "反撃"://超反撃について調査要
                {
                    double v2D = (double) v2 / 100;
                    outStr = "攻撃を受けた時" + v1 + "％の確率で防御力の" +　v2D +  "倍を攻撃力に変換し反撃";
                    return outStr;
                }

            case "追撃":
                {
                    outStr = "自身の攻撃後、自身はPT総合力の" + v1 + "％を攻撃力に変換し追撃";
                    return outStr;
                }

            case "ソラ効果上昇":
                {
                    outStr = "ソーラードライブの効果が" + v1 + "％上昇";
                    return outStr;
                }

            case "光ゲージ充填":
                {
                    outStr = "光GAUGEが" + v1 + "％溜まった状態から討伐開始";
                    return outStr;
                }

            case "シャイクリ泥率上昇":
                {
                    outStr = "シャインクリスタルのドロップ率が" + v1 + "％上昇";
                    return outStr;
                }

            case "ソラ効果シャイクリ泥率上昇":
                {
                    outStr = "ソーラードライブの効果が" + v1 + "％上昇しシャインクリスタルドロップ率が" + v2 + "％上昇";
                    return outStr;
                }

            case "ソラ効果光ゲージ充填上昇":
                {
                    outStr = "ソーラードライブの効果が" + v1 + "％上昇し光GAUGEが" + v2 + "％溜まった状態から討伐開始";
                    return outStr;
                }

            case "ソラ発動毎に攻撃力上昇":
                {
                    outStr = "ソーラードライブ発動毎に攻撃力が" + v1 + "％上昇";
                    return outStr;
                }

            case "ソラ発動毎にダメ上昇":
                {
                    outStr = "ソーラードライブ発動毎に与ダメージが" + v1 + "％増加。最大" + v2 + "％";
                    return outStr;
                }

            case "ダメ無効":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身が";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                numStr = "自身を含む" + num + "人が";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PTメンバーが";
                                break;
                            }
                    }
                    outStr = numStr + "それぞれ1回ダメージを無効化";
                    return outStr;
                }

            case "攻撃力低下":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "敵1体の" +
                                    "";
                                break;
                            }
                        case 2:
                            {
                                numStr = "敵2体の" +
                                    "";
                                break;
                            }
                        case 3:
                            {
                                numStr = "敵3体の";
                                break;
                            }
                        case 4:
                        case 5:
                            {
                                numStr = "敵全体の";
                                break;
                            }
                    }
                    outStr = numStr + "攻撃力を" + v1 + "％低下";
                    return outStr;
                }

            case "スキル発動率低下":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "敵1体の";
                                break;
                            }
                        case 2:
                            {
                                numStr = "敵2体の";
                                break;
                            }
                        case 3:
                            {
                                numStr = "敵3体の";
                                break;
                            }
                        case 4:
                        case 5:

                            {
                                numStr = "敵全体の";
                                break;
                            }
                    }
                    outStr = numStr + "スキル発動率を" + v1 + "％低下";
                    return outStr;
                }

            case "攻撃ミス":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "敵1体が";
                                break;
                            }
                        case 2:
                            {
                                numStr = "敵2体が";
                                break;
                            }
                        case 3:
                            {
                                numStr = "敵3体が";
                                break;
                            }
                        case 4:
                        case 5:

                            {
                                numStr = "敵全体が";
                                break;
                            }
                    }
                    outStr = numStr + v1 + "％の確率で攻撃ミス";
                    return outStr;
                }

            case "属性付与":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                numStr = "自身に";
                                break;
                            }
                        case 2:
                        case 3:
                        case 4:
                            {
                                num --;
                                numStr = "自身と他の" + num + "人に";
                                break;
                            }
                        case 5:
                            {
                                numStr = "PT全体に";
                                break;
                            }
                    }
                    //属性による場合分け
                    string numAtt = "";
                    switch (v1)
                    {
                        case 1://1属性
                            {
                                numAtt = Ex2;
                                break;
                            }
                        case 2://2属性
                            {
                                switch (Ex2)
                                {
                                    case "斬打":
                                        {
                                            numAtt = "斬、打";
                                            break;
                                        }
                                    case "斬突":
                                        {
                                            numAtt = "斬、突";
                                            break;
                                        }
                                    case "斬魔":
                                        {
                                            numAtt = "斬、魔";
                                            break;
                                        }
                                    case "打突":
                                        {
                                            numAtt = "打、突";
                                            break;
                                        }
                                    case "打魔":
                                        {
                                            numAtt = "打、魔";
                                            break;
                                        }
                                    case "突魔":
                                        {
                                            numAtt = "突、魔";
                                            break;
                                        }
                                }
                                break;
                            }
                        case 3://3属性
                            switch (Ex2)
                            {
                                case "斬打突":
                                    {
                                        numAtt = "斬、打、突";
                                        break;
                                    }
                                case "斬打魔":
                                    {
                                        numAtt = "斬、打、魔";
                                        break;
                                    }
                                case "斬突魔":
                                    {
                                        numAtt = "斬、突、魔";
                                        break;
                                    }
                                case "打突魔":
                                    {
                                        numAtt = "打、突、魔";
                                        break;
                                    }
                            }
                            break;
                       
                    }
                    outStr = numStr + numAtt + "属性を付与";
                    return outStr;
                }

            case "HP回復":
                {
                    switch (num)
                    {
                        case 1:
                            {
                                outStr = "毎ターン" + v1 + "％の確率で自身の最大HPの"+ v2 + "％回復";
                                break;
                            }
                        case 3:
                            {
                                outStr = "自身のHPが0になった場合、1度だけ敵のターン終了後、自身を含む3人のHPを" + v2 + "％回復";
                                break;
                            }
                        case 5:
                            {
                                outStr = "PT全体がそれぞれ毎ターン" + v1 + "％の確率で最大HPの" + v2 + "％回復";
                                break;
                            }
                    }
                    return outStr;
                }

            case "PT移動力増加":
                {
                    outStr = "パーティの移動力が" + v1 + "増加";
                    return outStr;
                }

            case "PT移動力増加し、移動力を攻撃力に追加":
                {
                    outStr = "PTの移動力が" + v1 + "増加し、PT全体の攻撃力に移動力の" + v2 + "％を追加";
                    return outStr;
                }

            case "自身が攻撃を受けた次Tにスキル発動率上昇":
                {
                    double v1D = (double)(v1 + 100) / 100;
                    outStr = "自身が攻撃を受けた次ターンにスキル発動率" + v1D + "倍";
                    return outStr;
                }

            case "自身が攻撃を受けた次Tに攻撃力上昇":
                {
                    outStr = "自身が攻撃を受けた次ターンに攻撃力" + v1 + "％上昇";
                    return outStr;
                }

            case "自身が攻撃を受けた次Tにダメ上昇":
                {
                    outStr = "自身が攻撃を受けた次ターンにダメージ" + v1 + "％増加";
                    return outStr;
                }

            case "その他(MAP画面スキル)":
                {
                    outStr = "MAP画面用アビリティ。現在表示出来ない";
                    return outStr;
                }
        }

        return "未登録アビ";
    }

    string OutSkillExplain(string sType, int sRatio, string fkgName)
    {
        string outExplain = "";
        double sRatioD = 0;
        switch (sType)
        {
            case "全体":
                {
                    sRatioD = (double)sRatio / 10;
                    if (fkgName == "ラベンダー　昇華")
                    {
                        outExplain += "敵4体に" + sRatioD + "倍";
                    }
                    else
                    {
                        outExplain += "敵全体に" + sRatioD + "倍";
                    }
                    return outExplain;
                }
            case "2体":
                {
                    sRatioD = (double)sRatio / 10;
                    outExplain += "敵2体に" + sRatioD + "倍";
                    return outExplain;
                }
            case "変則":
                {
                    if (sRatio == 47)
                    {
                        outExplain += "敵単体に4.7倍、敵2体に2.8倍、敵3体に2.2倍";
                    }
                    else
                    {
                        outExplain = "未定義";
                    }
                    return outExplain;
                }
            case "複数回":
                {
                    sRatioD = (double)sRatio / 10;
                    if (fkgName == "アネモネ")
                    {
                        outExplain += "敵に3回" + sRatioD + "倍、敵全体に0.5倍";
                    }
                    else
                    {
                        outExplain += "敵に3回" + sRatioD + "倍";
                    }
                    return outExplain;
                }
            case "吸収":
                {
                    sRatioD = (double)sRatio / 10;
                    outExplain += "敵単体に" + sRatioD + "倍し吸収";
                    return outExplain;
                }
            case "単体":
                {
                    sRatioD = (double)sRatio / 10;
                    outExplain += "敵単体に" + sRatioD + "倍";
                    return outExplain;
                }
            default:
                outExplain = "未定義";
                return outExplain;
        }

    }

    //文字列が数字かどうか確認
    public static bool IsAsciiDigit(char c)
    {
        return '0' <= c && c <= '9';
    }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
        //全チェック
    {
        CheckBox9.Checked = true;
        CheckBox10.Checked = true;
        CheckBox11.Checked = true;
        CheckBox12.Checked = true;
        CheckBox13.Checked = true;
        CheckBox14.Checked = true;


    }

    protected void Button3_Click(object sender, EventArgs e)
        //チェック外し
    {
        CheckBox9.Checked = false;
        CheckBox10.Checked = false;
        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
        CheckBox13.Checked = false;
        CheckBox14.Checked = false;

    }
}