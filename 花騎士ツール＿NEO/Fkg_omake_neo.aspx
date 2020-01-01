<%@ Page Title="おまけ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fkg_omake_neo.aspx.cs" Inherits="花騎士ツール＿NEO.WebForm3" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container-fluid">
        <h1>おまけ</h1>
        <div class="row">
            <div class="col-lg-4 col-xs-12">
                <h2 style="color: #009933">〇アンプルゥ数自動計算</h2>
                <h4>下のテキストボックスに入力すると他の項目を勝手に計算します</h4>
                <p>加算値：3000とか5400とか、アンプルゥによって追加される値</p>
                <p>通常数：通常アンプルゥ数</p>
                <p>上位数：上位アンプルゥ数</p>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="" Width="35px"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="加算値" Width="70px" Font-Bold="True"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="通常数" Width="70px" Font-Bold="True"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="上位数" Width="70px" Font-Bold="True"></asp:Label>
                    <br />
                    ＨＰ<asp:TextBox ID="TextBox1" runat="server" Width="70px">0</asp:TextBox>
                    <asp:TextBox ID="TextBox2" runat="server" Width="70px">0</asp:TextBox>
                    <asp:TextBox ID="TextBox3" runat="server" Width="70px">0</asp:TextBox>
                    <br />
                    攻撃<asp:TextBox ID="TextBox4" runat="server" Width="70px">0</asp:TextBox>
                    <asp:TextBox ID="TextBox5" runat="server" Width="70px">0</asp:TextBox>
                    <asp:TextBox ID="TextBox6" runat="server" Width="70px">0</asp:TextBox>
                    <br />
                    防御<asp:TextBox ID="TextBox7" runat="server" Width="70px">0</asp:TextBox>
                    <asp:TextBox ID="TextBox8" runat="server" Width="70px">0</asp:TextBox>
                    <asp:TextBox ID="TextBox9" runat="server" Width="70px">0</asp:TextBox>
                </div>
                <br />
            </div>
            <div class="col-lg-4 col-xs-12">
                <h2 style="color: #009933">〇昇華石必要数計算</h2>
                <h4>下記レアリティ、装備枠数、スキルレベルを指定すると自動的に必要数が計算されます</h4>
                <p>必要数と共に必要な11連ガチャ回数と単発ガチャ回数が表示されますので、この回数回して下さいw</p>
                <p style="font-weight: bold">レアリティ</p>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="2">☆2</asp:ListItem>
                        <asp:ListItem Value="3">☆3</asp:ListItem>
                        <asp:ListItem Value="4">☆4</asp:ListItem>
                        <asp:ListItem Value="5">☆5(イベ金)</asp:ListItem>
                        <asp:ListItem Selected="True" Value="6">☆5(ガチャ金)</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p style="font-weight: bold">装備枠</p>
                <div class="form-group" aria-orientation="horizontal">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1枠</asp:ListItem>
                        <asp:ListItem Value="2">2枠</asp:ListItem>
                        <asp:ListItem Value="3">3枠</asp:ListItem>
                        <asp:ListItem Selected="True" Value="4">4枠</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p style="font-weight: bold">スキルレベル</p>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem Selected="True">5</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p>この下に必要数が表示されます。</p>
                <div class="row">
                    <div class="col-lg-4 col-sm-2 col-xs-4">
                        <p>昇華石</p>
                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="500個" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-2 col-xs-4">
                        <p>11連</p>
                        <div class="form-group">
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Medium" Text="22回分" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-2 col-xs-4">
                        <p>単発</p>
                        <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Medium" Text="8回分" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
                <p>現在の昇華石所持数を入力すると、必要なガチャ回数を計算します</p>
                <div class="row">
                    <div class="col-sm-12 col-xs-12">
                        <div class="form-group">
                        <asp:Label ID="Label16" runat="server" Text="所持数(入力)　" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="TextBox10" runat="server" Text="0" Width="80px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-sm-2 col-xs-4">
                        <div class="form-group">
                        <p>必要数</p>
                            <asp:Label ID="Label8" runat="server" Text="500個" ForeColor="Blue" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-2 col-xs-4">
                        <p>11連</p>
                        <div class="form-group">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Medium" Text="22回分" ForeColor="Blue"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-2 col-xs-4">
                        <p>単発</p>
                        <div class="form-group">
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Medium" Text="8回分" ForeColor="Blue"></asp:Label>
                        </div>
                    </div>
                    
                </div>
                <br />
            </div>
            <div class="col-lg-4 col-xs-12">
                <h2 style="color: #009933">〇スワンボートレース</h2>
                <h2 style="color: #009933">　モコウ神予想解読</h2>
                <h4>オッズの順位、ワレモコウの予想マークを入力すると予想順位を表示します</h4>
                <div class="col-xs-1">
                    <!--dummy-->
                </div>
                <div class="col-xs-11">
                <div class="form-group">
                    <table border="1">
	                    <thead color: #fff;>
		                    <tr>
			                    <td>枠</td>
			                    <td>オッズ順位</td>
			                    <td>モコウ評価</td>
                                <%--<td>あなたの予想</td>--%>
			                    <td>予想順位</td>
		                    </tr>
                        </thead>
                        <tbody>
		                    <tr>
			                    <td style="text-align: center">1</td>
			                    <td> <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Selected="True" Value="1">10</asp:ListItem>
                                        <asp:ListItem Value="2">20</asp:ListItem>
                                        <asp:ListItem Value="3">30</asp:ListItem>
                                        <asp:ListItem Value="4">50</asp:ListItem>
                                        <asp:ListItem Value="5">100</asp:ListItem>
                                    </asp:DropDownList></td>
			                    <td>
                                    <asp:DropDownList ID="DropDownList6" runat="server">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>◎</asp:ListItem>
                                        <asp:ListItem>〇</asp:ListItem>
                                        <asp:ListItem>△</asp:ListItem>
                                    </asp:DropDownList></td>
                                <%--<td>
                                    <asp:DropDownList ID="DropDownList11" runat="server">
                                        <asp:ListItem>予想なし</asp:ListItem>
                                        <asp:ListItem Value="1">1位</asp:ListItem>
                                        <asp:ListItem Value="2">2位</asp:ListItem>
                                        <asp:ListItem Value="3">3位</asp:ListItem>
                                        <asp:ListItem Value="4">4位</asp:ListItem>
                                        <asp:ListItem Value="5">5位</asp:ListItem>
                                    </asp:DropDownList></td>--%>
			                    <td style="text-align: center">
                                    <asp:Label ID="Label11" runat="server" Text="1"></asp:Label></td>
		                    </tr>
		                    <tr>
			                    <td style="text-align: center">2</td>
			                    <td>
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem Value="1">10</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">20</asp:ListItem>
                                        <asp:ListItem Value="3">30</asp:ListItem>
                                        <asp:ListItem Value="4">50</asp:ListItem>
                                        <asp:ListItem Value="5">100</asp:ListItem>
                                    </asp:DropDownList></td>
			                    <td>
                                    <asp:DropDownList ID="DropDownList7" runat="server">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>◎</asp:ListItem>
                                        <asp:ListItem>〇</asp:ListItem>
                                        <asp:ListItem>△</asp:ListItem>
                                    </asp:DropDownList></td>
                               <%-- <td><asp:DropDownList ID="DropDownList12" runat="server">
                                        <asp:ListItem>予想なし</asp:ListItem>
                                        <asp:ListItem Value="1">1位</asp:ListItem>
                                        <asp:ListItem Value="2">2位</asp:ListItem>
                                        <asp:ListItem Value="3">3位</asp:ListItem>
                                        <asp:ListItem Value="4">4位</asp:ListItem>
                                        <asp:ListItem Value="5">5位</asp:ListItem>
                                    </asp:DropDownList></td>--%>
			                    <td style="text-align: center">
                                    <asp:Label ID="Label12" runat="server" Text="2"></asp:Label></td>
		                    </tr>
		                    <tr>
			                    <td style="text-align: center">3</td>
			                    <td><asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem Value="1">10</asp:ListItem>
                                        <asp:ListItem Value="2">20</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">30</asp:ListItem>
                                        <asp:ListItem Value="4">50</asp:ListItem>
                                        <asp:ListItem Value="5">100</asp:ListItem>
                                    </asp:DropDownList></td>
			                    <td><asp:DropDownList ID="DropDownList8" runat="server">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>◎</asp:ListItem>
                                        <asp:ListItem>〇</asp:ListItem>
                                        <asp:ListItem>△</asp:ListItem>
                                    </asp:DropDownList></td>
                                <%--<td><asp:DropDownList ID="DropDownList13" runat="server">
                                        <asp:ListItem>予想なし</asp:ListItem>
                                        <asp:ListItem Value="1">1位</asp:ListItem>
                                        <asp:ListItem Value="2">2位</asp:ListItem>
                                        <asp:ListItem Value="3">3位</asp:ListItem>
                                        <asp:ListItem Value="4">4位</asp:ListItem>
                                        <asp:ListItem Value="5">5位</asp:ListItem>
                                    </asp:DropDownList></td>--%>
			                    <td style="text-align: center">
                                    <asp:Label ID="Label13" runat="server" Text="3"></asp:Label></td>
		                    </tr>
		                    <tr>
			                    <td style="text-align: center">4</td>
			                    <td><asp:DropDownList ID="DropDownList4" runat="server">
                                        <asp:ListItem Value="1">10</asp:ListItem>
                                        <asp:ListItem Value="2">20</asp:ListItem>
                                        <asp:ListItem Value="3">30</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">50</asp:ListItem>
                                        <asp:ListItem Value="5">100</asp:ListItem>
                                    </asp:DropDownList></td>
			                    <td><asp:DropDownList ID="DropDownList9" runat="server">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>◎</asp:ListItem>
                                        <asp:ListItem>〇</asp:ListItem>
                                        <asp:ListItem>△</asp:ListItem>
                                    </asp:DropDownList></td>
                                <%--<td><asp:DropDownList ID="DropDownList14" runat="server">
                                        <asp:ListItem>予想なし</asp:ListItem>
                                        <asp:ListItem Value="1">1位</asp:ListItem>
                                        <asp:ListItem Value="2">2位</asp:ListItem>
                                        <asp:ListItem Value="3">3位</asp:ListItem>
                                        <asp:ListItem Value="4">4位</asp:ListItem>
                                        <asp:ListItem Value="5">5位</asp:ListItem>
                                    </asp:DropDownList></td>--%>
			                    <td style="text-align: center">
                                    <asp:Label ID="Label14" runat="server" Text="4"></asp:Label></td>
		                    </tr>
		                    <tr>
			                    <td style="text-align: center">5</td>
			                    <td><asp:DropDownList ID="DropDownList5" runat="server">
                                        <asp:ListItem Value="1">10</asp:ListItem>
                                        <asp:ListItem Value="2">20</asp:ListItem>
                                        <asp:ListItem Value="3">30</asp:ListItem>
                                        <asp:ListItem Value="4">50</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="5">100</asp:ListItem>
                                    </asp:DropDownList></td>
			                    <td><asp:DropDownList ID="DropDownList10" runat="server">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>◎</asp:ListItem>
                                        <asp:ListItem>〇</asp:ListItem>
                                        <asp:ListItem>△</asp:ListItem>
                                    </asp:DropDownList></td>
                                <%--<td><asp:DropDownList ID="DropDownList15" runat="server">
                                        <asp:ListItem>予想なし</asp:ListItem>
                                        <asp:ListItem Value="1">1位</asp:ListItem>
                                        <asp:ListItem Value="2">2位</asp:ListItem>
                                        <asp:ListItem Value="3">3位</asp:ListItem>
                                        <asp:ListItem Value="4">4位</asp:ListItem>
                                        <asp:ListItem Value="5">5位</asp:ListItem>
                                    </asp:DropDownList></td>--%>
			                    <td style="text-align: center">
                                    <asp:Label ID="Label15" runat="server" Text="5"></asp:Label></td>
		                    </tr>
	                    </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>    
     </div>
</asp:Content>
