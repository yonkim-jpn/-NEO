<%@ Page Async="true" Title="DB登録フォーム" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DB_register.aspx.cs" Inherits="花騎士ツール＿NEO.DB_register" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div>
            <h3 id="DBtop"><a href="https://xn--eckq7fg8cygsa1a1je.xn--wiki-4i9hs14f.com/index.php?%E5%9B%B3%E9%91%91" target="_blank">花騎士登録フォーム</a></h3>
            <asp:TextBox ID="wikiURL" runat="server" Text="wikiURL貼り付け" CssClass="DB-block" Width="400px"></asp:TextBox>
            <asp:Button ID="dbButton" runat="server" Text="スクレーピング" CssClass="DB-block" OnClick="Db_Button_Click" />
        </div>
        <div>
            <asp:Button ID="Button50003" runat="server" OnClick="Button50003_Click" Text="入力クリア" CssClass="DB-block" />
        </div>
        <p>
            ID<asp:TextBox ID="TextBox50001" runat="server" Width="85px"></asp:TextBox>
            名前<asp:TextBox ID="TextBox50002" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button50004" runat="server" OnClick="Button50004_Click" Text="データ呼出し" />
        </p>
        <p>
            ☆<asp:DropDownList ID="DropDownList50001" runat="server">
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList50005" runat="server">
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
            所属<asp:DropDownList ID="DropDownList50006" runat="server">
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
            HP<asp:TextBox ID="TextBox50003" runat="server" Width="100px"></asp:TextBox>
            ATK<asp:TextBox ID="TextBox50004" runat="server" Width="100px"></asp:TextBox>
            DEF<asp:TextBox ID="TextBox50005" runat="server" Width="100px"></asp:TextBox>
            MOV<asp:TextBox ID="TextBox50006" runat="server" Width="100px"></asp:TextBox>
            </p>
        <p>
            スキル発動率<asp:TextBox ID="TextBox50007" runat="server" Width="150px"></asp:TextBox>
            Lv5での発動率<asp:TextBox ID="TextBox50016" runat="server" Width="150px"></asp:TextBox>
            所有アビリティ数<asp:DropDownList ID="DropDownList50016" runat="server">
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            スキルタイプ<asp:DropDownList ID="DropDownList50017" runat="server">
                <asp:ListItem>全体</asp:ListItem>
                <asp:ListItem>2体</asp:ListItem>
                <asp:ListItem>変則</asp:ListItem>
                <asp:ListItem>変則吸収</asp:ListItem>
                <asp:ListItem>吸収</asp:ListItem>
                <asp:ListItem>複数回</asp:ListItem>
                <asp:ListItem>単体</asp:ListItem>
            </asp:DropDownList>
            スキル倍率<asp:TextBox ID="TextBox50017" runat="server" Width="150px"></asp:TextBox>
        </p>
        <p>
            アビリティ1</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList50002" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50003" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50004" runat="server" OnSelectedIndexChanged="DropDownList50004_SelectedIndexChanged">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキルLVで攻撃力上昇</asp:ListItem>
            <asp:ListItem>残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、残HPでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、弱点属性の敵に対するダメージ増加</asp:ListItem>
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
            <asp:ListItem>攻撃力上昇し、追撃</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自身のHP回復(効果値はEx2)</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇さらに自身のクリ率とダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身のスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇し、敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>自身が再行動し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>回避し、残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>回避し、反撃</asp:ListItem>
            <asp:ListItem>回避し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>迎撃</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>追撃し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、自身が再行動</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇し自身が再行動</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>防御力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、自身が再行動</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、スキルダメ上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox50008" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox50009" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList50021" runat="server">
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
            <asp:ListItem Value="追撃1">追撃1:単体</asp:ListItem>
            <asp:ListItem Value="追撃2">追撃2:全体</asp:ListItem>
            <asp:ListItem Value="追撃3">追撃3:単体と全体</asp:ListItem>
            <asp:ListItem Value="追撃4">追撃4:PT全体に単体20%付与</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>ガッツ付与1回</asp:ListItem>
            <asp:ListItem>3ターン</asp:ListItem>
            <asp:ListItem>PT全体</asp:ListItem>
            <asp:ListItem>自身</asp:ListItem>
        </asp:DropDownList>
        <p>
            アビリティ2</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList50007" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50008" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50009" runat="server">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキルLVで攻撃力上昇</asp:ListItem>
            <asp:ListItem>残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、残HPでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、弱点属性の敵に対するダメージ増加</asp:ListItem>
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
            <asp:ListItem>攻撃力上昇し、追撃</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自身のHP回復(効果値はEx2)</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇さらに自身のクリ率とダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身のスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>自身が再行動し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>回避し、残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>回避し、反撃</asp:ListItem>
            <asp:ListItem>回避し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>迎撃</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>追撃し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、自身が再行動</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇し自身が再行動</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>防御力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、自身が再行動</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、スキルダメ上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox50010" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox50011" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList50022" runat="server">
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
            <asp:ListItem Value="追撃1">追撃1:単体</asp:ListItem>
            <asp:ListItem Value="追撃2">追撃2:全体</asp:ListItem>
            <asp:ListItem Value="追撃3">追撃3:単体と全体</asp:ListItem>
            <asp:ListItem Value="追撃4">追撃4:PT全体に単体20%付与</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>ガッツ付与1回</asp:ListItem>
            <asp:ListItem>3ターン</asp:ListItem>
            <asp:ListItem>PT全体</asp:ListItem>
            <asp:ListItem>自身</asp:ListItem>
        </asp:DropDownList>
        <p>
            アビリティ3</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList50010" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50011" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50012" runat="server">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキルLVで攻撃力上昇</asp:ListItem>
            <asp:ListItem>残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、残HPでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、弱点属性の敵に対するダメージ増加</asp:ListItem>
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
            <asp:ListItem>攻撃力上昇し、追撃</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自身のHP回復(効果値はEx2)</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇さらに自身のクリ率とダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身のスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>自身が再行動し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>回避し、残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>回避し、反撃</asp:ListItem>
            <asp:ListItem>回避し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>迎撃</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>追撃し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、自身が再行動</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇し自身が再行動</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>防御力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、自身が再行動</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、スキルダメ上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox50012" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox50013" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList50023" runat="server">
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
            <asp:ListItem Value="追撃1">追撃1:単体</asp:ListItem>
            <asp:ListItem Value="追撃2">追撃2:全体</asp:ListItem>
            <asp:ListItem Value="追撃3">追撃3:単体と全体</asp:ListItem>
            <asp:ListItem Value="追撃4">追撃4:PT全体に単体20%付与</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>ガッツ付与1回</asp:ListItem>
            <asp:ListItem>3ターン</asp:ListItem>
            <asp:ListItem>PT全体</asp:ListItem>
            <asp:ListItem>自身</asp:ListItem>
        </asp:DropDownList>
        <p>
            アビリティ4</p>
        <p>
            発動形態&nbsp;&nbsp;&nbsp; 対象人数 スキル内容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;　上昇値&nbsp;&nbsp;&nbsp;&nbsp; 上昇値二つ目</p>
        <asp:DropDownList ID="DropDownList50013" runat="server">
            <asp:ListItem Value="0">常時発動</asp:ListItem>
            <asp:ListItem Value="1">1ターン目</asp:ListItem>
            <asp:ListItem Value="3">3ターン目</asp:ListItem>
            <asp:ListItem Value="4">その他</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50014" runat="server">
            <asp:ListItem Value="5">PT全体</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList50015" runat="server">
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>敵の数で攻撃力上昇</asp:ListItem>
           <asp:ListItem>ターンで攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキルLVで攻撃力上昇</asp:ListItem>
            <asp:ListItem>残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇ターンでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇1T目さらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇HP割合ダメ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、残HPでさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自信を含む3人がさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、敵の数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、PTメンバー数でさらに上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、1T目のスキル発動率上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、スキルダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、ターン毎にダメージ上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、弱点属性の敵に対するダメージ増加</asp:ListItem>
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
            <asp:ListItem>攻撃力上昇し、追撃</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>攻撃力上昇し、自身のHP回復(効果値はEx2)</asp:ListItem>
            <asp:ListItem>スキル発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>属性種類数により攻撃力上昇</asp:ListItem>
            <asp:ListItem>防御力に応じて攻撃力上昇</asp:ListItem>
            <asp:ListItem>PTメンバーの数で攻撃力上昇</asp:ListItem>
            <asp:ListItem>ダメージ上昇</asp:ListItem>
            <asp:ListItem>ターン毎ダメージ上昇</asp:ListItem>
            <asp:ListItem>HP割合ダメ上昇率</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加</asp:ListItem>
            <asp:ListItem>弱点属性の敵に対するダメージ増加し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>防御ダメ軽減率上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリダメ上昇し自身がさらに上昇</asp:ListItem>
            <asp:ListItem>クリ率クリダメ上昇</asp:ListItem>
            <asp:ListItem>クリ率上昇さらに自身のクリ率とダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1T目と3T目</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身のスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、自身と同属性のPTメンバーのスキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇し、敵全体の攻撃力低下</asp:ListItem>
            <asp:ListItem>スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇</asp:ListItem>
            <asp:ListItem>PTと自身スキルダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメ上昇し、ダメ無効</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボスダメ上昇自身のボスダメ上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身が更に上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力上昇し、自身を含む2人がさらに上昇</asp:ListItem>
            <asp:ListItem>対ボス攻撃力ダメ上昇</asp:ListItem>
            <asp:ListItem Value="再行動">自身が再行動</asp:ListItem>
            <asp:ListItem>自身が再行動し、シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>PTに再行動付与</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>回避し、残りHPで攻撃力上昇</asp:ListItem>
            <asp:ListItem>回避し、反撃</asp:ListItem>
            <asp:ListItem>回避し、敵3体が攻撃ミス</asp:ListItem>
            <asp:ListItem>迎撃</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>追撃し、スキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>追撃し、自身が再行動</asp:ListItem>
            <asp:ListItem>PTメンバーのHPが50%以下の場合、自身の攻撃、与ダメ上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>光ゲージ充填シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填し、自身が再行動</asp:ListItem>
            <asp:ListItem>シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果シャイクリ泥率上昇</asp:ListItem>
            <asp:ListItem>ソラ効果光ゲージ充填上昇</asp:ListItem>
            <asp:ListItem>ソラ効果上昇し自身が再行動</asp:ListItem>
            <asp:ListItem>ソラ発動毎に攻撃力上昇</asp:ListItem>
            <asp:ListItem>ソラ発動毎にダメ上昇</asp:ListItem>
            <asp:ListItem>ダメ無効</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>防御力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>攻撃ミス</asp:ListItem>
            <asp:ListItem>ターン毎に行動回数減</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>PT移動力増加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、スキルLVでスキル発動率上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、移動力を攻撃力に追加</asp:ListItem>
            <asp:ListItem>PT移動力増加し、対ボス攻撃力上昇</asp:ListItem>
            <asp:ListItem>PT移動力増加し、自身が再行動</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにスキル発動率上昇し、ダメージ上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tに攻撃力上昇</asp:ListItem>
            <asp:ListItem>自身が攻撃を受けた次Tにダメ上昇</asp:ListItem>
            <asp:ListItem>その他(MAP画面スキル)</asp:ListItem>
            <asp:ListItem>MAP画面アビと、攻撃力上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、スキルダメ上昇</asp:ListItem>
            <asp:ListItem>MAP画面アビと、移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox50014" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="TextBox50015" runat="server" Width="100px"></asp:TextBox>
        追加説明<asp:DropDownList ID="DropDownList50024" runat="server">
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
            <asp:ListItem Value="追撃1">追撃1:単体</asp:ListItem>
            <asp:ListItem Value="追撃2">追撃2:全体</asp:ListItem>
            <asp:ListItem Value="追撃3">追撃3:単体と全体</asp:ListItem>
            <asp:ListItem Value="追撃4">追撃4:PT全体に単体20%付与</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>ガッツ付与1回</asp:ListItem>
            <asp:ListItem>3ターン</asp:ListItem>
            <asp:ListItem>PT全体</asp:ListItem>
            <asp:ListItem>自身</asp:ListItem>
        </asp:DropDownList>
        <p>
            <asp:CheckBox ID="CheckBox50001" runat="server" Text="編集モード" />
            <asp:Button ID="Button50001" runat="server" Height="75px" Text="登録" Width="200px" OnClick="Button50001_Click" />
            　　　　　　　　　<asp:Button ID="Button50002" runat="server" Height="75px" OnClick="Button50002_Click" Text="入力クリア" Width="200px" />
            <a href="#DBtop">△</a>
        </p>
    </div>
</asp:Content>