<%@ Page Title="アビリティ検索" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Fkg_ability_serch_neo.aspx.cs" Inherits="花騎士ツール＿NEO.Fkg_ability_serch" MaintainScrollPositionOnPostback="true" %>
    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
            <div class="container-fluid">
                <div class="row">
                    <div class="col-xs-12">
                        <h2>検索設定項目</h2>
                        <p>下記の検索項目を設定すると、そのアビリティを持つ花騎士を絞り込んで表示します。</p>
                        <h4>レアリティ</h4>
                        <div class="form-group">
                            <asp:UpdatePanel ID ="UpdatePanel105" runat="server" >
                                <ContentTemplate>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="6">☆6</asp:ListItem>
                                <asp:ListItem Enabled="False" Value="5">☆5</asp:ListItem>
                                <asp:ListItem Enabled="False">その他</asp:ListItem>
                                <asp:ListItem>昇華虹のみ</asp:ListItem>
                                <asp:ListItem>通常虹のみ</asp:ListItem>
                            </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <h4>属性</h4>
                        <asp:UpdatePanel ID ="UpdatePanel108" runat="server" >
                            <ContentTemplate>
                        <button class="btn btn-default btn-xs" id="checkAttAll" onClick="CheckAbiATT()">全チェック</button>
                        <button class="btn btn-default btn-xs" id="uncheckAttAll" onClick="UnCheckAbiATT()">全消</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-group">
                        <asp:UpdatePanel ID ="UpdatePanel106" runat="server" >
                            <ContentTemplate>
                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Selected="True">斬</asp:ListItem>
                            <asp:ListItem Selected="True">打</asp:ListItem>
                            <asp:ListItem Selected="True">突</asp:ListItem>
                            <asp:ListItem Selected="True">魔</asp:ListItem>
                        </asp:CheckBoxList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        <h4>スキルタイプ</h4>
                        <asp:UpdatePanel ID ="UpdatePanel109" runat="server" >
                            <ContentTemplate>
                        <button class="btn btn-default btn-xs" id="checkStypeAll" onClick="CheckAbiStype()">全チェック</button>
                        <button class="btn btn-default btn-xs" id="uncheckStypeAll" onClick="UnCheckAbiStype()">全消</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-group">
                            <asp:UpdatePanel ID ="UpdatePanel107" runat="server" >
                                <ContentTemplate>
                            <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="CheckBoxList3_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="全体">全</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2体">2</asp:ListItem>
                                <asp:ListItem Selected="True" Value="変則">変</asp:ListItem>
                                <asp:ListItem Selected="True" Value="複数回">複</asp:ListItem>
                                <asp:ListItem Selected="True" Value="吸収">吸</asp:ListItem>
                                <asp:ListItem Selected="True" Value="単体">単</asp:ListItem>
                            </asp:CheckBoxList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <h4>付与属性</h4>
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel110" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="CheckBoxList4" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="CheckBoxList4_SelectedIndexChanged">
                                        <asp:ListItem>斬</asp:ListItem>
                                        <asp:ListItem>打</asp:ListItem>
                                        <asp:ListItem>突</asp:ListItem>
                                        <asp:ListItem>魔</asp:ListItem>
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-xs-12">
                            <h4 style="font-weight: bold">検索アビリティ1</h4>
                            <div class="form-group">
                                <asp:UpdatePanel ID ="UpdatePanel111" runat="server" >
                                <ContentTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1Changed" AutoPostBack="true">
                                    <asp:ListItem>選択無し</asp:ListItem>
                                    <asp:ListItem>スキル発動率1.2倍上昇</asp:ListItem>
                                    <asp:ListItem>スキル発動率1.65倍上昇</asp:ListItem>
                                    <asp:ListItem>攻撃を受けた次ターンにスキル発動率上昇</asp:ListItem>
                                    <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                                    <asp:ListItem>スキル発動率上昇</asp:ListItem>
                                    <asp:ListItem>スキルダメージ増加</asp:ListItem>
                                    <asp:ListItem>クリティカル率上昇</asp:ListItem>
                                    <asp:ListItem>クリティカルダメージ増加</asp:ListItem>
                                    <asp:ListItem>クリティカル率上昇（PT全体対象）</asp:ListItem>
                                    <asp:ListItem>クリティカルダメージ増加（PT全体対象）</asp:ListItem>
                                    <asp:ListItem>攻撃力上昇</asp:ListItem>
                                    <asp:ListItem>ターン毎攻撃力上昇</asp:ListItem>
                                    <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                                    <asp:ListItem>ボスに対して攻撃力上昇</asp:ListItem>
                                    <asp:ListItem>ダメージ増加</asp:ListItem>
                                    <asp:ListItem>ターン毎ダメージ増加</asp:ListItem>
                                    <asp:ListItem>回避</asp:ListItem>
                                    <asp:ListItem>回避付与</asp:ListItem>
                                    <asp:ListItem>迎撃</asp:ListItem>
                                    <asp:ListItem>反撃</asp:ListItem>
                                    <asp:ListItem>反撃（超反撃有）</asp:ListItem>
                                    <asp:ListItem>再行動</asp:ListItem>
                                    <asp:ListItem>再行動付与</asp:ListItem>
                                    <asp:ListItem>防御力・ダメージ軽減率上昇</asp:ListItem>
                                    <asp:ListItem>攻撃力低下</asp:ListItem>
                                    <asp:ListItem>防御力低下</asp:ListItem>
                                    <asp:ListItem>スキル発動率低下</asp:ListItem>
                                    <asp:ListItem>命中率低下</asp:ListItem>                                
                                    <asp:ListItem>行動回数減少</asp:ListItem>
                                    <asp:ListItem>追撃</asp:ListItem>
                                    <asp:ListItem>追撃付与</asp:ListItem>
                                    <asp:ListItem>ダメージ無効化</asp:ListItem>
                                    <asp:ListItem>属性付与</asp:ListItem>
                                    <asp:ListItem>弱点属性ダメ増加</asp:ListItem>
                                    <asp:ListItem>HP回復</asp:ListItem>
                                    <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                                    <asp:ListItem>シャインクリスタルドロップ率上昇</asp:ListItem>
                                    <asp:ListItem>光ゲージ充填</asp:ListItem>
                                    <asp:ListItem>移動力増加</asp:ListItem>
                                    <asp:ListItem>スキルにHP吸収付与</asp:ListItem>
                                    <asp:ListItem>イロモノ系</asp:ListItem>
                                    <asp:ListItem>イロモノサブアビ</asp:ListItem>
                                    <asp:ListItem>1ターン目系</asp:ListItem>
                                    <asp:ListItem>スキル：全体</asp:ListItem>
                                    <asp:ListItem>スキル：2体</asp:ListItem>
                                    <asp:ListItem>スキル：変則</asp:ListItem>
                                    <asp:ListItem>スキル：複数回</asp:ListItem>
                                    <asp:ListItem>スキル：吸収</asp:ListItem>
                                    <asp:ListItem>スキル：単体</asp:ListItem>
                                </asp:DropDownList>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-6 hidden-xs">
                            <div class="form-group">
                                <h4 style="font-weight: bold">検索値1</h4>
                                <asp:UpdatePanel ID ="UpdatePanel116" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="TextBox101" runat="server" Width="100px" OnTextChanged="TextBox101_TextChanged" AutoPostBack="true">0</asp:TextBox>
                                <asp:Label ID="serch1" runat="server" Text="％以上"></asp:Label>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-xs-12">
                            <h4 style="font-weight: bold">検索アビリティ2</h4>
                            <div class="form-group">
                                <asp:UpdatePanel ID ="UpdatePanel112" runat="server" >
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2Changed" AutoPostBack="true">
                                            <asp:ListItem>選択無し</asp:ListItem>
                                            <asp:ListItem>スキル発動率1.2倍上昇</asp:ListItem>
                                            <asp:ListItem>スキル発動率1.65倍上昇</asp:ListItem>
                                            <asp:ListItem>攻撃を受けた次ターンにスキル発動率上昇</asp:ListItem>
                                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                                            <asp:ListItem>スキル発動率上昇</asp:ListItem>
                                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                                            <asp:ListItem>クリティカル率上昇</asp:ListItem>
                                            <asp:ListItem>クリティカルダメージ増加</asp:ListItem>
                                            <asp:ListItem>クリティカル率上昇（PT全体対象）</asp:ListItem>
                                            <asp:ListItem>クリティカルダメージ増加（PT全体対象）</asp:ListItem>
                                            <asp:ListItem>攻撃力上昇</asp:ListItem>
                                            <asp:ListItem>ターン毎攻撃力上昇</asp:ListItem>
                                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                                            <asp:ListItem>ボスに対して攻撃力上昇</asp:ListItem>
                                            <asp:ListItem>ダメージ増加</asp:ListItem>
                                            <asp:ListItem>ターン毎ダメージ増加</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避付与</asp:ListItem>
                                            <asp:ListItem>迎撃</asp:ListItem>
                                            <asp:ListItem>反撃</asp:ListItem>
                                            <asp:ListItem>反撃（超反撃有）</asp:ListItem>
                                            <asp:ListItem>再行動</asp:ListItem>
                                            <asp:ListItem>再行動付与</asp:ListItem>
                                            <asp:ListItem>防御力・ダメージ軽減率上昇</asp:ListItem>
                                            <asp:ListItem>攻撃力低下</asp:ListItem>
                                            <asp:ListItem>防御力低下</asp:ListItem>
                                            <asp:ListItem>スキル発動率低下</asp:ListItem>
                                            <asp:ListItem>命中率低下</asp:ListItem>                                
                                            <asp:ListItem>行動回数減少</asp:ListItem>
                                            <asp:ListItem>追撃</asp:ListItem>
                                            <asp:ListItem>追撃付与</asp:ListItem>
                                            <asp:ListItem>ダメージ無効化</asp:ListItem>
                                            <asp:ListItem>属性付与</asp:ListItem>
                                            <asp:ListItem>弱点属性ダメ増加</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                                            <asp:ListItem>シャインクリスタルドロップ率上昇</asp:ListItem>
                                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                                            <asp:ListItem>移動力増加</asp:ListItem>
                                            <asp:ListItem>スキルにHP吸収付与</asp:ListItem>
                                            <asp:ListItem>イロモノ系</asp:ListItem>
                                            <asp:ListItem>イロモノサブアビ</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-6 hidden-xs">
                            <div class="form-group">
                                <h4 style="font-weight: bold">検索値2</h4>
                                <asp:UpdatePanel ID ="UpdatePanel117" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="TextBox102" runat="server" Width="100px"  AutoPostBack="true" OnTextChanged="TextBox102_TextChanged">0</asp:TextBox>
                                <asp:Label ID="serch2" runat="server" Text="％以上"></asp:Label>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-xs-12">
                            <h4 style="font-weight: bold">検索アビリティ3</h4>
                            <div class="form-group">
                                <asp:UpdatePanel ID ="UpdatePanel113" runat="server" >
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3Changed" AutoPostBack="true">
                                            <asp:ListItem>選択無し</asp:ListItem>
                                            <asp:ListItem>スキル発動率1.2倍上昇</asp:ListItem>
                                            <asp:ListItem>スキル発動率1.65倍上昇</asp:ListItem>
                                            <asp:ListItem>攻撃を受けた次ターンにスキル発動率上昇</asp:ListItem>
                                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                                            <asp:ListItem>スキル発動率上昇</asp:ListItem>
                                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                                            <asp:ListItem>クリティカル率上昇</asp:ListItem>
                                            <asp:ListItem>クリティカルダメージ増加</asp:ListItem>
                                            <asp:ListItem>クリティカル率上昇（PT全体対象）</asp:ListItem>
                                            <asp:ListItem>クリティカルダメージ増加（PT全体対象）</asp:ListItem>
                                            <asp:ListItem>攻撃力上昇</asp:ListItem>
                                            <asp:ListItem>ターン毎攻撃力上昇</asp:ListItem>
                                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                                            <asp:ListItem>ボスに対して攻撃力上昇</asp:ListItem>
                                            <asp:ListItem>ダメージ増加</asp:ListItem>
                                            <asp:ListItem>ターン毎ダメージ増加</asp:ListItem>
                                            <asp:ListItem>回避</asp:ListItem>
                                            <asp:ListItem>回避付与</asp:ListItem>
                                            <asp:ListItem>迎撃</asp:ListItem>
                                            <asp:ListItem>反撃</asp:ListItem>
                                            <asp:ListItem>反撃（超反撃有）</asp:ListItem>
                                            <asp:ListItem>再行動</asp:ListItem>
                                            <asp:ListItem>再行動付与</asp:ListItem>
                                            <asp:ListItem>防御力・ダメージ軽減率上昇</asp:ListItem>
                                            <asp:ListItem>攻撃力低下</asp:ListItem>
                                            <asp:ListItem>防御力低下</asp:ListItem>
                                            <asp:ListItem>スキル発動率低下</asp:ListItem>
                                            <asp:ListItem>命中率低下</asp:ListItem>                                
                                            <asp:ListItem>行動回数減少</asp:ListItem>
                                            <asp:ListItem>追撃</asp:ListItem>
                                            <asp:ListItem>追撃付与</asp:ListItem>
                                            <asp:ListItem>ダメージ無効化</asp:ListItem>
                                            <asp:ListItem>属性付与</asp:ListItem>
                                            <asp:ListItem>弱点属性ダメ増加</asp:ListItem>
                                            <asp:ListItem>HP回復</asp:ListItem>
                                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                                            <asp:ListItem>シャインクリスタルドロップ率上昇</asp:ListItem>
                                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                                            <asp:ListItem>移動力増加</asp:ListItem>
                                            <asp:ListItem>スキルにHP吸収付与</asp:ListItem>
                                            <asp:ListItem>イロモノ系</asp:ListItem>
                                            <asp:ListItem>イロモノサブアビ</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-6 hidden-xs">
                            <div class="form-group">
                                <h4 style="font-weight: bold">検索値3</h4>
                                <asp:UpdatePanel ID ="UpdatePanel118" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="TextBox103" runat="server" Width="100px"  AutoPostBack="true" OnTextChanged="TextBox103_TextChanged">0</asp:TextBox>
                                <asp:Label ID="serch3" runat="server" Text="％以上"></asp:Label>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="form-group">
                            <h4 style="font-weight: bold; font-style: italic;">☆エキスパート向け☆</h4>
                            <h4 style="font-weight: bold;" >除外するアビリティ</h4>
                            <asp:UpdatePanel ID ="UpdatePanel114" runat="server" >
                                <ContentTemplate>
                                    <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList1Changed"  AutoPostBack="true">
                                        <asp:ListItem>選択無し</asp:ListItem>
                                        <asp:ListItem>1ターン目系</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="form-group">
                            <h4 style="font-weight: bold;" >花騎士登録してる人限定</h4>
                            <asp:Label ID="registido" runat="server" Text=""></asp:Label>
                            <asp:UpdatePanel ID="UpdatePanel115" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="0">全キャラから</asp:ListItem>
                                        <asp:ListItem Value="1">所持キャラから</asp:ListItem>
                                        <asp:ListItem Value="2">未所持キャラから</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="advertencia" runat="server" Text=""></asp:Label>
                            <asp:Label ID="advertencia2" runat="server" Text=""></asp:Label>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2">
                        <div class="form-group">
                            <asp:UpdatePanel ID ="UpdatePanel101" runat="server">
                                <ContentTemplate>
                                    <asp:Button id="Button20" runat="server" Text="表示" OnClick="Button20_Click" CssClass="btn-danger btn-lg " Font-Bold="True" Font-Size="Large"  title="検索アビリティ1の値の順で出力されます" Visible="False"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <asp:UpdateProgress ID="UpdateProgress101" runat="server" DisplayAfter="50">
                            <ProgressTemplate>
                                通信中
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div class="col-xs-8">
                        <div class="form-group">
                            <asp:UpdatePanel ID ="UpdatePanel102" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="CounterNum" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button20" EventName ="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-12 hidden-xs hidden-sm hidden-md">
                        <div class="form-group">
                            <asp:UpdatePanel ID ="UpdatePanel103" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" ClientIDMode="AutoID">
                                <AlternatingItemTemplate><!--アイテム項目 alternate-->
                                    <tr style="background-color: #FFFFFF;color: #284775;">
                                    
                                        <td>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <div  class="text-center">
                                                <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="AttLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability1" runat="server" Text='<%# Eval("Abi1") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability2" runat="server" Text='<%# Eval("Abi2") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability3" runat="server" Text='<%# Eval("Abi3") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability4" runat="server" Text='<%# Eval("Abi4") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="SkillLabel" runat="server" Text='<%# Eval("SType") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="SRatioLabel" runat="server" Text='<%# Eval("SRatioRev") %>' />
                                        </td>

                                    </tr>
                                </AlternatingItemTemplate>
                                <EditItemTemplate>
                                    <tr style="background-color: #999999;">
                                        <td>
                                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="キャンセル" />
                                        </td>
                                        <td>
                                            <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="RarityTextBox" runat="server" Text='<%# Bind("Rarity") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="HPTextBox" runat="server" Text='<%# Bind("HP") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ATKTextBox" runat="server" Text='<%# Bind("ATK") %>' />
                                        </td>
                                    </tr>
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                        <tr>
                                            <td>何も表示出来ないよ～ん</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <InsertItemTemplate>
                                    <tr style="">
                                        <td>
                                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="挿入" />
                                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="クリア" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="RarityTextBox" runat="server" Text='<%# Bind("Rarity") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="HPTextBox" runat="server" Text='<%# Bind("HP") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ATKTextBox" runat="server" Text='<%# Bind("ATK") %>' />
                                        </td>
                                    </tr>
                                </InsertItemTemplate>
                                <ItemTemplate><!--アイテム項目-->
                                    <tr style="background-color: #E0FFFF;color: #333333;">
                                    
                                        <td>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <div  class="text-center">
                                                <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="AttLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability1" runat="server" Text='<%# Eval("Abi1") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability2" runat="server" Text='<%# Eval("Abi2") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability3" runat="server" Text='<%# Eval("Abi3") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability4" runat="server" Text='<%# Eval("Abi4") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="SkillLabel" runat="server" Text='<%# Eval("SType") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="SRatioLabel" runat="server" Text='<%# Eval("SRatioRev") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate><!-- ヘッダー部-->
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                    <tr runat="server" style="background-color: #777776;color: #ffffff;">
                                                        <th runat="server"><div  class="text-center">花騎士名</div></th>
                                                        <th runat="server"><div  class="text-center">レア</div></th>
                                                        <th runat="server"><div  class="text-center">属性</div></th>
                                                        <th runat="server"><div  class="text-center">アビリティ1</div></th>
                                                        <th runat="server"><div  class="text-center">アビリティ2</div></th>
                                                        <th runat="server"><div  class="text-center">アビリティ3</div></th>
                                                        <th runat="server"><div  class="text-center">アビリティ4</div></th>
                                                        <th runat="server"><div  class="text-center">スキル</div></th>
                                                        <th runat="server"><div  class="text-center">ダメ倍率</div></th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF"></td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <SelectedItemTemplate>
                                    <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">
                                        <td>
                                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability1" runat="server" Text='<%# Eval("Abi1") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability2" runat="server" Text='<%# Eval("Abi2") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability3" runat="server" Text='<%# Eval("Abi3") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability4" runat="server" Text='<%# Eval("Abi4") %>' />
                                        </td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button20" EventName ="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList4" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList3" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList4" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="RadioButtonList2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextBox101" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextBox102" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextBox103" EventName="TextChanged" />
                                </Triggers>
                                </asp:UpdatePanel>
                        </div>
                    </div>


                    <!--  トライ用   -->

                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <div class="form-group">
                            <asp:UpdatePanel ID ="UpdatePanel104" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:ListView ID="ListView2" runat="server" DataKeyNames="Id">
                                <AlternatingItemTemplate><!--モバイル用 アイテム項目 alternate-->
                                    <tr style="background-color: #FFFFFF;color: #284775;">
                                    
                                        <td rowspan="4">
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                            </div>
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="AttLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                            </div>
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="SkillLabel" runat="server" Text='<%# Eval("SType") %>' />
                                            </div>
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="SRatioLabel" runat="server" Text='<%# Eval("SRatioRevMovil") %>' />
                                            </div>
                                        </td>

                                        <td><div  class="text-center">1</div></td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Abi1") %>' />
                                        </td>
                                        
                                        <tr style="background-color: #FFFFFF;color: #284775;">
                                            <td><div  class="text-center">2</div></td>
                                            <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Abi2") %>' />
                                            </td>
                                        </tr>
                                         <tr style="background-color: #FFFFFF;color: #284775;">
                                             <td><div  class="text-center">3</div></td>
                                             <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Abi3") %>' />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #FFFFFF;color: #284775;">
                                            <td><div  class="text-center">4</div></td>
                                            <td>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Abi4") %>' />
                                            </td>
                                        </tr>


                                    </tr>
                                </AlternatingItemTemplate>
                                <EditItemTemplate>
                                    <tr style="background-color: #999999;">
                                        <td>
                                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="キャンセル" />
                                        </td>
                                        <td>
                                            <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="RarityTextBox" runat="server" Text='<%# Bind("Rarity") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="HPTextBox" runat="server" Text='<%# Bind("HP") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ATKTextBox" runat="server" Text='<%# Bind("ATK") %>' />
                                        </td>
                                    </tr>
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                        <tr>
                                            <td>何も表示出来ないよ～ん</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <InsertItemTemplate>
                                    <tr style="">
                                        <td>
                                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="挿入" />
                                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="クリア" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="RarityTextBox" runat="server" Text='<%# Bind("Rarity") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="HPTextBox" runat="server" Text='<%# Bind("HP") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ATKTextBox" runat="server" Text='<%# Bind("ATK") %>' />
                                        </td>
                                    </tr>
                                </InsertItemTemplate>
                                <ItemTemplate><!--モバイル用 アイテム項目-->
                                    <tr style="background-color: #E0FFFF;color: #333333;">
                                    
                                        <td rowspan="4">
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                            </div>
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="AttLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                            </div>
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="SkillLabel" runat="server" Text='<%# Eval("SType") %>' />
                                            </div>
                                        </td>
                                        <td rowspan="4">
                                            <div  class="text-center">
                                                <asp:Label ID="SRatioLabel" runat="server" Text='<%# Eval("SRatioRevMovil") %>' />
                                            </div>
                                        </td>
                                        <td><div  class="text-center">1</div></td>
                                        <td>
                                            <asp:Label ID="Ability1" runat="server" Text='<%# Eval("Abi1") %>' />
                                        </td>
                                        
                                        <tr style="background-color: #E0FFFF;color: #333333;">
                                            <td><div  class="text-center">2</div></td>
                                            <td>
                                            <asp:Label ID="Ability2" runat="server" Text='<%# Eval("Abi2") %>' />
                                            </td>
                                        </tr>
                                         <tr style="background-color: #E0FFFF;color: #333333;">
                                             <td><div  class="text-center">3</div></td>
                                             <td>
                                            <asp:Label ID="Ability3" runat="server" Text='<%# Eval("Abi3") %>' />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E0FFFF;color: #333333;">
                                            <td><div  class="text-center">4</div></td>
                                            <td>
                                            <asp:Label ID="Ability4" runat="server" Text='<%# Eval("Abi4") %>' />
                                            </td>
                                        </tr>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate><!--モバイル用  ヘッダー部-->
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                    <tr runat="server" style="background-color: #777776;color: #ffffff;">
                                                        <th runat="server"><div  class="text-center">花騎士名</div></th>
                                                        <th runat="server"><div  class="text-center">レア</div></th>
                                                        <th runat="server"><div  class="text-center">属性</div></th>
                                                        <th runat="server"><div  class="text-center">スキル</div></th>
                                                        <th runat="server"><div  class="text-center">倍率</div></th>
                                                        <th runat="server"><div  class="text-center">アビ順</div></th>
                                                        <th runat="server"><div  class="text-center">アビリティ</div></th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF"></td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <SelectedItemTemplate>
                                    <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">
                                        <td>
                                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability1" runat="server" Text='<%# Eval("Abi1") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability2" runat="server" Text='<%# Eval("Abi2") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability3" runat="server" Text='<%# Eval("Abi3") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="Ability4" runat="server" Text='<%# Eval("Abi4") %>' />
                                        </td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button20" EventName ="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownList4" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList3" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList4" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="RadioButtonList2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextBox101" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextBox102" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextBox103" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        
    </asp:Content>