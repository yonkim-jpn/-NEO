<%@ Page Title="花騎士編成支援ツール ホーム" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="花騎士ツール＿NEO._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <p class="lead">ここでは、ＤＭＭのゲーム、フラワーナイトガールに関するＷＥＢアプリを公開しています。</p>
        <p><a href="javascript:void(0);" class="btn btn-primary btn-lg" target="_blank" onclick="window.open('http://pc-play.games.dmm.com/play/flower/','_blank')">PC版サイト &raquo;</a></p>
        
        当サイトのデータに関しては、フラワーナイトガール攻略まとめwiki様を参考にさせていただいております。
        <p><a href="javascript:void(0);" class="btn btn-default"　target="_blank" onclick="window.open('http://xn--eckq7fg8cygsa1a1je.xn--wiki-4i9hs14f.com/','_blank')">wikiへはこちらから &raquo;</a></p>
    </div>
    <div>
        <asp:Label ID="Label1001" runat="server" Font-Bold="True"></asp:Label><br />
        <asp:Label ID="Label1002" runat="server" Font-Bold="True"></asp:Label><br />
        <asp:Label ID="Label1003" runat="server" Font-Bold="True"></asp:Label><br />
        <p>花騎士のデータ登録を協力してくれる方いらっしゃいましたら<a href="javascript:void(0);"　target="_blank" onclick="window.open('https://twitter.com/chiyokanemaru/status/1229573710912221185?s=20','_blank')">Twitterアカウント</a>までご連絡下さい。</p>
        <p>以下の項目に関してコンテンツを公開しております。</p>
    </div>
    <div class="row">
        <div class="col-md-4">
            <a runat="server" href="~/Fkg_select_neo"><h2>編成シミュ</h2></a>
            <p>
                5人の花騎士を選択し、PT編成時のスキル発動率等の計算を行います。
                花騎士の選択時には様々なフィルタを利用し、膨大なキャラの中から絞る事が出来ます。
            </p>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/Fkg_ability_serch_neo"><h2>アビリティ検索</h2></a>
            <p>
                花騎士を所有するアビリティにより検索できます。各フィルタにより、花騎士を絞って検索可能です。
            </p>
      </div>
        <div class="col-md-4">
            <a runat="server" href="~/Fkg_ranking_neo"><h2>ランキング</h2></a>
            <p>
                花騎士の能力値をランキング表示できます。
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <a runat="server" href="~/Fkg_omake_neo"><h2>おまけ</h2></a>
            <p>
                ちょっとしたおまけです。アンプルゥ計算機と昇華石計算機があります。スワンボートレースのワレモコウ予想解読ツールも追加。
            </p>
        </div>

        

        <div class="col-md-4">
            <a runat="server" href="~/Fkg_omake_darkness"><h2>闇の世界</h2></a>
            <p>水晶塔最上階にてゾンデを倒した光の戦士達は</p>
            <p>まっくらの雲を倒すため闇の世界へと旅立つ</p>
            <p>この先帰ってこれるかわからないよ？</p>
            <p>オヌヌメは賢者x2と忍者x2</p>
        </div>

        <div class="col-md-4">
            <a runat="server" href="~/FKG_register"><h2>花騎士登録</h2></a>
            <p>自分の所持する花騎士とスキルレベルを登録出来ます。</p>
            <p>それにより、編成シミュ上で自分の持っているキャラに</p>
            <p>対してスキルレベルを自動で入力する事が出来ます。</p>
            <p>（超上級者向け）</p>
        </div>

    </div>
    <div class="row">
        <div class="col-md-4">
            <a runat="server" href="~/La_via_al_paraiso"><h2>天上への道</h2></a>
            <p>
                他ゲーで天井ガチャへの課金額を知りたい場合の簡易計算ツール。完全なる管理人の趣味です
            </p>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/Magia_damage_calc"><h2>マギレコダメージ計算</h2></a>
            <p>
                マギレコのダメージ計算に興味があったので作ってみました。（現在作成中）完全なる管理人の趣味です
            </p>
        </div>
    </div>

    
  

</asp:Content>
