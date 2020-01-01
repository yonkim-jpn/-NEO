<%@ Page Title="マギレコダメ計算" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Magia_damage_calc.aspx.cs" Inherits="花騎士ツール＿NEO.Magia_damage_calc" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="container">
        <h2>マギレコ簡易ダメ計算<small>（超ベ〇ータ版）</small></h2>
        <h5>算出されたダメージ値に乱数はかかっていません</h5>
        <div class="row">
            
                <div class="col-sm-3 col-xs-6">
                    <h4 style="font-weight: bold">戦場</h4>
                    <asp:RadioButtonList ID="ventana" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">ミラーズ</asp:ListItem>
                        <asp:ListItem Value="0">クエスト</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-sm-3 col-xs-6">
                    <h4 style="font-weight: bold">攻撃方法</h4>
                    <asp:RadioButtonList ID="estadoAtk" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">通常攻撃</asp:ListItem>
                        <asp:ListItem Selected="True" Value="1">ピュエラコンボ</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-sm-3 col-xs-6">
                    <h4 style="font-weight: bold">Charge+</h4>
                    <p><input type="number" id= "chargePlus" name="chargePlus" min="0" max="20" value = "0"></p>
                </div>
                <div class="col-sm-3 col-xs-6">
                    <h4 style="font-weight: bold">属性</h4>
                    <asp:RadioButtonList ID="atributo" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>有利</asp:ListItem>
                        <asp:ListItem Selected="True">並盛</asp:ListItem>
                        <asp:ListItem>不利</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            
        </div>
        <div class="row">
            <div class="col-xs-12 visible-xs">
                <p>
                    <button id="atkCollapse" type="button" class="btn btn-info" data-toggle="collapse" data-target="#multiCollapseExample1" aria-expanded="false" aria-controls="multiCollapseExample1">攻撃設定 とじる</button>
                    <button id="defCollapse" type="button" class="btn btn-danger" data-toggle="collapse" data-target="#multiCollapseExample2" aria-expanded="false" aria-controls="multiCollapseExample2">守備設定 とじる</button>
                </p>
            </div>
        </div>
        
        <div class="row">
            <div class="collapse in" id="multiCollapseExample1">
                <div class="col-sm-6 col-xs-12 bg-info">
                    <div class="row">
                         <div class="col-xs-6">
                             <h5 style="font-weight: bold">攻撃側覚醒<small>星5想定</small></h5>
                            <canvas id ="canvas1" width ="150" height ="150">Canvasに対応したブラウザを使用してください。</canvas >
                        </div>
                        <div class="col-xs-6">
                            <div class="row">
                                <div class="col-xs-6">
                                    <h5 style="font-weight: bold">攻撃側ATK</h5>
                                    <asp:TextBox ID="TextBox666" runat="server" Text="0" Width="70px"></asp:TextBox>
                                </div>
                                <div class ="col-xs-6">
                                    <h5 style="font-weight: bold">メモリアATK</h5>
                                    <asp:TextBox ID="TextBox667" runat="server" Text="0" Width="65px"></asp:TextBox>
                                </div>
                                <div class="col-xs-12">
                                    <h5 style="font-weight: bold">魔法少女タイプ</h5>
                                    <asp:RadioButtonList ID="tipoPuella" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Enabled="True" Font-Size="Smaller" RepeatLayout="Table">
                                        <asp:ListItem>マギア</asp:ListItem>
                                        <asp:ListItem>サポート</asp:ListItem>
                                        <asp:ListItem Selected="True">アタック</asp:ListItem>
                                        <asp:ListItem>ヒール</asp:ListItem>
                                        <asp:ListItem>バランス</asp:ListItem>
                                        <asp:ListItem>ディフェンス</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class ="col-xs-6">
                            <h5 style="font-weight: bold">陣形による攻撃力上昇</h5>
                            <asp:RadioButtonList ID="ordendeBatalla" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">無</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class ="col-xs-6">
                            <h5 style="font-weight: bold">メモリア攻撃力UP</h5>
                            <asp:TextBox ID="AtkUp" runat="server" Width="90px" Text="0%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <h5 style="font-style: italic; font-size: medium; font-weight: bold">メモリア設定</h5>
                    </div>
                    <div class="row" style="margin-bottom:3px">
                        <div class="col-xs-6">
                            <h5 style="font-weight: bold">MP獲得量UP</h5>
                            <asp:DropDownList ID="MpUp" runat="server">
                                <asp:ListItem Value="0">無</asp:ListItem>
                                <asp:ListItem Value="1">Ⅰ</asp:ListItem>
                                <asp:ListItem Value="2">Ⅱ</asp:ListItem>
                                <asp:ListItem Value="3">Ⅲ</asp:ListItem>
                                <asp:ListItem Value="4">Ⅳ</asp:ListItem>
                                <asp:ListItem Value="5">Ⅴ</asp:ListItem>
                                <asp:ListItem Value="6">Ⅵ</asp:ListItem>
                                <asp:ListItem Value="7">Ⅶ</asp:ListItem>
                                <asp:ListItem Value="8">Ⅷ</asp:ListItem>
                                <asp:ListItem Value="9">Ⅸ</asp:ListItem>
                                <asp:ListItem Value="10">Ⅹ</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-6">
                            <h5 style="font-weight: bold">AccelMPUP</h5>
                            <asp:DropDownList ID="AMpUp" runat="server">
                                <asp:ListItem Value="0">無</asp:ListItem>
                                <asp:ListItem Value="1">Ⅰ</asp:ListItem>
                                <asp:ListItem Value="2">Ⅱ</asp:ListItem>
                                <asp:ListItem Value="3">Ⅲ</asp:ListItem>
                                <asp:ListItem Value="4">Ⅳ</asp:ListItem>
                                <asp:ListItem Value="5">Ⅴ</asp:ListItem>
                                <asp:ListItem Value="6">Ⅵ</asp:ListItem>
                                <asp:ListItem Value="7">Ⅶ</asp:ListItem>
                                <asp:ListItem Value="8">Ⅷ</asp:ListItem>
                                <asp:ListItem Value="9">Ⅸ</asp:ListItem>
                                <asp:ListItem Value="10">Ⅹ</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="collapse in" id="multiCollapseExample2">
            <div class="col-sm-6 col-xs-12 bg-danger">
                <div class="row">
                    <div class="col-xs-6">
                        <h5 style="font-weight: bold">守備側覚醒<small>DEFのみ可</small></h5>
                        <canvas id ="canvas2" width ="150" height ="100">Canvasに対応したブラウザを使用してください。</canvas >
                    </div>
                    <div class="col-xs-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <h5 style="font-weight: bold">守備側DEF</h5>
                                <asp:TextBox ID="TextBox668" runat="server" Text="0" Width="70px"></asp:TextBox>
                            </div>
                            <div class="col-xs-6">
                                <h5 style="font-weight: bold">メモリアDEF</h5>
                                <asp:TextBox ID="TextBox669" runat="server" Text="0" Width="65px"></asp:TextBox>
                            </div>
                            <div class="col-xs-12">
                                <h5 style="font-weight: bold">魔法少女タイプ</h5>
                                <asp:RadioButtonList ID="tipoPuella2" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Font-Size="Smaller">
                                    <asp:ListItem>マギア</asp:ListItem>
                                    <asp:ListItem>サポート</asp:ListItem>
                                    <asp:ListItem Selected="True">アタック</asp:ListItem>
                                    <asp:ListItem>ヒール</asp:ListItem>
                                    <asp:ListItem>バランス</asp:ListItem>
                                    <asp:ListItem>ディフェンス</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class ="col-xs-6">
                        <h5 style="font-weight: bold">陣形による守備力力上昇</h5>
                        <asp:RadioButtonList ID="ordendeBatalla2" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">無</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class ="col-xs-6">
                        <h5 style="font-weight: bold">メモリア守備力UP</h5>
                        <asp:TextBox ID="DefUp" runat="server" Width="90" Text="0%"></asp:TextBox>
                    </div>
                </div>
            </div>
            </div>
        </div>
        <div class ="row">
            <div class="col-xs-3 bg-warning">
                <h4 style="font-weight: bold">1個目</h4>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList666" runat="server" Font-Size="Small">
                        <asp:ListItem Value="A">Accel</asp:ListItem>
                        <asp:ListItem Selected="True" Value="B">Blast</asp:ListItem>
                        <asp:ListItem Value="C">Charge</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="col-xs-3 bg-warning">
                <h4 style="font-weight: bold">2個目</h4>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList667" runat="server" Font-Size="Small">
                        <asp:ListItem Value="A">Accel</asp:ListItem>
                        <asp:ListItem Value="B" Selected="True">Blast</asp:ListItem>
                        <asp:ListItem Value="C">Charge</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="col-xs-3 bg-warning">
                <h4 style="font-weight: bold">3個目</h4>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList668" runat="server" Font-Size="Small">
                        <asp:ListItem Value="A">Accel</asp:ListItem>
                        <asp:ListItem Value="B" Selected="True">Blast</asp:ListItem>
                        <asp:ListItem Value="C">Charge</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="col-xs-3 bg-warning">
                <h4 style="font-weight: bold">補助</h4>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList669" runat="server">
                        <asp:ListItem Value="A">全A</asp:ListItem>
                        <asp:ListItem Value="B" Selected="True">全B</asp:ListItem>
                        <asp:ListItem Value="C">全C</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>

        <!--結果表示-->
        <div class="row">
            <div class="col-xs-3 bg-success">
                <div id ="grande11"><h4 style="font-weight: bold">補正分</h4></div>
                <div id ="pequeno11" class="hidden"><h4 style="font-weight: bold">補正</h4></div>
                <asp:Label ID="modificado1" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande12"><h4 style="font-weight: bold">ダメージ</h4></div>
                <div id ="pequeno12" class="hidden"><h4 style="font-weight: bold">ダメ</h4></div>
                <asp:Label ID="dano1" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande13"><h4 style="font-weight: bold">獲得MP</h4></div>
                <div id ="pequeno13" class="hidden"><h4 style="font-weight: bold">MP増</h4></div>
                <asp:Label ID="magia1" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-xs-3 bg-success">
                <div id ="grande21"><h4 style="font-weight: bold">補正分</h4></div>
                <div id ="pequeno21" class="hidden"><h4 style="font-weight: bold">補正</h4></div>
                <asp:Label ID="modificado2" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande22"><h4 style="font-weight: bold">ダメージ</h4></div>
                <div id ="pequeno22" class="hidden"><h4 style="font-weight: bold">ダメ</h4></div>
                <asp:Label ID="dano2" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande23"><h4 style="font-weight: bold">獲得MP</h4></div>
                <div id ="pequeno23" class="hidden"><h4 style="font-weight: bold">MP増</h4></div>
                <asp:Label ID="magia2" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-xs-3 bg-success">
                <div id ="grande31"><h4 style="font-weight: bold">補正分</h4></div>
                <div id ="pequeno31" class="hidden"><h4 style="font-weight: bold">補正</h4></div>
                <asp:Label ID="modificado3" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande32"><h4 style="font-weight: bold">ダメージ</h4></div>
                <div id ="pequeno32" class="hidden"><h4 style="font-weight: bold">ダメ</h4></div>
                <asp:Label ID="dano3" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande33"><h4 style="font-weight: bold">獲得MP</h4></div>
                <div id ="pequeno33" class="hidden"><h4 style="font-weight: bold">MP増</h4></div>
                <asp:Label ID="magia3" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-xs-3 bg-success">
                <div id ="grande41"><h4 style="font-weight: bold">合計補正</h4></div>
                <div id ="pequeno41" class="hidden"><h4 style="font-weight: bold">合計</h4></div>
                <asp:Label ID="modificado4" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande42"><h4 style="font-weight: bold">合計ダメージ</h4></div>
                <div id ="pequeno42" class="hidden"><h4 style="font-weight: bold">合計</h4></div>
                <asp:Label ID="dano4" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                <div id ="grande43"><h4 style="font-weight: bold">合計獲得MP</h4></div>
                <div id ="pequeno43" class="hidden"><h4 style="font-weight: bold">合計</h4></div>
                <asp:Label ID="magia4" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-sm-6 col-xs-12">
                    <asp:CheckBox ID="decimal" runat="server" Text="MP小数表示" />
                </div>
            </div>
        </div>
        
    </section>
</asp:Content>