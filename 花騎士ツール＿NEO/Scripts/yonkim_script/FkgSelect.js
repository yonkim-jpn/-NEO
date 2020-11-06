//デバイス判定
//var deviceType = 0;//1:タッチ 2:マウス


//document.addEventListener("touchstart", detectDeviceType);
//document.addEventListener("mousemove", detectDeviceType);

//function detectDeviceType(event) {
//    deviceType = event.changedTouches ? 1 : 2;

//    document.removeEventListener("touchstart", detectDeviceType);
//    document.removeEventListener("mousemove", detectDeviceType);

//    var str = location.href;//ページ名取得
//    if (str.match("/")) {
//        if (deviceType !== 2)
//            $(".contents").find(".text").css("display", "block");
//    }
//}

$(function () {

    //master内でちょっとしたお遊び
    $(".dropdown-menu li").hover(function () {
        $(this).animate({
            "opacity": "1"
        },100)

     },function () {
        $(this).animate({
            "opacity": "0.5"
        },500)
        });


    //上の開ボタン
    //一発目だけなぜか閉じてしまうので、それを防ぐ処理を入れた
    $('#openall').click(function (event) {
        if (!($('#fkgdata1').hasClass('in'))) {
            $('#fkgdata1').collapse('show');
        }

        if (!($('#fkgdata2').hasClass('in'))) {
            $('#fkgdata2').collapse('show');
        }
        if (!($('#fkgdata3').hasClass('in'))) {
            $('#fkgdata3').collapse('show');
        }
        if (!($('#fkgdata4').hasClass('in'))) {
            $('#fkgdata4').collapse('show');
        }
        if (!($('#fkgdata5').hasClass('in'))) {
            $('#fkgdata5').collapse('show');
        }

        //$('#fkgdata2').collapse('show');
        //$('#fkgdata3').collapse('show');
        //$('#fkgdata4').collapse('show');
        //$('#fkgdata5').collapse('show');

        event.preventDefault();
    });

    //上の閉ボタン
    $('#closeall').click(function (event) {
        $('#fkgdata1').collapse('hide');
        $('#fkgdata2').collapse('hide');
        $('#fkgdata3').collapse('hide');
        $('#fkgdata4').collapse('hide');
        $('#fkgdata5').collapse('hide');
        event.preventDefault();
    });

    //下の開ボタン
    $('#openall2').click(function (event) {
        $('#fkgdata1').collapse('show');
        $('#fkgdata2').collapse('show');
        $('#fkgdata3').collapse('show');
        $('#fkgdata4').collapse('show');
        $('#fkgdata5').collapse('show');
        //document.getElementById('Button10').scrollIntoView(false);

        //計算開始ボタンまで画面スクロール
        var LowerOpen = 1;

        $('#fkgdata5').on('shown.bs.collapse', function () {
            if (LowerOpen == 1) {
                document.getElementById('Button10').scrollIntoView(false);
                window.scrollBy(0, 50);
                LowerOpen = 0;
            }
        });

        event.preventDefault();

    });


    //下の閉ボタン
    $('#closeall2').click(function (event) {
        $('#fkgdata1').collapse('hide');
        $('#fkgdata2').collapse('hide');
        $('#fkgdata3').collapse('hide');
        $('#fkgdata4').collapse('hide');
        $('#fkgdata5').collapse('hide');


        //$('#Button10').get(0).scrollIntoView(true);
        //計算開始ボタンまで画面スクロール
        var LowerClose = 1;

        $('#fkgdata5').on('hidden.bs.collapse', function () {
            if (LowerClose == 1) {
                document.getElementById('Button10').scrollIntoView(false);
                //window.scrollBy(0, 50);
                LowerClose = 0;
            }
        });
        event.preventDefault();
    });


    //編成シミュ内の花騎士選択ドロップダウンリストを変更した場合
    //選択した花騎士名に基づいて、スキルレベルを変更する処理
    $('#MainContent_DropDownList3').change(function () {
        var selectedText = $("#MainContent_DropDownList3 option:selected").text();
        //選択された文字列に#が含まれない場合は処理をしない
        if (selectedText.indexOf('#') == -1) {
            return;
        }
        //スキルレベル変更しないチェックがonで処理しない
        if ($("#MainContent_CheckBox3").prop("checked") == true) {
            return;
        }
        var selectedValue = $("#MainContent_DropDownList3").val();
        var Slv = GetSlv(selectedValue);
        $("#MainContent_DropDownList16").val(Slv);
    });

    $('#MainContent_DropDownList6').change(function () {
        var selectedText = $("#MainContent_DropDownList6 option:selected").text();
        //選択された文字列に#が含まれない場合は処理をしない
        if (selectedText.indexOf('#') == -1) {
            return;
        }
        //スキルレベル変更しないチェックがonで処理しない
        if ($("#MainContent_CheckBox3").prop("checked") == true) {
            return;
        }
        var selectedValue = $("#MainContent_DropDownList6").val();
        var Slv = GetSlv(selectedValue);
        $("#MainContent_DropDownList17").val(Slv);
    });

    $('#MainContent_DropDownList9').change(function () {
        var selectedText = $("#MainContent_DropDownList9 option:selected").text();
        //選択された文字列に#が含まれない場合は処理をしない
        if (selectedText.indexOf('#') == -1) {
            return;
        }
        //スキルレベル変更しないチェックがonで処理しない
        if ($("#MainContent_CheckBox3").prop("checked") == true) {
            return;
        }
        var selectedValue = $("#MainContent_DropDownList9").val();
        var Slv = GetSlv(selectedValue);
        $("#MainContent_DropDownList18").val(Slv);
    });

    $('#MainContent_DropDownList12').change(function () {
        var selectedText = $("#MainContent_DropDownList12 option:selected").text();
        //選択された文字列に#が含まれない場合は処理をしない
        if (selectedText.indexOf('#') == -1) {
            return;
        }
        //スキルレベル変更しないチェックがonで処理しない
        if ($("#MainContent_CheckBox3").prop("checked") == true) {
            return;
        }
        var selectedValue = $("#MainContent_DropDownList12").val();
        var Slv = GetSlv(selectedValue);
        $("#MainContent_DropDownList19").val(Slv);
    });

    $('#MainContent_DropDownList15').change(function () {
        var selectedText = $("#MainContent_DropDownList15 option:selected").text();
        //選択された文字列に#が含まれない場合は処理をしない
        if (selectedText.indexOf('#') == -1) {
            return;
        }
        //スキルレベル変更しないチェックがonで処理しない
        if ($("#MainContent_CheckBox3").prop("checked") == true) {
            return;
        }
        var selectedValue = $("#MainContent_DropDownList15").val();
        var Slv = GetSlv(selectedValue);
        $("#MainContent_DropDownList20").val(Slv);
    });

    //フォーム読込時より発動
    $(document).ready(function () {
        //when a group is shown, save it as the active accordion group
        //alert("フォーム読込");


        //編成シミュの場合の処理
        var str = location.href;//ページ名取得
        if (str.match("/Fkg_select_neo")) {

            //
            //フォーム再読み込み時の閉状態確認処理
            //
            var last1 = Cookies.get('fkgdata1Hidden');
            if (last1 == 1) {
                //alert("1を閉じる");
                $('#fkgdata1').collapse('hide');
            }

            var last2 = Cookies.get('fkgdata2Hidden');
            if (last2 == 1) {
                //alert("1を閉じる");
                $('#fkgdata2').collapse('hide');
            }

            var last3 = Cookies.get('fkgdata3Hidden');
            if (last3 == 1) {
                //alert("1を閉じる");
                $('#fkgdata3').collapse('hide');
            }

            var last4 = Cookies.get('fkgdata4Hidden');
            if (last4 == 1) {
                //alert("1を閉じる");
                $('#fkgdata4').collapse('hide');
            }

            var last5 = Cookies.get('fkgdata5Hidden');
            if (last5 == 1) {
                //alert("1を閉じる");
                $('#fkgdata5').collapse('hide');
            }

            //一つ目
            //
            $("#fkgdata1").on('hidden.bs.collapse', function () {
                Cookies.set('fkgdata1Hidden', 1);
                Cookies.remove('fkgdata1Shown');
            });
            $("#fkgdata1").on('shown.bs.collapse', function () {
                Cookies.set('fkgdata1Shown', 1);
                Cookies.remove('fkgdata1Hidden');
            });

            //二つ目
            //
            $("#fkgdata2").on('hidden.bs.collapse', function () {
                Cookies.set('fkgdata2Hidden', 1);
                Cookies.remove('fkgdata2Shown');
            });
            $("#fkgdata2").on('shown.bs.collapse', function () {
                Cookies.set('fkgdata2Shown', 1);
                Cookies.remove('fkgdata2Hidden');
            });


            //三つ目
            //
            $("#fkgdata3").on('hidden.bs.collapse', function () {
                Cookies.set('fkgdata3Hidden', 1);
                Cookies.remove('fkgdata3Shown');
            });
            $("#fkgdata3").on('shown.bs.collapse', function () {
                Cookies.set('fkgdata3Shown', 1);
                Cookies.remove('fkgdata3Hidden');
            });


            //四つ目
            //
            $("#fkgdata4").on('hidden.bs.collapse', function () {
                Cookies.set('fkgdata4Hidden', 1);
                Cookies.remove('fkgdata4Shown');
            });
            $("#fkgdata4").on('shown.bs.collapse', function () {
                Cookies.set('fkgdata4Shown', 1);
                Cookies.remove('fkgdata4Hidden');
            });


            //五つ目
            //
            $("#fkgdata5").on('hidden.bs.collapse', function () {
                Cookies.set('fkgdata5Hidden', 1);
                //Cookies.set('fkgdata5Hidden', 1, { expires: 7 });
                Cookies.remove('fkgdata5Shown');
            });
            $("#fkgdata5").on('shown.bs.collapse', function () {
                Cookies.set('fkgdata5Shown', 1);
                Cookies.remove('fkgdata5Hidden');
            });

        }
        
        if (str.match("/FKG_register")) {
            Iniciar_FKG_register();
        }
    });

    //ランキングの短縮名表示チェックボックス処理
    //CheckBox1の状態が変化したときに、フラグを入れる
    $("#MainContent_CheckBox1").change(function () {
        if ($("#MainContent_CheckBox1").prop("checked") == true) {
            //チェックonならクッキーに1をセット
            Cookies.set('nameShort', 1);
        } else if ($("#MainContent_CheckBox1").prop("checked") == false) {
            //チェックoffならクッキーに0をセット
            Cookies.set('nameShort', 0);
        }

    });

    //編成シミュの短縮名表示チェックボックス処理
    //CheckBox2の状態が変化したときに、フラグを入れる
    $("#MainContent_CheckBox2").change(function () {
        if ($("#MainContent_CheckBox2").prop("checked") == true) {
            //チェックonならクッキーに1をセット
            Cookies.set('nameShort2', 1);
        } else if ($("#MainContent_CheckBox2").prop("checked") == false) {
            //チェックoffならクッキーに0をセット
            Cookies.set('nameShort2', 0);
        }
    });

    //CheckBox3の状態が変化したときに、フラグを入れる
    //スキルレベル変更フラグ
    $("#MainContent_CheckBox3").change(function () {
        if ($("#MainContent_CheckBox3").prop("checked") == true) {
            //チェックonならクッキーに1をセット
            Cookies.set('noChangeSlv', 1);
        } else if ($("#MainContent_CheckBox2").prop("checked") == false) {
            //チェックoffならクッキーに0をセット
            Cookies.set('noChangeSlv', 0);
        }
    });

    $("#MainContent_ListView1_itemPlaceholderContainer").on("inview", function () {
        //ブラウザの表示域に表示されたときに実行する処理
        var str = location.href;//ページ名取得

        //編成シミュの場合の処理
        //
        if (str.match("/Fkg_select_neo")) {
            HideText1();
            //クッキーがある場合は、名前短縮処理フラグが1ならば、チェックボックスにチェック入れる
            var checkBox2 = document.getElementById('MainContent_CheckBox2');
            if (!Cookies.get("nameShort2")) {
                //クッキー無し
            } else {
                //クッキー有
                var checkBox2flag = Cookies.get("nameShort2");
                if (checkBox2flag == 1) {
                    checkBox2.checked = true;
                } else if (checkBox2flag == 0) {
                    checkBox2.checked = false;
                }
            }

            //クッキーがある場合は、スキルレベル変更しないフラグが1ならば、チェックボックスにチェック入れる
            var checkBox3 = document.getElementById('MainContent_CheckBox3');
            if (!Cookies.get("noChangeSlv")) {
                //クッキー無し
            } else {
                //クッキー有
                var checkBox3flag = Cookies.get("noChangeSlv");
                if (checkBox3flag == 1) {
                    checkBox3.checked = true;
                } else if (checkBox3flag == 0) {
                    checkBox3.checked = false;
                }
            }
        }
        
        
    });

    //画面ロード、リサイズ時の処理
    $(window).on('load resize', function () {
        var str = location.href;//ページ名取得

        //編成シミュの場合の処理
        //
        if (str.match("/Fkg_select_neo")) {
            //クッキーがある場合は、名前短縮処理フラグが1ならば、チェックボックスにチェック入れる
            var checkBox2 = document.getElementById('MainContent_CheckBox2');
            if (!Cookies.get("nameShort2")) {
                //クッキー無し
            } else {
                //クッキー有
                var checkBox2flag = Cookies.get("nameShort2");
                if (checkBox2flag == 1) {
                    checkBox2.checked = true;
                } else if (checkBox2flag == 0) {
                    checkBox2.checked = false;
                }
            }
            HideText1();

            //クッキーがある場合は、スキルレベル変更しないフラグが1ならば、チェックボックスにチェック入れる
            var checkBox3 = document.getElementById('MainContent_CheckBox3');
            if (!Cookies.get("noChangeSlv")) {
                //クッキー無し
            } else {
                //クッキー有
                var checkBox3flag = Cookies.get("noChangeSlv");
                if (checkBox3flag == 1) {
                    checkBox3.checked = true;
                } else if (checkBox3flag == 0) {
                    checkBox3.checked = false;
                }
            }
        }
        

        //アビリティ検索の場合の処理
        //


        //ランキングの場合の処理
        //
        if (str.match("/Fkg_ranking_neo")) {
            var w = $(window).width();
            var x = 768;

            if (w < x) {
                //画面サイズが768px未満のときの処理

                //クッキーがある場合は、名前短縮処理フラグが1ならば、チェックボックスにチェック入れる
                var checkBox1 = document.getElementById('MainContent_CheckBox1');
                if (!Cookies.get("nameShort")) {
                    //クッキー無し
                } else {
                    //クッキー有
                    var checkBox1flag = Cookies.get("nameShort");
                    if (checkBox1flag == 1) {
                        checkBox1.checked = true;
                    } else if (checkBox1flag == 0) {
                        checkBox1.checked = false;
                    }
                }

                //名前短縮処理フラグをクッキーにセット
                
                //if (checkBox1.checked == true) {
                //    Cookies.set('nameShort', 1);
                //} else {
                //    Cookies.set('nameShort', 0);
                //}

                HideText3();

            } else {
                //それ以外のときの処理
            }
            //テキスト変更処理
            ChangeText();
        }

        //団長登録の場合の処理
        //
        if (str.match("/FKG_register")) {
            Iniciar_FKG_register();
        }
        
    });

    //ページ表示後の処理Load
    $(window).ready(function () {
        var str = location.href;//ページ名取得
        if (str.match("/Fkg_select_neo")) {

            var selectedText = $("#MainContent_DropDownList3 option:selected").text();
            //選択された文字列に#が含まれない場合は処理をしない
            if (selectedText.indexOf('#') == -1) {
                return;
            }
            //スキルレベル変更しないチェックがonで処理しない
            if ($("#MainContent_CheckBox3").prop("checked") == true) {
                return;
            }
            var selectedValue = $("#MainContent_DropDownList3").val();
            var Slv = GetSlv(selectedValue);
            $("#MainContent_DropDownList16").val(Slv);


            selectedText = $("#MainContent_DropDownList6 option:selected").text();
            //選択された文字列に#が含まれない場合は処理をしない
            if (selectedText.indexOf('#') == -1) {
                return;
            }
            //スキルレベル変更しないチェックがonで処理しない
            if ($("#MainContent_CheckBox3").prop("checked") == true) {
                return;
            }
            selectedValue = $("#MainContent_DropDownList6").val();
            Slv = GetSlv(selectedValue);
            $("#MainContent_DropDownList17").val(Slv);


            selectedText = $("#MainContent_DropDownList9 option:selected").text();
            //選択された文字列に#が含まれない場合は処理をしない
            if (selectedText.indexOf('#') == -1) {
                return;
            }
            //スキルレベル変更しないチェックがonで処理しない
            if ($("#MainContent_CheckBox3").prop("checked") == true) {
                return;
            }
            selectedValue = $("#MainContent_DropDownList9").val();
            Slv = GetSlv(selectedValue);
            $("#MainContent_DropDownList18").val(Slv);


            selectedText = $("#MainContent_DropDownList12 option:selected").text();
            //選択された文字列に#が含まれない場合は処理をしない
            if (selectedText.indexOf('#') == -1) {
                return;
            }
            //スキルレベル変更しないチェックがonで処理しない
            if ($("#MainContent_CheckBox3").prop("checked") == true) {
                return;
            }
            selectedValue = $("#MainContent_DropDownList12").val();
            Slv = GetSlv(selectedValue);
            $("#MainContent_DropDownList19").val(Slv);


            selectedText = $("#MainContent_DropDownList15 option:selected").text();
            //選択された文字列に#が含まれない場合は処理をしない
            if (selectedText.indexOf('#') == -1) {
                return;
            }
            //スキルレベル変更しないチェックがonで処理しない
            if ($("#MainContent_CheckBox3").prop("checked") == true) {
                return;
            }
            selectedValue = $("#MainContent_DropDownList15").val();
            Slv = GetSlv(selectedValue);
            $("#MainContent_DropDownList20").val(Slv);
        }

        if (str.match("/Fkg_ranking_neo")) {
            var checkBox1 = document.getElementById('MainContent_CheckBox1');
            if (checkBox1.checked == true) {
                HideText3();
            }
            //テキスト変更処理
            ChangeText();
        }
        if (str.match("/Fkg_ability_serch_neo")) {
            ActivateTextBox(1);
            ActivateTextBox(2);
            ActivateTextBox(3);
        }
        

        if (!str.match("/FKG_register")) {
            //ページが団長登録でなければ抜ける
            return;
        }
        else {
            Iniciar_FKG_register();
        }
                
        return;
    });



    //非同期AJAX動作時、Updatepanel内のイベントを発火させる為使用
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(
        function (evt, args) {
            var str = location.href;//ページ名取得

            if (str.match("/Fkg_select_neo")) {
                //編成シミュの場合の処理
                //
                //編成シミュ内の花騎士選択ドロップダウンリストを変更した場合
                //選択した花騎士名に基づいて、スキルレベルを変更する処理
                $('#MainContent_DropDownList3').change(function () {
                    var selectedText = $("#MainContent_DropDownList3 option:selected").text();
                    //選択された文字列に#が含まれない場合は処理をしない
                    if (selectedText.indexOf('#') == -1) {
                        return;
                    }
                    //スキルレベル変更しないチェックがonで処理しない
                    if ($("#MainContent_CheckBox3").prop("checked") == true) {
                        return;
                    }
                    var selectedValue = $("#MainContent_DropDownList3").val();
                    var Slv = GetSlv(selectedValue);
                    $("#MainContent_DropDownList16").val(Slv);
                });

                $('#MainContent_DropDownList6').change(function () {
                    var selectedText = $("#MainContent_DropDownList6 option:selected").text();
                    //選択された文字列に#が含まれない場合は処理をしない
                    if (selectedText.indexOf('#') == -1) {
                        return;
                    }
                    //スキルレベル変更しないチェックがonで処理しない
                    if ($("#MainContent_CheckBox3").prop("checked") == true) {
                        return;
                    }
                    var selectedValue = $("#MainContent_DropDownList6").val();
                    var Slv = GetSlv(selectedValue);
                    $("#MainContent_DropDownList17").val(Slv);
                });

                $('#MainContent_DropDownList9').change(function () {
                    var selectedText = $("#MainContent_DropDownList9 option:selected").text();
                    //選択された文字列に#が含まれない場合は処理をしない
                    if (selectedText.indexOf('#') == -1) {
                        return;
                    }
                    //スキルレベル変更しないチェックがonで処理しない
                    if ($("#MainContent_CheckBox3").prop("checked") == true) {
                        return;
                    }
                    var selectedValue = $("#MainContent_DropDownList9").val();
                    var Slv = GetSlv(selectedValue);
                    $("#MainContent_DropDownList18").val(Slv);
                });

                $('#MainContent_DropDownList12').change(function () {
                    var selectedText = $("#MainContent_DropDownList12 option:selected").text();
                    //選択された文字列に#が含まれない場合は処理をしない
                    if (selectedText.indexOf('#') == -1) {
                        return;
                    }
                    //スキルレベル変更しないチェックがonで処理しない
                    if ($("#MainContent_CheckBox3").prop("checked") == true) {
                        return;
                    }
                    var selectedValue = $("#MainContent_DropDownList12").val();
                    var Slv = GetSlv(selectedValue);
                    $("#MainContent_DropDownList19").val(Slv);
                });

                $('#MainContent_DropDownList15').change(function () {
                    var selectedText = $("#MainContent_DropDownList15 option:selected").text();
                    //選択された文字列に#が含まれない場合は処理をしない
                    if (selectedText.indexOf('#') == -1) {
                        return;
                    }
                    //スキルレベル変更しないチェックがonで処理しない
                    if ($("#MainContent_CheckBox3").prop("checked") == true) {
                        return;
                    }
                    var selectedValue = $("#MainContent_DropDownList15").val();
                    var Slv = GetSlv(selectedValue);
                    $("#MainContent_DropDownList20").val(Slv);
                });
            }

            else if (str.match("/Fkg_ability_serch_neo")) {
                CheckCharacter();
                $('#MainContent_DropDownList1').change(function () {
                    ActivateTextBox(1);
                });
                $('#MainContent_DropDownList2').change(function () {
                    ActivateTextBox(2);
                });
                $('#MainContent_DropDownList3').change(function () {
                    ActivateTextBox(3);
                });

                //$('input[name="ctl00$MainContent$RadioButtonList2"]').change(function () {

                //    if ($("input[name='ctl00$MainContent$RadioButtonList2'][value='1']:checked").val() == '1') {
                //        //クッキーを確認し、アイテムが無ければチェックを強引にオフにするｗ
                //        if (!Cookies.get('FkgName')) {

                //            $("input[name='ctl00$MainContent$RadioButtonList2'][value='0']").prop("checked", true);
                //            $("#MainContent_advertencia").text("登録されてないのでチェック出来ません");
                //            return;
                //        }
                //    }
                //});

                //$('#MainContent_RadioButtonList2_1').click(function () {
                //    if ($(this).prop('checked') == true) {
                //        //クッキーを確認し、アイテムが無ければチェックを強引にオフにするｗ
                //        if (!Cookies.get('FkgName')) {
                //            $('#MainContent_RadioButtonList2').prop("checked", true);
                //            $("#MainContent_advertencia").text("登録されてないのでチェック出来ません");
                //            return;
                //        }
                //    }
                //});
                //MainContent_CheckBox1
            }
        });
    //////////////////////////////
    //
    //団長登録画面の処理
    //
    //ドロップダウンリストで選択されている項目が登録されているか確認する処理
    $('#MainContent_DropDownList2001').change(function () {
        $('#r1').html($(this).val());

        var obj = document.getElementById("MainContent_Label2001");
        obj.textContent = checkCookie("MainContent_DropDownList2001");

        document.getElementById("MainContent_HiddenValue1").value = checkCookie("MainContent_DropDownList2001");
    });

    $('#MainContent_DropDownList2002').change(function () {
        $('#r1').html($(this).val());

        var obj = document.getElementById("MainContent_Label2002");
        obj.textContent = checkCookie("MainContent_DropDownList2002");

        document.getElementById("MainContent_HiddenValue2").value = checkCookie("MainContent_DropDownList2002");
    });
    
    $('#MainContent_DropDownList2003').change(function () {
        $('#r1').html($(this).val());

        var obj = document.getElementById("MainContent_Label2003");
        obj.textContent = checkCookie("MainContent_DropDownList2003");

        document.getElementById("MainContent_HiddenValue3").value = checkCookie("MainContent_DropDownList2003");
    });

    $('#MainContent_DropDownList2004').change(function () {
        $('#r1').html($(this).val());

        var obj = document.getElementById("MainContent_Label2004");
        obj.textContent = checkCookie("MainContent_DropDownList2004");

        document.getElementById("MainContent_HiddenValue4").value = checkCookie("MainContent_DropDownList2004");
    });

    $('#MainContent_DropDownList2005').change(function () {
        $('#r1').html($(this).val());

        var obj = document.getElementById("MainContent_Label2005");
        obj.textContent = checkCookie("MainContent_DropDownList2005");

        document.getElementById("MainContent_HiddenValue5").value = checkCookie("MainContent_DropDownList2005");
    });

    //編集内のドロップダウンリストを変更した場合
    //選択した花騎士名に基づいて、スキルレベルを変更する処理
    $('#MainContent_DropDownList2011').change(function () {
        var selectedValue = $("#MainContent_DropDownList2011").val();
        selectedValue = GetSlv(selectedValue.slice(2));
        $("#MainContent_DropDownList2012").val(selectedValue);
    });

    var mouseCount = 0;
    $(document).on('scroll mouseover',function () {
        //登録画面初期化
        var str = location.href;//ページ名取得
        if (str.match("/Fkg_register")&&mouseCount===0) {
            Iniciar_FKG_register();
        }
        mouseCount++;
    });


    ////////////////////////////////////////////////////
    //おまけ
    //

    ////
    //アンプルゥ自動計算
    ////


    //HP
    $("#MainContent_TextBox1").change(function () {
        //値が変更されたときの処理
        //check value
        var inputText1 = $('#MainContent_TextBox1').val();
        inputText1 = Number(inputText1);
        if ((inputText1 >= 0) && (inputText1 <= 6000)) {
            if (inputText1 >= 3000) {
                var ampul1 = 100;
                var hiampul1 = Math.floor((inputText1 - 3000) / 30);
            }
            else {
                ampul1 = Math.floor(inputText1 / 30);
                hiampul1 = 0;
            }
            $('#MainContent_TextBox2').val(ampul1);
            $('#MainContent_TextBox3').val(hiampul1);
        }
        else {
            $('#MainContent_TextBox1').val("想定外だよ");
        }

    });

    //命の通常アンプル数変更
    $("#MainContent_TextBox2").change(function () {
        var ampul1 = $('#MainContent_TextBox2').val();
        ampul1 = Number(ampul1);
        if ((ampul1 >= 0) && (ampul1 <= 100)) {
            var inputText1 = ampul1 * 30;
            var hiampul1 = 0;


            $('#MainContent_TextBox1').val(inputText1);
            $('#MainContent_TextBox3').val(hiampul1);
        }
        else {
            $('#MainContent_TextBox2').val("想定外だよ");
        }
    });

    //命の上位アンプル数変更
    $("#MainContent_TextBox3").change(function () {
        var hiampul1 = $('#MainContent_TextBox3').val();
        hiampul1 = Number(hiampul1);
        if ((hiampul1 >= 0) && (hiampul1 <= 100)) {
            if (hiampul1 == 0) {
                var ampul1 = $('#MainContent_TextBox2').val();
                if (ampul1 == "想定外だよ") {
                    inputText1 = "想定外だよ";
                }
                else {
                    var inputText1 = ampul1 * 30;
                }
            }
            else {
                inputText1 = 3000 + hiampul1 * 30;
                ampul1 = 100;
            }
            $('#MainContent_TextBox1').val(inputText1);
            $('#MainContent_TextBox2').val(ampul1);
        }
        else {
            $('#MainContent_TextBox3').val("想定外だよ");
        }
    });

    //攻撃
    $("#MainContent_TextBox4").change(function () {
        //値が変更されたときの処理
        //check value
        var inputText1 = $('#MainContent_TextBox4').val();
        inputText1 = Number(inputText1);
        if ((inputText1 >= 0) && (inputText1 <= 2000)) {
            if (inputText1 >= 1000) {
                var ampul1 = 100;
                var hiampul1 = Math.floor((inputText1 - 1000) / 10);
            }
            else {
                ampul1 = Math.floor(inputText1 / 10);
                hiampul1 = 0;
            }
            $('#MainContent_TextBox5').val(ampul1);
            $('#MainContent_TextBox6').val(hiampul1);
        }
        else {
            $('#MainContent_TextBox4').val("想定外だよ");
        }

    });

    //攻の通常アンプル数変更
    $("#MainContent_TextBox5").change(function () {
        var ampul1 = $('#MainContent_TextBox5').val();
        ampul1 = Number(ampul1);
        if ((ampul1 >= 0) && (ampul1 <= 100)) {
            var inputText1 = ampul1 * 10;
            var hiampul1 = 0;


            $('#MainContent_TextBox4').val(inputText1);
            $('#MainContent_TextBox6').val(hiampul1);
        }
        else {
            $('#MainContent_TextBox5').val("想定外だよ");
        }
    });

    //攻の上位アンプル数変更
    $("#MainContent_TextBox6").change(function () {
        var hiampul1 = $('#MainContent_TextBox6').val();
        hiampul1 = Number(hiampul1);
        if ((hiampul1 >= 0) && (hiampul1 <= 100)) {
            if (hiampul1 == 0) {
                var ampul1 = $('#MainContent_TextBox5').val();
                if (ampul1 == "想定外だよ") {
                    inputText1 = "想定外だよ";
                }
                else {
                    var inputText1 = ampul1 * 10;
                }
            }
            else {
                inputText1 = 1000 + hiampul1 * 10;
                ampul1 = 100;
            }
            $('#MainContent_TextBox4').val(inputText1);
            $('#MainContent_TextBox5').val(ampul1);
        }
        else {
            $('#MainContent_TextBox6').val("想定外だよ");
        }
    });

    //防御
    $("#MainContent_TextBox7").change(function () {
        //値が変更されたときの処理
        //check value
        var inputText1 = $('#MainContent_TextBox7').val();
        inputText1 = Number(inputText1);
        if ((inputText1 >= 0) && (inputText1 <= 800)) {
            if (inputText1 >= 400) {
                var ampul1 = 100;
                var hiampul1 = Math.floor((inputText1 - 400) / 4);
            }
            else {
                ampul1 = Math.floor(inputText1 / 4);
                hiampul1 = 0;
            }
            $('#MainContent_TextBox8').val(ampul1);
            $('#MainContent_TextBox9').val(hiampul1);
        }
        else {
            $('#MainContent_TextBox7').val("想定外だよ");
        }

    });

    //守の通常アンプル数変更
    $("#MainContent_TextBox8").change(function () {
        var ampul1 = $('#MainContent_TextBox8').val();
        ampul1 = Number(ampul1);
        if ((ampul1 >= 0) && (ampul1 <= 100)) {
            var inputText1 = ampul1 * 4;
            var hiampul1 = 0;


            $('#MainContent_TextBox7').val(inputText1);
            $('#MainContent_TextBox9').val(hiampul1);
        }
        else {
            $('#MainContent_TextBox8').val("想定外だよ");
        }
    });

    //守の上位アンプル数変更
    $("#MainContent_TextBox9").change(function () {
        var hiampul1 = $('#MainContent_TextBox9').val();
        hiampul1 = Number(hiampul1);
        if ((hiampul1 >= 0) && (hiampul1 <= 100)) {
            if (hiampul1 == 0) {
                var ampul1 = $('#MainContent_TextBox8').val();
                if (ampul1 == "想定外だよ") {
                    inputText1 = "想定外だよ";
                }
                else {
                    var inputText1 = ampul1 * 4;
                }
            }
            else {
                inputText1 = 400 + hiampul1 * 4;
                ampul1 = 100;
            }
            $('#MainContent_TextBox7').val(inputText1);
            $('#MainContent_TextBox8').val(ampul1);
        }
        else {
            $('#MainContent_TextBox9').val("想定外だよ");
        }
    });

    //昇華石必要数計算機用
    //
    //レアリティ変更時
    $('input[name="ctl00$MainContent$RadioButtonList1"]:radio').change(function () {
        var radioval = $(this).val();
        ShoukasekiCalc();
    });
    //装備枠数変更時
    $('input[name="ctl00$MainContent$RadioButtonList2"]:radio').change(function () {
        var radioval = $(this).val();
        ShoukasekiCalc();
    });

    //スキルレベル変更時
    $('input[name="ctl00$MainContent$RadioButtonList3"]:radio').change(function () {
        var radioval = $(this).val();
        ShoukasekiCalc();
    });

    //昇華石所持数入力の場合
    $("#MainContent_TextBox10").change(function () {
        ShoukasekiCalc();
    });


    //モコウ神判定用
    $('#MainContent_DropDownList1').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList2').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList3').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList4').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList5').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList6').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList7').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList8').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList9').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList10').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList11').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList12').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList13').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList14').change(function () {
        Mokou();
    });
    $('#MainContent_DropDownList15').change(function () {
        Mokou();
    });
    ////////////////////////////////////////////////////
    //闇のおまけ
    //

    ////
    //DMMポイント必要数自動計算
    ////

    //毎日
    $('input[name="ctl00$MainContent$RadioButtonList3001"]:radio').change(function () {
        var radioval = $(this).val();
        DmmPtCalc();
    });

    //ウィークリー
    $('input[name="ctl00$MainContent$RadioButtonList3002"]:radio').change(function () {
        var radioval = $(this).val();
        DmmPtCalc();
    });

    //スペチケ
    $('input[name="ctl00$MainContent$RadioButtonList3003"]:radio').change(function () {
        var radioval = $(this).val();
        DmmPtCalc();
    });

    //装花
    $('input[name="ctl00$MainContent$RadioButtonList3004"]:radio').change(function () {
        var radioval = $(this).val();
        DmmPtCalc();
    });

    //期間
    $('input[name="ctl00$MainContent$RadioButtonList3005"]:radio').change(function () {
        var radioval = $(this).val();
        DmmPtCalc();
    });

    //Default内処理
    $(".title1").mouseenter(function () {
            if (deviceType === 2) {
                $(".text1").slideDown(500, function (e) { $(".text1").stop(false, true); });
                $(".text1").addClass("open");
            }
    });

    $(".title1").mouseleave(function () {
            if (deviceType === 2) {
                if ($(this).find("open")) {
                    // $(".text1").removeClass("open");
                    // $(".text1").slideUp(500, function (e) { $(".text1").stop(false, true); });
                }
            }
    });
    $(".title2").mouseenter(function () {
            if (deviceType === 2) {
                $(".text2").slideDown(500, function (e) { $(".text2").stop(false, true); });
                $(".text2").addClass("open");
            }
    });
    $(".title2").mouseleave(function () {
            if (deviceType === 2) {
                if ($(this).find("open")) {
                    // $(".text2").removeClass("open");
                    // $(".text2").slideUp(500, function (e) { $(".text2").stop(false, true); });
                }
        };
    });
    $(".title3").mouseenter(function () {
            if (deviceType === 2) {
                $(".text3").slideDown(500, function (e) { $(".text3").stop(false, true); });
                $(".text3").addClass("open");
            }
    });
    $(".title3").mouseleave(function () {
            if (deviceType === 2) {
                if ($(this).find("open")) {
                    // $(".text3").removeClass("open");
                    // $(".text3").slideUp(500, function (e) { $(".text3").stop(false, true); });
                }
        }
    });
});

