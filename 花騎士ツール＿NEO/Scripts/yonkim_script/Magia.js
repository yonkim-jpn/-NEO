//まずは精神強化分を抜いた状態で作成し、後で精神強化分を追加
//キャラステで、覚醒素材のセット具合を入力できるようにする


//各ディスクの値取得
//特にエラー処理は不要

//ATKとDEFより、基礎ダメージ算出
//ATKに値が入力されていない時のみエラー

function CalcDano() {
    //値取得
    var Atk = Number(Zenhan($('#MainContent_TextBox666').val())) || 0;
    var mAtk = Number(Zenhan($('#MainContent_TextBox667').val())) || 0;
    var Def = Number(Zenhan($('#MainContent_TextBox668').val())) || 0;
    var mDef = Number(Zenhan($('#MainContent_TextBox669').val())) || 0;

    //覚醒補正
    var tablaDespierta1 = ConsigoTablaDespierta(1);
    var tablaDespierta2 = ConsigoTablaDespierta(2);
    var atkDespierto = 0;
    var defDespierto = 0;
    if (color[1] === 1) {
        atkDespierto = tablaDespierta1[1];
    }
    //相手側DEFにより
    if (color2[0] === 1) {
        defDespierto = tablaDespierta2[0];
    }

    //陣形補正
    var ordenAtk = $("input[name='ctl00$MainContent$ordendeBatalla']:checked").val();
    var ordenDef = $("input[name='ctl00$MainContent$ordendeBatalla2']:checked").val();
    switch (ordenAtk) {
        case "1":
            {
                ordenAtk = 1.1;
                break;
            }
        case "2":
            {
                ordenAtk = 1.15;
                break;
            }
        case "0":
            {
                ordenAtk = 1;
                break;
            }
    }
    switch (ordenDef) {
        case "1":
            {
                ordenDef = 1.1;
                break;
            }
        case "2":
            {
                ordenDef = 1.15;
                break;
            }
        case "0":
            {
                ordenDef = 1;
                break;
            }
    }

    //攻撃力UP補正
    var AtkUp = Number(Zenhan($('#MainContent_AtkUp').val())) || 0;
    var DefUp = Number(Zenhan($('#MainContent_DefUp').val())) || 0;

    AtkUp = (AtkUp > 200) ? 200 : AtkUp;
    AtkUp = ((AtkUp < 5) && (AtkUp > -5)) ? 0 : AtkUp;
    AtkUp = (AtkUp < -100) ? -100 : AtkUp;
    DefUp = (DefUp > 200) ? 200 : DefUp;
    DefUp = ((DefUp < 5) && (DefUp > -5)) ? 0 : DefUp;
    DefUp = (DefUp < -100) ? -100 : DefUp;


    AtkUp = 1 + AtkUp / 100;
    DefUp = 1 + DefUp / 100;

    //UPDOWN補正をいったん抜いて簡易作成
    var AtkReal = (Atk * (1 + atkDespierto) + mAtk) * AtkUp * ordenAtk;
    var DefReal = (Def * (1 + defDespierto) + mDef) * DefUp * ordenDef;
    var dano = AtkReal - DefReal / 3;

    return dano;
}


