<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fkg_select.aspx.cs" Inherits="Fkg_select" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="description" content="DMMゲーム　フラワーナイトガール（花騎士）のパーティー編成シミュレーターです。スキル発動率、デバフ効果等計算します。アビリティ検索機能、能力値ランキングもあります。"/>
    <meta name="viewport" content="width=device-width,initial-scale=1"/>
    <title>花騎士編成支援ツール</title>
    <script src="JavaScript.js" type="text/javascript">
    </script>
    <script type="text/javascript">
        var appInsights = window.appInsights || function (a) {
            function b(a) { c[a] = function () { var b = arguments; c.queue.push(function () { c[a].apply(c, b) }) } } var c = { config: a }, d = document, e = window; setTimeout(function () { var b = d.createElement("script"); b.src = a.url || "https://az416426.vo.msecnd.net/scripts/a/ai.0.js", d.getElementsByTagName("script")[0].parentNode.appendChild(b) }); try { c.cookie = d.cookie } catch (a) { } c.queue = []; for (var f = ["Event", "Exception", "Metric", "PageView", "Trace", "Dependency"]; f.length;)b("track" + f.pop()); if (b("setAuthenticatedUserContext"), b("clearAuthenticatedUserContext"), b("startTrackEvent"), b("stopTrackEvent"), b("startTrackPage"), b("stopTrackPage"), b("flush"), !a.disableExceptionTracking) { f = "onerror", b("_" + f); var g = e[f]; e[f] = function (a, b, d, e, h) { var i = g && g(a, b, d, e, h); return !0 !== i && c["_" + f](a, b, d, e, h), i } } return c
        }({
            instrumentationKey: "ebbb90e3-81c3-4489-bbaa-48b333be9cf2"
        });

        window.appInsights = appInsights, appInsights.queue && 0 === appInsights.queue.length && appInsights.trackPageView();
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="全読込" Visible="False" />
            <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Debug" Visible="False" />
            <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="デバッグ用選択" ClientIDMode="Static" EnableTheming="True" OnCheckedChanged="CheckBox1_CheckedChanged" CausesValidation="True" Visible="False" />
            <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="javascript起動" Visible="False" />
            <br />
            <br />
            製作者 <a href="https://twitter.com/chiyokanemaru" target="_blank">@chiyokanemaru</a>
            <br />
            （ご意見ご要望については上記ツイッターアカウントにお願いします。）<br />
&nbsp; 現在開発中であり、花騎士データも☆6までしか入力されておりません。<br />
            <br />
            目次<br />