/////////////////////////////////////////////////////
//
//pageload
//
//
/////////////////////////////////////////////////////
function pageLoad() {
    var str = location.href;//ページ名取得
    if (str.match("/Fkg_ability_serch_neo")) {
        ActivateTextBox(1);
        ActivateTextBox(2);
        ActivateTextBox(3);
    }
}



//////////////////////////////
//専用関数群
//
//
//編成シミュ専用関数
//
function HideText1() {
    //テーブル取得
    var tblTbody = document.getElementById('MainContent_ListView1_itemPlaceholderContainer');
    if (!tblTbody) {
        //オブジェクト非存在
        return;
    }
    for (var i = 0, rowLen = tblTbody.rows.length; i < rowLen; i++) {
        //名前行の文字列修正
        if (i == 1) {
            
                //花騎士名の書き換え
                for (var j = 2; j < 7; j++) {
                    var thisText = tblTbody.rows[i].cells[j].textContent.trim();
                    if (thisText.indexOf('#') > -1) {
                        //#マークを抜く
                        thisText = thisText.slice(1);
                    }
                    var textLength = thisText.length;
                    if (thisText.indexOf("未選択") == -1) {
                        var fkgSelected;
                        var getText;
                        var insertText;
                        //switch (j) {
                        //    case 2:
                        //        {
                        //            fkgSelected = document.getElementById('MainContent_DropDownList3');
                        //            break;
                        //        }
                        //    case 3:
                        //        {
                        //            fkgSelected = document.getElementById('MainContent_DropDownList6');
                        //            break;
                        //        }
                        //    case 4:
                        //        {
                        //            fkgSelected = document.getElementById('MainContent_DropDownList9');
                        //            break;
                        //        }

                        //    case 5:
                        //        {
                        //            fkgSelected = document.getElementById('MainContent_DropDownList12');
                        //            break;
                        //        }

                        //    case 6:
                        //        {
                        //            fkgSelected = document.getElementById('MainContent_DropDownList15');
                        //            break;
                        //        }
                        //}
                        //選択されているか判定
                        //if ($(fkgSelected).val() != null) {
                            //getText = $(fkgSelected).children("option:selected").text();
                            if (Cookies.get("nameShort2") == 1) {
                                //insertText = ShortText(getText, 4);
                                insertText = ShortText(thisText, 4);
                            } else {
                                insertText = thisText;
                            }
                            tblTbody.rows[i].cells[j].textContent = insertText;
                            tblTbody.rows[i].cells[j].style.textAlign = "center";
                        //}




                    }


                    //var textLength = thisText.length;
                    //if (textLength > count) {
                    //    var showText = thisText.substring(0, count);
                    //    var insertText = showText;
                    //    tblTbody.rows[i].cells[j].textContent = insertText;
                    //}
                
            }
        }
        else { //raw二つ目以降
            if (i > 9) {
                //テーブルの本体の修正
                var titleText = tblTbody.rows[i].cells[0].textContent.trim();
                var maxValue = 0;
                switch(titleText)
                {

                    case "再行動": {
                        if (tblTbody.rows[i].cells[1].textContent.trim() == "1Ｔ目") {
                            maxValue = 100;
                        }
                        else {
                            //通常時
                            maxValue = 90;
                        }
                        break;
                    }

                    case "クリ発生率":
                    {
                        maxValue = 80;
                        break;
                    }
                    
                    case "":
                        {
                            switch (tblTbody.rows[i - 1].cells[0].textContent.trim()) {

                                case "クリ発生率":{
                                    maxValue = 80;
                                    break;
                                }
                                case "再行動":{
                                    maxValue = 90;
                                    break;
                                }
                            }
                        //if (tblTbody.rows[i - 1].cells[0].textContent.trim() == "クリ発生率")
                        //        maxValue = 80;
                        
                        break;
                    }
                }


                for ( j = 2; j < 7; j++) {
                    thisText = tblTbody.rows[i].cells[j].textContent.trim();
                    textLength = thisText.length;

                    if (maxValue != 0) {
                        //値が許容値を超えていれば色を変えて明示
                        thisText = thisText.substr(0, textLength - 1);
                        thisText = thisText.substr(1);
                        if (thisText > maxValue) {
                            tblTbody.rows[i].cells[j].style.color = "red";                        }
                    }
                    //変更を反映
                    //tblTbody.rows[i].cells[j].textContent = insertText;
                }
                
            }
        }
    }

    //第2テーブルへの処理
    tblTbody = document.getElementById("MainContent_ListView2_itemPlaceholderContainer");
    if (!tblTbody) {
        //オブジェクト非存在
        return;
    }
    for (i = 0, rowLen = tblTbody.rows.length; i < rowLen; i++) {
        titleText = tblTbody.rows[i].cells[0].textContent.trim();
        maxValue = 0;
        switch (titleText) {
            case "攻撃力低下率":
                {
                    maxValue = 70;
                    break;
                }
            case "スキル発動低下率":
                {
                    maxValue = 70;
                    break;
                }
            case "攻撃ミス率":
                {
                    maxValue = 90;
                    break;
                } 
        }
        thisText = tblTbody.rows[i].cells[1].textContent.trim();
        textLength = thisText.length;

        if (maxValue != 0) {
            //値が許容値を超えていれば色を変えて明示
            thisText = thisText.substr(0, textLength - 1);
            thisText = thisText.substr(1);
            if (thisText > maxValue) {
                tblTbody.rows[i].cells[1].style.color = "red";
            }
        }

    }

}

