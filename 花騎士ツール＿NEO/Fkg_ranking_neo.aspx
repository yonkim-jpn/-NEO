<%@ Page Title="ランキング" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Fkg_ranking_neo.aspx.cs" Inherits="花騎士ツール＿NEO.Fkg_ranking_neo" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <h3>ランキング</h3>
                    <h5>開花LV80、好感度咲100％での能力値です。</h5>
                    <h4>レアリティ</h4>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">全レア</asp:ListItem>
                        <asp:ListItem >6</asp:ListItem>
                        <asp:ListItem Enabled="False">5</asp:ListItem>
                        <asp:ListItem>昇華虹</asp:ListItem>
                    </asp:RadioButtonList>
                    <h4>属性</h4>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">全属性</asp:ListItem>
                        <asp:ListItem>斬</asp:ListItem>
                        <asp:ListItem>打</asp:ListItem>
                        <asp:ListItem>突</asp:ListItem>
                        <asp:ListItem>魔</asp:ListItem>
                    </asp:RadioButtonList>
                    <h4>所属国家</h4>
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">全国家</asp:ListItem>
                        <asp:ListItem>知徳花</asp:ListItem>
                        <asp:ListItem>深緑花</asp:ListItem>
                        <asp:ListItem>常夏花</asp:ListItem>
                        <asp:ListItem>風谷花</asp:ListItem>
                        <asp:ListItem>雪原花</asp:ListItem>
                        <asp:ListItem>湖畔花</asp:ListItem>
                    </asp:RadioButtonList>
                    <h4>ソート項目</h4>
                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">総合力</asp:ListItem>
                        <asp:ListItem >HP</asp:ListItem>
                        <asp:ListItem >攻撃力</asp:ListItem>
                        <asp:ListItem >防御力</asp:ListItem>
                        <asp:ListItem >移動力</asp:ListItem>
                        <asp:ListItem>ID</asp:ListItem>
                        <asp:ListItem>登録日</asp:ListItem>
                    </asp:RadioButtonList>
                    
                    <h4>ソート方法</h4>
                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">降順</asp:ListItem>
                        <asp:ListItem>昇順</asp:ListItem>
                    </asp:RadioButtonList>
                    
                    <h4>表示数</h4>
                    <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">30</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>全</asp:ListItem>
                    </asp:RadioButtonList>

                     <div class="col-xs-12 visible-xs">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="短縮名表示" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-3 col-md-2">

            </div>

            <div class="col-xs-2">
                <div class="form-group" >
                    <asp:Button ID="Button1" runat="server" Text="表示" CssClass="btn-info btn-lg " OnClick="Button1_Click" />
                </div>
            </div>
        </div>
    
        <div class="row">
            <div class="col-xs-12">
                <!-- Listview記述 -->    
                <div class="form-group">
                    
                    <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" DataMember="DefaultView" EnablePersistedSelection="True" DataKeyNames="Id" ClientIDMode="AutoID">
                        <EmptyDataTemplate>
                            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                <tr>
                                    <td>データは返されませんでした。</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <AlternatingItemTemplate><!-- 各アイテム Alter -->
                            <tr style="background-color: #FFFFFF;color: #284775;">
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Ranking") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="ATTLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                    </div>
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="TotalLabel1" runat="server" Text='<%# Eval("Total") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="HPLabel" runat="server" Text='<%# Eval("HP") %>' />
                                    </div>
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="ATKLabel" runat="server" Text='<%# Eval("ATK") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="DEFLabel" runat="server" Text='<%# Eval("DEF") %>' />
                                    </div>
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="MOVLabel" runat="server" Text='<%# Eval("MOV") %>' />
                                    </div>
                                </td>
                                
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="UnitLabel" runat="server" Text='<%# Eval("Unit") %>' />
                                    </div>
                                </td>
                        
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="background-color: #FFFFFF;color: #284775;">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="キャンセル" />
                                </td>
                                <td>
                                    <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Ranking") %>' />
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
                                <td>
                                    <asp:TextBox ID="DEFTextBox" runat="server" Text='<%# Bind("DEF") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="MOVTextBox" runat="server" Text='<%# Bind("MOV") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ATTTextBox" runat="server" Text='<%# Bind("ATT") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="UnitTextBox" runat="server" Text='<%# Bind("Unit") %>' />
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
                            <tr style="background-color: #FFFFFF;color: #284775;">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="挿入" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="クリア" />
                                </td>
                                <td>
                                    <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Ranking") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="RarityTextBox" runat="server" Text='<%# Bind("Rarity") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ATTTextBox" runat="server" Text='<%# Bind("ATT") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="TotalTextBox1" runat="server" Text='<%# Bind("Total") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="HPTextBox" runat="server" Text='<%# Bind("HP") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ATKTextBox" runat="server" Text='<%# Bind("ATK") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="DEFTextBox" runat="server" Text='<%# Bind("DEF") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="MOVTextBox" runat="server" Text='<%# Bind("MOV") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="UnitTextBox" runat="server" Text='<%# Bind("Unit") %>' />
                                </td>
                        
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate><!-- 各アイテム -->
                            <tr style="background-color: #FFFFFF;color: #284775;">
                        
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Ranking") %>'/>
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="ATTLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                    </div>
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="TotalLabel1" runat="server" Text='<%# Eval("Total") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="HPLabel" runat="server" Text='<%# Eval("HP") %>' />
                                    </div>
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="ATKLabel" runat="server" Text='<%# Eval("ATK") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="DEFLabel" runat="server" Text='<%# Eval("DEF") %>' />
                                    </div>
                                </td>
                                <td style="background-color: #E0FFFF;color: #333333;">
                                    <div  class="text-center">
                                        <asp:Label ID="MOVLabel" runat="server" Text='<%# Eval("MOV") %>' />
                                    </div>
                                </td>
                                
                                <td>
                                    <div  class="text-center">
                                        <asp:Label ID="UnitLabel" runat="server"  Text='<%# Eval("Unit") %>' />
                                    </div>
                                </td>
                        
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate><!-- ヘッダー部 -->
                            <table runat="server" >
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;" class="text-nowrap">
                                            <tr runat="server" style="background-color: #777776;color: #ffffff;">
                                                <th runat="server"><div  class="text-center">順位</div></th>
                                                <th runat="server"><div  class="text-center">名前</div></th>
                                                <th runat="server"><div  class="text-center">レア</div></th>
                                                <th runat="server"><div  class="text-center">属性</div></th>
                                                <th runat="server"><div  class="text-center">総合力</div></th>
                                                <th runat="server"><div  class="text-center">HP</div></th>
                                                <th runat="server"><div  class="text-center">攻撃力</div></th>
                                                <th runat="server"><div  class="text-center">防御力</div></th>
                                                <th runat="server"><div  class="text-center">移動力</div></th>
                                                <th runat="server"><div  class="text-center">所属国家</div></th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">
                                <td>
                                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Ranking") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="RarityLabel" runat="server" Text='<%# Eval("Rarity") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ATTLabel" runat="server" Text='<%# Eval("ATT") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TotalLabel1" runat="server" Text='<%# Eval("Total") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="HPLabel" runat="server" Text='<%# Eval("HP") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ATKLabel" runat="server" Text='<%# Eval("ATK") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DEFLabel" runat="server" Text='<%# Eval("DEF") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MOVLabel" runat="server" Text='<%# Eval("MOV") %>' />
                                </td>
                                
                                <td>
                                    <asp:Label ID="UnitLabel" runat="server" Text='<%# Eval("Unit") %>' />
                                </td>
                        
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    
                </div>
            </div>

        </div><!-- コンテナ定義終わり -->
    
    </div>
</asp:Content>