&nbsp; <a href="#fkg_select">〇花騎士編成支援ツール</a><br />
&nbsp;&nbsp;&nbsp; メインコンテンツです。PT編成時の各花騎士の持つアビリティにより変化する各種確率を計算して表示する編成シミュレータです。<br />
            <br />
            おまけ<br />
            &nbsp;
            <a href="fkg_ranking.aspx" target="_blank">〇花騎士ランキング</a> 花騎士の能力値を順位表示します。(リンク先に飛びます）<br />
            
&nbsp; <a href="ability_search.aspx" target="_blank">〇花騎士アビリティ検索</a> お迎えする花騎士の選定にどうぞ。(リンク先に飛びます）<br />
            
            &nbsp;
            <!--
            〇アンプルゥ簡易計算機<br />
&nbsp;&nbsp;&nbsp; すぐ下のTextBoxです。その名の通り、必要アンプルゥ数を算出します。アンプルゥ・上には未対応です。<br />
            &nbsp;&nbsp;&nbsp;
            <br />
            　 数値入力後enter押して下さい。<br />
            <p>
                <asp:Label ID="Label91" runat="server" Width="50px"></asp:Label>
                <asp:Label ID="Label85" runat="server" Text="増分値" Width="80px"></asp:Label>
                <asp:Label ID="Label86" runat="server" Text="投入数" Width="80px"></asp:Label>
                <asp:Label ID="Label87" runat="server" Text="フル必要数" Width="80px"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label88" runat="server" Text="HP" Width="50px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" Width="80px">3000</asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged" Width="80px">100</asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="TextBox3_TextChanged" Width="80px">0</asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label89" runat="server" Text="攻撃" Width="50px"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="TextBox4_TextChanged" Width="80px">1000</asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="TextBox5_TextChanged" Width="80px">100</asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server" OnTextChanged="TextBox6_TextChanged" Width="80px">0</asp:TextBox>
            </p>
            <asp:Label ID="Label90" runat="server" Text="防御" Width="50px"></asp:Label>
            <asp:TextBox ID="TextBox7" runat="server" OnTextChanged="TextBox7_TextChanged" Width="80px">400</asp:TextBox>
            <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="TextBox8_TextChanged" Width="80px">100</asp:TextBox>
            <asp:TextBox ID="TextBox9" runat="server" OnTextChanged="TextBox9_TextChanged" Width="80px">0</asp:TextBox>
                -->
            <br />
            <p id = "fkg_select">
                <asp:Label ID="Label178" runat="server" Font-Bold="True" Font-Size="X-Large" Text="〇花騎士編成支援ツール"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label310" runat="server" Font-Bold="True" Font-Size="Small" Text="このサイトの使い方" Font-Italic="True" Font-Underline="True" ForeColor="#0099FF"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label311" runat="server" Font-Bold="True" Font-Size="Small" Text="以下のフィルタで花騎士を限定し、下の赤い読込みボタンを押すと、横のボックス内にフィルタされた花騎士名がリストとなって現れます。" Font-Italic="True" Font-Underline="True" ForeColor="#0099FF"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label312" runat="server" Font-Bold="True" Font-Size="Small" Text="次に花騎士を選択し、すぐ下の計算開始ボタンを押すと、各種発動率等を計算、表示します。" Font-Italic="True" Font-Underline="True" ForeColor="#0099FF"></asp:Label>
            </p>
            <p>
            <asp:Label ID="Label315" runat="server" Width="220px" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Black"></asp:Label>
            <asp:Label ID="Label316" runat="server" Width="175px" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Black" Font-Italic="True">花騎士1</asp:Label>
            <asp:Label ID="Label317" runat="server" Width="175px" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Black" Font-Italic="True">花騎士2</asp:Label>
            <asp:Label ID="Label318" runat="server" Width="175px" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Black" Font-Italic="True">花騎士3</asp:Label>
            <asp:Label ID="Label319" runat="server" Width="175px" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Black" Font-Italic="True">花騎士4</asp:Label>
            <asp:Label ID="Label320" runat="server" Width="175px" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Black" Font-Italic="True">花騎士5</asp:Label>
            </p>
            <p>
            <asp:Label ID="Label313" runat="server" Width="180px" Font-Bold="True" Font-Size="Large" Font-Underline="True" ForeColor="Red">各種フィルタ群</asp:Label>
            </p>
            </div>
       <!--
        <p>
            <asp:Label ID="Label40" runat="server" Text="選択フィルタ" Width="180px"></asp:Label>
            ☆<asp:DropDownList ID="DropDownList1" runat="server" Height="21px" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList2" runat="server" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp; ☆<asp:DropDownList ID="DropDownList4" runat="server" Height="21px" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList5" runat="server" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp; ☆<asp:DropDownList ID="DropDownList7" runat="server" Height="21px" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList8" runat="server" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp; ☆<asp:DropDownList ID="DropDownList10" runat="server" Height="21px" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList11" runat="server" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp; ☆<asp:DropDownList ID="DropDownList13" runat="server" Height="21px" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            属性<asp:DropDownList ID="DropDownList14" runat="server" Width="50px">
                <asp:ListItem Value="0">全</asp:ListItem>
                <asp:ListItem>斬</asp:ListItem>
                <asp:ListItem>打</asp:ListItem>
                <asp:ListItem>突</asp:ListItem>
                <asp:ListItem>魔</asp:ListItem>
            </asp:DropDownList>
        </p>
           -->
        <p>
            <asp:Label ID="Label151" runat="server" Width="180px" Font-Bold="True">〇レアリティ</asp:Label>
            <asp:RadioButton ID="RadioButton101" runat="server" Checked="True" GroupName="group6" Text="全" />
            <asp:RadioButton ID="RadioButton102" runat="server" GroupName="group6" Text="6" />
            <asp:RadioButton ID="RadioButton103" runat="server" Enabled="False" GroupName="group6" Text="5" />
            <asp:RadioButton ID="RadioButton104" runat="server" Enabled="False" GroupName="group6" Text="他" />
            <asp:Label ID="Label159" runat="server" Width="25px"></asp:Label>
            <asp:RadioButton ID="RadioButton105" runat="server" Text="全" Checked="True" GroupName="group7" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton106" runat="server" Text="6" GroupName="group7" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton107" runat="server" Text="5" Enabled="False" GroupName="group7" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton108" runat="server" Text="他" Enabled="False" GroupName="group7" ForeColor="#996600" />
            <asp:Label ID="Label160" runat="server" Width="25px"></asp:Label>
            <asp:RadioButton ID="RadioButton109" runat="server" Text="全" Checked="True" GroupName="group8" />
            <asp:RadioButton ID="RadioButton110" runat="server" Text="6" GroupName="group8" />
            <asp:RadioButton ID="RadioButton111" runat="server" Text="5" GroupName="group8" Enabled="False" />
            <asp:RadioButton ID="RadioButton112" runat="server" Text="他" GroupName="group8" Enabled="False" />
            <asp:Label ID="Label161" runat="server" Width="25px"></asp:Label>
            <asp:RadioButton ID="RadioButton113" runat="server" Text="全" GroupName="group9" Checked="True" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton114" runat="server" Text="6" GroupName="group9" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton115" runat="server" Text="5" GroupName="group9" Enabled="False" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton116" runat="server" Text="他" GroupName="group9" Enabled="False" ForeColor="#996600" />
            <asp:Label ID="Label162" runat="server" Width="25px"></asp:Label>
            <asp:RadioButton ID="RadioButton117" runat="server" Text="全" GroupName="group10" Checked="True" />
            <asp:RadioButton ID="RadioButton118" runat="server" Text="6" GroupName="group10" />
            <asp:RadioButton ID="RadioButton119" runat="server" Text="5" GroupName="group10" Enabled="False" />
            <asp:RadioButton ID="RadioButton120" runat="server" Text="他" GroupName="group10" Enabled="False" />
        </p>
        <p>
            <asp:Label ID="Label148" runat="server" Width="180px" Font-Bold="True">〇属性</asp:Label>
            <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="group1" Text="全属性" Width="85px" />
            <asp:Label ID="Label150" runat="server" Width="90px"></asp:Label>
            <asp:RadioButton ID="RadioButton6" runat="server" Checked="True" GroupName="group2" Text="全属性" Width="85px" ForeColor="#996600" />
            <asp:Label ID="Label152" runat="server" Width="90px"></asp:Label>
            <asp:RadioButton ID="RadioButton11" runat="server" Checked="True" GroupName="group3" Text="全属性" Width="85px" />
            <asp:Label ID="Label154" runat="server" Width="90px"></asp:Label>
            <asp:RadioButton ID="RadioButton16" runat="server" Checked="True" GroupName="group4" Text="全属性" Width="85px" ForeColor="#996600" />
            <asp:Label ID="Label155" runat="server" Width="90px"></asp:Label>
            <asp:RadioButton ID="RadioButton21" runat="server" Checked="True" GroupName="group5" Text="全属性" Width="85px" />
        </p>
        <p>
            <asp:Label ID="Label146" runat="server" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="group1" Text="斬" Width="40px" />
            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="group1" Text="打" Width="40px" />
            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="group1" Text="突" Width="40px" />
            <asp:RadioButton ID="RadioButton5" runat="server" GroupName="group1" Text="魔" Width="40px" />
            <asp:RadioButton ID="RadioButton7" runat="server" GroupName="group2" Text="斬" Width="40px" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton8" runat="server" GroupName="group2" Text="打" Width="40px" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton9" runat="server" GroupName="group2" Text="突" Width="40px" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton10" runat="server" GroupName="group2" Text="魔" Width="40px" ForeColor="#996600" />
            <asp:Label ID="Label157" runat="server" Width="5px"></asp:Label>
            <asp:RadioButton ID="RadioButton12" runat="server" GroupName="group3" Text="斬" Width="40px" />
            <asp:RadioButton ID="RadioButton13" runat="server" GroupName="group3" Text="打" Width="40px" />
            <asp:RadioButton ID="RadioButton14" runat="server" GroupName="group3" Text="突" Width="40px" />
            <asp:RadioButton ID="RadioButton15" runat="server" GroupName="group3" Text="魔" Width="40px" />
            <asp:RadioButton ID="RadioButton17" runat="server" GroupName="group4" Text="斬" Width="40px" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton18" runat="server" GroupName="group4" Text="打" Width="40px" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton19" runat="server" GroupName="group4" Text="突" Width="40px" ForeColor="#996600" />
            <asp:RadioButton ID="RadioButton20" runat="server" GroupName="group4" Text="魔" Width="40px" ForeColor="#996600" />
            <asp:Label ID="Label158" runat="server" Width="5px"></asp:Label>
            <asp:RadioButton ID="RadioButton22" runat="server" GroupName="group5" Text="斬" Width="40px" />
            <asp:RadioButton ID="RadioButton23" runat="server" GroupName="group5" Text="打" Width="40px" />
            <asp:RadioButton ID="RadioButton24" runat="server" GroupName="group5" Text="突" Width="40px" />
            <asp:RadioButton ID="RadioButton25" runat="server" GroupName="group5" Text="魔" Width="40px" />
        </p>
        <p>
            <asp:Label ID="Label295" runat="server" Width="180px" Font-Bold="True">〇スキルタイプ</asp:Label>
            <asp:Button ID="Button11" runat="server" Font-Size="X-Small" Text="全選択" Width="40px" OnClick="Button11_Click" />
            <asp:Button ID="Button12" runat="server" Font-Size="X-Small" Text="全消" Width="40px" OnClick="Button12_Click" />
            <asp:CheckBox ID="CheckBox11" runat="server" Text="全" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox12" runat="server" Text="2" Width="40px" Checked="True" />
            <asp:Button ID="Button13" runat="server" Font-Size="X-Small" Text="全選択" Width="40px" ForeColor="#996600" OnClick="Button13_Click" />
            <asp:Button ID="Button14" runat="server" Font-Size="X-Small" Text="全消" Width="40px" ForeColor="#996600" OnClick="Button14_Click" />
            <asp:CheckBox ID="CheckBox21" runat="server" Text="全" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox22" runat="server" Text="2" ForeColor="#996600" Width="40px" Checked="True" />
            <asp:Button ID="Button15" runat="server" Font-Size="X-Small" Text="全選択" Width="40px" OnClick="Button15_Click" />
            <asp:Button ID="Button16" runat="server" Font-Size="X-Small" Text="全消" Width="40px" OnClick="Button16_Click" />
            <asp:CheckBox ID="CheckBox31" runat="server" Text="全" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox32" runat="server" Text="2" Width="40px" Checked="True" />
            <asp:Button ID="Button17" runat="server" Font-Size="X-Small" Text="全選択" Width="40px" ForeColor="#996600" OnClick="Button17_Click" />
            <asp:Button ID="Button18" runat="server" Font-Size="X-Small" Text="全消" Width="40px" ForeColor="#996600" OnClick="Button18_Click" />
            <asp:CheckBox ID="CheckBox41" runat="server" Text="全" Width="40px" ForeColor="#996600" Checked="True" Height="44px" />
            <asp:CheckBox ID="CheckBox42" runat="server" Text="2" ForeColor="#996600" Width="40px" Checked="True" />
            <asp:Button ID="Button19" runat="server" Font-Size="X-Small" Text="全選択" Width="40px" OnClick="Button19_Click" />
            <asp:Button ID="Button20" runat="server" Font-Size="X-Small" Text="全消" Width="40px" OnClick="Button20_Click" />
            <asp:CheckBox ID="CheckBox51" runat="server" Text="全" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox52" runat="server" Text="2" Width="40px" Checked="True" />
        </p>
        <p>
            <asp:Label ID="Label296" runat="server" Width="180px"></asp:Label>
            <asp:CheckBox ID="CheckBox13" runat="server" Text="変" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox14" runat="server" Text="吸" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox15" runat="server" Text="複" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox16" runat="server" Text="単" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox23" runat="server" Text="変" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox24" runat="server" Text="吸" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox25" runat="server" Text="複" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox26" runat="server" Text="単" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox33" runat="server" Text="変" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox34" runat="server" Text="吸" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox35" runat="server" Text="複" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox36" runat="server" Text="単" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox43" runat="server" Text="変" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox44" runat="server" Text="吸" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox45" runat="server" Text="複" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox46" runat="server" Text="単" Width="40px" ForeColor="#996600" Checked="True" />
            <asp:CheckBox ID="CheckBox53" runat="server" Text="変" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox54" runat="server" Text="吸" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox55" runat="server" Text="複" Width="40px" Checked="True" />
            <asp:CheckBox ID="CheckBox56" runat="server" Text="単" Width="40px" Checked="True" />
        </p>
        <p>
            <asp:Label ID="Label299" runat="server" Width="180px" Font-Bold="True">〇アビリティ1</asp:Label>
            <asp:DropDownList ID="DropDownList21" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList22" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static" ForeColor="#996600">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList23" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList24" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static" ForeColor="#996600">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList25" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="Label305" runat="server" Width="180px" Font-Bold="True">〇アビリティ2</asp:Label>
            <asp:DropDownList ID="DropDownList26" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList27" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static" ForeColor="#996600">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList28" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList29" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static" ForeColor="#996600">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList30" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="Label308" runat="server" Width="180px" Font-Bold="True">〇アビリティ3</asp:Label>
            <asp:DropDownList ID="DropDownList31" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList32" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static" ForeColor="#996600">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList33" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList34" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static" ForeColor="#996600">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList35" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="175px" ClientIDMode="Static">
                <asp:ListItem>未選択</asp:ListItem>
                <asp:ListItem>1ターン目系</asp:ListItem>
                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                <asp:ListItem>クリ率上昇</asp:ListItem>
                <asp:ListItem>クリダメ上昇</asp:ListItem>
                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                <asp:ListItem>ダメージ増加</asp:ListItem>
                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                <asp:ListItem>回避</asp:ListItem>
                <asp:ListItem>反撃</asp:ListItem>
                <asp:ListItem>防御</asp:ListItem>
                <asp:ListItem>再行動</asp:ListItem>
                <asp:ListItem>バリア</asp:ListItem>
                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                <asp:ListItem>光ゲージ充填</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="Label314" runat="server" Width="180px" Font-Bold="True" Font-Size="Large" Font-Underline="True" ForeColor="Red">クイックフィルタ</asp:Label>
        </p>
        <p>
            <asp:Label ID="Label297" runat="server" Width="180px" Font-Bold="True" Font-Size="Small">（チェック入れるとオン）</asp:Label>
            <asp:CheckBox ID="CheckBox111" runat="server" Text="1.65倍" Width="90px" />
            <asp:CheckBox ID="CheckBox112" runat="server" Text="昇華" Width="85px" />
            <asp:CheckBox ID="CheckBox121" runat="server" Text="1.65倍" Width="90px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox122" runat="server" Text="昇華" Width="85px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox131" runat="server" Text="1.65倍" Width="90px" />
            <asp:CheckBox ID="CheckBox132" runat="server" Text="昇華" Width="85px" />
            <asp:CheckBox ID="CheckBox141" runat="server" Text="1.65倍" Width="90px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox142" runat="server" Text="昇華" Width="85px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox151" runat="server" Text="1.65倍" Width="90px" />
            <asp:CheckBox ID="CheckBox152" runat="server" Text="昇華" Width="85px" />
        </p>
        <p>
            <asp:Label ID="Label309" runat="server" Width="180px" Font-Italic="True">〇属性付与</asp:Label>
            <asp:CheckBox ID="CheckBox211" runat="server" Text="斬" Width="40px" />
            <asp:CheckBox ID="CheckBox212" runat="server" Text="打" Width="40px" />
            <asp:CheckBox ID="CheckBox213" runat="server" Text="突" Width="40px" />
            <asp:CheckBox ID="CheckBox214" runat="server" Text="魔" Width="40px" />
            <asp:CheckBox ID="CheckBox221" runat="server" Text="斬" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox222" runat="server" Text="打" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox223" runat="server" Text="突" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox224" runat="server" Text="魔" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox231" runat="server" Text="斬" Width="40px" />
            <asp:CheckBox ID="CheckBox232" runat="server" Text="打" Width="40px" />
            <asp:CheckBox ID="CheckBox233" runat="server" Text="突" Width="40px" />
            <asp:CheckBox ID="CheckBox234" runat="server" Text="魔" Width="40px" />
            <asp:CheckBox ID="CheckBox241" runat="server" Text="斬" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox242" runat="server" Text="打" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox243" runat="server" Text="突" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox244" runat="server" Text="魔" Width="40px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox251" runat="server" Text="斬" Width="40px" />
            <asp:CheckBox ID="CheckBox252" runat="server" Text="打" Width="40px" />
            <asp:CheckBox ID="CheckBox253" runat="server" Text="突" Width="40px" />
            <asp:CheckBox ID="CheckBox254" runat="server" Text="魔" Width="40px" />
        </p>
        <p>
            <asp:Label ID="Label302" runat="server" Width="90px" Font-Italic="True">〇デバフ</asp:Label>
            <asp:Button ID="Button21" runat="server" Font-Size="X-Small" Text="全選択" Width="50px" OnClick="Button21_Click" />
            <asp:Button ID="Button22" runat="server" Font-Size="X-Small" Text="全消" Width="40px" OnClick="Button22_Click" />
            <asp:CheckBox ID="CheckBox113" runat="server" Text="攻撃力低下" Width="175px" />
            <asp:CheckBox ID="CheckBox123" runat="server" Text="攻撃力低下" Width="175px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox133" runat="server" Text="攻撃力低下" Width="175px" />
            <asp:CheckBox ID="CheckBox143" runat="server" Text="攻撃力低下" Width="175px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox153" runat="server" Text="攻撃力低下" Width="175px" />
        </p>
        <p>
            <asp:Label ID="Label303" runat="server" Width="90px" Font-Italic="True">対象人数</asp:Label>
            <asp:Button ID="Button23" runat="server" Font-Size="X-Small" Text="全選択" Width="50px" OnClick="Button23_Click" />
            <asp:Button ID="Button24" runat="server" Font-Size="X-Small" Text="全消" Width="40px" OnClick="Button24_Click" />
            <asp:Label ID="Label304" runat="server" Width="40px"></asp:Label>
            <asp:CheckBox ID="CheckBox114" runat="server" Text="3人以上" Width="175px" />
            <asp:CheckBox ID="CheckBox124" runat="server" Text="3人以上" Width="175px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox134" runat="server" Text="3人以上" Width="175px" />
            <asp:CheckBox ID="CheckBox144" runat="server" Text="3人以上" Width="175px" ForeColor="#996600" />
            <asp:CheckBox ID="CheckBox154" runat="server" Text="3人以上" Width="175px" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="Label41" runat="server" Text="花騎士" Width="70px" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
            &nbsp;
            <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="全員読込" Width="70px" ForeColor="Red" Font-Bold="True" />
&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="読込" Width="40px" ForeColor="Red" Font-Bold="True" />
            <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="135px" ClientIDMode="Static" BackColor="White">
            </asp:DropDownList>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="読込" Width="40px" ForeColor="Red" Font-Bold="True" />
            <asp:DropDownList ID="DropDownList6" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="135px" ClientIDMode="Static" BackColor="White">
            </asp:DropDownList>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="読込" Width="40px" ForeColor="Red" Font-Bold="True" />
            <asp:DropDownList ID="DropDownList9" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="135px" ClientIDMode="Static" BackColor="White">
            </asp:DropDownList>
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="読込" Width="40px" ForeColor="Red" Font-Bold="True" />
            <asp:DropDownList ID="DropDownList12" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="135px" ClientIDMode="Static" BackColor="White">
            </asp:DropDownList>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="読込" Width="40px" ForeColor="Red" Font-Bold="True" />
            <asp:DropDownList ID="DropDownList15" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" OnTextChanged="DropDownList3_TextChanged" Width="135px" ClientIDMode="Static" BackColor="White">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="Label79" runat="server" Text="スキルレベル" Width="180px"></asp:Label>
            <asp:Label ID="Label80" runat="server" Width="60px"></asp:Label>
            <asp:DropDownList ID="DropDownList16" runat="server" Width="55px">
                <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label81" runat="server" Width="120px"></asp:Label>
            <asp:DropDownList ID="DropDownList17" runat="server" Width="55px">
                <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label82" runat="server" Width="120px"></asp:Label>
            <asp:DropDownList ID="DropDownList18" runat="server" Width="55px">
                <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label83" runat="server" Width="120px"></asp:Label>
            <asp:DropDownList ID="DropDownList19" runat="server" Width="55px">
                <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label84" runat="server" Width="120px"></asp:Label>
            <asp:DropDownList ID="DropDownList20" runat="server" Width="55px">
                <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="計算開始ボタン" Height="60px"  ForeColor="Red" Font-Bold="True" Font-Size="Large" />
        </p>
        <p>
            <asp:Label ID="Label92" runat="server" Text="1.2倍持ち" Width="120px"></asp:Label>
            <asp:Label ID="Label93" runat="server" Text="0人" Width="60px"></asp:Label>
            <asp:Label ID="Label107" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label97" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label98" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label99" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label100" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label101" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label94" runat="server" Text="1.65倍持ち" Width="120px"></asp:Label>
            <asp:Label ID="Label95" runat="server" Text="0人" Width="60px"></asp:Label>
            <asp:Label ID="Label108" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label102" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label103" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label104" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label105" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label106" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label163" runat="server" Text="属性付与（有無のみ）" Width="185px"></asp:Label>
            <asp:Label ID="Label165" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label170" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label171" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label172" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label173" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label174" runat="server" Width="175px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label254" runat="server" Text="斬" Width="120px" Font-Underline="False" ForeColor="Red" Height="25px"></asp:Label>
            <asp:Label ID="Label291" runat="server" Text="0人" Width="60px"></asp:Label>
            <asp:Label ID="Label281" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label261" runat="server" Width="175px" ForeColor="Red"></asp:Label>
            <asp:Label ID="Label262" runat="server" Width="175px" ForeColor="Red"></asp:Label>
            <asp:Label ID="Label263" runat="server" Width="175px" ForeColor="Red"></asp:Label>
            <asp:Label ID="Label264" runat="server" Width="175px" ForeColor="Red"></asp:Label>
            <asp:Label ID="Label265" runat="server" Width="175px" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label255" runat="server" Text="打" Width="120px" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label292" runat="server" Text="0人" Width="60px"></asp:Label>
            <asp:Label ID="Label282" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label266" runat="server" Width="175px" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label267" runat="server" Width="175px" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label268" runat="server" Width="175px" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label269" runat="server" Width="175px" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label270" runat="server" Width="175px" ForeColor="Blue"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label256" runat="server" Text="突" Width="120px" ForeColor="#006600"></asp:Label>
            <asp:Label ID="Label293" runat="server" Text="0人" Width="60px"></asp:Label>
            <asp:Label ID="Label283" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label271" runat="server" Width="175px" ForeColor="#006600"></asp:Label>
            <asp:Label ID="Label272" runat="server" Width="175px" ForeColor="#006600"></asp:Label>
            <asp:Label ID="Label273" runat="server" Width="175px" ForeColor="#006600"></asp:Label>
            <asp:Label ID="Label274" runat="server" Width="175px" ForeColor="#006600"></asp:Label>
            <asp:Label ID="Label275" runat="server" Width="175px" ForeColor="#006600"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label257" runat="server" Text="魔" Width="120px" ForeColor="#9900CC"></asp:Label>
            <asp:Label ID="Label294" runat="server" Text="0人" Width="60px"></asp:Label>
            <asp:Label ID="Label284" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label276" runat="server" Width="175px" ForeColor="#9900CC"></asp:Label>
            <asp:Label ID="Label277" runat="server" Width="175px" ForeColor="#9900CC"></asp:Label>
            <asp:Label ID="Label278" runat="server" Width="175px" ForeColor="#9900CC"></asp:Label>
            <asp:Label ID="Label279" runat="server" Width="175px" ForeColor="#9900CC"></asp:Label>
            <asp:Label ID="Label280" runat="server" Width="175px" ForeColor="#9900CC"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label96" runat="server" Text="スキル発動率" Width="180px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label53" runat="server" Text="1T目" Width="180px"></asp:Label>
            <asp:Label ID="Label109" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label54" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label55" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label56" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label57" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label58" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label42" runat="server" Text="2T目以降" Width="180px"></asp:Label>
            <asp:Label ID="Label110" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label1" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label2" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label3" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label4" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label5" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label59" runat="server" Text="2T目" Width="180px" Visible="False"></asp:Label>
            <asp:Label ID="Label60" runat="server" Text="☆6では要らない？" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label61" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label62" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label63" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label64" runat="server" Width="175px" Visible="False"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label65" runat="server" Text="3T目" Width="180px" Visible="False"></asp:Label>
            <asp:Label ID="Label111" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label66" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label67" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label68" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label69" runat="server" Width="175px" Visible="False"></asp:Label>
            <asp:Label ID="Label70" runat="server" Width="115px" Visible="False"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label43" runat="server" Text="スキルダメージ" Width="180px"></asp:Label>
            <asp:Label ID="Label112" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label6" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label7" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label8" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label9" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label10" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label125" runat="server" Text="クリ発生率" Width="180px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label44" runat="server" Text="1T目" Width="180px"></asp:Label>
            <asp:Label ID="Label113" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label11" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label12" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label13" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label14" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label15" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label126" runat="server" Text="2T目以降" Width="180px"></asp:Label>
            <asp:Label ID="Label127" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label131" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label132" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label133" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label134" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label135" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label45" runat="server" Text="クリダメ上昇率" Width="180px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label136" runat="server" Text="1T目" Width="180px"></asp:Label>
            <asp:Label ID="Label114" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label16" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label17" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label18" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label19" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label20" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label137" runat="server" Text="2T目以降" Width="180px"></asp:Label>
            <asp:Label ID="Label138" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label141" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label142" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label143" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label144" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label145" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label46" runat="server" Text="攻撃力上昇率" Width="180px"></asp:Label>
            <asp:Label ID="Label115" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label21" runat="server" Width="175px"></asp:Label>
            <asp:Label ID="Label22" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label23" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label24" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label25" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label47" runat="server" Text="ダメージ上昇率" Width="180px"></asp:Label>
            <asp:Label ID="Label116" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label26" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label27" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label28" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label29" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label30" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label48" runat="server" Text="対ボス攻撃力上昇率" Width="180px"></asp:Label>
            <asp:Label ID="Label117" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label31" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label32" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label33" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label34" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label35" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label73" runat="server" Text="対ボスダメ上昇率" Width="180px"></asp:Label>
            <asp:Label ID="Label118" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label74" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label75" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label76" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label77" runat="server" Width="175px"></asp:Label>
            
        
            
            <asp:Label ID="Label78" runat="server" Width="115px"></asp:Label>
        </p>
        <p>
            &nbsp;PT全体効果</p>
        <p>
            <asp:Label ID="Label49" runat="server" Text="ソーラー上昇率" Width="180px"></asp:Label>
            <asp:Label ID="Label119" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label36" runat="server" Width="175px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label50" runat="server" Text="シャイクリ泥率" Width="180px"></asp:Label>
            <asp:Label ID="Label120" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label37" runat="server" Width="175px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label71" runat="server" Text="移動力" Width="180px"></asp:Label>
            <asp:Label ID="Label121" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label72" runat="server" Width="175px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label51" runat="server" Text="攻撃力低下率" Width="180px"></asp:Label>
            <asp:Label ID="Label122" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label38" runat="server" Width="175px"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label52" runat="server" Text="スキル発動低下率" Width="180px"></asp:Label>
            <asp:Label ID="Label123" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label39" runat="server" Width="175px"></asp:Label>
        </p>

    
   
        
    <p>
            <asp:Label ID="Label175" runat="server" Text="攻撃ミス率" Width="180px"></asp:Label>
            <asp:Label ID="Label176" runat="server" Width="60px"></asp:Label>
            <asp:Label ID="Label177" runat="server" Width="175px"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
         
            <br />
            <p>
                &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
        <asp:Label ID="Label210" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label211" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label212" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label213" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label214" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label215" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label216" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label217" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label218" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label219" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label220" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label221" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label222" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label223" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label224" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label225" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label226" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label227" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label228" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label229" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label230" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label231" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        </p>

    
    <p>
        <a href="http://pc-play.games.dmm.com/play/flower/" target="_blank">『フラワーナイトガール』</a>（C) DMMゲームズ</p>
    <p>
        当サイトのデータに関しては、<a href="http://xn--eckq7fg8cygsa1a1je.xn--wiki-4i9hs14f.com/" target="_blank">フラワーナイトガール攻略まとめwiki</a>様を参考にさせていただいております。</p>
        <p>
        <asp:Label ID="Label232" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label233" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label234" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label235" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label236" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label237" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label238" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label239" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label240" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label241" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label242" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label243" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label244" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label245" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label246" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label247" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label248" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label249" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label250" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label251" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label252" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label253" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        </p>
    <p>
        <asp:Label ID="Label124" runat="server" ClientIDMode="Static" Text="読めるかな？" Visible="False"></asp:Label>
        </p>
        <!--
        <a href="花騎士登録フォーム.aspx" target="_blank">〇登録フォーム</a><br />
        -->
    <script>
        /*
       window.onload = greet();
       window.onload = checkFalse();
       window.onload = addDropDownList();
        window.onload = addDropDownList2();
        window.onload = testJsc();
       */

   </script>
        
    </form>

    </body>
</html>