//編成シミュ内で、選択された花騎士名により、クッキー確認し、スキルレベルを算出する
function GetSlv(selectedFkgNum) {
    //クッキーの有無確認
    //あれば情報引き出し
    if (!Cookies.get('FkgName')) {
        //無ければ1を返す（デフォルト値）
        return 1;
    }
    //クッキー有
    var cookieString = Cookies.get('FkgName');
    var cookieData = cookieString.slice(1).split('#');
    var loopMax = cookieData.length;
    var serchFlag = 0;
    for (var i = 0; i < loopMax; i++) {
        //
        //偶数の場合は名前なので、一致するか検索
        if (i % 2 == 0) {
            if (cookieData[i] == selectedFkgNum) {
                serchFlag = 1;
            }
        }
        //
        //奇数の場合はスキルレベルなので、フラグがたった場合は抜ける
        if (i % 2 != 0) {
            if (serchFlag == 1) {
                return cookieData[i];
            }
        }

    }
    //見つからなければデフォルト値の1を返す
    return 1;
}


//
//アビリティ検索専用関数
//
//MainContent_CheckBoxList2_2
//document.getElementById("checkAttAll").addEventListener('click', function () {
//    CheckAbiATT();
//}, false);
function CheckAbiATT() {
    $('#MainContent_CheckBoxList2_0').prop('checked', true);
    $('#MainContent_CheckBoxList2_1').prop('checked', true);
    $('#MainContent_CheckBoxList2_2').prop('checked', true);
    $('#MainContent_CheckBoxList2_3').prop('checked', true);
}
function UnCheckAbiATT() {
    $('#MainContent_CheckBoxList2_0').prop('checked', false);
    $('#MainContent_CheckBoxList2_1').prop('checked', false);
    $('#MainContent_CheckBoxList2_2').prop('checked', false);
    $('#MainContent_CheckBoxList2_3').prop('checked', false);
}

