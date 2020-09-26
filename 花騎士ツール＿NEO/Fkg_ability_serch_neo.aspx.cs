using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 花騎士ツール＿NEO
{
    public partial class Fkg_ability_serch : System.Web.UI.Page
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
            //クッキー調べて登録人数を表示する関数
            IniciarAbilitySerch();
        }

        
        protected void Button20_Click(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        //
        //メイン計算処理
        //
        private void Main_Caluc()
        { 
            //初期状態チェック
            //属性フィルタ
            int countAtt = 0;
            foreach (ListItem item in CheckBoxList2.Items)
            {
                if (item.Selected)
                {
                    countAtt++;
                }
            }
            if (countAtt == 0)
            {
               CounterNum.Text = "少なくとも一つは属性にチェック入れて下さい。処理を終了します。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }
            //スキルフィルタ
            int countSkill = 0;
            foreach (ListItem item in CheckBoxList3.Items)
            {
                if (item.Selected)
                {
                    countSkill++;
                }
            }
            if (countSkill == 0)
            {
                CounterNum.Text = "少なくとも一つはスキルタイプにチェック入れて下さい。処理を終了します。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }

            //付与属性フィルタ
            int countAddAtt = 0;
            foreach (ListItem item in CheckBoxList4.Items)
            {
                if (item.Selected)
                {
                    countAddAtt++;
                }
            }
            if (countAddAtt == 4)
            {
                CounterNum.Text = "4属性付与を持つ花騎士は存在しません。処理を終了します。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }

            //未選択の場合の処理
            if ((this.DropDownList1.SelectedValue == "選択無し") && (this.DropDownList2.SelectedValue == "選択無し") && (this.DropDownList3.SelectedValue == "選択無し"))
            {
                //メッセージボックス表示
                //string script1 = "<script language=javascript>" + "window.alert('アビリティを選択して下さい。処理を終了します')" + "</script>";
                //Response.Write(script1);
                CounterNum.Text = "選択アビリティを最低一つ選択して下さい。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();


                return;
            }

            //登録キャラのみから選択のチェックの場合の処理
            foreach (ListItem item in RadioButtonList2.Items)
            {
                if (item.Selected)
                {
                    if ((item.Value == "1") || (item.Value == "2"))
                    {
                        //クッキーが存在しなければ、何も処理しない
                        if (HttpContext.Current.Request.Cookies["FkgName"] == null)
                        {
                            CounterNum.Text = "クッキーに誰も登録されていないので処理出来ません。";
                            this.ListView1.DataBind();
                            this.ListView2.DataBind();
                            return;
                        }
                    }
                }
            }

            

            //クエリ作成
            string get_query = "";
            get_query = "SELECT Id,Name,Rarity,ATT,A1st1,A1NO,A1Ex1,A1V1,A1V2,A1Ex2,A2st1,A2NO,A2Ex1,A2V1,A2V2,A2Ex2,A3st1,A3NO,A3Ex1,A3V1,A3V2,A3Ex2,A4st1,A4NO,A4Ex1,A4V1,A4V2,A4Ex2,SType,SRatio " +
                "FROM [dbo].[Fkgmbr]";
            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(get_query);
            DataTable dt_fkg0 = ds_fkg.Tables[0];
            DataTable dt_fkg = new DataTable();


            //登録済みフィルタ処理
            foreach (ListItem item in RadioButtonList2.Items)
            {
                if (item.Selected)
                {
                    switch (Convert.ToInt32(item.Value))
                    {
                        case 1:
                        case 2:
                            dt_fkg0 = GetDataFilterCookie(dt_fkg0, Convert.ToInt32(item.Value));
                            break;
                        case 0:
                            break;
                    }
                }
            }


            

            //レアリティフィルタ処理
            dt_fkg0 = GetDataFilterRare(dt_fkg0);
            //スキルタイプフィルタ処理
            dt_fkg0 = GetDataFilterSType(dt_fkg0);

            //データテーブルが空の場合
            if (dt_fkg0 == null)
            {
                CounterNum.Text = "検索結果は見つかりませんでした。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }

            //属性フィルタ処理
            dt_fkg = GetDataFilterAtt(dt_fkg0);
            //データテーブルが空の場合
            if (dt_fkg == null)
            {
                CounterNum.Text = "検索結果は見つかりませんでした。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }

            
            //LINQ用クエリ生成ルーチン
            //　選択された値からWHERE句のパーツ生成

            string[,] requestWord = new string[4,2];
            string[] fieldStr = new string[4];

            int[] selectValue = new int[4];
            int[] inputValue = new int[3];

            int[] requestValue = new int[3];//検索値


            requestWord[0,0] = this.DropDownList1.SelectedValue;
            requestWord[1,0] = this.DropDownList2.SelectedValue;
            requestWord[2,0] = this.DropDownList3.SelectedValue;
            //除外ワード
            requestWord[3,0] = this.DropDownList4.SelectedValue;

            //検索値の取得
            //全角半角変換
            Regex re = new Regex("[０-９]+");
            requestWord[0, 1] = Regex.Replace(this.TextBox101.Text, "[０-９]", delegate (Match m) {
                char ch = (char)('0' + (m.Value[0] - '０'));
                return ch.ToString();
            });
            requestWord[1, 1] =  Regex.Replace(this.TextBox102.Text, "[０-９]", delegate (Match m) {
                char ch = (char)('0' + (m.Value[0] - '０'));
                return ch.ToString();
            });
            requestWord[2, 1] =  Regex.Replace(this.TextBox103.Text, "[０-９]", delegate (Match m) {
                    char ch = (char)('0' + (m.Value[0] - '０'));
                    return ch.ToString();
            });



            //ストリング分析してパーツ生成
            for (int i = 0; i < 4; i++)
            {
                switch (requestWord[i,0])
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
                    case "攻撃を受けた次ターンにスキル発動率上昇":
                        {
                            fieldStr[i] = "自身が攻撃を受けた次Tにスキル発動率上昇";
                            break;
                        }
                    case "スキルLVによりスキル発動率上昇":
                        {
                            fieldStr[i] = "スキルLVでスキル発動率上昇";
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
                            if(requestWord[i,1]!="0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
                            break;
                        }
                    case "クリティカルダメージ増加"://クリタエちゃん問題ある
                                        //"クリ率クリダメ上昇"
                        {
                            fieldStr[i] = "クリダメ上昇";
                            if (requestWord[i, 1] != "0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
                            break;
                        }
                    case "クリティカル率上昇（PT全体対象）"://クリタエちゃん問題ある
                                     //"クリ率クリダメ上昇"
                        {
                            fieldStr[i] = "クリ率上昇";
                            selectValue[i] = 5;
                            if (requestWord[i, 1] != "0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
                            break;
                        }
                    case "クリティカルダメージ増加（PT全体対象）":
                        {
                            fieldStr[i] = "クリダメ上昇";
                            selectValue[i] = 5;
                            if (requestWord[i, 1] != "0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
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
                    case "ターン毎ダメージ増加":
                        {
                            fieldStr[i] = "ターン毎ダメージ上昇";
                            break;
                        }
                    case "回避":
                        {
                            fieldStr[i] = "回避";
                            break;
                        }
                    case "迎撃":
                        {
                            fieldStr[i] = "迎撃";
                            break;
                        }
                    case "反撃":
                        {
                            fieldStr[i] = "反撃とか";
                            break;
                        }
                    case "反撃（超反撃有）":
                        {
                            fieldStr[i] = "超反撃";
                            break;
                        }
                    case "再行動":
                        {
                            fieldStr[i] = "再行動";
                            if (requestWord[i, 1] != "0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
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
                            if (requestWord[i, 1] != "0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
                            break;
                        }
                    case "防御力低下":
                        {
                            fieldStr[i] = "防御力低下";
                            break;
                        }
                    case "スキル発動率低下":
                        {
                            fieldStr[i] = "スキル発動率低下";
                            if (requestWord[i, 1] != "0")
                            {
                                //さらに数字であるか確認
                                if (int.TryParse(requestWord[i, 1], out int test))
                                {
                                    requestValue[i] = Convert.ToInt32(requestWord[i, 1]);
                                }
                            }
                            break;
                        }
                    case "命中率低下":
                        {
                            fieldStr[i] = "攻撃ミス";
                            break;
                        }
                    case "行動回数減少":
                        {
                            fieldStr[i] = "ターン毎に行動回数減";
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
                    case "弱点属性ダメ増加":
                        {
                            fieldStr[i] = "弱点属性の敵に対するダメージ増加";
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


            //フィルタメイン処理をかける
            //準備　発見したアビがfieldStr何番に当たるか記憶するカラム追加(keyと呼称)
            dt_fkg.Columns.Add("Key1", typeof(int));
            dt_fkg.Columns.Add("Key2", typeof(int));
            dt_fkg.Columns.Add("Key3", typeof(int));

            //データテーブル取得
            DataTable dt_fkg_out = GetDataFilter(ref fieldStr, ref selectValue, ref inputValue, ref requestValue, dt_fkg);
            if (dt_fkg_out == null)
            {
                //メッセージボックス表示
                CounterNum.Text = "検索結果は見つかりませんでした。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }

            //付与属性フィルタによる
            dt_fkg_out = GetDataFilterAddAtt(dt_fkg_out);
            if (dt_fkg_out == null)
            {
                //メッセージボックス表示
                CounterNum.Text = "検索結果は見つかりませんでした。";
                this.ListView1.DataBind();
                this.ListView2.DataBind();
                return;
            }

            string resultMessage = "検索結果 " + dt_fkg_out.Rows.Count + "件";

            DataTable dt_fkg_out_exclusive;
            
            //除外処理
            if (fieldStr[3] != "")
            {
                dt_fkg_out_exclusive = GetDataFilterExclusion(fieldStr[3],selectValue[3], dt_fkg_out);

                //データテーブルが空の場合//恐らくここには来ない。nullで返ってくることはあり得ない
                if (dt_fkg_out_exclusive == null)
                {
                    //メッセージボックス表示
                    //string script1 = "<script language=javascript>" + "window.alert('アビ除外失敗！該当する花騎士を見つけることが出来ませんでした。処理を終了します')" + "</script>";
                    //Response.Write(script1);
                    CounterNum.Text = "検索結果は見つかりませんでした。";
                    this.ListView1.DataBind();
                    this.ListView2.DataBind();
                    return;
                }

                //データテーブルが、除外する前と同じ場合
                if (dt_fkg_out.Rows.Count == dt_fkg_out_exclusive.Rows.Count)
                {
                    //リザルトメッセージ表示
                    resultMessage += "  除外データ  0件";
                }
                else
                {//アビ除外成功した場合
                    
                    //リザルトメッセージ表示
                    int diferencia = dt_fkg_out.Rows.Count - dt_fkg_out_exclusive.Rows.Count;
                    resultMessage = "表示した検索結果 " + dt_fkg_out_exclusive.Rows.Count + "件" + "（除外データ  " + diferencia + "件）";

                    //アビ除外成功した場合は代入する
                    dt_fkg_out = dt_fkg_out_exclusive;
                }

            }

            CounterNum.Text = resultMessage;


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
            dt_fkg_out.Columns.Add("SRatioRevMovil", typeof(string));

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
            for (int i = 0; i < dt_fkg_out.Rows.Count; i++)
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

                            //アビの値比較用
                            string[] A = new string[4];
                            int[] AV = new int[4];

                            for (int j = 0; j < 4; j++)
                            {
                                for (int m = 0; m < count; m++)//mは検索Keyナンバー
                                {
                                    string pattern = @"\s*(?<!対ボス|防御力に応じて|ターンで)攻撃力上昇\s*";
                                    //基本的に複合アビに対して必要な部分
                                    //ソートにA1V1を使うタイプ
                                    if ((findKey[m] == outAEx[j])
                                         || ((findKey[m] == "攻撃力上昇") && (Regex.IsMatch(outAEx[j].ToString(), pattern)))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキルLVでスキル発動率上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、対ボス攻撃力上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、対ボスダメ上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、自身のスキルダメ上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、ダメ無効"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、シャイクリ泥率上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率上昇し、敵全体の攻撃力低下"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキル発動率1T目と3T目"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "自身が攻撃を受けた次Tにスキル発動率上昇"))
                                         || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇"))
                                         || ((findKey[m] == "クリ率上昇") && (outAEx[j] == "クリ率クリダメ上昇"))
                                         || ((findKey[m] == "クリダメ上昇") && (outAEx[j] == "クリダメ上昇し自身がさらに上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "PTと自身スキルダメ上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、対ボスダメ上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、シャイクリ泥率上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、スキル発動率上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、スキルLVでスキル発動率上昇"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキルダメ上昇し、ダメ無効"))
                                         || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "MAP画面アビと、スキルダメ上昇"))
                                         || ((findKey[m] == "対ボス攻撃力上昇") && (outAEx[j] == "対ボス攻撃力ダメ上昇"))
                                         || ((findKey[m] == "対ボス攻撃力上昇") && (outAEx[j] == "対ボス攻撃力上昇し、自身が更に上昇"))
                                         || ((findKey[m] == "対ボス攻撃力上昇") && (outAEx[j] == "対ボス攻撃力上昇し、自身を含む2人がさらに上昇"))
                                         || ((findKey[m] == "光ゲージ充填") && (outAEx[j] == "光ゲージ充填シャイクリ泥率上昇"))
                                         || ((findKey[m] == "光ゲージ充填") && (outAEx[j] == "光ゲージ充填し、自身が再行動"))
                                         || ((findKey[m] == "ソラ効果上昇") && (outAEx[j] == "ソラ効果シャイクリ泥率上昇"))
                                         || ((findKey[m] == "ソラ効果上昇") && (outAEx[j] == "ソラ効果光ゲージ充填上昇"))
                                         || ((findKey[m] == "ソラ効果上昇") && (outAEx[j] == "ソラ効果上昇し自身が再行動"))
                                         || ((findKey[m] == "攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、自信を含む2人がさらに上昇"))
                                         || ((findKey[m] == "攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、自信を含む3人がさらに上昇"))
                                         || ((findKey[m] == "攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、スキル発動率上昇"))
                                         || ((findKey[m] == "対ボスダメ上昇") && (outAEx[j] == "対ボスダメ上昇自身のボスダメ上昇"))
                                         || ((findKey[m] == "対ボスダメ上昇") && (outAEx[j] == "対ボスダメ上昇し、敵の数で攻撃力上昇"))
                                         || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "ターン毎ダメージ上昇"))
                                         || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "ソラ発動毎にダメ上昇"))
                                         || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "自身が攻撃を受けた次Tにダメ上昇"))
                                         || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "HP割合ダメ上昇率"))
                                         || ((findKey[m] == "弱点属性の敵に対するダメージ増加") && (outAEx[j] == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇"))
                                         || ((findKey[m] == "PT移動力増加") && (outAEx[j] == "PT移動力増加し、スキルLVでスキル発動率上昇"))
                                         || ((findKey[m] == "PT移動力増加") && (outAEx[j] == "PT移動力増加し、移動力を攻撃力に追加"))
                                         || ((findKey[m] == "PT移動力増加") && (outAEx[j] == "PT移動力増加し、対ボス攻撃力上昇"))
                                         || ((findKey[m] == "PT移動力増加") && (outAEx[j] == "MAP画面アビと、移動力増加"))
                                         || ((findKey[m] == "PT移動力増加") && (outAEx[j] == "PT移動力増加し、自身が再行動"))
                                         || ((findKey[m] == "回避") && (outAEx[j] == "回避し、残りHPで攻撃力上昇"))
                                         || ((findKey[m] == "回避") && (outAEx[j] == "回避し、敵3体が攻撃ミス"))
                                         || ((findKey[m] == "回避") && (outAEx[j] == "回避し、反撃"))
                                         || ((findKey[m] == "追撃") && (outAEx[j] == "追撃し、スキル発動率上昇"))
                                         || ((findKey[m] == "追撃") && (outAEx[j] == "追撃し、スキルLVでスキル発動率上昇"))
                                         || ((findKey[m] == "追撃") && (outAEx[j] == "追撃し、自身が再行動"))
                                         || ((findKey[m] == "再行動") && (outAEx[j] == "PTに再行動付与"))
                                         || ((findKey[m] == "再行動") && (outAEx[j] == "自身が再行動し、シャイクリ泥率上昇"))
                                         || ((findKey[m] == "自身が攻撃を受けた次Tにスキル発動率上昇") && (outAEx[j] == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇"))
                                         )

                                    {//if文の中身
                                        A[j] = outAOri[j];
                                        AV[j] = outAVOri[j];

                                        //検索Keyに対応するアビリティ番号を代入
                                        switch (m)
                                        {
                                            case 0:
                                                {
                                                    dt_fkg_out.Rows[i]["Key1"] = j + 1;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    dt_fkg_out.Rows[i]["Key2"] = j + 1;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    dt_fkg_out.Rows[i]["Key3"] = j + 1;
                                                    break;
                                                }
                                        }
                                    }

                                    //ソートにA1V2を使うタイプ
                                    else if ((findKey[m] == outAEx[j])
                                     || ((findKey[m] == "反撃とか") && (outAEx[j] == "反撃"))
                                     || ((findKey[m] == "反撃とか") && (outAEx[j] == "回避し、反撃"))
                                     || ((findKey[m] == "超反撃") && (outAEx[j] == "反撃"))
                                     || ((findKey[m] == "防御力に応じて攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、防御力に応じて攻撃力上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "攻撃力上昇し、スキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "攻撃力上昇し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "攻撃力上昇し、1T目のスキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキルダメ上昇し、スキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "スキルダメ上昇し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "追撃し、スキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "追撃し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキル発動率上昇") && (outAEx[j] == "PT移動力増加し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキルLVでスキル発動率上昇") && (outAEx[j] == "攻撃力上昇し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキルLVでスキル発動率上昇") && (outAEx[j] == "スキルダメ上昇し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキルLVでスキル発動率上昇") && (outAEx[j] == "PT移動力増加し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキルLVでスキル発動率上昇") && (outAEx[j] == "追撃し、スキルLVでスキル発動率上昇"))
                                     || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "攻撃力上昇し、スキルダメージ上昇"))
                                     || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキル発動率上昇し、自身のスキルダメ上昇"))
                                     || ((findKey[m] == "スキルダメ上昇") && (outAEx[j] == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇"))
                                     || ((findKey[m] == "クリ率上昇") && (outAEx[j] == "クリ率上昇さらに自身のクリ率とダメ上昇"))
                                     || ((findKey[m] == "クリダメ上昇") && (outAEx[j] == "クリ率クリダメ上昇"))
                                     || ((findKey[m] == "対ボス攻撃力上昇") && (outAEx[j] == "攻撃力上昇し、対ボス攻撃力上昇"))
                                     || ((findKey[m] == "対ボス攻撃力上昇") && (outAEx[j] == "スキル発動率上昇し、対ボス攻撃力上昇"))
                                     || ((findKey[m] == "対ボス攻撃力上昇") && (outAEx[j] == "PT移動力増加し、対ボス攻撃力上昇"))
                                     || ((findKey[m] == "対ボスダメ上昇") && (outAEx[j] == "攻撃力上昇し、対ボスダメ上昇"))
                                     || ((findKey[m] == "対ボスダメ上昇") && (outAEx[j] == "対ボス攻撃力ダメ上昇"))
                                     || ((findKey[m] == "対ボスダメ上昇") && (outAEx[j] == "スキル発動率上昇し、対ボスダメ上昇"))
                                     || ((findKey[m] == "対ボスダメ上昇") && (outAEx[j] == "スキルダメ上昇し、対ボスダメ上昇"))
                                     || ((findKey[m] == "ターン毎ダメージ上昇") && (outAEx[j] == "攻撃力上昇し、ターン毎にダメージ上昇"))
                                     || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "攻撃力上昇し、ターン毎にダメージ上昇"))
                                     || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "攻撃力上昇HP割合ダメ上昇"))
                                     || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇"))
                                     || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "クリ率上昇さらに自身のクリ率とダメ上昇"))
                                     || ((findKey[m] == "ダメージ上昇") && (outAEx[j] == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇"))
                                     || ((findKey[m] == "弱点属性の敵に対するダメージ増加") && (outAEx[j] == "攻撃力上昇し、弱点属性の敵に対するダメージ増加"))
                                     || ((findKey[m] == "シャイクリ泥率上昇") && (outAEx[j] == "光ゲージ充填シャイクリ泥率上昇"))
                                     || ((findKey[m] == "シャイクリ泥率上昇") && (outAEx[j] == "ソラ効果シャイクリ泥率上昇"))
                                     || ((findKey[m] == "シャイクリ泥率上昇") && (outAEx[j] == "攻撃力上昇し、シャイクリ泥率上昇"))
                                     || ((findKey[m] == "シャイクリ泥率上昇") && (outAEx[j] == "スキル発動率上昇し、シャイクリ泥率上昇"))
                                     || ((findKey[m] == "シャイクリ泥率上昇") && (outAEx[j] == "スキルダメ上昇し、シャイクリ泥率上昇"))
                                     || ((findKey[m] == "シャイクリ泥率上昇") && (outAEx[j] == "自身が再行動し、シャイクリ泥率上昇"))
                                     || ((findKey[m] == "ソラ効果上昇") && (outAEx[j] == "攻撃力上昇し、ソラ効果上昇"))
                                     || ((findKey[m] == "光ゲージ充填") && (outAEx[j] == "ソラ効果光ゲージ充填上昇"))
                                     || ((findKey[m] == "光ゲージ充填") && (outAEx[j] == "攻撃力上昇し、光ゲージ充填"))
                                     || ((findKey[m] == "攻撃力低下") && (outAEx[j] == "攻撃力上昇し、敵全体の攻撃力低下"))
                                     || ((findKey[m] == "攻撃力低下") && (outAEx[j] == "スキル発動率上昇し、敵全体の攻撃力低下"))
                                     || ((findKey[m] == "攻撃ミス") && (outAEx[j] == "攻撃力上昇し、敵3体が攻撃ミス"))
                                     || ((findKey[m] == "攻撃ミス") && (outAEx[j] == "回避し、敵3体が攻撃ミス"))
                                     || ((findKey[m] == "再行動") && (outAEx[j] == "攻撃力上昇し、再行動"))
                                     || ((findKey[m] == "再行動") && (outAEx[j] == "ソラ効果上昇し自身が再行動"))
                                     || ((findKey[m] == "再行動") && (outAEx[j] == "光ゲージ充填し、自身が再行動"))
                                     || ((findKey[m] == "再行動") && (outAEx[j] == "PT移動力増加し、自身が再行動"))
                                     || ((findKey[m] == "再行動") && (outAEx[j] == "追撃し、自身が再行動"))
                                     || ((findKey[m] == "回避") && (outAEx[j] == "攻撃力上昇し、回避"))
                                     || ((findKey[m] == "追撃") && (outAEx[j] == "攻撃力上昇し、追撃"))
                                     || ((findKey[m] == "ダメ無効") && (outAEx[j] == "スキルダメ上昇し、ダメ無効"))
                                     || ((findKey[m] == "ダメ無効") && (outAEx[j] == "スキル発動率上昇し、ダメ無効"))
                                     || ((findKey[m] == "ターンで攻撃力上昇") && (outAEx[j] == "攻撃力上昇ターンでさらに上昇"))
                                     || ((findKey[m] == "ターンで攻撃力上昇") && (outAEx[j] == "攻撃力上昇1T目さらに上昇"))
                                     || ((findKey[m] == "1ターン目系") && (outAEx[j] == "攻撃力上昇1T目さらに上昇"))
                                     || ((findKey[m] == "1ターン目系") && (outAEx[j] == "攻撃力上昇し、1T目のスキル発動率上昇"))
                                     || ((findKey[m] == "HP回復") && (outAEx[j] == "攻撃力上昇し、自身のHP回復(効果値はEx2)"))
                                     || ((findKey[m] == "PT移動力増加") && (outAEx[j] == "攻撃力上昇し、移動力追加"))
                                     || ((findKey[m] == "自身が攻撃を受けた次Tにスキル発動率上昇") && (outAEx[j] == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                     || ((findKey[m] == "自身が攻撃を受けた次Tにダメ上昇") && (outAEx[j] == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇"))
                                     )


                                    {
                                        A[j] = outAOri[j];
                                        AV[j] = outAV2Ori[j];

                                        //検索Keyに対応するアビリティ番号を代入
                                        switch (m)
                                        {
                                            case 0:
                                                {
                                                    dt_fkg_out.Rows[i]["Key1"] = j + 1;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    dt_fkg_out.Rows[i]["Key2"] = j + 1;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    dt_fkg_out.Rows[i]["Key3"] = j + 1;
                                                    break;
                                                }

                                        }
                                    }
                                    //例外　1ターン目系
                                    else if (findKey[m] == "1ターン目系" && outAOri[j].Contains("1T目"))
                                    {
                                        ////1T目スキルが何個あるかカウント
                                        //count1T++;
                                        //switch (count1T)
                                        //{//最大1T目スキルが3個まであると想定
                                        //    case 1:
                                        //        {
                                        //            index1st = j;
                                        //            break;
                                        //        }
                                        //    case 2:
                                        //        {
                                        //            index2nd = j;
                                        //            break;
                                        //        }
                                        //    case 3:
                                        //        {
                                        //            index3rd = j;
                                        //            break;
                                        //        }
                                        //}
                                        //index1st = j;

                                        //上からコピー
                                        A[j] = outAOri[j];
                                        AV[j] = outAVOri[j];

                                        //検索Keyに対応するアビリティ番号を代入
                                        switch (m)
                                        {
                                            case 0:
                                                {
                                                    dt_fkg_out.Rows[i]["Key1"] = j + 1;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    dt_fkg_out.Rows[i]["Key2"] = j + 1;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    dt_fkg_out.Rows[i]["Key3"] = j + 1;
                                                    break;
                                                }

                                        }//ここまでコピー

                                    }
                                    //スキルタイプでの検索の場合、検索キーは0を入れる
                                    else if ((findKey[m] == "全体" ) || (findKey[m] == "2体") || (findKey[m] == "複数回") || (findKey[m] == "変則") || (findKey[m] == "吸収") || (findKey[m] == "単体"))
                                    {
                                        A[j] = outAOri[j];
                                        AV[j] = outAVOri[j];

                                        //検索Keyに対応するアビリティ番号を代入
                                        switch (m)
                                        {
                                            case 0:
                                                {
                                                    dt_fkg_out.Rows[i]["Key1"] = 0;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    dt_fkg_out.Rows[i]["Key2"] = 0;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    dt_fkg_out.Rows[i]["Key3"] = 0;
                                                    break;
                                                }

                                        }
                                    }

                                    }//mループ終わり
                            }//jループ終わり

                            //アビの入れ替え処理
                            {
                                //同系統アビ所持時、その中で最大の値を選別
                                //int maxV = 0;
                                //for (int k = 0; k < 4; k++)
                                //{
                                //    if (AV[k] == AV.Max())
                                //    {
                                //        maxV = k;
                                //    }
                                //}
                                //outA1 = A[maxV];
                                //outA1V = AV[maxV];

                                int Key1 = Convert.ToInt32(dt_fkg_out.Rows[i]["Key1"]);
                                int Key2 = 0;
                                int Key3 = 0;

                                    if (!dt_fkg_out.Rows[i].IsNull("Key2"))
                                {
                                    Key2 = Convert.ToInt32(dt_fkg_out.Rows[i]["Key2"]);
                                }
                                if (!dt_fkg_out.Rows[i].IsNull("Key3"))
                                {
                                    Key3 = Convert.ToInt32(dt_fkg_out.Rows[i]["Key3"]);
                                }
                                

                                int[] index = GetOutAbiNo(Key1, Key2, Key3);

                                //outA1 = A[Key1 - 1];
                                //outA1V = AV[Key1 - 1];

                                outA1 = outAOri[index[0]];
                                outA2 = outAOri[index[1]];
                                outA3 = outAOri[index[2]];
                                outA4 = outAOri[index[3]];

                                outA1V = AV[index[0]];
                                outA2V = AV[index[1]];
                                outA3V = AV[index[2]];
                                outA4V = AV[index[3]];

                                //outA1V = outAVOri[index[0]];
                                //outA2V = outAVOri[index[1]];
                                //outA3V = outAVOri[index[2]];
                                //outA4V = outAVOri[index[3]];
                            }
                            //    switch (maxV)
                            //    {
                            //        case 0:
                            //            {
                            //                //第2検索Key以降があるか確認
                            //                if (count == 1)
                            //                {
                            //                    outA2 = outAOri[1];
                            //                    outA3 = outAOri[2];
                            //                    outA4 = outAOri[3];

                            //                    outA2V = outAVOri[1];
                            //                    outA3V = outAVOri[2];
                            //                    outA4V = outAVOri[3];
                            //                }
                            //                else if(count == 2)
                            //                {
                            //                    switch (Convert.ToInt32(dt_fkg_out.Rows[i]["Key2"]))
                            //                    {
                            //                        case 2:
                            //                            {
                            //                                outA2 = outAOri[1];
                            //                                outA3 = outAOri[2];
                            //                                outA4 = outAOri[3];

                            //                                outA2V = outAVOri[1];
                            //                                outA3V = outAVOri[2];
                            //                                outA4V = outAVOri[3];
                            //                                break;
                            //                            }
                            //                        case 3:
                            //                            {
                            //                                outA2 = outAOri[2];
                            //                                outA3 = outAOri[1];
                            //                                outA4 = outAOri[3];

                            //                                outA2V = outAVOri[1];
                            //                                outA3V = outAVOri[2];
                            //                                outA4V = outAVOri[3];
                            //                                break;
                            //                            }
                            //                    }

                            //                }
                            //                else if (count == 3)
                            //                {

                            //                }
                            //                break;
                            //            }
                            //        case 1:
                            //            {
                            //                outA2 = outAOri[0];
                            //                outA3 = outAOri[2];
                            //                outA4 = outAOri[3];

                            //                outA2V = outAVOri[0];
                            //                outA3V = outAVOri[2];
                            //                outA4V = outAVOri[3];
                            //                break;
                            //            }
                            //        case 2:
                            //            {
                            //                outA2 = outAOri[0];
                            //                outA3 = outAOri[1];
                            //                outA4 = outAOri[3];

                            //                outA2V = outAVOri[0];
                            //                outA3V = outAVOri[1];
                            //                outA4V = outAVOri[3];
                            //                break;
                            //            }
                            //        case 3:
                            //            {
                            //                outA2 = outAOri[0];
                            //                outA3 = outAOri[1];
                            //                outA4 = outAOri[2];

                            //                outA2V = outAVOri[0];
                            //                outA3V = outAVOri[1];
                            //                outA4V = outAVOri[2];
                            //                break;
                            //            }
                            //    }
                            //}

                            ////例外　1ターン目系　最大値抜き出し処理なし
                            //if (findKey[0] == "1ターン目系")
                            //{
                            //    outA1 = outAOri[index1st];
                            //    outA1V = outAVOri[index1st];
                            //    switch (index1st)
                            //    {
                            //        case 0:
                            //            {
                            //                outA2 = outAOri[1];
                            //                outA3 = outAOri[2];
                            //                outA4 = outAOri[3];

                            //                outA2V = outAVOri[1];
                            //                outA3V = outAVOri[2];
                            //                outA4V = outAVOri[3];
                            //                break;
                            //            }
                            //        case 1:
                            //            {
                            //                outA2 = outAOri[0];
                            //                outA3 = outAOri[2];
                            //                outA4 = outAOri[3];

                            //                outA2V = outAVOri[0];
                            //                outA3V = outAVOri[2];
                            //                outA4V = outAVOri[3];
                            //                break;
                            //            }
                            //        case 2:
                            //            {
                            //                outA2 = outAOri[0];
                            //                outA3 = outAOri[1];
                            //                outA4 = outAOri[3];

                            //                outA2V = outAVOri[0];
                            //                outA3V = outAVOri[1];
                            //                outA4V = outAVOri[3];
                            //                break;
                            //            }
                            //        case 3:
                            //            {
                            //                outA2 = outAOri[0];
                            //                outA3 = outAOri[1];
                            //                outA4 = outAOri[2];

                            //                outA2V = outAVOri[0];
                            //                outA3V = outAVOri[1];
                            //                outA4V = outAVOri[2];
                            //                break;
                            //            }
                            //    }
                            //}

                            //スキルタイプの場合
                            //if ((findKey[0] == "全体") || (findKey[0] == "2体") || (findKey[0] == "変則") || (findKey[0] == "複数回") || (findKey[0] == "吸収") || (findKey[0] == "単体"))
                            //{
                            //    //特別にアビ欄を入れ替えない
                            //    outA1 = outAOri[0];
                            //    outA2 = outAOri[1];
                            //    outA3 = outAOri[2];
                            //    outA4 = outAOri[3];

                            //    outA1V = outAVOri[0];
                            //    outA2V = outAVOri[1];
                            //    outA3V = outAVOri[2];
                            //    outA4V = outAVOri[3];
                            //}
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
                int movilFlag = 0;
                dt_fkg_out.Rows[i]["SRatioRev"] = OutSkillExplain(Convert.ToString(dt_fkg_out.Rows[i]["SType"]), Convert.ToInt32(dt_fkg_out.Rows[i]["SRatio"]), Convert.ToString(dt_fkg_out.Rows[i]["Name"]), movilFlag);
                movilFlag = 1;
                dt_fkg_out.Rows[i]["SRatioRevMovil"] = OutSkillExplain(Convert.ToString(dt_fkg_out.Rows[i]["SType"]), Convert.ToInt32(dt_fkg_out.Rows[i]["SRatio"]), Convert.ToString(dt_fkg_out.Rows[i]["Name"]),movilFlag);

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

            //
            //くっきーより所持キャラにマーカー追加
            //
            Fkg_select_neo Fkg_sel_neo = new Fkg_select_neo();
            dt_fkg_out2 = Fkg_sel_neo.MarkFromCookie(dt_fkg_out2);

            dt_fkg_out2 = AddATTcolor(dt_fkg_out2);

            //結果を表示
            ListView1.DataSource = dt_fkg_out2;
            ListView1.DataBind();

            ListView2.DataSource = dt_fkg_out2;
            ListView2.DataBind();




        }



        //
        //与えられたクエリ要素と、AND、ORの別より、データテーブル生成する関数
        //
        private DataTable GetDataFilter(ref string[] fieldStrIn, ref int[] selectValueIn, ref int[] inputValueIn,ref int[] requestValueIn, DataTable dt_input)
        {

            string[] fieldStr = new string[3];
            int[] requestValue = new int[3];

            int[] selectValue = new int[3];
            int[] inputValue = { 0, 0, 0 };


            //入力文字列の検証
            int countInput = 0;//検索Key個数
            //int countSpecial = 0;
            //ノーマルの場合のカウント
            for (int i = 0; i < 3; i++)
            {
                if (fieldStrIn[i] != "")
                {
                    fieldStr[countInput] = fieldStrIn[i];
                    selectValue[countInput] = selectValueIn[i];
                    requestValue[countInput] = requestValueIn[i];
                    countInput++;
                }
            }

            //検索文字列がない場合は検索しない
            if (countInput == 0)
            {
                return null;
            }

            for (int i = 0; i < 3; i++)
            {
                if (inputValueIn[i] != 0)
                {
                    inputValue[i] = inputValueIn[i];
                }
            }


            //LINQ処理
            DataTable[] dt = new DataTable[4];
            dt[0] = dt_input;

            for (int i = 0; i < 3; i++)
            {
                if (selectValue[i] == 0)//ユーザーによる数値入力が存在しない場合
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
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボスダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、ダメ無効") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、ダメ無効") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、ダメ無効") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、ダメ無効"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A2Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A3Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A4Ex1") == "スキル発動率1T目と3T目"))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "追撃し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "追撃し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "追撃し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "追撃し、スキル発動率上昇"))
                                                 select p;

                                //検索結果が無い場合
                                if (outputData.Count() == 0)
                                {
                                    return null;
                                }
                                
                                dt[i + 1] = outputData.CopyToDataTable();
                                //検索Key数チェック
                                if (countInput == (i + 1))
                                {
                                    return dt[i + 1];
                                }

                                break;
                            }

                        case "スキルLVでスキル発動率上昇":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "追撃し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "追撃し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "追撃し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "追撃し、スキルLVでスキル発動率上昇"))
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

                        case "自身が攻撃を受けた次Tにスキル発動率上昇":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (
                                                 (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇"))
                                                 )
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
                            //"クリ率クリダメ上昇"も引っ掛ける
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (
                                                 (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 )
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
                        
                        case "クリダメ上昇":
                        case "クリダメ上昇し自身がさらに上昇":
                            //"クリ率クリダメ上昇"も引っ掛ける
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (
                                                 (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 )
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
                                                 || ((p.Field<string>("A1Ex1") == "対ボスダメ上昇し、敵の数で攻撃力上昇") || (p.Field<string>("A2Ex1") == "対ボスダメ上昇し、敵の数で攻撃力上昇") || (p.Field<string>("A3Ex1") == "対ボスダメ上昇し、敵の数で攻撃力上昇") || (p.Field<string>("A4Ex1") == "対ボスダメ上昇し、敵の数で攻撃力上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "対ボスダメ上昇自身のボスダメ上昇") || (p.Field<string>("A2Ex1") == "対ボスダメ上昇自身のボスダメ上昇") || (p.Field<string>("A3Ex1") == "対ボスダメ上昇自身のボスダメ上昇") || (p.Field<string>("A4Ex1") == "対ボスダメ上昇自身のボスダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボスダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、対ボスダメ上昇"))
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
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、対ボス攻撃力上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、対ボス攻撃力上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、対ボス攻撃力上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、対ボス攻撃力上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、対ボス攻撃力上昇") || (p.Field<string>("A2Ex1") == "PT移動力増加し、対ボス攻撃力上昇") || (p.Field<string>("A3Ex1") == "PT移動力増加し、対ボス攻撃力上昇") || (p.Field<string>("A4Ex1") == "PT移動力増加し、対ボス攻撃力上昇"))
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
                                                 || (((p.Field<string>("A1Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A1V1") >= inputValue[i])) || ((p.Field<string>("A2Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A2V1") >= inputValue[i])) || ((p.Field<string>("A3Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A3V1") >= inputValue[i])) || ((p.Field<string>("A4Ex1") == "ソラ効果上昇し自身が再行動")) && (p.Field<int>("A4V1") >= inputValue[i]))
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
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "ソラ効果シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "ソラ効果シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "ソラ効果シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "ソラ効果シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "光ゲージ充填シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "光ゲージ充填シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "光ゲージ充填シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "光ゲージ充填シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "自身が再行動し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "自身が再行動し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "自身が再行動し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "自身が再行動し、シャイクリ泥率上昇"))
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
                                                 || ((p.Field<string>("A1Ex1") == "光ゲージ充填シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "光ゲージ充填シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "光ゲージ充填シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "光ゲージ充填シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "ソラ効果光ゲージ充填上昇") || (p.Field<string>("A2Ex1") == "ソラ効果光ゲージ充填上昇") || (p.Field<string>("A3Ex1") == "ソラ効果光ゲージ充填上昇") || (p.Field<string>("A4Ex1") == "ソラ効果光ゲージ充填上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、光ゲージ充填") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、光ゲージ充填") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、光ゲージ充填") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、光ゲージ充填"))
                                                 || ((p.Field<string>("A1Ex1") == "光ゲージ充填し、自身が再行動") || (p.Field<string>("A2Ex1") == "光ゲージ充填し、自身が再行動") || (p.Field<string>("A3Ex1") == "光ゲージ充填し、自身が再行動") || (p.Field<string>("A4Ex1") == "光ゲージ充填し、自身が再行動"))
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
                                                 where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "攻撃力上昇し、敵全体の攻撃力低下") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A4V2") >= requestValue[i])))
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

                        case "スキル発動率低下":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i])))
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
                                                 || ((p.Field<string>("A1Ex1") == "回避し、敵3体が攻撃ミス") || (p.Field<string>("A2Ex1") == "回避し、敵3体が攻撃ミス") || (p.Field<string>("A3Ex1") == "回避し、敵3体が攻撃ミス") || (p.Field<string>("A4Ex1") == "回避し、敵3体が攻撃ミス"))
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

                        case "反撃とか":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == "反撃") || (p.Field<string>("A2Ex1") == "反撃") || (p.Field<string>("A3Ex1") == "反撃") || (p.Field<string>("A4Ex1") == "反撃"))
                                                 || ((p.Field<string>("A1Ex1") == "回避し、反撃") || (p.Field<string>("A2Ex1") == "回避し、反撃") || (p.Field<string>("A3Ex1") == "回避し、反撃") || (p.Field<string>("A4Ex1") == "回避し、反撃"))
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

                        case "超反撃":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (((p.Field<string>("A1Ex1") == "反撃") && (p.Field<string>("A1Ex2") == "超反撃")) || ((p.Field<string>("A2Ex1") == "反撃") && (p.Field<string>("A2Ex2") == "超反撃")) || ((p.Field<string>("A3Ex1") == "反撃") && (p.Field<string>("A3Ex2") == "超反撃")) || ((p.Field<string>("A4Ex1") == "反撃") && (p.Field<string>("A4Ex2") == "超反撃")))
                                                 || (((p.Field<string>("A1Ex1") == "回避し、反撃") && (p.Field<string>("A1Ex2") == "超反撃")) || ((p.Field<string>("A2Ex1") == "回避し、反撃") && (p.Field<string>("A2Ex2") == "超反撃")) || ((p.Field<string>("A3Ex1") == "回避し、反撃") && (p.Field<string>("A3Ex2") == "超反撃")) || ((p.Field<string>("A4Ex1") == "回避し、反撃") && (p.Field<string>("A4Ex2") == "超反撃")))
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
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、回避") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、回避") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、回避") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、回避"))
                                                 || ((p.Field<string>("A1Ex1") == "回避し、残りHPで攻撃力上昇") || (p.Field<string>("A2Ex1") == "回避し、残りHPで攻撃力上昇") || (p.Field<string>("A3Ex1") == "回避し、残りHPで攻撃力上昇") || (p.Field<string>("A4Ex1") == "回避し、残りHPで攻撃力上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "回避し、反撃") || (p.Field<string>("A2Ex1") == "回避し、反撃") || (p.Field<string>("A3Ex1") == "回避し、反撃") || (p.Field<string>("A4Ex1") == "回避し、反撃"))
                                                 || ((p.Field<string>("A1Ex1") == "回避し、敵3体が攻撃ミス") || (p.Field<string>("A2Ex1") == "回避し、敵3体が攻撃ミス") || (p.Field<string>("A3Ex1") == "回避し、敵3体が攻撃ミス") || (p.Field<string>("A4Ex1") == "回避し、敵3体が攻撃ミス"))
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
                                                 || ((p.Field<string>("A1Ex1") == "PTと自身スキルダメ上昇") || (p.Field<string>("A2Ex1") == "PTと自身スキルダメ上昇") || (p.Field<string>("A3Ex1") == "PTと自身スキルダメ上昇") || (p.Field<string>("A4Ex1") == "PTと自身スキルダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、対ボスダメ上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、対ボスダメ上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、対ボスダメ上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、対ボスダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、シャイクリ泥率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、ダメ無効") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、ダメ無効") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、ダメ無効") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、ダメ無効"))
                                                 || ((p.Field<string>("A1Ex1") == "MAP画面アビと、スキルダメ上昇") || (p.Field<string>("A2Ex1") == "MAP画面アビと、スキルダメ上昇") || (p.Field<string>("A3Ex1") == "MAP画面アビと、スキルダメ上昇") || (p.Field<string>("A4Ex1") == "MAP画面アビと、スキルダメ上昇"))
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
                                                 where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "攻撃力上昇し、再行動") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "攻撃力上昇し、再行動") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "攻撃力上昇し、再行動") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "攻撃力上昇し、再行動") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "ソラ効果上昇し自身が再行動") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "光ゲージ充填し、自身が再行動") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "光ゲージ充填し、自身が再行動") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "光ゲージ充填し、自身が再行動") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "光ゲージ充填し、自身が再行動") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "PT移動力増加し、自身が再行動") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "PT移動力増加し、自身が再行動") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "PT移動力増加し、自身が再行動") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "PT移動力増加し、自身が再行動") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "追撃し、自身が再行動") && (p.Field<int>("A1V2") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "追撃し、自身が再行動") && (p.Field<int>("A2V2") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "追撃し、自身が再行動") && (p.Field<int>("A3V2") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "追撃し、自身が再行動") && (p.Field<int>("A4V2") >= requestValue[i])))
                                                 || ((p.Field<string>("A1Ex1") == "自身が再行動し、シャイクリ泥率上昇") || (p.Field<string>("A2Ex1") == "自身が再行動し、シャイクリ泥率上昇") || (p.Field<string>("A3Ex1") == "自身が再行動し、シャイクリ泥率上昇") || (p.Field<string>("A4Ex1") == "自身が再行動し、シャイクリ泥率上昇"))
                                                 || (((p.Field<string>("A1Ex1") == "PTに再行動付与") && (p.Field<int>("A1V1") >= requestValue[i])) || ((p.Field<string>("A2Ex1") == "PTに再行動付与") && (p.Field<int>("A2V1") >= requestValue[i])) || ((p.Field<string>("A3Ex1") == "PTに再行動付与") && (p.Field<int>("A3V1") >= requestValue[i])) || ((p.Field<string>("A4Ex1") == "PTに再行動付与") && (p.Field<int>("A4V1") >= requestValue[i])))
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

                        case "追撃":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "追撃し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "追撃し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "追撃し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "追撃し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "追撃し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "追撃し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "追撃し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "追撃し、スキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "追撃し、自身が再行動") || (p.Field<string>("A2Ex1") == "追撃し、自身が再行動") || (p.Field<string>("A3Ex1") == "追撃し、自身が再行動") || (p.Field<string>("A4Ex1") == "追撃し、自身が再行動"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、追撃") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、追撃") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、追撃") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、追撃"))
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

                        case "HP回復":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、自身のHP回復(効果値はEx2)") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、自身のHP回復(効果値はEx2)") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、自身のHP回復(効果値はEx2)") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、自身のHP回復(効果値はEx2)"))
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
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A2Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A3Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇") || (p.Field<string>("A4Ex1") == "PT移動力増加し、スキルLVでスキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、移動力を攻撃力に追加") || (p.Field<string>("A2Ex1") == "PT移動力増加し、移動力を攻撃力に追加") || (p.Field<string>("A3Ex1") == "PT移動力増加し、移動力を攻撃力に追加") || (p.Field<string>("A4Ex1") == "PT移動力増加し、移動力を攻撃力に追加"))
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、対ボス攻撃力上昇") || (p.Field<string>("A2Ex1") == "PT移動力増加し、対ボス攻撃力上昇") || (p.Field<string>("A3Ex1") == "PT移動力増加し、対ボス攻撃力上昇") || (p.Field<string>("A4Ex1") == "PT移動力増加し、対ボス攻撃力上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "PT移動力増加し、自身が再行動") || (p.Field<string>("A2Ex1") == "PT移動力増加し、自身が再行動") || (p.Field<string>("A3Ex1") == "PT移動力増加し、自身が再行動") || (p.Field<string>("A4Ex1") == "PT移動力増加し、自身が再行動"))
                                                 || ((p.Field<string>("A1Ex1") == "MAP画面アビと、移動力増加") || (p.Field<string>("A2Ex1") == "MAP画面アビと、移動力増加") || (p.Field<string>("A3Ex1") == "MAP画面アビと、移動力増加") || (p.Field<string>("A4Ex1") == "MAP画面アビと、移動力増加"))
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
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "ソラ発動毎にダメ上昇") || (p.Field<string>("A2Ex1") == "ソラ発動毎にダメ上昇") || (p.Field<string>("A3Ex1") == "ソラ発動毎にダメ上昇") || (p.Field<string>("A4Ex1") == "ソラ発動毎にダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") || (p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "HP割合ダメ上昇率") || (p.Field<string>("A2Ex1") == "HP割合ダメ上昇率") || (p.Field<string>("A3Ex1") == "HP割合ダメ上昇率") || (p.Field<string>("A4Ex1") == "HP割合ダメ上昇率"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇HP割合ダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇") || (p.Field<string>("A2Ex1") == "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇") || (p.Field<string>("A3Ex1") == "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇") || (p.Field<string>("A4Ex1") == "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") || (p.Field<string>("A2Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") || (p.Field<string>("A3Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") || (p.Field<string>("A4Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇"))
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

                        case "ターン毎ダメージ上昇":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、ターン毎にダメージ上昇"))
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

                        case "弱点属性の敵に対するダメージ増加":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") || (p.Field<string>("A2Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") || (p.Field<string>("A3Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") || (p.Field<string>("A4Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、弱点属性の敵に対するダメージ増加") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、弱点属性の敵に対するダメージ増加") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、弱点属性の敵に対するダメージ増加") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、弱点属性の敵に対するダメージ増加"))
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

                        case "ダメ無効":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、ダメ無効") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、ダメ無効") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、ダメ無効") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、ダメ無効"))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、ダメ無効") || (p.Field<string>("A2Ex1") == "スキル発動率上昇し、ダメ無効") || (p.Field<string>("A3Ex1") == "スキル発動率上昇し、ダメ無効") || (p.Field<string>("A4Ex1") == "スキル発動率上昇し、ダメ無効"))
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

                        case "攻撃力上昇"://考えてみるとみんな攻撃力上昇持ってるから、このコードはいらないかもしれない。
                                     //DB登録種類列記
                                     //"敵の数で攻撃力上昇"
                                     //"攻撃力上昇HP割合ダメ上昇"
                                     //"攻撃力上昇し自身がさらに上昇"
                                     //"スキル発動毎に攻撃力上昇"fkg_selectでのコーディングはなし
                            {
                                string pattern_ex = @"^(?!.*対ボス攻撃力上昇|.*ターンで攻撃力上昇|.*防御力に応じて).*$";
                                string pattern = @"^.*攻撃力上昇.*$";
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("A1Ex1") == fieldStr[i]) || (p.Field<string>("A2Ex1") == fieldStr[i]) || (p.Field<string>("A3Ex1") == fieldStr[i]) || (p.Field<string>("A4Ex1") == fieldStr[i])
                                                //  || (Regex.IsMatch(p.Field<string>("A1Ex1").ToString(), pattern_ex) || Regex.IsMatch(p.Field<string>("A2Ex1").ToString(), pattern_ex) || Regex.IsMatch(p.Field<string>("A3Ex1").ToString(), pattern_ex) || Regex.IsMatch(p.Field<string>("A4Ex1").ToString(), pattern_ex))
                                                 || (Regex.IsMatch(p.Field<string>("A1Ex1").ToString(), pattern_ex) && Regex.IsMatch(p.Field<string>("A1Ex1").ToString(), pattern)) || (Regex.IsMatch(p.Field<string>("A2Ex1").ToString(), pattern_ex) && Regex.IsMatch(p.Field<string>("A2Ex1").ToString(), pattern)) || (Regex.IsMatch(p.Field<string>("A3Ex1").ToString(), pattern_ex) && Regex.IsMatch(p.Field<string>("A3Ex1").ToString(), pattern)) || (Regex.IsMatch(p.Field<string>("A4Ex1").ToString(), pattern_ex) && Regex.IsMatch(p.Field<string>("A4Ex1").ToString(), pattern))
                                                //  || (Regex.IsMatch(p.Field<string>("A1Ex1").ToString(), pattern) || Regex.IsMatch(p.Field<string>("A2Ex1").ToString(), pattern) || Regex.IsMatch(p.Field<string>("A3Ex1").ToString(), pattern) || Regex.IsMatch(p.Field<string>("A4Ex1").ToString(), pattern))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、対ボス攻撃力上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、対ボス攻撃力上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、対ボス攻撃力上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、対ボス攻撃力上昇"))
                                                 //|| ((p.Field<string>("A1Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇HP割合ダメ上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇HP割合ダメ上昇"))
                                                 //|| ((p.Field<string>("A1Ex1") == "攻撃力上昇し自身がさらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し自身がさらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し自身がさらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し自身がさらに上昇"))
                                                 //|| ((p.Field<string>("A1Ex1") == "スキル発動毎に攻撃力上昇") || (p.Field<string>("A2Ex1") == "スキル発動毎に攻撃力上昇") || (p.Field<string>("A3Ex1") == "スキル発動毎に攻撃力上昇") || (p.Field<string>("A4Ex1") == "スキル発動毎に攻撃力上昇"))
                                                 )
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


                        case "全体":
                        case "2体":
                        case "複数回":
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
                        case "変則":
                        case "吸収":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((p.Field<string>("SType") == fieldStr[i])
                                                 || (p.Field<string>("SType") == "変則吸収")
                                                 )
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
                //スペシャル selectValueがある場合（検索パラメータに数値が付与されている場合はこちら
                {
                    switch (fieldStr[i])
                    {
                        case "1ターン目系":
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (p.Field<int>("A1st1") == selectValue[i]) || (p.Field<int>("A2st1") == selectValue[i]) || (p.Field<int>("A3st1") == selectValue[i]) || (p.Field<int>("A4st1") == selectValue[i])
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇1T目さらに上昇"))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇"))

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
                                string pattern = @".*、スキルLVでスキル発動率上昇.*$";
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where (((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率1T目と3T目") && (p.Field<int>("A4V2") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボス攻撃力上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、対ボスダメ上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、対ボスダメ上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、対ボスダメ上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、対ボスダメ上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、自身のスキルダメ上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、ダメ無効") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、ダメ無効") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、ダメ無効") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、ダメ無効") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、シャイクリ泥率上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキル発動率上昇し、敵全体の攻撃力低下") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") && (p.Field<int>("A1V1") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") && (p.Field<int>("A2V1") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") && (p.Field<int>("A3V1") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇") && (p.Field<int>("A4V1") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "攻撃力上昇し、スキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                //  || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "攻撃力上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A2Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A3Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇") || (p.Field<string>("A4Ex1") == "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇"))
                                                 //  || ((p.Field<string>("A1Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "スキルダメ上昇し、スキルLVでスキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "弱点属性の敵に対するダメージ増加し、スキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                 || ((p.Field<string>("A1Ex1") == "追撃し、スキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "追撃し、スキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "追撃し、スキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "追撃し、スキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                //  || ((p.Field<string>("A1Ex1") == "追撃し、スキルLVでスキル発動率上昇") && (p.Field<int>("A1V2") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "追撃し、スキルLVでスキル発動率上昇") && (p.Field<int>("A2V2") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "追撃し、スキルLVでスキル発動率上昇") && (p.Field<int>("A3V2") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "追撃し、スキルLVでスキル発動率上昇") && (p.Field<int>("A4V2") == selectValue[i]))
                                                 || ((Regex.IsMatch(p.Field<string>("A1Ex1").ToString(), pattern) && (p.Field<int>("A1V2") == selectValue[i])) || (Regex.IsMatch(p.Field<string>("A2Ex1").ToString(), pattern) && (p.Field<int>("A2V2") == selectValue[i])) || (Regex.IsMatch(p.Field<string>("A3Ex1").ToString(), pattern) && (p.Field<int>("A3V2") == selectValue[i])) || (Regex.IsMatch(p.Field<string>("A4Ex1").ToString(), pattern) && (p.Field<int>("A4V2") == selectValue[i])))
                                                 )
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
                            //"クリ率クリダメ上昇"も引っ掛ける
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i]) && (p.Field<int>("A1NO") == selectValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i]) && (p.Field<int>("A2NO") == selectValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i]) && (p.Field<int>("A3NO") == selectValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i]) && (p.Field<int>("A4NO") == selectValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A1V1") >= requestValue[i]) && (p.Field<int>("A1NO") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A2V1") >= requestValue[i]) && (p.Field<int>("A2NO") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A3V1") >= requestValue[i]) && (p.Field<int>("A3NO") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A4V1") >= requestValue[i]) && (p.Field<int>("A4NO") == selectValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A1V2") >= requestValue[i]) && (p.Field<int>("A1NO") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A2V2") >= requestValue[i]) && (p.Field<int>("A2NO") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A3V2") >= requestValue[i]) && (p.Field<int>("A3NO") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "クリ率上昇さらに自身のクリ率とダメ上昇") && (p.Field<int>("A4V2") >= requestValue[i]) && (p.Field<int>("A4NO") == selectValue[i])))
                                                 )
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

                        case "クリダメ上昇":
                        case "クリダメ上昇し自身がさらに上昇":
                            //"クリ率クリダメ上昇"も引っ掛ける
                            {
                                var outputData = from p in dt[i].AsEnumerable()
                                                 where ((((p.Field<string>("A1Ex1") == fieldStr[i]) && (p.Field<int>("A1V1") >= requestValue[i]) && (p.Field<int>("A1NO") == selectValue[i])) || ((p.Field<string>("A2Ex1") == fieldStr[i]) && (p.Field<int>("A2V1") >= requestValue[i]) && (p.Field<int>("A2NO") == selectValue[i])) || ((p.Field<string>("A3Ex1") == fieldStr[i]) && (p.Field<int>("A3V1") >= requestValue[i]) && (p.Field<int>("A3NO") == selectValue[i])) || ((p.Field<string>("A4Ex1") == fieldStr[i]) && (p.Field<int>("A4V1") >= requestValue[i]) && (p.Field<int>("A4NO") == selectValue[i])))
                                                 || (((p.Field<string>("A1Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A1V2") >= requestValue[i]) && (p.Field<int>("A1NO") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A2V2") >= requestValue[i]) && (p.Field<int>("A2NO") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A3V2") >= requestValue[i]) && (p.Field<int>("A3NO") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "クリ率クリダメ上昇") && (p.Field<int>("A4V2") >= requestValue[i]) && ((p.Field<int>("A4NO") == selectValue[i]))))
                                                 || (((p.Field<string>("A1Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A1V1") >= requestValue[i]) && (p.Field<int>("A1NO") == selectValue[i])) || ((p.Field<string>("A2Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A2V1") >= requestValue[i]) && (p.Field<int>("A2NO") == selectValue[i])) || ((p.Field<string>("A3Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A3V1") >= requestValue[i]) && (p.Field<int>("A3NO") == selectValue[i])) || ((p.Field<string>("A4Ex1") == "クリダメ上昇し自身がさらに上昇") && (p.Field<int>("A4V1") >= requestValue[i]) && (p.Field<int>("A4NO") == selectValue[i])))
                                                 )
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
        }

        //
        //クッキーに登録されているキャラのみをフィルタ処理する
        //
        private DataTable GetDataFilterCookie(DataTable dt_input,int type)
        {
            //クッキー登録データ取得
            FKG_register fkg_register = new FKG_register();
            string[,] registeredString = fkg_register.DecodeFkgName();


            DataTable dt_fkg = dt_input.Clone();

            for (int i = dt_input.Rows.Count - 1; i >= 0 ; i--)
            {
                int type2count = 0;
                for (int j = 0; j < registeredString.GetLength(0); j++)
                {
                    
                    if (dt_input.Rows[i]["Id"].ToString() == registeredString[j, 0])
                    {
                        //type1
                        if (type == 1)
                        {
                            dt_fkg.ImportRow(dt_input.Rows[i]);
                        }
                        else if(type == 2)
                        {
                            continue;
                        }
                        continue;
                    }
                    else
                    {
                        type2count++;
                        //全部当たって、外れた場合のみここに来る
                        if (type2count == registeredString.GetLength(0))
                        {
                            //type2
                            if (type == 2)
                            {
                                dt_fkg.ImportRow(dt_input.Rows[i]);
                            }
                        }
                        continue;
                    }
                }
            }

            if(dt_fkg.Rows.Count == 0)
            {
                //ここには絶対来ないと思う
                return dt_input;
            }
            
            return dt_fkg;
        }

        private DataTable GetDataFilterRare(DataTable dt_input)
        {
            DataTable dt_out;

            
            switch (RadioButtonList1.SelectedItem.Text)
            {
                case "6":
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                            where (p.Field<int>("Rarity") == 6)
                                            select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        dt_input = outputData.CopyToDataTable();
                        break;
                    }
                case "5":
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                            where (p.Field<int>("Rarity") == 5)
                                            select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        dt_input = outputData.CopyToDataTable();
                        break;
                    }
                case "その他":
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                            where (p.Field<int>("Rarity") == 4) || (p.Field<int>("Rarity") == 3) || (p.Field<int>("Rarity") == 2)
                                            select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        dt_input = outputData.CopyToDataTable();
                        break;
                    }
                case "昇華虹のみ":
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                            where (p.Field<int>("Id") >= 10000)
                                            select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        dt_input = outputData.CopyToDataTable();
                        break;
                    }
                case "通常虹のみ":
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (p.Field<int>("Id") < 10000)
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        dt_input = outputData.CopyToDataTable();
                        break;
                    }
                default:
                    {

                        break;
                    }


            }
            dt_out = dt_input;
            return dt_out;
        }


        //スキルタイプフィルタ処理
        private DataTable GetDataFilterSType(DataTable dt_input)
        {
            DataTable dt_out;

            int countTrue = 0;
            string[] filterValue = new string[6];
            foreach (ListItem item in CheckBoxList3.Items)
            {
                if (item.Selected)
                {
                    filterValue[countTrue] = item.Value;
                    countTrue++;
                }
            }

            string filterValue0 = filterValue[0];
            string filterValue1 = filterValue[1];
            string filterValue2 = filterValue[2];
            string filterValue3 = filterValue[3];
            string filterValue4 = filterValue[4];


            switch (countTrue)
            {
                case 1:
                    {
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (p.Field<string>("SType").IndexOf(filterValue0) != -1)
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
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (p.Field<string>("SType").IndexOf(filterValue0) != -1) || (p.Field<string>("SType").IndexOf(filterValue1) != -1)
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
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (p.Field<string>("SType").IndexOf(filterValue0) != -1) || (p.Field<string>("SType").IndexOf(filterValue1) != -1) || (p.Field<string>("SType").IndexOf(filterValue2) != -1)
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
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (p.Field<string>("SType").IndexOf(filterValue0) != -1) || (p.Field<string>("SType").IndexOf(filterValue1) != -1) || (p.Field<string>("SType").IndexOf(filterValue2) != -1) || (p.Field<string>("SType").IndexOf(filterValue3) != -1)
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
                        var outputData = from p in dt_input.AsEnumerable()
                                         where (p.Field<string>("SType").IndexOf(filterValue0) != -1) || (p.Field<string>("SType").IndexOf(filterValue1) != -1) || (p.Field<string>("SType").IndexOf(filterValue2) != -1) || (p.Field<string>("SType").IndexOf(filterValue3) != -1) || (p.Field<string>("SType").IndexOf(filterValue4) != -1)
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }

                        dt_out = outputData.CopyToDataTable();
                        return dt_out;
                    }
                case 6:
                    {
                        return dt_input;
                    }
            }

            return null;
        }

        //除外するアビリティにより、レコードを省く
        private DataTable GetDataFilterExclusion(string keyWord,int keyValue, DataTable dt_input)
        {
            if (dt_input == null)
            {
                return null;
            }


            DataTable dt_out;
            switch(keyWord)
            {
                case "1ターン目系":
                    {
                        var outputData = from p in dt_input.AsEnumerable()//"攻撃力上昇し、1T目のスキル発動率上昇"
                                         where !(((p.Field<int>("A1st1") == keyValue) || (p.Field<int>("A2st1") == keyValue) || (p.Field<int>("A3st1") == keyValue) || (p.Field<int>("A4st1") == keyValue))
                                                || ((p.Field<string>("A1Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇し、1T目のスキル発動率上昇"))
                                                || ((p.Field<string>("A1Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A2Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A3Ex1") == "スキル発動率1T目と3T目") || (p.Field<string>("A4Ex1") == "スキル発動率1T目と3T目"))
                                                || ((p.Field<string>("A1Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A2Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A3Ex1") == "攻撃力上昇1T目さらに上昇") || (p.Field<string>("A4Ex1") == "攻撃力上昇1T目さらに上昇"))
                                                )
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


            //属性数チェック
            string[,] arrayAtt = new string[4, 2];


            arrayAtt[0, 1] = "斬";
            arrayAtt[1, 1] = "打";
            arrayAtt[2, 1] = "突";
            arrayAtt[3, 1] = "魔";

            int attCount = 0;
            foreach (ListItem item in CheckBoxList2.Items)
            {
                if (item.Selected)
                {
                    arrayAtt[attCount,0] = "True";
                    arrayAtt[attCount, 1] = item.Value;
                    attCount++;
                }
            }
            switch (attCount)
            {
                case 1://1個の場合
                    {
                        string attInput = "";
                        //どれを入れるか選別
                        for (int i = 0; i < 4; i++)
                        {
                            if (arrayAtt[i, 0] == "True")
                            {
                                attInput = arrayAtt[i, 1];
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
                case 4://全属性指定の場合、処理しない
                    {
                        return dt_input;
                    }
                default:
                    return null;
            }
            return dt_out;
        }

        //属性フィルターのoverride
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

        private DataTable FilterSub(string Att1, string Att2, string Att3, DataTable dt_in)
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

        //属性付与フィルタによるフィルタリング
        private DataTable GetDataFilterAddAtt(DataTable dt_input)
        {
            //LINQ処理
            DataTable dt = dt_input;

            //付与属性フィルタにチェックあるか確認
            {
                int countAddAtt = 0;
                int[] AddAtt = new int[4];
                foreach (ListItem item in CheckBoxList4.Items)
                {
                    if (item.Selected)
                    {
                        switch (item.Text)
                        {
                            case "斬":
                                {
                                    AddAtt[countAddAtt] = 0;
                                    break;
                                }
                            case "打":
                                {
                                    AddAtt[countAddAtt] = 1;
                                    break;
                                }
                            case "突":
                                {
                                    AddAtt[countAddAtt] = 2;
                                    break;
                                }
                            case "魔":
                                {
                                    AddAtt[countAddAtt] = 3;
                                    break;
                                }
                        }
                        countAddAtt++;
                    }
                }
                //付与属性フィルタにチェックある場合の処理
                if (countAddAtt != 0)
                {
                    //準備
                    //
                    string[,] findString = new string[4, 7]
                    { {"斬","斬打","斬突", "斬魔", "斬打突", "斬打魔", "斬突魔" },
                                    {"打","斬打","打突", "打魔", "斬打突", "斬打魔", "打突魔" },
                                    {"突","斬突","打突", "突魔", "斬打突", "斬突魔", "打突魔" },
                                    {"魔","斬魔","打魔", "突魔", "斬打魔", "斬突魔", "打突魔" }};


                    for (int j = 0; j < countAddAtt; j++)
                    {
                        var outputData = from p in dt.AsEnumerable()
                                         where ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 0])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 0])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 0])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 0]))
                                                || ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 1])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 1])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 1])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 1]))
                                                || ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 2])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 2])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 2])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 2]))
                                                || ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 3])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 3])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 3])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 3]))
                                                || ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 4])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 4])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 4])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 4]))
                                                || ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 5])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 5])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 5])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 5]))
                                                || ((p.Field<string>("A1Ex1") == "属性付与") && (p.Field<string>("A1Ex2") == findString[AddAtt[j], 6])) || ((p.Field<string>("A2Ex1") == "属性付与") && (p.Field<string>("A2Ex2") == findString[AddAtt[j], 6])) || ((p.Field<string>("A3Ex1") == "属性付与") && (p.Field<string>("A3Ex2") == findString[AddAtt[j], 6])) || ((p.Field<string>("A4Ex1") == "属性付与") && (p.Field<string>("A4Ex2") == findString[AddAtt[j], 6]))
                                         select p;
                        //検索結果が無い場合
                        if (outputData.Count() == 0)
                        {
                            return null;
                        }
                        dt = outputData.CopyToDataTable();
                    }
                }
            }
            return dt;
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
        string OutString(int turn, int num, string Ex, int v1, int v2, string Name, string Ex2)
        {


            string turnStr = "";
            string numStr = "";
            string outStr = "";

            //常套句
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

            switch (Ex)
            {
                case "攻撃力上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "敵の数で攻撃力上昇":
                    {
                        if (v2 == -1)
                        {//ウキツリボク専用
                            outStr = "敵の数が3体で<span class=\"fkg-value\">" + v1 + "</span>％攻撃力増加、敵の数が減るほど<span class=\"fkg-value\">" + v1 + "</span>％ずつ<span class=\"fkg-value2\">" +numStr + "</span>攻撃力増加";
                        }
                        else
                        {
                            outStr = "敵の数ｘ<span class=\"fkg-value\">" + v1 + "</span>％<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が上昇";
                        }
                        return outStr;
                    }

                case "ターンで攻撃力上昇"://シャクヤクハロ、スノドロ、ヤドリギ(MAX40%)
                    {
                        int maxValue = v1 * 2;
                        if (v2 != 0)
                            maxValue = v2;
                        outStr = "ターン毎に<span class=\"fkg-value2\">" +numStr + "</span>攻撃力<span class=\"fkg-value\">" + v1 + "</span>％上昇　最大" + maxValue + "％";
                        return outStr;
                    }

                case "スキルLVで攻撃力上昇":
                    {
                        outStr = "自身のスキルLVに応じて<span class=\"fkg-value2\">" + "自信を含む人数(1～5人)の" + "</span>攻撃力<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "残りHPで攻撃力上昇":
                    {
                        outStr = "自身の</span>残HPにより攻撃力上昇";
                        return outStr;
                    }

                case "攻撃力上昇ターンでさらに上昇"://アデニウム昇華
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し," + turnStr + "ターン毎にPT全体の攻撃力<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "攻撃力上昇1T目さらに上昇"://マンリョウ
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、1T目の攻撃力が更に<span class=\"fkg-value\">" + v2 + "</span>％上昇";
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
                        outStr = "PT全体の攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し更に<span class=\"fkg-value2\">" +numStr + "</span>現在のHPの" + v2Sub + "の割合で<span class=\"fkg-value2\">" +numStr + "</span>与ダメージが増加";
                        return outStr;
                    }

                case "攻撃力上昇し自身がさらに上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身の攻撃力が更に<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "攻撃力上昇し、残HPでさらに上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、残HPにより<span class=\"fkg-value2\">自身の</span>攻撃力上昇";
                        return outStr;
                    }

                case "攻撃力上昇し、自信を含む2人がさらに上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身を含む2人の攻撃力が更に<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "攻撃力上昇し、自信を含む3人がさらに上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身を含む3人の攻撃力が更に<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }
                case "攻撃力上昇し、敵の数でさらに上昇":
                    {
                            outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、 敵の数ｘ<span class=\"fkg-value\">" + v2 + "</span>％攻撃力が上昇";
                            return outStr;
                    }

                case "攻撃力上昇し、PTメンバー数でさらに上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、更に戦闘開始時のメンバーの数ｘ<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "攻撃力上昇し、防御力に応じて攻撃力上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、更にPTメンバーの攻撃力にそれぞれの防御力の<span class=\"fkg-value\">" + v2 + "</span>％を追加";
                        return outStr;
                    }

                case "攻撃力上昇し、スキル発動率上昇":
                    {
                        double v2D = (double)(v2 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、スキル発動率が<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;

                    }

                case "攻撃力上昇し、スキルLVでスキル発動率上昇":
                    {
                        double v2D = (double)(v2 + 100) / 100;
                        double v3D = v2D + 0.08;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、スキル発動率が自身のスキルレベルに応じ<span class=\"fkg-value\">" + v2D + "</span>～<span class=\"fkg-value\">" + v3D + "</span>倍";
                        return outStr;

                    }

                case "攻撃力上昇し、1T目のスキル発動率上昇":
                    {
                        double v2D = (double)(v2 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、1T目のスキル発動率が<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;
                    }

                case "攻撃力上昇し、スキルダメージ上昇":
                    {
                        if (num != 5)
                        {
                            outStr = turnStr + "PT全体のスキルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加し、<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        }
                        else
                        {
                            outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、スキルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        }
                        return outStr;

                    }

                case "攻撃力上昇し、ターン毎にダメージ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、与ダメージがターン毎に<span class=\"fkg-value\">" + v2 + "</span>％増加 最大" + v2 * 2 + "％";


                        return outStr;

                    }

                case "攻撃力上昇し、弱点属性の敵に対するダメージ増加":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、弱点属性の敵に対してダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";


                        return outStr;

                    }

                case "攻撃力上昇し、対ボス攻撃力上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、ボス敵に対して攻撃力が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "攻撃力上昇し、対ボスダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、ボスに対して与えるダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "攻撃力上昇し、敵全体の攻撃力低下":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、敵全体の攻撃力が<span class=\"fkg-value\">" + v2 + "</span>％低下";
                        return outStr;
                    }
                case "攻撃力上昇し、敵3体が攻撃ミス":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、敵3体が<span class=\"fkg-value\">" + v2 + "</span>％の確率で攻撃ミス";
                        return outStr;
                    }
                case "攻撃力上昇し、回避":
                    {
                        int v3 = 0;
                        switch (v2)
                        {
                            case 80:
                                {
                                    v3 = 50;
                                    break;
                                }
                        }

                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身が" + "2T目まで<span class=\"fkg-value\">" + v2 + "</span>％、以降は" + v3 + "％の確率で回避";
                        return outStr;
                    }
                case "攻撃力上昇し、再行動":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身が<span class=\"fkg-value\">" + v2 + "</span>％の確率で再行動";
                        return outStr;
                    }
                case "攻撃力上昇し、追撃":
                    {
                        string add = "攻撃した敵に";
                        switch (Ex2)
                        {
                            case "追撃1":
                                {
                                    add = "攻撃した敵に";
                                    break;
                                }
                            case "追撃2":
                                {
                                    add = "敵全体に";
                                    break;
                                }
                        }
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身の攻撃後、自身はPT総合力の<span class=\"fkg-value\">" + v2 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">" +add + "</span>追撃";
                        return outStr;
                    }
                case "攻撃力上昇し、移動力追加":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、パーティの移動力が<span class=\"fkg-value\">" + v2 + "</span>増加";
                        return outStr;
                    }
                case "攻撃力上昇し、ソラ効果上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、ソーラードライブの効果が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }
                case "攻撃力上昇し、光ゲージ充填":
                   
                    outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、光GAUGEが<span class=\"fkg-value\">" + v2 + "</span>％溜まった状態から討伐開始";
                    return outStr;

                case "攻撃力上昇し、シャイクリ泥率上昇":
                    outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、シャインクリスタルのドロップ率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                    return outStr;

                case "攻撃力上昇し、自身のHP回復(効果値はEx2)":
                    outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、<span class=\"fkg-value2\">自身は</span>毎ターン<span class=\"fkg-value\">" + v2 + "</span>％の確率で最大HPの<span class=\"fkg-value\">" + Ex2 + "</span>％回復";
                    return outStr;

                case "スキル発動毎に攻撃力上昇":
                    {
                        string add = "";
                        if (v2 != 0)
                        {
                            add = "　最大で<span class=\"fkg-value\">" + v2 + "</span>％";
                        }
                        outStr = "スキル発動毎に<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇" + add;
                        return outStr;
                    }

                case "属性種類数により攻撃力上昇":
                    {
                        if (v2 == -1)
                            outStr = "PT全体の属性種類が少ないほど攻撃力が上昇。3属性以上で<span class=\"fkg-value\">" + v1 + "</span>％、2属性で<span class=\"fkg-value\">" + v1 * 2 + "</span>％、1属性で<span class=\"fkg-value\">" + v1 * 3 + "</span>％";
                        else
                            outStr = "PT全体の属性種類が多いほど攻撃力が上昇。1属性で<span class=\"fkg-value\">" + v1 + "</span>％、2属性で<span class=\"fkg-value\">" + v1 * 2 + "</span>％、3属性以上で<span class=\"fkg-value\">" + v1 * 3 + "<span>％";
                        return outStr;
                    }

                case "防御力に応じて攻撃力上昇":
                    {
                        outStr = "PTメンバーの攻撃力に、それぞれの防御力の<span class=\"fkg-value\">" + v1 + "</span>％を追加";
                        return outStr;

                    }

                case "PTメンバーの数で攻撃力上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が戦闘開始時のメンバーの数ｘ<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }
                case "ダメージ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>与ダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加";
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
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>現在のHPの<span class=\"fkg-value\">" +v1Sub + "</span>の割合で<span class=\"fkg-value2\">" +numStr + "</span>与ダメージが増加";
                        return outStr;
                    }

                case "ターン毎ダメージ上昇"://パフィオペディルム
                    {
                        int maxValue = v1 * 3;
                        if(v2 != 0)
                            maxValue = v2;
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>与ダメージがターン毎に<span class=\"fkg-value\">" + v1 + "</span>％増加　最大" + maxValue + "％";
                        return outStr;
                    }

                case "弱点属性の敵に対するダメージ増加":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>弱点属性の敵に対するダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加";
                        return outStr;

                    }

                case "弱点属性の敵に対するダメージ増加し、スキル発動率上昇":
                    {
                        double v2D = (double)(v2 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>弱点属性の敵に対するダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、<span class=\"fkg-value2\">" +numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;
                    }

                case "移動力を攻撃力に追加":
                    {
                        outStr = "PT全体の攻撃力に移動力の<span class=\"fkg-value\">" + v1 + "</span>％を追加";
                        return outStr;
                    }

                case "防御ダメ軽減率上昇":
                    {
                        string gut = "<span class=\"fkg-value2\">自身が</span><span class=\"fkg-value\">3</span>回GUTS発動";
                        if (Ex2 == "ガッツ付与1回")
                            gut = "<span class=\"fkg-value2\">全員に</span><span class=\"fkg-value\">1</span>回100%発動するGUTS付与";
                        double v2D = (double)v2 / 10;
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>防御力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、防御時のダメージ軽減率を<span class=\"fkg-value\">" + v2D + "</span>％軽減、" + gut;
                        return outStr;
                    }

                case "クリ率上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>クリティカル発生率が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }
                case "クリ率上昇さらに自身のクリ率とダメ上昇":
                    {
                        int v12 = v1 + v2;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>クリティカル率が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身のクリティカル率とダメージが<span class=\"fkg-value\">" + v2 + "</span>％上昇（自身のクリ率<span class=\"fkg-value\">" + v12 + "</span>％上昇）";
                        return outStr;
                    }


                case "クリダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>クリティカルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加";
                        return outStr;
                    }

                case "クリダメ上昇し自身がさらに上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>クリティカルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、自身のクリティカルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "クリ率クリダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>クリティカル率が<span class=\"fkg-value\">" + v1 + "</span>％上昇しクリティカルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキル発動率上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍";
                        return outStr;
                    }

                case "スキル発動率1T目と3T目":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        double v2D = (double)(v2 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、3T目のスキル発動率が<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;
                    }

                case "スキル発動率上昇し、対ボス攻撃力上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、ボス敵に対して攻撃力が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "スキル発動率上昇し、対ボスダメ上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、ボスに与えるダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキル発動率上昇し、自身のスキルダメ上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、自身のスキルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        //属性による場合分け
                        string Att = "";
                        switch (Ex2)
                        {
                            case "残":
                                {
                                    Att = "<span class=\"fkg-value\">" + "斬" + "</span>";
                                    break;
                                }
                            case "打":
                                {
                                    Att = "<span class=\"fkg-value2\">" + "打" + "</span>";
                                    break;
                                }
                            case "突":
                                {
                                    Att = "<span class=\"fkg-value3\">" + "突" + "</span>";
                                    break;
                                }
                            case "魔":
                                {
                                    Att = "<span class=\"fkg-value4\">" + "魔" + "</span>";
                                    break;
                                }
                        }

                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、<span class=\"fkg-value2\">自身</span>と" + Att + "<span class=\"fkg-value2\">属性のPTメンバーの</span>スキルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキル発動率上昇し、ダメ無効":
                    {
                        string numStr_2 = "";
                        switch (num)
                        {
                            case 5:
                                {
                                    numStr_2 = "PTメンバーがそれぞれ";
                                    break;
                                }
                        }
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、<span class=\"fkg-value2\">" + numStr_2 + "</span><span class=\"fkg-value\">" + v2 + "</span>回ダメージ無効化";
                        return outStr;
                    }

                case "スキル発動率上昇し、シャイクリ泥率上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、シャインクリスタルのドロップ率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "スキル発動率上昇し、敵全体の攻撃力低下":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v1D + "</span>倍、<span class=\"fkg-value2\">敵全体の</spnan>攻撃力が<span class=\"fkg-value\">" + v2 + "</span>％低下";
                        return outStr;
                    }

                case "スキルLVでスキル発動率上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        double v2D = (double)(v2 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキル発動率が自身のスキルレベルに応じ<span class=\"fkg-value\">" + v1D + "</span>～<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;
                    }

                case "スキルダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加";
                        return outStr;
                    }

                case "PTと自身スキルダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、更に自身のスキルダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキルダメ上昇し、対ボスダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、更にボスに与えるダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキルダメ上昇し、シャイクリ泥率上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、シャインクリスタルのドロップ率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "スキルダメ上昇し、スキル発動率上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、<span class=\"fkg-value2\">" +numStr + "</span>スキル発動率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇":
                    {
                        string add = numStr;
                        switch (Ex2)
                        {
                            case "PT全体":
                                {
                                    add = "PT全体の";
                                    break;
                                }
                            case "自身":
                                {
                                    add = "自身";
                                    break;
                                }
                        }
                        double v2D = (double)(v2 + 100) / 100;
                        outStr = "<span class=\"fkg-value2\">" + numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、自身が攻撃を受けた次ターンに<span class=\"fkg-value2\">" + add + "</span>スキル発動率<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;
                    }

                case "スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇":
                    {
                        string add = numStr;
                        switch (Ex2)
                        {
                            case "PT全体":
                                {
                                    add = "PT全体の";
                                    break;
                                }
                            case "自身":
                                {
                                    add = "自身";
                                    break;
                                }
                        }
                        outStr = "<span class=\"fkg-value2\">" + numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、自身が攻撃を受けた次ターンに<span class=\"fkg-value2\">" + numStr + "</span>ダメージ<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "スキルダメ上昇し、スキルLVでスキル発動率上昇":
                    {
                        double v2D = (double)(v2 + 100) / 100;
                        double v3D = v2D + 0.08;
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、<span class=\"fkg-value2\">PT全体の</span>スキル発動率が自身のスキルレベルに応じ<span class=\"fkg-value\">" + v2D + "</span>～<span class=\"fkg-value\">" + v3D + "</span>倍";
                        return outStr;
                    }

                case "スキルダメ上昇し、ダメ無効":
                    {
                        string numStr_2 = "";
                        switch (num)
                        {
                            case 5:
                                {
                                    numStr_2 = "PTメンバーがそれぞれ";
                                    break;
                                }
                        }
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、<span class=\"fkg-value2\">" + numStr_2 + "</span><span class=\"fkg-value\">" + v2 + "</span>回ダメージを無効化";
                        return outStr;
                    }
                case "対ボスダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>ボスに与えるダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加";
                        return outStr;
                    }

                case "対ボスダメ上昇自身のボスダメ上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>ボスに与えるダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し自身のボスに与えるダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "対ボスダメ上昇し、敵の数で攻撃力上昇":
                    {
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>ボスに与えるダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加し、敵の数ｘ<span class=\"fkg-value\">" + v2 + "</span>％攻撃力が上昇";
                        return outStr;
                    }

                case "対ボス攻撃力上昇":
                    {
                        outStr = "ボス敵に対して<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "対ボス攻撃力上昇し、自身が更に上昇":
                    {
                        outStr = "ボス敵に対してPT全体の攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、<span class=\"fkg-value\">自身が</span>さらに<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "対ボス攻撃力上昇し、自身を含む2人がさらに上昇":
                    {
                        outStr = "ボス敵に対してPT全体の攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、<span class=\"fkg-value\">自身を含む2人が</span>さらに<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }


                case "対ボス攻撃力ダメ上昇":
                    {
                        outStr = "ボス敵に対して<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、与ダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "再行動":
                    {
                        if (turn == 1)
                        {
                            turnStr = "1T目に";
                        }

                        outStr = turnStr + "自身が敵にダメージを与えた後<span class=\"fkg-value\">" + v1 + "</span>％の確率で自身が再行動";
                        return outStr;
                    }

                case "自身が再行動し、シャイクリ泥率上昇":
                    {
                        if (turn == 1)
                        {
                            turnStr = "1T目に";
                        }

                        outStr = turnStr + "自身が敵にダメージを与えた後<span class=\"fkg-value\">" + v1 + "</span>％の確率で自身が再行動し、シャインクリスタルのドロップ率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "PTに再行動付与":
                    {
                        outStr = "<span class=\"fkg-value2\">PT全体の</span>再行動確率を<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "回避":
                    {
                        string turnV = "<span class=\"fkg-value2\">2</span>T目まで";
                        if (Ex2 == "3ターン")
                            turnV = "<span class=\"fkg-value2\">3</span>T目まで";
                        outStr = turnV + "<span class=\"fkg-value\">" + v1 + "</span>％、以降は<span class=\"fkg-value\">" + v2 + "</span>％の確率で回避";
                        return outStr;
                    }

                case "回避し、残りHPで攻撃力上昇":
                    {
                        string turnV = "<span class=\"fkg-value2\">2</span>T目まで";
                        if (Ex2 == "3ターン")
                            turnV = "<span class=\"fkg-value2\">3</span>T目まで";
                        outStr = turnV + "<span class=\"fkg-value\">" + v1 + "</span>％、以降は<span class=\"fkg-value\">" + v2 + "</span>％の確率で回避し、<span class=\"fkg-value2\">自身の</span>残HPにより攻撃力上昇";
                        return outStr;
                    }

                case "回避し、反撃":
                    {
                        int v1_2 = 0;
                        if(v1 == 80)
                            v1_2 = 50;
                        int v2_1 = 100;
                        string add = "";
                        if(Ex2 == "超反撃")
                        {
                            add = "(超反撃)";
                        }
                        double v2D = (double)v2 / 100;
                        return outStr =  "2T目まで<span class=\"fkg-value\">" + v1 + "</span>％、以降は<span class=\"fkg-value\">" + v1_2 + "</span>％の確率で回避し、攻撃を受けた時<span class=\"fkg-value\">" + v2_1 + "</span>％の確率で防御力の<span class=\"fkg-value\">" + v2D + "</span>倍を攻撃力に変換し反撃<span class=\"fkg-value3\">" + add + "</span>";
                    }

                case "回避し、敵3体が攻撃ミス":
                    {
                        int v1_2 = 0;
                        if(v1 == 80)
                            v1_2 = 50;
                        outStr = "2T目まで<span class=\"fkg-value\">" + v1 + "</span>％、以降は<span class=\"fkg-value\">" + v1_2 + "</span>％の確率で回避し、敵3体が<span class=\"fkg-value\">" + v2 + "</span>%の確率で攻撃ミス";
                        return outStr;
                    }

                case "迎撃":
                    {
                        outStr = "2T目まで<span class=\"fkg-value\">" + v1 + "</span>％、以降は<span class=\"fkg-value\">" + v2 + "</span>％の確率で回避し、回避成功時100%の確率で迎撃する";
                        return outStr;
                    }

                case "反撃"://超反撃について調査要
                    {
                        string add = "";
                        if(Ex2 == "超反撃")
                        {
                            add = "(超反撃)";
                        }
                        double v2D = (double)v2 / 100;
                        outStr = "攻撃を受けた時<span class=\"fkg-value\">" + v1 + "</span>％の確率で防御力の<span class=\"fkg-value\">" + v2D + "</span>倍を攻撃力に変換し反撃<span class=\"fkg-value3\">" + add + "</span>";
                        return outStr;
                    }

                case "追撃":
                    {
                        string add = "攻撃した敵に";
                        switch (Ex2)
                        {
                            case "追撃1":
                                {
                                    add = "攻撃した敵に";
                                    break;
                                }
                            case "追撃2":
                                {
                                    add = "敵全体に";
                                    break;
                                }
                            case "追撃3":
                                {
                                    outStr = "自身の攻撃後、自身はPT総合力の<span class=\"fkg-value\">" + v1 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">攻撃した敵に</span>追撃、更にPT総合力の<span class=\"fkg-value\">" + v2 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">敵全体に</span>追撃";
                                    return outStr;
                                }
                            case "追撃4:PT全体に単体20%付与":
                                {
                                    outStr = "<span class=\"fkg-value\">PTメンバーが</span><span class=\"fkg-value\">20</span>％の確率でPT総合力の<span class=\"fkg-value\">" + v1 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">攻撃した敵に</span>追撃";
                                    return outStr;
                                }
                        }
                        outStr = "自身の攻撃後、自身はPT総合力の<span class=\"fkg-value\">" + v1 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">" +add + "</span>追撃";
                        return outStr;
                    }

                case "追撃し、スキル発動率上昇":
                    {
                        string add = "攻撃した敵に";
                        switch (Ex2)
                        {
                            case "追撃1":
                                {
                                    add = "攻撃した敵に";
                                    break;
                                }
                            case "追撃2":
                                {
                                    add = "敵全体に";
                                    break;
                                }
                        }
                        double v2D = (double)(v2 + 100)/100;
                        outStr = "自身の攻撃後、自身はPT総合力の<span class=\"fkg-value\">" + v1 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">" +add + "</span>追撃し、<span class=\"fkg-value2\">PT全体の</span>スキル発動率が<span class=\"fkg-value\">" + v2D + "</span>倍";
                        return outStr;
                    }

                case "追撃し、スキルLVでスキル発動率上昇":
                    {
                        string add = "攻撃した敵に";
                        switch (Ex2)
                        {
                            case "追撃1":
                                {
                                    add = "攻撃した敵に";
                                    break;
                                }
                            case "追撃2":
                                {
                                    add = "敵全体に";
                                    break;
                                }
                        }
                        double v2D = (double)(v2 + 100) / 100;
                        double v3D = v2D + 0.08;
                        outStr = "自身の攻撃後、自身はPT総合力の<span class=\"fkg-value\">" + v1 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">" +add + "</span>追撃し、<span class=\"fkg-value2\">PT全体の</span>スキル発動率が自身のスキルレベルに応じ<span class=\"fkg-value\">" + v2D + "</span>～<span class=\"fkg-value\">" + v3D + "</span>倍";
                        return outStr;
                    }

                case "追撃し、自身が再行動":
                    {
                        if (turn == 1)
                        {
                            turnStr = "1T目に";
                        }
                        string add = "攻撃した敵に";
                        switch (Ex2)
                        {
                            case "追撃1":
                                {
                                    add = "攻撃した敵に";
                                    break;
                                }
                            case "追撃2":
                                {
                                    add = "敵全体に";
                                    break;
                                }
                        }
                        outStr = "自身の攻撃後、自身はPT総合力の<span class=\"fkg-value\">" + v1 + "</span>％を攻撃力に変換し<span class=\"fkg-value2\">" +add + "</span>追撃し、" + turnStr + "自身が敵にダメージを与えた後<span class=\"fkg-value\">" + v2 + "</span>％の確率で自身が再行動";
                        return outStr;
                    }

                case "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇":
                    {
                        outStr = "PTメンバーのいずれかのHPが50%以下の場合、自身の攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身の与ダメージが<span class=\"fkg-value\">" + v2 + "</span>％増加";
                        return outStr;
                    }

                case "ソラ効果上昇":
                    {
                        outStr = "ソーラードライブの効果が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "光ゲージ充填":
                    {
                        outStr = "光GAUGEが<span class=\"fkg-value\">" + v1 + "</span>％溜まった状態から討伐開始";
                        return outStr;
                    }
                case "光ゲージ充填シャイクリ泥率上昇":
                    {
                        outStr = "光GAUGEが<span class=\"fkg-value\">" + v1 + "</span>％溜まった状態から討伐開始し、シャインクリスタルのドロップ率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "光ゲージ充填し、自身が再行動":
                    {
                        outStr = "光GAUGEが<span class=\"fkg-value\">" + v1 + "</span>％溜まった状態から討伐開始し、自身が敵にダメージを与えた後<span class=\"fkg-value\">" + v2 + "</span>％の確率で自身が再行動";
                        return outStr;
                    }

                case "シャイクリ泥率上昇":
                    {
                        outStr = "シャインクリスタルのドロップ率が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "ソラ効果シャイクリ泥率上昇":
                    {
                        outStr = "ソーラードライブの効果が<span class=\"fkg-value\">" + v1 + "</span>％上昇しシャインクリスタルドロップ率が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "ソラ効果光ゲージ充填上昇":
                    {
                        outStr = "ソーラードライブの効果が<span class=\"fkg-value\">" + v1 + "</span>％上昇し光GAUGEが<span class=\"fkg-value\">" + v2 + "</span>％溜まった状態から討伐開始";
                        return outStr;
                    }

                case "ソラ効果上昇し自身が再行動":
                    {
                        outStr = "ソーラードライブの効果が<span class=\"fkg-value\">" + v1 + "</span>％上昇し、自身が敵にダメージを与えた後<span class=\"fkg-value\">" + v2 + "</span>％の確率で自身が再行動";
                        return outStr;
                    }

                case "ソラ発動毎に攻撃力上昇":
                    {
                        
                        outStr = "ソーラードライブ発動毎に<span class=\"fkg-value2\">" +numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "ソラ発動毎にダメ上昇":
                    {
                        outStr = "ソーラードライブ発動毎に<span class=\"fkg-value2\">" +numStr + "</span>与ダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加。最大<span class=\"fkg-value\">" + v2 + "</span>％";
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
                                    numStr = "自身を含む" + num + "人がそれぞれ";
                                    break;
                                }
                            case 5:
                                {
                                    numStr = "PTメンバーがそれぞれ";
                                    break;
                                }
                        }
                        if (v1 <= 1)
                            v1 = 1;
                        outStr = "<span class=\"fkg-value2\">" + numStr + "</span><span class=\"fkg-value\">" + v1 + "</span>回ダメージを無効化";
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
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>攻撃力を<span class=\"fkg-value\">" + v1 + "</span>％低下";
                        return outStr;
                    }

                case "防御力低下":
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
                        outStr = "<span class=\"fkg-value2\">" + numStr + "</span>防御力を<span class=\"fkg-value\">" + v1 + "</span>％低下";
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
                        outStr = "<span class=\"fkg-value2\">" +numStr + "</span>スキル発動率を<span class=\"fkg-value\">" + v1 + "</span>％低下";
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
                        outStr = "<span class=\"fkg-value2\">" + numStr + "</span><span class=\"fkg-value\">" + v1 + "</span>％の確率で攻撃ミス";
                        return outStr;
                    }

                case "ターン毎に行動回数減":
                    {
                        
                        outStr = "ターン毎に<span class=\"fkg-value\">" + v1 + "</span>％の確率で敵の行動回数が<span class=\"fkg-value\">" + v2 + "</span>回減少";
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
                                    num--;
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
                        string Zan = "<span class=\"fkg-value\">" + "斬" + "</span>";
                        string Da = "<span class=\"fkg-value2\">" + "打" + "</span>";
                        string Totu = "<span class=\"fkg-value3\">" + "突" + "</span>";
                        string Ma = "<span class=\"fkg-value4\">" + "魔" + "</span>";

                        switch (v1)
                        {
                            case 1://1属性
                                {
                                    switch(Ex2){
                                        case "斬":
                                            numAtt = Zan;
                                            break;
                                        case "打":
                                            numAtt = Da;
                                            break;
                                        case "突":
                                            numAtt = Totu;
                                            break;
                                        case "魔":
                                            numAtt = Ma;
                                            break;
                                    }
                                    break;
                                }
                            case 2://2属性
                                {
                                    switch (Ex2)
                                    {
                                        case "斬打":
                                            {
                                                numAtt = Zan + "、" + Da;
                                                break;
                                            }
                                        case "斬突":
                                            {
                                                numAtt = Zan + "、" + Totu;
                                                break;
                                            }
                                        case "斬魔":
                                            {
                                                numAtt = Zan + "、" + Ma;
                                                break;
                                            }
                                        case "打突":
                                            {
                                                numAtt = Da + "、" + Totu;
                                                break;
                                            }
                                        case "打魔":
                                            {
                                                numAtt = Da + "、" + Ma;
                                                break;
                                            }
                                        case "突魔":
                                            {
                                                numAtt = Totu + "、" + Ma;
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
                                            numAtt = Zan + "、" + Da + "、" + Totu;
                                            break;
                                        }
                                    case "斬打魔":
                                        {
                                            numAtt = Zan + "、" + Da + "、" + Ma;
                                            break;
                                        }
                                    case "斬突魔":
                                        {
                                            numAtt = Zan + "、" + Totu + "、" + Ma;
                                            break;
                                        }
                                    case "打突魔":
                                        {
                                            numAtt = Da + "、" + Totu + "、" + Ma;
                                            break;
                                        }
                                }
                                break;

                        }
                        outStr = "<span class=\"fkg-value2\">" + numStr + "</span>" + numAtt + "属性を付与";
                        return outStr;
                    }

                case "HP回復":
                    {
                        switch (num)
                        {
                            case 1:
                                {
                                    outStr = "毎ターン<span class=\"fkg-value\">" + v1 + "</span>％の確率で自身の最大HPの<span class=\"fkg-value\">" + v2 + "</span>％回復";
                                    break;
                                }
                            case 3:
                                {
                                    outStr = "自身のHPが0になった場合、1度だけ敵のターン終了後、自身を含む3人のHPを<span class=\"fkg-value\">" + v2 + "</span>％回復";
                                    break;
                                }
                            case 5:
                                {
                                    outStr = "PT全体がそれぞれ毎ターン<span class=\"fkg-value\">" + v1 + "</span>％の確率で最大HPの<span class=\"fkg-value\">" + v2 + "</span>％回復";
                                    break;
                                }
                        }
                        return outStr;
                    }

                case "PT移動力増加":
                    {
                        outStr = "パーティの移動力が<span class=\"fkg-value\">" + v1 + "</span>増加";
                        return outStr;
                    }

                case "PT移動力増加し、スキルLVでスキル発動率上昇":
                    {
                        double v2D = (double)(v2 + 100) / 100;
                        double v3D = v2D + 0.08;
                        outStr = "PTの移動力が<span class=\"fkg-value\">" + v1 + "</span>増加し、PT全体のスキル発動率が自身のスキルレベルに応じ<span class=\"fkg-value\">" + v2D + "</span>～<span class=\"fkg-value\">" + v3D + "</span>倍";
                        return outStr;
                    }

                case "PT移動力増加し、移動力を攻撃力に追加":
                    {
                        outStr = "PTの移動力が<span class=\"fkg-value\">" + v1 + "</span>増加し、PT全体の攻撃力に移動力の<span class=\"fkg-value\">" + v2 + "</span>％を追加";
                        return outStr;
                    }

                case "PT移動力増加し、対ボス攻撃力上昇":
                    {
                        outStr = "PTの移動力が<span class=\"fkg-value\">" + v1 + "</span>増加し、ボス敵に対して<span class=\"fkg-value\">PT全体の</span>攻撃力が<span class=\"fkg-value\">" + v2 + "</span>％上昇";
                        return outStr;
                    }

                case "PT移動力増加し、自身が再行動":
                    {
                        if (turn == 1)
                        {
                            turnStr = "1T目に";
                        }
                        outStr = "PTの移動力が<span class=\"fkg-value\">" + v1 + "</span>増加し、" + turnStr + "自身が敵にダメージを与えた後<span class=\"fkg-value\">" + v2 + "</span>％の確率で自身が再行動";
                        return outStr;
                    }

                case "自身が攻撃を受けた次Tにスキル発動率上昇":
                    {
                        double v1D = (double)(v1 + 100) / 100;
                        outStr = "自身が攻撃を受けた次ターンに<span class=\"fkg-value2\">" + numStr + "</span>スキル発動率<span class=\"fkg-value\">" + v1D + "</span>倍";
                        return outStr;
                    }

                case "自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇":
                    {
                        string add = numStr;
                        switch (Ex2)
                        {
                            case "PT全体":
                                {
                                    add = "PT全体の";
                                    break;
                                }
                            case "自身":
                                {
                                    add = "自身";
                                    break;
                                }
                        }
                                double v1D = (double)(v1 + 100) / 100;
                        outStr = "自身が攻撃を受けた次ターンに<span class=\"fkg-value2\">" + numStr + "</span>スキル発動率<span class=\"fkg-value\">" + v1D + "</span>倍し、<span class=\"fkg-value2\">" + add + "</span>与ダメージが<span class=\"fkg-value\">" + v2 + "</span>%増加";
                        return outStr;
                    }

                case "自身が攻撃を受けた次Tに攻撃力上昇":
                    {
                        outStr = "自身が攻撃を受けた次ターンに<span class=\"fkg-value2\">" + numStr + "</span>攻撃力<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "自身が攻撃を受けた次Tにダメ上昇":
                    {
                        outStr = "自身が攻撃を受けた次ターンに<span class=\"fkg-value2\">" + numStr + "</span>ダメージ<span class=\"fkg-value\">" + v1 + "</span>％増加";
                        return outStr;
                    }

                case "その他(MAP画面スキル)":
                    {
                        outStr = "MAP画面用アビリティ。現在表示出来ない";
                        return outStr;
                    }
                case "MAP画面アビと、攻撃力上昇":
                    {
                        outStr = "MAP画面アビリティ、更に<span class=\"fkg-value2\">" + turnStr + numStr + "</span>攻撃力が<span class=\"fkg-value\">" + v1 + "</span>％上昇";
                        return outStr;
                    }

                case "MAP画面アビと、スキルダメ上昇":
                    {
                        outStr = "MAP画面アビリティ、更に<span class=\"fkg-value2\">" + turnStr + numStr + "</span>スキルダメージが<span class=\"fkg-value\">" + v1 + "</span>％増加";
                        return outStr;
                    }

                case "MAP画面アビと、移動力増加":
                    {
                        outStr = "MAP画面アビリティ、更にパーティの移動力が<span class=\"fkg-value\">" + v1 + "</span>増加";
                        return outStr;
                    }
            }

            return "未登録アビ";
        }

        DataTable AddATTcolor(DataTable dt)
        {
            for(int i = 0; i<dt.Rows.Count;i++){
                switch(dt.Rows[i]["ATT"])
                {
                    case "斬"://赤
                        {
                            dt.Rows[i]["ATT"] = "<span class=\"fkg-value\">" + Convert.ToString(dt.Rows[i]["ATT"]) + "</span>";
                            break;
                        }
                    case "打"://青
                        {
                            dt.Rows[i]["ATT"] = "<span class=\"fkg-value2\">" + Convert.ToString(dt.Rows[i]["ATT"]) + "</span>";
                            break;
                        }
                    case "突"://黄
                        {
                            dt.Rows[i]["ATT"] = "<span class=\"fkg-value3\">" + Convert.ToString(dt.Rows[i]["ATT"]) + "</span>";
                            break;
                        }
                    case "魔"://紫
                        {
                            dt.Rows[i]["ATT"] = "<span class=\"fkg-value4\">" + Convert.ToString(dt.Rows[i]["ATT"]) + "</span>";
                            break;
                        }
                }
            }
            return dt;
        }
        string OutSkillExplain(string sType, int sRatio, string fkgName, int movilFlag)
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
                            if (movilFlag == 1)
                            {
                                outExplain += "<span class=\"fkg-value2\">4体</span><span class=\"fkg-value\">" + sRatioD + "</span>倍";
                            }
                            else
                            {
                                outExplain += "<span class=\"fkg-value2\">敵4体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍";
                            }
                        }
                        else
                        {
                            if (movilFlag == 1)
                            {
                                outExplain += "<span class=\"fkg-value\">" + sRatioD + "</span>倍";
                            }
                            else
                            {
                                outExplain += "<span class=\"fkg-value2\">敵全体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍";
                            }
                        }
                        return outExplain;
                    }
                case "2体":
                    {
                        sRatioD = (double)sRatio / 10;
                        if (movilFlag == 1)
                        {
                            outExplain += "<span class=\"fkg-value2\">敵2体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍";
                        }
                        else
                        {
                            outExplain += "<span class=\"fkg-value2\">敵2体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍";
                        }
                        return outExplain;
                    }
                case "変則":
                    {
                        if (sRatio == 47)
                        {
                            if (movilFlag == 1)
                            {
                                outExplain += "<span class=\"fkg-value2\">単体</span><span class=\"fkg-value\">4.7</span>倍他";
                                //outExplain += "単体4.7倍、2体2.8倍、3体2.2倍";
                            }
                            else
                            {
                                outExplain += "<span class=\"fkg-value2\">敵単体に</span><span class=\"fkg-value\">4.7</span>倍、<span class=\"fkg-value2\">敵2体に</span><span class=\"fkg-value\">2.8</span>倍、<span class=\"fkg-value2\">敵3体に</span><span class=\"fkg-value\">2.2</span>倍";
                            }
                        }
                        else
                        {
                            outExplain = "未定義";
                        }
                        return outExplain;
                    }
                case "変則吸収":
                    {
                        if (sRatio == 47)
                        {
                            if (movilFlag == 1)
                            {
                                outExplain += "<span class=\"fkg-value2\">単体</span><span class=\"fkg-value\">4.7</span>倍し吸収他";
                                //outExplain += "単体4.7倍、2体2.8倍、3体2.2倍";
                            }
                            else
                            {
                                outExplain += "<span class=\"fkg-value2\">敵単体に</span><span class=\"fkg-value\">4.7</span>倍し<span class=\"fkg-value4\">吸収</span>、<span class=\"fkg-value2\">敵2体に</span><span class=\"fkg-value\">2.8</span>倍し<span class=\"fkg-value4\">吸収</span>、<span class=\"fkg-value2\">敵3体に</span><span class=\"fkg-value\">2.2</span>倍し<span class=\"fkg-value4\">吸収</span>";
                            }
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
                            if (movilFlag == 1)
                            {
                                outExplain += "<span class=\"fkg-value2\">3</span>回<span class=\"fkg-value\">" + sRatioD + "</span>倍、全体0.5倍";
                            }
                            else
                            {
                                outExplain += "敵に<span class=\"fkg-value2\">3</span>回<span class=\"fkg-value\">" + sRatioD + "</span>倍、<span class=\"fkg-value2\">敵全体に</span>0.5倍";
                            }
                        }
                        else
                        {
                            if (movilFlag == 1)
                            {
                                outExplain += "<span class=\"fkg-value2\">3</span>回<span class=\"fkg-value\">" + sRatioD + "</span>倍";
                            }
                            else
                            {
                                outExplain += "敵に<span class=\"fkg-value2\">3</span>回<span class=\"fkg-value\">" + sRatioD + "</span>倍";
                            }
                        }
                        return outExplain;
                    }
                case "吸収":
                    {
                        sRatioD = (double)sRatio / 10;
                        if (movilFlag == 1)
                        {
                            outExplain += "<span class=\"fkg-value2\">敵単体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍し<span class=\"fkg-value4\">吸収</span>";
                        }
                        else
                        {
                            outExplain += "<span class=\"fkg-value2\">敵単体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍し<span class=\"fkg-value4\">吸収</span>";
                        }
                        return outExplain;
                    }
                case "単体":
                    {
                        sRatioD = (double)sRatio / 10;
                        if (movilFlag == 1)
                        {
                            outExplain += "<span class=\"fkg-value\">" + sRatioD + "</span>倍";
                        }
                        else
                        {
                            outExplain += "<span class=\"fkg-value2\">敵単体に</span><span class=\"fkg-value\">" + sRatioD + "</span>倍";
                        }
                        return outExplain;
                    }
                default:
                    outExplain = "未定義";
                    return outExplain;
            }

        }

        //検索されたDB内の全てのFKGに対して、どの検索Keyがhitしたかの情報を入力する関数のオーバーロード

        //protected int[] GetOutAbiNo(int Key1)
        //{
        //    int[] ocupado =  new int[4];
        //    int[] output = new int[4];
        //    int outCount = 0;

        //    //1～4までの数のうち、Key1に一致する数は抜く
        //    ocupado[Key1] = 1;
        //    for(int i = 0;i<4;i++)
        //    {
        //        if(ocupado[i]==0)
        //        {
        //            output[outCount++] = i;
        //        }
        //    }
        //    return output;
        //}

        //protected int[] GetOutAbiNo(int Key1, int Key2)
        //{
        //    int[] ocupado = new int[4];
        //    int[] output = new int[4];
        //    int outCount = 0;

        //    //1～4までの数のうち、Key1に一致する数は抜く
        //    //Key2の数字は優先的に返す

        //    ocupado[Key1] = 1;
        //    output[0] = Key2 - 1;

        //    for (int i = 1; i < 4; i++)
        //    {
        //        if (ocupado[i] == 0)
        //        {
        //            output[outCount++] = i;
        //        }
        //    }
        //    return output;
        //}


        //Keyは1-4の値をとる
        protected int[] GetOutAbiNo(int Key1, int Key2, int Key3)
        {
            int[] ocupado = new int[4];
            int[] output = new int[4];
            int outCount = 0;

            //1～4までの数のうち、Key1に一致する数は抜く
            //Key2の数字は優先的に返す

            if((Key1 == 0) && ((Key2 != 0) || (Key3 != 0)))//Key1が0で、他のキーがある場合、キーを書き換える
            {
                if(Key3 == 0)
                {
                    Key1 = Key2;
                    Key2 = 0;
                }

                else if (Key2 == 0)
                {
                    Key1 = Key3;
                    Key3 = 0;
                }
                else //Key2もKey3もある場合
                {
                    Key1 = Key2;
                    Key2 = Key3;
                    Key3 = 0;
                }
            }


            if (Key1 != 0)//スキルタイプ検索じゃない場合
            { 
                ocupado[Key1 - 1] = 1;
                output[0] = Key1 - 1;
                outCount++;
            }
            //Key2あるいはKey3がKey1とだぶってる場合、Keyの書き換え
            if ((Key1 == Key2)&&(Key2 != Key3))
            {
                Key2 = Key3;
                Key3 = 0;
            }
            if ((Key1 == Key3) && (Key2 != Key3))
            {
                Key3 = 0;
            }
            if ((Key1==Key2)&&(Key2==Key3))
            {
                Key2 = 0;
                Key3 = 0;
            }
            //key2とkey3が等しい場合
            if((Key1 != Key2)&&(Key2==Key3))
            {
                Key3 = 0;
            }


            //Key2以降の値により、ocupadoのさらなる追加
            if (Key2 != 0)
            {
                output[1] = Key2 - 1;
                ocupado[Key2 - 1] = 1;
                outCount++;

                if (Key3 != 0)
                {
                    output[2] = Key3 - 1;
                    ocupado[Key3 - 1] = 1;
                    outCount++;
                }
            }
            
            //1-4のうち、どの数字が使えるか判定
            for (int i = 0; i < 4; i++)
            {
                if (ocupado[i] == 0)
                {
                    //使える数字はoutputに入れる
                    output[outCount++] = i;
                }
            }
            return output;
        }

        



        //文字列が数字かどうか確認
        public static bool IsAsciiDigit(char c)
        {
            return '0' <= c && c <= '9';
        }

        protected void DropDownList1Changed(object sender, EventArgs e)
        {
            
            Main_Caluc();
        }

        protected void DropDownList2Changed(object sender, EventArgs e)
        {

            Main_Caluc();
        }

        protected void DropDownList3Changed(object sender, EventArgs e)
        {

            Main_Caluc();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void CheckBoxList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }
        protected void IniciarAbilitySerch()
        {
            FKG_register fkg_register = new FKG_register();
            string[,] registeredString = fkg_register.DecodeFkgName();
            
            if (HttpContext.Current.Request.Cookies["FkgName"] == null)
            {
                this.registido.Text = "登録した人数は"+ 0 + "人";
                return;
            }


            this.registido.Text = "登録した人数は" + registeredString.GetLength(0) + "人";
        }

        protected void TextBox101_TextChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void TextBox102_TextChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }

        protected void TextBox103_TextChanged(object sender, EventArgs e)
        {
            Main_Caluc();
        }
    }//クラスお終い
}