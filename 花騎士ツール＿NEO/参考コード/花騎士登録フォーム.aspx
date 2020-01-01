<%@ Page Language="C#" AutoEventWireup="true" CodeFile="花騎士登録フォーム.aspx.cs" Inherits="花騎士登録フォーム" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="robots" content="noindex,nofollow"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="花騎士登録フォーム"></asp:Label>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="入力クリア" />
        </div>
        <p>
            ID<asp:TextBox ID="TextBox1" runat="server" Width="85px"></asp:TextBox>
            名前<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="データ呼出し" />
        </p>
        <p>
            ☆<asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList5" runat="server">
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
            所属<asp:DropDownList ID="DropDownList6" runat="server">
                <asp:ListItem>ブロッサムヒル</asp:ListItem>
                <asp:ListItem>リリィウッド</asp:ListItem>
                <asp:ListItem>バナナオーシャン</asp:ListItem>
                <asp:ListItem>ベルガモットバレー</asp:ListItem>
                <asp:ListItem>ウィンターローズ</asp:ListItem>
                <asp:ListItem>ロータスレイク</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            各ステータス</p>
        <p>
            HP<asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox>
            ATK<asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
            DEF<asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
            MOV<asp:TextBox ID="TextBox6" runat="server" Width="100px"></asp:TextBox>
            </p>
        <p>
            スキル発動率<asp:TextBox ID="TextBox7" runat="server" Width="150px"></asp:TextBox>
            Lv5での発動率<asp:TextBox ID="TextBox16" runat="server" Width="150px"></asp:TextBox>
            所有アビリティ数<asp:DropDownList ID="DropDownList16" runat="server">
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            スキルタイプ<asp:DropDownList ID="DropDownList17" runat="server">
                <asp:ListItem>全体</asp:ListItem>
                <asp:ListItem>2体</asp:ListItem>
                <asp:ListItem>変則</asp:ListItem>
                <asp:ListItem>吸収</asp:ListItem>
                <asp:ListItem>複数回</asp:ListItem>
                <asp:ListItem>単体</asp:ListItem>
            </asp:DropDownList>
            スキル倍率<asp:TextBox ID="TextBox17" runat="server" Width="150px"></asp:TextBox>
        </p>
        <p>
            アビリティ1</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList3" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、回避</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、再行動</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、移動力追加</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ソラ効果上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、光ゲージ充填</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox8" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox9" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList21" runat="server">
            <asp:ListItem Value="特に無し">特に無し</asp:ListItem>
            <asp:ListItem>斬</asp:ListItem>
            <asp:ListItem>打</asp:ListItem>
            <asp:ListItem>突</asp:ListItem>
            <asp:ListItem>魔</asp:ListItem>
            <asp:ListItem>斬打</asp:ListItem>
            <asp:ListItem>斬突</asp:ListItem>
            <asp:ListItem>斬魔</asp:ListItem>
            <asp:ListItem>打突</asp:ListItem>
            <asp:ListItem>打魔</asp:ListItem>
            <asp:ListItem>突魔</asp:ListItem>
            <asp:ListItem>斬打突</asp:ListItem>
            <asp:ListItem>斬打魔</asp:ListItem>
            <asp:ListItem>斬突魔</asp:ListItem>
            <asp:ListItem>打突魔</asp:ListItem>
            <asp:ListItem>超反撃</asp:ListItem>
            <asp:ListItem>敵の数減少</asp:ListItem>
        </asp:DropDownList>
        <p>
            アビリティ2</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList7" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList8" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList9" runat="server">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、回避</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、再行動</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、移動力追加</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ソラ効果上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、光ゲージ充填</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox10" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox11" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList22" runat="server">
            <asp:ListItem Value="特に無し">特に無し</asp:ListItem>
            <asp:ListItem>斬</asp:ListItem>
            <asp:ListItem>打</asp:ListItem>
            <asp:ListItem>突</asp:ListItem>
            <asp:ListItem>魔</asp:ListItem>
            <asp:ListItem>斬打</asp:ListItem>
            <asp:ListItem>斬突</asp:ListItem>
            <asp:ListItem>斬魔</asp:ListItem>
            <asp:ListItem>打突</asp:ListItem>
            <asp:ListItem>打魔</asp:ListItem>
            <asp:ListItem>突魔</asp:ListItem>
            <asp:ListItem>斬打突</asp:ListItem>
            <asp:ListItem>斬打魔</asp:ListItem>
            <asp:ListItem>斬突魔</asp:ListItem>
            <asp:ListItem>打突魔</asp:ListItem>
            <asp:ListItem>超反撃</asp:ListItem>
            <asp:ListItem>敵の数減少</asp:ListItem>
        </asp:DropDownList>
        <p>
            アビリティ3</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList10" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList11" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList12" runat="server">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、回避</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、再行動</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、移動力追加</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ソラ効果上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、光ゲージ充填</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox12" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox13" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList23" runat="server">
            <asp:ListItem Value="特に無し">特に無し</asp:ListItem>
            <asp:ListItem>斬</asp:ListItem>
            <asp:ListItem>打</asp:ListItem>
            <asp:ListItem>突</asp:ListItem>
            <asp:ListItem>魔</asp:ListItem>
            <asp:ListItem>斬打</asp:ListItem>
            <asp:ListItem>斬突</asp:ListItem>
            <asp:ListItem>斬魔</asp:ListItem>
            <asp:ListItem>打突</asp:ListItem>
            <asp:ListItem>打魔</asp:ListItem>
            <asp:ListItem>突魔</asp:ListItem>
            <asp:ListItem>斬打突</asp:ListItem>
            <asp:ListItem>斬打魔</asp:ListItem>
            <asp:ListItem>斬突魔</asp:ListItem>
            <asp:ListItem>打突魔</asp:ListItem>
            <asp:ListItem>超反撃</asp:ListItem>
            <asp:ListItem>敵の数減少</asp:ListItem>
        </asp:DropDownList>
        <p>
            アビリティ4</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList13" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList14" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList15" runat="server">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
           <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、回避</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、再行動</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、移動力追加</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ソラ効果上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、光ゲージ充填</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox14" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox15" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList24" runat="server">
            <asp:ListItem Value="特に無し">特に無し</asp:ListItem>
            <asp:ListItem>斬</asp:ListItem>
            <asp:ListItem>打</asp:ListItem>
            <asp:ListItem>突</asp:ListItem>
            <asp:ListItem>魔</asp:ListItem>
            <asp:ListItem>斬打</asp:ListItem>
            <asp:ListItem>斬突</asp:ListItem>
            <asp:ListItem>斬魔</asp:ListItem>
            <asp:ListItem>打突</asp:ListItem>
            <asp:ListItem>打魔</asp:ListItem>
            <asp:ListItem>突魔</asp:ListItem>
            <asp:ListItem>斬打突</asp:ListItem>
            <asp:ListItem>斬打魔</asp:ListItem>
            <asp:ListItem>斬突魔</asp:ListItem>
            <asp:ListItem>打突魔</asp:ListItem>
            <asp:ListItem>超反撃</asp:ListItem>
            <asp:ListItem>敵の数減少</asp:ListItem>
        </asp:DropDownList>
        <p>
            <asp:CheckBox ID="CheckBox1" runat="server" Text="編集モード" />
            <asp:Button ID="Button1" runat="server" Height="75px" Text="登録" Width="200px" OnClick="Button1_Click" />
            　　　　　　　　　<asp:Button ID="Button2" runat="server" Height="75px" OnClick="Button2_Click" Text="入力クリア" Width="200px" />
        </p>
    </form>
</body>
</html>
