<%@ Page Title="花騎士登録" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FKG_register.aspx.cs" Inherits="花騎士ツール＿NEO.FKG_register" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <h3>登録画面</h3>
                <p>ここで登録したキャラに対して、編成シミュ上で花騎士リストを表示した際に、#マークが名前の横に付きます。</p>
                <p>現在のDBへの花騎士登録数（星6のみ）</p>
                <asp:Label ID="Label2101" runat="server" Text="0人"></asp:Label>
                <p>あなたがクッキーに登録した花騎士数</p>
                <asp:Label ID="Label2102" runat="server" Text="0人"></asp:Label>
                <asp:HiddenField ID="HiddenField0" runat="server" />
                <p>その内昇華した花騎士数</p>
                <asp:Label ID="Label2103" runat="server" Text="0人"></asp:Label>
            </div>
            <div class="col-xs-12">
                <div class="form-group">
                    <h4>花騎士名1</h4>
                    <asp:DropDownList ID="DropDownList2001" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label2001" runat="server" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenValue1" runat="server" />
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList2001" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Skil Lv1</asp:ListItem>
                        <asp:ListItem Value="2">Skil Lv2</asp:ListItem>
                        <asp:ListItem Value="3">Skil LV3</asp:ListItem>
                        <asp:ListItem Value="4">Skil Lv4</asp:ListItem>
                        <asp:ListItem Value="5">Skil Lv5</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                
            </div>

            <div class="col-xs-12">
                <div class="form-group">
                    <h4>花騎士名2</h4>
                    <asp:DropDownList ID="DropDownList2002" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label2002" runat="server" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenValue2" runat="server" />
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList2002" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Skil Lv1</asp:ListItem>
                            <asp:ListItem Value="2">Skil Lv2</asp:ListItem>
                            <asp:ListItem Value="3">Skil LV3</asp:ListItem>
                            <asp:ListItem Value="4">Skil Lv4</asp:ListItem>
                            <asp:ListItem Value="5">Skil Lv5</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="col-xs-12">
                <div class="form-group">
                    <h4>花騎士名3</h4>
                    <asp:DropDownList ID="DropDownList2003" runat="server"></asp:DropDownList>
                    
                    <asp:Label ID="Label2003" runat="server" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenValue3" runat="server" />
                    
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList2003" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Skil Lv1</asp:ListItem>
                        <asp:ListItem Value="2">Skil Lv2</asp:ListItem>
                        <asp:ListItem Value="3">Skil LV3</asp:ListItem>
                        <asp:ListItem Value="4">Skil Lv4</asp:ListItem>
                        <asp:ListItem Value="5">Skil Lv5</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="col-xs-12">
                <div class="form-group">
                    <h4>花騎士名4</h4>
                    <asp:DropDownList ID="DropDownList2004" runat="server"></asp:DropDownList>
                    
                    <asp:Label ID="Label2004" runat="server" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenValue4" runat="server" />
                    
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList2004" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Skil Lv1</asp:ListItem>
                        <asp:ListItem Value="2">Skil Lv2</asp:ListItem>
                        <asp:ListItem Value="3">Skil LV3</asp:ListItem>
                        <asp:ListItem Value="4">Skil Lv4</asp:ListItem>
                        <asp:ListItem Value="5">Skil Lv5</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="col-xs-12">
                <div class="form-group">
                    <h4>花騎士名5</h4>
                    <asp:DropDownList ID="DropDownList2005" runat="server"></asp:DropDownList>
                    
                    <asp:Label ID="Label2005" runat="server" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenValue5" runat="server" />
                    
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList2005" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Skil Lv1</asp:ListItem>
                        <asp:ListItem Value="2">Skil Lv2</asp:ListItem>
                        <asp:ListItem Value="3">Skil LV3</asp:ListItem>
                        <asp:ListItem Value="4">Skil Lv4</asp:ListItem>
                        <asp:ListItem Value="5">Skil Lv5</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-4">
                <div class="form-group">
                    <asp:Button ID="Button101" runat="server" Text="登録" CssClass="btn-primary btn-lg " Font-Bold="True" OnClick="Button101_Click"  />
                </div>
            </div>
            <div class="col-xs-4">
                <!--dummy-->
            </div>
            <div class="col-xs-4">
                <div class="form-group">
                    <asp:Button ID="Button102" class="btn btn-danger btn-group" runat="server" Text="登録全削除" OnClick="Button102_Click"/>
                    <p style="color: #FF0000; font-weight: bold">注意！押すと登録データ消えます。緊急用</p>
                    <asp:CheckBox ID="CheckBox1" runat="server" text="消去用チェック"/>
                </div>
            </div>
        </div>

        <!--編集画面-->
        <div class="row">
            <div class="col-xs-12">
                <h3>編集用コマンド</h3>
                <p>既に登録されているデータに対して、修正出来ます。</p>
                <h4>〇スキルレベル修正</h4>
                <p>修正したい花騎士名を選択し、スキルレベルを変更後、スキルレベル変更ボタンを</p>
                <p>押して下さい。クッキーのデータを書き換えます。</p>
                <h4>〇データ削除</h4>
                <p>削除したい花騎士名を選択し、削除変更ボタンを</p>
                <p>押して下さい。クッキーのデータを書き換えます。</p>
                <div class="form-group">
                    <asp:DropDownList ID="DropDownList2011" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2012" runat="server">
                        <asp:ListItem Value="1">SkillLV1</asp:ListItem>
                        <asp:ListItem Value="2">SkillLV2</asp:ListItem>
                        <asp:ListItem Value="3">SkillLV3</asp:ListItem>
                        <asp:ListItem Value="4">SkillLV4</asp:ListItem>
                        <asp:ListItem Value="5">SkillLV5</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <input id="Button111" class="btn btn-success btn-group" type="button" value="スキルレベル変更" onclick="changeSlv()"/>
                </div>
                <div class="form-group">
                    <input id="Button112" class="btn btn-danger btn-group" type="button" value="削除" onclick="DeleteFkg()"/>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-lg-10">
                <h3>登録データ一覧</h3>
                <h5>削除した場合はこの表示が更新されないので注意して下さい（いずれ修正します）</h5>
                <h5>更新する為には再度「花騎士登録」を開く必要があります</h5>
                    <div id="grid">
                    </div>
                <br />
                <div class="form-group">
                    <input id="export-file" class="btn btn-succes btn-group" type="button" value="csv出力" />
                </div>
                <p>macでは開かないかもしれません（未確認）</p>
            </div>
            <div class="col-xs-2 visible-lg">
                <a runat="server" href="~/DB_register"><h2 style="color: #FFFFFF">DB登録</h2></a>
            </div>
        </div>
    </div><!--コンテナ終了-->
</asp:Content>