<%@ Page Title="マギレコダメ計算" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Magia_damage_calc.aspx.cs" Inherits="花騎士ツール＿NEO.Magia_damage_calc" MaintainScrollPositionOnPostback="true" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="container">
        <h3>マギレコ簡易ダメ計算<small>（超ベ〇ータ3版）</small></h3>
        <h5>算出されたダメージ値に乱数はかかっていません</h5>
        <h5>キャラデータは星5 LV100を想定しています</h5>
        <div class="row">
            
                <div class="col-sm-3 col-xs-6">
                    <h4 style="font-weight: bold">戦場</h4>
                    <asp:RadioButtonList ID="ventana" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">ミラーズ</asp:ListItem>
                        <asp:ListItem Value="0">クエスト</asp:ListItem>
                    </asp:RadioButtonList>
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
        
        <ul class="nav nav-tabs">
	        <li class="active"><a href="#persona" data-toggle="tab">キャラ選択</a></li>
	        <li><a href="#despierto" data-toggle="tab">覚醒補正</a></li>
	        <li><a href="#memoria" data-toggle="tab">メモリア</a></li>
	        <li><a href="#enemigo" data-toggle="tab">相手側設定</a></li>
	        <%--<li><a href="#mirrors" data-toggle="tab">ミラランpt</a></li>--%>
        </ul>
        <div class="panel panel-info">
          <div class="panel-body">
        <div class="tab-content">
	        <div class="tab-pane active" id="persona">
                <%--キャラ設定スタート--%>

                        <div class="row">
                            <div class="col-xs-12">
                                <h4 style="font-weight: bold">フィルタ設定</h4>
                                <div class ="row">
                                    <div class ="col-lg-4 col-sm-6 col-xs-12" style="margin-bottom:5px">
                                        <div class="filterMagia">
                                        <asp:CheckBoxList ID="filtro1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">全</asp:ListItem>
                                            <asp:ListItem Selected="True">光</asp:ListItem>
                                            <asp:ListItem Selected="True">闇</asp:ListItem>
                                            <asp:ListItem Selected="True">火</asp:ListItem>
                                            <asp:ListItem Selected="True">水</asp:ListItem>
                                            <asp:ListItem Selected="True">木</asp:ListItem>
                                            <asp:ListItem Selected="True">無</asp:ListItem>
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
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class ="col-lg-1 col-sm-6 col-xs-12" style="margin-bottom:5px">
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
                                    <div class ="col-lg-4 col-sm-6 col-xs-12" style="margin-bottom:5px">
                                        <div class="filterMagia">
                                        <asp:DropDownList ID="tipoMagia" runat="server">
                                            <asp:ListItem Selected="True">マギア指定1</asp:ListItem>
                                            <asp:ListItem>敵全体</asp:ListItem>
                                            <asp:ListItem>敵単体</asp:ListItem>
                                            <asp:ListItem>敵縦一列</asp:ListItem>
                                            <asp:ListItem>ランダム</asp:ListItem>
                                            <asp:ListItem>属性強化</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>MP回復</asp:ListItem>
                                            <asp:ListItem>低HPほど威力UP</asp:ListItem>
                                            <asp:ListItem>状態強化解除</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="tipoMagia2" runat="server">
                                            <asp:ListItem Selected="True">マギア指定2</asp:ListItem>
                                            <asp:ListItem>敵全体</asp:ListItem>
                                            <asp:ListItem>敵単体</asp:ListItem>
                                            <asp:ListItem>敵縦一列</asp:ListItem>
                                            <asp:ListItem>ランダム</asp:ListItem>
                                            <asp:ListItem>属性強化</asp:ListItem>
                                            <asp:ListItem>BlastダメUP</asp:ListItem>
                                            <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>HP自動回復</asp:ListItem>
                                            <asp:ListItem>MP回復</asp:ListItem>
                                            <asp:ListItem>低HPほど威力UP</asp:ListItem>
                                            <asp:ListItem>状態強化解除</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-lg-4 col-sm-6 col-xs-12" style="margin-bottom:5px">
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
                                <div class ="row">
                                    <div class="col-xs-12">
                                        <h4 style="font-weight: bold">ソート設定</h4>
                                        <div class="filterMagia">
                                            <asp:RadioButtonList ID="orden1" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>ATK</asp:ListItem>
                                                <asp:ListItem>DEF</asp:ListItem>
                                                <asp:ListItem>HP</asp:ListItem>
                                                <asp:ListItem>名前50音</asp:ListItem>
                                                <asp:ListItem Enabled="False">マギア値</asp:ListItem>
                                                <asp:ListItem Enabled="False">コネクト値</asp:ListItem>
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
                                    <asp:ListItem Value="1" Selected="True">1人目選択 : 無</asp:ListItem>
                                    <asp:ListItem Value="2" Enabled="False">2人目選択 : 無</asp:ListItem>
                                    <asp:ListItem Value="3" Enabled="False">3人目選択 : 無</asp:ListItem>
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
                    <div class="row">
                        <h4 style="font-weight: bold">フィルタ設定</h4>
                        <div class="col-sm-4 col-xs-12">
                            
                            <div class="filterMagia">
                                <asp:RadioButtonList ID="tipoMemoria" runat="server" RepeatDirection="Horizontal">
                                     <asp:ListItem Selected="True">全て</asp:ListItem>
                                    <asp:ListItem>スキル</asp:ListItem>
                                    <asp:ListItem>アビリティ</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RadioButtonList ID="rarityM" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem value="4" Selected="True">☆4</asp:ListItem>
                                    <asp:ListItem value="3" Enabled="False">☆3</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-xs-12">
                                <asp:DropDownList ID="memoria1" runat="server">
                                    <asp:ListItem Selected="True">効果1</asp:ListItem>
                                    <asp:ListItem>攻撃力UP</asp:ListItem>
                                    <asp:ListItem>防御力UP</asp:ListItem>
                                    <asp:ListItem>ダメージUP</asp:ListItem>
                                    <asp:ListItem>BlastダメUP</asp:ListItem>
                                    <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                    <asp:ListItem>クリティカル</asp:ListItem>
                                    <asp:ListItem>ダメUP状態</asp:ListItem>
                                    <asp:ListItem>ダメCUT状態</asp:ListItem>
                                    <asp:ListItem>回避</asp:ListItem>
                                    <asp:ListItem>回避無効</asp:ListItem>
                                    <asp:ListItem>MP獲得量UP</asp:ListItem>
                                    <asp:ListItem>AcceleMPUP</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="memoria2" runat="server">
                                    <asp:ListItem Selected="True">効果2</asp:ListItem>
                                    <asp:ListItem>攻撃力UP</asp:ListItem>
                                    <asp:ListItem>防御力UP</asp:ListItem>
                                    <asp:ListItem>ダメージUP</asp:ListItem>
                                    <asp:ListItem>BlastダメUP</asp:ListItem>
                                    <asp:ListItem>Charge後ダメUP</asp:ListItem>
                                    <asp:ListItem>クリティカル</asp:ListItem>
                                    <asp:ListItem>ダメUP状態</asp:ListItem>
                                    <asp:ListItem>ダメCUT状態</asp:ListItem>
                                    <asp:ListItem>回避</asp:ListItem>
                                    <asp:ListItem>回避無効</asp:ListItem>
                                    <asp:ListItem>MP獲得量UP</asp:ListItem>
                                    <asp:ListItem>AcceleMPUP</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <asp:RadioButtonList ID="totuM" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">LV上限50(完凸)</asp:ListItem>
                                    <asp:ListItem Value="0">LV上限45以下</asp:ListItem>
                            </asp:RadioButtonList>
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
                    </div>
                    <div class ="col-xs-12 visible-xs">
                        <canvas id="canvas5" width="280" height ="500">Canvasに対応したブラウザを使用してください。</canvas>
                    </div>
                    <div class ="col-xs-12 hidden-xs">
                        <canvas id="canvas51" width="900" height ="500">Canvasに対応したブラウザを使用してください。</canvas>
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
                                        <div class="col-sm-6 col-xs-12">
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
                                        </div>
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
                        <asp:ListItem>4回</asp:ListItem>
                        <asp:ListItem>5回</asp:ListItem>
                        <asp:ListItem>6回</asp:ListItem>
                    </asp:RadioButtonList>
                     <asp:Label ID="MdoppelL" runat="server" Text="ドッペル発動回数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mdoppel" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">0回</asp:ListItem>
                        <asp:ListItem>1回</asp:ListItem>
                        <asp:ListItem>2回</asp:ListItem>
                        <asp:ListItem>3回</asp:ListItem>
                        <asp:ListItem>4回</asp:ListItem>
                        <asp:ListItem>5回</asp:ListItem>
                        <asp:ListItem>6回</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MskillL" runat="server" Text="スキル回数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:RadioButtonList ID="Mskill" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">0回</asp:ListItem>
                         <asp:ListItem>1回</asp:ListItem>
                        <asp:ListItem>2回</asp:ListItem>
                        <asp:ListItem>3回</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="MhpL" runat="server" Text="残HP人数" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <div class="row">
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
                    </div>
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
                <h4 style="font-weight: bold">1回目の攻撃</h4>
                <div class="col-xs-5">
                    <p id="1a">未選択</p>
                </div>
                <div class="col-xs-7">
                    <div id="grid1a"></div>
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
            <div class="col-sm-4 col-xs-12 bg-info">
                <h4 style="font-weight: bold">2回目の攻撃</h4>
                <div class="col-xs-5">
                    <p id="2a">未選択</p>
                </div>
                <div class="col-xs-7">
                    <div id="grid2a"></div>
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
            <div class="col-sm-4 col-xs-12 bg-info">
                <h4 style="font-weight: bold">3回目の攻撃</h4>
                <div class="col-xs-5">
                    <p id="3a">未選択</p>
                </div>
                <div class="col-xs-7">
                    <div id="grid3a"></div>
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
        <div class="row">
            <div class="col-xs-3 bg-success">
                <div class="row">
                    <div class="col-sm-12 col-md-5">
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
                    <div class="col-sm-7 hidden-xs hidden-sm">
                        <div id="grid1"></div>
                    </div>
                </div>

            </div>
            <div class="col-xs-3 bg-success">
                <div class="row">
                    <div class="col-sm-12 col-md-5">
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
                    <div class="col-sm-7 hidden-xs hidden-sm">
                        <div id="grid2"></div>
                    </div>
                </div>
            </div>
            <div class="col-xs-3 bg-success">
                <div class="row">
                    <div class="col-sm-12 col-md-5">
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
        <div class="row">
            <div class="col-xs-12">
                <div class="col-sm-6 col-xs-12">
                    <asp:CheckBox ID="decimal" runat="server" Text="MP小数表示" />
                </div>
            </div>
        </div>
        
    </section>
</asp:Content>