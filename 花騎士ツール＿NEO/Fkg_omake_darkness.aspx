<%@ Page Title="闇のおまけ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fkg_omake_darkness.aspx.cs" Inherits="花騎士ツール＿NEO.WebForm3" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <h1 style="font-weight: bold">闇のおまけ</h1>
                <h3>ここではFKGの闇とも言える課金について扱います。</h3>
                <h3>一般的な課金モデル？と思われる、毎日100円ガチャと、ウィークリーガチャについていくら課金しているか計算してしまおう！というわけです。</h3>
                <h3>かなりのショックを受ける恐れがあるため、心臓の弱い人は注意して下さい。</h3>
                <h3>（DMMポイントで計算）</h3>
            </div>
        </div>
         <div class="row">
            <div class="col-sm-4 col-xs-12">
                <h2 aria-atomic="False" style="font-weight: bold; font-style: italic">課金スタイル</h2>
                <p style="font-weight: bold" class="text-info">〇毎日100ポイントガチャ</p>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList3001" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">しない</asp:ListItem>
                        <asp:ListItem Value="1">1回</asp:ListItem>
                        <asp:ListItem Value="2">2回</asp:ListItem>
                        <asp:ListItem Value="3">3回</asp:ListItem>
                        <asp:ListItem Value="4">4回</asp:ListItem>
                        <asp:ListItem Value="5">5回</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p style="font-weight: bold" class="text-info">〇ウィークリーガチャ</p>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList3002" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">買わない</asp:ListItem>
                        <asp:ListItem Value="1">毎週買う</asp:ListItem>
                        <asp:ListItem Value="2">隔週で買う</asp:ListItem>
                        <asp:ListItem Value="3">月1で買う</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p style="font-weight: bold" class="text-info">〇スペチケ(二ヵ月に一回くらいとして)</p>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList3003" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">買わない</asp:ListItem>
                        <asp:ListItem Value="1">毎回買う</asp:ListItem>
                        <asp:ListItem Value="2">二回にいっかい買う</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p style="font-weight: bold" class="text-info">〇装花ガチャ(毎月一回くらいとして)</p>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList3004" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">買わない</asp:ListItem>
                        <asp:ListItem Value="1">毎回買う</asp:ListItem>
                        <asp:ListItem Value="2">二回に一回買う</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="col-sm-4 col-xs-12">
                <h2 style="font-weight: bold; font-style: italic">期間</h2>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList3005" runat="server">
                        <asp:ListItem Selected="True" Value="4">4週間</asp:ListItem>
                        <asp:ListItem Value="13">約三か月(13週間)</asp:ListItem>
                        <asp:ListItem Value="26">約半年間(26週間)</asp:ListItem>
                        <asp:ListItem Value="52">一年間(52週間)</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        
            <div class="col-sm-4 col-xs-12 bg-danger">
                <h2 style="font-weight: bold; font-style: italic">計算結果</h2>
                <div class="form-group">
                    <p style="font-weight: bold" >総消費DMMポイント
                    <asp:Label ID="Label3001" runat="server" Text="0ポイント"></asp:Label>
                    </p>
                    <p style="font-weight: bold" >虹メダ取得数(確定)
                    <asp:Label ID="Label3002" runat="server" Text="0個"></asp:Label>
                    </p>
                    <p style="font-weight: bold" >団長メダル取得数
                    <asp:Label ID="Label3003" runat="server" Text="0個"></asp:Label>
                    </p>
                    <p style="font-weight: bold" >昇華石ガチャ出来る回数
                    <asp:Label ID="Label3004" runat="server" Text="0個"></asp:Label>
                    </p>
                    <p style="font-weight: bold" >昇華石ガチャでの入手期待値
                    <asp:Label ID="Label3005" runat="server" Text="0個"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>