<%@ Page Title="マギレコダメージ計算" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Magia_damage_calc.aspx.cs" Inherits="花騎士ツール＿NEO.Magia_damage_calc" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="container">
        <h3>マギレコダメージ計算<small>（試用版）</small></h3>
        <div class="explain">
            <h5>マギアレコードに関するWEBアプリです</h5>
            <h5>算出されたダメージ値に乱数はかかっていません</h5>
            <h5>キャラデータは星5 LV100を想定しています</h5>
            <h5>精神強化反映については100/100状態を想定しています</h5>
        </div>
        <div class="subtitle">
            <div class="row">
            
                    <div class="col-sm-3 col-xs-6">
                        <h4 style="font-weight: bold">戦場</h4>
                        <asp:RadioButtonList ID="ventana" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">ミラーズ</asp:ListItem>
                            <asp:ListItem Value="0">クエスト</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-sm-3 col-xs-6">
                        <h4 style="font-weight: bold">相手の属性</h4>
                        <asp:RadioButtonList ID="atributo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">光</asp:ListItem>
                            <asp:ListItem>闇</asp:ListItem>
                            <asp:ListItem>火</asp:ListItem>
                            <asp:ListItem>水</asp:ListItem>
                            <asp:ListItem>木</asp:ListItem>
                            <asp:ListItem>無</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
            </div>
        </div>
        
        <ul class="nav nav-tabs">
	        <li class="active"><a href="#persona" data-toggle="tab">キャラ選択</a></li>
	        <li><a href="#despierto" data-toggle="tab">覚醒補正</a></li>
	        <li><a href="#memoria" data-toggle="tab">メモリア</a></li>
	        <li><a href="#enemigo" data-toggle="tab">相手側設定</a></li>
	        <li><a href="#mirrors" data-toggle="tab" style="left: 0px; top: 0px">ミラランpt</a></li>
        </ul>
        <div class="panel panel-info">
          <div class="panel-body">
        <div class="tab-content">
	        <div class="tab-pane active" id="persona">
                <div class="noselect subtitle">
                <%--キャラ設定スタート--%>

                        <div class="row">
                            <div class="col-xs-12">
                                <h4 style="font-weight: bold">フィルタ設定</h4>
                                <div class ="row">
                                    <div class ="col-lg-4 col-sm-6 col-xs-12 selector">
                                        <div class="filterMagia">
                                        <asp:CheckBoxList ID="filtro1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">全</asp:ListItem>
                                            <asp:ListItem>光</asp:ListItem>
                                            <asp:ListItem>闇</asp:ListItem>
                                            <asp:ListItem>火</asp:ListItem>
                                            <asp:ListItem>水</asp:ListItem>
                                            <asp:ListItem>木</asp:ListItem>
                                            <asp:ListItem>無</asp:ListItem>
                                        </asp:CheckBoxList>
                                    
                                        <asp:DropDownList ID="tipo1" runat="server">
                                            <asp:ListItem>タイプ無</asp:ListItem>
                                            <asp:ListItem>マギア</asp:ListItem>
                                            <asp:ListItem>サポート</asp:ListItem>
                                            <asp:ListItem>アタック</asp:ListItem>
                                            <asp:ListItem>ヒール</asp:ListItem>
                                            <asp:ListItem>バランス</asp:ListItem>
                                            <asp:ListItem>ディフェンス</asp:ListItem>
                                            <asp:ListItem>アルティメット</asp:ListItem>
                                            <asp:ListItem>エクシード</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class ="col-lg-1 col-sm-6 col-xs-12 selector">
                                        <asp:DropDownList ID="gorila" runat="server">
                                            <asp:ListItem>ディスク</asp:ListItem>
                                            <asp:ListItem>A3枚</asp:ListItem>
                                            <asp:ListItem>A2枚</asp:ListItem>
                                            <asp:ListItem>B3枚</asp:ListItem>
                                            <asp:ListItem>B2枚</asp:ListItem>
                                            <asp:ListItem>C3枚</asp:ListItem>
                                            <asp:ListItem>C2枚</asp:ListItem>
                                        </asp:DropDownList>
                                    </div> 
                                    <div class="col-xs-12 hidden-xs"></div>
                                    <div class ="col-lg-4 col-sm-6 col-xs-12 selector">
                                        <div class="filterMagia">
                                        <asp:DropDownList ID="tipoMagia" runat="server">
                                            <asp:ListItem Selected="True">マギア指定1</asp:ListItem>
                                            <asp:ListItem>敵全体</asp:ListItem>
                                            <asp:ListItem>敵単体</asp:ListItem>
                                            <asp:ListItem>敵縦一列</asp:ListItem>
                                            <asp:ListItem>ランダム</asp:ListItem>
                                            <asp:ListItem>属性強化</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>防御力DOWN</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>MP回復</asp:ListItem>
                                            <asp:ListItem>低HPほど威力UP</asp:ListItem>
                                            <asp:ListItem>状態強化解除</asp:ListItem>
                                            <asp:ListItem>バフ解除</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="tipoMagia2" runat="server">
                                            <asp:ListItem Selected="True">マギア指定2</asp:ListItem>
                                            <asp:ListItem>敵全体</asp:ListItem>
                                            <asp:ListItem>敵単体</asp:ListItem>
                                            <asp:ListItem>敵縦一列</asp:ListItem>
                                            <asp:ListItem>ランダム</asp:ListItem>
                                            <asp:ListItem>属性強化</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>防御力DOWN</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>MP回復</asp:ListItem>
                                            <asp:ListItem>低HPほど威力UP</asp:ListItem>
                                            <asp:ListItem>状態強化解除</asp:ListItem>
                                            <asp:ListItem>バフ解除</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-lg-4 col-sm-6 col-xs-12 selector">
                                        <asp:DropDownList ID="connect1" runat="server">
                                            <asp:ListItem Selected="True">コネクト指定1</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>防御力UP</asp:ListItem>
                                            <asp:ListItem>クリティカル</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避無効</asp:ListItem>
                                            <asp:ListItem>防御無視</asp:ListItem>
                                            <asp:ListItem>拘束</asp:ListItem>
                                            <asp:ListItem>魅了</asp:ListItem>
                                            <asp:ListItem>幻惑</asp:ListItem>
                                            <asp:ListItem>スタン</asp:ListItem>
                                            <asp:ListItem>挑発</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>MP回復</asp:ListItem>
                                            <asp:ListItem>MP獲得量UP</asp:ListItem>
                                            <asp:ListItem>AcceleMPUP</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="connect2" runat="server">
                                            <asp:ListItem Selected="True">コネクト指定2</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>防御力UP</asp:ListItem>
                                            <asp:ListItem>クリティカル</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避無効</asp:ListItem>
                                            <asp:ListItem>防御無視</asp:ListItem>
                                            <asp:ListItem>拘束</asp:ListItem>
                                            <asp:ListItem>魅了</asp:ListItem>
                                            <asp:ListItem>幻惑</asp:ListItem>
                                            <asp:ListItem>スタン</asp:ListItem>
                                            <asp:ListItem>挑発</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>MP回復</asp:ListItem>
                                            <asp:ListItem>MP獲得量UP</asp:ListItem>
                                            <asp:ListItem>AcceleMPUP</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-sm-6 col-xs-12 selector">
                                        <asp:DropDownList ID="menteS1" runat="server">
                                            <asp:ListItem Selected="True">精神強化スキル</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>防御力UP</asp:ListItem>
                                            <asp:ListItem>クリティカル</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>マギアダメージUP</asp:ListItem>
                                            <asp:ListItem>追撃</asp:ListItem>
                                            <asp:ListItem>ダメCUT状態</asp:ListItem>
                                            <asp:ListItem>ダメCUT無視</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避無効</asp:ListItem>
                                            <asp:ListItem>防御無視</asp:ListItem>
                                            <asp:ListItem>拘束</asp:ListItem>
                                            <asp:ListItem>魅了</asp:ListItem>
                                            <asp:ListItem>幻惑</asp:ListItem>
                                            <asp:ListItem>スタン</asp:ListItem>
                                            <asp:ListItem>呪い</asp:ListItem>
                                            <asp:ListItem>霧</asp:ListItem>
                                            <asp:ListItem>暗闇</asp:ListItem>
                                            <asp:ListItem>攻撃力DOWN</asp:ListItem>
                                            <asp:ListItem>防御力DOWN</asp:ListItem>
                                            <asp:ListItem>AcceleMPUP</asp:ListItem>
                                            <asp:ListItem>状態異常耐性UP</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6 col-sm-6 col-xs-12 selector">
                                        <asp:DropDownList ID="menteA1" runat="server">
                                            <asp:ListItem Selected="True">精神強化アビ1</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>瀕死時攻撃力UP</asp:ListItem>
                                            <asp:ListItem>HP最大時攻撃力UP</asp:ListItem>
                                            <asp:ListItem>防御力UP</asp:ListItem>
                                            <asp:ListItem>瀕死時防御力UP</asp:ListItem>
                                            <asp:ListItem>HP最大時防御力UP</asp:ListItem>
                                            <asp:ListItem>クリティカル</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>Charge板ダメUP</asp:ListItem>
                                            <asp:ListItem>マギアダメージUP</asp:ListItem>
                                            <asp:ListItem>ドッペルダメージUP</asp:ListItem>
                                            <asp:ListItem>対魔女ダメージアップ</asp:ListItem>
                                            <asp:ListItem>Blast攻撃時MP獲得</asp:ListItem>
                                            <asp:ListItem>ダメUP状態</asp:ListItem>
                                            <asp:ListItem>ダメCUT状態</asp:ListItem>
                                            <asp:ListItem>属性ダメCUT状態</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避無効</asp:ListItem>
                                            <asp:ListItem>カウンター無効</asp:ListItem>
                                            <asp:ListItem>防御無視</asp:ListItem>
                                            <asp:ListItem>ダメージカット無視</asp:ListItem>
                                            <asp:ListItem>挑発無視</asp:ListItem>
                                            <asp:ListItem>呪い付与</asp:ListItem>
                                            <asp:ListItem>拘束付与</asp:ListItem>
                                            <asp:ListItem>魅了付与</asp:ListItem>
                                            <asp:ListItem>幻惑付与</asp:ListItem>
                                            <asp:ListItem>スタン付与</asp:ListItem>
                                            <asp:ListItem>毒付与</asp:ListItem>
                                            <asp:ListItem>やけど付与</asp:ListItem>
                                            <asp:ListItem>霧付与</asp:ListItem>
                                            <asp:ListItem>暗闇付与</asp:ListItem>
                                            <asp:ListItem>スキル不可付与</asp:ListItem>
                                            <asp:ListItem>スキルクイック</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>MP自動回復</asp:ListItem>
                                            <asp:ListItem>MP獲得量UP</asp:ListItem>
                                            <asp:ListItem>MP100以上時MP獲得量UP</asp:ListItem>
                                            <asp:ListItem>状態異常耐性UP</asp:ListItem>
                                            <asp:ListItem>呪い無効</asp:ListItem>
                                            <asp:ListItem>拘束無効</asp:ListItem>
                                            <asp:ListItem>魅了無効</asp:ListItem>
                                            <asp:ListItem>幻惑無効</asp:ListItem>
                                            <asp:ListItem>スタン無効</asp:ListItem>
                                            <asp:ListItem>毒無効</asp:ListItem>
                                            <asp:ListItem>スキル不可無効</asp:ListItem>
                                            <asp:ListItem>マギア不可無効</asp:ListItem>
                                            <asp:ListItem>マギアダメカット</asp:ListItem>
                                            <asp:ListItem>Charge数+</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="menteA2" runat="server">
                                            <asp:ListItem Selected="True">精神強化アビ2</asp:ListItem>
                                            <asp:ListItem>攻撃力UP</asp:ListItem>
                                            <asp:ListItem>瀕死時攻撃力UP</asp:ListItem>
                                            <asp:ListItem>HP最大時攻撃力UP</asp:ListItem>
                                            <asp:ListItem>防御力UP</asp:ListItem>
                                            <asp:ListItem>瀕死時防御力UP</asp:ListItem>
                                            <asp:ListItem>HP最大時防御力UP</asp:ListItem>
                                            <asp:ListItem>クリティカル</asp:ListItem>
                                            <asp:ListItem>ダメージUP</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>Charge板ダメUP</asp:ListItem>
                                            <asp:ListItem>マギアダメージUP</asp:ListItem>
                                            <asp:ListItem>ドッペルダメージUP</asp:ListItem>
                                            <asp:ListItem>対魔女ダメージアップ</asp:ListItem>
                                            <asp:ListItem>Blast攻撃時MP獲得</asp:ListItem>
                                            <asp:ListItem>ダメUP状態</asp:ListItem>
                                            <asp:ListItem>ダメCUT状態</asp:ListItem>
                                            <asp:ListItem>属性ダメCUT状態</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避無効</asp:ListItem>
                                            <asp:ListItem>カウンター無効</asp:ListItem>
                                            <asp:ListItem>防御無視</asp:ListItem>
                                            <asp:ListItem>ダメージカット無視</asp:ListItem>
                                            <asp:ListItem>挑発無視</asp:ListItem>
                                            <asp:ListItem>呪い付与</asp:ListItem>
                                            <asp:ListItem>拘束付与</asp:ListItem>
                                            <asp:ListItem>魅了付与</asp:ListItem>
                                            <asp:ListItem>幻惑付与</asp:ListItem>
                                            <asp:ListItem>スタン付与</asp:ListItem>
                                            <asp:ListItem>毒付与</asp:ListItem>
                                            <asp:ListItem>やけど付与</asp:ListItem>
                                            <asp:ListItem>霧付与</asp:ListItem>
                                            <asp:ListItem>暗闇付与</asp:ListItem>
                                            <asp:ListItem>スキル不可付与</asp:ListItem>
                                            <asp:ListItem>スキルクイック</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>MP自動回復</asp:ListItem>
                                            <asp:ListItem>MP獲得量UP</asp:ListItem>
                                            <asp:ListItem>MP100以上時MP獲得量UP</asp:ListItem>
                                            <asp:ListItem>状態異常耐性UP</asp:ListItem>
                                            <asp:ListItem>呪い無効</asp:ListItem>
                                            <asp:ListItem>拘束無効</asp:ListItem>
                                            <asp:ListItem>魅了無効</asp:ListItem>
                                            <asp:ListItem>幻惑無効</asp:ListItem>
                                            <asp:ListItem>スタン無効</asp:ListItem>
                                            <asp:ListItem>毒無効</asp:ListItem>
                                            <asp:ListItem>スキル不可無効</asp:ListItem>
                                            <asp:ListItem>マギア不可無効</asp:ListItem>
                                            <asp:ListItem>マギアダメカット</asp:ListItem>
                                            <asp:ListItem>Charge数+</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2 visible-lg selector">
                                        <asp:CheckBox ID="apareceAbiDetalle" runat="server" Text="アビ内訳" Enabled="False" />
                                    </div>
                                </div>
                                <div class ="row">
                                    <div class="col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-2 col-xs-4">                                        
                                                <h4 style="font-weight: bold">ソート設定</h4>
                                            </div>
                                            <div class="col-sm-4 col-xs-4 selector2">
                                                <asp:CheckBox ID="espiritu2" runat="server" Text="精神強化反映" />
                                            </div>
                                        </div>
                                        <div class="filterMagia">
                                            <asp:RadioButtonList ID="orden1" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>ATK</asp:ListItem>
                                                <asp:ListItem>DEF</asp:ListItem>
                                                <asp:ListItem>HP</asp:ListItem>
                                                <asp:ListItem>名前50音</asp:ListItem>
                                                <asp:ListItem Enabled="False">マギア値</asp:ListItem>
                                                <asp:ListItem Enabled="False">コネクト値</asp:ListItem>
                                                <asp:ListItem Enabled="False">精神スキル</asp:ListItem>
                                                <asp:ListItem Enabled="False">精神アビ</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <asp:Label ID="indicaPersonas" runat="server" Text="" Font-Bold="True" Font-Size="Small"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class ="col-xs-12 visible-xs">
                                <canvas id ="canvas3" width="280" height="140">Canvasに対応したブラウザを使用してください。</canvas>
                            </div>
                        </div>
                        <div class="row">
                            <div class ="col-xs-12 hidden-xs">
                                <canvas id ="canvas31" width="900" height="140">Canvasに対応したブラウザを使用してください。</canvas>
                            </div>
                        </div>
                        <div class="row">
                            <div class ="col-xs-12">
                                <asp:Label ID="debug" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <h4 style="font-weight: bold">戦闘スタイル</h4>
                                <asp:RadioButtonList ID="estadoAtk" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">3キャラ選択</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">同一キャラで攻撃</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class ="col-xs-12">
                                <h4 style="font-weight: bold">キャラ選択</h4>
                                <asp:Label ID="seleccionado1" runat="server"></asp:Label>
                                <asp:RadioButtonList ID="seleccionado" name="eleccion" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">1人目 : 無</asp:ListItem>
                                    <asp:ListItem Value="2" Enabled="False">2人目 : 無</asp:ListItem>
                                    <asp:ListItem Value="3" Enabled="False">3人目 : 無</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            
                            <%--<div class="collapse in" id="multiCollapseExample3">
                            <div id="collapseConnect" class="panel-collapse collapse">--%>
                                <div class="col-xs-12">
                                    <h4 style="font-weight: bold">コネクト選択</h4>
                                    <div class="filterMagia">
                                        <input id="radioConnect1" type="radio" name="ctl00$MainContent$seleccionado" value="connect1"/> <label for="radioConnect1">1回目 : 無</label>
                                        <input id="radioConnect2" type="radio" name="ctl00$MainContent$seleccionado" value="connect2"/> <label for="radioConnect2">2回目 : 無</label>
                                        <input id="radioConnect3" type="radio" name="ctl00$MainContent$seleccionado" value="connect3"/> <label for="radioConnect3">3回目 : 無</label>
                                    </div>
                                </div>
                            <%--</div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            
                        <%--<div class="row">
                            <div class="col-xs-12">
                                <p>
                                    <button id="atkCollapse" type="button" class="btn btn-info" data-toggle="collapse" data-target="#multiCollapseExample1" aria-expanded="false" aria-controls="multiCollapseExample1">攻撃設定 とじる</button>
                                    <button id="defCollapse" type="button" class="btn btn-danger" data-toggle="collapse" data-target="#multiCollapseExample2" aria-expanded="false" aria-controls="multiCollapseExample2">守備設定 とじる</button>
                                </p>
                            </div>
                        </div>--%>
                <div class="tab-pane" id="despierto">
                    <div class="row">
                    <%--攻撃側1--%>
                        <div class="col-sm-3 col-xs-12">
                            <div class="row">
                                <asp:Label ID="nombre1" runat="server" Text="攻撃側1人目 : 選択無" Font-Size="Medium"></asp:Label>
                                <%--<h5 style="font-weight: bold"><small>星5想定</small></h5>--%>
                            </div>
                        <canvas id ="canvas1" width ="150" height ="150">Canvasに対応したブラウザを使用してください。</canvas >
                        </div>
                        <div id="nombre23" class="panel-collapse collapse">
                        <%--攻撃側2--%>
                            <div class="col-sm-3 col-xs-12">
                                <div class="row">
                                    <asp:Label ID="nombre2" runat="server" Text="攻撃側2人目 : 選択無" Font-Size="Medium"></asp:Label>
                                    <%--<h5 style="font-weight: bold"><small>星5想定</small></h5>--%>
                                </div>
                            <canvas id ="canvas12" width ="150" height ="150">Canvasに対応したブラウザを使用してください。</canvas >
                            </div>
                            <%--攻撃側3--%>
                            <div class="col-sm-3 col-xs-12">
                                    <div class="row">
                                        <asp:Label ID="nombre3" runat="server" Text="攻撃側3人目 : 選択無" Font-Size="Medium"></asp:Label>
                                        <%--<h5 style="font-weight: bold"><small>星5想定</small></h5>--%>
                                    </div>
                                <canvas id ="canvas13" width ="150" height ="150">Canvasに対応したブラウザを使用してください。</canvas >
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="memoria">
                    <div class="noselect subtitle">
                    <div class="row">
                        <h4 style="font-weight: bold">フィルタ設定</h4>
                        <div class="col-sm-4 col-xs-12">
                            
                            <div class="filterMagia">
                                <asp:RadioButtonList ID="tipoMemoria" runat="server" RepeatDirection="Horizontal">
                                     <asp:ListItem Selected="True">全て</asp:ListItem>
                                    <asp:ListItem>スキル</asp:ListItem>
                                    <asp:ListItem>アビリティ</asp:ListItem>
                                </asp:RadioButtonList>
                                
                                <asp:CheckBoxList ID="rarityCheckM" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem value="4" Selected="True">☆4</asp:ListItem>
                                    <asp:ListItem value="3"  Selected="True">☆3専用</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-xs-12">
                                <asp:DropDownList ID="memoria1" runat="server">
                                    <asp:ListItem Selected="True">効果1</asp:ListItem>
                                    <asp:ListItem>攻撃力UP</asp:ListItem>
                                    <asp:ListItem>防御力UP</asp:ListItem>
                                    <asp:ListItem>ダメージUP</asp:ListItem>
                                    <asp:ListItem>防御力無視</asp:ListItem>
                                    <asp:ListItem>BlastダメUP</asp:ListItem>
                                    <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                    <asp:ListItem>クリティカル</asp:ListItem>
                                    <asp:ListItem>ダメUP状態</asp:ListItem>
                                    <asp:ListItem>ダメCUT状態</asp:ListItem>
                                    <asp:ListItem>かばう</asp:ListItem>
                                    <asp:ListItem>攻撃力DOWN</asp:ListItem>
                                    <asp:ListItem>防御力DOWN</asp:ListItem>
                                    <asp:ListItem>ダメージDOWN</asp:ListItem>
                                    <asp:ListItem>状態異常耐性DOWN</asp:ListItem>
                                    <asp:ListItem>MP獲得量DOWN</asp:ListItem>
                                    <asp:ListItem>回避</asp:ListItem>
                                    <asp:ListItem>回避無効</asp:ListItem>
                                    <asp:ListItem>MP獲得量UP</asp:ListItem>
                                    <asp:ListItem>AcceleMPUP</asp:ListItem>
                                    <asp:ListItem>MP溜まった状態</asp:ListItem>
                                    <asp:ListItem>状態異常耐性UP</asp:ListItem>
                                    <asp:ListItem>呪い無効</asp:ListItem>
                                    <asp:ListItem>拘束無効</asp:ListItem>
                                    <asp:ListItem>魅了無効</asp:ListItem>
                                    <asp:ListItem>幻惑無効</asp:ListItem>
                                    <asp:ListItem>暗闇無効</asp:ListItem>
                                    <asp:ListItem>スタン無効</asp:ListItem>
                                    <asp:ListItem>霧無効</asp:ListItem>
                                    <asp:ListItem>デバフ無効</asp:ListItem>
                                    <asp:ListItem>挑発</asp:ListItem>
                                    <asp:ListItem>HP自動回復</asp:ListItem>
                                    <asp:ListItem>HP回復</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="memoria2" runat="server">
                                    <asp:ListItem Selected="True">効果2</asp:ListItem>
                                    <asp:ListItem>攻撃力UP</asp:ListItem>
                                    <asp:ListItem>防御力UP</asp:ListItem>
                                    <asp:ListItem>ダメージUP</asp:ListItem>
                                    <asp:ListItem>防御力無視</asp:ListItem>
                                    <asp:ListItem>BlastダメUP</asp:ListItem>
                                    <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                    <asp:ListItem>クリティカル</asp:ListItem>
                                    <asp:ListItem>ダメUP状態</asp:ListItem>
                                    <asp:ListItem>ダメCUT状態</asp:ListItem>
                                    <asp:ListItem>かばう</asp:ListItem>
                                    <asp:ListItem>攻撃力DOWN</asp:ListItem>
                                    <asp:ListItem>防御力DOWN</asp:ListItem>
                                    <asp:ListItem>ダメージDOWN</asp:ListItem>
                                    <asp:ListItem>状態異常耐性DOWN</asp:ListItem>
                                    <asp:ListItem>MP獲得量DOWN</asp:ListItem>
                                    <asp:ListItem>回避</asp:ListItem>
                                    <asp:ListItem>回避無効</asp:ListItem>
                                    <asp:ListItem>MP獲得量UP</asp:ListItem>
                                    <asp:ListItem>MP溜まった状態</asp:ListItem>
                                    <asp:ListItem>AcceleMPUP</asp:ListItem>
                                    <asp:ListItem>状態異常耐性UP</asp:ListItem>
                                    <asp:ListItem>呪い無効</asp:ListItem>
                                    <asp:ListItem>拘束無効</asp:ListItem>
                                    <asp:ListItem>魅了無効</asp:ListItem>
                                    <asp:ListItem>幻惑無効</asp:ListItem>
                                    <asp:ListItem>暗闇無効</asp:ListItem>
                                    <asp:ListItem>スタン無効</asp:ListItem>
                                    <asp:ListItem>霧無効</asp:ListItem>
                                    <asp:ListItem>デバフ無効</asp:ListItem>
                                    <asp:ListItem>挑発</asp:ListItem>
                                    <asp:ListItem>HP自動回復</asp:ListItem>
                                    <asp:ListItem>HP回復</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <asp:DropDownList ID="diskM" runat="server">
                                <asp:ListItem Selected="True">ディスク系</asp:ListItem>
                                    <asp:ListItem>Aドロー</asp:ListItem>
                                    <asp:ListItem>Bドロー</asp:ListItem>
                                    <asp:ListItem>Cドロー</asp:ListItem>
                                    <asp:ListItem>自分のDisc</asp:ListItem>
                                    <asp:ListItem>再度引く</asp:ListItem>
                                    <asp:ListItem>同じ属性</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12">
                            <h4 style="font-weight: bold">ソート設定</h4>
                            <asp:RadioButtonList ID="ordenMemoria" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">50音</asp:ListItem>
                                <asp:ListItem>ATK</asp:ListItem>
                                <asp:ListItem>DEF</asp:ListItem>
                                <asp:ListItem>HP</asp:ListItem>
                                <asp:ListItem>ミラーズ発動T</asp:ListItem>
                                <asp:ListItem Enabled="False">効果値</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-xs-12">
                            <h4 style="font-weight: bold">凸設定<small>（効果値ソート用）</small></h4>
                            <asp:RadioButtonList ID="totuM" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">LV上限50(完凸)</asp:ListItem>
                                    <asp:ListItem Value="0">LV上限45以下</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-xs-12">
                            <asp:Label ID="indicaMemoria" runat="server" Text="" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </div>
                    </div>
                    <div class ="col-xs-12 visible-xs">
                        <canvas id="canvas5" width="280" height ="500">Canvasに対応したブラウザを使用してください。</canvas>
                    </div>
                    <div class ="col-xs-12 hidden-xs">
                        <canvas id="canvas51" width="900" height ="500">Canvasに対応したブラウザを使用してください。</canvas>
                    </div>
                        <!-- モーダル・ダイアログ -->
                            <div class="modal" id="selectModal" tabindex="-1">
	                            <div class="modal-dialog">
		                            <div class="modal-content">
			                            <%--<div class="modal-header">
				                            <button type="button" class="close" data-dismiss="modal"><span>×</span></button>
			                            </div>--%>
			                            <div class="modal-body">
                                            <div class="row">
                                                <div style="overflow-x:scroll">
                                                    <canvas id ="canvasM" width ="510" height ="150">Canvasに対応したブラウザを使用してください。</canvas >                                                    
                                                </div>
                                            </div>
                                            <div class="modalDialog">
                                                <div style="overflow-x:hidden">
                                                    <div class="row">
                                                        <div class="col-sm-4 col-xs-12">
                                                            <div id="grid1m"></div>
                                                        </div>
                                                        <div class="col-sm-4 col-xs-12">
                                                            <div id="grid2m"></div>
                                                        </div>
                                                        <div class="col-sm-4 col-xs-12">
                                                            <div id="grid3m"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
			                            <div class="modal-footer">
                                            <asp:CheckBox ID="quitaM" runat="server" text="メモリア外す"/>
				                            <button type="button" class="btn btn-default" data-dismiss="modal">閉じる</button>
			                            </div>
		                            </div>
	                            </div>
                            </div>
                    </div>
                </div>
                <div class="tab-pane" id="enemigo">
                    
                        <div class="col-xs-12">
                            <div class="row">
                                <div class="col-sm-4 col-xs-12">
                                    <h5 style="font-weight: bold">相手側覚醒<small>DEFのみ可</small></h5>
                                    <canvas id ="canvas2" width ="150" height ="100">Canvasに対応したブラウザを使用してください。</canvas >
                                </div>
                                <div class="col-sm-4 col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <h5 style="font-weight: bold">相手側DEF</h5>
                                            <asp:TextBox ID="TextBox668" runat="server" Text="0" Width="70px"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-6">
                                            <h5 style="font-weight: bold">メモリアDEF</h5>
                                            <asp:TextBox ID="TextBox669" runat="server" Text="0" Width="65px"></asp:TextBox>
                                        </div>
                                        <%--<div class="col-sm-6 col-xs-12">
                                            <h5 style="font-weight: bold">タイプ</h5>
                                                <asp:DropDownList ID="tipoPuella4" runat="server">
                                                    <asp:ListItem>マギア</asp:ListItem>
                                                    <asp:ListItem>サポート</asp:ListItem>
                                                    <asp:ListItem Selected="True">アタック</asp:ListItem>
                                                    <asp:ListItem>ヒール</asp:ListItem>
                                                    <asp:ListItem>バランス</asp:ListItem>
                                                    <asp:ListItem>ディフェンス</asp:ListItem>
                                                    <asp:ListItem>アルティメット</asp:ListItem>
                                                </asp:DropDownList>
                                        </div>--%>
                                        <div class ="col-sm-6 col-xs-12">
                                            <h5 style="font-weight: bold">陣形効果 守備上昇</h5>
                                            <asp:RadioButtonList ID="ordendeBatalla4" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="0">無</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                    
                                <div class ="col-sm-4 col-xs-12">
                                    <div class="row">
                                        <h5 style="font-style: italic; font-size: medium; font-weight: bold">メモリア設定</h5>
                                    </div>
                                    <div class="row" style="margin-bottom:3px">
                                        <h5 style="font-weight: bold">メモリア守備力UP</h5>
                                        <asp:TextBox ID="DefUp" runat="server" Width="90" Text="0%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    
                </div>
                <div class="tab-pane" id="mirrors">
                    <asp:Label ID="Mtotal" runat="server" Text="あなたのポイントは 0ptです" Font-Bold="True" Font-Size="Large" Font-Italic="True" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="MturnL" runat="server" Text="戦闘ターン数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mturn" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>1T</asp:ListItem>
                        <asp:ListItem Selected="True">2T</asp:ListItem>
                        <asp:ListItem>3T</asp:ListItem>
                        <asp:ListItem>4T</asp:ListItem>
                        <asp:ListItem>5T</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MconnectL" runat="server" Text="コネクト数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mconnect" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">0回</asp:ListItem>
                         <asp:ListItem>1回</asp:ListItem>
                        <asp:ListItem>2回</asp:ListItem>
                        <asp:ListItem>3回</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MmagiaL" runat="server" Text="マギア発動回数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mmagia" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">0回</asp:ListItem>
                        <asp:ListItem>1回</asp:ListItem>
                        <asp:ListItem>2回</asp:ListItem>
                        <asp:ListItem>3回</asp:ListItem>
                    </asp:RadioButtonList>
                     <asp:Label ID="MdoppelL" runat="server" Text="ドッペル発動回数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mdoppel" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">0回</asp:ListItem>
                        <asp:ListItem>1回</asp:ListItem>
                        <asp:ListItem>2回</asp:ListItem>
                        <asp:ListItem>3回</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MskillL" runat="server" Text="スキル回数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mskill" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">0回</asp:ListItem>
                         <asp:ListItem>1回</asp:ListItem>
                        <asp:ListItem>2回</asp:ListItem>
                        <asp:ListItem>3回</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MhpL" runat="server" Text="残HPボーナス" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mhp" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">100-80%</asp:ListItem>
                        <asp:ListItem Value="2">70-70%</asp:ListItem>
                        <asp:ListItem Value="3">69-60%</asp:ListItem>
                        <asp:ListItem Value="4">59-50%</asp:ListItem>
                        <asp:ListItem Value="5">49%-</asp:ListItem>
                    </asp:RadioButtonList>
                    <%--<div class="row">
                        <div class="col-lg-1 col-xs-2">
                            <p>100-80%</p>
                            <p><input type="number" id= "Mhp1" name="Mhp1" min="0" max="5" value = "0"></p>
                        </div>
                        <div class="col-lg-1 col-xs-2">
                            <p>79-70%</p>
                            <p><input type="number" id= "Mhp2" name="Mhp2" min="0" max="5" value = "0"></p>
                        </div>
                        <div class="col-lg-1 col-xs-2">
                            <p>69-60%</p>
                            <p><input type="number" id= "Mhp3" name="Mhp3" min="0" max="5" value = "0"></p>
                        </div>
                        <div class="col-lg-1 col-xs-2">
                            <p>59-50%</p>
                            <p><input type="number" id= "Mhp4" name="Mhp4" min="0" max="5" value = "0"></p>
                        </div>
                        <div class="col-lg-1 col-xs-2">
                            <p>49-0%</p>
                            <p><input type="number" id= "Mhp5" name="Mhp5" min="0" max="5" value = "0"></p>
                        </div>
                    </div>--%>
                    <asp:Label ID="Msurvive" runat="server" Text="生存人数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <div class="filterMagia">
                    <p>編成人数</p>
                        <asp:RadioButtonList ID="Mpt" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">5人PT</asp:ListItem>
                            <asp:ListItem>4人PT</asp:ListItem>
                            <asp:ListItem>3人PT</asp:ListItem>
                            <asp:ListItem>2人PT</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="filterMagia">
                        <p>死亡者数</p>
                        <asp:RadioButtonList ID="Mdeath" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">0人</asp:ListItem>
                            <asp:ListItem>1人</asp:ListItem>
                            <asp:ListItem>2人</asp:ListItem>
                            <asp:ListItem>3人</asp:ListItem>
                            <asp:ListItem>4人</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <asp:Label ID="MbonusL" runat="server" Text="戦力ボーナス" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mbonus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True"  Value="1">1倍</asp:ListItem>
                         <asp:ListItem Value="1.1">1.1倍</asp:ListItem>
                        <asp:ListItem Value="1.15">1.15倍</asp:ListItem>
                        <asp:ListItem Value="1.2">1.2倍</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MbreakL" runat="server" Text="ブレークポイント" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mbreak" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">1倍</asp:ListItem>
                         <asp:ListItem Value="1.5">1.5倍</asp:ListItem>
                        <asp:ListItem Value="2">2倍</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                
        </div>
              </div>
        </div>
        <div class ="row">
            <div class="col-sm-4 col-xs-12 bg-info">
                <div class="noselect subtitle">
                    <h4 style="font-weight: bold">1回目の攻撃</h4>
                
                    <div class="row">
                        <div class="col-xs-6">
                            <canvas id="canvas61" width="160" height ="120">Canvasに対応したブラウザを使用してください。</canvas>
                        </div>
                        <div class="col-xs-6">
                            <div id="grid1a"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:RadioButtonList ID="RadioButtonList666" runat="server" Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem Value="A">Accel</asp:ListItem>
                                <asp:ListItem Selected="True" Value="B">Blast</asp:ListItem>
                                <asp:ListItem Value="C">Charge</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-xs-12 bg-info">
                <div class="noselect subtitle">
                    <h4 style="font-weight: bold">2回目の攻撃</h4>
                    <div class="row">
                        <div class="col-xs-6">
                            <canvas id="canvas62" width="160" height ="120">Canvasに対応したブラウザを使用してください。</canvas>
                        </div>
                        <div class="col-xs-6">
                            <div id="grid2a"></div>
                        </div>
                    </div>
                    <div class="row">
                         <div class="form-group">
                            <asp:RadioButtonList ID="RadioButtonList667" runat="server" Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem Value="A">Accel</asp:ListItem>
                                <asp:ListItem Value="B" Selected="True">Blast</asp:ListItem>
                                <asp:ListItem Value="C">Charge</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-xs-12 bg-info">
                <div class="noselect subtitle">
                    <h4 style="font-weight: bold">3回目の攻撃</h4>
                    <div class="row">
                        <div class="col-xs-6">
                            <canvas id="canvas63" width="160" height ="120">Canvasに対応したブラウザを使用してください。</canvas>
                        </div>
                        <div class="col-xs-6">
                            <div id="grid3a"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:RadioButtonList ID="RadioButtonList668" runat="server" Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem Value="A">Accel</asp:ListItem>
                                <asp:ListItem Value="B" Selected="True">Blast</asp:ListItem>
                                <asp:ListItem Value="C">Charge</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6">
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList669" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="A">全A</asp:ListItem>
                        <asp:ListItem Value="B" Selected="True">全B</asp:ListItem>
                        <asp:ListItem Value="C">全C</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="col-xs-6">
                <div class="statusMagia">
                    <h6 style="font-weight: bold">溜まったチャージ数</h6>
                    <p><input type="number" id= "chargePlus" name="chargePlus" min="0" max="20" value = "0"></p>
                </div>
            </div>
        </div>
        <!--結果表示-->
        <div class="resultado">
            <div class="row magireco">
                <div class="col-xs-3 bg-success">
                    <div class="row">
                        <div class="col-sm-12 col-md-5">
                            <div id ="grande11"><h4 style="font-weight: bold">補正1</h4></div>
                            <div id ="pequeno11" class="hidden"><h4 style="font-weight: bold">補正</h4></div>
                            <asp:Label ID="modificado1" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                            <div id ="grande12"><h4 style="font-weight: bold">ダメージ1</h4></div>
                            <div id ="pequeno12" class="hidden"><h4 style="font-weight: bold">ダメ</h4></div>
                            <asp:Label ID="dano1" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                            <div id ="grande13"><h4 style="font-weight: bold">獲得MP1</h4></div>
                            <div id ="pequeno13" class="hidden"><h4 style="font-weight: bold">MP増</h4></div>
                            <asp:Label ID="magia1" runat="server" Text="0" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-7 hidden-xs hidden-sm">
                            <div id="grid1"></div>
                        </div>
                    </div>

                </div>
                <div class="col-xs-3 bg-success">
                    <div class="row">
                        <div class="col-sm-12 col-md-5">
                            <div id ="grande21"><h4 style="font-weight: bold">補正2</h4></div>
                            <div id ="pequeno21" class="hidden"><h4 style="font-weight: bold">補正</h4></div>
                            <asp:Label ID="modificado2" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                            <div id ="grande22"><h4 style="font-weight: bold">ダメージ2</h4></div>
                            <div id ="pequeno22" class="hidden"><h4 style="font-weight: bold">ダメ</h4></div>
                            <asp:Label ID="dano2" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                            <div id ="grande23"><h4 style="font-weight: bold">獲得MP2</h4></div>
                            <div id ="pequeno23" class="hidden"><h4 style="font-weight: bold">MP増</h4></div>
                            <asp:Label ID="magia2" runat="server" Text="0" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-7 hidden-xs hidden-sm">
                            <div id="grid2"></div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-3 bg-success">
                    <div class="row">
                        <div class="col-sm-12 col-md-5">
                            <div id ="grande31"><h4 style="font-weight: bold">補正3</h4></div>
                            <div id ="pequeno31" class="hidden"><h4 style="font-weight: bold">補正</h4></div>
                            <asp:Label ID="modificado3" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                            <div id ="grande32"><h4 style="font-weight: bold">ダメージ3</h4></div>
                            <div id ="pequeno32" class="hidden"><h4 style="font-weight: bold">ダメ</h4></div>
                            <asp:Label ID="dano3" runat="server" Text="1.0" ForeColor="Red"></asp:Label>
                            <div id ="grande33"><h4 style="font-weight: bold">獲得MP3</h4></div>
                            <div id ="pequeno33" class="hidden"><h4 style="font-weight: bold">MP増</h4></div>
                            <asp:Label ID="magia3" runat="server" Text="0" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-7 hidden-xs hidden-sm">
                            <div id="grid3"></div>
                        </div>
                    </div>
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
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-sm-6 col-xs-12">
                    <asp:CheckBox ID="decimal" runat="server" Text="MP小数表示" />
                    <asp:CheckBox ID="espiritu" runat="server" Text="精神強化反映" />
                </div>
            </div>
        </div>
        
    </section>
</asp:Content>