function CheckAbiStype() {
    $('#MainContent_CheckBoxList3_0').prop('checked', true);
    $('#MainContent_CheckBoxList3_1').prop('checked', true);
    $('#MainContent_CheckBoxList3_2').prop('checked', true);
    $('#MainContent_CheckBoxList3_3').prop('checked', true);
    $('#MainContent_CheckBoxList3_4').prop('checked', true);
    $('#MainContent_CheckBoxList3_5').prop('checked', true);
}
function UnCheckAbiStype() {
    $('#MainContent_CheckBoxList3_0').prop('checked', false);
    $('#MainContent_CheckBoxList3_1').prop('checked', false);
    $('#MainContent_CheckBoxList3_2').prop('checked', false);
    $('#MainContent_CheckBoxList3_3').prop('checked', false);
    $('#MainContent_CheckBoxList3_4').prop('checked', false);
    $('#MainContent_CheckBoxList3_5').prop('checked', false);
}
function CheckCharacter() {
    if ($('#MainContent_RadioButtonList2_1').prop('checked') == true) {
        //クッキーを確認し、アイテムが無ければチェックを強引にオフにするｗ
        if (!Cookies.get('FkgName')) {
            $('#MainContent_RadioButtonList2_0').prop("checked", true);
            $("#MainContent_advertencia2").text("登録されてないのでチェック出来ません");
            return;
        }
    } else if ($('#MainContent_RadioButtonList2_0').prop('checked') == true) {
        $("#MainContent_advertencia2").text("");
        return;
    }
}
function ActivateTextBox(number) {
    var dropdownlist;
    var objectTextbox;
    switch (number) {
        case 1:
            {
                dropdownlist = "#MainContent_DropDownList1";
                objectTextbox = "#MainContent_TextBox101";
                break;
            }
        case 2:
            {
                dropdownlist = "#MainContent_DropDownList2";
                objectTextbox = "#MainContent_TextBox102";
                break;
            }
        case 3:
            {
                dropdownlist = "#MainContent_DropDownList3";
                objectTextbox = "#MainContent_TextBox103";
                break;
            }
    }
    //ドロップダウンリストの値取得
    var selectVal = $(dropdownlist).val();
    switch (selectVal) {
        case "選択無し": {
            $(objectTextbox).prop("disabled", true);
            break;
        }
        case "クリティカル率上昇":
        case "クリティカルダメージ増加":
        case "クリティカル率上昇（PT全体対象）":
        case "クリティカルダメージ増加（PT全体対象）":
        case "再行動":
        case "攻撃力低下":
        case "スキル発動率低下":
            {
            $(objectTextbox).prop("disabled", false);
            break;
        }
        default: {
            $(objectTextbox).prop("disabled", true);
            break;
        }
    }


}


