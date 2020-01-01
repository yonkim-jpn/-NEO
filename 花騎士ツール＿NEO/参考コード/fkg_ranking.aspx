<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fkg_ranking.aspx.cs" Inherits="fkg_ranking" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>花騎士ランキング</title>
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
        <div>
            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Large" Text="〇花騎士ランキング"></asp:Label>
            <br />
            <br />
            各花騎士の能力値は、開花後、好感度咲ＭＡＸ状態での値です。（アンプルゥ入ってません。）<br />
            <br />
        </div>
        <asp:Label ID="Label1" runat="server" Text="各種フィルタ" Width="180px"></asp:Label>
        <p>
            <asp:Label ID="Label2" runat="server" Text="・レアリティ" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="group1" Text="全レア" Width="70px" />
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="group1" Text="6" Width="70px" />
            <asp:RadioButton ID="RadioButton3" runat="server" Enabled="False" GroupName="group1" Text="5" Width="70px" />
            <asp:RadioButton ID="RadioButton4" runat="server" Enabled="False" GroupName="group1" Text="他" Width="70px" />
            <asp:RadioButton ID="RadioButton27" runat="server" GroupName="group1" Text="昇華虹" Width="70px" />
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="・属性" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton5" runat="server" Checked="True" GroupName="group2" Text="全属性" Width="70px" />
            <asp:RadioButton ID="RadioButton6" runat="server" GroupName="group2" Text="斬" Width="70px" />
            <asp:RadioButton ID="RadioButton7" runat="server" GroupName="group2" Text="打" Width="70px" />
            <asp:RadioButton ID="RadioButton8" runat="server" GroupName="group2" Text="突" Width="70px" />
            <asp:RadioButton ID="RadioButton9" runat="server" GroupName="group2" Text="魔" Width="70px" />
        </p>
        <p>
            <asp:Label ID="Label4" runat="server" Text="・国家" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton10" runat="server" Checked="True" GroupName="group3" Text="全国家" Width="70px" />
            <asp:RadioButton ID="RadioButton11" runat="server" GroupName="group3" Text="知徳花" Width="70px" />
            <asp:RadioButton ID="RadioButton12" runat="server" GroupName="group3" Text="深緑花" Width="70px" />
            <asp:RadioButton ID="RadioButton13" runat="server" GroupName="group3" Text="常夏花" Width="70px" />
            <asp:RadioButton ID="RadioButton14" runat="server" GroupName="group3" Text="風谷花" Width="70px" />
            <asp:RadioButton ID="RadioButton15" runat="server" GroupName="group3" Text="雪原花" Width="70px" />
            <asp:RadioButton ID="RadioButton16" runat="server" GroupName="group3" Text="湖畔花" Width="70px" />
        </p>
        <p>
            <asp:Label ID="Label5" runat="server" Text="・評価項目" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton17" runat="server" Checked="True" GroupName="group4" Text="総合" Width="70px" />
            <asp:RadioButton ID="RadioButton18" runat="server" GroupName="group4" Text="ＨＰ" Width="70px" />
            <asp:RadioButton ID="RadioButton19" runat="server" GroupName="group4" Text="攻撃" Width="70px" />
            <asp:RadioButton ID="RadioButton20" runat="server" GroupName="group4" Text="防御" Width="70px" />
            <asp:RadioButton ID="RadioButton21" runat="server" GroupName="group4" Text="移動" Width="70px" />
        </p>
        <p>
            <asp:Label ID="Label6" runat="server" Text="・ソート順" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton22" runat="server" Checked="True" GroupName="group5" Text="降順" Width="70px" />
            <asp:RadioButton ID="RadioButton23" runat="server" GroupName="group5" Text="昇順" Width="70px" />
        </p>
        <p>
            <asp:Label ID="Label7" runat="server" Text="・表示数" Width="180px"></asp:Label>
            <asp:RadioButton ID="RadioButton24" runat="server" Checked="True" GroupName="group6" Text="30" Width="70px" />
            <asp:RadioButton ID="RadioButton25" runat="server" GroupName="group6" Text="50" Width="70px" />
            <asp:RadioButton ID="RadioButton26" runat="server" GroupName="group6" Text="100" Width="70px" />

        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Height="60px" OnClick="Button1_Click" Text="表示" Width="120px" />

        </p>
        <p>
            &nbsp;</p>
        <p>

            <asp:GridView ID="grvDetail" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Ranking" HeaderText="順位" SortExpression="Ranking">
                    <ItemStyle HorizontalAlign="Right" Width="40px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="名前" >
                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Rarity" HeaderText="レア">
                    <ItemStyle HorizontalAlign="Center" Width="40px" Wrap="False" />
                    </asp:BoundField>
<asp:BoundField DataField="ATT" HeaderText="属性">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
<asp:BoundField DataField="Total" HeaderText="総合">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
<asp:BoundField DataField="HP" HeaderText="ＨＰ">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
<asp:BoundField DataField="ATK" HeaderText="攻撃">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
<asp:BoundField DataField="DEF" HeaderText="防御">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:BoundField>
<asp:BoundField DataField="MOV" HeaderText="移動力">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:BoundField>
<asp:BoundField DataField="Unit" HeaderText="在籍国家">
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                </Columns>
         
            </asp:GridView>

        </p>
        <asp:ListBox ID="ListBox1" runat="server" Height="1000px" Visible="False" Width="1000px"></asp:ListBox>

        <br />
        <asp:Label ID="Label189" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label190" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label191" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label192" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label193" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label194" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label195" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label196" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label197" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label198" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label199" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label200" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label201" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label202" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label203" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label204" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label205" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label206" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label207" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label208" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label209" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <asp:Label ID="Label210" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        <br />
    メインページは<a href="fkg_select.aspx">花騎士編成支援ツール</a>です。
    <p>
        <a href="http://pc-play.games.dmm.com/play/flower/" target="_blank">『フラワーナイトガール』</a>（C) DMMゲームズ</p>
    <p>
        当サイトのデータに関しては、<a href="http://xn--eckq7fg8cygsa1a1je.xn--wiki-4i9hs14f.com/" target="_blank">フラワーナイトガール攻略まとめwiki</a>様を参考にさせていただいております。</p>
        <p>
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
        <asp:Label ID="Label232" runat="server" Font-Overline="False" Font-Size="Smaller" Font-Underline="False" Text="ティムポ"></asp:Label>
        </p>
    </form>
    </body>
</html>