//補正ダメージ計算関数
//補正係数は複雑なので、いったん考えない（2019/12/04）
function CalcAjustado(eleccion1, eleccion2, eleccion3, orden) {
    var eleccion = [eleccion1, eleccion2, eleccion3];
    var ventana = $("input[name='ctl00$MainContent$ventana']:checked").val();


    //a ディスク倍率
    var calcA = 1;
    switch (eleccion[orden - 1]) {
        case "A":
            {
                calcA = 1;
                break;
            }
        case "B":
            {
                //var baseBlast = 0.6;
                calcA = 0.6;
                //if ((ventana === "1")&&(puella !== "1")&&(conjunto === "no"))
                //    baseBlast += 0.1;
                ////何枚目のブラストか求める
                //switch (orden) {
                //    case 1:
                //        {
                //            calcA = baseBlast;
                //            break;
                //        }
                //    case 2:
                //        {
                //            calcA = baseBlast * 1.1;
                //            break;
                //        }
                //    case 3:
                //        {
                //            calcA = baseBlast * 1.2;
                //            break;
                //        }
                //}
                break;
            }
        case "C":
            {
                calcA = 0.8;
                break;
            }
    }

    //b コンボ倍率
    //まずはピュエラコンボ状態で出す
    var calcB = 1;
    //コンボ判定
    var conjunto = "no";
    if ((eleccion[0] === eleccion[1]) && (eleccion[1] === eleccion[2])) {
        conjunto = eleccion[0];
    }

    var puella = $("input[name='ctl00$MainContent$estadoAtk']:checked").val();
    //puella無しの場合
    if (puella === "0") {
        if (conjunto === "B")
            calcB = 1.5;
    } else {
        //puella有りの場合
        switch (eleccion[orden - 1]) {
            case "A":
                {
                    calcB = 1.2;
                    break;
                }
            case "B":
                {
                    //Blastコンボの場合
                    if (conjunto === "B")
                        calcB = 2;
                    else
                        calcB = 1.5;
                    break;
                }
            case "C":
                {
                    calcB = 1.5;
                    break;
                }
        }
    }




    //c チャージ倍率 ついでに獲得MPもこちらで処理
    //この攻撃の前までに溜まっているチャージ倍率に応じて上昇する値
    //溜まっているチャージ数に応じてテーブルより値を取得する必要有り。
    //今回の行動がAccelかBlastかにより、テーブルは異なる
    var tablaA = [1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8, 2.9, 3];
    var tablaB = [1.4, 1.7, 2, 2.3, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.7, 3.9, 4.1, 4.3, 4.5, 4.6, 4.7, 4.8, 4.9, 5];
    var tablaMp = [1.3, 1.6, 1.9, 2.2, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.9, 4.3, 4.7, 5.1, 5.5, 6, 6.5, 7, 7.5, 8];




    //チャージ倍率取得
    var ratioCori = Number($('#chargePlus').val()) || 0;
    //この溜まったチャージをどこで使うか確認する
    //1回目で使った場合は、二回目以降では使えない
    switch (orden) {
        case 2:
            {
                if (eleccion[0] !== "C")
                    //すでに使った
                    ratioCori = 0;
                break;
            }
        case 3:
            {
                if (eleccion[0] !== "C")
                    //すでに使った
                    ratioCori = 0;
                else if (eleccion[1] !== "C")
                    //すでに使った
                    ratioCori = 0;
                break;
            }
    }

    //現在のチャージ数取得
    var ratioC = ratioCori;
    switch (orden) {
        case 1:
            break;
        case 2:
            {
                if (eleccion[0] === "C")
                    ratioC++;
                break;
            }
        case 3:
            {
                //Cが合計何個あるか
                if (eleccion[0] === "C")
                    ratioC++;
                if (eleccion[1] === "C")
                    ratioC++;
                break;
            }
    }

    //チャージ数に応じて、テーブルの値を取得
    var calcC = 1;
    var cmp = 1;

    //チャージ数は20が限界なので、20を超える分を20にする
    if (ratioC > 20)
        ratioC = 20;

    if (ratioC !== 0) {
        //AccelとBlastのみ対応
        switch (eleccion[orden - 1]) {
            case "A":
                {
                    calcC = tablaA[ratioC - 1];
                    cmp = tablaMp[ratioC - 1];
                    //ミラーズ補正
                    if (ventana === "1")
                        calcC *= 0.9;
                    break;
                }
            case "B":
                {
                    calcC = tablaB[ratioC - 1];
                    //ミラーズ補正
                    if (ventana === "1")
                        calcC *= 0.9;
                    break;
                }
        }

    }

    //そもそもチャージ補正可能なのか判定
    //orden2の場合
    // 1個目がA,Bの場合チャージ初期化
    //orden3の場合
    // 2個目がA,Bの場合チャージ初期化
    switch (orden) {
        case 2:
            {
                if (eleccion[0] !== "C") {
                    calcC = 1;
                    cmp = 1;
                }
                break;
            }
        case 3:
            {
                if (eleccion[1] !== "C") {
                    calcC = 1;
                    cmp = 1;
                }
                break;
            }
    }


    if (calcC > 5.5)//チャージ倍率上限
        calcC = 5.5;

    /////////////////////////
    //mp獲得量計算

    //mpテーブル
    var tablaMagiaA = [[10, 10.5, 14], [13.5, 15.75, 21]];
    var tablaMagiaC = [[2, 3, 4], [3, 4.5, 6]];

    var calcMp = 0;
    var bonusA = [0, 0, 0];
    var bonusC = 0;

    //A:ボーナス決定
    switch (eleccion[0]) {
        case "A":
            {
                bonusA = [0, 3, 3];
                break;
            }
        case "C":
            {
                //この記述は何かちがうっぽい。
                //bonusC = [3, 5, 0];
                break;
            }
    }


    switch (eleccion[orden - 1]) {
        case "A":
            {
                calcMp = tablaMagiaA[ventana][orden - 1] + bonusA[orden - 1];
                break;
            }
        case "B":
            {
                calcMp += bonusA[orden - 1];
                break;
            }
        case "C":
            {
                calcMp = tablaMagiaC[ventana][orden - 1] + bonusA[orden - 1];
                break;
            }
    }

    //B:魔法少女タイプ取得
    var tipoPuella = $("input[name='ctl00$MainContent$tipoPuella']:checked").val();
    var calcMpB = 1;
    switch (tipoPuella) {
        case "マギア":
        case "サポート":
            {
                calcMpB = 1.2;
                break;
            }
        case "アタック":
        case "ヒール":
            {
                break;
            }
        case "バランス":
            {
                calcMpB = 0.9;
                break;
            }
        case "ディフェンス":
            {
                calcMpB = 0.8;
                break;
            }
    }

    //魔法少女タイプによる補正
    calcMp = Math.floor(calcMp * calcMpB * 10) / 10;

    //C:MP獲得量UP
    //メモリア側
    var MpUp = Number($("#MainContent_MpUp").val());
    var calcMpC = MpUp === 0 ? 1 : 1 + (2.5 + 2.5 * MpUp) / 100;
    //calcMp = Math.floor(calcMp * calcMpC * 10)/10;

    //D:AccelMPUP
    //メモリア側 アクセルのみかかる
    var AMpUp = eleccion[orden - 1] === "A" ? Number($("#MainContent_AMpUp").val()) : 0;
    var calcMpD = AMpUp === 0 ? 1 : 1 + (7.5 + 2.5 * AMpUp) / 100;
    //calcMp = Math.floor(calcMp * calcMpD * 100)/100;

    //チャージ数による補正
    //calcMp = Math.floor(calcMp * cmp * 100) / 100;

    //そらまめ氏の指摘により、CとDの計算順を変更し、D処理後、小数点以下2位を切り上げ
    calcMp = Math.ceil(calcMp * calcMpD * 10) / 10;
    calcMp = Math.floor(calcMp * calcMpC * cmp * 10) / 10;



    ////アクセルコンボ確認
    //if ((eleccion[0] === "A") && (eleccion[1] === "A") && (eleccion[3] === "A"))
    //    calcMp += 20;




    //d 属性倍率
    var calcD = 1;
    //ラジオボタンで選択したい
    var atributo = $("input[name='ctl00$MainContent$atributo']:checked").val();
    switch (atributo) {
        case "有利":
            {
                calcD = 1.5;
                break;
            }
        case "不利":
            {
                calcD = 0.5;
                break;
            }
        default://並盛
            break;
    }



    //e 補正係数
    var calcE = 1;

    var calcFinal = 1;
    var calcSub = 1;
    if (eleccion[orden - 1] === "B") {
        switch (orden) {
            case 2:
                {
                    calcSub = 1.1;
                    break;
                }
            case 3:
                {
                    calcSub = 1.2;
                    break;
                }
        }
    }
    var calcBsub = 0;
    if (eleccion[orden - 1] === "B")
        calcBsub = 0.1;
    if (ventana === "1") {
        calcFinal = (calcA * calcB + calcBsub) * calcSub * calcC * calcD * calcE;
    }
    else
        calcFinal = calcA * calcB * calcSub * calcC * calcD * calcE;


    //取り合えず小数点以下3桁で切り捨てる
    calcFinal = Math.floor(calcFinal * 1000) / 1000;
    return [calcFinal, calcMp];

}

//全角→半角 数字変換関数
function Zenhan(str) {
    str = str.replace(/[０-９]/g, function (s) {
        return String.fromCharCode(s.charCodeAt(0) - 0xFEE0);
    });
    str = str.replace(/[\%\％]/, "");
    str = str.replace("－", "-");
    return str;
}

//表示補助関数
function ChequeaPorcentaje(str) {
    var Add = "";
    if (String(str).match(/[%％]/) === null) {
        add = "%";
    }
    return str + add;
}

//数値チェック関数
function ChequeaLimite(str, id) {
    //数値部取り出し
    str = str.replace(/[%％]/, "");
    var obj = document.getElementById(id);
    if (((Number(str) < -200) || ((Number(str) < 5) && (Number(str) > -5)) || (Number(str) > 200)) && (Number(str) !== 0))
        obj.style.color = "red";
    else
        obj.style.color = "blue";
}