//
//ランキング専用関数
//
//指定した文字列以上見えなくする
function HideText3() {
    if (window.innerWidth <= 768) {

        var count = 2;//ヘッダー部を何文字目まで表示するか決定

        //対象となる要素の選択
        //テーブル取得
        var tblTbody = document.getElementById('ctl00_MainContent_ListView1_itemPlaceholderContainer');
        if (!tblTbody) {
            //オブジェクト非存在
            return;
        }
        //テーブル内の行をループ
        for (var i = 0, rowLen = tblTbody.rows.length; i < rowLen; i++) {
            //タイトル行の文字列修正
            if (i == 0) {
                for (var j = 0; j < 10; j++) {
                    var thisText = tblTbody.rows[i].cells[j].textContent.trim();
                    var textLength = thisText.length;
                    if (textLength > count) {
                        var showText = thisText.substring(0, count);
                        var insertText = showText;
                        tblTbody.rows[i].cells[j].textContent = insertText;
                        tblTbody.rows[i].cells[j].style.textAlign = "center";
                    }
                }
            } else {
                //データレコードの修正
                //所属国家の短縮表示
                thisText = tblTbody.rows[i].cells[9].textContent.trim();
                textLength = thisText.length;
                if (textLength > count) {
                    showText = thisText.substring(0, count);
                    insertText = showText;
                    insertText += '...';
                    tblTbody.rows[i].cells[9].textContent = insertText;
                }
                //花騎士名短縮処理
                //名前→４文字まで
                //（）は修正しない
                //昇華→昇とする

                //処理の有無をクッキーに確認
                if (Cookies.get("nameShort") == 1) {
                    var fkgName = tblTbody.rows[i].cells[1].textContent.trim();
                    var nameLength = fkgName.length;
                    //if ((nameLength > 4) && (fkgName.indexOf('...') == -1)) {
                    //    //花騎士名場合分け//ひたすら後ろから取っていく。

                    //    var nameStr = "";
                    //    var versionStr = "";
                    //    var shoukaStr = "";
                    //    var tmpStr = fkgName;

                    //    //昇華部分取出し
                    //    if (tmpStr.indexOf("　昇華") != -1) {
                    //        tmpStr = tmpStr.substring(0, tmpStr.indexOf("　昇華"));
                    //        shoukaStr = " 昇";
                    //    }
                    //    //バージョン部分取出し
                    //    if (tmpStr.indexOf("（") != -1) {
                    //        versionStr = tmpStr.substring(tmpStr.indexOf("（"));
                    //        tmpStr = tmpStr.substring(0, tmpStr.indexOf("（"));

                    //        //バージョン短縮処理
                    //        if (versionStr.indexOf("ジューンブライド") != -1) {
                    //            versionStr = versionStr.replace("ジューンブライド", "JB");
                    //        }
                    //        if (versionStr.indexOf("光華の姫君") != -1) {
                    //            versionStr = versionStr.replace("光華の姫君", "光華");
                    //        }
                    //        if (versionStr.indexOf("クリスマス") != -1) {
                    //            versionStr = versionStr.replace("クリスマス", "クリ");
                    //        }
                    //        if (versionStr.indexOf("ハロウィン") != -1) {
                    //            versionStr = versionStr.replace("ハロウィン", "ハロ");
                    //        }
                    //        if (versionStr.indexOf("フォスの花嫁") != -1) {
                    //            versionStr = versionStr.replace("フォスの花嫁", "フォス");
                    //        }
                    //        if (versionStr.indexOf("天つ花の令嬢") != -1) {
                    //            versionStr = versionStr.replace("天つ花の令嬢", "天つ花");
                    //        }

                    //        //括弧を全角→半角に変換
                    //        versionStr = versionStr.replace("（", "(");
                    //        versionStr = versionStr.replace("）", ")");
                    //    }

                    //    //名前短縮
                    //    if (tmpStr.length > 4) {
                    //        nameStr = tmpStr.substring(0, 4) + "...";
                    //    }
                    //    else {
                    //        nameStr = tmpStr;
                    //    }

                    //    showText = nameStr + versionStr + shoukaStr;

                    //    tblTbody.rows[i].cells[1].textContent = showText;

                    //}
                    showText = ShortText(fkgName, 4);
                    tblTbody.rows[i].cells[1].textContent = showText;

                }

            }
        }
    }
}

function ChangeText() {
    var tblTbody = document.getElementById('ctl00_MainContent_ListView1_itemPlaceholderContainer');
    if (!tblTbody) {
        //オブジェクト非存在
        return;
    }

    var selection = $("input[name='ctl00$MainContent$RadioButtonList4']:checked").val();
    if ((selection == "ID") || (selection == "登録日")) {
        //文字変更
        tblTbody.rows[0].cells[0].textContent = "ID";
        tblTbody.rows[0].cells[0].style.textAlign = "center";

        tblTbody.rows[0].cells[9].textContent = "DB登録日";
        tblTbody.rows[0].cells[9].style.textAlign = "center";
    }

}

    

