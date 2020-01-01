//花騎士登録ボタン
protected void Button101_Click(object sender, EventArgs e)
{
    //ドロップダウンリストより花騎士名入手
    //スキルレベル入手
    //デリミタは#

    string fkg1Name = this.DropDownList1.SelectedValue;
    string fkg1Slv = "";

    switch (RadioButtonList1.SelectedItem.Value)
    {
        case "1":
            {
                fkg1Slv = "1";
                break;
            }
        case "2":
            {
                fkg1Slv = "2";
                break;
            }
        case "3":
            {
                fkg1Slv = "3";
                break;
            }
        case "4":
            {
                fkg1Slv = "4";
                break;
            }
        case "5":
            {
                fkg1Slv = "5";
                break;
            }
    }

    //
    //同じ番号が登録されていないか確認する処理
    //
    //if{
    //    //もし同じ番号があれば、エラーメッセージ出して処理終了
    //    return;
    //}


    string writeStr = "#" + fkg1Name + "#" + fkg1Slv;



    //
    //userIdはクッキーより取得　ここでは適当に1とする
    //
    int Id = 1;


    //DBから取得
    //どこへ書き込むべきか確認する
    string list_querry = "SELECT Id, Data1Num,Data2Num,Data3Num FROM [dbo].[Userdata] WHERE Id = " + Id;
    DataSet ds_check = new DataSet();
    ds_check = GetData(list_querry);

    //null許容型を指定
    int?[] dataNum = new int?[3];
    dataNum[0] = ds_check.Tables[0].Rows[0].Field<int?>("Data1Num");
    dataNum[1] = ds_check.Tables[0].Rows[0].Field<int?>("Data2Num");
    dataNum[2] = ds_check.Tables[0].Rows[0].Field<int?>("Data3Num");

    //どこへ書き込むか判定する
    string dataNumStr = "";
    int writeNum = 0;
    if (dataNum[2] != null)//data3が既に書き込まれている場合data3に書き込む
    {
        list_querry = "SELECT Id, Data3 FROM [dbo].[Userdata] WHERE Id = " + Id;
        dataNumStr = "Data3";

        writeNum = (int)dataNum[2] + 1;
    }
    else if (dataNum[1] >= 450)//data2がいっぱいの場合はdata3に書き込む
    {
        list_querry = "SELECT Id, Data3 FROM [dbo].[Userdata] WHERE Id = " + Id;
        dataNumStr = "Data3";

        if (dataNum[2] == null)
        {
            dataNum[2] = 0;
        }
        writeNum = (int)dataNum[2] + 1;
    }
    else if (dataNum[1] != null)//data2が既に書き込まれている場合data2に書き込む
    {
        list_querry = "SELECT Id, Data2 FROM [dbo].[Userdata] WHERE Id = " + Id;
        dataNumStr = "Data2";

        writeNum = (int)dataNum[1] + 1;

    }
    else if (dataNum[0] >= 450)//data1がいっぱいの場合はdata2に書き込む
    {
        list_querry = "SELECT Id, Data2 FROM [dbo].[Userdata] WHERE Id = " + Id;
        dataNumStr = "Data2";

        if (dataNum[1] == null)
        {
            dataNum[1] = 0;
        }
        writeNum = (int)dataNum[1] + 1;
    }
    else//data2,3にデータが入ってない場合はdata1に入れる
    {
        list_querry = "SELECT Id, Data1 FROM [dbo].[Userdata] WHERE Id = '" + Id + "'";
        dataNumStr = "Data1";

        if (dataNum[0] == null)
        {
            dataNum[0] = 0;
        }
        writeNum = (int)dataNum[0] + 1;
    }

    //DBより書き込むべきデータ取得
    DataSet ds_data = GetData(list_querry);
    string dataOrigen = ds_data.Tables[0].Rows[0][dataNumStr].ToString();
    string dataInput = dataOrigen + writeStr;

    //DBへ追加書き込み
    //書き込みクエリ作成
    string insertCmd = "SET Id = '" + Id + "', " + dataNumStr + " = '" + dataInput + "', " + dataNumStr + "Num = '" + writeNum + "' "
                        + "WHERE Id = '" + Id + "'";

    try
    {
        //編集処理
        cn.ConnectionString = cnstr;
        cn.Open();
        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE [dbo].[Userdata] " + insertCmd;
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
    