//覚醒テーブル取得関数
function ConsigoTablaDespierta(tipo) {
    //覚醒補正テーブル
    var tablaDespiertaB = [0.07, 0.07, 0.07, 0.05, 0.09, 0.05];
    var tablaDespiertaM = [0.06, 0.08, 0.07, 0.07, 0.05, 0.07];
    var tablaDespiertaD = [0.09, 0.05, 0.07, 0.07, 0.07, 0.05];
    var tablaDespiertaH = [0.08, 0.06, 0.07, 0.07, 0.05, 0.07];
    var tablaDespiertaA = [0.05, 0.09, 0.06, 0.09, 0.05, 0.05];
    var tablaDespiertaS = [0.06, 0.06, 0.09, 0.05, 0.07, 0.07];

    //魔法少女タイプにより、覚醒テーブルを確定
    var tablaDespierta;
    if (tipo === 1) {
        tipoMagia = $("input[name='ctl00$MainContent$tipoPuella']:checked").val();
    }
    else {
        tipoMagia = $("input[name='ctl00$MainContent$tipoPuella2']:checked").val();
    }
    switch (tipoMagia) {
        case "マギア":
            {
                tablaDespierta = tablaDespiertaM;
                break;
            }
        case "サポート":
            {
                tablaDespierta = tablaDespiertaS;
                break;
            }
        case "ヒール":
            {
                tablaDespierta = tablaDespiertaH;
                break;
            }
        case "ディフェンス":
            {
                tablaDespierta = tablaDespiertaD;
                break;
            }
        case "アタック":
            {
                tablaDespierta = tablaDespiertaA;
                break;
            }
        case "バランス":
            {
                tablaDespierta = tablaDespiertaB;
                break;
            }
    }
    return tablaDespierta;
}


//表示処理関数
function IndicaResultado() {
    //ラジオボタン選択状態の取得
    var eleccion1 = $("input[name='ctl00$MainContent$RadioButtonList666']:checked").val();
    var eleccion2 = $("input[name='ctl00$MainContent$RadioButtonList667']:checked").val();
    var eleccion3 = $("input[name='ctl00$MainContent$RadioButtonList668']:checked").val();

    var calc1 = CalcAjustado(eleccion1, eleccion2, eleccion3, 1);
    var calc2 = CalcAjustado(eleccion1, eleccion2, eleccion3, 2);
    var calc3 = CalcAjustado(eleccion1, eleccion2, eleccion3, 3);

    document.getElementById('MainContent_modificado1').innerText = calc1[0];
    document.getElementById('MainContent_modificado2').innerText = calc2[0];
    document.getElementById('MainContent_modificado3').innerText = calc3[0];
    document.getElementById('MainContent_modificado4').innerText = (calc1[0] * 1000 + calc2[0] * 1000 + calc3[0] * 1000) / 1000;

    ////////
    //MP処理
    var calcTodo = Math.floor((calc1[1] + calc2[1] + calc3[1]) * 10) / 10;
    //小数点チェックボックスoff時
    if ($("input[name='ctl00$MainContent$decimal']:checked").val() !== "on") {
        calcTodo = Math.floor(calc1[1] + calc2[1] + calc3[1]);
        calc1[1] = Math.floor(calc1[1]);
        calc2[1] = Math.floor(calc2[1]);
        calc3[1] = Math.floor(calc3[1]);
    }
    else {
        calc1[1] = Math.floor(calc1[1] * 10) / 10;
        calc2[1] = Math.floor(calc2[1] * 10) / 10;
        calc3[1] = Math.floor(calc3[1] * 10) / 10;
    }
    //MP表示処理
    //AAAのみ特殊処理
    var AAA = "";
    if (eleccion1 === "A" && eleccion2 === "A" && eleccion3 === "A")
        AAA = " + 20";

    document.getElementById('MainContent_magia1').innerText = calc1[1];
    document.getElementById('MainContent_magia2').innerText = calc2[1];
    document.getElementById('MainContent_magia3').innerText = calc3[1];
    document.getElementById('MainContent_magia4').innerText = calcTodo + AAA;


    var dano = CalcDano();
    if (dano < 500)
        dano = 500;
    //覚醒補正
    var ajusteDespierto = [0, 0, 0];
    var eleccion = [eleccion1, eleccion2, eleccion3];
    var tablaDespierta = ConsigoTablaDespierta(1);
    for (let i = 0; i < 3; i++) {
        switch (eleccion[i]) {
            case "A":
                {
                    if (color[3] === 1)
                        ajusteDespierto[i] = tablaDespierta[3];
                    break;
                }
            case "B":
                {
                    if (color[5] === 1)
                        ajusteDespierto[i] = tablaDespierta[5];
                    break;
                }
            case "C":
                {
                    if (color[4] === 1)
                        ajusteDespierto[i] = tablaDespierta[4];
                    break;
                }

        }
    }

    var danoFinal = [calc1[0] * dano * (1 + ajusteDespierto[0]), calc2[0] * dano * (1 + ajusteDespierto[1]), calc3[0] * dano * (1 + ajusteDespierto[2])];

    for (var j = 0; j < 3; j++) {
        if (danoFinal[j] < 250)
            danoFinal[j] = 250;
    }

    if ($("input[name='ctl00$MainContent$ventana']:checked").val() === "1") {
        //ミラーズ補正
        for (var i = 0; i < 3; i++) {
            danoFinal[i] *= 0.7;
        }
    }

    document.getElementById('MainContent_dano1').innerText = Math.floor(danoFinal[0]);
    document.getElementById('MainContent_dano2').innerText = Math.floor(danoFinal[1]);
    document.getElementById('MainContent_dano3').innerText = Math.floor(danoFinal[2]);
    document.getElementById('MainContent_dano4').innerText = Math.floor(danoFinal[0] + danoFinal[1] + danoFinal[2]);
}

//モバイル版表示時にモバイル用文字を表示
function CambiaLetra() {
    if (window.parent.screen.width <= 750) {
        $("#pequeno11").removeClass("hidden");
        $("#pequeno12").removeClass("hidden");
        $("#pequeno13").removeClass("hidden");
        $("#pequeno21").removeClass("hidden");
        $("#pequeno22").removeClass("hidden");
        $("#pequeno23").removeClass("hidden");
        $("#pequeno31").removeClass("hidden");
        $("#pequeno32").removeClass("hidden");
        $("#pequeno33").removeClass("hidden");
        $("#pequeno41").removeClass("hidden");
        $("#pequeno42").removeClass("hidden");
        $("#pequeno43").removeClass("hidden");

        $("#grande11").addClass("hidden");
        $("#grande12").addClass("hidden");
        $("#grande13").addClass("hidden");
        $("#grande21").addClass("hidden");
        $("#grande22").addClass("hidden");
        $("#grande23").addClass("hidden");
        $("#grande31").addClass("hidden");
        $("#grande32").addClass("hidden");
        $("#grande33").addClass("hidden");
        $("#grande41").addClass("hidden");
        $("#grande42").addClass("hidden");
        $("#grande43").addClass("hidden");
    }
}

