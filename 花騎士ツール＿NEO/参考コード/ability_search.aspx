<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ability_search.aspx.cs" Inherits="Ability_search" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>花騎士アビリティ検索</title>
    <script type="text/javascript">
  var appInsights=window.appInsights||function(a){
    function b(a){c[a]=function(){var b=arguments;c.queue.push(function(){c[a].apply(c,b)})}}var c={config:a},d=document,e=window;setTimeout(function(){var b=d.createElement("script");b.src=a.url||"https://az416426.vo.msecnd.net/scripts/a/ai.0.js",d.getElementsByTagName("script")[0].parentNode.appendChild(b)});try{c.cookie=d.cookie}catch(a){}c.queue=[];for(var f=["Event","Exception","Metric","PageView","Trace","Dependency"];f.length;)b("track"+f.pop());if(b("setAuthenticatedUserContext"),b("clearAuthenticatedUserContext"),b("startTrackEvent"),b("stopTrackEvent"),b("startTrackPage"),b("stopTrackPage"),b("flush"),!a.disableExceptionTracking){f="onerror",b("_"+f);var g=e[f];e[f]=function(a,b,d,e,h){var i=g&&g(a,b,d,e,h);return!0!==i&&c["_"+f](a,b,d,e,h),i}}return c
    }({
        instrumentationKey:"ebbb90e3-81c3-4489-bbaa-48b333be9cf2"
    });
    
  window.appInsights=appInsights,appInsights.queue&&0===appInsights.queue.length&&appInsights.trackPageView();
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Large" Text="〇花騎士アビリティ検索"></asp:Label>
        </p>
        <p>
            検索設定項目</p>
        <p>
            〇検索フィルタ</p>
        <p>
            <asp:Label ID="Label10" runat="server" Text="レアリティ" Width="90px"></asp:Label>
            <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="☆6" />
            <asp:CheckBox ID="CheckBox2" runat="server" Text="☆5" Enabled="False" />
            <asp:CheckBox ID="CheckBox3" runat="server" Text="その他一同" Enabled="False" />
        </p>
        <p>
            <asp:Label ID="Label11" runat="server" Text="属性" Width="90px"></asp:Label>
            <asp:CheckBox ID="CheckBox5" runat="server" Checked="True" Text="斬" />
            <asp:CheckBox ID="CheckBox6" runat="server" Checked="True" Text="打" />
            <asp:CheckBox ID="CheckBox7" runat="server" Checked="True" Text="突" />
            <asp:CheckBox ID="CheckBox8" runat="server" Checked="True" Text="魔" />
        </p>
        <p>
            <asp:Label ID="Label13" runat="server" Width="90px">スキル種</asp:Label>
            <asp:CheckBox ID="CheckBox9" runat="server" Checked="True" Text="全" />
            <asp:CheckBox ID="CheckBox10" runat="server" Checked="True" Text="２" />
            <asp:CheckBox ID="CheckBox11" runat="server" Checked="True" Text="変" />
            <asp:CheckBox ID="CheckBox12" runat="server" Checked="True" Text="複" />
            <asp:CheckBox ID="CheckBox13" runat="server" Checked="True" Text="吸" />
            <asp:CheckBox ID="CheckBox14" runat="server" Checked="True" Text="単" />
        &nbsp;<asp:Button ID="Button2" runat="server" Height="30px" OnClick="Button2_Click" Text="全チェック" Width="120px" />
            &nbsp;<asp:Button ID="Button3" runat="server" Height="30px" OnClick="Button3_Click" Text="チェック外し" Width="120px" />
        </p>
        <p>
            〇検索アビリティの設定</p>
        <p>
            検索アビリティ1</p>
        <asp:DropDownList ID="DropDownList1" runat="server" Width="300px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="選択無し">選択無し</asp:ListItem>
            <asp:ListItem>スキル発動率1.2倍上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1.65倍上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメージ増加</asp:ListItem>
            <asp:ListItem>クリティカル率上昇</asp:ListItem>
            <asp:ListItem>クリティカルダメージ増加</asp:ListItem>
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターン毎攻撃力上昇</asp:ListItem>
            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
            <asp:ListItem Value="ボスに対して攻撃力上昇"></asp:ListItem>
            <asp:ListItem>ダメージ増加</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>再行動</asp:ListItem>
            <asp:ListItem>防御力・ダメージ軽減率上昇</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>命中率低下</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>ダメージ無効化</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
            <asp:ListItem>シャインクリスタルドロップ率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>移動力増加</asp:ListItem>
            <asp:ListItem>1ターン目系</asp:ListItem>
            <asp:ListItem>スキル：全体</asp:ListItem>
            <asp:ListItem>スキル：2体</asp:ListItem>
            <asp:ListItem>スキル：変則</asp:ListItem>
            <asp:ListItem>スキル：複数回</asp:ListItem>
            <asp:ListItem>スキル：吸収</asp:ListItem>
            <asp:ListItem>スキル：単体</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Text="数値指定の場合は半角で入力"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Width="50px" Enabled="False" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="以上" Width="50px"></asp:Label>
        <p>
            検索アビリティ2</p>
        <asp:DropDownList ID="DropDownList2" runat="server" Width="300px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="選択無し">選択無し</asp:ListItem>
            <asp:ListItem>スキル発動率1.2倍上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1.65倍上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメージ増加</asp:ListItem>
            <asp:ListItem>クリティカル率上昇</asp:ListItem>
            <asp:ListItem>クリティカルダメージ増加</asp:ListItem>
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターン毎攻撃力上昇</asp:ListItem>
            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
            <asp:ListItem Value="ボスに対して攻撃力上昇"></asp:ListItem>
            <asp:ListItem>ダメージ増加</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>再行動</asp:ListItem>
            <asp:ListItem>防御力・ダメージ軽減率上昇</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>命中率低下</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>ダメージ無効化</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
            <asp:ListItem>シャインクリスタルドロップ率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Text="数値指定の場合は半角で入力"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" Width="50px" Enabled="False"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="以上" Width="50px"></asp:Label>
        <p>
            検索アビリティ3</p>
        <asp:DropDownList ID="DropDownList3" runat="server" Width="300px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="選択無し">選択無し</asp:ListItem>
            <asp:ListItem>スキル発動率1.2倍上昇</asp:ListItem>
            <asp:ListItem>スキル発動率1.65倍上昇</asp:ListItem>
            <asp:ListItem>スキル発動率上昇</asp:ListItem>
            <asp:ListItem>スキルダメージ増加</asp:ListItem>
            <asp:ListItem>クリティカル率上昇</asp:ListItem>
            <asp:ListItem>クリティカルダメージ増加</asp:ListItem>
            <asp:ListItem>攻撃力上昇</asp:ListItem>
            <asp:ListItem>ターン毎攻撃力上昇</asp:ListItem>
            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
            <asp:ListItem Value="ボスに対して攻撃力上昇"></asp:ListItem>
            <asp:ListItem>ダメージ増加</asp:ListItem>
            <asp:ListItem>回避</asp:ListItem>
            <asp:ListItem>反撃</asp:ListItem>
            <asp:ListItem>再行動</asp:ListItem>
            <asp:ListItem>防御力・ダメージ軽減率上昇</asp:ListItem>
            <asp:ListItem>攻撃力低下</asp:ListItem>
            <asp:ListItem>スキル発動率低下</asp:ListItem>
            <asp:ListItem>命中率低下</asp:ListItem>
            <asp:ListItem>追撃</asp:ListItem>
            <asp:ListItem>ダメージ無効化</asp:ListItem>
            <asp:ListItem>属性付与</asp:ListItem>
            <asp:ListItem>HP回復</asp:ListItem>
            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
            <asp:ListItem>シャインクリスタルドロップ率上昇</asp:ListItem>
            <asp:ListItem>光ゲージ充填</asp:ListItem>
            <asp:ListItem>移動力増加</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label7" runat="server" Text="数値指定の場合は半角で入力"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" Width="50px" Enabled="False"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Text="以上" Width="50px"></asp:Label>
        <br />
        <p>
            <asp:CheckBox ID="CheckBox4" runat="server" Text="OR検索（上級者向け）" Enabled="False" Visible="False" />
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Height="60px" OnClick="Button1_Click" Text="検索" Width="120px" />
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="名前" />
                <asp:BoundField DataField="Rarity" HeaderText="レア" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="ATT" HeaderText="属性">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="Abi1" HeaderText="アビリティ1" >
                <ItemStyle Width="220px" />
                </asp:BoundField>
                <asp:BoundField DataField="Abi2" HeaderText="アビリティ2" >
                <ItemStyle Width="220px" />
                </asp:BoundField>
                <asp:BoundField DataField="Abi3" HeaderText="アビリティ3" >
                <ItemStyle Width="220px" />
                </asp:BoundField>
                <asp:BoundField DataField="Abi4" HeaderText="アビリティ4" >
                <ItemStyle Width="220px" />
                </asp:BoundField>
                <asp:BoundField DataField="SType" HeaderText="スキル" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SRatioRev" HeaderText="ダメ倍率" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        </form>
        メインページは<a href="fkg_select.aspx">花騎士編成支援ツール</a>です。
    <p>
        <a href="http://pc-play.games.dmm.com/play/flower/" target="_blank">『フラワーナイトガール』</a>（C) DMMゲームズ</p>
    <p>
        当サイトのデータに関しては、<a href="http://xn--eckq7fg8cygsa1a1je.xn--wiki-4i9hs14f.com/" target="_blank">フラワーナイトガール攻略まとめwiki</a>様を参考にさせていただいております。</p>
</body>
</html>