//ラジオボタンの表示を変える
//引数は
//textId 対象のID
//count 何文字まで残すか
function ShortText(originalText, count) {
    //
    if (originalText.indexOf('#') > -1) {
        originalText = originalText.splice(1);
    }
    var originalLength = originalText.length;
    //名前が4文字の場合はそのまま返す
    if ((originalLength <= count) || (originalText.indexOf('...') != -1)) {
        return originalText;
    }

    //名前が4文字で、処理済みの場合は、...が入らない。しかし処理済みなので
    if ((originalText.indexOf(')') != -1)||(originalText.indexOf(' 昇') != -1)){
        return originalText;
    }

    if ((originalLength > count) && (originalText.indexOf('...') == -1)) {
        //花騎士名場合分け//ひたすら後ろから取っていく。

        var nameStr = "";
        var versionStr = "";
        var shoukaStr = "";
        var tmpStr = originalText;

        //昇華部分取出し
        if (tmpStr.indexOf("　昇華") != -1) {
            tmpStr = tmpStr.substring(0, tmpStr.indexOf("　昇華"));
            shoukaStr = " 昇";
        }
        //バージョン部分取出し
        if (tmpStr.indexOf("（") != -1) {
            versionStr = tmpStr.substring(tmpStr.indexOf("（"));
            tmpStr = tmpStr.substring(0, tmpStr.indexOf("（"));

            //バージョン短縮処理
            if (versionStr.indexOf("ジューンブライド") != -1) {
                versionStr = versionStr.replace("ジューンブライド", "JB");
            }
            if (versionStr.indexOf("光華の姫君") != -1) {
                versionStr = versionStr.replace("光華の姫君", "光華");
            }
            if (versionStr.indexOf("クリスマス") != -1) {
                versionStr = versionStr.replace("クリスマス", "クリ");
            }
            if (versionStr.indexOf("ハロウィン") != -1) {
                versionStr = versionStr.replace("ハロウィン", "ハロ");
            }
            if (versionStr.indexOf("フォスの花嫁") != -1) {
                versionStr = versionStr.replace("フォスの花嫁", "フォス");
            }
            if (versionStr.indexOf("天つ花の令嬢") != -1) {
                versionStr = versionStr.replace("天つ花の令嬢", "天つ花");
            }
            if (versionStr.indexOf("世界花の巫女") != -1) {
                versionStr = versionStr.replace("世界花の巫女", "巫女");
            }
            if (versionStr.indexOf("きぐるみのんびり貴族") != -1) {
                versionStr = versionStr.replace("きぐるみのんびり貴族", "きぐるみ");
            }
            if (versionStr.indexOf("きぐるみ教師見習い") != -1) {
                versionStr = versionStr.replace("きぐるみ教師見習い", "きぐるみ");
            }
            if (versionStr.indexOf("きぐるみ花火師") != -1) {
                versionStr = versionStr.replace("きぐるみ花火師", "きぐるみ");
            }
            if (versionStr.indexOf("バレンタイン") != -1) {
                versionStr = versionStr.replace("バレンタイン", "バレ ");
            }
            if (versionStr.indexOf("ベイサボール") != -1) {
                versionStr = versionStr.replace("ベイサボール", "ベ球");
            }
            if (versionStr.indexOf("精華の王女") != -1) {
                versionStr = versionStr.replace("精華の王女", "精女");
            }
            if (versionStr.indexOf("憧れのくつろぎ女王") != -1) {
                versionStr = versionStr.replace("憧れのくつろぎ女王", "憧れ");
            }
            if (versionStr.indexOf("憧れの煌めく新星") != -1) {
                versionStr = versionStr.replace("憧れの煌めく新星", "憧れ");
            }
            if (versionStr.indexOf("憧れの青春想う教育係") != -1) {
                versionStr = versionStr.replace("憧れの青春想う教育係", "憧れ");
            }

            //括弧を全角→半角に変換
            versionStr = versionStr.replace("（", "(");
            versionStr = versionStr.replace("）", ")");
        }

        //名前短縮
        if (tmpStr.length > count) {
            nameStr = tmpStr.substring(0, count) + "...";
        }
        else {
            nameStr = tmpStr;
        }

        return nameStr + versionStr + shoukaStr;

            

    }
}

////////////////////////////////////////
//
//花騎士登録用
//
///////////////////////////////////////
function checkCookie(dropdownlistId){
    //クッキーの有無確認
    //あれば情報引き出し
    if (!Cookies.get('FkgName')) {
        return;
    }
    //クッキー有
    var cookieString = Cookies.get('FkgName');
    var cookieData = cookieString.slice(1).split('#');
    var loopMax = cookieData.length;
    for (var i = loopMax -1; i > -1; i--) {
        //奇数の場合はスキルレベルなので、その項目を削除
        if (i % 2 != 0) {
            cookieData.splice(i, 1);
        }
    }

    //ドロップダウンリスト取得
    var dropdown = document.getElementById(dropdownlistId);
    var selectedFkg = dropdown.options[dropdown.selectedIndex].value;

    //クッキーデータと一致するか確認
    for (var i = 0; i < cookieData.length; i++) {
        if (selectedFkg == cookieData[i]) {
            var returnString = "既に登録されています";
            return returnString;
        }
    }
    //一致しない場合は、画面に表示しない
    return "";
}

//登録数取得
function registedNum() {
    //クッキーの有無確認
    //あれば情報引き出し
    if (!Cookies.get('FkgName')) {
        var nulo = [0, 0];
        return nulo;
    }
    //クッキー有
    var cookieString = Cookies.get('FkgName');
    var cookieData = cookieString.slice(1).split('#');
    var loopMax = cookieData.length;
    var countShouka = 0;
    for (var i = loopMax - 1; i > -1; i--) {
        //奇数の場合はスキルレベルなので、その項目を削除
        if (i % 2 != 0) {
            cookieData.splice(i, 1);
        }
        else {
            if (cookieData[i] > 10000)
                countShouka++;
        }
    }

    var respuesta = [ cookieData.length, countShouka ];
    return respuesta;
}

//編集用
//スキルレベル変更処理
//クッキーに登録する
function changeSlv() {
    //dropdown1は花騎士名
    var dropdown1 = document.getElementById("MainContent_DropDownList2011");
    var selectedValue = dropdown1.options[dropdown1.selectedIndex].value;

    if (selectedValue == 0) {//初期値の場合は処理しない
        return;
    }

    //Valueは、スキルLV＋"#"＋花騎士idになっているので花騎士IDを引っ張り出す必要がある
    var slv = selectedValue.substr(0,1);
    var fkgId = selectedValue.slice(2);

    //dropdown2はユーザーが選択したスキルレベル
    var dropdown2 = document.getElementById("MainContent_DropDownList2012");
    var selectedSlv = dropdown2.options[dropdown2.selectedIndex].value;

    //登録されているスキルレベルと、変更しようとしているスキルレベルが同じなら
    //エラーメッセージ出して処理終了
    //これやろうと思ったけど、一度変更したスキルレベル情報を書き換えられそうもない
    //if (slv == selectedSlv) {
    //    //
    //    return;
    //}

    ////dropdown1を修正する
    //dropdown1.options[dropdown1.selectedIndex].value(selectedSlv + '#' + fkgId);


    //クッキーから登録データを呼び出す
    //クッキーの有無確認
    //あれば情報引き出し
    if (!Cookies.get('FkgName')) {
        return;//間違ってクッキー消去ボタン押さない限りここには来ない
    }
    //クッキーデータ取得
    var cookieString = Cookies.get('FkgName');
    var cookieData = cookieString.slice(1).split('#');
    var loopMax = cookieData.length;
    var Name = [loopMax / 2];
    var Slv = [loopMax / 2];
    var j = 0;
    for (var i = 0; i < loopMax; i++)
    {
        
        if (i % 2 == 0) {
            //偶数
            Name[j] = cookieData[i];
        }
        else {//奇数
            Slv[j] = cookieData[i];
            j++;

        }
    }

    //クッキーデータ内から、修正データ捜索し修正する
    //さらにクッキー登録用データ生成する
    var writeString = "";
    for (i = 0; i < Name.length; i++) {
        if (Name[i] == fkgId) {
            Slv[i] = selectedSlv;
        }
        writeString += '#' + Name[i] + '#' + Slv[i];
    }

    //クッキー修正登録
    Cookies.set('FkgName', writeString, { expires: 180 });

}

function DeleteFkg() {
    //dropdown1は花騎士名
    var dropdown1 = document.getElementById("MainContent_DropDownList2011");
    var selectedValue = dropdown1.options[dropdown1.selectedIndex].value;

    if (selectedValue == 0) {//初期値の場合は処理しない
        return;
    }

    //Valueは、スキルLV＋"#"＋花騎士idになっているので花騎士IDを引っ張り出す必要がある
    var fkgId = selectedValue.slice(2);
    //クッキーから登録データを呼び出す
    //クッキーの有無確認
    //あれば情報引き出し
    if (!Cookies.get('FkgName')) {
        return;//間違ってクッキー消去ボタン押さない限りここには来ない
    }
    //クッキーデータ取得
    var cookieString = Cookies.get('FkgName');
    var cookieData = cookieString.slice(1).split('#');
    var loopMax = cookieData.length;
    var Name = [loopMax / 2];
    var Slv = [loopMax / 2];
    var j = 0;
    for (var i = 0; i < loopMax; i++) {

        if (i % 2 == 0) {
            //偶数
            Name[j] = cookieData[i];
        }
        else {//奇数
            Slv[j] = cookieData[i];
            j++;

        }
    }

    //クッキーデータ内から、修正データ捜索し修正する
    //さらにクッキー登録用データ生成する
    var writeString = "";
    for (i = Name.length - 1; i > -1; i--) {
        if (Name[i] == fkgId) {
            //削除する
            Name.splice(i, 1);
            Slv.splice(i, 1);
            continue;
        }
        writeString += '#' + Name[i] + '#' + Slv[i];
    }

    //書き込む文字が存在しない場合はつまりクッキー消去
    if (writeString == "") {
        Cookies.remove('FkgName');
    }

    //クッキー修正登録
    else {
        Cookies.set('FkgName', writeString, { expires: 180 });
    }
    //ドロップダウンリストからも削除
    dropdown1.remove(dropdown1.selectedIndex);

    //画面表示関係の処理
    //クッキー内容確認して、登録数算出する処理
    var number = registedNum();
    document.getElementById("MainContent_Label2102").textContent = number[0] + "人";
    document.getElementById("MainContent_Label2103").textContent = number[1] + "人";

    //クッキー内容確認して、登録用選択肢の状態を表示する
    var obj = document.getElementById("MainContent_Label2001");
    obj.textContent = checkCookie("MainContent_DropDownList2001");

    obj = document.getElementById("MainContent_Label2002");
    obj.textContent = checkCookie("MainContent_DropDownList2002");

    obj = document.getElementById("MainContent_Label2003");
    obj.textContent = checkCookie("MainContent_DropDownList2003");

    obj = document.getElementById("MainContent_Label2004");
    obj.textContent = checkCookie("MainContent_DropDownList2004");

    obj = document.getElementById("MainContent_Label2005");
    obj.textContent = checkCookie("MainContent_DropDownList2005");

    //grid生成用フラグを消去
    document.getElementById("MainContent_HiddenValue1").value = "";
}