$(function () {
    $(document).ready(function () {
        //マギレコ簡易ダメージ計算ツール
        var str = location.href;//ページ名取得
        if (str.match("/Magia_damage_calc")) {
            CambiaLetra();
            IndicaResultado();
        }
    });
    //ラジオボタン変更
    $('input[name="ctl00$MainContent$RadioButtonList666"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList667"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList668"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList669"]:radio').change(function () {
        var elegido = $('input[name="ctl00$MainContent$RadioButtonList669"]:checked').val();
        switch (elegido) {
            case "A":
                {
                    $('input[name="ctl00$MainContent$RadioButtonList666"]:radio').val(["A"]);
                    $('input[name="ctl00$MainContent$RadioButtonList667"]:radio').val(["A"]);
                    $('input[name="ctl00$MainContent$RadioButtonList668"]:radio').val(["A"]);
                    break;
                }
            case "B":
                {
                    $('input[name="ctl00$MainContent$RadioButtonList666"]:radio').val(["B"]);
                    $('input[name="ctl00$MainContent$RadioButtonList667"]:radio').val(["B"]);
                    $('input[name="ctl00$MainContent$RadioButtonList668"]:radio').val(["B"]);
                    break;
                }
            case "C":
                {
                    $('input[name="ctl00$MainContent$RadioButtonList666"]:radio').val(["C"]);
                    $('input[name="ctl00$MainContent$RadioButtonList667"]:radio').val(["C"]);
                    $('input[name="ctl00$MainContent$RadioButtonList668"]:radio').val(["C"]);
                    break;
                }
        }
        IndicaResultado();
    });
    //チャージ数入力
    $('input[name="chargePlus"]').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$atributo"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$TextBox666"]').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$TextBox667"]').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$TextBox668"]').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$TextBox669"]').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$ventana"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$estadoAtk"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$tipoPuella"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$tipoPuella2"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$decimal"]').change(function () {
        IndicaResultado();
    });

    //陣形補正、メモリア攻撃力UPの取得
    $('input[name="ctl00$MainContent$ordendeBatalla"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$ordendeBatalla2"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$AtkUp"]').change(function () {
        IndicaResultado();

        //表示処理
        var AtkUp = Number(Zenhan($('#MainContent_AtkUp').val())) || 0;
        AtkUp = ChequeaPorcentaje(AtkUp);
        ChequeaLimite(AtkUp, 'MainContent_AtkUp');

        $('#MainContent_AtkUp').val(AtkUp);
    });
    $('input[name="ctl00$MainContent$DefUp"]').change(function () {
        IndicaResultado();

        //表示処理
        var DefUp = Number(Zenhan($('#MainContent_DefUp').val())) || 0;
        DefUp = ChequeaPorcentaje(DefUp);
        ChequeaLimite(DefUp, 'MainContent_DefUp');

        $('#MainContent_DefUp').val(DefUp);
    });
    //メモリアMP補正
    $("#MainContent_MpUp").change(function () {
        IndicaResultado();
    });
    $("#MainContent_AMpUp").change(function () {
        IndicaResultado();
    });



    //アコーデオン
    $('#atkCollapse').on("click", { name: '#atkCollapse' }, CambiaEstado);
    $('#defCollapse').on("click", { name: '#defCollapse' }, CambiaEstado);
});

//アコーデオン処理関数
function CambiaEstado(event) {
    var address = event.data.name;
    var str = $(address).text();
    str = str.substr(0, 5);
    $(address).toggleClass("active");
    if (str === "攻撃側設定")
        $("#multiCollapseExample1").collapse("toggle");
    else
        $("#multiCollapseExample2").collapse("toggle");
    if ($(address).hasClass("active")) {
        str += " ひらく";

    }
    else {
        str += " とじる";
    }
    $(address).html(str);
}


//////////////////////////////////////////////////////
//canvas処理
//////////////////////////////////////////////////////
window.addEventListener("load", function () {
    draw();
    draw2();
    draw3();
});
var color = [0, 0, 0, 0, 0, 0];
var color2 = [0, 0, 0, 0, 0, 0];

