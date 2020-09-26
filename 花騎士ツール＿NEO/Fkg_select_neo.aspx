<%@ Page Title="編成シミュ" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Fkg_select_neo.aspx.cs" Inherits="花騎士ツール＿NEO.Fkg_select_neo" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <section class="container hidden">
            <div class="row">
                <div class="col-xs-12">
                    <h2>編成シミュ</h2>
                    <p>花騎士毎に下記の各フィルタを設定し読込ボタンを押すと、絞り込んだ花騎士名がドロップダウンリストに表示されます。</p>
                    <p>各リスト上で花騎士を選択したのち、計算開始ボタンを押すと、PT編成時の各種アビ効果を下に表示します。</p>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    <button class="btn btn-default btn-sm" id="openall" >全てひらく</button>
                </div>
                <div class="col-xs-4 visible-xs visible-sm visible-md">
                    <button class="btn btn-default btn-sm" id="closeall">全てとじる</button>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2 hidden-xs hidden-sm hidden-md">
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="レアリティ"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="属性"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="スキル種"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="アビ選択1のみアビの値でソート"></asp:Label>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="アビ選択2"></asp:Label>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="アビ選択3"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="クイック"></asp:Label>
                    <asp:Label ID="Label19" runat="server" Text="フィルタ"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="属性付与"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="デバフ"></asp:Label>
                </div>

                <!-- 1つ目 -->
                <div class="col-lg-2 col-sm-4 col-xs-6">
                    <div class="col-lg-12 visible-lg"> 
                        <h2>花騎士1</h2>
                    </div>
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <asp:Label data-toggle="collapse" data-target="#fkgdata1" href="" runat="server" ID="fkgdata1_expander"  aria-controls="fkgdata1" ToolTip="開閉"><h3>花騎士1</h3></asp:Label>
                    </div>
                    <div id="fkgdata1" class="collapse multi-collapse in">
                        <div class="col-xs-12 visible-xs visible-sm visible-md">レアリティ</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton201" runat="server" Checked="True" GroupName="11" Text="全" />
                            <asp:RadioButton ID="RadioButton202" runat="server" Text="6" GroupName="11" />
                            <asp:RadioButton ID="RadioButton203" runat="server" Enabled="False" Text="5以下" GroupName="11" />
                        </div>
                        
                        <div class="col-xs-12 visible-xs visible-sm visible-md">属性</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton1001" runat="server" Checked="True" GroupName="12" Text="全属性" />
                            <br />
                            <asp:RadioButton ID="RadioButton1002" runat="server" Text="斬" GroupName="12" />
                            <asp:RadioButton ID="RadioButton1003" runat="server" Text="打" GroupName="12" />
                            <asp:RadioButton ID="RadioButton1004" runat="server" Text="突" GroupName="12" />
                            <asp:RadioButton ID="RadioButton1005" runat="server" Text="魔" GroupName="12" />
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">スキルタイプ</div>
                        <div class="form-group">
                            
                            <asp:CheckBoxList ID="CheckBoxList11" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="全体">全</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2体">2</asp:ListItem>
                                <asp:ListItem Selected="True" Value="変則">変</asp:ListItem>
                                <asp:ListItem Selected="True" Value="吸収">吸</asp:ListItem>
                                <asp:ListItem Selected="True" Value="複数回">複</asp:ListItem>
                                <asp:ListItem Selected="True" Value="単体">単</asp:ListItem>
                            </asp:CheckBoxList>
                            
                        </div>

                        

                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ1(アビ値でソート)</div>
                            <asp:DropDownList ID="DropDownList21" runat="server"  AppendDataBoundItems="True"   ClientIDMode="Static" Width="160px">
                                <asp:ListItem>未選択</asp:ListItem>
                                <asp:ListItem>1ターン目系</asp:ListItem>
                                <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                                <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                                <asp:ListItem>スキル発動率上昇</asp:ListItem>
                                <asp:ListItem>クリ率上昇</asp:ListItem>
                                <asp:ListItem>クリダメ上昇</asp:ListItem>
                                <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                                <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                                <asp:ListItem>スキルダメージ増加</asp:ListItem>
                                <asp:ListItem>ダメージ増加</asp:ListItem>
                                <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                                <asp:ListItem>回避</asp:ListItem>
                                <asp:ListItem>反撃</asp:ListItem>
                                <asp:ListItem>防御</asp:ListItem>
                                <asp:ListItem>追撃</asp:ListItem>
                                <asp:ListItem>再行動</asp:ListItem>
                                <asp:ListItem>バリア</asp:ListItem>
                                <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                                <asp:ListItem>光ゲージ充填</asp:ListItem>
                                <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                            </asp:DropDownList>


                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ2</div>
                        <asp:DropDownList ID="DropDownList26" runat="server" AppendDataBoundItems="True" ClientIDMode="Static" Width="160px"  >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ3</div>
                        <asp:DropDownList ID="DropDownList31" runat="server" AppendDataBoundItems="True" ClientIDMode="Static" Width="160px"  >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">クイックフィルタ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox111" runat="server" value="" />1.65倍
                                </label>
                            </div>
                    
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox112" runat="server" value="" />昇華
                                </label>
                            </div>
                        </div>

                        <div class="col-xs-12 visible-xs visible-sm visible-md">付与属性</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList12" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>斬</asp:ListItem>
                                <asp:ListItem>打</asp:ListItem>
                                <asp:ListItem>突</asp:ListItem>
                                <asp:ListItem>魔</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>

                        <div class="col-xs-12 visible-xs visible-sm visible-md">デバフ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox113" runat="server" value="" />攻撃力低下
                                </label>
                            </div>
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox114" runat="server" value="" />3人以上
                                </label>
                            </div>
                            <br />
                        </div>
                    </div>
                    <%-- 読込 --%>
                    <div class="form-group">
                        <asp:UpdatePanel ID ="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button1" runat="server" Text="花騎士1読込" OnClick="Button1_Click" CssClass="btn-primary"/>
                                <br />    
                                <br />
                        
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="160px">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName ="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Button9" EventName ="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <asp:DropDownList ID="DropDownList16" runat="server">
                            <asp:ListItem Value="1">SLv1</asp:ListItem>
                            <asp:ListItem Value="2">SLv2</asp:ListItem>
                            <asp:ListItem Value="3">SLv3</asp:ListItem>
                            <asp:ListItem Value="4">SLv4</asp:ListItem>
                            <asp:ListItem Value="5">SLv5</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </div>
                </div>

                
                <!-- 2人目 -->
                 
                <div class="col-lg-2 col-sm-4 col-xs-6">
                    <div class="col-lg-12 visible-lg"> 
                        <h2>花騎士2</h2>
                    </div>
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <asp:Label data-toggle="collapse" data-target="#fkgdata2" href="" runat="server" ID="fkgdata2_expander"  aria-controls="fkgdata2" ToolTip="開閉"><h3>花騎士2</h3></asp:Label>
                    </div>
                    <div id="fkgdata2" class="collapse multi-collapse in">
                        <div class="col-xs-12 visible-xs visible-sm visible-md">レアリティ</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton205" runat="server" Checked="True" GroupName="21" Text="全" />
                            <asp:RadioButton ID="RadioButton206" runat="server" Text="6" GroupName="21" />
                            <asp:RadioButton ID="RadioButton207" runat="server" Enabled="False" Text="5以下" GroupName="21" />
                        </div>
                       
                        <div class="col-xs-12 visible-xs visible-sm visible-md">属性</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton1006" runat="server" Checked="True" GroupName="22" Text="全属性" />
                            <br />
                        
                            <asp:RadioButton ID="RadioButton1007" runat="server" Text="斬" GroupName="22" />
                            <asp:RadioButton ID="RadioButton1008" runat="server" Text="打" GroupName="22" />
                            <asp:RadioButton ID="RadioButton1009" runat="server" Text="突" GroupName="22" />
                            <asp:RadioButton ID="RadioButton1010" runat="server" Text="魔" GroupName="22" />
                        </div>
                        
                        <div class="col-xs-12 visible-xs visible-sm visible-md">スキルタイプ</div>
                        <div class="form-group">

                            <asp:CheckBoxList ID="CheckBoxList21" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="全体">全</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2体">2</asp:ListItem>
                                <asp:ListItem Selected="True" Value="変則">変</asp:ListItem>
                                <asp:ListItem Selected="True" Value="吸収">吸</asp:ListItem>
                                <asp:ListItem Selected="True" Value="複数回">複</asp:ListItem>
                                <asp:ListItem Selected="True" Value="単体">単</asp:ListItem>
                            </asp:CheckBoxList>

                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ1(アビ値でソート)</div>
                        <asp:DropDownList ID="DropDownList22" runat="server"  AppendDataBoundItems="True"   ClientIDMode="Static" Width="160px">
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>スキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>

                        

                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ2</div>
                        <asp:DropDownList ID="DropDownList27" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ3</div>
                        <asp:DropDownList ID="DropDownList32" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">クイックフィルタ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox121" runat="server" value="" />1.65倍
                                </label>
                            </div>
                    
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox122" runat="server" value="" />昇華
                                </label>
                            </div>
                        </div>
                        
                        <div class="col-xs-12 visible-xs visible-sm visible-md">付与属性</div>
                        <div class="form-group">

                            <asp:CheckBoxList ID="CheckBoxList22" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>斬</asp:ListItem>
                                <asp:ListItem>打</asp:ListItem>
                                <asp:ListItem>突</asp:ListItem>
                                <asp:ListItem>魔</asp:ListItem>
                            </asp:CheckBoxList>

                        </div>
                      
                        <div class="col-xs-12 visible-xs visible-sm visible-md">デバフ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox123" runat="server" value="" />攻撃力低下
                                </label>
                            </div>
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox124" runat="server" value="" />3人以上
                                </label>
                            </div>
                            <br />
                        </div>
                    </div>
                    <%--  読込--%>
                    <div class="form-group">
                         <asp:UpdatePanel ID ="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button2" runat="server" Text="花騎士2読込" OnClick="Button2_Click" CssClass="btn-primary"/>
                                <br />    
                                <br />
                                <asp:DropDownList ID="DropDownList6" runat="server" Width="160px">
                                </asp:DropDownList>
                            </ContentTemplate>
                             <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName ="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Button9" EventName ="Click" />
                            </Triggers>
                             </asp:UpdatePanel>
                        <br />
                        <asp:DropDownList ID="DropDownList17" runat="server">
                            <asp:ListItem Value="1">SLv1</asp:ListItem>
                            <asp:ListItem Value="2">SLv2</asp:ListItem>
                            <asp:ListItem Value="3">SLv3</asp:ListItem>
                            <asp:ListItem Value="4">SLv4</asp:ListItem>
                            <asp:ListItem Value="5">SLv5</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- 3つ目 -->
                <div class="col-lg-2 col-sm-4 col-xs-6">
                    <div class="col-lg-12 visible-lg"> 
                        <h2>花騎士3</h2>
                    </div>
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <asp:Label data-toggle="collapse" data-target="#fkgdata3" href="" runat="server" ID="fkgdata3_expander"  aria-controls="fkgdata3" ToolTip="開閉"><h3>花騎士3</h3></asp:Label>
                    </div>
                    <div id="fkgdata3" class="collapse multi-collapse in">
                        <div class="col-xs-12 visible-xs visible-sm visible-md">レアリティ</div>
                        <div class="form-group">
                            <p>
                                <asp:RadioButton ID="RadioButton209" runat="server" Text="全" GroupName="31" Checked="True" />
                                <asp:RadioButton ID="RadioButton210" runat="server" Text="6" GroupName="31" />
                                <asp:RadioButton ID="RadioButton211" runat="server" Text="5以下" GroupName="31" Enabled="False" />
                            </p>
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">属性</div>
                        <div class="form-group">
                             
                            <asp:RadioButton ID="RadioButton1011" runat="server" Checked="True" GroupName="32" Text="全属性" />
                            <br />
                            <asp:RadioButton ID="RadioButton1012" runat="server" Text="斬" GroupName="32" />
                            <asp:RadioButton ID="RadioButton1013" runat="server" Text="打" GroupName="32" />
                            <asp:RadioButton ID="RadioButton1014" runat="server" Text="突" GroupName="32" />
                            <asp:RadioButton ID="RadioButton1015" runat="server" Text="魔" GroupName="32" />
                            
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">スキルタイプ</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList31" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="全体">全</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2体">2</asp:ListItem>
                                <asp:ListItem Selected="True" Value="変則">変</asp:ListItem>
                                <asp:ListItem Selected="True" Value="吸収">吸</asp:ListItem>
                                <asp:ListItem Selected="True" Value="複数回">複</asp:ListItem>
                                <asp:ListItem Selected="True" Value="単体">単</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                      


                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ1(アビ値でソート)</div>
                        <asp:DropDownList ID="DropDownList23" runat="server"  AppendDataBoundItems="True"   ClientIDMode="Static" Width="160px">
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>スキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>

                        

                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ2</div>
                        <asp:DropDownList ID="DropDownList28" runat="server" AppendDataBoundItems="True" ClientIDMode="Static" Width="160px"  >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ3</div>
                        <asp:DropDownList ID="DropDownList33" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">クイックフィルタ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox131" runat="server" value="" />1.65倍
                                </label>
                            </div>
                    
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox132" runat="server" value="" />昇華
                                </label>
                            </div>
                        </div>
                        
                        <div class="col-xs-12 visible-xs visible-sm visible-md">付与属性</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList32" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>斬</asp:ListItem>
                                <asp:ListItem>打</asp:ListItem>
                                <asp:ListItem>突</asp:ListItem>
                                <asp:ListItem>魔</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                       
                        <div class="col-xs-12 visible-xs visible-sm visible-md">デバフ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox133" runat="server" value="" />攻撃力低下
                                </label>
                            </div>
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox134" runat="server" value="" />3人以上
                                </label>
                            </div>
                            <br />
                        </div>
                    </div>
                    <%-- 読込 --%>
                    <div class="form-group">
                        <asp:UpdatePanel ID ="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button3" runat="server" Text="花騎士3読込" OnClick="Button3_Click" CssClass="btn-primary"/>
                                <br />    
                                <br />
                                <asp:DropDownList ID="DropDownList9" runat="server" Width="160px">
                                </asp:DropDownList>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button3" EventName ="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="Button9" EventName ="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        <br />
                        <asp:DropDownList ID="DropDownList18" runat="server">
                            <asp:ListItem Value="1">SLv1</asp:ListItem>
                            <asp:ListItem Value="2">SLv2</asp:ListItem>
                            <asp:ListItem Value="3">SLv3</asp:ListItem>
                            <asp:ListItem Value="4">SLv4</asp:ListItem>
                            <asp:ListItem Value="5">SLv5</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- 4つ目 -->
                <div class="col-lg-2 col-sm-4 col-xs-6">
                    <div class="col-lg-12 visible-lg"> 
                        <h2>花騎士4</h2>
                    </div>
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <asp:Label data-toggle="collapse" data-target="#fkgdata4" href="" runat="server" ID="fkgdata4_expander"  aria-controls="fkgdata4" ToolTip="開閉"><h3>花騎士4</h3></asp:Label>
                    </div>
                    <div id="fkgdata4" class="collapse multi-collapse in">
                        <div class="col-xs-12 visible-xs visible-sm visible-md">レアリティ</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton213" runat="server" Checked="True" GroupName="41" Text="全" />
                            <asp:RadioButton ID="RadioButton214" runat="server" GroupName="41" Text="6" />
                            <asp:RadioButton ID="RadioButton215" runat="server" Enabled="False" GroupName="41" Text="5以下" />
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">属性</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton1016" runat="server" Checked="True" GroupName="42" Text="全属性" />
                            <br />
                            <asp:RadioButton ID="RadioButton1017" runat="server" GroupName="42" Text="斬" />
                            <asp:RadioButton ID="RadioButton1018" runat="server" GroupName="42" Text="打" />
                            <asp:RadioButton ID="RadioButton1019" runat="server" GroupName="42" Text="突" />
                            <asp:RadioButton ID="RadioButton1020" runat="server" GroupName="42" Text="魔" />
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">スキルタイプ</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList41" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="全体">全</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2体">2</asp:ListItem>
                                <asp:ListItem Selected="True" Value="変則">変</asp:ListItem>
                                <asp:ListItem Selected="True" Value="吸収">吸</asp:ListItem>
                                <asp:ListItem Selected="True" Value="複数回">複</asp:ListItem>
                                <asp:ListItem Selected="True" Value="単体">単</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ1(アビ値でソート)</div>
                        <asp:DropDownList ID="DropDownList24" runat="server"  AppendDataBoundItems="True"   ClientIDMode="Static" Width="160px">
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>スキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>

                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ2</div>
                        <asp:DropDownList ID="DropDownList29" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ3</div>
                        <asp:DropDownList ID="DropDownList34" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">クイックフィルタ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox141" runat="server" value="" />1.65倍
                                </label>
                            </div>
                    
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox142" runat="server" value="" />昇華
                                </label>
                            </div>
                        </div>

                        <div class="col-xs-12 visible-xs visible-sm visible-md">付与属性</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList42" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>斬</asp:ListItem>
                                <asp:ListItem>打</asp:ListItem>
                                <asp:ListItem>突</asp:ListItem>
                                <asp:ListItem>魔</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>

                        <div class="col-xs-12 visible-xs visible-sm visible-md">デバフ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox143" runat="server" value="" />攻撃力低下
                                </label>
                            </div>
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox144" runat="server" value="" />3人以上
                                </label>
                            </div>
                            <br />
                        </div>
                    </div>
                    <%-- 読込 --%>
                    <div class="form-group">
                        <asp:UpdatePanel ID ="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button4" runat="server" Text="花騎士4読込" OnClick="Button4_Click" CssClass="btn-primary"/>
                                <br />    
                                <br />
                                <asp:DropDownList ID="DropDownList12" runat="server" Width="160px">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button4" EventName ="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Button9" EventName ="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <asp:DropDownList ID="DropDownList19" runat="server">
                            <asp:ListItem Value="1">SLv1</asp:ListItem>
                            <asp:ListItem Value="2">SLv2</asp:ListItem>
                            <asp:ListItem Value="3">SLv3</asp:ListItem>
                            <asp:ListItem Value="4">SLv4</asp:ListItem>
                            <asp:ListItem Value="5">SLv5</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- 5つ目 -->
                <div class="col-lg-2 col-sm-4 col-xs-6">
                    <div class="col-lg-12 visible-lg"> 
                        <h2>花騎士5</h2>
                    </div>
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <asp:Label data-toggle="collapse" data-target="#fkgdata5" href="" runat="server" ID="fkgdata5_expander"  aria-controls="fkgdata5" ToolTip="開閉"><h3>花騎士5</h3></asp:Label>
                    </div>
                    <div id="fkgdata5" class="collapse multi-collapse in">
                        <div class="col-xs-12 visible-xs visible-sm visible-md">レアリティ</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton217" runat="server" Checked="True" GroupName="51" Text="全" />
                            <asp:RadioButton ID="RadioButton218" runat="server" GroupName="51" Text="6" />
                            <asp:RadioButton ID="RadioButton219" runat="server" Enabled="False" GroupName="51" Text="5以下" />
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">属性</div>
                        <div class="form-group">
                            <asp:RadioButton ID="RadioButton1021" runat="server" GroupName="52" Text="全属性" Checked="True" />
                            <br />
                            <asp:RadioButton ID="RadioButton1022" runat="server" GroupName="52" Text="斬" />
                            <asp:RadioButton ID="RadioButton1023" runat="server" GroupName="52" Text="打" />
                            <asp:RadioButton ID="RadioButton1024" runat="server" GroupName="52" Text="突" />
                            <asp:RadioButton ID="RadioButton1025" runat="server" GroupName="52" Text="魔" />
                        </div>
                        <div class="col-xs-12 visible-xs visible-sm visible-md">スキルタイプ</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList51" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="全体">全</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2体">2</asp:ListItem>
                                <asp:ListItem Selected="True" Value="変則">変</asp:ListItem>
                                <asp:ListItem Selected="True" Value="吸収">吸</asp:ListItem>
                                <asp:ListItem Selected="True" Value="複数回">複</asp:ListItem>
                                <asp:ListItem Selected="True" Value="単体">単</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
 

                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ1(アビ値でソート)</div>
                        <asp:DropDownList ID="DropDownList25" runat="server"  AppendDataBoundItems="True"   ClientIDMode="Static" Width="160px">
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>スキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>

                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ2</div>
                        <asp:DropDownList ID="DropDownList30" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">アビリティ3</div>
                        <asp:DropDownList ID="DropDownList35" runat="server" AppendDataBoundItems="True" ClientIDMode="Static"  Width="160px" >
                            <asp:ListItem>未選択</asp:ListItem>
                            <asp:ListItem>1ターン目系</asp:ListItem>
                            <asp:ListItem>スキル発動率1.2倍</asp:ListItem>
                            <asp:ListItem>スキルLVによりスキル発動率上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇</asp:ListItem>
                            <asp:ListItem>クリダメ上昇</asp:ListItem>
                            <asp:ListItem>クリ率上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>クリダメ上昇（PT全体対象）</asp:ListItem>
                            <asp:ListItem>スキルダメージ増加</asp:ListItem>
                            <asp:ListItem>ダメージ増加</asp:ListItem>
                            <asp:ListItem>ボスに与えるダメージ増加</asp:ListItem>
                            <asp:ListItem>回避</asp:ListItem>
                            <asp:ListItem>反撃</asp:ListItem>
                            <asp:ListItem>防御</asp:ListItem>
                            <asp:ListItem>追撃</asp:ListItem>
                            <asp:ListItem>再行動</asp:ListItem>
                            <asp:ListItem>バリア</asp:ListItem>
                            <asp:ListItem>ソーラードライブ効果上昇</asp:ListItem>
                            <asp:ListItem>光ゲージ充填</asp:ListItem>
                            <asp:ListItem>自身が攻撃を受けた次ターン系</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <div class="col-xs-12 visible-xs visible-sm visible-md">クイックフィルタ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox151" runat="server" value="" />1.65倍
                                </label>
                            </div>
                    
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox152" runat="server" value="" />昇華
                                </label>
                            </div>
                        </div>

                        <div class="col-xs-12 visible-xs visible-sm visible-md">付与属性</div>
                        <div class="form-group">
                            <asp:CheckBoxList ID="CheckBoxList52" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>斬</asp:ListItem>
                                <asp:ListItem>打</asp:ListItem>
                                <asp:ListItem>突</asp:ListItem>
                                <asp:ListItem>魔</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>

                        <div class="col-xs-12 visible-xs visible-sm visible-md">デバフ</div>
                        <div class="form-group">
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox153" runat="server" value="" />攻撃力低下
                                </label>
                            </div>
                            <div class="checkbox-inline">
                                <label>
                                    <input type="checkbox" id="CheckBox154" runat="server" value="" />3人以上
                                </label>
                            </div>
                            <br />
                        </div>
                    </div>
                    <%--  読込--%>
                    <div class="form-group">
                        <asp:UpdatePanel ID ="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button5" runat="server" Text="花騎士5読込" OnClick="Button5_Click" CssClass="btn-primary"/>
                                <br />    
                                <br />    
                                <asp:DropDownList ID="DropDownList15" runat="server" Width="160px">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button5" EventName ="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Button9" EventName ="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />    
                        <asp:DropDownList ID="DropDownList20" runat="server">
                            <asp:ListItem Value="1">SLv1</asp:ListItem>
                            <asp:ListItem Value="2">SLv2</asp:ListItem>
                            <asp:ListItem Value="3">SLv3</asp:ListItem>
                            <asp:ListItem Value="4">SLv4</asp:ListItem>
                            <asp:ListItem Value="5">SLv5</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                </div>
            </div>
        <br />
            <%-- 計算 --%>

            <div class="row hidden">
                <div class="col-xs-6">
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        <div>
                            <button class="btn btn-default btn-sm"   id="openall2">全てひらく</button>
                        </div>
                        
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="col-xs-12 visible-xs visible-sm visible-md">
                        
                        <div >
                            <button class="btn btn-default btn-sm" id="closeall2">全てとじる</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-xs-4">
                    <!--ダミー--> 
                </div>
                <div class="col-lg-2 col-xs-8">
                    <div class="form-group">
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="短縮名表示" />
                    </div>
                    <div class="form-group">
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Slv自動変更しない" />
                    </div>
                </div>
                <div class="col-lg-2 col-xs-6">
                    <div class="form-group">
                        <h5 style="font-weight: bold">敵の数</h5>
                        <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">1体</asp:ListItem>
                            <asp:ListItem Value="2">2体</asp:ListItem>
                            <asp:ListItem Selected="True" Value="3">3体</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="col-lg-2 col-xs-6">
                    <div class="form-group">
                        <h5 style="font-weight: bold">ランタナアビ(仮称)</h5>
                        <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">無</asp:ListItem>
                            <asp:ListItem Value="10">10％</asp:ListItem>
                            <asp:ListItem Value="20">20％</asp:ListItem>
                            <asp:ListItem Value="30">30％</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                
            </div>
            <br />
            <div class="row">
                <div class="col-lg-5 col-xs-4">
                    <div>
                        <asp:UpdatePanel ID ="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button9" runat="server" Text="全読込" OnClick="Button9_Click" CssClass="btn-primary btn-lg " Font-Bold="True" Font-Size="Large" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-lg-2 col-xs-4">
                    <div id="Button10">
                        <asp:Button  runat="server" Text="計算開始" OnClick="Button10_Click" CssClass="btn-danger btn-lg " Font-Bold="True" Font-Size="Large" />
                    </div>
                </div>

            </div>
            <br />
            <%--結果表示--%>            
            <div class="row">
                <div class="col-md-1 visible-lg">
                    <!--ダミー-->
                </div>

                <div class="col-md-8 col-xs-12">


                    <!--個人用表示項目-->
                    <div class="form-group">
                        <asp:ListView ID="ListView1" runat="server" DataKeyNames="id">
                            <AlternatingItemTemplate><!--アイテム項目 alternate-->
                                <tr style="background-color: #FFFFFF;color: #284775;">
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="TypeExpLabel1" runat="server" Text='<%# Eval("typeExp1") %>' />
                                        </div>
                                    </td>
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="TypeExpLabel2" runat="server" Text='<%# Eval("typeExp2") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg1Label" runat="server" Text='<%# Eval("fkg1") %>' />
                                        </div>
                                    </td>
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg2Label" runat="server" Text='<%# Eval("fkg2") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg3Label" runat="server" Text='<%# Eval("fkg3") %>' />
                                        </div>
                                    </td>
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg4Label" runat="server" Text='<%# Eval("fkg4") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg5Label" runat="server" Text='<%# Eval("fkg5") %>' />
                                        </div>
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
                                </tr>
                            </EditItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                    <tr>
                                        <td>データは返されませんでした。</td>
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
                                </tr>
                            </InsertItemTemplate>
                            <ItemTemplate><!--アイテム項目-->
                                <tr style="background-color: #FFFFFF;color: #284775;">
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="TypeExpLabel1" runat="server" Text='<%# Eval("typeExp1") %>' />
                                        </div>
                                    </td>
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="TypeExpLabel2" runat="server" Text='<%# Eval("typeExp2") %>' />
                                        </div>
                                   </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg1Label" runat="server" Text='<%# Eval("fkg1") %>' />
                                        </div>
                                    </td>
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg2Label" runat="server" Text='<%# Eval("fkg2") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg3Label" runat="server" Text='<%# Eval("fkg3") %>' />
                                        </div>
                                    </td>
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg4Label" runat="server" Text='<%# Eval("fkg4") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                             <asp:Label ID="Fkg5Label" runat="server" Text='<%# Eval("fkg5") %>' />
                                        </div>
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate><!-- ヘッダー部-->
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                <tr runat="server" style="background-color: #777776;color: #ffffff;">
                                                    <th runat="server"><div  class="text-center">項目</div></th>
                                                    <th runat="server"><div  class="text-center">詳細</div></th>
                                                    <th runat="server"><div  class="text-center" >花騎士1</div></th>
                                                    <th runat="server"><div  class="text-center">花騎士2</div></th>
                                                    <th runat="server"><div  class="text-center">花騎士3</div></th>
                                                    <th runat="server"><div  class="text-center">花騎士4</div></th>
                                                    <th runat="server"><div  class="text-center">花騎士5</div></th>
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
                                        <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="TypeExpLabel1" runat="server" Text='<%# Eval("typeExp1") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="TypeExpLabel2" runat="server" Text='<%# Eval("typeExp2") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Fkg1Label" runat="server" Text='<%# Eval("fkg1") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Fkg2Label" runat="server" Text='<%# Eval("fkg2") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Fkg3Label" runat="server" Text='<%# Eval("fkg3") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Fkg4Label" runat="server" Text='<%# Eval("fkg4") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Fkg5Label" runat="server" Text='<%# Eval("fkg5") %>' />
                                    </td>
                                </tr>
                            </SelectedItemTemplate>
                        </asp:ListView>
                    </div>
                </div>

                <div class="col-md-2 col-xs-12">

                    <!--PT表示項目-->
                    <div class="form-group">
                        <asp:ListView ID="ListView2" runat="server" DataKeyNames="id">
                            <AlternatingItemTemplate><!--アイテム項目 alternate-->
                                <tr style="background-color: #FFFFFF;color: #284775;">
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="TypeExpLabel1" runat="server" Text='<%# Eval("typeExp1") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="Fkg1Label" runat="server" Text='<%# Eval("fkgPt") %>' />
                                        </div>
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
                                </tr>
                            </EditItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                    <tr>
                                        <td>データは返されませんでした。</td>
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
                                </tr>
                            </InsertItemTemplate>
                            <ItemTemplate><!--アイテム項目-->
                                <tr style="background-color: #FFFFFF;color: #284775;">
                                    <td>
                                        <div  class="text-center">
                                            <asp:Label ID="TypeExpLabel1" runat="server" Text='<%# Eval("typeExp1") %>' />
                                        </div>
                                    </td>
                                    <td style="background-color: #E0FFFF;color: #333333;">
                                        <div  class="text-center">
                                            <asp:Label ID="FkgPtLabel" runat="server" Text='<%# Eval("fkgPt") %>' />
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate><!-- ヘッダー部-->
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                <tr runat="server" style="background-color: #777776;color: #ffffff;">
                                                    <th runat="server"><div  class="text-center">PT項目</div></th>
                                                    <th runat="server"><div  class="text-center">値</div></th>
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
                                        <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="TypeExpLabel1" runat="server" Text='<%# Eval("typeExp1") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="FkgPtLabel" runat="server" Text='<%# Eval("fkgPt") %>' />
                                    </td>
                                </tr>
                            </SelectedItemTemplate>
                        </asp:ListView>
                    </div>
                </div>

            </div>
        </section>

  <%--  </form>--%>
</asp:Content>
