<%@ Page Title="天上への道" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="La_via_al_paraiso.aspx.cs" Inherits="花騎士ツール＿NEO.La_via_al_paraiso" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <h2 style="font-weight: bold; font-style: italic">天井への道</h2>
                    <h3>ガチャの天井までにいくら予算を想定しておけばいいのか簡易計算機を用意しました。</h3>
                    <h3>残高をしっかり確認して素晴らしいガチャライフを送りましょう!?</h3>
                    <h4>ゲーム選択</h4>
                    <asp:RadioButtonList ID="tipo_del_juego" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>プリコネ</asp:ListItem>
                        <asp:ListItem>マギレコ</asp:ListItem>
                        <asp:ListItem Selected="True">自分で設定</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-xs-12 bg-warning">
                <div class="form-group">
                    <h3 style="font-weight: bold">ガチャ設定</h3>
                    <h4>天井数選択</h4>
                    <asp:RadioButtonList ID="RadioButtonList777" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem Selected="True">300</asp:ListItem>
                        <asp:ListItem Value="0">それ以外</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:TextBox ID="numero_de_paraiso" runat="server" Width="400px">それ以外の場合はこちらに天井数を入力</asp:TextBox>
                    <br />
                    <h4>石価格</h4>
                    <asp:TextBox ID="billete" runat="server">価格</asp:TextBox>
                    <asp:TextBox ID="piedra" runat="server">石</asp:TextBox>
                    <h4>ガチャ10回分の石で何回引けるか？</h4>
                    <asp:RadioButtonList ID="RadioButtonList778" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                    </asp:RadioButtonList>
                    <h4>ガチャ10回分に必要な石</h4>
                    <asp:RadioButtonList ID="RadioButtonList779" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">250</asp:ListItem>
                        <asp:ListItem>1500</asp:ListItem>
                        <asp:ListItem>2500</asp:ListItem>
                        <asp:ListItem>3000</asp:ListItem>
                        <asp:ListItem>5000</asp:ListItem>
                        <asp:ListItem Value="0">その他</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:TextBox ID="precio" runat="server" Width="401px">その他の場合はこちらに石価格を入力</asp:TextBox>
                </div>
            </div>
            <div class="col-md-4 col-xs-12 bg-danger">
                <div class="form-group">
                    <h3 style="font-weight: bold">自分の状態を入力</h3>
                    <h4>引いたガチャ数</h4>
                    <asp:TextBox ID="numero_de_la_gacha" runat="server">0</asp:TextBox>
                    <h4>所持石数</h4>
                    <asp:TextBox ID="numero_de_la_piedra" runat="server">0</asp:TextBox>
                 </div>
                <div class="form-group">
                    <h4 style="font-weight: bold">プリコネ限定設定</h4>
                    <h5>7500石を何回買ったか</h5>
                    <asp:RadioButtonList ID="RadioButtonList780" runat="server" Enabled="False" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">未購入</asp:ListItem>
                        <asp:ListItem Value="1">1回</asp:ListItem>
                        <asp:ListItem Value="2">2回</asp:ListItem>
                        <asp:ListItem Value="3">3回</asp:ListItem>
                    </asp:RadioButtonList>

                </div>
            </div>
            <div class="col-md-4 col-xs-12 bg-success">
                <div class="form-group">
                    <h2 style="font-weight: bold">結果</h2>
                    <h3>必要な石の数</h3>
                    <asp:Label ID="resultado" runat="server" Text=""></asp:Label>
                    <h3>必要な課金額</h3>
                    <asp:Label ID="resultado2" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div class ="row">
            <div class="col-xs-9"></div>
            <div class="col-xs-3">
                <a runat="server" href="~/Magia_damage_calc"><h4>マギレコダメージ計算（仮）</h4></a>
            </div>
        </div>
    </div>
</asp:Content>