function draw() {
    var canvas = document.getElementById("canvas1");
    // canvas.id = "canvas1";
    var scaleF = 0.6;
    // canvas.width = 320;
    // canvas.height = 300;
    // document.body.appendChild(canvas);
    if (!canvas || !canvas.getContext) {
        return;
    }

    //設定
    var ctx = canvas.getContext("2d");
    var r = 20;
    var letra = ["DEF", "ATK", "HP", "A", "C", "B"];

    const c1 = {
        x: 40 * scaleF,
        y: 80 * scaleF,
    };
    const c2 = {
        x: 120 * scaleF,
        y: 40 * scaleF,
    };
    const c3 = {
        x: 200 * scaleF,
        y: 80 * scaleF,
    };
    const c4 = {
        x: 40 * scaleF,
        y: 160 * scaleF,
    };
    const c5 = {
        x: 120 * scaleF,
        y: 200 * scaleF,
    };
    const c6 = {
        x: 200 * scaleF,
        y: 160 * scaleF,
    };

    var circle = [c1, c2, c3, c4, c5, c6];

    //描画
    for (let i = 0; i < 6; i++) {
        ctx.save();
        ctx.beginPath();
        if (color[i] === 1)
            ctx.fillStyle = "red";
        else
            ctx.fillStyle = "whitesmoke";
        ctx.arc(circle[i].x, circle[i].y, r, 0, 2 * Math.PI);
        ctx.fill();
        ctx.stroke();
        ctx.restore();

        ctx.save();
        ctx.font = '15px sans-serif';
        if (color[i] === 1)
            ctx.fillStyle = "whitesmoke";
        else
            ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'middle';
        ctx.fillText(letra[i], circle[i].x, circle[i].y);
        ctx.restore();
    }

    //on offボタン
    ctx.save();
    ctx.beginPath();
    ctx.fillStyle = "whitesmoke";
    ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
    ctx.fill();
    ctx.stroke();
    ctx.restore();

    ctx.save();
    ctx.font = '20px sans-serif';
    ctx.fillStyle = "red";
    ctx.textAlign = "center";
    ctx.textBaseline = 'middle';
    ctx.fillText("ON", circle[1].x, (circle[4].y + circle[1].y) / 2);
    ctx.restore();
    //描画ここまで

    var hit = new Array(6);
    var hitOn = false;
    //on offボタン用
    var checkOnoff = 0;

    //クリックイベント
    var timer = null;
    canvas.addEventListener("click", function (e) {
        clearInterval(timer);
        //座標取得 真ん中の中心からの位置
        const rect = canvas.getBoundingClientRect();
        const point = {
            x: e.clientX - rect.left,
            y: e.clientY - rect.top
        };
        var variantR = 1;//描画円周


        for (let j = 0; j < 6; j++) {
            hit[j] = Math.pow(circle[j].x - point.x, 2) + Math.pow(circle[j].y - point.y, 2) <= Math.pow(r, 2);
        }

        //周囲の円check
        for (let i = 0; i < 6; i++) {
            if (hit[i] === true) {
                color[i] = CheckHit(color[i], circle[i], letra[i], r, ctx, true);
            }
        }

        //on offボタンの処理
        hitOn = Math.pow(circle[1].x - point.x, 2) + Math.pow((circle[4].y + circle[1].y) / 2 - point.y, 2) <= Math.pow(r * 1.2, 2);

        //Onボタンのみの処理
        if (hitOn === false) {
            IndicaResultado();
            return;
        }
        //描画メイン処理
        timer = setInterval(function () {
            ctx.clearRect(0, 0, canvas.width, canvas.height, 2 * r, 2 * r);
            DrawObject(ctx, color, circle, r, letra, checkOnoff);
            ctx.beginPath();
            //クリック地点を中心とする円
            ctx.save();
            ctx.globalCompositeOperation = "source-atop";//交差部分のみ着色
            var radialGrad = ctx.createRadialGradient(point.x, point.y, 1, point.x, point.y, variantR);
            if (checkOnoff === 0) {
                // ctx.fillStyle = "red";
                radialGrad.addColorStop(0, "white");
                radialGrad.addColorStop(0.90, "white");
                radialGrad.addColorStop(0.95, "red");
                radialGrad.addColorStop(1, "red");
            }
            else {
                // ctx.fillStyle = "white";
                radialGrad.addColorStop(0, "red");
                radialGrad.addColorStop(0.90, "red");
                radialGrad.addColorStop(0.95, "white");
                radialGrad.addColorStop(1, "whitesmoke");
            }
            ctx.globalAlpha = 0.8;
            ctx.arc(point.x, point.y, variantR, 0, 2 * Math.PI);
            ctx.fillStyle = radialGrad;
            ctx.fill();
            ctx.restore();
            LetraEscrita(ctx, color, circle, r, letra, checkOnoff);
            variantR += 3;
            //終了条件
            if (variantR > canvas.width * 2 / 3) {
                ctx.clearRect(0, 0, canvas.width, canvas.height, 2 * r, 2 * r);
                clearInterval(timer);
                //DrawObject(ctx, circle, r, letra);



                if (hitOn === true) {
                    switch (checkOnoff) {
                        case 0:
                            {
                                //全部on
                                for (let i = 0; i < 6; i++) {
                                    color[i] = CheckHit(0, circle[i], letra[i], r, ctx, true);
                                }
                                checkOnoff = 1;

                                ctx.save();
                                ctx.beginPath();
                                ctx.fillStyle = "red";
                                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                                ctx.fill();
                                ctx.stroke();
                                ctx.restore();

                                ctx.save();
                                ctx.font = '20px sans-serif';
                                ctx.fillStyle = "whitesmoke";
                                ctx.textAlign = "center";
                                ctx.textBaseline = 'middle';
                                ctx.fillText("OFF", circle[1].x, (circle[4].y + circle[1].y) / 2);
                                ctx.restore();
                                break;
                            }
                        case 1:
                            {
                                //全部off
                                for (let i = 0; i < 6; i++) {
                                    color[i] = CheckHit(1, circle[i], letra[i], r, ctx, true);
                                }
                                checkOnoff = 0;

                                ctx.save();
                                ctx.beginPath();
                                ctx.fillStyle = "whitesmoke";
                                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                                ctx.fill();
                                ctx.stroke();
                                ctx.restore();

                                ctx.save();
                                ctx.font = '20px sans-serif';
                                ctx.fillStyle = "red";
                                ctx.textAlign = "center";
                                ctx.textBaseline = 'middle';
                                ctx.fillText("ON", circle[1].x, (circle[4].y + circle[1].y) / 2);
                                ctx.restore();
                                break;
                            }
                    }
                }
                IndicaResultado();
            }
        }, 1000 / 60);
    });
}

function DrawObject(ctx, color, circle, r, letra, checkOnoff) {
    //外周描画
    // for(let i = 0; i<6; i++){
    //     ctx.save();
    //     ctx.beginPath();
    //     if(color[i]===1)
    //         ctx.fillStyle = "red";
    //     else
    //         ctx.fillStyle = "whitesmoke";
    //     ctx.arc(circle[i].x,circle[i].y,r,0,2*Math.PI);
    //     ctx.fill();
    //     ctx.stroke();
    //     ctx.restore();

    //     ctx.save();
    //     ctx.font = '15px sans-serif';
    //     if(color[i]===1)
    //         ctx.fillStyle = "whitesmoke";
    //     else
    //         ctx.fillStyle = "red";
    //     ctx.textAlign = "center";
    //     ctx.textBaseline = 'middle';
    //     ctx.fillText(letra[i], circle[i].x, circle[i].y);
    //     ctx.restore();
    // }
    //中心描画
    switch (checkOnoff) {
        case 1:
            {
                //全部on状態の描画
                for (let i = 0; i < 6; i++) {
                    CheckHit(0, circle[i], letra[i], r, ctx, false);
                }
                // checkOnoff = 1;

                ctx.save();
                ctx.beginPath();
                ctx.fillStyle = "red";
                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                ctx.fill();
                ctx.stroke();
                ctx.restore();

                ctx.save();
                ctx.font = '20px sans-serif';
                ctx.fillStyle = "whitesmoke";
                ctx.textAlign = "center";
                ctx.textBaseline = 'middle';
                ctx.fillText("OFF", circle[1].x, (circle[4].y + circle[1].y) / 2);
                ctx.restore();
                break;
            }
        case 0:
            {
                //全部off状態の描画
                for (let i = 0; i < 6; i++) {
                    CheckHit(1, circle[i], letra[i], r, ctx, false);
                }
                // checkOnoff = 0;

                ctx.save();
                ctx.beginPath();
                ctx.fillStyle = "whitesmoke";
                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                ctx.fill();
                ctx.stroke();
                ctx.restore();

                ctx.save();
                ctx.font = '20px sans-serif';
                ctx.fillStyle = "red";
                ctx.textAlign = "center";
                ctx.textBaseline = 'middle';
                ctx.fillText("ON", circle[1].x, (circle[4].y + circle[1].y) / 2);
                ctx.restore();
                break;
            }
    }
}