function Iniciar_FKG_register() {
    //クッキー内容確認して、登録数算出する処理
    var number = registedNum();
    document.getElementById("MainContent_Label2102").textContent = number[0] + "人";
    document.getElementById("MainContent_Label2103").textContent = number[1] + "人";

    //第1ドロップダウンリストのみ登録されているか確認//HiddenValue1
    var obj = document.getElementById("MainContent_Label2001");
    obj.textContent = checkCookie("MainContent_DropDownList2001");

    //今使って無いhidden処理
    //document.getElementById("MainContent_HiddenValue1").value = checkCookie("MainContent_DropDownList2001");

    //編集の処理
    //選択した花騎士名に基づいて、スキルレベルを変更する処理
    selectedValue1 = $("#MainContent_DropDownList2011").val();
    if (selectedValue1 != null) {
        selectedValue1 = GetSlv(selectedValue1.slice(2));
        $("#MainContent_DropDownList2012").val(selectedValue1);
    }

    //以下の処理は一度しか行いたくないため、下記のhidden処理を入れる
    if (document.getElementById("MainContent_HiddenValue1").value != "listo") {
        MakeGrid();
        document.getElementById("MainContent_HiddenValue1").value = "listo";
    }
    return;
}

//関数化　クッキーデータを読み込んで、内部の登録データの配列を返す
// 無い場合は0を返す
function GetRegisteredData() {
    if (!Cookies.get('FkgName')) {
        return 0;//見つからない場合は0
    }
    //クッキーデータ取得
    var cookieString = Cookies.get('FkgName');
    var cookieData = cookieString.slice(1).split('#');
    var loopMax = cookieData.length;
    var returnData = new Array(loopMax / 2);
    //var Name = [loopMax / 2];
    //var Slv = [loopMax / 2];
    //var j = 0;
    for (var i = 0; i < loopMax; i++) {

        if (i % 2 == 0) {
            //偶数
            returnData[i/2] = new Array(2);
            returnData[i/2][0] = cookieData[i];
            //Name[j] = cookieData[i];
        }
        else {//奇数
            returnData[Math.floor(i/2)][1] = cookieData[i];
            //Slv[j] = cookieData[i];
            //j++;

        }

        //test
        //if (i % 2 != 0) {
        //    //奇数
        //    returnData[Math.floor(i / 2)] = [cookieData[i - 1], cookieData[i]];
        //}

    }
    
    return returnData;
}



//grid作成
function MakeGrid()
{
    //クッキーからデータ取得
    var fkgData = GetRegisteredData();
    if (fkgData == 0) {
        return;
    }
    //ドロップダウンリスト1からデータ取得
    var compareData = GetFkgName();
    for (var i = 0; i < fkgData.length; i++) {
        for (var j = 0; j < compareData.length; j++) {
            if (fkgData[i][0] == compareData[j].value) {
                fkgData[i][0] = compareData[j].text;
                continue;
            }
        }
    }

    var data = 
        fkgData
    ;

    


    var grid = document.getElementById('grid');

    var hot1 = new Handsontable(grid, {
        data: data,
        rowHeaders: true,
        colHeaders: ['花騎士名', 'スキルレベル'],
        columnSorting: true,
        className: "htCenter",
        licenseKey: 'non-commercial-and-evaluation'
    });

    //データ出力用
    var button1 = document.getElementById('export-file');
    var exportPlugin1 = hot1.getPlugin('exportFile');

    button1.addEventListener('click', function () {
        exportPlugin1.downloadFile('csv', {
            bom: true,
            columnDelimiter: ',',
            columnHeaders: false,
            exportHiddenColumns: true,
            exportHiddenRows: true,
            fileExtension: 'csv',
            filename: 'Registered_FKG_[YYYY]-[MM]-[DD]',
            mimeType: 'text/csv',
            rowDelimiter: '\r\n',
            rowHeaders: true
        });
    });
}

//準備関数
function GetFkgName() {
    var data = $('#MainContent_DropDownList2001').children();
    var returnData = data.get();
    return returnData;
}

///////////////////////////////
//
//おまけ用
//
///////////////////////////////

function ShoukasekiCalc() {

    //チェックボタン状況の確認
    var rarity = $("input[name='ctl00$MainContent$RadioButtonList1']:checked").val();
    var item = $("input[name='ctl00$MainContent$RadioButtonList2']:checked").val();
    var sLevel = $("input[name='ctl00$MainContent$RadioButtonList3']:checked").val();

    //計算
    var outValue;
    var itemRatio;
    var sRatio;

    switch (rarity) {
        case "2":
            {
                outValue = 750;
                itemRatio = 1;
                sRatio = 1;
                break;
            }
        case "3":
            {
                outValue = 740;
                itemRatio = 3;
                sRatio = 3;
                break;
            }
        case "4":
            {
                outValue = 730;
                itemRatio = 9;
                sRatio = 9;
                break;
            }
        case "5"://イベ金
            {
                outValue = 720;
                itemRatio = 15;
                sRatio = 15;
                break;
            }
        case "6"://ガチャ金
            {
                outValue = 680;
                itemRatio = 40;
                sRatio = 15;
                break;
            }
    }

    outValue = outValue - (item - 1) * itemRatio - (sLevel - 1) * sRatio;
    var gachaNum = outValue / 2;
    var gachaNum1 = Math.floor(gachaNum / 11);
    var gachaNum2 = Math.floor(gachaNum % 11);

    if ((outValue - (gachaNum1 * 22 + gachaNum2 * 2)) == 1) {
        //画面表示
        if (gachaNum2 + 1 == 11) {
            gachaNum1++;
            gachaNum2 = 0;
        }
        document.getElementById("MainContent_Label5").innerText = outValue + "個";
        document.getElementById("MainContent_Label6").innerText = gachaNum1 + "回分";
        document.getElementById("MainContent_Label7").innerText = gachaNum2 + "回分";
    }
    else {
        //画面表示
        document.getElementById("MainContent_Label5").innerText = outValue + "個";
        document.getElementById("MainContent_Label6").innerText = gachaNum1 + "回分";
        document.getElementById("MainContent_Label7").innerText = gachaNum2 + "回分";
    }

    //現在の昇華石所持数に入力がある場合の処理
    if ($('#MainContent_TextBox10').val() >= 0) {
        ShoukasekiCalc2(outValue);
    }

}

//現在の昇華石所持数に応じて計算
function ShoukasekiCalc2(outValue1) {
    var inputText = $('#MainContent_TextBox10').val();
    if (isNaN(inputText)) {
        $('#MainContent_TextBox10').val("想定外だよ");
    }
    inputText = Number(inputText);
    
    if (inputText >= 0) {
        var necesitoValue = outValue1 - inputText;
        if (necesitoValue < 0) {
            //$('#MainContent_TextBox10').val("充分ある");
            document.getElementById("MainContent_Label8").innerText = "石おっぱい";
            document.getElementById("MainContent_Label9").innerText = "ぱふ";
            document.getElementById("MainContent_Label10").innerText ="ぱふ";
        }
        else {
            if (necesitoValue % 2 == 1) {
                necesitoValue++;
            }
            var gachaNum = Math.floor(necesitoValue / 2);
            var gachaNum1 = Math.floor(gachaNum / 11);
            var gachaNum2 = Math.floor(gachaNum % 11);
            if ((necesitoValue - (gachaNum1 * 22 + gachaNum2 * 2)) == 1) {
                if (gachaNum2 + 1 == 11) {
                    gachaNum1++;
                    gachaNum2 = 0;
                }
                document.getElementById("MainContent_Label8").innerText = necesitoValue + "個";
                document.getElementById("MainContent_Label9").innerText = gachaNum1 + "回分";
                document.getElementById("MainContent_Label10").innerText = gachaNum2 + "回分";
            }
            else {
                document.getElementById("MainContent_Label8").innerText = necesitoValue + "個";
                document.getElementById("MainContent_Label9").innerText = gachaNum1 + "回分";
                document.getElementById("MainContent_Label10").innerText = gachaNum2 + "回分";
            }
            }
    }
}

