using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace 花騎士ツール＿NEO
{
    public partial class Fkg_select_neo : System.Web.UI.Page
    {
        private SqlConnection cn = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        //private SqlDataReader rd;
        private string cnstr =
            //@"Data Source=(LocalDB)\MSSQLLocalDB;" +
            //@"AttachDbFilename=""C:\Users\yonki\OneDrive\database\fkgdata.mdf"";" +
            //@"Integrated Security = True; Connect Timeout = 30";
            @"Server=tcp:fkg-data-yonkim.database.windows.net,1433;Initial Catalog=花騎士データ;Persist Security Info=False;User ID=yonkim;Password=Hornet600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //PT内の各メンバーIDの値格納用文字列
        private int[] mbrid = new int[5];
        //スキルレベル格納
        private int[] skilLv = new int[5];

        private string[] mbrName = new string[5];



        protected void Page_Load(object sender, EventArgs e)
        {
            //Button9.Click += new EventHandler(this.Button1_Click);
            //Button9.Click += new EventHandler(this.Button2_Click);
            //Button9.Click += new EventHandler(this.Button3_Click);
            //Button9.Click += new EventHandler(this.Button4_Click);
            //Button9.Click += new EventHandler(this.Button5_Click);
        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
            //リスト生成
            //リストクリア
            DropDownList3.Items.Clear();
            DropDownList3.ClearSelection();

            //クエリを生成する
            //DropDownListの値を参照して生成する
            //ドロップダウンリストの指定により分類
            //0 指定なし Add query不要
            //1以上 ANDにて条件追加

            int query_type = 0;
            string[] add_Query_Component = new string[10];

            string rarityQuerry = "";
            string attQuerry = "";

            

            
            {
                //Rarity指定
                if (!this.RadioButton201.Checked)
                {
                    query_type++;
                    //radioButton106.checked　☆6の場合のみ
                    add_Query_Component[query_type] = "Rarity = 6";
                }

                //ATT指定
                if (!this.RadioButton1001.Checked)
                {
                    query_type++;

                    //各属性値の指定
                    if (this.RadioButton1002.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'斬'";
                    }
                    else if (this.RadioButton1003.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'打'";
                    }
                    else if (this.RadioButton1004.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'突'";
                    }
                    else if (this.RadioButton1005.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'魔'";
                    }
                }

                //Skill Type指定
                // checkboxlist用コード
                int itemCount = 0;
                int checkCount = 0;
                string queryCheckboxlist = "";

                foreach (ListItem item in CheckBoxList11.Items)
                {
                    if (item.Selected)
                    {
                        if (checkCount == 0)
                        {
                            //処理開始
                            queryCheckboxlist += "(";
                        }
                        else
                        {
                            queryCheckboxlist += " OR ";
                        }
                        queryCheckboxlist += "SType = N'" + item.Value + "'";
                        checkCount++;
                    }
                    if (itemCount == 5)
                    {
                        //最後にカッコのフタ
                        queryCheckboxlist += ")";
                        if (checkCount == 0)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。1人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li1 = new ListItem("スキルタイプのどれか一つにチェックを入れて下さい。", "0");
                            this.DropDownList3.Items.Add(li1);
                            return;
                        }
                        query_type++;
                        add_Query_Component[query_type] = queryCheckboxlist;
                    }
                    itemCount++;


                }

               

                //ドロップダウンリスト処理1
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList21.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理2
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList26.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理3
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList31.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //アビクイック選択
                //1.65倍
                if (CheckBox111.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "(((A1Ex1 = N'スキル発動率上昇' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率上昇' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率上昇' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率上昇' AND A4V1 = 65)) OR ((A1Ex1 = N'スキル発動率1T目と3T目' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率1T目と3T目' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率1T目と3T目' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率1T目と3T目' AND A4V1 = 65)))";
                }
                //昇華
                if (CheckBox112.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "Id > 10000";
                }


                //属性付与
                // checkboxlist用コード
                int itemCountAddAtt = 0;
                int checkCountAddAtt = 0;
                int[] attInput = new int[4];

                foreach (ListItem item in CheckBoxList12.Items)
                {
                    if (item.Selected)
                    {

                        attInput[itemCountAddAtt] = 1;
                        checkCountAddAtt++;
                    }
                    if (itemCountAddAtt == 3)
                    {

                        if (checkCountAddAtt == 4)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('')" + "</script>";
                            //Response.Write(script);
                            ListItem li2 = new ListItem("全属性付与出来る花騎士は存在しません。", "0");
                            this.DropDownList3.Items.Add(li2);
                            return;
                        }
                        if (checkCountAddAtt != 0)
                        {
                            query_type++;
                            add_Query_Component[query_type] = Add_ATT(attInput[0], attInput[1], attInput[2], attInput[3]);
                        }
                    }
                    itemCountAddAtt++;


                }


                //攻撃力低下
                if (CheckBox113.Checked == true)
                {
                    query_type++;
                    //対象三人以上の処理
                    if (CheckBox114.Checked == true)
                    {
                        add_Query_Component[query_type] += "("
                           + "(((A1NO > 2) AND (A1Ex1 = N'攻撃力低下')) OR ((A2NO > 2) AND (A2Ex1 = N'攻撃力低下')) OR ((A3NO > 2) AND (A3Ex1 = N'攻撃力低下')) OR ((A4NO > 2) AND (A4Ex1 = N'攻撃力低下'))) "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                    else
                    {
                        add_Query_Component[query_type] += "("
                           + "(A1Ex1 = N'攻撃力低下' OR A2Ex1 = N'攻撃力低下' OR A3Ex1 = N'攻撃力低下' OR A4Ex1 = N'攻撃力低下') "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                }


            }

            string add_querry = "";

            //add_query作りこみ
            for (int i = 1; i <= query_type; i++)
            {
                if (i == 1)
                {
                    add_querry = " WHERE " + add_Query_Component[1];
                }
                else
                {
                    add_querry += " AND " + add_Query_Component[i];
                }
            }






            //string list_querry = "SELECT Id, Name FROM [dbo].[Fkgmbr]" + add_querry;
            string list_querry = "SELECT Id, Name,A1Ex1,A1V1,A1V2,A2Ex1,A2V1,A2V2,A3Ex1,A3V1,A3V2,A4Ex1,A4V1,A4V2 FROM [dbo].[Fkgmbr]" + add_querry;


            //GetDataによりdataset入手する

            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);

            if (ds_fkg.Tables[0].Rows.Count == 0)
            {
                //メッセージボックス表示
                //string script = "<script language=javascript>" + "window.alert('検索結果が見つかりません。1人目の花騎士は読込不可能です。')" + "</script>";
                //Response.Write(script);
                ListItem li3 = new ListItem("見つかりません。", "0");
                this.DropDownList3.Items.Add(li3);
                return;
            }


            //検索アビリティ1に選択があるなら、アビリティ値でのソートの準備処理を行う
            //関数に、datatableを送り、更に検索アビリティ1の値を送る
            DataTable dt_out;
            int abi1Flag = 0;
            //検索アビリティ1の値の取得
            {
                //リストから値を得て関数に値を投入
                string abi_Select = DropDownList21.Text;
                if (abi_Select != "未選択")
                {
                    //DataTable加工関数呼び出し
                    ds_fkg.Tables[0].Columns.Add("SortValue", typeof(int));
                    dt_out = AddSortValue(ds_fkg.Tables[0],abi_Select,ref abi1Flag);
                }
                else
                {
                    dt_out = ds_fkg.Tables[0];
                }
            }

            //レコードのソートを行う
            DataView view = new DataView(dt_out);

            string Name = "Name";

            //abi1が選択されていて、フラグがonの場合のみ、アビ1の値によるソート
            if (abi1Flag == 1)
            {
                view.Sort = "SortValue DESC";
                Name = "Name and Value";
            }
            else
            {
                //通常時
                view.Sort = "Name";
            }

            //所持キャラをクッキーから読み込んで、マーカーつける処理
            DataTable dt_out1 = MarkFromCookie(view.ToTable());

            //datasetの値をDropDownList3に入力する
            this.DropDownList3.DataSource = dt_out1;
            this.DropDownList3.DataTextField = Name;
            this.DropDownList3.DataValueField = "Id";
            this.DropDownList3.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //リスト生成
            //リストクリア
            DropDownList6.Items.Clear();
            DropDownList6.ClearSelection();

            //クエリを生成する
            //DropDownListの値を参照して生成する
            //ドロップダウンリストの指定により分類
            //0 指定なし Add query不要
            //1以上 ANDにて条件追加

            int query_type = 0;
            string[] add_Query_Component = new string[10];

            string rarityQuerry = "";
            string attQuerry = "";

            

           
            {
                //Rarity指定
                if (!this.RadioButton205.Checked)
                {
                    query_type++;
                    //radioButton206.checked　☆6の場合のみ
                    add_Query_Component[query_type] = "Rarity = 6";
                }

                //ATT指定
                if (!this.RadioButton1006.Checked)
                {
                    query_type++;

                    //各属性値の指定
                    if (this.RadioButton1007.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'斬'";
                    }
                    else if (this.RadioButton1008.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'打'";
                    }
                    else if (this.RadioButton1009.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'突'";
                    }
                    else if (this.RadioButton1010.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'魔'";
                    }
                }
                //Skill Type指定
                // checkboxlist用コード
                int itemCount = 0;
                int checkCount = 0;
                string queryCheckboxlist = "";

                foreach (ListItem item in CheckBoxList21.Items)
                {
                    if (item.Selected)
                    {
                        if (checkCount == 0)
                        {
                            //処理開始
                            queryCheckboxlist += "(";
                        }
                        else
                        {
                            queryCheckboxlist += " OR ";
                        }
                        queryCheckboxlist += "SType = N'" + item.Value + "'";
                        checkCount++;
                    }
                    if (itemCount == 5)
                    {
                        //最後にカッコのフタ
                        queryCheckboxlist += ")";
                        if (checkCount == 0)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。2人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li1 = new ListItem("スキルタイプのどれか一つにチェックを入れて下さい。", "0");
                            this.DropDownList6.Items.Add(li1);
                            return;
                        }
                        query_type++;
                        add_Query_Component[query_type] = queryCheckboxlist;
                    }
                    itemCount++;


                }
                ////Skill Type指定
                //if (!((CheckBox21.Checked == true) & (CheckBox22.Checked == true) & (CheckBox23.Checked == true) & (CheckBox24.Checked == true) & (CheckBox25.Checked == true) & (CheckBox26.Checked == true)))
                //{//全チェック入りならこの処理はスルー
                //    if (!((CheckBox21.Checked == false) & (CheckBox22.Checked == false) & (CheckBox23.Checked == false) & (CheckBox24.Checked == false) & (CheckBox25.Checked == false) & (CheckBox26.Checked == false)))
                //    {
                //        query_type++;
                //        int i = 0;//カウンタ


                //        if (CheckBox21.Checked == true)//全体
                //        {
                //            add_Query_Component[query_type] += "(SType = N'全体'";
                //            i++;
                //        }
                //        if (CheckBox22.Checked == true)//2体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'2体'";
                //            i++;
                //        }
                //        if (CheckBox23.Checked == true)//変則
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'変則'";
                //            i++;
                //        }
                //        if (CheckBox24.Checked == true)//吸収
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'吸収'";
                //            i++;
                //        }
                //        if (CheckBox25.Checked == true)//複数回
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'複数回'";
                //            i++;
                //        }
                //        if (CheckBox26.Checked == true)//単体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'単体'";
                //            i++;
                //        }
                //        //最後にカッコのフタ
                //        add_Query_Component[query_type] += ")";
                //    }
                //    else//スキルチェック全無しの場合
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。3人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //}

                //ドロップダウンリスト処理
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList22.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理2
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList27.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理4
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList32.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }

                //アビクイック選択
                //1.65倍
                if (CheckBox121.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "(((A1Ex1 = N'スキル発動率上昇' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率上昇' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率上昇' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率上昇' AND A4V1 = 65)) OR ((A1Ex1 = N'スキル発動率1T目と3T目' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率1T目と3T目' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率1T目と3T目' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率1T目と3T目' AND A4V1 = 65)))";
                }
                //昇華
                if (CheckBox122.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "Id > 10000";
                }
                //属性付与
                // checkboxlist用コード
                int itemCountAddAtt = 0;
                int checkCountAddAtt = 0;
                int[] attInput = new int[4];

                foreach (ListItem item in CheckBoxList22.Items)
                {
                    if (item.Selected)
                    {

                        attInput[itemCountAddAtt] = 1;
                        checkCountAddAtt++;
                    }
                    if (itemCountAddAtt == 3)
                    {

                        if (checkCountAddAtt == 4)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。2人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li2 = new ListItem("全属性付与出来る花騎士は存在しません。", "0");
                            this.DropDownList6.Items.Add(li2);
                            return;
                        }
                        if (checkCountAddAtt != 0)
                        {
                            query_type++;
                            add_Query_Component[query_type] = Add_ATT(attInput[0], attInput[1], attInput[2], attInput[3]);
                        }
                    }
                    itemCountAddAtt++;


                }
                ////属性付与
                //if ((CheckBox221.Checked == true) || (CheckBox222.Checked == true) || (CheckBox223.Checked == true) || (CheckBox224.Checked == true))
                //{
                //    //チェックボックスが全て埋まっている場合は抜ける
                //    if ((CheckBox221.Checked == true) && (CheckBox222.Checked == true) && (CheckBox223.Checked == true) && (CheckBox224.Checked == true))
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。2人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //    //少なくともどれか一つは属性にチェック入っているので処理開始
                //    query_type++;
                //    int[] att_input = new int[4];
                //    //斬
                //    if (CheckBox221.Checked == true)
                //    {
                //        att_input[0] = 1;
                //    }
                //    //打
                //    if (CheckBox222.Checked == true)
                //    {
                //        att_input[1] = 1;
                //    }
                //    //突
                //    if (CheckBox223.Checked == true)
                //    {
                //        att_input[2] = 1;
                //    }
                //    //魔
                //    if (CheckBox224.Checked == true)
                //    {
                //        att_input[3] = 1;
                //    }

                //    add_Query_Component[query_type] += Add_ATT(att_input[0], att_input[1], att_input[2], att_input[3]);
                //}
                //攻撃力低下
                if (CheckBox123.Checked == true)
                {
                    query_type++;
                    //対象三人以上の処理
                    if (CheckBox124.Checked == true)
                    {
                        add_Query_Component[query_type] += "("
                           + "(((A1NO > 2) AND (A1Ex1 = N'攻撃力低下')) OR ((A2NO > 2) AND (A2Ex1 = N'攻撃力低下')) OR ((A3NO > 2) AND (A3Ex1 = N'攻撃力低下')) OR ((A4NO > 2) AND (A4Ex1 = N'攻撃力低下'))) "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                    else
                    {
                        add_Query_Component[query_type] += "("
                           + "(A1Ex1 = N'攻撃力低下' OR A2Ex1 = N'攻撃力低下' OR A3Ex1 = N'攻撃力低下' OR A4Ex1 = N'攻撃力低下') "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                }


            }

            string add_querry = "";

            //add_query作りこみ
            for (int i = 1; i <= query_type; i++)
            {
                if (i == 1)
                {
                    add_querry = " WHERE " + add_Query_Component[1];
                }
                else
                {
                    add_querry += " AND " + add_Query_Component[i];
                }
            }


            //string list_querry = "SELECT Id, Name FROM [dbo].[Fkgmbr]" + add_querry;
            string list_querry = "SELECT Id, Name,A1Ex1,A1V1,A1V2,A2Ex1,A2V1,A2V2,A3Ex1,A3V1,A3V2,A4Ex1,A4V1,A4V2 FROM [dbo].[Fkgmbr]" + add_querry;


            //GetDataによりdataset入手する

            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);

            if (ds_fkg.Tables[0].Rows.Count == 0)
            {
                //メッセージボックス表示
                //string script = "<script language=javascript>" + "window.alert('検索結果が見つかりません。2人目の花騎士は読込不可能です。')" + "</script>";
                //Response.Write(script);
                ListItem li3 = new ListItem("見つかりません。", "0");
                this.DropDownList6.Items.Add(li3);
                return;
            }

            //検索アビリティ1に選択があるなら、アビリティ値でのソートの準備処理を行う
            //関数に、datatableを送り、更に検索アビリティ1の値を送る
            DataTable dt_out;
            int abi1Flag = 0;
            //検索アビリティ1の値の取得
            {
                //リストから値を得て関数に値を投入
                string abi_Select = DropDownList22.Text;
                if (abi_Select != "未選択")
                {
                    //DataTable加工関数呼び出し
                    ds_fkg.Tables[0].Columns.Add("SortValue", typeof(int));
                    dt_out = AddSortValue(ds_fkg.Tables[0], abi_Select, ref abi1Flag);
                }
                else
                {
                    dt_out = ds_fkg.Tables[0];
                }
            }

            //レコードのソートを行う
            DataView view = new DataView(dt_out);
            string Name = "Name";

            //abi1が選択されていて、フラグがonの場合のみ、アビ1の値によるソート
            if (abi1Flag == 1)
            {
                view.Sort = "SortValue DESC";
                Name = "Name and Value";
            }
            else
            {
                //通常時
                view.Sort = "Name";
            }


            //所持キャラをクッキーから読み込んで、マーカーつける処理
            DataTable dt_out1 = MarkFromCookie(view.ToTable());

            //datasetの値をDropDownList6に入力する
            this.DropDownList6.DataSource = dt_out1;
            this.DropDownList6.DataTextField = Name;
            this.DropDownList6.DataValueField = "Id";
            this.DropDownList6.DataBind();
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            
            //リスト生成
            //リストクリア
            DropDownList9.Items.Clear();
            DropDownList9.ClearSelection();

            //クエリを生成する
            //DropDownListの値を参照して生成する
            //ドロップダウンリストの指定により分類
            //0 指定なし Add query不要
            //1以上 ANDにて条件追加

            int query_type = 0;
            string[] add_Query_Component = new string[10];

            string rarityQuerry = "";
            string attQuerry = "";

            //ドロップダウンリスト使う場合
            //useDropdown = 1;//0:使わない 1:使う

            
            {
                //Rarity指定
                if (!this.RadioButton209.Checked)
                {
                    query_type++;
                    //radioButton106.checked　☆6の場合のみ
                    add_Query_Component[query_type] = "Rarity = 6";
                }

                //ATT指定
                if (!this.RadioButton1011.Checked)
                {
                    query_type++;

                    //各属性値の指定
                    if (this.RadioButton1012.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'斬'";
                    }
                    else if (this.RadioButton1013.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'打'";
                    }
                    else if (this.RadioButton1014.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'突'";
                    }
                    else if (this.RadioButton1015.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'魔'";
                    }
                }

                //Skill Type指定
                // checkboxlist用コード
                int itemCount = 0;
                int checkCount = 0;
                string queryCheckboxlist = "";

                foreach (ListItem item in CheckBoxList31.Items)
                {
                    if(item.Selected)
                    {
                        if(checkCount == 0)
                        {
                            //処理開始
                            queryCheckboxlist += "(";
                        }
                        else
                        {
                            queryCheckboxlist += " OR ";
                        }
                        queryCheckboxlist += "SType = N'" + item.Value +"'";
                        checkCount++;
                    }
                    if(itemCount == 5)
                    {
                        //最後にカッコのフタ
                        queryCheckboxlist += ")";
                        if(checkCount == 0)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。3人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li1 = new ListItem("スキルタイプのどれか一つにチェックを入れて下さい。", "0");
                            this.DropDownList9.Items.Add(li1);
                            return;
                        }
                        query_type++;
                        add_Query_Component[query_type] = queryCheckboxlist;
                    }
                    itemCount++;


                }

                ////Skill Type指定
                //if (!((CheckBox31.Checked == true) & (CheckBox32.Checked == true) & (CheckBox33.Checked == true) & (CheckBox34.Checked == true) & (CheckBox35.Checked == true) & (CheckBox36.Checked == true)))
                //{//全チェック入りならこの処理はスルー
                //    if (!((CheckBox31.Checked == false) & (CheckBox32.Checked == false) & (CheckBox33.Checked == false) & (CheckBox34.Checked == false) & (CheckBox35.Checked == false) & (CheckBox36.Checked == false)))
                //    {
                //        query_type++;
                //        int i = 0;//カウンタ


                //        if (CheckBox31.Checked == true)//全体
                //        {
                //            add_Query_Component[query_type] += "(SType = N'全体'";
                //            i++;
                //        }
                //        if (CheckBox32.Checked == true)//2体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'2体'";
                //            i++;
                //        }
                //        if (CheckBox33.Checked == true)//変則
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'変則'";
                //            i++;
                //        }
                //        if (CheckBox34.Checked == true)//吸収
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'吸収'";
                //            i++;
                //        }
                //        if (CheckBox35.Checked == true)//複数回
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'複数回'";
                //            i++;
                //        }
                //        if (CheckBox36.Checked == true)//単体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'単体'";
                //            i++;
                //        }
                //        //最後にカッコのフタ
                //        add_Query_Component[query_type] += ")";
                //    }
                //    else//スキルチェック全無しの場合
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。3人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //}
                //ドロップダウンリスト処理
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList23.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理2
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList28.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理4
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList33.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //アビクイック選択
                //1.65倍
                if (CheckBox131.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "(((A1Ex1 = N'スキル発動率上昇' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率上昇' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率上昇' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率上昇' AND A4V1 = 65)) OR ((A1Ex1 = N'スキル発動率1T目と3T目' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率1T目と3T目' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率1T目と3T目' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率1T目と3T目' AND A4V1 = 65)))";
                }
                //昇華
                if (CheckBox132.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "Id > 10000";
                }


                //属性付与
                // checkboxlist用コード
                int itemCountAddAtt = 0;
                int checkCountAddAtt = 0;
                int[] attInput = new int[4];

                foreach (ListItem item in CheckBoxList32.Items)
                {
                    if (item.Selected)
                    {

                        attInput[itemCountAddAtt] = 1;
                        checkCountAddAtt++;
                    }
                    if (itemCountAddAtt == 3)
                    {
                        
                        if (checkCountAddAtt == 4)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。3人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li2 = new ListItem("全属性付与出来る花騎士は存在しません。", "0");
                            this.DropDownList9.Items.Add(li2);
                            return;
                        }

                        if (checkCountAddAtt != 0)
                        {
                            query_type++;
                            add_Query_Component[query_type] = Add_ATT(attInput[0], attInput[1], attInput[2], attInput[3]);
                        }
                    }
                    itemCountAddAtt++;


                }
                ////属性付与
                //if ((CheckBox231.Checked == true) || (CheckBox232.Checked == true) || (CheckBox233.Checked == true) || (CheckBox234.Checked == true))
                //{
                //    //チェックボックスが全て埋まっている場合は抜ける
                //    if ((CheckBox231.Checked == true) && (CheckBox232.Checked == true) && (CheckBox233.Checked == true) && (CheckBox234.Checked == true))
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。3人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //    //少なくともどれか一つは属性にチェック入っているので処理開始
                //    query_type++;
                //    int[] att_input = new int[4];
                //    //斬
                //    if (CheckBox231.Checked == true)
                //    {
                //        att_input[0] = 1;
                //    }
                //    //打
                //    if (CheckBox232.Checked == true)
                //    {
                //        att_input[1] = 1;
                //    }
                //    //突
                //    if (CheckBox233.Checked == true)
                //    {
                //        att_input[2] = 1;
                //    }
                //    //魔
                //    if (CheckBox234.Checked == true)
                //    {
                //        att_input[3] = 1;
                //    }

                //    add_Query_Component[query_type] += Add_ATT(att_input[0], att_input[1], att_input[2], att_input[3]);
                //}

                //攻撃力低下
                if (CheckBox133.Checked == true)
                {
                    query_type++;
                    //対象三人以上の処理
                    if (CheckBox134.Checked == true)
                    {
                        add_Query_Component[query_type] += "("
                           + "(((A1NO > 2) AND (A1Ex1 = N'攻撃力低下')) OR ((A2NO > 2) AND (A2Ex1 = N'攻撃力低下')) OR ((A3NO > 2) AND (A3Ex1 = N'攻撃力低下')) OR ((A4NO > 2) AND (A4Ex1 = N'攻撃力低下'))) "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                    else
                    {
                        add_Query_Component[query_type] += "("
                           + "(A1Ex1 = N'攻撃力低下' OR A2Ex1 = N'攻撃力低下' OR A3Ex1 = N'攻撃力低下' OR A4Ex1 = N'攻撃力低下') "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                }
            }

            string add_querry = "";

            //add_query作りこみ
            for (int i = 1; i <= query_type; i++)
            {
                if (i == 1)
                {
                    add_querry = " WHERE " + add_Query_Component[1];
                }
                else
                {
                    add_querry += " AND " + add_Query_Component[i];
                }
            }

            //string list_querry = "SELECT Id, Name FROM [dbo].[Fkgmbr]" + add_querry;
            string list_querry = "SELECT Id, Name,A1Ex1,A1V1,A1V2,A2Ex1,A2V1,A2V2,A3Ex1,A3V1,A3V2,A4Ex1,A4V1,A4V2 FROM [dbo].[Fkgmbr]" + add_querry;


            //GetDataによりdataset入手する

            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);

            if (ds_fkg.Tables[0].Rows.Count == 0)
            {
                //メッセージボックス表示
                //string script = "<script language=javascript>" + "window.alert('検索結果が見つかりません。3人目の花騎士は読込不可能です。')" + "</script>";
                //Response.Write(script);
                ListItem li3 = new ListItem("見つかりません。", "0");
                this.DropDownList9.Items.Add(li3);
                return;
            }

            //検索アビリティ1に選択があるなら、アビリティ値でのソートの準備処理を行う
            //関数に、datatableを送り、更に検索アビリティ1の値を送る
            DataTable dt_out;
            int abi1Flag = 0;
            //検索アビリティ1の値の取得
            {
                //リストから値を得て関数に値を投入
                string abi_Select = DropDownList23.Text;
                if (abi_Select != "未選択")
                {
                    //DataTable加工関数呼び出し
                    ds_fkg.Tables[0].Columns.Add("SortValue", typeof(int));
                    dt_out = AddSortValue(ds_fkg.Tables[0], abi_Select, ref abi1Flag);
                }
                else
                {
                    dt_out = ds_fkg.Tables[0];
                }
            }

            //レコードのソートを行う
            DataView view = new DataView(dt_out);
            string Name = "Name";

            //abi1が選択されていて、フラグがonの場合のみ、アビ1の値によるソート
            if (abi1Flag == 1)
            {
                view.Sort = "SortValue DESC";
                Name = "Name and Value";
            }
            else
            {
                //通常時
                view.Sort = "Name";
            }


            //所持キャラをクッキーから読み込んで、マーカーつける処理
            DataTable dt_out1 = MarkFromCookie(view.ToTable());

            //datasetの値をDropDownList9に入力する
            this.DropDownList9.DataSource = dt_out1;
            this.DropDownList9.DataTextField = Name;
            this.DropDownList9.DataValueField = "Id";
            this.DropDownList9.DataBind();

        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            //リスト生成
            //リストクリア
            DropDownList12.Items.Clear();
            DropDownList12.ClearSelection();

            //クエリを生成する
            //DropDownListの値を参照して生成する
            //ドロップダウンリストの指定により分類
            //0 指定なし Add query不要
            //1以上 ANDにて条件追加

            int query_type = 0;
            string[] add_Query_Component = new string[10];

            string rarityQuerry = "";
            string attQuerry = "";

            //ドロップダウンリスト使う場合
            //useDropdown = 1;//0:使わない 1:使う

            
            {
                //Rarity指定
                if (!this.RadioButton213.Checked)
                {
                    query_type++;
                    //radioButton106.checked　☆6の場合のみ
                    add_Query_Component[query_type] = "Rarity = 6";
                }

                //ATT指定
                if (!this.RadioButton1016.Checked)
                {
                    query_type++;

                    //各属性値の指定
                    if (this.RadioButton1017.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'斬'";
                    }
                    else if (this.RadioButton1018.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'打'";
                    }
                    else if (this.RadioButton1019.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'突'";
                    }
                    else if (this.RadioButton1020.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'魔'";
                    }
                }

                //Skill Type指定
                // checkboxlist用コード
                int itemCount = 0;
                int checkCount = 0;
                string queryCheckboxlist = "";

                foreach (ListItem item in CheckBoxList41.Items)
                {
                    if (item.Selected)
                    {
                        if (checkCount == 0)
                        {
                            //処理開始
                            queryCheckboxlist += "(";
                        }
                        else
                        {
                            queryCheckboxlist += " OR ";
                        }
                        queryCheckboxlist += "SType = N'" + item.Value + "'";
                        checkCount++;
                    }
                    if (itemCount == 5)
                    {
                        //最後にカッコのフタ
                        queryCheckboxlist += ")";
                        if (checkCount == 0)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。4人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li1 = new ListItem("スキルタイプのどれか一つにチェックを入れて下さい。", "0");
                            this.DropDownList12.Items.Add(li1);
                            return;
                        }
                        query_type++;
                        add_Query_Component[query_type] = queryCheckboxlist;
                    }
                    itemCount++;


                }

                ////Skill Type指定
                //if (!((CheckBox41.Checked == true) & (CheckBox42.Checked == true) & (CheckBox43.Checked == true) & (CheckBox44.Checked == true) & (CheckBox45.Checked == true) & (CheckBox46.Checked == true)))
                //{//全チェック入りならこの処理はスルー
                //    if (!((CheckBox41.Checked == false) & (CheckBox42.Checked == false) & (CheckBox43.Checked == false) & (CheckBox44.Checked == false) & (CheckBox45.Checked == false) & (CheckBox46.Checked == false)))
                //    {
                //        query_type++;
                //        int i = 0;//カウンタ


                //        if (CheckBox41.Checked == true)//全体
                //        {
                //            add_Query_Component[query_type] += "(SType = N'全体'";
                //            i++;
                //        }
                //        if (CheckBox42.Checked == true)//2体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'2体'";
                //            i++;
                //        }
                //        if (CheckBox43.Checked == true)//変則
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'変則'";
                //            i++;
                //        }
                //        if (CheckBox44.Checked == true)//吸収
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'吸収'";
                //            i++;
                //        }
                //        if (CheckBox45.Checked == true)//複数回
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'複数回'";
                //            i++;
                //        }
                //        if (CheckBox46.Checked == true)//単体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'単体'";
                //            i++;
                //        }
                //        //最後にカッコのフタ
                //        add_Query_Component[query_type] += ")";
                //    }
                //    else//スキルチェック全無しの場合
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。4人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //}

                //ドロップダウンリスト処理
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList24.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理2
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList29.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理4
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList34.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //アビクイック選択
                //1.65倍
                if (CheckBox141.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "(((A1Ex1 = N'スキル発動率上昇' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率上昇' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率上昇' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率上昇' AND A4V1 = 65)) OR ((A1Ex1 = N'スキル発動率1T目と3T目' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率1T目と3T目' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率1T目と3T目' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率1T目と3T目' AND A4V1 = 65)))";
                }
                //昇華
                if (CheckBox142.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "Id > 10000";
                }

                //属性付与
                // checkboxlist用コード
                int itemCountAddAtt = 0;
                int checkCountAddAtt = 0;
                int[] attInput = new int[4];

                foreach (ListItem item in CheckBoxList42.Items)
                {
                    if (item.Selected)
                    {

                        attInput[itemCountAddAtt] = 1;
                        checkCountAddAtt++;
                    }
                    if (itemCountAddAtt == 3)
                    {

                        if (checkCountAddAtt == 4)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。4人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li2 = new ListItem("全属性付与出来る花騎士は存在しません。", "0");
                            this.DropDownList12.Items.Add(li2);
                            return;
                        }

                        if (checkCountAddAtt != 0)
                        {
                            query_type++;
                            add_Query_Component[query_type] = Add_ATT(attInput[0], attInput[1], attInput[2], attInput[3]);
                        }
                    }
                    itemCountAddAtt++;


                }

                ////属性付与
                //if ((CheckBox241.Checked == true) || (CheckBox242.Checked == true) || (CheckBox243.Checked == true) || (CheckBox244.Checked == true))
                //{
                //    //チェックボックスが全て埋まっている場合は抜ける
                //    if ((CheckBox241.Checked == true) && (CheckBox242.Checked == true) && (CheckBox243.Checked == true) && (CheckBox244.Checked == true))
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。4人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //    //少なくともどれか一つは属性にチェック入っているので処理開始
                //    query_type++;
                //    int[] att_input = new int[4];
                //    //斬
                //    if (CheckBox241.Checked == true)
                //    {
                //        att_input[0] = 1;
                //    }
                //    //打
                //    if (CheckBox242.Checked == true)
                //    {
                //        att_input[1] = 1;
                //    }
                //    //突
                //    if (CheckBox243.Checked == true)
                //    {
                //        att_input[2] = 1;
                //    }
                //    //魔
                //    if (CheckBox244.Checked == true)
                //    {
                //        att_input[3] = 1;
                //    }

                //    add_Query_Component[query_type] += Add_ATT(att_input[0], att_input[1], att_input[2], att_input[3]);
                //}
                //攻撃力低下
                if (CheckBox143.Checked == true)
                {
                    query_type++;
                    //対象三人以上の処理
                    if (CheckBox144.Checked == true)
                    {
                        add_Query_Component[query_type] += "("
                           + "(((A1NO > 2) AND (A1Ex1 = N'攻撃力低下')) OR ((A2NO > 2) AND (A2Ex1 = N'攻撃力低下')) OR ((A3NO > 2) AND (A3Ex1 = N'攻撃力低下')) OR ((A4NO > 2) AND (A4Ex1 = N'攻撃力低下'))) "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                    else
                    {
                        add_Query_Component[query_type] += "("
                           + "(A1Ex1 = N'攻撃力低下' OR A2Ex1 = N'攻撃力低下' OR A3Ex1 = N'攻撃力低下' OR A4Ex1 = N'攻撃力低下') "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                }
            }

            string add_querry = "";

            //add_query作りこみ
            for (int i = 1; i <= query_type; i++)
            {
                if (i == 1)
                {
                    add_querry = " WHERE " + add_Query_Component[1];
                }
                else
                {
                    add_querry += " AND " + add_Query_Component[i];
                }
            }

            //string list_querry = "SELECT Id, Name FROM [dbo].[Fkgmbr]" + add_querry;
            string list_querry = "SELECT Id, Name,A1Ex1,A1V1,A1V2,A2Ex1,A2V1,A2V2,A3Ex1,A3V1,A3V2,A4Ex1,A4V1,A4V2 FROM [dbo].[Fkgmbr]" + add_querry;


            //GetDataによりdataset入手する

            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);

            if (ds_fkg.Tables[0].Rows.Count == 0)
            {
                //メッセージボックス表示
                //string script = "<script language=javascript>" + "window.alert('検索結果が見つかりません。4人目の花騎士は読込不可能です。')" + "</script>";
                //Response.Write(script);
                ListItem li3 = new ListItem("見つかりません。", "0");
                this.DropDownList12.Items.Add(li3);
                return;
            }

            //検索アビリティ1に選択があるなら、アビリティ値でのソートの準備処理を行う
            //関数に、datatableを送り、更に検索アビリティ1の値を送る
            DataTable dt_out;
            int abi1Flag = 0;
            //検索アビリティ1の値の取得
            {
                //リストから値を得て関数に値を投入
                string abi_Select = DropDownList24.Text;
                if (abi_Select != "未選択")
                {
                    //DataTable加工関数呼び出し
                    ds_fkg.Tables[0].Columns.Add("SortValue", typeof(int));
                    dt_out = AddSortValue(ds_fkg.Tables[0], abi_Select, ref abi1Flag);
                }
                else
                {
                    dt_out = ds_fkg.Tables[0];
                }
            }

            //レコードのソートを行う
            DataView view = new DataView(dt_out);
            string Name = "Name";

            //abi1が選択されていて、フラグがonの場合のみ、アビ1の値によるソート
            if (abi1Flag == 1)
            {
                view.Sort = "SortValue DESC";
                Name = "Name and Value";
            }
            else
            {
                //通常時
                view.Sort = "Name";
            }


            //所持キャラをクッキーから読み込んで、マーカーつける処理
            DataTable dt_out1 = MarkFromCookie(view.ToTable());

            //datasetの値をDropDownList12に入力する
            this.DropDownList12.DataSource = dt_out1;
            this.DropDownList12.DataTextField = Name;
            this.DropDownList12.DataValueField = "Id";
            this.DropDownList12.DataBind();

        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            //リスト生成
            //リストクリア
            DropDownList15.Items.Clear();
            DropDownList15.ClearSelection();

            //クエリを生成する
            //DropDownListの値を参照して生成する
            //ドロップダウンリストの指定により分類
            //0 指定なし Add query不要
            //1以上 ANDにて条件追加

            int query_type = 0;
            string[] add_Query_Component = new string[10];

            string rarityQuerry = "";
            string attQuerry = "";

            //ドロップダウンリスト使う場合
            //useDropdown = 1;//0:使わない 1:使う

            
            {
                //Rarity指定
                if (!this.RadioButton217.Checked)
                {
                    query_type++;
                    //radioButton106.checked　☆6の場合のみ
                    add_Query_Component[query_type] = "Rarity = 6";
                }

                //ATT指定
                if (!this.RadioButton1021.Checked)
                {
                    query_type++;

                    //各属性値の指定
                    if (this.RadioButton1022.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'斬'";
                    }
                    else if (this.RadioButton1023.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'打'";
                    }
                    else if (this.RadioButton1024.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'突'";
                    }
                    else if (this.RadioButton1025.Checked)
                    {
                        add_Query_Component[query_type] = "ATT = N'魔'";
                    }
                }

                //Skill Type指定
                // checkboxlist用コード
                int itemCount = 0;
                int checkCount = 0;
                string queryCheckboxlist = "";

                foreach (ListItem item in CheckBoxList51.Items)
                {
                    if (item.Selected)
                    {
                        if (checkCount == 0)
                        {
                            //処理開始
                            queryCheckboxlist += "(";
                        }
                        else
                        {
                            queryCheckboxlist += " OR ";
                        }
                        queryCheckboxlist += "SType = N'" + item.Value + "'";
                        checkCount++;
                    }
                    if (itemCount == 5)
                    {
                        //最後にカッコのフタ
                        queryCheckboxlist += ")";
                        if (checkCount == 0)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。5人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li1 = new ListItem("スキルタイプのどれか一つにチェックを入れて下さい。", "0");
                            this.DropDownList15.Items.Add(li1);
                            return;
                        }
                        query_type++;
                        add_Query_Component[query_type] = queryCheckboxlist;
                    }
                    itemCount++;


                }

                ////Skill Type指定
                //if (!((CheckBox51.Checked == true) & (CheckBox52.Checked == true) & (CheckBox53.Checked == true) & (CheckBox54.Checked == true) & (CheckBox55.Checked == true) & (CheckBox56.Checked == true)))
                //{//全チェック入りならこの処理はスルー
                //    if (!((CheckBox51.Checked == false) & (CheckBox52.Checked == false) & (CheckBox53.Checked == false) & (CheckBox54.Checked == false) & (CheckBox55.Checked == false) & (CheckBox56.Checked == false)))
                //    {
                //        query_type++;
                //        int i = 0;//カウンタ


                //        if (CheckBox51.Checked == true)//全体
                //        {
                //            add_Query_Component[query_type] += "(SType = N'全体'";
                //            i++;
                //        }
                //        if (CheckBox52.Checked == true)//2体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'2体'";
                //            i++;
                //        }
                //        if (CheckBox53.Checked == true)//変則
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'変則'";
                //            i++;
                //        }
                //        if (CheckBox54.Checked == true)//吸収
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'吸収'";
                //            i++;
                //        }
                //        if (CheckBox55.Checked == true)//複数回
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'複数回'";
                //            i++;
                //        }
                //        if (CheckBox56.Checked == true)//単体
                //        {
                //            if (i > 0)
                //            {
                //                add_Query_Component[query_type] += " OR ";
                //            }
                //            else
                //            {
                //                add_Query_Component[query_type] += "(";
                //            }
                //            add_Query_Component[query_type] += "SType = N'単体'";
                //            i++;
                //        }
                //        //最後にカッコのフタ
                //        add_Query_Component[query_type] += ")";
                //    }
                //    else//スキルチェック全無しの場合
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('スキルタイプのどれか一つにチェックを入れて下さい。5人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //}
                //ドロップダウンリスト処理
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList25.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理2
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList30.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //ドロップダウンリスト処理4
                {
                    //リストから値を得て関数に値を投入
                    string abi_Select = DropDownList35.Text;
                    if (abi_Select != "未選択")
                    {
                        string abi_Result = Abi_Select(abi_Select);
                        //空白で返ってきた場合は処理を行わない
                        if (abi_Result != "")
                        {
                            query_type++;
                            add_Query_Component[query_type] += abi_Result;
                        }
                    }
                }
                //アビクイック選択
                //1.65倍
                if (CheckBox151.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "(((A1Ex1 = N'スキル発動率上昇' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率上昇' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率上昇' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率上昇' AND A4V1 = 65)) OR ((A1Ex1 = N'スキル発動率1T目と3T目' AND A1V1 = 65) OR (A2Ex1 = N'スキル発動率1T目と3T目' AND A2V1 = 65) OR (A3Ex1 = N'スキル発動率1T目と3T目' AND A3V1 = 65) OR (A4Ex1 = N'スキル発動率1T目と3T目' AND A4V1 = 65)))";
                }
                //昇華
                if (CheckBox152.Checked == true)
                {
                    query_type++;
                    add_Query_Component[query_type] += "Id > 10000";
                }

                //属性付与
                // checkboxlist用コード
                int itemCountAddAtt = 0;
                int checkCountAddAtt = 0;
                int[] attInput = new int[4];

                foreach (ListItem item in CheckBoxList52.Items)
                {
                    if (item.Selected)
                    {

                        attInput[itemCountAddAtt] = 1;
                        checkCountAddAtt++;
                    }
                    if (itemCountAddAtt == 3)
                    {

                        if (checkCountAddAtt == 4)
                        {
                            //メッセージボックス表示
                            //string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。5人目の花騎士は読込不可能です。')" + "</script>";
                            //Response.Write(script);
                            ListItem li2 = new ListItem("全属性付与出来る花騎士は存在しません。", "0");
                            this.DropDownList15.Items.Add(li2);
                            return;
                        }

                        if (checkCountAddAtt != 0)
                        {
                            query_type++;
                            add_Query_Component[query_type] = Add_ATT(attInput[0], attInput[1], attInput[2], attInput[3]);
                        }
                    }
                    itemCountAddAtt++;


                }

                ////属性付与
                //if ((CheckBox251.Checked == true) || (CheckBox252.Checked == true) || (CheckBox253.Checked == true) || (CheckBox254.Checked == true))
                //{
                //    //チェックボックスが全て埋まっている場合は抜ける
                //    if ((CheckBox251.Checked == true) && (CheckBox252.Checked == true) && (CheckBox253.Checked == true) && (CheckBox254.Checked == true))
                //    {
                //        //メッセージボックス表示
                //        string script = "<script language=javascript>" + "window.alert('全ての属性を付与出来る花騎士は存在しません。5人目の花騎士は読込不可能です。')" + "</script>";
                //        Response.Write(script);
                //        return;
                //    }
                //    //少なくともどれか一つは属性にチェック入っているので処理開始
                //    query_type++;
                //    int[] att_input = new int[4];
                //    //斬
                //    if (CheckBox251.Checked == true)
                //    {
                //        att_input[0] = 1;
                //    }
                //    //打
                //    if (CheckBox252.Checked == true)
                //    {
                //        att_input[1] = 1;
                //    }
                //    //突
                //    if (CheckBox253.Checked == true)
                //    {
                //        att_input[2] = 1;
                //    }
                //    //魔
                //    if (CheckBox254.Checked == true)
                //    {
                //        att_input[3] = 1;
                //    }

                //    add_Query_Component[query_type] += Add_ATT(att_input[0], att_input[1], att_input[2], att_input[3]);
                //}

                //攻撃力低下
                if (CheckBox153.Checked == true)
                {
                    query_type++;
                    //対象三人以上の処理
                    if (CheckBox154.Checked == true)
                    {
                        add_Query_Component[query_type] += "("
                           + "(((A1NO > 2) AND (A1Ex1 = N'攻撃力低下')) OR ((A2NO > 2) AND (A2Ex1 = N'攻撃力低下')) OR ((A3NO > 2) AND (A3Ex1 = N'攻撃力低下')) OR ((A4NO > 2) AND (A4Ex1 = N'攻撃力低下'))) "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                    else
                    {
                        add_Query_Component[query_type] += "("
                           + "(A1Ex1 = N'攻撃力低下' OR A2Ex1 = N'攻撃力低下' OR A3Ex1 = N'攻撃力低下' OR A4Ex1 = N'攻撃力低下') "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                    }
                }

            }
            string add_querry = "";

            //add_query作りこみ
            for (int i = 1; i <= query_type; i++)
            {
                if (i == 1)
                {
                    add_querry = " WHERE " + add_Query_Component[1];
                }
                else
                {
                    add_querry += " AND " + add_Query_Component[i];
                }
            }
            //string list_querry = "SELECT Id, Name FROM [dbo].[Fkgmbr]" + add_querry;
            string list_querry = "SELECT Id, Name,A1Ex1,A1V1,A1V2,A2Ex1,A2V1,A2V2,A3Ex1,A3V1,A3V2,A4Ex1,A4V1,A4V2 FROM [dbo].[Fkgmbr]" + add_querry;


            //GetDataによりdataset入手する

            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(list_querry);

            if (ds_fkg.Tables[0].Rows.Count == 0)
            {
                //メッセージボックス表示
                //string script = "<script language=javascript>" + "window.alert('検索結果が見つかりません。5人目の花騎士は読込不可能です。')" + "</script>";
                //Response.Write(script);
                ListItem li3 = new ListItem("見つかりません。", "0");
                this.DropDownList15.Items.Add(li3);
                return;
            }

            //検索アビリティ1に選択があるなら、アビリティ値でのソートの準備処理を行う
            //関数に、datatableを送り、更に検索アビリティ1の値を送る
            DataTable dt_out;
            int abi1Flag = 0;
            //検索アビリティ1の値の取得
            {
                //リストから値を得て関数に値を投入
                string abi_Select = DropDownList25.Text;
                if (abi_Select != "未選択")
                {
                    //DataTable加工関数呼び出し
                    ds_fkg.Tables[0].Columns.Add("SortValue", typeof(int));
                    dt_out = AddSortValue(ds_fkg.Tables[0], abi_Select, ref abi1Flag);
                }
                else
                {
                    dt_out = ds_fkg.Tables[0];
                }
            }

            //レコードのソートを行う
            DataView view = new DataView(dt_out);
            string Name = "Name";

            //abi1が選択されていて、フラグがonの場合のみ、アビ1の値によるソート
            if (abi1Flag == 1)
            {
                view.Sort = "SortValue DESC";
                Name = "Name and Value";
            }
            else
            {
                //通常時
                view.Sort = "Name";
            }


            //所持キャラをクッキーから読み込んで、マーカーつける処理
            DataTable dt_out1 = MarkFromCookie(view.ToTable());

            //datasetの値をDropDownList15に入力する
            this.DropDownList15.DataSource = dt_out1;
            this.DropDownList15.DataTextField = Name;
            this.DropDownList15.DataValueField = "Id";
            this.DropDownList15.DataBind();


        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            //何もしない、単なるトリガー
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            //編成シミュ無効化
            //CalcPT();
        }



        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        //
        //
        // オリジナル関数（自動生成されていない関数群）
        //
        //
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////



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
        //属性付与判定に必要なクエリを返す関数
        //
        protected string Add_ATT(int Zan, int Da, int Totsu, int Ma)
        {
            string abi_Return = "";
            //何属性が指定されているか、属性値を足して確認
            //1の場合
            // 4属性に対して、それぞれ考えられる属性を、全14個の登録項目の中から選ぶ
            //2の場合  10項目の中から選ぶ
            //3の場合　4項目の中から選ぶ

            //int add_Att_num = Zan + Da + Totsu + Ma;
            string[] att = new string[4];
            int count = 0;

            //配列に属性文字
            if (Zan == 1)
            {
                count++;
                att[count] = "斬";

            }
            if (Da == 1)
            {
                count++;
                att[count] = "打";
            }
            if (Totsu == 1)
            {
                count++;
                att[count] = "突";
            }
            if (Ma == 1)
            {
                count++;
                att[count] = "魔";
            }

            //フタする
            abi_Return = "(";

            //countの値によって分類
            if (count == 1)
            {
                //斬
                if ("斬".Contains(att[1]))
                {
                    abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬'))";
                }
                //打
                if ("打".Contains(att[1]))
                {
                    abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'打') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'打') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'打') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'打'))";
                }
                //突
                if ("突".Contains(att[1]))
                {
                    abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'突') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'突') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'突') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'突'))";
                }
                //魔
                if ("魔".Contains(att[1]))
                {
                    abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'魔'))";
                }
            }

            //2属性
            if ((count == 2) || (count == 1))
            {
                if (count == 1)
                {
                    abi_Return += " OR ";
                }

                int hitCount = 0;

                int attNo = 0;

                //斬打
                for (int i = 1; i <= count; i++)
                {
                    if ("斬打".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬打') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬打') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬打') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬打'))";
                        }
                    }
                }
                attNo = 0;
                //斬突
                for (int i = 1; i <= count; i++)
                {
                    if ("斬突".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬突') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬突') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬突') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬突'))";
                        }
                    }
                }
                attNo = 0;
                //斬魔
                for (int i = 1; i <= count; i++)
                {
                    if ("斬魔".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬魔'))";
                        }
                    }
                }
                attNo = 0;
                //打突
                for (int i = 1; i <= count; i++)
                {
                    if ("打突".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'打突') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'打突') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'打突') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'打突'))";
                        }
                    }
                }
                attNo = 0;
                //打魔
                for (int i = 1; i <= count; i++)
                {
                    if ("打魔".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'打魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'打魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'打魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'打魔'))";
                        }
                    }
                }
                attNo = 0;
                //突魔
                for (int i = 1; i <= count; i++)
                {
                    if ("突魔".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'突魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'突魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'突魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'突魔'))";
                        }
                    }
                }
            }

            //3属性
            if ((count == 3) || (count == 2) || (count == 1))
            {
                if ((count == 1) || (count == 2))
                {
                    abi_Return += " OR ";
                }

                int hitCount = 0;

                int attNo = 0;
                //斬打突
                for (int i = 1; i <= count; i++)
                {
                    if ("斬打突".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        //count=3なら3属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)) || ((count == 3) && (attNo == 3)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬打突') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬打突') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬打突') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬打突'))";
                        }
                    }
                }
                attNo = 0;
                //斬打魔
                for (int i = 1; i <= count; i++)
                {
                    if ("斬打魔".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        //count=3なら3属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)) || ((count == 3) && (attNo == 3)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬打魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬打魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬打魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬打魔'))";
                        }
                    }
                }
                attNo = 0;
                //斬突魔
                for (int i = 1; i <= count; i++)
                {
                    if ("斬突魔".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        //count=3なら3属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)) || ((count == 3) && (attNo == 3)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'斬突魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'斬突魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'斬突魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'斬突魔'))";
                        }
                    }
                }
                attNo = 0;
                //打突魔
                for (int i = 1; i <= count; i++)
                {
                    if ("打突魔".Contains(att[i]))
                    {
                        attNo++;
                        //count=1ならフリーパス、count=2なら、2属性目が一致して初めて中に入れる
                        //count=3なら3属性目が一致して初めて中に入れる
                        if ((count == 1) || ((count == 2) && (attNo == 2)) || ((count == 3) && (attNo == 3)))
                        {
                            if (hitCount != 0)
                            {
                                abi_Return += "OR";
                            }
                            hitCount++;
                            abi_Return += "((A1Ex1 = N'属性付与' AND A1Ex2 = N'打突魔') OR (A2Ex1 = N'属性付与' AND A2Ex2 = N'打突魔') OR (A3Ex1 = N'属性付与' AND A3Ex2 = N'打突魔') OR (A4Ex1 = N'属性付与' AND A4Ex2 = N'打突魔'))";
                        }
                    }
                }
            }
            //フタする
            abi_Return += ")";

            return abi_Return;
        }

        //
        //選択されたアビに応じて、クエリーストリングを返す関数
        //

        protected string Abi_Select(string abi_Input)
        {
            string abi_Return = "";

            switch (abi_Input)
            {
                case "1ターン目系"://"攻撃力上昇し、1T目のスキル発動率上昇"
                    {
                        abi_Return = "(" +
                            "(A1st1 = 1 OR A2st1 = 1 OR A3st1 = 1 OR A4st1 = 1) "//
                        + "OR ((A1Ex1 = N'攻撃力上昇1T目さらに上昇') OR (A2Ex1 = N'攻撃力上昇1T目さらに上昇') OR (A3Ex1 = N'攻撃力上昇1T目さらに上昇') OR (A4Ex1 = N'攻撃力上昇1T目さらに上昇'))"
                        + "OR ((A1Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇') OR (A2Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇') OR (A3Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇') OR (A4Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇'))"
                              + ")";
                        return abi_Return;
                    }
                case "スキル発動率1.2倍":
                    {
                        abi_Return = "("
                             + "((A1Ex1 = N'スキル発動率上昇' AND A1V1 = 20) OR (A2Ex1 = N'スキル発動率上昇' AND A2V1 = 20) OR (A3Ex1 = N'スキル発動率上昇' AND A3V1 = 20) OR (A4Ex1 = N'スキル発動率上昇' AND A4V1 = 20)) "
                             + "OR ((A1Ex1 = N'攻撃力上昇し、スキル発動率上昇' AND A1V2 = 20) OR (A2Ex1 = N'攻撃力上昇し、スキル発動率上昇' AND A2V2 = 20) OR (A3Ex1 = N'攻撃力上昇し、スキル発動率上昇' AND A3V2 = 20) OR (A4Ex1 = N'攻撃力上昇し、スキル発動率上昇' AND A4V2 = 20))"
                             + "OR ((A1Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' AND A1V1 = 20) OR (A2Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' AND A2V1 = 20) OR (A3Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' AND A3V1 = 20) OR (A4Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' AND A4V1 = 20))"
                             + "OR ((A1Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A1V1 = 20) OR (A2Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A2V1 = 20) OR (A3Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A3V1 = 20) OR (A4Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A4V1 = 20))"
                              + ")";
                        return abi_Return;
                    }
                case "スキルLVによりスキル発動率上昇":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'スキルLVでスキル発動率上昇' OR A2Ex1 = N'スキルLVでスキル発動率上昇' OR A3Ex1 = N'スキルLVでスキル発動率上昇' OR A4Ex1 = N'スキルLVでスキル発動率上昇') "
                            + "OR (A1Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇' OR A2Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇' OR A3Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇' OR A4Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇') "
                            + ")";
                        return abi_Return;
                    }
                case "スキル発動率上昇":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'スキル発動率上昇' OR A2Ex1 = N'スキル発動率上昇' OR A3Ex1 = N'スキル発動率上昇' OR A4Ex1 = N'スキル発動率上昇') "
                            + "OR (A1Ex1 = N'スキルLVでスキル発動率上昇' OR A2Ex1 = N'スキルLVでスキル発動率上昇' OR A3Ex1 = N'スキルLVでスキル発動率上昇' OR A4Ex1 = N'スキルLVでスキル発動率上昇') "
                            + "OR (A1Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' OR A2Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' OR A3Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' OR A4Ex1 = N'スキル発動率上昇し、対ボスダメ上昇') "
                            + "OR ((A1Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A1V1 = 20) OR (A2Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A2V1 = 20) OR (A3Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A3V1 = 20) OR (A4Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' AND A4V1 = 20))"
                            + "OR (A1Ex1 = N'スキル発動率1T目と3T目' OR A2Ex1 = N'スキル発動率1T目と3T目' OR A3Ex1 = N'スキル発動率1T目と3T目' OR A4Ex1 = N'スキル発動率1T目と3T目') "
                            + "OR (A1Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇' OR A2Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇' OR A3Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇' OR A4Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇') "
                            + "OR ((A1Ex1 = N'攻撃力上昇し、スキル発動率上昇') OR (A2Ex1 = N'攻撃力上昇し、スキル発動率上昇') OR (A3Ex1 = N'攻撃力上昇し、スキル発動率上昇') OR (A4Ex1 = N'攻撃力上昇し、スキル発動率上昇'))"
                            + "OR ((A1Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇') OR (A2Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇') OR (A3Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇') OR (A4Ex1 = N'攻撃力上昇し、スキルLVでスキル発動率上昇'))"
                            + "OR ((A1Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇') OR (A2Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇') OR (A3Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇') OR (A4Ex1 = N'攻撃力上昇し、1T目のスキル発動率上昇'))"
                             + ")";
                        return abi_Return;

                    }
                case "クリ率上昇":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'クリ率上昇' OR A2Ex1 = N'クリ率上昇' OR A3Ex1 = N'クリ率上昇' OR A4Ex1 = N'クリ率上昇') "
                            + "OR (A1Ex1 = N'クリ率クリダメ上昇' OR A2Ex1 = N'クリ率クリダメ上昇' OR A3Ex1 = N'クリ率クリダメ上昇' OR A4Ex1 = N'クリ率クリダメ上昇')"
                            + ")";
                        return abi_Return;
                    }
                case "クリダメ上昇":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'クリダメ上昇' OR A2Ex1 = N'クリダメ上昇' OR A3Ex1 = N'クリダメ上昇' OR A4Ex1 = N'クリダメ上昇') "
                            + "OR (A1Ex1 = N'クリ率クリダメ上昇' OR A2Ex1 = N'クリ率クリダメ上昇' OR A3Ex1 = N'クリ率クリダメ上昇' OR A4Ex1 = N'クリ率クリダメ上昇')"
                            + "OR (A1Ex1 = N'クリダメ上昇し自身がさらに上昇' OR A2Ex1 = N'クリダメ上昇し自身がさらに上昇' OR A3Ex1 = N'クリダメ上昇し自身がさらに上昇' OR A4Ex1 = N'クリダメ上昇し自身がさらに上昇')"
                            + ")";
                        return abi_Return;
                    }
                case "クリ率上昇（PT全体対象）":
                    {
                        abi_Return = "("
                            + "((A1Ex1 = N'クリ率上昇'  AND A1NO = 5) OR (A2Ex1 = N'クリ率上昇'  AND A2NO = 5) OR (A3Ex1 = N'クリ率上昇'  AND A3NO = 5) OR (A4Ex1 = N'クリ率上昇'  AND A4NO = 5)) "
                            + "OR ((A1Ex1 = N'クリ率クリダメ上昇'  AND A1NO = 5) OR (A2Ex1 = N'クリ率クリダメ上昇'  AND A2NO = 5) OR (A3Ex1 = N'クリ率クリダメ上昇'  AND A3NO = 5) OR (A4Ex1 = N'クリ率クリダメ上昇'  AND A4NO = 5))"
                            + ")";
                        return abi_Return;
                    }
                case "クリダメ上昇（PT全体対象）":
                    {
                        abi_Return = "("
                            + "((A1Ex1 = N'クリダメ上昇'  AND A1NO = 5) OR (A2Ex1 = N'クリダメ上昇'  AND A2NO = 5) OR (A3Ex1 = N'クリダメ上昇'  AND A3NO = 5) OR (A4Ex1 = N'クリダメ上昇'  AND A4NO = 5)) "
                            + "OR ((A1Ex1 = N'クリ率クリダメ上昇'  AND A1NO = 5) OR (A2Ex1 = N'クリ率クリダメ上昇'  AND A2NO = 5) OR (A3Ex1 = N'クリ率クリダメ上昇'  AND A3NO = 5) OR (A4Ex1 = N'クリ率クリダメ上昇'  AND A4NO = 5))"
                            + "OR ((A1Ex1 = N'クリダメ上昇し自身がさらに上昇'  AND A1NO = 5) OR (A2Ex1 = N'クリダメ上昇し自身がさらに上昇'  AND A2NO = 5) OR (A3Ex1 = N'クリダメ上昇し自身がさらに上昇'  AND A3NO = 5) OR (A4Ex1 = N'クリダメ上昇し自身がさらに上昇'  AND A4NO = 5))"
                            + ")";
                        return abi_Return;
                    }
                case "スキルダメージ増加":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'スキルダメ上昇' OR A2Ex1 = N'スキルダメ上昇' OR A3Ex1 = N'スキルダメ上昇' OR A4Ex1 = N'スキルダメ上昇') "
                            + "OR (A1Ex1 = N'攻撃力上昇し、スキルダメージ上昇' OR A2Ex1 = N'攻撃力上昇し、スキルダメージ上昇' OR A3Ex1 = N'攻撃力上昇し、スキルダメージ上昇' OR A4Ex1 = N'攻撃力上昇し、スキルダメージ上昇')"
                            + "OR (A1Ex1 = N'PTと自身スキルダメ上昇' OR A2Ex1 = N'PTと自身スキルダメ上昇' OR A3Ex1 = N'PTと自身スキルダメ上昇' OR A4Ex1 = N'PTと自身スキルダメ上昇')"
                            + "OR (A1Ex1 = N'スキルダメ上昇し、対ボスダメ上昇' OR A2Ex1 = N'スキルダメ上昇し、対ボスダメ上昇' OR A3Ex1 = N'スキルダメ上昇し、対ボスダメ上昇' OR A4Ex1 = N'スキルダメ上昇し、対ボスダメ上昇')"
                            + "OR (A1Ex1 = N'スキルダメ上昇し、シャイクリ泥率上昇' OR A2Ex1 = N'スキルダメ上昇し、シャイクリ泥率上昇' OR A3Ex1 = N'スキルダメ上昇し、シャイクリ泥率上昇' OR A4Ex1 = N'スキルダメ上昇し、シャイクリ泥率上昇')"
                            + "OR (A1Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' OR A2Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' OR A3Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇' OR A4Ex1 = N'スキル発動率上昇し、自身のスキルダメ上昇')"
                            + ")";
                        return abi_Return;
                    }
                case "ダメージ増加":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'ダメージ上昇' OR A2Ex1 = N'ダメージ上昇' OR A3Ex1 = N'ダメージ上昇' OR A4Ex1 = N'ダメージ上昇') "
                            + "OR (A1Ex1 = N'ターン毎ダメージ上昇' OR A2Ex1 = N'ターン毎ダメージ上昇' OR A3Ex1 = N'ターン毎ダメージ上昇' OR A4Ex1 = N'ターン毎ダメージ上昇')"
                            + "OR (A1Ex1 = N'攻撃力上昇し、ターン毎にダメージ上昇' OR A2Ex1 = N'攻撃力上昇し、ターン毎にダメージ上昇' OR A3Ex1 = N'攻撃力上昇し、ターン毎にダメージ上昇' OR A4Ex1 = N'攻撃力上昇し、ターン毎にダメージ上昇')"
                            + "OR (A1Ex1 = N'ソラ発動毎にダメ上昇' OR A2Ex1 = N'ソラ発動毎にダメ上昇' OR A3Ex1 = N'ソラ発動毎にダメ上昇' OR A4Ex1 = N'ソラ発動毎にダメ上昇')"
                            + "OR (A1Ex1 = N'HP割合ダメ上昇率' OR A2Ex1 = N'HP割合ダメ上昇率' OR A3Ex1 = N'HP割合ダメ上昇率' OR A4Ex1 = N'HP割合ダメ上昇率')"
                            + "OR (A1Ex1 = N'攻撃力上昇HP割合ダメ上昇' OR A2Ex1 = N'攻撃力上昇HP割合ダメ上昇' OR A3Ex1 = N'攻撃力上昇HP割合ダメ上昇' OR A4Ex1 = N'攻撃力上昇HP割合ダメ上昇')"
                            + "OR (A1Ex1 = N'PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇' OR A2Ex1 = N'PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇' OR A3Ex1 = N'PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇' OR A4Ex1 = N'PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇')"
                            + ")";
                        return abi_Return;
                    }
                case "ボスに与えるダメージ増加":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'対ボスダメ上昇' OR A2Ex1 = N'対ボスダメ上昇' OR A3Ex1 = N'対ボスダメ上昇' OR A4Ex1 = N'対ボスダメ上昇') "
                            + "OR (A1Ex1 = N'攻撃力上昇し、対ボスダメ上昇' OR A2Ex1 = N'攻撃力上昇し、対ボスダメ上昇' OR A3Ex1 = N'攻撃力上昇し、対ボスダメ上昇' OR A4Ex1 = N'攻撃力上昇し、対ボスダメ上昇')"
                            + "OR (A1Ex1 = N'対ボス攻撃力ダメ上昇' OR A2Ex1 = N'対ボス攻撃力ダメ上昇' OR A3Ex1 = N'対ボス攻撃力ダメ上昇' OR A4Ex1 = N'対ボス攻撃力ダメ上昇')"
                            + "OR (A1Ex1 = N'スキルダメ上昇し、対ボスダメ上昇' OR A2Ex1 = N'スキルダメ上昇し、対ボスダメ上昇' OR A3Ex1 = N'スキルダメ上昇し、対ボスダメ上昇' OR A4Ex1 = N'スキルダメ上昇し、対ボスダメ上昇')"
                            + "OR (A1Ex1 = N'対ボスダメ上昇自身のボスダメ上昇' OR A2Ex1 = N'対ボスダメ上昇自身のボスダメ上昇' OR A3Ex1 = N'対ボスダメ上昇自身のボスダメ上昇' OR A4Ex1 = N'対ボスダメ上昇自身のボスダメ上昇')"
                            + "OR (A1Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' OR A2Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' OR A3Ex1 = N'スキル発動率上昇し、対ボスダメ上昇' OR A4Ex1 = N'スキル発動率上昇し、対ボスダメ上昇')"
                            + ")";
                        return abi_Return;
                    }
                case "回避":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'回避' OR A2Ex1 = N'回避' OR A3Ex1 = N'回避' OR A4Ex1 = N'回避') "
                            + "OR (A1Ex1 = N'攻撃力上昇し、回避' OR A2Ex1 = N'攻撃力上昇し、回避' OR A3Ex1 = N'攻撃力上昇し、回避' OR A4Ex1 = N'攻撃力上昇し、回避')"
                            + ")";
                        return abi_Return;
                    }
                case "反撃":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'反撃' OR A2Ex1 = N'反撃' OR A3Ex1 = N'反撃' OR A4Ex1 = N'反撃')"
                            + ")";
                        return abi_Return;
                    }
                case "防御":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'防御ダメ軽減率上昇' OR A2Ex1 = N'防御ダメ軽減率上昇' OR A3Ex1 = N'防御ダメ軽減率上昇' OR A4Ex1 = N'防御ダメ軽減率上昇')"
                            + ")";
                        return abi_Return;
                    }
                case "追撃":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'追撃' OR A2Ex1 = N'追撃' OR A3Ex1 = N'追撃' OR A4Ex1 = N'追撃')"
                            + ")";
                        return abi_Return;
                    }
                case "再行動":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'再行動' OR A2Ex1 = N'再行動' OR A3Ex1 = N'再行動' OR A4Ex1 = N'再行動') "
                            + "OR (A1Ex1 = N'攻撃力上昇し、再行動' OR A2Ex1 = N'攻撃力上昇し、再行動' OR A3Ex1 = N'攻撃力上昇し、再行動' OR A4Ex1 = N'攻撃力上昇し、再行動')"
                            + "OR (A1Ex1 = N'ソラ効果上昇し自身が再行動' OR A2Ex1 = N'ソラ効果上昇し自身が再行動' OR A3Ex1 = N'ソラ効果上昇し自身が再行動' OR A4Ex1 = N'ソラ効果上昇し自身が再行動')"
                            + "OR (A1Ex1 = N'光ゲージ充填し、自身が再行動' OR A2Ex1 = N'光ゲージ充填し、自身が再行動' OR A3Ex1 = N'光ゲージ充填し、自身が再行動' OR A4Ex1 = N'光ゲージ充填し、自身が再行動')"
                            + "OR (A1Ex1 = N'PTに再行動付与' OR A2Ex1 = N'PTに再行動付与' OR A3Ex1 = N'PTに再行動付与' OR A4Ex1 = N'PTに再行動付与')"
                            + "OR (A1Ex1 = N'PT移動力増加し、自身が再行動' OR A2Ex1 = N'PT移動力増加し、自身が再行動' OR A3Ex1 = N'PT移動力増加し、自身が再行動' OR A4Ex1 = N'PT移動力増加し、自身が再行動')"
                            + ")";
                        return abi_Return;
                    }
                case "攻撃力低下"://攻撃力上昇し、敵全体の攻撃力低下
                    {
                        abi_Return = "("
                           + "(A1Ex1 = N'攻撃力低下' OR A2Ex1 = N'攻撃力低下' OR A3Ex1 = N'攻撃力低下' OR A4Ex1 = N'攻撃力低下') "
                           + "OR (A1Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A2Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A3Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下' OR A4Ex1 = N'攻撃力上昇し、敵全体の攻撃力低下') "
                           + ")";
                        return abi_Return;
                    }
                case "バリア":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'ダメ無効' OR A2Ex1 = N'ダメ無効' OR A3Ex1 = N'ダメ無効' OR A4Ex1 = N'ダメ無効')"
                            + ")";
                        return abi_Return;
                    }
                case "ソーラードライブ効果上昇":
                    {
                        abi_Return = "("
                          + "(A1Ex1 = N'ソラ効果上昇' OR A2Ex1 = N'ソラ効果上昇' OR A3Ex1 = N'ソラ効果上昇' OR A4Ex1 = N'ソラ効果上昇') "
                          + "OR (A1Ex1 = N'ソラ効果シャイクリ泥率上昇' OR A2Ex1 = N'ソラ効果シャイクリ泥率上昇' OR A3Ex1 = N'ソラ効果シャイクリ泥率上昇' OR A4Ex1 = N'ソラ効果シャイクリ泥率上昇') "
                          + "OR (A1Ex1 = N'ソラ効果光ゲージ充填上昇' OR A2Ex1 = N'ソラ効果光ゲージ充填上昇' OR A3Ex1 = N'ソラ効果光ゲージ充填上昇' OR A4Ex1 = N'ソラ効果光ゲージ充填上昇') "
                          + "OR (A1Ex1 = N'ソラ効果上昇し自身が再行動' OR A2Ex1 = N'ソラ効果上昇し自身が再行動' OR A3Ex1 = N'ソラ効果上昇し自身が再行動' OR A4Ex1 = N'ソラ効果上昇し自身が再行動')"
                            + "OR (A1Ex1 = N'攻撃力上昇し、ソラ効果上昇' OR A2Ex1 = N'攻撃力上昇し、ソラ効果上昇' OR A3Ex1 = N'攻撃力上昇し、ソラ効果上昇' OR A4Ex1 = N'攻撃力上昇し、ソラ効果上昇') "
                          + ")";
                        return abi_Return;
                    }
                case "光ゲージ充填":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'光ゲージ充填' OR A2Ex1 = N'光ゲージ充填' OR A3Ex1 = N'光ゲージ充填' OR A4Ex1 = N'光ゲージ充填') "
                            + "OR (A1Ex1 = N'ソラ効果光ゲージ充填上昇' OR A2Ex1 = N'ソラ効果光ゲージ充填上昇' OR A3Ex1 = N'ソラ効果光ゲージ充填上昇' OR A4Ex1 = N'ソラ効果光ゲージ充填上昇') "
                          + "OR (A1Ex1 = N'攻撃力上昇し、光ゲージ充填' OR A2Ex1 = N'攻撃力上昇し、光ゲージ充填' OR A3Ex1 = N'攻撃力上昇し、光ゲージ充填' OR A4Ex1 = N'攻撃力上昇し、光ゲージ充填') "
                          + "OR (A1Ex1 = N'光ゲージ充填し、自身が再行動' OR A2Ex1 = N'光ゲージ充填し、自身が再行動' OR A3Ex1 = N'光ゲージ充填し、自身が再行動' OR A4Ex1 = N'光ゲージ充填し、自身が再行動') "
                          + ")";
                        return abi_Return;
                    }
                case "自身が攻撃を受けた次ターン系":
                    {
                        abi_Return = "("
                            + "(A1Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇' OR A2Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇' OR A3Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇' OR A4Ex1 = N'自身が攻撃を受けた次Tにスキル発動率上昇') "
                            + "OR (A1Ex1 = N'自身が攻撃を受けた次Tに攻撃力上昇' OR A2Ex1 = N'自身が攻撃を受けた次Tに攻撃力上昇' OR A3Ex1 = N'自身が攻撃を受けた次Tに攻撃力上昇' OR A4Ex1 = N'自身が攻撃を受けた次Tに攻撃力上昇') "
                          + "OR (A1Ex1 = N'自身が攻撃を受けた次Tにダメ上昇' OR A2Ex1 = N'自身が攻撃を受けた次Tにダメ上昇' OR A3Ex1 = N'自身が攻撃を受けた次Tにダメ上昇' OR A4Ex1 = N'自身が攻撃を受けた次Tにダメ上昇') "
                          + ")";
                        return abi_Return;
                    }


                //何も見つからなかったときは空白を返す
                default:
                    return abi_Return;

            }

        }

        //持ってるキャラにマーキングする処理
        public DataTable MarkFromCookie(DataTable dt_in)
        {
            //クッキーが存在しなければ、何も処理しない
            if (HttpContext.Current.Request.Cookies["FkgName"] == null)
            {
                return dt_in;
            }

            //クッキー登録データ取得
            FKG_register fkg_register = new FKG_register();
            string[,] cookieFkg = fkg_register.DecodeFkgName();

            for(int i = 0; i < dt_in.Rows.Count; i++)
            {
                for(int j = 0; j < cookieFkg.GetLength(0); j ++)
                {
                    if(dt_in.Rows[i]["id"].ToString() == cookieFkg[j,0])
                    {
                        dt_in.Rows[i]["Name"] = "<span class=\"fkg-value\">" + dt_in.Rows[i]["Name"].ToString() + "</span>";

                        try
                        {
                            if (dt_in.Rows[i]["Name and Value"].ToString() != "")
                            {
                                dt_in.Rows[i]["Name and Value"] = "#" + dt_in.Rows[i]["Name and Value"].ToString();
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }

            }

            return dt_in;
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////
        //
        //
        //計算開始ボタンに関する関数群
        //
        //
        ////////////////////////////////////////////////////////////////////////////////////////////////

        protected int Getmbrid(int ptid)
        {
            //一つも選択されていない場合
            if (((DropDownList3.SelectedItem == null)||(DropDownList3.SelectedValue == "0")) & ((DropDownList6.SelectedItem == null) || (DropDownList6.SelectedValue == "0")) & ((DropDownList9.SelectedItem == null) || (DropDownList9.SelectedValue == "0")) & ((DropDownList12.SelectedItem == null) || (DropDownList12.SelectedValue == "0")) & ((DropDownList15.SelectedItem == null) || (DropDownList15.SelectedValue == "0")))
            {
                return 3;
            }

            string mbrName0 = "";

            switch (ptid)
            {
                case 1:
                    //セレクトされている場合のみ、IDを格納する。セレクトされていない場合は0を格納する
                    if ((DropDownList3.SelectedValue != "")&(DropDownList3.SelectedValue != "0"))
                    {
                        mbrid[0] = Convert.ToInt32(DropDownList3.SelectedValue);
                        mbrName0 = DropDownList3.SelectedItem.ToString();
                        if(mbrName0.IndexOf("+") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("+") - 1);
                        }
                        else if (mbrName0.IndexOf("x") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("x") - 1);
                        }
                        mbrName[0] = mbrName0;
                    }
                    else
                    {
                        mbrid[0] = 0;
                        mbrName[0] = "未選択";
                    }

                    if ((DropDownList6.SelectedValue != "")&(DropDownList6.SelectedValue != "0"))
                    {
                        mbrid[1] = Convert.ToInt32(DropDownList6.SelectedValue);
                        mbrName0 = DropDownList6.SelectedItem.ToString();
                        if (mbrName0.IndexOf("+") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("+") - 1);
                        }
                        else if (mbrName0.IndexOf("x") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("x") - 1);
                        }
                        mbrName[1] = mbrName0;
                    }
                    else
                    {
                        mbrid[1] = 0;
                        mbrName[1] = "未選択";
                    }

                    if ((DropDownList9.SelectedValue != "")&(DropDownList9.SelectedValue != "0"))
                    {
                        mbrid[2] = Convert.ToInt32(DropDownList9.SelectedValue);
                        mbrName0 = DropDownList9.SelectedItem.ToString();
                        if (mbrName0.IndexOf("+") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("+") - 1);
                        }
                        else if (mbrName0.IndexOf("x") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("x") - 1);
                        }
                        mbrName[2] = mbrName0;
                    }
                    else
                    {
                        mbrid[2] = 0;
                        mbrName[2] = "未選択";
                    }

                    if ((DropDownList12.SelectedValue != "") & (DropDownList12.SelectedValue != "0"))
                    {
                        mbrid[3] = Convert.ToInt32(DropDownList12.SelectedValue);
                        mbrName0 = DropDownList12.SelectedItem.ToString();
                        if (mbrName0.IndexOf("+") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("+") - 1);
                        }
                        else if (mbrName0.IndexOf("x") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("x") - 1);
                        }
                        mbrName[3] = mbrName0;
                    }
                    else
                    {
                        mbrid[3] = 0;
                        mbrName[3] = "未選択";
                    }

                    if ((DropDownList15.SelectedValue != "") & (DropDownList15.SelectedValue != "0"))
                    {
                        mbrid[4] = Convert.ToInt32(DropDownList15.SelectedValue);
                        mbrName0 = DropDownList15.SelectedItem.ToString();
                        if (mbrName0.IndexOf("+") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("+") - 1);
                        }
                        else if (mbrName0.IndexOf("x") != -1)
                        {
                            mbrName0 = mbrName0.Substring(0, mbrName0.IndexOf("x") - 1);
                        }
                        mbrName[4] = mbrName0;
                    }
                    else
                    {
                        mbrid[4] = 0;
                        mbrName[4] = "未選択";
                    }
                    //スキルレベルをGETする
                    skilLv[0] = Convert.ToInt32(DropDownList16.SelectedValue);
                    skilLv[1] = Convert.ToInt32(DropDownList17.SelectedValue);
                    skilLv[2] = Convert.ToInt32(DropDownList18.SelectedValue);
                    skilLv[3] = Convert.ToInt32(DropDownList19.SelectedValue);
                    skilLv[4] = Convert.ToInt32(DropDownList20.SelectedValue);


                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            //セレクトされているidが重複する場合はメソッドは0を返す
            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    if ((mbrid[i] == mbrid[j]) & (mbrid[i] != 0))
                    {
                        return 2;//同ID選択時
                    }

                }
            }


            return 1;//正常な戻り値
        }

        //
        // メンバーIDに基づいてそのPTの各能力値を計算する
        //
        protected void CalcPT()
        {
            //mbridに基づき、花騎士の各ステータス値取得のためのクエリ作成
            //（各ＰＴメンバーごとにクエリ作成したほうが、ＤＢアクセスコスト軽減できると思われる。ID=0のメンバはクエリ作成なし）
            //Getdataにクエリ送信しデータセット取得
            //取得したデータセットに基づき、各能力値を計算
            //計算結果配列に値を格納する

            string get_query = "";

            //計算用変数定義
            int[,] calcRes = new int[5, 28];
            /* 内訳
            0:スキル発動率（通常時）
            1:1T目
            2:2T目
            3:3T目
            4:スキルダメージ
            5:クリ発生率
            6:1T目
            7:クリダメ上昇率
            8:1T目
            9:攻撃力上昇率
            10:1T目
            11:2T目
            12:3T目
            13:ダメージ上昇率
            14:対ボス攻撃力上昇率
            15:対ボスダメ上昇率      
            16:所持スキルの基本発動率
            17:1.2倍持ち
            18:1.65倍持ち
            19:属性付与持ち
            20:回避持ち
            21:バリア持ち
            22:防御
            23:反撃
            24:再行動 1T目
            25:再行動 通常
            26:ダメージ上昇率ターン毎（２T目から加算される
            27:追撃　（1:通常の２０％単体追撃 2:10％全体追撃 3:通常、全体追撃持ち-デュランタのみ）
             */
            int[] ptcalcRes = new int[36];
            /* 内訳
            0:スキル発動率（通常時）
            1:1T目
            2:2T目
            3:3T目
            4:スキルダメージ
            5:クリ発生率
            6:1T目
            7:クリダメ上昇率
            8:1T目
            9:攻撃力上昇率
            10:1T目 //マンリョウ
            11:2T目 //スノドロ、シャクヤクハロ　ターン毎30%でMAX60%
            12:3T目 //ヤドリギ　ターン毎20%でMAX40%
            13:ダメージ上昇率
            14:対ボス攻撃力上昇
            15:対ボスダメ上昇率
            20:ソーラー効果上昇率（使わない）
            21:シャイクリ泥率
            22:移動力
            23:攻撃力低下率
            24:スキル発動低下率
            25:スタート時光充填率
            26:攻撃ミス
            27:1.2倍持ち人数
            28:1.65倍持ち人数
            29:回避持ち
            30:バリア持ち
            31:防御
            32:反撃
            33:再行動
            34:ダメージ上昇率ターン毎（２T目から加算される　3回加算
            35:ダメージ上昇率ターン毎（２T目から加算される　2回加算、現状アジサイのみ

             */

            //その他必要と思われる変数宣言
            //int[,] skilLev = new int[5,2];//スキルレベル 5人分、発動確率MINとMAXで5,2
            int enemyNum = 3;//敵の数
            double solarRatio = 1;
            //移動力計算関係
            int[] mov = new int[5];
            int mbrNum = 0;
            int movAdd = 0;

            //敵の数を取得
            switch (RadioButtonList11.SelectedItem.Value)
            {
                case "3":
                    {
                        enemyNum = 3;
                        break;
                    }
                case "2":
                    {
                        enemyNum = 2;
                        break;
                    }
                case "1":
                    {
                        enemyNum = 1;
                        break;
                    }
            }

            //ランタナアビにより、スキル発動率補正
            switch (RadioButtonList12.SelectedItem.Value)
            {
                case "30":
                    {
                        ptcalcRes[0] += 30;
                        break;
                    }
                case "20":
                    {
                        ptcalcRes[0] += 20;
                        break;
                    }
                case "10":
                    {
                        ptcalcRes[0] += 10;
                        break;
                    }
                case "0":
                    break;
            }
            //MBR IDの入手
            //第1PTに対して入手
            switch (Getmbrid(1))
            {
                case 1://OK
                    break;
                case 2://同じ花騎士を選択
                       //メッセージボックス表示
                    string script = "<script language=javascript>" + "window.alert('同じ花騎士は選択出来ません。処理を終了します')" + "</script>";
                    Response.Write(script);
                    return;                
                case 3://花騎士を選択していない
                       //メッセージボックス表示
                    string script1 = "<script language=javascript>" + "window.alert('花騎士を選択してください。処理を終了します')" + "</script>";
                    Response.Write(script1);
                    return;

            }

            //PT全員分の情報を入手
            get_query = "SELECT Id,Name,MOV,ATT,STP,STPMax,SType,SRatio,ANum,A1st1,A1NO,A1Ex1,A1V1,A1V2,A1Ex2,A2st1,A2NO,A2Ex1,A2V1,A2V2,A2Ex2,A3st1,A3NO,A3Ex1,A3V1,A3V2,A3Ex2,A4st1,A4NO,A4Ex1,A4V1,A4V2,A4Ex2 " +
            //"FROM [dbo].[Fkgmbr] WHERE Id =" + mbrid[0] + ", Id =" + mbrid[1] + ", Id =" + mbrid[2] + ", Id =" + mbrid[3] + ", Id =" + mbrid[4];
            "FROM [dbo].[Fkgmbr] WHERE Id IN(" + mbrid[0] + ", " + mbrid[1] + ", " + mbrid[2] + ", " + mbrid[3] + ", " + mbrid[4] + ")";
            DataSet ds_fkg = new DataSet();
            ds_fkg = GetData(get_query);
            DataTable dt_fkg = new DataTable();



            //クエリにより取得したデータ順が、mbrid内のID順と異なるため、修正
            //Mbrid内の人数算出
            for (int i = 0; i < 5; i++)
            {
                if (mbrid[i] != 0)
                {
                    mbrNum++;
                }
            }
            //データセットにMbridカラムの追加
            ds_fkg.Tables[0].Columns.Add("Mbrid", typeof(Int32));

            for (int i = 0; i < mbrNum; i++)
            {
                for (int j = 0; j < mbrNum; j++)
                {
                    if (Convert.ToInt32(ds_fkg.Tables[0].Rows[j]["Id"]) == mbrid[i])
                    {
                        ds_fkg.Tables[0].Rows[j]["Mbrid"] = i + 1;
                    }
                }

            }

            

            //レコードのソートを行う
            DataView view = new DataView(ds_fkg.Tables[0]);
            view.Sort = "Mbrid";
            dt_fkg = view.ToTable();

            //属性処理
            //
            //1列目　属性 (0:斬 1:打 2:突 3:魔)
            //2列目　キャラクター順番
            int[,] AttState = new int[4, 5];

            //各属性へのアビによる付与可能数
            int[] attAddNum = new int[4];

            //花騎士の各属性について取得
            int dataRow = 0;
            for (int i = 0; i < 5; i++)
            {
                if (mbrid[i] != 0)
                {
                    switch (dt_fkg.Rows[dataRow]["ATT"].ToString())
                    {
                        case "斬":
                            {
                                AttState[0, i] = 1;
                                break;
                            }

                        case "打":
                            {
                                AttState[1, i] = 1;
                                break;

                            }
                        case "突":
                            {
                                AttState[2, i] = 1;
                                break;

                            }
                        case "魔":
                            {
                                AttState[3, i] = 1;
                                break;
                            }
                    }
                    dataRow++;
                }
            }

            //ヒツジグサ用　属性カウント処理
            // Att[0] = 斬
            // Att[1] = 打
            // Att[2] = 突
            // Att[3] = 魔

            int[] Att = new int[4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < mbrNum; j++)
                {
                    Att[i] += AttState[i, j];
                }
            }

            //結果表示用のみ
            //キャラのスキルタイプ、スキル倍率取得
            string[,] SkillInfo = new string[5,2];
            int SkillCount = 0;
            for (int i = 0; i < 5; i++)
            {
                if (mbrid[i] != 0)
                {
                    SkillInfo[i, 0] = dt_fkg.Rows[SkillCount]["SType"].ToString();//スキルタイプ
                    SkillInfo[i, 1] = dt_fkg.Rows[SkillCount++]["SRatio"].ToString();//スキル倍率
                }
            }





            //////////////////////////////////////////////////////////////////////
            //PT内の各キャラクタ毎にループしてデータ取得、計算
            //////////////////////////////////////////////////////////////////////
            int k = 0;
            for (int i = 0; i <= 4; i++)
            {
                //データが入っていない場合は
                if (mbrid[i] == 0)
                {
                    continue;
                }

                //移動力計算サブ処理
                mov[i] = Convert.ToInt32(dt_fkg.Rows[k]["MOV"]);
                //mbrNum += 1;//PT人数カウント


                //
                //所持スキルの基本発動率計算処理
                //

                int diff = Convert.ToInt32(dt_fkg.Rows[k]["STPMax"]) - Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                switch (diff)
                {
                    case 10://スキルLV5とLV1の発動率差が10の時
                        {
                            switch (skilLv[i])
                            {
                                case 5:
                                    {
                                        calcRes[i, 16] = 10 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 4:
                                    {
                                        calcRes[i, 16] = 7 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 3:
                                    {
                                        calcRes[i, 16] = 5 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 2:
                                    {
                                        calcRes[i, 16] = 2 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 1:
                                    {
                                        calcRes[i, 16] = Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                            }

                            break;
                        }
                    case 8:
                        {
                            calcRes[i, 16] = 2 * (skilLv[i] - 1) + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                            break;

                        }

                    case 5://アルストロメリア☆6
                        {
                            switch (skilLv[i])
                            {
                                case 5:
                                    {
                                        calcRes[i, 16] = 5 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 4:
                                    {
                                        calcRes[i, 16] = 3 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 3:
                                    {
                                        calcRes[i, 16] = 2 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 2:
                                    {
                                        calcRes[i, 16] = 1 + Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                                case 1:
                                    {
                                        calcRes[i, 16] = Convert.ToInt32(dt_fkg.Rows[k]["STP"]);
                                        break;
                                    }
                            }
                            break;
                        }


                }
                k++;





                //アビリティ1～4をストリング配列にいれる
                DataRow[] dtrow = dt_fkg.Select("Id = " + mbrid[i]);

                //各アビリティ名格納
                string[] abi = { "", dtrow[0]["A1Ex1"].ToString(), dtrow[0]["A2Ex1"].ToString(), dtrow[0]["A3Ex1"].ToString(), dtrow[0]["A4Ex1"].ToString() };


                ////////////////////////////////////////////////
                //各アビリティ毎にループ
                ////////////////////////////////////////////////
                for (int j = 1; j <= 4; j++)
                {
                    //アビリティ数がオーバーする場合はループ抜け、次の花騎士へ
                    if (Convert.ToInt32(dtrow[0]["ANum"]) < j)
                    {
                        break;
                    }

                    //dtrowより各値を取得し、個人用とPT用に格納する
                    //abi_offsetを定義
                    int abi_offset = (j - 1) * 6 + 9;
                    //abi_offset        A1st:発動形態　0:常時、1:1T、3:3T、4:その他
                    //abi_offset + 1    A1NO：対象人数　5人か、1人か、3人かで分ける。二人もあるけどね
                    //abi_offset + 2    A1Ex1
                    //abi_offset + 3    A1V1
                    //abi_offset + 4    A1V2
                    //abi_offset + 5    A1Ex2

                    switch (abi[j])
                    {
                        case "攻撃力上昇":
                        case "攻撃力上昇し、回避"://回避についてはコーディングしない
                        case "MAP画面アビと、攻撃力上昇":
                            
                            if (abi[j]== "攻撃力上昇し、回避")
                            {
                                //回避フラグ立て
                                calcRes[i, 20] = 1;
                                ptcalcRes[29]++;
                            }
                            switch (dtrow[0][abi_offset])
                            {
                                case 0://常時
                                       //対象人数で場合分け
                                    switch (dtrow[0][abi_offset + 1])
                                    {
                                        case 5://全員
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                break;
                                            }
                                        case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                break;

                                            }
                                        case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                break;

                                            }
                                        case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                break;

                                            }
                                        case 1://1人
                                            {
                                                calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                break;
                                            }
                                    }
                                    break;
                                case 1://1T目のみ
                                    {
                                        //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }




                                        break;
                                    }

                                //以下必要性不明のためコーディングせず
                                case 3://3T目のみ ☆5で必要

                                    break;
                                case 4://その他？なんだろうこれ？
                                    break;
                            }




                            break;

                        case "敵の数で攻撃力上昇":
                            {
                                //常時発動のみでコーディング
                                //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    case 5:
                                        {
                                            if (Convert.ToInt32(dtrow[0][abi_offset + 4]) == -1)
                                            {//敵の数が減るほど攻撃力増加 ウキツリボク（競技会）
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * (4 - enemyNum);
                                            }
                                            else
                                            {//敵の数が増えると攻撃力増加
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * enemyNum;
                                            }
                                            break;
                                        }
                                }

                                break;
                            }

                        case "ターンで攻撃力上昇"://シャクヤクハロ、スノドロ
                                         //上限ＭＡＸは、アビ2個目の値で指定するようにする。上記4人とも記入ずみ 2018/3/5
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        {
                                            int addMax = Convert.ToInt32(dtrow[0][abi_offset + 4]);


                                            //2T目から加算
                                            //基本的に2T目、3T目の攻撃力に加算するだけ。パフィオの3回目の加算はとりあえず見送り
                                            //ターン毎加算の仕様不明だが、同一PTに複数いた場合、それぞれが加算すると仮定して計算
                                            ptcalcRes[11] += Convert.ToInt32(dtrow[0][abi_offset + 3]);

                                            //3T目加算
                                            ptcalcRes[12] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            //4T目　必要？とりあえずコーディングしない
                                            //パフィオのみ。その他をはじくif分追加する
                                            /*
                                            4T目配列 += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3;
                                            */
                                            break;
                                        }
                                }
                                break;
                            }

                        case "攻撃力上昇ターンでさらに上昇"://アデニウム昇華
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);

                                            //2T目から加算
                                            //基本的に2T目、3T目の攻撃力に加算するだけ。パフィオの3回目の加算はとりあえず見送り
                                            //ターン毎加算の仕様不明だが、同一PTに複数いた場合、それぞれが加算すると仮定して計算
                                            ptcalcRes[11] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                            //3T目加算
                                            ptcalcRes[12] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                            //4T目　必要？とりあえずコーディングしない
                                            //パフィオのみ。その他をはじくif分追加する
                                            /*
                                            4T目配列 += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3;
                                            */


                                            break;
                                        }
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 1:
                                        {
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);

                                            //2T目から加算
                                            //基本的に2T目、3T目の攻撃力に加算するだけ。パフィオの3回目の加算はとりあえず見送り
                                            //ターン毎加算の仕様不明だが、同一PTに複数いた場合、それぞれが加算すると仮定して計算
                                            ptcalcRes[11] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                            //3T目加算
                                            ptcalcRes[12] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                            //4T目　必要？とりあえずコーディングしない
                                            //パフィオのみ。その他をはじくif分追加する
                                            /*
                                            4T目配列 += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3;
                                            */


                                            break;
                                        }
                                }
                                break;
                            }

                        case "攻撃力上昇1T目さらに上昇"://マンリョウ
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                        break;
                                }
                                break;
                            }

                        case "攻撃力上昇HP割合ダメ上昇"://合体アビ。シャボンソウのみ、ヤマブキは単体アビだから違う
                                             //HP割合の値は1/2の2とか、1/3の3が入力される
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                        calcRes[i, 13] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                        break;
                                }
                                break;
                            }

                        case "攻撃力上昇し自身がさらに上昇"://アルストロメリア昇華
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                        break;

                                                    }

                                            }
                                            //自分の分にさらに加算
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                            break;
                                        }
                                    case 1://1T目
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                        break;

                                                    }

                                            }
                                            //自分の分にさらに加算
                                            calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                            break;
                                        }
                                        break;
                                }



                                break;
                            }

                        case "攻撃力上昇し、自信を含む2人がさらに上昇":
                            //対象人数で場合分け
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    case 5://全員
                                        {
                                            ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            break;
                                        }
                                    case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                            break;

                                        }
                                    case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                            break;

                                        }
                                    case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                            break;

                                        }

                                }
                                //自分の分にさらに加算
                                calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) - (Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4);
                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4;
                                break;
                            }

                        case "攻撃力上昇し、自信を含む3人がさらに上昇":
                            //対象人数で場合分け
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    case 5://全員
                                        {
                                            ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            break;
                                        }
                                    case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                            break;

                                        }
                                    case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                            break;

                                        }
                                    case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                            calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                            break;

                                        }

                                }
                                //自分の分にさらに加算
                                calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) - (Convert.ToInt32(dtrow[0][abi_offset + 4]) / 2);
                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) / 2;
                                break;
                            }

                        case "攻撃力上昇し、敵の数でさらに上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    
                                                    if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                    {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                    }
                                                    else
                                                    {//敵の数が増えると攻撃力増加
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                    }
                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                    {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                    }
                                                    else
                                                    {//敵の数が増えると攻撃力増加
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                    }
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                    {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                    }
                                                    else
                                                    {//敵の数が増えると攻撃力増加
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                    }
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                    {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                    }
                                                    else
                                                    {//敵の数が増えると攻撃力増加
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                    }
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                    {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                    }
                                                    else
                                                    {//敵の数が増えると攻撃力増加
                                                        ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                        {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                        }
                                                        else
                                                        {//敵の数が増えると攻撃力増加
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                        }
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                        if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                        {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                        }
                                                        else
                                                        {//敵の数が増えると攻撃力増加
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                        }
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                        if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                        {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                        }
                                                        else
                                                        {//敵の数が増えると攻撃力増加
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                        }
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                        if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                        {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                        }
                                                        else
                                                        {//敵の数が増えると攻撃力増加
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                        }
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        if (dtrow[0][abi_offset + 5].ToString() == "敵の数減少")
                                                        {//敵の数が減るほど攻撃力増加 ハナオクラ　昇華
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * (4 - enemyNum);
                                                        }
                                                        else
                                                        {//敵の数が増えると攻撃力増加
                                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * enemyNum;
                                                        }
                                                        break;
                                                    }
                                            }




                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }

                                break;
                            }

                        case "攻撃力上昇し、PTメンバー数でさらに上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * mbrNum;

                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * mbrNum;


                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "攻撃力上昇し、スキル発動率上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    //1.2倍持ちの人数をカウント
                                                    if (Convert.ToInt32(dtrow[0][abi_offset + 4]) == 20)
                                                    {
                                                        ptcalcRes[27]++;
                                                        calcRes[i, 17] = 1;//trueの意。outputで〇に変換する
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        //1.65倍持ちの人数をカウント
                                                        if (Convert.ToInt32(dtrow[0][abi_offset + 4]) == 65)
                                                        {
                                                            ptcalcRes[28]++;
                                                            calcRes[i, 18] = 1;//trueの意。outputで〇に変換する
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;


                            }

                        case "攻撃力上昇し、スキルLVでスキル発動率上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 4]) + (skilLv[i] - 1) * 2;

                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 4]) + (skilLv[i] - 1) * 2;
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;


                            }

                        case "攻撃力上昇し、1T目のスキル発動率上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    //1.2倍持ちの人数をカウント
                                                    if (Convert.ToInt32(dtrow[0][abi_offset + 4]) == 20)
                                                    {
                                                        ptcalcRes[27]++;
                                                        calcRes[i, 17] = 1;//trueの意。outputで〇に変換する
                                                    }
                                                    //1.65倍持ちの人数をカウント
                                                    else if (Convert.ToInt32(dtrow[0][abi_offset + 4]) == 65)
                                                    {
                                                        ptcalcRes[28]++;
                                                        calcRes[i, 18] = 1;//trueの意。outputで〇に変換する
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    //case 1://1T目のみ
                                    //    {
                                    //        //対象人数で場合分け
                                    //        switch (dtrow[0][abi_offset + 1])
                                    //        {
                                    //            case 5://全員
                                    //                {
                                    //                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                    //                    ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                    //                    //1.65倍持ちの人数をカウント
                                    //                    if (Convert.ToInt32(dtrow[0][abi_offset + 4]) == 65)
                                    //                    {
                                    //                        ptcalcRes[28]++;
                                    //                        calcRes[i, 18] = 1;//trueの意。outputで〇に変換する
                                    //                    }
                                    //                    break;
                                    //                }
                                    //        }
                                    //        break;
                                    //    }
                                    //以下必要性不明のためコーディングせず
                                    //case 3://3T目のみ ☆5で必要

                                    //    break;
                                    //case 4://その他？なんだろうこれ？
                                    //    break;
                                }
                                break;
                            }

                        case "攻撃力上昇し、スキルダメージ上昇"://スキルダメージはPT全員に効果
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);

                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                //スキルダメージ付与
                                ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }

                        case "攻撃力上昇し、ターン毎にダメージ上昇"://スキルダメージはPT全員に効果
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);

                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                //ターン毎ダメージ付与（2回だけ付与）
                                ptcalcRes[35] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }


                        case "攻撃力上昇し、対ボス攻撃力上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            case 1://自身のみ
                                                {
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 14] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;

                            }

                        case "攻撃力上昇し、対ボスダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            case 1://自身のみ
                                                {
                                                    calcRes[i, 9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;

                            }

                        case "攻撃力上昇し、敵全体の攻撃力低下":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[23] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[23] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;


                            }

                        case "攻撃力上昇し、敵3体が攻撃ミス":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[26] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[26] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;


                            }




                        case "攻撃力上昇し、再行動":
                            

                            switch (dtrow[0][abi_offset])
                            {
                                case 0://常時
                                       //対象人数で場合分け
                                    switch (dtrow[0][abi_offset + 1])
                                    {
                                        case 5://全員
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                //再行動フラグ立て
                                                calcRes[i, 25] = Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                break;
                                            }
                                    }
                                    break;
                                case 1://1T目のみ
                                    {
                                        //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    //再行動フラグ立て
                                                    calcRes[i, 24] = Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //以下必要性不明のためコーディングせず
                                case 3://3T目のみ ☆5で必要

                                    break;
                                case 4://その他？なんだろうこれ？
                                    break;
                            }
                            break;

                        case "攻撃力上昇し、移動力追加":
                            switch (dtrow[0][abi_offset])
                            {
                                case 0://常時
                                       //対象人数で場合分け
                                    switch (dtrow[0][abi_offset + 1])
                                    {
                                        case 5://全員
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                movAdd += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                break;
                                            }
                                    }
                                    break;
                                case 1://1T目のみ
                                    {
                                        //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    movAdd += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //以下必要性不明のためコーディングせず
                                case 3://3T目のみ ☆5で必要

                                    break;
                                case 4://その他？なんだろうこれ？
                                    break;
                            }
                            break;

                        case "攻撃力上昇し、ソラ効果上昇":
                            switch (dtrow[0][abi_offset])
                            {
                                case 0://常時
                                       //対象人数で場合分け
                                    switch (dtrow[0][abi_offset + 1])
                                    {
                                        case 5://全員
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                solarRatio *= 1 + (Convert.ToDouble(dtrow[0][abi_offset + 4])) / 100;
                                                break;
                                            }
                                    }
                                    break;
                                case 1://1T目のみ
                                    {
                                        //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    solarRatio *= 1 + (Convert.ToDouble(dtrow[0][abi_offset + 4])) / 100;

                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //以下必要性不明のためコーディングせず
                                case 3://3T目のみ ☆5で必要

                                    break;
                                case 4://その他？なんだろうこれ？
                                    break;
                            }
                            break;

                        case "攻撃力上昇し、光ゲージ充填":
                            switch (dtrow[0][abi_offset])
                            {
                                case 0://常時
                                       //対象人数で場合分け
                                    switch (dtrow[0][abi_offset + 1])
                                    {
                                        case 5://全員
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                ptcalcRes[25] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                break;
                                            }
                                    }
                                    break;
                                case 1://1T目のみ
                                    {
                                        //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[25] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //以下必要性不明のためコーディングせず
                                case 3://3T目のみ ☆5で必要

                                    break;
                                case 4://その他？なんだろうこれ？
                                    break;
                            }
                            break;

                        case "攻撃力上昇し、シャイクリ泥率上昇":
                            switch (dtrow[0][abi_offset])
                            {
                                case 0://常時
                                       //対象人数で場合分け
                                    switch (dtrow[0][abi_offset + 1])
                                    {
                                        case 5://全員
                                            {
                                                ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                break;
                                            }
                                    }
                                    break;
                                case 1://1T目のみ
                                    {
                                        //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //以下必要性不明のためコーディングせず
                                case 3://3T目のみ ☆5で必要

                                    break;
                                case 4://その他？なんだろうこれ？
                                    break;
                            }
                            break;

                        case "スキル発動毎に攻撃力上昇":
                            {
                                //実現不可能。コーディングしない

                                break;
                            }

                        case "属性種類数により攻撃力上昇"://ヒツジグサ
                            {
                                int AttNum = 0;
                                for (int m = 0; m < 4; m++)
                                {
                                    if (Att[m] != 0)
                                    {
                                        AttNum++;
                                    }
                                }
                                switch (AttNum)
                                {
                                    case 1:
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            break;
                                        }
                                    case 2:
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 2;
                                            break;
                                        }
                                    case 3:
                                    case 4:
                                        {
                                            ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3;
                                            break;
                                        }
                                }

                                break;
                            }

                        case "防御力に応じて攻撃力上昇"://イキシア専用　現在未対応
                            {
                                break;
                            }

                        case "PTメンバーの数で攻撃力上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[9] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * mbrNum;

                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[10] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * mbrNum;

                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "対ボス攻撃力上昇":
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                        break;
                                }
                                break;
                            }

                        case "対ボス攻撃力上昇し、自身が更に上昇":
                            {
                                ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                calcRes[i, 14] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;


                            }

                        case "対ボス攻撃力上昇し、自身を含む2人がさらに上昇":
                            {
                                ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4;
                                calcRes[i, 14] += Convert.ToInt32(dtrow[0][abi_offset + 4]) - (Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4);
                                break;


                            }

                        case "ダメージ上昇":
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        ptcalcRes[13] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                        break;
                                }
                                break;
                            }

                        case "ターン毎ダメージ上昇"://パフィオペディルム、トリトニア
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点ではPT全体にしか効果が確認されていない
                                    case 5:
                                        {
                                            ptcalcRes[34] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            break;
                                        }
                                    case 1:
                                        {
                                            calcRes[i, 26] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            break;
                                        }
                                }
                                break;
                            }

                        case "HP割合ダメ上昇率":
                            {
                                switch (dtrow[0][abi_offset + 1])
                                {
                                    //
                                    //対象人数で場合分け(現時点では単体にしか効果が確認されていない
                                    case 5:
                                        ptcalcRes[13] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                        break;
                                    case 1:
                                        calcRes[i, 13] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                        break;
                                }
                                break;
                            }

                        case "弱点属性の敵に対するダメージ増加":
                            {
                                //とりあえず計算無し
                                break;
                            }

                        case "移動力を攻撃力に追加":
                            {
                                //移動力確定後、計算するのでここでは計算しない
                                break;
                            }

                        case "防御ダメ軽減率上昇":
                            {
                                //防御持ちフラグ立て
                                calcRes[i, 22] = 1;
                                ptcalcRes[31]++;
                                break;
                            }

                        case "クリ率上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[5] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 5] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[6] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        ptcalcRes[6] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        calcRes[i, 6] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        ptcalcRes[6] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        calcRes[i, 6] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        ptcalcRes[6] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        calcRes[i, 6] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 6] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }
                        case "クリダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[8] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        ptcalcRes[8] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        calcRes[i, 8] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        ptcalcRes[8] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        calcRes[i, 8] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        ptcalcRes[8] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        calcRes[i, 8] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 8] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }

                                break;
                            }

                        case "クリ率クリダメ上昇"://ウメとクリタエちゃんのみ？
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://5人
                                                {
                                                    ptcalcRes[5] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 5] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);

                                                    ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4;
                                                    calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 4]) - (Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 5] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://5人
                                                    {
                                                        ptcalcRes[6] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[8] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 6] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        calcRes[i, 8] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ
                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "クリダメ上昇し自身がさらに上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        ptcalcRes[7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }


                                }
                                calcRes[i, 7] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }

                        case "スキル発動率上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    //1.2倍持ちの人数をカウント
                                                    if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 20)
                                                    {
                                                        ptcalcRes[27]++;
                                                        calcRes[i, 17] = 1;//trueの意。outputで〇に変換する
                                                    }

                                                    break;
                                                }
                                            case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                {
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                                    break;

                                                }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                {
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                    break;

                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ。特殊あふん算に入る
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        //1.65倍持ちの人数カウント
                                                        if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 65)
                                                        {
                                                            ptcalcRes[28]++;
                                                            calcRes[i, 18] = 1;//trueの意。outputで〇に変換
                                                        }
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "スキル発動率1T目と3T目":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 1://1T目のみ。特殊あふん算に入る
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        //1.65倍持ちの人数カウント
                                                        if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 65)
                                                        {
                                                            ptcalcRes[28]++;
                                                            calcRes[i, 18] = 1;//trueの意。outputで〇に変換
                                                        }
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "スキル発動率上昇し、対ボスダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                                    //1.2倍持ちの人数をカウント
                                                    if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 20)
                                                    {
                                                        ptcalcRes[27]++;
                                                        calcRes[i, 17] = 1;//trueの意。outputで〇に変換する
                                                    }

                                                    break;
                                                }

                                            case 1://1人
                                                {
                                                    calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ。特殊あふん算に入る
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        //1.65倍持ちの人数カウント
                                                        if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 65)
                                                        {
                                                            ptcalcRes[28]++;
                                                            calcRes[i, 18] = 1;//trueの意。outputで〇に変換
                                                        }
                                                        break;
                                                    }

                                                case 1://1人
                                                    {
                                                        calcRes[i, 1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "スキル発動率上昇し、自身のスキルダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i,4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                                    //1.2倍持ちの人数をカウント
                                                    if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 20)
                                                    {
                                                        ptcalcRes[27]++;
                                                        calcRes[i, 17] = 1;//trueの意。outputで〇に変換する
                                                    }

                                                    break;
                                                }

                                            case 1://1人
                                                {
                                                    calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ。特殊あふん算に入る
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        calcRes[i,4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        //1.65倍持ちの人数カウント
                                                        if (Convert.ToInt32(dtrow[0][abi_offset + 3]) == 65)
                                                        {
                                                            ptcalcRes[28]++;
                                                            calcRes[i, 18] = 1;//trueの意。outputで〇に変換
                                                        }
                                                        break;
                                                    }

                                                case 1://1人
                                                    {
                                                        calcRes[i, 1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }

                        case "スキルLVでスキル発動率上昇":
                            {

                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    //V2-V1が4で割り切れない場合はエラーになります。
                                                    ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) + (skilLv[i] - 1) * (Convert.ToInt32(dtrow[0][abi_offset + 4]) - Convert.ToInt32(dtrow[0][abi_offset + 3])) / 4;
                                                    break;
                                                }
                                            //case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            //case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            //case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            case 1://1人
                                                {
                                                    calcRes[i, 0] += skilLv[i] * (Convert.ToInt32(dtrow[0][abi_offset + 4]) - Convert.ToInt32(dtrow[0][abi_offset + 3])) / 4;
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目のみ
                                        {
                                            //対象人数で場合分け
                                            switch (dtrow[0][abi_offset + 1])
                                            {
                                                case 5://全員
                                                    {
                                                        ptcalcRes[1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                                case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                    {
                                                        //ptcalcRes[1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                        //calcRes[i, 1] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[1];
                                                        break;

                                                    }
                                                case 1://1人
                                                    {
                                                        calcRes[i, 1] += 100 + Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }

                                    //以下必要性不明のためコーディングせず
                                    case 3://3T目のみ ☆5で必要

                                        break;
                                    case 4://その他？なんだろうこれ？
                                        break;
                                }
                                break;
                            }








                        case "スキルダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                            //case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            //case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            //case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            case 1://1人
                                                {
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }

                                }
                                break;
                            }

                        case "PTと自身スキルダメ上昇": //カカオ
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目
                                        {
                                            break;
                                        }


                                }

                                break;
                            }

                        case "スキルダメ上昇し、対ボスダメ上昇"://自分だけフルで加算、残りは全員に1/4加算（平均）
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            case 2://2人
                                                {
                                                    //スキルダメ
                                                    ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    //ボスダメ
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]) / 4;
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]) * 3 / 4;
                                                    break;
                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目
                                        {
                                            break;
                                        }


                                }

                                break;
                            }

                        case "スキルダメ上昇し、シャイクリ泥率上昇"://自分だけフルで加算、残りは全員に1/4加算（平均）
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            case 2://2人
                                                {
                                                    //スキルダメ
                                                    ptcalcRes[4] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                                    //シャイクリ泥率
                                                    ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            case 1://1人
                                                {
                                                    calcRes[i, 4] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;
                                    case 1://1T目
                                        {
                                            break;
                                        }


                                }

                                break;
                            }

                        case "対ボスダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                            //case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                            //        break;

                                            //    }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    break;

                                                }
                                            //case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                            //        break;

                                            //    }
                                            case 1://1人
                                                {
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }

                                }

                                break;
                            }



                        case "対ボスダメ上昇自身のボスダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            //case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4);
                                            //        break;

                                            //    }
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                                {
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2);
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;

                                                }
                                                //case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                                //    {
                                                //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                                //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - (Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4);
                                                //        break;

                                                //    }
                                                //case 1://1人
                                                //    {
                                                //        calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                //        break;
                                                //    }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }

                                }

                                break;
                            }
                        case "対ボス攻撃力ダメ上昇":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            case 5://全員
                                                {
                                                    ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                            //case 4://4人。自分だけフルで加算、残りは全員に3/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) * 3 / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            //case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            //    {
                                            //        ptcalcRes[15] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 2;
                                            //        calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[15];
                                            //        break;

                                            //    }
                                            //case 2://2人。自分だけフルで加算、残りは全員に1/4加算（平均）
                                            //    {
                                            //        ptcalcRes[0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) / 4;
                                            //        calcRes[i, 0] += Convert.ToInt32(dtrow[0][abi_offset + 3]) - ptcalcRes[0];
                                            //        break;

                                            //    }
                                            case 1://1人
                                                {
                                                    calcRes[i, 14] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    calcRes[i, 15] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                                    break;
                                                }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }

                                }


                                break;
                            }

                        case "再行動":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                        {
                                            //再行動フラグ立て
                                            calcRes[i, 25] = Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            //ptcalcRes[33]++;
                                            break;
                                        }

                                    case 1://1ターン目
                                        {
                                            //再行動フラグ立て
                                            calcRes[i, 24] = Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                            break;

                                        }
                                }
                                break;
                            }

                        case "PTに再行動付与":
                            {
                                ptcalcRes[33]+= Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                break;
                            }

                        case "回避":
                            {
                                //回避フラグ立て
                                calcRes[i, 20] = 1;
                                ptcalcRes[29]++;

                                break;
                            }

                        case "反撃"://超反撃持ちを表示させたい
                            {
                                //コーディングはしない。現在、V2の値は100倍して入っているので注意。
                                //反撃持ちフラグ立て
                                calcRes[i, 23] = 1;
                                ptcalcRes[32]++;
                                break;
                            }

                        case "追撃":
                            {
                                //追撃タイプにより分類
                                switch (Convert.ToString(dtrow[0][abi_offset + 5]))
                                {
                                    case "追撃1"://単体20%
                                        {
                                            calcRes[i, 27] += 100;
                                            break;
                                        }
                                    case "追撃2"://全体10%
                                        {
                                            calcRes[i, 27] += 20;
                                            break;
                                        }
                                    case "追撃3"://単体20% + 全体10%
                                        {
                                            calcRes[i, 27] += 3;
                                            break;
                                        }
                                    default://単体20%
                                        {
                                            calcRes[i, 27] += 100;
                                            break;
                                        }
                                }

                                break;
                            }

                        case "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇":
                            {
                                //コーディングはしない
                                break;
                            }

                        case "ソラ効果上昇":
                            {
                                solarRatio *= 1 + (Convert.ToDouble(dtrow[0][abi_offset + 3])) / 100;
                                break;
                            }

                        case "光ゲージ充填":
                            {
                                ptcalcRes[25] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                break;
                            }

                        case "光ゲージ充填し、自身が再行動":
                            {
                                ptcalcRes[25] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                //再行動フラグ立て
                                calcRes[i, 25] = Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }

                        case "シャイクリ泥率上昇":
                            {
                                ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                break;
                            }

                        case "ソラ効果シャイクリ泥率上昇":
                            {
                                solarRatio *= 1 + (Convert.ToDouble(dtrow[0][abi_offset + 3])) / 100;
                                ptcalcRes[21] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }

                        case "ソラ効果光ゲージ充填上昇":
                            {
                                solarRatio *= 1 + (Convert.ToDouble(dtrow[0][abi_offset + 3])) / 100;
                                ptcalcRes[25] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }

                        case "ソラ効果上昇し自身が再行動":
                            {
                                solarRatio *= 1 + (Convert.ToDouble(dtrow[0][abi_offset + 3])) / 100;
                                calcRes[i,25] += Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                break;
                            }

                        case "ソラ発動毎に攻撃力上昇":
                            {
                                //コーディングはしない
                                break;
                            }

                        case "ソラ発動毎にダメ上昇":
                            {
                                //コーディングはしない
                                break;
                            }

                        case "ダメ無効":
                            {
                                //バリアフラグ立て
                                calcRes[i, 21] = 1; 
                                ptcalcRes[30] += 1;
                                break;
                            }

                        case "攻撃力低下":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            //コーディング不可。敵が何匹出るのか予測出来ないので
                                            case 5://全員
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            case 2:
                                            case 1:
                                                {
                                                    ptcalcRes[23] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }

                                }
                                break;
                            }

                        case "スキル発動率低下":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            //コーディング不可。敵が何匹出るのか予測出来ないので
                                            case 5://全員
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            case 2:
                                            case 1:
                                                {
                                                    ptcalcRes[24] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }
                                }
                                break;
                            }

                        case "攻撃ミス":
                            {
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                           //対象人数で場合分け
                                        switch (dtrow[0][abi_offset + 1])
                                        {
                                            //コーディング不可。敵が何匹出るのか予測出来ないので
                                            case 5://全員
                                            case 3://3人。自分だけフルで加算、残りは全員に半分加算（平均）
                                            case 2:
                                            case 1:
                                                {
                                                    ptcalcRes[26] += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                                    break;
                                                }
                                        }
                                        break;

                                    case 1://1T目
                                        {
                                            //現時点ではここに来ることはないようだ
                                            break;
                                        }
                                }
                                break;
                            }

                        case "属性付与":
                            {
                                //有無を表すだけ
                                calcRes[i, 19] = 1;
                                //付与属性を配る処理
                                string attAdd = "";
                                attAdd = Convert.ToString(dtrow[0][abi_offset + 5]);

                                switch (attAdd)
                                {
                                    //i = 人
                                    //mbridの有無で人がいるか判断する
                                    case "斬":
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;
                                        }
                                        break;

                                    case "打":
                                        {
                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;
                                        }
                                        break;

                                    case "突":
                                        {
                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;
                                        }
                                        break;

                                    case "魔":
                                        {
                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;

                                    case "斬打":
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;

                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;
                                        }
                                        break;

                                    case "斬突":
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;

                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;
                                        }
                                        break;

                                    case "斬魔":
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;

                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;

                                    case "打突":
                                        {
                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;

                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;
                                        }
                                        break;

                                    case "打魔":
                                        {
                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;

                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;

                                    case "突魔":
                                        {
                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;

                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;

                                    case "斬打突":
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;

                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;

                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;


                                        }
                                        break;

                                    case "斬打魔":
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;

                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;

                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;

                                    case "斬突魔"://オジギソウ
                                        {
                                            //斬
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[0] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[0, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[0] += 1;

                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;

                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;

                                    case "打突魔"://ヒツジグサ
                                        {
                                            //打
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[1] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[1, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[1] += 1;

                                            //突
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[2] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[2, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[2] += 1;

                                            //魔
                                            //他人に付与できる数だから、自身への付与は除く
                                            attAddNum[3] += Convert.ToInt32(dtrow[0][abi_offset + 1]) - 1;
                                            //自身へのみ確定付与
                                            AttState[3, i] = 3;
                                            //またその属性持ちの中にカウントする
                                            Att[3] += 1;
                                        }
                                        break;
                                }



                                break;
                            }

                        case "HP回復":
                            {
                                //コーディングはしない
                                break;
                            }

                        case "PT移動力増加":
                        case "MAP画面アビと、移動力増加":

                            {
                                //ループ終了後、改めて移動力算定ループを作成し算出する
                                movAdd += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                break;
                            }

                        case "PT移動力増加し、移動力を攻撃力に追加":
                            {
                                //ループ終了後、改めて移動力算定ループを作成し算出する
                                movAdd += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                break;
                                //この先の算出ルーチンに書いてあるが、現状、移動力を攻撃力に追加する処理は行っていない
                            }

                        case "PT移動力増加し、対ボス攻撃力上昇":
                            {
                                movAdd += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                ptcalcRes[14] += Convert.ToInt32(dtrow[0][abi_offset + 4]);

                                break;
                            }

                        case "PT移動力増加し、自身が再行動":
                            {
                                movAdd += Convert.ToInt32(dtrow[0][abi_offset + 3]);
                                switch (dtrow[0][abi_offset])
                                {
                                    case 0://常時
                                        {
                                            //再行動フラグ立て
                                            calcRes[i, 25] = Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                            //ptcalcRes[33]++;
                                            break;
                                        }

                                    case 1://1ターン目
                                        {
                                            //再行動フラグ立て
                                            calcRes[i, 24] = Convert.ToInt32(dtrow[0][abi_offset + 4]);
                                            break;

                                        }
                                }
                                break;
                            }



                        case "自身が攻撃を受けた次Tにスキル発動率上昇":
                            {
                                //コーディングしない
                                break;
                            }
                        case "自身が攻撃を受けた次Tに攻撃力上昇":
                            {
                                //コーディングしない
                                break;
                            }
                        case "自身が攻撃を受けた次Tにダメ上昇"://ストック
                            {
                                //コーディングしない
                                break;
                            }


                    }

                }















                //i番目のキャラループ終了
            }
            //移動力計算ループ
            int pt_ave_mov = 0;


            //平均移動力算出
            foreach (int iMov in mov)
            {
                pt_ave_mov += iMov;
            }
            pt_ave_mov /= mbrNum;
            pt_ave_mov += movAdd;

            //
            // 移動力を上乗せする等の計算ルーチン挿入
            //
            //攻撃力に対して移動力を追加するが、現在の攻撃力に対して何％上昇という表示になっている以上、移動力増加分の％は表示できない
            //出力に対して行追加して、移動力による追加を実現する必要がある


            //PT移動力はptcalcResに格納する
            ptcalcRes[22] = pt_ave_mov;

            //ソーラー上昇率を格納
            ptcalcRes[20] = Convert.ToInt32(((solarRatio - 1) * 100));


            //属性付与確定ルーチン

            //属性付与アビ持ちの属性付与判定用関数
            //戻り値
            // -1  : 付与出来ず(関数が何も動作しない)
            // 0 : 付与不要
            // 2  : 属性付与可能性あり
            // 3  : 属性付与確定
            //
            //protected int AttAddConfirm(int charaNum, int mbrNum, ref int[] mbrid, int attAdd, ref int[,] AttState, ref int[] Att, int[] addNum)
            //attAdd 付与される属性No 0,1,2,3で斬、打、突、魔
            //addNum 付与可能属性数

            //属性付与確定ルーチン

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((AttState[i, j] == 1) || (AttState[i, j] == 3))
                    {
                        continue;
                    }
                    int result = AttAddConfirm(j, mbrNum, ref mbrid, i, ref AttState, ref Att, ref attAddNum);
                    AttState[i, j] = result;
                }
            }

            ////////////////////
            //出力メソッド呼出し
            ////////////////////
            OutputData(ref calcRes, ref ptcalcRes, ref mbrid, ref AttState, ref SkillInfo);


        }
        ///////////////////////////////////////////////////////
        //データ計算メソッド終わり
        ///////////////////////////////////////////////////////



        //属性付与アビ持ちの属性付与判定用関数
        //戻り値
        // -1  : 付与出来ず(関数が何も動作しない)
        // 0 : 付与不要
        // 2  : 属性付与可能性あり
        // 3  : 属性付与確定
        //
        protected int AttAddConfirm(int charaOrder, int mbrNum, ref int[] mbrid, int attAdd, ref int[,] AttState, ref int[] Att, ref int[] addNum)
        {
            //attAdd 付与される属性No 0,1,2,3で斬、打、突、魔
            //addNum 付与可能属性数

            int m = charaOrder;

            if (mbrid[m] != 0)
            {
                //自分の属性と同じ場合は属性付与しない
                if (AttState[attAdd, m] != 1)
                {
                    //属性付与が確定的かどうか確認
                    //属性付与可能数によって考察 2,4,6以上が考えられる

                    //属性付与可能数が4以下の場合
                    if ((addNum[attAdd] <= 4) && (addNum[attAdd] >= 1))
                    {

                        //その属性を既に持っているメンバー数と、PT全体のメンバー数を比べる。付与可能数より少なければ全員に付与

                        if (mbrNum - Att[attAdd] <= addNum[attAdd])
                        {
                            //確定的属性付与
                            return 3;
                            //AttState[0, m] = 3;
                        }
                        //
                        else
                        {
                            //付与可能性あり。△マークで表示する
                            return 2;
                            //AttState[0, m] = 2;
                        }
                    }
                    //属性付与数が5以上の場合
                    else if (addNum[attAdd] >= 5)
                    {
                        //確定的属性付与
                        return 3;
                    }
                    //属性付与数が0の場合
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    //自分の属性と同じなので付与しない
                    return 0;
                }
            }

            else
            {
                return -1;
            }
            //return -1;
        }


        ///////////////////////////////////////
        //
        // 出力ルーチン
        //
        ///////////////////////////////////////

        /// //////////
        /// 出力メソッド
        /// //////////
        protected void OutputData(ref int[,] calc, ref int[] ptcalc, ref int[] mbr, ref int[,] Att, ref string[,] SkillInfo)
        {
            //calc = new int[5, 20];
            //ptcalc = new int[30];

            /* もらいものの内訳
            0:スキル発動率（通常時）
            1:1T目
            2:2T目
            3:3T目
            4:スキルダメージ
            5:クリ発生率
            6:1T目
            7:クリダメ上昇率
            8:1T目
            9:攻撃力上昇率
            10:1T目
            11:2T目
            12:3T目
            13:ダメージ上昇率
            14:対ボス攻撃力上昇率
            15:対ボスダメ上昇率
            16:所持スキルの発動率
            17:1.2倍フラグ
            18:1.65倍フラグ
            19:属性付与持ちカウント
            20:回避持ち
            21:バリア持ち
            22:防御
            23:反撃
            24:再行動 1T目
            25:再行動 通常
            26:ダメージ上昇率 ターン毎（2T～4Tまで）
            27:追撃　（1:通常の２０％単体追撃 2:10％全体追撃 3:通常、全体追撃持ち-デュランタのみ）
             */


            /* もらいものの内訳
            0:スキル発動率（通常時）
            1:1T目
            2:2T目
            3:3T目
            4:スキルダメージ
            5:クリ発生率
            6:1T目
            7:クリダメ上昇率
            8:1T目
            9:攻撃力上昇率
            10:1T目 //マンリョウ
            11:2T目 //スノドロ、シャクヤクハロ　ターン毎30%でMAX60%
            12:3T目 //ヤドリギ　ターン毎20%でMAX40%
            13:ダメージ上昇率
            14:対ボス攻撃力上昇
            15:対ボスダメ上昇率
            20:ソーラー効果上昇率
            21:シャイクリ泥率
            22:移動力
            23:攻撃力低下率
            24:スキル発動低下率
            25:スタート時光充填率
            26:攻撃ミス
            27:1.2倍持ち人数
            28:1.65倍持ち人数
            29:回避持ち
            30:バリア持ち
            31:防御
            32:反撃
            33:再行動
            34::ダメージ上昇率 ターン毎（2T～4Tまで）

             */

            //各種数値の表示リミット値
            int critical_ratio_max = 80;
            int damage_down_max = 70;

            //敵の数、変則スキル倍率
            int enemyNum = 3;
            string hensokuRatio = ""; 
            //敵の数、変則スキル時のスキル倍率を取得
            switch (RadioButtonList11.SelectedItem.Value)
            {
                case "3":
                    {
                        enemyNum = 3;
                        hensokuRatio = "2.2";
                        break;
                    }
                case "2":
                    {
                        enemyNum = 2;
                        hensokuRatio = "2.8";
                        break;
                    }
                case "1":
                    {
                        enemyNum = 1;
                        hensokuRatio = "4.7";
                        break;
                    }
            }

            //計算結果を格納する配列
            //1次元目は対象となるものを規定する。
            //0:PT全体 1-5はPTメンバー
            int[,] output = new int[6, 30];

            //計算途中を格納
            int[] ratioBase = new int[5];

            //データテーブル作成
            //個人用結果出力用
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("fkgOut");
            ds.Tables.Add(dt);

            //カラム追加
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("typeExp1", typeof(string));
            dt.Columns.Add("typeExp2", typeof(string));
            dt.Columns.Add("fkg1", typeof(string));
            dt.Columns.Add("fkg2", typeof(string));
            dt.Columns.Add("fkg3", typeof(string));
            dt.Columns.Add("fkg4", typeof(string));
            dt.Columns.Add("fkg5", typeof(string));


            //もいっこデータテーブル作成
            //ＰＴ用計算結果出力用
            DataTable dtPt = new DataTable("fkgOutPt");
            ds.Tables.Add(dtPt);

            //カラム追加
            dtPt.Columns.Add("id", typeof(int));
            dtPt.Columns.Add("type", typeof(string));
            dtPt.Columns.Add("typeExp1", typeof(string));
            dtPt.Columns.Add("fkgPt", typeof(string));
            


            //id用変数定義
            int id = 0;

            //コーディング　計算ルーチン

            //出力ルーチン。表示場所が一か所の場合は、ptNumはあまり関係ないかも。もらったものを入れるだけ

            //花騎士名入力用文字列
            //スキルタイプ
            DataRow datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "名前";
            datarow["typeExp2"] = "";
            datarow["fkg1"] = mbrName[0];
            datarow["fkg2"] = mbrName[1];
            datarow["fkg3"] = mbrName[2];
            datarow["fkg4"] = mbrName[3];
            datarow["fkg5"] = mbrName[4];
            dt.Rows.Add(datarow);


            //スキルタイプ、倍率出力データ整形ルーチン
            double[] SkillRatio = new double[5];
            for (int i = 0; i < 5; i++)
            {
                if (SkillInfo[i, 1] != null)
                {
                    SkillRatio[i] = Convert.ToDouble(SkillInfo[i, 1]) / 10;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0;j<2;j++)
                {
                    if(SkillInfo[i, j]==null)
                    {
                        SkillInfo[i, j] = "&#x2713";
                    }
                }
            }

            //スキルタイプ
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "スキルタイプ";
            datarow["typeExp2"] = "";
            datarow["fkg1"] = SkillInfo[0, 0].ToString();
            datarow["fkg2"] = SkillInfo[1, 0].ToString();
            datarow["fkg3"] = SkillInfo[2, 0].ToString();
            datarow["fkg4"] = SkillInfo[3, 0].ToString();
            datarow["fkg5"] = SkillInfo[4, 0].ToString();
            dt.Rows.Add(datarow);

            //
            //ダメージ倍率出力
            //
            

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "スキルダメ倍率";
            datarow["typeExp2"] = "";
            if(SkillRatio[0]!=0)
            {
                if(SkillInfo[0, 0].ToString() == "変則")
                {
                    datarow["fkg1"] = hensokuRatio;
                }
                else
                    datarow["fkg1"] = SkillRatio[0].ToString();
            }
            else
                datarow["fkg1"] = "&#x2713";

            if (SkillRatio[1] != 0)
            {
                if (SkillInfo[1, 0].ToString() == "変則")
                {
                    datarow["fkg2"] = hensokuRatio;
                }
                else
                    datarow["fkg2"] = SkillRatio[1].ToString();
            }
            else
                datarow["fkg2"] = "&#x2713";

            if (SkillRatio[2] != 0)
            {
                if (SkillInfo[2, 0].ToString() == "変則")
                {
                    datarow["fkg3"] = hensokuRatio;
                }
                else
                    datarow["fkg3"] = SkillRatio[2].ToString();
            }   
            else
                datarow["fkg3"] = "&#x2713";

            if (SkillRatio[3] != 0)
            {
                if (SkillInfo[3, 0].ToString() == "変則")
                {
                    datarow["fkg4"] = hensokuRatio;
                }
                else
                    datarow["fkg4"] = SkillRatio[3].ToString();
            }   
            else
                datarow["fkg4"] = "&#x2713";

            if (SkillRatio[4] != 0)
            {
                if (SkillInfo[4, 0].ToString() == "変則")
                {
                    datarow["fkg5"] = hensokuRatio;
                }
                else
                    datarow["fkg5"] = SkillRatio[4].ToString();
            }   
            else
                datarow["fkg5"] = "&#x2713";

            dt.Rows.Add(datarow);

            //スキルレベルskilLv
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "スキルレベル";
            datarow["typeExp2"] = "";
            datarow["fkg1"] = skilLv[0].ToString();
            datarow["fkg2"] = skilLv[1].ToString();
            datarow["fkg3"] = skilLv[2].ToString();
            datarow["fkg4"] = skilLv[3].ToString();
            datarow["fkg5"] = skilLv[4].ToString();
            dt.Rows.Add(datarow);

            //スキル発動率

            //通常時の確率（の元）の計算
            for (int i = 0; i < 5; i++)
            {
                ratioBase[i] = calc[i, 16] * (100 + ptcalc[0] + calc[i, 0]);
            }


            //1.2倍、1.65倍の人数出力
            string[] out12 = new string[5];
            string[] out165 = new string[5];
            for (int m = 0; m < 5; m++)
            {
                
                {
                    out12[m] = "&#x2713";
                    out165[m] = "&#x2713";
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 17] == 1)
                {
                    out12[i] = "〇";
                }
                if (calc[i, 18] == 1)
                {
                    out165[i] = "〇";
                }

            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "1.2倍持ち";
            datarow["typeExp2"] = ptcalc[27].ToString() + "人";
            datarow["fkg1"] = out12[0];
            datarow["fkg2"] = out12[1];
            datarow["fkg3"] = out12[2];
            datarow["fkg4"] = out12[3];
            datarow["fkg5"] = out12[4];
            dt.Rows.Add(datarow);

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "1.65倍持ち";
            datarow["typeExp2"] = ptcalc[28].ToString() + "人";
            datarow["fkg1"] = out165[0];
            datarow["fkg2"] = out165[1];
            datarow["fkg3"] = out165[2];
            datarow["fkg4"] = out165[3];
            datarow["fkg5"] = out165[4];
            dt.Rows.Add(datarow);


            //回避
            string[] kaihiString = new string[5];
            for (int n = 0; n < 5; n++)
            {
                if (mbr[n] != 0)
                {
                    kaihiString[n] = "&#x2713";
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 20] == 1)
                {
                    kaihiString[i] = "〇";
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "回避";
            datarow["typeExp2"] = ptcalc[29].ToString() + "人";
            datarow["fkg1"] = kaihiString[0];
            datarow["fkg2"] = kaihiString[1];
            datarow["fkg3"] = kaihiString[2];
            datarow["fkg4"] = kaihiString[3];
            datarow["fkg5"] = kaihiString[4];
            dt.Rows.Add(datarow);


            


            //防御
            string[] deffendString = new string[5];
            for (int n = 0; n < 5; n++)
            {
                if (mbr[n] != 0)
                {
                    deffendString[n] = "&#x2713";
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 22] == 1)
                {
                    deffendString[i] = "〇";
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "防御";
            datarow["typeExp2"] = ptcalc[31].ToString() + "人";
            datarow["fkg1"] = deffendString[0];
            datarow["fkg2"] = deffendString[1];
            datarow["fkg3"] = deffendString[2];
            datarow["fkg4"] = deffendString[3];
            datarow["fkg5"] = deffendString[4];
            dt.Rows.Add(datarow);


            //反撃
            string[] hangekiString = new string[5];
            for (int n = 0; n < 5; n++)
            {
                if (mbr[n] != 0)
                {
                    hangekiString[n] = "&#x2713";
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 23] == 1)
                {
                    hangekiString[i] = "〇";
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "反撃";
            datarow["typeExp2"] = ptcalc[32].ToString() + "人";
            datarow["fkg1"] = hangekiString[0];
            datarow["fkg2"] = hangekiString[1];
            datarow["fkg3"] = hangekiString[2];
            datarow["fkg4"] = hangekiString[3];
            datarow["fkg5"] = hangekiString[4];
            dt.Rows.Add(datarow);

            //追撃
            string[] tsuigekiString = new string[5];
            for (int n = 0; n < 5; n++)
            {
                if (mbr[n] != 0)
                {
                    tsuigekiString[n] = "&#x2713";
                }
            }
            int p = 0;
            for (int i = 0; i < 5; i++)
            {
                switch (calc[i, 27])
                {
                    case 100:
                        {
                            tsuigekiString[i] = "単体";
                            p++;
                            break;
                        }
                    case 20:
                        {
                            tsuigekiString[i] = "全体";
                            p++;
                            break;
                        }
                    case 3:
                    case 23:
                    case 103:
                    case 120:
                    case 123:
                        {
                            tsuigekiString[i] = "単体＋全体";
                            p++;
                            break;
                        }
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "追撃";
            datarow["typeExp2"] = p + "人";
            datarow["fkg1"] = tsuigekiString[0];
            datarow["fkg2"] = tsuigekiString[1];
            datarow["fkg3"] = tsuigekiString[2];
            datarow["fkg4"] = tsuigekiString[3];
            datarow["fkg5"] = tsuigekiString[4];
            dt.Rows.Add(datarow);

            //バリア
            string[] bariaString = new string[5];
            for (int n = 0; n < 5; n++)
            {
                if (mbr[n] != 0)
                {
                    bariaString[n] = "&#x2713";
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 21] == 1)
                {
                    bariaString[i] = "〇";
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "ダメ無効";
            datarow["typeExp2"] = ptcalc[30].ToString() + "人";
            datarow["fkg1"] = bariaString[0];
            datarow["fkg2"] = bariaString[1];
            datarow["fkg3"] = bariaString[2];
            datarow["fkg4"] = bariaString[3];
            datarow["fkg5"] = bariaString[4];
            dt.Rows.Add(datarow);

            //再行動
            int saikoudouFlag = 0;
            //1T目のみの再行動値がある場合にフラグをオン
            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 24] > 0)
                {
                    saikoudouFlag++;
                }
            }
            //1T目の再行動値がある場合は、1ターン目と2ターン以降に分ける
            if(saikoudouFlag > 0)
            {
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "再行動";
                datarow["typeExp2"] = "1Ｔ目";
                datarow["fkg1"] = "＋" + (calc[0, 24] + calc[0, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg2"] = "＋" + (calc[1, 24] + calc[1, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg3"] = "＋" + (calc[2, 24] + calc[2, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg4"] = "＋" + (calc[3, 24] + calc[3, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg5"] = "＋" + (calc[4, 24] + calc[4, 25] + ptcalc[33]).ToString() + "％";
                dt.Rows.Add(datarow);

                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "";
                datarow["typeExp2"] = "2Ｔ以降";
                datarow["fkg1"] = "＋" + (calc[0, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg2"] = "＋" + (calc[1, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg3"] = "＋" + (calc[2, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg4"] = "＋" + (calc[3, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg5"] = "＋" + (calc[4, 25] + ptcalc[33]).ToString() + "％";
                dt.Rows.Add(datarow);

            }
            else
            {
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "再行動";
                datarow["typeExp2"] = "";
                datarow["fkg1"] = "＋" + (calc[0, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg2"] = "＋" + (calc[1, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg3"] = "＋" + (calc[2, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg4"] = "＋" + (calc[3, 25] + ptcalc[33]).ToString() + "％";
                datarow["fkg5"] = "＋" + (calc[4, 25] + ptcalc[33]).ToString() + "％"; 
                dt.Rows.Add(datarow);

            }

            //属性付与
            string[] attString = new string[5];
            for (int n = 0; n < 5; n++)
            {
                if (mbr[n] != 0)
                {
                    attString[n] = "&#x2713";
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (calc[i, 19] == 1)
                {
                    attString[i] = "〇";
                }
            }


            //属性表示

            string[,] AttString = new string[4, 5];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    switch (Att[i, j])
                    {
                        case 0:
                            {
                                AttString[i, j] = "&#x2713";
                                break;
                            }

                        case 1://元からその属性を持っている
                            {
                                AttString[i, j] = "〇";
                                break;
                            }
                        case 2://属性付与可能性がある
                            {
                                AttString[i, j] = "△";
                                break;
                            }
                        case 3://確定で属性付与される
                            {
                                AttString[i, j] = "〇";
                                break;
                            }

                        case -1://そこにPTメンバーいない時
                            {
                                AttString[i, j] = "&#x2713";
                                break;
                            }
                    }


                }

            }
            

           

            //属性数カウント処理
            int[] attCount = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((Att[i, j] == 1) || (Att[i, j] == 3))
                        attCount[i]++;
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "斬";
            datarow["typeExp2"] = attCount[0].ToString() + "人確定";
            datarow["fkg1"] = AttString[0, 0];
            datarow["fkg2"] = AttString[0, 1];
            datarow["fkg3"] = AttString[0, 2];
            datarow["fkg4"] = AttString[0, 3];
            datarow["fkg5"] = AttString[0, 4];
            dt.Rows.Add(datarow);

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "打";
            datarow["typeExp2"] = attCount[1].ToString() + "人確定";
            datarow["fkg1"] = AttString[1, 0];
            datarow["fkg2"] = AttString[1, 1];
            datarow["fkg3"] = AttString[1, 2];
            datarow["fkg4"] = AttString[1, 3];
            datarow["fkg5"] = AttString[1, 4];
            dt.Rows.Add(datarow);

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "突";
            datarow["typeExp2"] = attCount[2].ToString() + "人確定";
            datarow["fkg1"] = AttString[2, 0];
            datarow["fkg2"] = AttString[2, 1];
            datarow["fkg3"] = AttString[2, 2];
            datarow["fkg4"] = AttString[2, 3];
            datarow["fkg5"] = AttString[2, 4];
            dt.Rows.Add(datarow);

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "魔";
            datarow["typeExp2"] = attCount[3].ToString() + "人確定";
            datarow["fkg1"] = AttString[3, 0];
            datarow["fkg2"] = AttString[3, 1];
            datarow["fkg3"] = AttString[3, 2];
            datarow["fkg4"] = AttString[3, 3];
            datarow["fkg5"] = AttString[3, 4];
            dt.Rows.Add(datarow);

            



            //スキル発動率
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "スキル発動率";
            datarow["typeExp2"] = "1Ｔ目";

            if ((ptcalc[1] + calc[0, 1]) != 0)
                datarow["fkg1"] = ((ratioBase[0] / 100 * (ptcalc[1] + calc[0, 1])) / 100).ToString() + "％";
            else
                datarow["fkg1"] = (ratioBase[0] / 100).ToString() + "％";

            if ((ptcalc[1] + calc[1, 1]) != 0)
                datarow["fkg2"] = ((ratioBase[1] / 100 * (ptcalc[1] + calc[1, 1])) / 100).ToString() + "％";
            else
                datarow["fkg2"] = (ratioBase[1] / 100).ToString() + "％";

            if ((ptcalc[1] + calc[2, 1]) != 0)
                datarow["fkg3"] = ((ratioBase[2] / 100 * (ptcalc[1] + calc[2, 1])) / 100).ToString() + "％";
            else
                datarow["fkg3"] = (ratioBase[2] / 100).ToString() + "％";

            if ((ptcalc[1] + calc[3, 1]) != 0)
                datarow["fkg4"] = ((ratioBase[3] / 100 * (ptcalc[1] + calc[3, 1])) / 100).ToString() + "％";
            else
                datarow["fkg4"] = (ratioBase[3] / 100).ToString() + "％";

            if ((ptcalc[1] + calc[4, 1]) != 0)
                datarow["fkg5"] = ((ratioBase[4] / 100 * (ptcalc[1] + calc[4, 1])) / 100).ToString() + "％";
            else
                datarow["fkg5"] = (ratioBase[4] / 100).ToString() + "％";
            
            dt.Rows.Add(datarow);

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "";
            datarow["typeExp2"] = "2Ｔ以降";
            datarow["fkg1"] = (ratioBase[0] / 100).ToString() + "％";
            datarow["fkg2"] = (ratioBase[1] / 100).ToString() + "％";
            datarow["fkg3"] = (ratioBase[2] / 100).ToString() + "％";
            datarow["fkg4"] = (ratioBase[3] / 100).ToString() + "％";
            datarow["fkg5"] = (ratioBase[4] / 100).ToString() + "％";
            dt.Rows.Add(datarow);



            //スキルダメ上昇
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "スキルダメ上昇率";
            datarow["typeExp2"] = "";
            datarow["fkg1"] = "＋" + (ptcalc[4] + calc[0, 4]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[4] + calc[1, 4]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[4] + calc[2, 4]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[4] + calc[3, 4]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[4] + calc[4, 4]).ToString() + "％";
            dt.Rows.Add(datarow);

           


            //クリ発生率 1T目 
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "クリ発生率";
            datarow["typeExp2"] = "1Ｔ目";
            datarow["fkg1"] = "＋" + (ptcalc[5] + calc[0, 5] + ptcalc[6] + calc[0, 6]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[5] + calc[1, 5] + ptcalc[6] + calc[1, 6]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[5] + calc[2, 5] + ptcalc[6] + calc[2, 6]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[5] + calc[3, 5] + ptcalc[6] + calc[3, 6]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[5] + calc[4, 5] + ptcalc[6] + calc[4, 6]).ToString() + "％";
            dt.Rows.Add(datarow);

            //2T目以降
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "";
            datarow["typeExp2"] = "2Ｔ以降";
            datarow["fkg1"] = "＋" + (ptcalc[5] + calc[0, 5]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[5] + calc[1, 5]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[5] + calc[2, 5]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[5] + calc[3, 5]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[5] + calc[4, 5]).ToString() + "％";
            dt.Rows.Add(datarow);


            //クリダメ上昇率 1T目
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "クリダメ上昇率";
            datarow["typeExp2"] = "1Ｔ目";
            datarow["fkg1"] = "＋" + (ptcalc[7] + calc[0, 7] + ptcalc[8] + calc[0, 8]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[7] + calc[1, 7] + ptcalc[8] + calc[1, 8]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[7] + calc[2, 7] + ptcalc[8] + calc[2, 8]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[7] + calc[3, 7] + ptcalc[8] + calc[3, 8]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[7] + calc[4, 7] + ptcalc[8] + calc[4, 8]).ToString() + "％";
            dt.Rows.Add(datarow);

            ////2T目
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "";
            datarow["typeExp2"] = "2Ｔ以降";
            datarow["fkg1"] = "＋" + (ptcalc[7] + calc[0, 7]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[7] + calc[1, 7]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[7] + calc[2, 7]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[7] + calc[3, 7]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[7] + calc[4, 7]).ToString() + "％";
            dt.Rows.Add(datarow);


            ////攻撃力上昇率
            //ターン毎上昇値がある場合の処理
            int AtkUpByTurn = 0;
            if (ptcalc[11]>0)
            {
                AtkUpByTurn++;
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "攻撃力上昇率";
            if (AtkUpByTurn > 0)
            {//ターン毎フラグ時
                datarow["typeExp2"] = "1Ｔ目";
            }
            else
            {//通常時
                datarow["typeExp2"] = "";
            }
            datarow["fkg1"] = "＋" + (ptcalc[9] + calc[0, 9]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[9] + calc[1, 9]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[9] + calc[2, 9]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[9] + calc[3, 9]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[9] + calc[4, 9]).ToString() + "％";
            dt.Rows.Add(datarow);

            if (AtkUpByTurn > 0)
            {//ターン毎フラグ時
                //2T目と3T目を追加
                //2T目
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "";
                datarow["typeExp2"] = "2Ｔ目";
                datarow["fkg1"] = "＋" + (ptcalc[9] + calc[0, 9] + ptcalc[11]).ToString() + "％";
                datarow["fkg2"] = "＋" + (ptcalc[9] + calc[1, 9] + ptcalc[11]).ToString() + "％";
                datarow["fkg3"] = "＋" + (ptcalc[9] + calc[2, 9] + ptcalc[11]).ToString() + "％";
                datarow["fkg4"] = "＋" + (ptcalc[9] + calc[3, 9] + ptcalc[11]).ToString() + "％";
                datarow["fkg5"] = "＋" + (ptcalc[9] + calc[4, 9] + ptcalc[11]).ToString() + "％";
                dt.Rows.Add(datarow);

                //3T目
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "";
                datarow["typeExp2"] = "3Ｔ以降";
                datarow["fkg1"] = "＋" + (ptcalc[9] + calc[0, 9] + ptcalc[11] * 2).ToString() + "％";
                datarow["fkg2"] = "＋" + (ptcalc[9] + calc[1, 9] + ptcalc[11] * 2).ToString() + "％";
                datarow["fkg3"] = "＋" + (ptcalc[9] + calc[2, 9] + ptcalc[11] * 2).ToString() + "％";
                datarow["fkg4"] = "＋" + (ptcalc[9] + calc[3, 9] + ptcalc[11] * 2).ToString() + "％";
                datarow["fkg5"] = "＋" + (ptcalc[9] + calc[4, 9] + ptcalc[11] * 2).ToString() + "％";
                dt.Rows.Add(datarow);

            }



            ////ダメージ上昇率
            ///ターン毎上昇フラグ確認
            int DmgUpByTurn = 0;
            if((ptcalc[34] > 0) || (ptcalc[35] > 0))
            {
                DmgUpByTurn++;
            }
            for (int i = 0; i<5;i++)
            {
                if(calc[i, 26] > 0)
                {
                    DmgUpByTurn++;
                }
            }

            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "ダメージ上昇率";
            if (DmgUpByTurn > 0)
            {//ターン毎フラグ時
                datarow["typeExp2"] = "1Ｔ目";
            }
            else
            {//通常時
                datarow["typeExp2"] = "";
            }
            datarow["fkg1"] = "＋" + (ptcalc[13] + calc[0, 13]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[13] + calc[1, 13]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[13] + calc[2, 13]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[13] + calc[3, 13]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[13] + calc[4, 13]).ToString() + "％";
            dt.Rows.Add(datarow);

            if (DmgUpByTurn > 0)
            {//ターン毎フラグ時
                //2T目追加
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "";
                datarow["typeExp2"] = "2Ｔ目";
                datarow["fkg1"] = "＋" + (ptcalc[13] + calc[0, 13] + ptcalc[34] + ptcalc[35] + calc[0, 26]).ToString() + "％";
                datarow["fkg2"] = "＋" + (ptcalc[13] + calc[1, 13] + ptcalc[34] + ptcalc[35] + calc[1, 26]).ToString() + "％";
                datarow["fkg3"] = "＋" + (ptcalc[13] + calc[2, 13] + ptcalc[34] + ptcalc[35] + calc[2, 26]).ToString() + "％";
                datarow["fkg4"] = "＋" + (ptcalc[13] + calc[3, 13] + ptcalc[34] + ptcalc[35] + calc[3, 26]).ToString() + "％";
                datarow["fkg5"] = "＋" + (ptcalc[13] + calc[4, 13] + ptcalc[34] + ptcalc[35] + calc[4, 26]).ToString() + "％";
                dt.Rows.Add(datarow);

                //3T目追加
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "";
                datarow["typeExp2"] = "3Ｔ目";
                datarow["fkg1"] = "＋" + (ptcalc[13] + calc[0, 13] + (ptcalc[34] + ptcalc[35] + calc[0, 26]) * 2).ToString() + "％";
                datarow["fkg2"] = "＋" + (ptcalc[13] + calc[1, 13] + (ptcalc[34] + ptcalc[35] + calc[1, 26]) * 2).ToString() + "％";
                datarow["fkg3"] = "＋" + (ptcalc[13] + calc[2, 13] + (ptcalc[34] + ptcalc[35] + calc[2, 26]) * 2).ToString() + "％";
                datarow["fkg4"] = "＋" + (ptcalc[13] + calc[3, 13] + (ptcalc[34] + ptcalc[35] + calc[3, 26]) * 2).ToString() + "％";
                datarow["fkg5"] = "＋" + (ptcalc[13] + calc[4, 13] + (ptcalc[34] + ptcalc[35] + calc[4, 26]) * 2).ToString() + "％";
                dt.Rows.Add(datarow);

                //4T目追加
                datarow = dt.NewRow();
                datarow["id"] = id++;
                datarow["typeExp1"] = "";
                datarow["typeExp2"] = "4Ｔ以降";
                datarow["fkg1"] = "＋" + (ptcalc[13] + calc[0, 13] + (ptcalc[34] + calc[0, 26]) * 3 + ptcalc[35] *2).ToString() + "％";
                datarow["fkg2"] = "＋" + (ptcalc[13] + calc[1, 13] + (ptcalc[34] + calc[1, 26]) * 3 + ptcalc[35] * 2).ToString() + "％";
                datarow["fkg3"] = "＋" + (ptcalc[13] + calc[2, 13] + (ptcalc[34] + calc[2, 26]) * 3 + ptcalc[35] * 2).ToString() + "％";
                datarow["fkg4"] = "＋" + (ptcalc[13] + calc[3, 13] + (ptcalc[34] + calc[3, 26]) * 3 + ptcalc[35] * 2).ToString() + "％";
                datarow["fkg5"] = "＋" + (ptcalc[13] + calc[4, 13] + (ptcalc[34] + calc[4, 26]) * 3 + ptcalc[35] * 2).ToString() + "％";
                dt.Rows.Add(datarow);
            }


            ////対ボス攻撃力上昇率
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "対ボス攻撃力上昇率";
            datarow["typeExp2"] = "";
            datarow["fkg1"] = "＋" + (ptcalc[14] + calc[0, 14]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[14] + calc[1, 14]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[14] + calc[2, 14]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[14] + calc[3, 14]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[14] + calc[4, 14]).ToString() + "％";
            dt.Rows.Add(datarow);


            ////対ボスダメ上昇率
            datarow = dt.NewRow();
            datarow["id"] = id++;
            datarow["typeExp1"] = "対ボスダメ上昇率";
            datarow["typeExp2"] = "";
            datarow["fkg1"] = "＋" + (ptcalc[15] + calc[0, 15]).ToString() + "％";
            datarow["fkg2"] = "＋" + (ptcalc[15] + calc[1, 15]).ToString() + "％";
            datarow["fkg3"] = "＋" + (ptcalc[15] + calc[2, 15]).ToString() + "％";
            datarow["fkg4"] = "＋" + (ptcalc[15] + calc[3, 15]).ToString() + "％";
            datarow["fkg5"] = "＋" + (ptcalc[15] + calc[4, 15]).ToString() + "％";
            dt.Rows.Add(datarow);


            //
            //PT全体効果
            //
            int idPt = 0;

            //ソーラー上昇率
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "ソーラー上昇率";
            datarow["fkgPt"] = "＋" + ptcalc[20].ToString() + "％";
            dtPt.Rows.Add(datarow);


            //シャイクリ泥率
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "シャイクリ泥率";
            datarow["fkgPt"] = "＋" + ptcalc[21].ToString() + "％";
            dtPt.Rows.Add(datarow);


            //ソーラー充填率
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "光ゲージ充填率";
            datarow["fkgPt"] = "＋" + ptcalc[25].ToString() + "％";
            dtPt.Rows.Add(datarow);


            //移動力
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "移動力";
            datarow["fkgPt"] = ptcalc[22].ToString();
            dtPt.Rows.Add(datarow);


            //攻撃力低下率
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "攻撃力低下率";
            datarow["fkgPt"] = "ー" + ptcalc[23].ToString() + "％";
            dtPt.Rows.Add(datarow);


            //スキル発動低下率
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "スキル発動低下率";
            datarow["fkgPt"] = "ー" + Convert.ToInt32(ptcalc[24].ToString()) + "％";
            dtPt.Rows.Add(datarow);


            //攻撃ミス率
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "攻撃ミス率";
            datarow["fkgPt"] = "ー" + Convert.ToInt32(ptcalc[26].ToString()) + "％";
            dtPt.Rows.Add(datarow);
            
            //敵の数
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "敵の数設定";
            datarow["fkgPt"] = enemyNum + "体";
            dtPt.Rows.Add(datarow);

            //ランタナアビ
            //ランタナアビにより、スキル発動率補正
            int rantana = 0;
            switch (RadioButtonList12.SelectedItem.Value)
            {
                case "30":
                    {
                        rantana = 30;
                        break;
                    }
                case "20":
                    {
                        rantana = 20;
                        break;
                    }
                case "10":
                    {
                        rantana = 10;
                        break;
                    }
                case "0":
                    break;
            }
            datarow = dtPt.NewRow();
            datarow["id"] = idPt++;
            datarow["typeExp1"] = "ランタナアビ";
            datarow["fkgPt"] = "＋" + rantana + "％";
            dtPt.Rows.Add(datarow);

            //結果表示
            //ListView1表示
            ListView1.DataSource = dt;
            ListView1.DataBind();

            //ListView1表示
            ListView2.DataSource = dtPt;
            ListView2.DataBind();

        }


        //
        //キャラ検索補助関数。DataTableに、SortValueを追加し、アビ数値順にソートしやすくする
        //
        //レコード毎にアビを検索し、ユーザーが入力した検索アビに応じたアビ数値を判定し追加

        protected DataTable AddSortValue(DataTable dt_in, string ability1, ref int abi1Flag)
        {
            string[] AEx = new string[] {"A1Ex1","A2Ex1","A3Ex1","A4Ex1" };
            string[] AV1 = new string[] {"A1V1", "A2V1", "A3V1", "A4V1" };
            string[] AV2 = new string[] { "A1V2", "A2V2", "A3V2", "A4V2" };

            dt_in.Columns.Add("Name and Value", typeof(string));

            //DBのレコードでループ
            for (int i = 0; i < dt_in.Rows.Count; i++)
            {
                switch (ability1)
                {
                    case "1ターン目系":
                        {
                            //スルー
                            break;
                        }

                    case "スキル発動率1.2倍":
                        {
                            //スルー
                            break;
                        }
                    case "スキルLVでスキル発動率上昇":
                        {
                            //スルー
                            break;
                        }
                    case "スキル発動率上昇":
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    //ただし、"自身が攻撃を受けた次Tにスキル発動率上昇"は
                                    //計算しないため含まない
                                    case "スキル発動率上昇":
                                    case "スキルLVでスキル発動率上昇":
                                    case "スキル発動率上昇し、対ボスダメ上昇":
                                    case "スキル発動率上昇し、自身のスキルダメ上昇":
                                    case "スキル発動率1T目と3T目":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            double AV1D = (double)(100 + Convert.ToInt32(dt_in.Rows[i][AV1[j]])) / 100;
                                            string addStr = "";
                                            if (AV1D == 1.28)
                                                addStr = "-1.36";
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + AV1D + addStr + "倍";
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "攻撃力上昇し、スキル発動率上昇":
                                    case "攻撃力上昇し、スキルLVでスキル発動率上昇":
                                    case "攻撃力上昇し、1T目のスキル発動率上昇":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                            double AV2D = (double)(100 + Convert.ToInt32(dt_in.Rows[i][AV2[j]]))/100;
                                            string addStr = "";
                                            if (AV2D == 1.28)
                                                addStr = "-1.36";
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + AV2D + addStr + "倍";
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;

                            break;
                        }

                    case "クリ率上昇"://クリ率クリダメ上昇
                    case "クリ率上昇（PT全体対象）":
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch(dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "クリ率上昇":
                                    case "クリ率クリダメ上昇":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV1[j]] +"％";
                                            break;
                                        }
                                }
                            }
                            abi1Flag = 1;

                            break;
                        }

                    case "クリダメ上昇":
                    case "クリダメ上昇（PT全体対象）":
                        {
                            //sortValue判定ルーチン追加
                            int[] sortValue = { 0, 0, 0, 0 };

                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "クリダメ上昇":
                                    case "クリダメ上昇し自身がさらに上昇":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV1[j]]);
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "クリ率クリダメ上昇":                                    
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV2[j]]);
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;
                            dt_in.Rows[i]["SortValue"] = sortValue.Max();
                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + sortValue.Max() + "％";
                            break;
                        }

                    case "スキルダメージ増加":
                        {
                            //sortValue判定ルーチン追加
                            int[] sortValue = { 0, 0, 0, 0 };

                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "スキルダメ上昇":
                                    case "PTと自身スキルダメ上昇":
                                    case "スキルダメ上昇し、対ボスダメ上昇":
                                    case "スキルダメ上昇し、シャイクリ泥率上昇":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV1[j]]);
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "攻撃力上昇し、スキルダメージ上昇":
                                    case "スキル発動率上昇し、自身のスキルダメ上昇":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV2[j]]);
                                            break;
                                        }
                                }
                            }
                            abi1Flag = 1;
                            dt_in.Rows[i]["SortValue"] = sortValue.Max();
                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + sortValue.Max() + "％";
                            break;
                        }

                    case "ダメージ増加":
                        {
                            //sortValue判定ルーチン追加
                            int[] sortValue = { 0, 0, 0, 0 };

                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "ダメージ上昇":
                                    case "ターン毎ダメージ上昇":
                                    case "HP割合ダメ上昇率":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV1[j]]);
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "ソラ発動毎にダメ上昇":
                                    case "攻撃力上昇し、ターン毎にダメージ上昇":
                                    case "攻撃力上昇HP割合ダメ上昇":
                                    case "PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV2[j]]);
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;
                            dt_in.Rows[i]["SortValue"] = sortValue.Max();
                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + sortValue.Max() + "％";
                            break;
                        }

                    case "ボスに与えるダメージ増加":
                        {
                            //sortValue判定ルーチン追加
                            int[] sortValue = { 0, 0, 0, 0 };

                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "対ボスダメ上昇":
                                    case "対ボスダメ上昇自身のボスダメ上昇":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV1[j]]);
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "攻撃力上昇し、対ボスダメ上昇":
                                    case "対ボス攻撃力ダメ上昇":
                                    case "スキル発動率上昇し、対ボスダメ上昇":
                                    case "スキルダメ上昇し、対ボスダメ上昇":
                                        {
                                            sortValue[j] = Convert.ToInt32(dt_in.Rows[i][AV2[j]]);
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;
                            dt_in.Rows[i]["SortValue"] = sortValue.Max();
                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + sortValue.Max() + "％";

                            break;
                        }

                    case "再行動":
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "再行動":
                                    case "PTに再行動付与":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV1[j]] + "％";
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "攻撃力上昇し、再行動":
                                    case "光ゲージ充填し、自身が再行動":
                                    case "ソラ効果上昇し自身が再行動":
                                    case "PT移動力増加し、自身が再行動":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV2[j]] + "％";
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;
                            break;
                        }

                    case "防御"://防御ダメ軽減率上昇
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "防御ダメ軽減率上昇":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV1[j]] + "％";
                                            break;
                                        }

                                    //V2を使うパターン
                                    //case "":
                                    //    {
                                    //        dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                    //        break;
                                    //    }

                                }
                            }
                            abi1Flag = 1;
                            break;
                        }

                    case "反撃":
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    ////V1を使うパターン
                                    //case "防御ダメ軽減率上昇":
                                    //    {
                                    //        dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                    //        break;
                                    //    }

                                        //V2を使うパターン
                                    case "反撃":
                                    {
                                        dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                            double AV2Double = Convert.ToDouble(dt_in.Rows[i][AV2[j]]);
                                            AV2Double /= 100;
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " x" + AV2Double + "倍";
                                            break;
                                    }

                                }
                            }
                            abi1Flag = 1;
                            break;
                        }

                    case "攻撃力低下"://攻撃力上昇し、敵全体の攻撃力低下
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "攻撃力低下":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV1[j]] + "％";
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "攻撃力上昇し、敵全体の攻撃力低下":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV2[j]] + "％";
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;
                            break;
                        }

                    case "光ゲージ充填"://ソラ効果光ゲージ充填上昇
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "光ゲージ充填":
                                    case "光ゲージ充填し、自身が再行動":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV1[j]] + "％";
                                            break;
                                        }

                                    //V2を使うパターン
                                    case "ソラ効果光ゲージ充填上昇":
                                    case "攻撃力上昇し、光ゲージ充填":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV2[j]] + "％";
                                            break;
                                        }

                                }
                            }
                            abi1Flag = 1;
                            break;
                        }

                    case "自身が攻撃を受けた次ターン系"://ソラ効果光ゲージ充填上昇
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                //実際のEx1値を確認
                                switch (dt_in.Rows[i][AEx[j]])
                                {
                                    //V1を使うパターン
                                    case "自身が攻撃を受けた次Tにスキル発動率上昇":
                                    case "自身が攻撃を受けた次Tに攻撃力上昇":
                                    case "自身が攻撃を受けた次Tにダメ上昇":
                                        {
                                            dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV1[j]];
                                            dt_in.Rows[i]["Name and Value"] = dt_in.Rows[i]["Name"].ToString() + " +" + dt_in.Rows[i][AV1[j]] + "％";
                                            break;
                                        }

                                    ////V2を使うパターン
                                    //case "ソラ効果光ゲージ充填上昇":
                                    //case "攻撃力上昇し、光ゲージ充填":
                                    //    {
                                    //        dt_in.Rows[i]["SortValue"] = dt_in.Rows[i][AV2[j]];
                                    //        break;
                                    //    }

                                }
                            }
                            abi1Flag = 1;
                            break;
                        }


                }
            }



            return dt_in;
        }



    }//クラス外枠
}