//円内の文字描画処理
function LetraEscrita(ctx, color, circle, r, letra, checkOnoff) {

    //中心
    switch (checkOnoff) {
        case 0:
            {
                ctx.save();
                ctx.font = '20px sans-serif';
                ctx.fillStyle = "whitesmoke";
                ctx.textAlign = "center";
                ctx.textBaseline = 'middle';
                ctx.fillText("OFF", circle[1].x, (circle[4].y + circle[1].y) / 2);
                ctx.restore();

                color.forEach(ele => {
                    ele = 0;
                });

                break;
            }
        case 1:
            {
                ctx.save();
                ctx.font = '20px sans-serif';
                ctx.fillStyle = "red";
                ctx.textAlign = "center";
                ctx.textBaseline = 'middle';
                ctx.fillText("ON", circle[1].x, (circle[4].y + circle[1].y) / 2);
                ctx.restore();

                color.forEach(ele => {
                    ele = 1;
                });
                break;
            }
    }
    //外周円
    for (let i = 0; i < 6; i++) {
        CheckHit2();
    }
}

//フラグに合わせて周囲の円を描画する関数
function CheckHit(color, circle, letra, r, ctx, returnFlag) {
    // alert("clicked");
    if (color === 0) {
        ctx.save();
        ctx.beginPath();
        ctx.fillStyle = "red";
        ctx.arc(circle.x, circle.y, r, 0, 2 * Math.PI);
        ctx.fill();
        ctx.font = '20px sans-serif';
        ctx.fillStyle = "whitesmoke";
        ctx.textAlign = "center";
        ctx.textBaseline = 'middle';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.restore();
        if (returnFlag === true)
            color = 1;
    }
    else if (color === 1) {
        ctx.save();
        ctx.beginPath();
        ctx.fillStyle = "whitesmoke";
        ctx.arc(circle.x, circle.y, r, 0, 2 * Math.PI);
        ctx.fill();
        ctx.font = '20px sans-serif';
        ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'middle';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.restore();
        if (returnFlag === true)
            color = 0;
    }
    if (returnFlag === true)
        return color;
    else
        return;
}

//フラグに合わせて周囲の文字を描画する関数
function CheckHit2(color, circle, letra, r, ctx) {
    // alert("clicked");
    if (color === 0) {
        ctx.save();
        ctx.font = '20px sans-serif';
        ctx.fillStyle = "whitesmoke";
        ctx.textAlign = "center";
        ctx.textBaseline = 'middle';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.restore();
    }
    else if (color === 1) {
        ctx.save();
        ctx.font = '20px sans-serif';
        ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'middle';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.restore();
    }
}

function draw2() {
    var canvas = document.getElementById("canvas2");
    // canvas.id = "canvas1";
    var scaleF = 0.6;
    // canvas.width = 320;
    // canvas.height = 300;
    // document.body.appendChild(canvas);
    if (!canvas || !canvas.getContext) {
        return;
    }

    //設定
    var ctx = canvas.getContext("2d");
    var r = 20;
    var letra = ["DEF", "ATK", "HP", "A", "C", "B"];

    const c1 = {
        x: 40 * scaleF,
        y: 80 * scaleF,
    };
    const c2 = {
        x: 120 * scaleF,
        y: 40 * scaleF,
    };
    const c3 = {
        x: 200 * scaleF,
        y: 80 * scaleF,
    };
    const c4 = {
        x: 40 * scaleF,
        y: 160 * scaleF,
    };
    const c5 = {
        x: 120 * scaleF,
        y: 200 * scaleF,
    };
    const c6 = {
        x: 200 * scaleF,
        y: 160 * scaleF,
    };

    var circle = [c1, c2, c3, c4, c5, c6];

    //描画
    for (let i = 0; i < 3; i++) {
        ctx.save();
        ctx.beginPath();
        ctx.fillStyle = "whitesmoke";
        ctx.arc(circle[i].x, circle[i].y, r, 0, 2 * Math.PI);
        ctx.fill();
        ctx.stroke();
        ctx.restore();

        ctx.save();
        ctx.font = '15px sans-serif';
        ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'middle';
        ctx.fillText(letra[i], circle[i].x, circle[i].y);
        ctx.restore();
    }

    var hit = new Array(6);

    //クリックイベント
    canvas.addEventListener("click", e => {
        const rect = canvas.getBoundingClientRect();
        const point = {
            x: e.clientX - rect.left,
            y: e.clientY - rect.top
        };

        for (let j = 0; j < 3; j++) {
            hit[j] = Math.pow(circle[j].x - point.x, 2) + Math.pow(circle[j].y - point.y, 2) <= Math.pow(r, 2);
        }

        //check
        for (let i = 0; i < 1; i++) {
            if (hit[i] === true) {
                color2[i] = CheckHit(color2[i], circle[i], letra[i], r, ctx, true);
            }
        }
        IndicaResultado();
    });
}

////////////////////////////
//キャラ選択画面用描画関数
////////////////////////////
var elegida = -1;