//モコウ神判定
function Mokou() {
    //ドロップダウンリストから値を取得
    var odds1 = $("#MainContent_DropDownList1").val();
    var odds2 = $("#MainContent_DropDownList2").val();
    var odds3 = $("#MainContent_DropDownList3").val();
    var odds4 = $("#MainContent_DropDownList4").val();
    var odds5 = $("#MainContent_DropDownList5").val();
    var mokou1 = $("#MainContent_DropDownList6").val();
    var mokou2 = $("#MainContent_DropDownList7").val();
    var mokou3 = $("#MainContent_DropDownList8").val();
    var mokou4 = $("#MainContent_DropDownList9").val();
    var mokou5 = $("#MainContent_DropDownList10").val();

    //var yosou1 = $("#MainContent_DropDownList11").val();
    //var yosou2 = $("#MainContent_DropDownList12").val();
    //var yosou3 = $("#MainContent_DropDownList13").val();
    //var yosou4 = $("#MainContent_DropDownList14").val();
    //var yosou5 = $("#MainContent_DropDownList15").val();

    //真の順位
    //var real = [ 0, 0, 0, 0, 0, 0 ];
    var real1 = 0;
    var real2 = 0;
    var real3 = 0;
    var real4 = 0;
    var real5 = 0;


    //モコウマーク無しの場合は判定しない
    //if (mokou1 == "") {
    //    document.getElementById("MainContent_Label11").innerText = odds1;
    //    switch (odds1 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}
    //if (mokou2 == "") {
    //    document.getElementById("MainContent_Label12").innerText = odds2;
    //    switch (odds2 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}
    //if (mokou3 == "") {
    //    document.getElementById("MainContent_Label13").innerText = odds3;
    //    switch (odds3 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}
    //if (mokou4 == "") {
    //    document.getElementById("MainContent_Label14").innerText = odds4;
    //    switch (odds4 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}
    //if (mokou5 == "") {
    //    document.getElementById("MainContent_Label15").innerText = odds5;
    //    switch (odds5 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}

    //モコウマークがある場合の判定処理
    var out1 = MokouHantei(odds1, mokou1);
    if (out1 != -1) {
        switch (out1 * 1) {
            case 1:
                real1 += 1;
                break;
            case 2:
                real2 += 1;
                break;
            case 3:
                real3 += 1;
                break;
            case 4:
                real4 += 1;
                break;
            case 5:
                real5 += 1;
                break;
        }
    }
    var out2 = MokouHantei(odds2, mokou2);
    if (out2 != -1) {
        switch (out2 * 1) {
            case 1:
                real1 += 1;
                break;
            case 2:
                real2 += 1;
                break;
            case 3:
                real3 += 1;
                break;
            case 4:
                real4 += 1;
                break;
            case 5:
                real5 += 1;
                break;
        }
    }
    var out3 = MokouHantei(odds3, mokou3);
    if (out3 != -1) {
        switch (out3 * 1) {
            case 1:
                real1 += 1;
                break;
            case 2:
                real2 += 1;
                break;
            case 3:
                real3 += 1;
                break;
            case 4:
                real4 += 1;
                break;
            case 5:
                real5 += 1;
                break;
        }
    }
    var out4 = MokouHantei(odds4, mokou4);
    if (out4 != -1) {
        switch (out4 * 1) {
            case 1:
                real1 += 1;
                break;
            case 2:
                real2 += 1;
                break;
            case 3:
                real3 += 1;
                break;
            case 4:
                real4 += 1;
                break;
            case 5:
                real5 += 1;
                break;
        }
    }
    var out5 = MokouHantei(odds5, mokou5);
    if (out5 != -1) {
        switch (out5 * 1) {
            case 1:
                real1 += 1;
                break;
            case 2:
                real2 += 1;
                break;
            case 3:
                real3 += 1;
                break;
            case 4:
                real4 += 1;
                break;
            case 5:
                real5 += 1;
                break;
        }
    }

    //読者予想
    //if (yosou1 != "") {
    //    switch (yosou1 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}

    //if (yosou2 != "") {
    //    switch (yosou2 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}

    //if (yosou3 != "") {
    //    switch (yosou3 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}

    //if (yosou4 != "") {
    //    switch (yosou4 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}

    //if (yosou5 != "") {
    //    switch (yosou5 * 1) {
    //        case 1:
    //            real1 += 1;
    //            break;
    //        case 2:
    //            real2 += 1;
    //            break;
    //        case 3:
    //            real3 += 1;
    //            break;
    //        case 4:
    //            real4 += 1;
    //            break;
    //        case 5:
    //            real5 += 1;
    //            break;
    //    }
    //}
    var real = [0, real1, real2, real3, real4, real5];

    var output1 = MokouHantei2(out1, real);
    var output2 = MokouHantei2(out2, real);
    var output3 = MokouHantei2(out3, real);
    var output4 = MokouHantei2(out4, real);
    var output5 = MokouHantei2(out5, real);

    //何度か回すｗ
    for (var i = 0; i < 5; i++) {
        if (output1 == "?" || output1 == "1 ?") {
            output1 = MokouHantei2(out1, real);
        }
        if (output2 == "?" || output2 == "1 ?") {
            output2 = MokouHantei2(out2, real);
        }
        if (output3 == "?" || output3 == "1 ?") {
            output3 = MokouHantei2(out3, real);
        }
        if (output4 == "?" || output4 == "1 ?") {
            output4 = MokouHantei2(out4, real);
        }
        if (output5 == "?" || output5 == "1 ?") {
            output5 = MokouHantei2(out5, real);
        }
    }

    document.getElementById("MainContent_Label11").innerText = output1;
    document.getElementById("MainContent_Label12").innerText = output2;
    document.getElementById("MainContent_Label13").innerText = output3;
    document.getElementById("MainContent_Label14").innerText = output4;
    document.getElementById("MainContent_Label15").innerText = output5;

}

function MokouHantei(odds, mokou) {
    switch (mokou) {
        case "◎":
            {
                switch (odds * 1){
                    case 1:
                        return "4or5";
                    case 2:
                        return 5;
                    case 3:
                        return -1;
                    case 4:
                        return 1;
                    case 5:
                        return "1or2";
                }
                break;
            }

        case "〇":
            {
                switch (odds * 1) {
                    case 1:
                    case 2:
                        return odds * 1 + 2;
                    case 3:
                        return "1or5";
                    case 4:
                    case 5:
                        return odds * 1 - 2;

                }
                break;
            }

        case "△":
            {
                switch (odds * 1) {
                    case 1:
                        return 2;
                    case 2:
                        return "1or3";
                    case 3:
                        return "2or4";
                    case 4:
                        return "3or5";
                    case 5:
                        return 4;

                }
                break;
            }
        case "":
            {
                return odds;
            }
    }
}

function MokouHantei2(out, real) {
    switch (out) {
        case 1:
        case "1":
            {
                if (real[1] * 1 == 1) {
                    return 1;
                }
                else
                    return "?";
            }
        case 2:
        case "2":
            {
                if (real[2] * 1 == 1) {
                    return 2;
                }
                else
                    return "?";
            }
        case 3:
        case "3":
            {
                if (real[3] * 1 == 1) {
                    return 3;
                }
                else
                    return "?";
            }
        case 4:
        case "4":
            {
                if (real[4] * 1 == 1) {
                    return 4;
                }
                else
                    return "?";
            }
        case 5:
        case "5":
            {
                if (real[5] * 1 == 1) {
                    return 5;
                }
                else
                    return "?";
            }
        case "1or5":
            {

                if (real[1] * 1 == 1 && real[5] * 1 != 1) {
                    real[5] += 1;
                    return 5;
                }
                else if (real[1] * 1 != 1 && real[5] * 1 == 1) {
                    real[1] += 1;
                    return 1;
                }
                else {
                    return "1 ?";
                }
            }
        case "1or3":
            {
                if (real[1] * 1 == 1 && real[3] * 1 != 1) {
                    real[3] += 1;
                    return 3;
                }
                else if (real[1] * 1 != 1 && real[3] * 1 == 1) {
                    real[1] += 1;
                    return 1;
                }
                else {
                    return "1 ?";
                }
            }
        case "2or4":
            {
                if (real[2] * 1 == 1 && real[4] * 1 != 1) {
                    real[4] += 1;
                    return 4;
                }
                else if (real[2] * 1 != 1 && real[4] * 1 == 1) {
                    real[2] += 1;
                    return 2;
                }
                else {
                    return "?";
                }
            }
        case "3or5":
            {
                if (real[3] * 1 == 1 && real[5] * 1 != 1) {
                    real[5] += 1;
                    return 5;
                }
                else if (real[3] * 1 != 1 && real[5] * 1 == 1) {
                    real[3] += 1;
                    return 3;
                }
                else {
                    return "?";
                }
            }
        case "4or5":
            {
                if (real[4] * 1 == 1 && real[5] * 1 != 1) {
                    real[5] += 1;
                    return 5;
                }
                else if (real[4] * 1 != 1 && real[5] * 1 == 1) {
                    real[4] += 1;
                    return 4;
                }
                else {
                    return "?";
                }
            }
        case "1or2":
            if (real[1] * 1 == 1 && real[2] * 1 != 1) {
                real[2] += 1;
                return 2;
            }
            else if (real[1] * 1 != 1 && real[2] * 1 == 1) {
                real[1] += 1;
                return 1;
            }
            else {
                return "1 ?";
            }

        case "-1":
            {
                return out;
            }
        default:
            return "?";

    }
}

///////////////////////////////
//
//闇のおまけ用
//
///////////////////////////////

function DmmPtCalc() {
    //データ取得
    var type100pt = $("input[name='ctl00$MainContent$RadioButtonList3001']:checked").val();
    var typeWeekly = $("input[name='ctl00$MainContent$RadioButtonList3002']:checked").val();
    var typeSpe = $("input[name='ctl00$MainContent$RadioButtonList3003']:checked").val();
    var typeSouka = $("input[name='ctl00$MainContent$RadioButtonList3004']:checked").val();
    var plazoM = $("input[name='ctl00$MainContent$RadioButtonList3005']:checked").val();

    //計算
    //日数
    var plazoD = plazoM * 7;
    switch (typeWeekly) {
        case '0': {
            var numNiji = 0;
            break;
        }
        case '1': {
            numNiji = plazoM;
            break;
        }
        case '2': {
            numNiji = Math.ceil(plazoM / 2);
            break;
        }
        case '3': {
            numNiji = Math.ceil(plazoM / 4);
            break;
        }
    }
    switch (typeSpe) {
        case '0': {
            var numSpe = 0;
            break;
        }
        case '1': {
            numSpe = Math.ceil(plazoM / 9);
            break;
        }
        case '2': {
            numSpe = Math.ceil(plazoM / 18);
            break;
        }
    }
    //次で必要になるのでここで書き換え
    switch (plazoM) {
        case '52': {
            plazoM = 48;
            break;
        }
        case '26': {
            plazoM = 24;
            break;
        }
        case '13': {
            plazoM = 12;
            break;
        }
    }
    switch (typeSouka) {
        case '0': {
            var numSouka = 0;
            break;
        }
        case '1': {
            numSouka = Math.ceil(plazoM / 4);
            break;
        }
        case '2': {
            numSouka = Math.ceil(plazoM / 8);
            break;
        }
    }

    var dmmPt = plazoD * type100pt * 100 + numNiji * 480 + numSpe * 5000 + numSouka * 3000;
    var niji = numNiji * 20;
    var dancho = plazoD * type100pt;
    var shoukaProbabilidad = Math.floor(dancho * 3.472);


    //表示
    document.getElementById("MainContent_Label3001").innerText = dmmPt + "ポイント";
    document.getElementById("MainContent_Label3002").innerText = niji + "個";
    document.getElementById("MainContent_Label3003").innerText = dancho + "個";
    document.getElementById("MainContent_Label3004").innerText = dancho + "回";
    document.getElementById("MainContent_Label3005").innerText = "約 " + shoukaProbabilidad + "個";
}

///////////////////////////////
//
//花騎士登録用
//
///////////////////////////////