function draw3() {
    var canvas = document.getElementById("canvas3");
    if (!canvas || !canvas.getContext) {
        return;
    }

    //設定
    var ctx = canvas.getContext("2d");
    var personas = 10;
    
    //JSONGET処理
    $.getJSON("Scripts/magia_json/magia.json", function (data) {
        personas = data.length;
    });
    
    var scaleF = 1;
    var r = 30;
    var x0 = r;
    var x = [personas];
    var y = 40;
    var offset = 2 * r + 10;
    var letra = [personas];
    var jsonData;

    //JSONGET処理
    $.getJSON("Scripts/magia_json/magia.json", function (data) {
         jsonData = JSON.parse(data);
    });

    for (let i = 0; i < personas; i++) {
        x[i] = i === 0 ? x0 : x[i - 1] + offset;
        letra[i] = i + 1;
    }

    for (let i = 0; i < personas; i++) {
        DrawCircle(ctx, x[i], y, r, 1);
        DrawText(ctx, x[i], y, letra[i], 1);
    }

    var pointerFlag = false;
    // console.log(pointerFlag + "0");

    //向き判定
    var direction;

    var number;
    var clickX;
    var clickY;

    var isStatic = 0;

    canvas.addEventListener("pointerdown", function (e) {
        isStatic = 1;
        //エリア内ならフラグon
        //ポインタがcanvas内にあるか判定
        const rect = canvas.getBoundingClientRect();
        clickX = e.clientX - rect.left;
        clickY = e.clientY - rect.top;
        if ((clickX < canvas.width) && (clickY < canvas.height)) {
            pointerFlag = true;
            // console.log(pointerFlag + " down");
        }

        //クリックした物体の判定処理
        // if(number === -1)
        //     return;
        number = -1;
        //ポインタが円内にあるか判定
        for (let i = 0; i < personas; i++) {
            if ((clickX - x[i]) ** 2 + (clickY - y) ** 2 < r ** 2) {
                number = i;
                break;
            }
        }
    });
    //ポインタ乗ったらドラッグ処理
    canvas.addEventListener('pointermove', function (e) {
        if (!pointerFlag) {
            // console.log(pointerFlag + " move");
            return;
        }
        //オフセット位置取得
        const rect = canvas.getBoundingClientRect();
        var px = e.clientX - rect.left - clickX;
        var py = e.clientY - rect.top - clickY;
        clickX = e.clientX - rect.left;
        clickY = e.clientY - rect.top;

        //canvas外は受け付けない
        if ((clickX > canvas.width) || (clickX < 0) || (clickY > canvas.height) || (clickY < 0)) {
            pointerFlag = false;
            // console.log(pointerFlag + " limit");
            return;
        }

        //両端の移動制限
        //左端
        if ((px > 0) && (x[0] >= x0)) {
            if (clickX > canvas.width)
                pointerFlag = false;
            // console.log(pointerFlag + " left");
            return;
        }
        //右端
        if ((px < 0) && (x[personas - 1] <= canvas.width - (x0))) {
            if (clickX < 0)
                pointerFlag = false;
            // console.log(pointerFlag + " right");
            return;
        }
        console.log("px " + px);
        //pxが少ない場合は色変え
        if (Math.abs(px) > 3)
            isStatic = 0;

        //描画メイン処理
        if (pointerFlag) {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            for (let i = 0; i < personas; i++) {
                x[i] += px;
                if (elegida === i) {//色変え
                    DrawCircle(ctx, x[i], y, r, -1);
                    DrawText(ctx, x[i], y, letra[i], -1);
                }
                else {//通常色
                    DrawCircle(ctx, x[i], y, r, 1);
                    DrawText(ctx, x[i], y, letra[i], 1);
                }
            }
            direction = px;

        }

    });



    //ポインタ離れたらドラッグ終了
    canvas.addEventListener("pointerup", function (e) {
        pointerFlag = false;
        // console.log(pointerFlag + " UP");

        if (isStatic === 1 && number !== -1) {
            console.log("elegida " + elegida + " number" + number);
            switch (elegida) {
                case number:
                    {//同色の場合は色を初期に戻す
                        //同じ番号の場合
                        //色を戻す処理
                        elegida = -1;
                        console.log(elegida + " 同番の場合色戻し");
                        DrawCircle(ctx, x[number], y, r, 1);
                        DrawText(ctx, x[number], y, letra[number], 1);
                        break;
                    }
                default://他番号の場合
                    {
                        //もともとの色を戻す
                        if (elegida !== -1) {//もともと未選択の場合はここをパス
                            DrawCircle(ctx, x[elegida], y, r, 1);
                            DrawText(ctx, x[elegida], y, letra[elegida], 1);
                        }
                        //新しく選択されたものの色を変える
                        DrawCircle(ctx, x[number], y, r, -1);
                        DrawText(ctx, x[number], y, letra[number], -1);
                        elegida = number;
                        console.log(elegida + " 他番の場合色変え");
                        break;
                    }
            }
        }
    });

    canvas.addEventListener('pointercancel', function (e) {
        pointerFlag = false;
        // console.log(pointerFlag + " cancel");
    });
}
//color 
//1 black それ以外 white
function DrawCircle(ctx, x, y, r, color) {
    ctx.beginPath();
    ctx.save();
    ctx.fillStyle = color === 1 ? "black" : "white";
    ctx.strokeStyle = "black";
    ctx.arc(x, y, r, 0, 2 * Math.PI);
    ctx.fill();
    ctx.stroke();
    ctx.restore();
}
function DrawText(ctx, x, y, letra, color) {
    ctx.save();
    ctx.font = "16px sans-serif";
    ctx.fillStyle = color === 1 ? "white" : "black";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(letra, x, y);
    ctx.restore();
}





///////////////////////////////////////////////////////////
//ガチャ石計算
///////////////////////////////////////////////////////////
function CalcResultado() {
    //入力値から値取得
    var type = $("input[name='ctl00$MainContent$RadioButtonList777']:checked").val();
    var typeText = $('#MainContent_numero_de_paraiso').val();
    var billete = $('#MainContent_billete').val();
    var piedra = $('#MainContent_piedra').val();
    var type10 = $("input[name='ctl00$MainContent$RadioButtonList778']:checked").val();
    var precio = $("input[name='ctl00$MainContent$RadioButtonList779']:checked").val();
    var precioText = $('#MainContent_precio').val();

    var gachaNum = $('#MainContent_numero_de_la_gacha').val();
    var numeroPiedra = $('#MainContent_numero_de_la_piedra').val();
    var resultado = document.getElementById('MainContent_resultado');
    var resultado2 = document.getElementById('MainContent_resultado2');

    //入力値の確認
    if ((type == 0) && (typeText == "それ以外の場合はこちらに天井数を入力")) {
        resultado.innerText = "天井数を入力して下さい";
        resultado2.innerText = "天井数を入力して下さい";
        return;
    }
    if (type == 0)
        type = typeText;
    if ((precio == 0) && (precioText == "その他の場合はこちらに石価格を入力")) {
        resultado.innerText = "10回分の石価格がわからないので指定して下さい";
        resultado2.innerText = "10回分の石価格がわからないので指定して下さい";
        return;
    }
    if (precio == 0)
        precio = precioText;
    if ((billete == "価格") || (piedra == "石")) {
        resultado.innerText = "石価格を設定して下さい";
        resultado2.innerText = "石価格を設定して下さい";
        return;
    }


    //計算部分
    //必要な石の計算
    var realLimite = type - gachaNum;
    if ((realLimite > type) || (realLimite < 0))
        resultado.innerText = "いっぱい石あるんで回しましょ";




    //石が何個必要か算出
    //10連を何回するか
    var gacha10 = Math.floor(realLimite / type10);
    var sobra = Math.floor(realLimite % type10);
    //必要石の計算
    var piedraNecesitada = gacha10 * precio + sobra * precio / 10 - numeroPiedra;
    if (piedraNecesitada < 0) {
        resultado.innerText = "いっぱい石あるんで回しましょ";
        resultado2.innerText = "いっぱい石あるんで回しましょ";
        return;
    }
    //必要石表示
    resultado.innerText = piedraNecesitada + "個";
    //のちのち使うため保存
    var piedraNecesitadaOri = piedraNecesitada;

    //課金額算出
    //プリコネ7500石が絡む場合はあらかじめ別計算
    var juego = $("input[name='ctl00$MainContent$tipo_del_juego']:checked").val();
    var prikoneCuenta = 0;
    var cuenta7500 = $("input[name='ctl00$MainContent$RadioButtonList780']:checked").val();
    if (juego == "プリコネ") {
        if (cuenta7500 != 3) {
            for (var i = 0; i < (3 - cuenta7500); i++) {
                piedraNecesitada -= 7500;
                if (piedraNecesitada < 0) {
                    //もう7500石で買う必要が無い場合
                    //特殊処理
                    //プリコネカウント0の時
                    // 出力は例えばさらに7500石を買った場合、何個あまるみたいな表示にする？→それだめだ。他の表示と整合がとれない
                    //この先考えると、piedraを7500に変更しておいて、その先、kakinが0になった場合の処理を追加する方針にする
                    //プリコネカウント1
                    // 
                    //プリコネカウント2

                    piedra = 7500;

                    //forループ抜け
                    break;
                }
                else {
                    prikoneCuenta++;//7500での購入回数
                }
            }
        }

    }

    var piedraUnidad = Math.floor(piedraNecesitada / piedra);
    var kakin = (piedraUnidad + prikoneCuenta) * billete;
    //あまり分算出
    var sobraFinal = Math.floor(piedraNecesitada % piedra);




    //最終結果表示
    if (juego == "プリコネ") {
        if ((piedraNecesitada <= 0) && (!((piedraNecesitadaOri >= 5000) && (cuenta7500 == 3)))) {
            resultado2.innerText = piedraNecesitadaOri + "個の石を買う必要があります";
        }
        //かなり特殊な場合。3回購入済み、必要な石が7500個よりも少ない場合
        else if ((piedraNecesitadaOri >= 5000) && (piedraNecesitadaOri < 7500) && (cuenta7500 == 3)) {
            if (Math.floor(piedraNecesitadaOri == 5000)) {
                //ちょうど課金であがなえる場合
                resultado2.innerText = billete + "円課金する必要があります";
            }
            else {
                resultado2.innerText = billete + "円課金し、更に" + (piedraNecesitadaOri - 5000) + "個の石を買う必要があります";
            }
        }
        else if (sobraFinal == 0) {
            resultado2.innerText = kakin + "円課金する必要があります";
        }
        else if (piedraNecesitadaOri < 5000) {
            resultado2.innerText = piedraNecesitadaOri + "個の石を買う必要があります";
        }
        else {
            resultado2.innerText = kakin + "円課金し、更に" + sobraFinal + "個の石を買う必要があります";
        }
    }
    else {
        if (piedraNecesitada < piedra) {
            resultado2.innerText = piedraNecesitadaOri + "個の石を買う必要があります";
        }
        else {
            resultado2.innerText = kakin + "円課金し、更に" + sobraFinal + "個の石を買う必要があります";
        }
    }
}

//ゲームにより設定変更
function CambioPropiedades() {
    var juego = $("input[name='ctl00$MainContent$tipo_del_juego']:checked").val();
    var data = [5];

    //ゲームによる設定値
    switch (juego) {
        case "プリコネ":
            {
                //天井数
                data[0] = 300;
                //石価格
                data[1] = 9800;
                data[2] = 5000;
                //ガチャ10回で何回
                data[3] = 10;
                //ガチャ10回分の価格
                data[4] = 1500;
                break;
            }
        case "マギレコ":
            {
                //天井数
                data[0] = 300;
                //石価格
                data[1] = 11500;
                data[2] = 1085;
                //ガチャ10回で何回
                data[3] = 10;
                //ガチャ10回分の価格
                data[4] = 250;
                break;
            }
        case "自分で設定":
            {
                //天井数
                data[0] = 300;
                //石価格
                data[1] = "価格";
                data[2] = "石";
                //ガチャ10回で何回
                data[3] = 10;
                //ガチャ10回分の価格
                data[4] = 250;
                break;
            }
    }

    //得られた情報により計算


    //天井
    var type = $("input[name='ctl00$MainContent$RadioButtonList777']:radio");
    var typeText = $('#MainContent_numero_de_paraiso');
    switch (data[0]) {
        case 100:
            {
                type[0].checked = true;
                break;
            }
        case 300:
            {
                type[1].checked = true;
                break;
            }
        default:
            {
                type[2].checked = true;
                typeText.value = data[0];
                break;
            }
    }



    //石価格
    document.getElementById("MainContent_billete").value = data[1];
    document.getElementById("MainContent_piedra").value = data[2];

    ////ガチャ10回で何回
    var type10 = $("input[name='ctl00$MainContent$RadioButtonList778']:radio");
    switch (data[3]) {
        case 10:
            {
                type10[0].checked = true;
                break;
            }
        case 11:
            {
                type10[1].checked = true;
                break;
            }
    }
    //ガチャ10回分の価格
    var precio = $("input[name='ctl00$MainContent$RadioButtonList779']:radio");
    var precioText = $('#MainContent_precio');
    switch (data[4]) {
        case 250:
            {
                precio[0].checked = true;
                break;
            }
        case 1500:
            {
                precio[1].checked = true;
                break;
            }
        case 2500:
            {
                precio[2].checked = true;
                break;
            }
        case 3000:
            {
                precio[3].checked = true;
                break;
            }
        case 5000:
            {
                precio[4].checked = true;
                break;
            }
        case undefined:
            break;

        default:
            {
                precio[5].checked = true;
                precioText.value = data[4];
                break;
            }
    }
    return;
}





$(function () {
    $(document).ready(function () {
        //天上への道の処理
        var str = location.href;//ページ名取得
        if (str.match("/La_via_al_paraiso")) {
            CalcResultado();
        }
    });
    //ラジオボタン変更イベント
    $('input[name="ctl00$MainContent$tipo_del_juego"]:radio').change(function () {
        CambioPropiedades();
        //プリコネの場合はenable
        if ($("input[name='ctl00$MainContent$tipo_del_juego']:checked").val() == "プリコネ") {
            $('input[name="ctl00$MainContent$RadioButtonList780"]').prop('disabled', false);
        }
        else {
            $('input[name="ctl00$MainContent$RadioButtonList780"]').prop('disabled', true);
        }
        CalcResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList777"]:radio').change(function () {
        CalcResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList778"]:radio').change(function () {
        CalcResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList779"]:radio').change(function () {
        CalcResultado();
    });
    $('input[name="ctl00$MainContent$RadioButtonList780"]:radio').change(function () {
        CalcResultado();
    });
    //入力イベント
    $("#MainContent_numero_de_paraiso").change(function () {
        CalcResultado();
    });
    $("#MainContent_billete").change(function () {
        CalcResultado();
    });
    $("#MainContent_piedra").change(function () {
        CalcResultado();
    });
    $("#MainContent_precio").change(function () {
        CalcResultado();
    });
    $("#MainContent_numero_de_la_gacha").change(function () {
        CalcResultado();
    });
    $("#MainContent_numero_de_la_piedra").change(function () {
        CalcResultado();
    });

});
