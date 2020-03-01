//データ出力用配列
var salida1 = new Array();
var salida2 = new Array();
var salida3 = new Array();
var salida4 = new Array();
var salida = [salida1, salida2, salida3, salida4];

//ATKとDEFより、基礎ダメージ算出
//ATKに値が入力されていない時のみエラー

function CalcDano(numero,numeroDeConnect) {
    //値取得
    var Atk = [3];
    Atk[0] = Number(Zenhan($('#MainContent_TextBox666').val())) || 0;
    Atk[1] = Number(Zenhan($('#MainContent_TextBox666_2').val())) || 0;
    Atk[2] = Number(Zenhan($('#MainContent_TextBox666_3').val())) || 0;
    var mAtk = [3];
    mAtk[0] = Number(Zenhan($('#MainContent_TextBox667').val())) || 0;
    mAtk[1] = Number(Zenhan($('#MainContent_TextBox667_2').val())) || 0;
    mAtk[2] = Number(Zenhan($('#MainContent_TextBox667_3').val())) || 0;
    var Def = Number(Zenhan($('#MainContent_TextBox668').val())) || 0;
    var mDef = Number(Zenhan($('#MainContent_TextBox669').val())) || 0;

    //覚醒補正
    var atkDespierto = 0;
    var defDespierto = 0;
    
    if (color[numero][1] === 1) {
        atkDespierto = valorAjustado[numero][1]/100 ;
    }
    //相手側DEFにより
    if (color2[0] === 1) {
        defDespierto = valorAjustado2[0]/100;
    }

    //陣形補正
    var ordenAtk = [3];
    ordenAtk[0] = $("input[name='ctl00$MainContent$ordendeBatalla']:checked").val();
    ordenAtk[1] = $("input[name='ctl00$MainContent$ordendeBatalla2']:checked").val();
    ordenAtk[2] = $("input[name='ctl00$MainContent$ordendeBatalla3']:checked").val();
    var ordenDef = $("input[name='ctl00$MainContent$ordendeBatalla4']:checked").val();
    switch (ordenAtk[numero]) {
        case "1":
            {
                ordenAtk[numero] = 1.1;
                break;
            }
        case "2":
            {
                ordenAtk[numero] = 1.15;
                break;
            }
        case "0":
            {
                ordenAtk[numero] = 1;
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
    var AtkUp = [3];
    AtkUp[0] = Number(Zenhan($('#MainContent_AtkUp').val())) || 0;
    AtkUp[1] = Number(Zenhan($('#MainContent_AtkUp2').val())) || 0;
    AtkUp[2] = Number(Zenhan($('#MainContent_AtkUp3').val())) || 0;
    var DefUp = Number(Zenhan($('#MainContent_DefUp').val())) || 0;

    //コネクト補正を加える
    if(typeof connectAjustado[numeroDeConnect][0] !== "undefined")
        AtkUp[numero] += connectAjustado[numeroDeConnect][0];
    if (typeof connectAjustado[numeroDeConnect][6] !== "undefined") {
        //選択されたキャラの名前部分抜き出し
        let charaSelected = [document.getElementById("MainContent_seleccionado_0").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_1").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_2").nextSibling.innerText]
        for (let i = 0; i < 3; i++)
            charaSelected[i] = charaSelected[i].substring(charaSelected[i].indexOf(":") + 2);
        if(charaSelected[numero]==="天音　月咲")
            AtkUp[numero] += connectAjustado[numeroDeConnect][6];
    }
    AtkUp[numero] = (AtkUp[numero] > 200) ? 200 : AtkUp[numero];
    AtkUp[numero] = ((AtkUp[numero] < 5) && (AtkUp[numero] > -5)) ? 0 : AtkUp[numero];
    AtkUp[numero] = (AtkUp[numero] < -100) ? -100 : AtkUp[numero];
    DefUp = (DefUp > 200) ? 200 : DefUp;
    DefUp = ((DefUp < 5) && (DefUp > -5)) ? 0 : DefUp;
    DefUp = (DefUp < -100) ? -100 : DefUp;

    //出力データ作成
    salida[numeroDeConnect].unshift(GetSalida("攻撃力",AtkUp[numero]));

    AtkUp[numero] = 1 + AtkUp[numero] / 100;
    DefUp = 1 + DefUp / 100;

    //UPDOWN補正を加味して最終値出力
    var AtkReal = (Atk[numero] * (1 + atkDespierto) + mAtk[numero]) * AtkUp[numero] * ordenAtk[numero];
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
    //選択されたキャラの名前部分抜き出し
    let charaSelected = [document.getElementById("MainContent_seleccionado_0").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_1").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_2").nextSibling.innerText]
    for (let i = 0; i < 3; i++)
        charaSelected[i] = charaSelected[i].substring(charaSelected[i].indexOf(":") + 2);
    
    //puella無しの場合
    if (puella === "0" && !((charaSelected[0] === charaSelected[1]) && (charaSelected[1] === charaSelected[2]) && (charaSelected[0].indexOf("無")===-1))) {
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
    var chargeFlag = 0;//charge後ダメージUP用のフラグ

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
        case 1:
            {
                if (ratioCori > 0)
                    chargeFlag = 1;
                break;
            }
        case 2:
            {
                if (eleccion[0] !== "C") {
                    calcC = 1;
                    cmp = 1;
                }
                else
                    chargeFlag = 1;
                break;
            }
        case 3:
            {
                if (eleccion[1] !== "C") {
                    calcC = 1;
                    cmp = 1;
                }
                else
                    chargeFlag = 1;
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
    var tipoPuella = $("#MainContent_tipoPuella1").val();
    var calcMpB = 1;
    switch (tipoPuella) {
        case "マギア":
        case "円環マギア":
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
    var MpUpM;
    switch (orden) {
        case 1:
            {
                MpUpM = Number($("#MainContent_MpUp").val());
                break;
            }
        case 2:
            {
                MpUpM = Number($("#MainContent_MpUp2").val());
                break;
            }
        case 3:
            {
                MpUpM = Number($("#MainContent_MpUp3").val());
                break;
            }
    }
    var calcMpC = MpUpM === 0 ? 1 : 1 + (2.5 + 2.5 * MpUpM) / 100;
    //コネクト側
    var MpUpC = connectAjustado[orden - 1][11];
    if (typeof MpUpC === "undefined")
        MpUpC = 0;
    calcMpC += MpUpC / 100;

    //D:AccelMPUP
    //メモリア側 アクセルのみかかる
    var AMpUpM;
    switch (orden) {
        case 1:
            {
                AMpUpM = Number($("#MainContent_AMpUp").val());
                break;
            }
        case 2:
            {
                AMpUpM = Number($("#MainContent_AMpUp2").val());
                break;
            }
        case 3:
            {
                AMpUpM = Number($("#MainContent_AMpUp3").val());
                break;
            }
    } 
    var calcMpD = AMpUpM === 0 ? 1 : 1 + (7.5 + 2.5 * AMpUpM) / 100;
    //コネクト側
    var AMpUpC = connectAjustado[orden - 1][10];
    if (typeof AMpUpC === "undefined")
        AMpUpC = 0;
    calcMpD += AMpUpC / 100;
    calcMpD = eleccion[orden - 1] === "A" ? calcMpD : 1;


    //チャージ数による補正
    //そらまめ氏の指摘により、CとDの計算順を変更し、D処理後、小数点以下2位を切り上げ
    calcMp = Math.ceil(calcMp * calcMpD * 10) / 10;
    calcMp = Math.floor(calcMp * calcMpC * cmp * 10) / 10;

    //MP回復処理
    var aumentoMP = connectAjustado[orden - 1][12];
    if (typeof aumentoMP === "undefined")
        aumentoMP = 0;
    aumentoMP = aumentoMP * calcMpC;

    //出力データ作成
    salida[orden-1].push(GetSalida("MP獲得量",(calcMpC-1) * 100));
    salida[orden-1].push(GetSalida("AcceleMP", (calcMpD-1) * 100));
    let s = ["MP回復量", "+" + String(Math.floor(aumentoMP*10)/10)];
    salida[orden-1].push(s);

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

    //各種補正値の取得
    var t = connectAjustado[orden - 1][2]//Charge後ダメージUP
    if (typeof t === "undefined")
        t = 0;
    t = t > 100 ? 100 : t;
    var u = connectAjustado[orden - 1][1];//与えるダメージUP
    if (typeof u === "undefined")
        u = 0;
    u = u > 100 ? 100 : u;
    var v = connectAjustado[orden - 1][3];//BlastダメージUP
    if (typeof v === "undefined")
        v = 0;
    v = v > 100 ? 100 : v;
    var w = 0;//ダメージアップ状態
    var x = 0;//敵状態異常時ダメージアップ
    var y = 0;//ダメージカット
    var zProbabilidad = connectAjustado[orden - 1][13];//クリティカル確率
    if (typeof zProbabilidad === "undefined")
        zProbabilidad = 0;

    //tとvはディスク構成によるので確認する
    t = chargeFlag === 1 ? t : 0;
    v = eleccion[orden - 1] === "B" ? v : 0;

    //出力データ作成
    salida[orden-1].unshift(GetSalida("Cダメ",t));
    salida[orden-1].unshift(GetSalida("Bダメ", v));
    salida[orden-1].unshift(GetSalida("与ダメ",u));//これで配列の先頭に入る
    
    var calcX = 100 + t + u + v + w + x - y;
    calcX = calcX > 300 ? 300 : calcX;

    calcE = calcX / 100;
    //クリティカルの場合
    z = calcX > 100 ? 100 : calcX;
    var calcEcri = (calcX + z)/100;

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
    if (ventana === "1") {//ミラーズ補正
        calcFinal = (calcA * calcB + calcBsub) * calcSub * calcC * calcD * calcE;
    }
    else
        calcFinal = calcA * calcB * calcSub * calcC * calcD * calcE;


    //取り合えず小数点以下3桁で切り捨てる
    calcFinal = Math.floor(calcFinal * 1000) / 1000;
    return [calcFinal, calcMp + aumentoMP];

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

//コネクト補正効果作成関数
var limite = 30;
var connectAjustado = new Array(3);
function GenerateConnect() {
    //コネクト補正初期化
    let array1 = new Array(limite);
    let array2 = new Array(limite);
    let array3 = new Array(limite);
    connectAjustado = [array1, array2, array3];

    //コネクト位置チェック
    var connectArray = [document.querySelector('label[for="radioConnect1"]').innerText,
                        document.querySelector('label[for="radioConnect2"]').innerText,
                        document.querySelector('label[for="radioConnect3"]').innerText];
    var connectPosition = new Array(3);
    connectPosition[0] = connectArray[0].indexOf("無") === -1 ? true : false;
    connectPosition[1] = connectArray[1].indexOf("無") === -1 ? true : false;
    connectPosition[2] = connectArray[2].indexOf("無") === -1 ? true : false;


    //選択されたキャラの名前部分抜き出し
    var charaSelected = [document.getElementById("MainContent_seleccionado_0").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_1").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_2").nextSibling.innerText]
    for (let i = 0; i < 3; i++)
        charaSelected[i] = charaSelected[i].substring(charaSelected[i].indexOf(":") + 2);

    //ピュエラの場合は、初コネクトが入ってから全て増幅
    //通常の場合は、初コネクトの位置によって場合分け
    let estadoAtk = $("input[name='ctl00$MainContent$estadoAtk']:checked").val();
    switch (estadoAtk) {
        case "0"://通常 キャラの選択状況により場合分け 下記4つの場合
                //登場キャラが1人の場合もある→ピュエラの場合に移行させる
                //登場キャラが2人の場合
                //AAB, ABA, BAA
                //登場キャラが3人の場合
                //ABC        
            {
                //登場キャラの把握
                //ABC コネクトは加算されないため、何も考えずコピー
                if ((charaSelected[0] !== charaSelected[1]) && (charaSelected[1] !== charaSelected[2]) && (charaSelected[2] !== charaSelected[0])) {
                    for (let i = 0; i < 3; i++) {
                        if (connectPosition[i]) {
                            for (let j = 0; j < limite; j++) {
                                connectAjustado[i][j] = connectElegido[i][j];
                            }
                        }
                    }
                    break;
                }
                //登場キャラが2人の場合 AAB,ABA,BAA
                //同キャラの、最初の攻撃でコネクトが入った場合、次の回でコネクト値に前の値の加算が発生 つまり、AABなら1回目のコネクト, ABAも同じ、BAAなら2回目のコネクト
                else {
                    //登場キャラ数把握
                    var chara;
                    if ((charaSelected[0] === charaSelected[1]) && (charaSelected[1] !== charaSelected[2]))
                        chara = "AAB";
                    else if ((charaSelected[0] === charaSelected[2]) && (charaSelected[0] !== charaSelected[1]))
                        chara = "ABA";
                    else if ((charaSelected[1] === charaSelected[2]) && (charaSelected[0] !== charaSelected[1]))
                        chara = "BAA";
                    else
                        chara = "AAA";

                    switch (chara) {
                        case "AAB":
                            {
                                for (let i = 0; i < 3; i++) {
                                    if ((i === 0 || i ===2) && connectPosition[i]) {//一,三回目のコネクト
                                        for (let j = 0; j < limite; j++) {
                                            connectAjustado[i][j] = connectElegido[i][j];
                                        }
                                    }
                                    else if(i===1) {//二回目 加算処理
                                        for (let j = 0; j < limite; j++) {
                                            if (typeof connectAjustado[i - 1][j] !== "undefined" && (j !== 12)) {//MP回復のみ別処理
                                                if (connectAjustado[i - 1] !== true) {
                                                    if (connectPosition[i] && (typeof connectElegido[i][j] !== "undefined"))
                                                        connectAjustado[i][j] = connectAjustado[i - 1][j] + connectElegido[i][j];
                                                    else
                                                        connectAjustado[i][j] = connectAjustado[i - 1][j];
                                                }
                                                //trueを書き換えようがないからここはずっとtrue
                                                else connectAjustado[i][j] = true;
                                            }
                                            else {//前の値がundefinedなので、足す必要が無い
                                                if (connectPosition[i])
                                                    connectAjustado[i][j] = connectElegido[i][j];
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "ABA":
                            {
                                for (let i = 0; i < 3; i++) {
                                    if ((i === 0 || i ===1) && connectPosition[i]) {//一,二回目のコネクト
                                        for (let j = 0; j < limite; j++) {
                                            connectAjustado[i][j] = connectElegido[i][j];
                                        }
                                    }
                                    else if(i===2) {//三回目を一回目に対して加算処理
                                        for (let j = 0; j < limite; j++) {
                                            if (typeof connectAjustado[i - 2][j] !== "undefined" && (j !== 12)) {//MP回復のみ別処理
                                                if (connectAjustado[i - 2] !== true) {
                                                    if (connectPosition[i] && (typeof connectElegido[i][j] !== "undefined"))
                                                        connectAjustado[i][j] = connectAjustado[i - 2][j] + connectElegido[i][j];
                                                    else
                                                        connectAjustado[i][j] = connectAjustado[i - 2][j];
                                                }
                                                //trueを書き換えようがないからここはずっとtrue
                                                else connectAjustado[i][j] = true;
                                            }
                                            else {//前の値がundefinedなので、足す必要が無い
                                                if (connectPosition[i])
                                                    connectAjustado[i][j] = connectElegido[i][j];
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "BAA":
                            {
                                for (let i = 0; i < 3; i++) {
                                    if ((i === 0 || i ===1) && connectPosition[i]) {//一,二回目のコネクト
                                        for (let j = 0; j < limite; j++) {
                                            connectAjustado[i][j] = connectElegido[i][j];
                                        }
                                    }
                                    else if(i===2){//三回目を二回目に対して加算処理
                                        for (let j = 0; j < limite; j++) {
                                            if (typeof connectAjustado[i - 1][j] !== "undefined" && (j !== 12)) {//MP回復のみ別処理
                                                if (connectAjustado[i - 1] !== true) {
                                                    if (connectPosition[i] && (typeof connectElegido[i][j] !== "undefined"))
                                                        connectAjustado[i][j] = connectAjustado[i - 1][j] + connectElegido[i][j];
                                                    else
                                                        connectAjustado[i][j] = connectAjustado[i - 1][j];
                                                }
                                                //trueを書き換えようがないからここはずっとtrue
                                                else connectAjustado[i][j] = true;
                                            }
                                            else {//前の値がundefinedなので、足す必要が無い
                                                if (connectPosition[i])
                                                    connectAjustado[i][j] = connectElegido[i][j];
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "AAA"://ピュエラの場合をコピー
                            {
                                //コネクト位置で場合分け
                                for (let i = 0; i < 3; i++) {
                                    if (i === 0 && connectPosition[i]) {//一回目のコネクト
                                        for (let j = 0; j < limite; j++) {
                                            connectAjustado[i][j] = connectElegido[i][j];
                                        }
                                    }
                                    else if (i !== 0) {//二回目以降は、前のループの値をひたすら足していけばいい
                                        for (let j = 0; j < limite; j++) {
                                            if (typeof connectAjustado[i - 1][j] !== "undefined" && (j !== 12)) {//MP回復のみ別処理
                                                if (connectAjustado[i - 1] !== true) {
                                                    if (connectPosition[i] && (typeof connectElegido[i][j] !== "undefined"))
                                                        connectAjustado[i][j] = connectAjustado[i - 1][j] + connectElegido[i][j];
                                                    else
                                                        connectAjustado[i][j] = connectAjustado[i - 1][j];
                                                }
                                                //trueを書き換えようがないからここはずっとtrue
                                                else connectAjustado[i][j] = true;
                                            }
                                            else {//前の値がundefinedなので、足す必要が無い
                                                if (connectPosition[i])
                                                    connectAjustado[i][j] = connectElegido[i][j];
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
                break;
            }
        case "1"://ピュエラ
            {
                //コネクト位置で場合分け
                for (let i = 0; i < 3; i++) {
                    if (i === 0　&& connectPosition[i]) {//一回目のコネクト
                        for (let j = 0; j < limite; j++) {
                            connectAjustado[i][j] = connectElegido[i][j];
                        }
                    }
                    else if(i !== 0){//二回目以降は、前のループの値をひたすら足していけばいい
                        for (let j = 0; j < limite; j++) {
                            if (typeof connectAjustado[i - 1][j] !== "undefined"  && (j !== 12)) {//MP回復のみ別処理
                                if (connectAjustado[i - 1] !== true) {
                                    if (connectPosition[i] && (typeof connectElegido[i][j] !== "undefined"))
                                        connectAjustado[i][j] = connectAjustado[i - 1][j] + connectElegido[i][j];
                                    else
                                        connectAjustado[i][j] = connectAjustado[i - 1][j];
                                }
                                //trueを書き換えようがないからここはずっとtrue
                                else connectAjustado[i][j] = true;
                            }
                            else {//前の値がundefinedなので、足す必要が無い
                                if (connectPosition[i])
                                    connectAjustado[i][j] = connectElegido[i][j];
                            }
                        }
                    }
                }
                break;
            }
    }
}

//出力データ作成関数
function GetSalida(name, data) {
    data = typeof data === "undefined" ? 0 : data;
    data = Math.floor(data * 10) / 10;

    let s = [name, "+" + String(data) + "%"];
    return s;
}



//表示処理関数
function IndicaResultado() {
    //出力初期化
    let d1 = new Array();
    let d2 = new Array();
    let d3 = new Array();
    let d4 = new Array();
    salida = [d1, d2, d3, d4];
    
    //ラジオボタン選択状態の取得
    var eleccion1 = $("input[name='ctl00$MainContent$RadioButtonList666']:checked").val();
    var eleccion2 = $("input[name='ctl00$MainContent$RadioButtonList667']:checked").val();
    var eleccion3 = $("input[name='ctl00$MainContent$RadioButtonList668']:checked").val();

    //コネクト効果作成
    GenerateConnect();

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

    var estadoAtk = $("input[name='ctl00$MainContent$estadoAtk']:checked").val();
    var dano = [3];
    for (let i = 0; i < 3; i++){
        if (estadoAtk === "1")//ピュエラ
            dano[i] = CalcDano(0,i);
        else//通常
            dano[i] = CalcDano(i,i);
        dano[i] = dano[i] < 500 ? 500 : dano[i];
    }
    // var dano = CalcDano(0);
    // if (dano < 500)
    //     dano = 500;
    //覚醒補正
    var numero = 0;//ピュエラ
    var ajusteDespierto = [0, 0, 0];
    var eleccion = [eleccion1, eleccion2, eleccion3];
    for (let i = 0; i < 3; i++) {
        if (estadoAtk === "0")//通常
            numero = i;
        switch (eleccion[i]) {
            case "A":
                {
                    if (color[numero][3] === 1)
                        ajusteDespierto[i] = valorAjustado[numero][3]/100;
                    break;
                }
            case "B":
                {
                    if (color[numero][5] === 1)
                        ajusteDespierto[i] = valorAjustado[numero][5]/100;
                    break;
                }
            case "C":
                {
                    if (color[numero][4] === 1)
                        ajusteDespierto[i] = valorAjustado[numero][4]/100;
                    break;
                }

        }
    }

    var danoFinal = [calc1[0] * dano[0] * (1 + ajusteDespierto[0]), calc2[0] * dano[1] * (1 + ajusteDespierto[1]), calc3[0] * dano[2] * (1 + ajusteDespierto[2])];

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

    //出力処理
    IndicaSalida(salida[0], "grid1");
    IndicaSalida(salida[1], "grid2");
    IndicaSalida(salida[2], "grid3");
}

function IndicaSalida(data,g) {
    let grid = document.getElementById(g);
    let i = g.substring(4);

    var hot1 = new Handsontable(grid, {//前のテーブルともども消すためのダミー
        data: []
    });
    hot1.destroy();//テーブル削除
    hot1 = new Handsontable(grid, {//本番のテーブル
        data: [],
        rowHeaders: false,
        colHeaders: ['補正値' + i, '値' + i],
        columnSorting: false,
        readOnly: true,
        currentRowClassName: 'currentRow',
        currentColClassName: 'currentCol',        
        className: "htCenter",
        licenseKey: 'non-commercial-and-evaluation'
    });
    

    hot1.loadData(data);
    
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
        
        var e0 = document.getElementById("MainContent_seleccionado_0");
        var e1 = document.getElementById("MainContent_seleccionado_1");
        var e2 = document.getElementById("MainContent_seleccionado_2");
        if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
            $('input:radio[name="ctl00$MainContent$seleccionado"]').val(["1"]);
            //ピュエラの時は、2人目以降の選択不可
            e0.disabled = false;
            e1.disabled = true;
            e2.disabled = true;
            //名前コピー
            var nombre = document.getElementById("MainContent_seleccionado_0").nextSibling.innerText;
            if (nombre === "1人目選択 : 無")
                nombre = "";
            else {
                nombre = nombre.substring(nombre.indexOf(" : ") + 3);
                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目選択 : " + nombre;
                document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目選択 : " + nombre;
            }
            //アコーデオン閉じる
            $("#collapse2").collapse('hide');
            $("#collapseConnect").collapse('hide');

            //ハイライト解除
            document.getElementById("MainContent_seleccionado_0").nextSibling.classList.remove("highlight");
            document.getElementById("MainContent_seleccionado_1").nextSibling.classList.remove("highlight");
            document.getElementById("MainContent_seleccionado_2").nextSibling.classList.remove("highlight");
        }
        else {
            e0.disabled = false;
            e1.disabled = false;
            e2.disabled = false;
            //名前を下から拾う処理
            var n2 = document.getElementById("MainContent_nombre2").innerText;
            var n3 = document.getElementById("MainContent_nombre3").innerText;
            //n2,n3に値があれば入れる
            if (n2.substring(n2.indexOf(" : ") + 3) === "選択無") {
                n2 = "無";
            }
            else
                n2 = n2.substring(n2.indexOf(" : ") + 3);
            if (n3.substring(n3.indexOf(" : ") + 3) === "選択無"){
                n3 = "無";
            }
            else
                n3 = n3.substring(n3.indexOf(" : ") + 3);
            document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目選択 : " + n2;
            document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目選択 : " + n3;

            //アコーデオン開く
            $("#collapse2").collapse('show');
            $("#collapseConnect").collapse('show');

            //ハイライト処理
            document.getElementById("MainContent_seleccionado_0").nextSibling.classList.add("highlight");
        }
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$seleccionado"]:radio').change(function () {
        //ハイライト解除
        document.getElementById("MainContent_seleccionado_0").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_seleccionado_1").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_seleccionado_2").nextSibling.classList.remove("highlight");
        //付加
        let selected = $('input[name="ctl00$MainContent$seleccionado"]:checked').val();
        switch (selected) {
            case "1":
                {
                    document.getElementById("MainContent_seleccionado_0").nextSibling.classList.add("highlight");
                    break;
                }
            case "2":
                {
                    document.getElementById("MainContent_seleccionado_1").nextSibling.classList.add("highlight");
                    break;
                }
            case "3":
                {
                    document.getElementById("MainContent_seleccionado_2").nextSibling.classList.add("highlight");
                    break;
                }

        }
    })
    $("#MainContent_tipoPuella1").change(function () {
        IndicaResultado();
    })
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
    $('input[name="ctl00$MainContent$ordendeBatalla4"]:radio').change(function () {
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
    if (str === "攻撃設定 ")
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
    draw(0);
    draw(1);
    draw(2);
    draw2();//防衛側の精神強化
    draw3();
});
var color = [[0, 0, 0, 0, 0, 0],[0, 0, 0, 0, 0, 0],[0, 0, 0, 0, 0, 0]];
var color2 = [0, 0, 0, 0, 0, 0];
var valorAjustado = [[0, 0, 0, 0, 0, 0],[0, 0, 0, 0, 0, 0],[0, 0, 0, 0, 0, 0],[0, 0, 0, 0, 0, 0]];//覚醒補正値
var valorAjustado2 = [0, 0, 0, 0, 0, 0];//覚醒補正値2
var letra = ["DEF", "ATK", "HP", "A", "C", "B"];
var r = 20;
var scaleF = 0.6;
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
//on offボタン用
var checkOnoff = [0,0,0];

function draw(numero) {
    var canvas;
    switch (numero) {
        case 0:
            {
                canvas = document.getElementById("canvas1");
                break;
            }
        case 1:
            {
                canvas = document.getElementById("canvas12");
                break;
            }
        case 2:
            {
                canvas = document.getElementById("canvas13");
                break;
            }
    }
    // var canvas = document.getElementById("canvas1");
    // var scaleF = 0.6;
    if (!canvas || !canvas.getContext) {
        return;
    }

    //設定
    var ctx = canvas.getContext("2d");
    
    
    
    // const c1 = {
    //     x: 40 * scaleF,
    //     y: 80 * scaleF,
    // };
    // const c2 = {
    //     x: 120 * scaleF,
    //     y: 40 * scaleF,
    // };
    // const c3 = {
    //     x: 200 * scaleF,
    //     y: 80 * scaleF,
    // };
    // const c4 = {
    //     x: 40 * scaleF,
    //     y: 160 * scaleF,
    // };
    // const c5 = {
    //     x: 120 * scaleF,
    //     y: 200 * scaleF,
    // };
    // const c6 = {
    //     x: 200 * scaleF,
    //     y: 160 * scaleF,
    // };

    // var circle = [c1, c2, c3, c4, c5, c6];

    //描画
    for (let i = 0; i < 6; i++) {
        ctx.save();
        ctx.beginPath();
        if (color[numero][i] === 1)
            ctx.fillStyle = "red";
        else
            ctx.fillStyle = "whitesmoke";
        ctx.arc(circle[i].x, circle[i].y, r, 0, 2 * Math.PI);
        ctx.fill();
        ctx.stroke();
        ctx.restore();

        ctx.save();
        ctx.font = '15px sans-serif';
        if (color[numero][i] === 1)
            ctx.fillStyle = "whitesmoke";
        else
            ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'bottom';
        ctx.fillText(letra[i], circle[i].x, circle[i].y);
        ctx.textBaseline = 'top';
        ctx.fillText(String(valorAjustado[numero][i]) + "%", circle[i].x, circle[i].y);
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
                color[numero][i] = CheckHit(color[numero][i], circle[i], letra[i], valorAjustado[numero][i], r, ctx, true);
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
            DrawObject(ctx, color[numero],circle, r, letra, checkOnoff[numero],valorAjustado[numero]);
            ctx.beginPath();
            //クリック地点を中心とする円
            ctx.save();
            ctx.globalCompositeOperation = "source-atop";//交差部分のみ着色
            var radialGrad = ctx.createRadialGradient(point.x, point.y, 1, point.x, point.y, variantR);
            if (checkOnoff[numero] === 0) {
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
            LetraEscrita(ctx, color[numero], circle, r, letra, checkOnoff[numero]);
            variantR += 3;
            //終了条件
            if (variantR > canvas.width * 2 / 3) {
                ctx.clearRect(0, 0, canvas.width, canvas.height, 2 * r, 2 * r);
                clearInterval(timer);

                if (hitOn === true) {
                    switch (checkOnoff[numero]) {
                        case 0:
                            {
                                //全部on
                                for (let i = 0; i < 6; i++) {
                                    color[numero][i] = CheckHit(0, circle[i], letra[i], valorAjustado[numero][i], r, ctx, true);
                                }
                                checkOnoff[numero] = 1;

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
                                    color[numero][i] = CheckHit(1, circle[i], letra[i], valorAjustado[numero][i], r, ctx, true);
                                }
                                checkOnoff[numero] = 0;

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

//checkOnoff :0 全off :1 全on :3 offボタンで現在の外周円 :4 onボタンで現在の外周円
function DrawObject(ctx, color,circle, r, letra, checkOnoff,valorAjustado) {
    //中心描画
    switch (checkOnoff) {
        case 1:
            {
                //全部on状態の描画
                for (let i = 0; i < 6; i++) {
                    CheckHit(0, circle[i], letra[i], valorAjustado[i], r, ctx, false);
                }
            
                ctx.save();
                ctx.beginPath();
                ctx.fillStyle = "red";
                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                ctx.fill();
                ctx.stroke();
                ctx.restore();

                ctx.save();
                ctx.font = '15px sans-serif';
                ctx.fillStyle = "whitesmoke";
                ctx.textAlign = "center";
                ctx.textBaseline = 'middle';
                ctx.fillText("OFF", circle[1].x, (circle[4].y + circle[1].y) / 2);
                ctx.restore();
                break;
            }
        case 3:
            {
                //現在の状態の描画
                // for (let i = 0; i < 6; i++) {
                //     CheckHit(color, circle[i], letra[i], valorAjustado[i], r, ctx, false);
                // }
            
                ctx.save();
                ctx.beginPath();
                ctx.fillStyle = "red";
                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                ctx.fill();
                ctx.stroke();
                ctx.restore();

                ctx.save();
                ctx.font = '15px sans-serif';
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
                    CheckHit(1, circle[i], letra[i], valorAjustado[i], r, ctx, false);
                }
             
                ctx.save();
                ctx.beginPath();
                ctx.fillStyle = "whitesmoke";
                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                ctx.fill();
                ctx.stroke();
                ctx.restore();

                ctx.save();
                ctx.font = '15px sans-serif';
                ctx.fillStyle = "red";
                ctx.textAlign = "center";
                ctx.textBaseline = 'middle';
                ctx.fillText("ON", circle[1].x, (circle[4].y + circle[1].y) / 2);
                ctx.restore();
                break;
            }
        case 2:
            {
                //現在の状態の描画
                // for (let i = 0; i < 6; i++) {
                //     CheckHit(color[i], circle[i], letra[i], valorAjustado[i], r, ctx, false);
                // }

                ctx.save();
                ctx.beginPath();
                ctx.fillStyle = "whitesmoke";
                ctx.arc(circle[1].x, (circle[4].y + circle[1].y) / 2, r * 1.2, 0, 2 * Math.PI);
                ctx.fill();
                ctx.stroke();
                ctx.restore();

                ctx.save();
                ctx.font = '15px sans-serif';
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
function CheckHit(color, circle, letra, valorAjustado, r, ctx, returnFlag) {
    // alert("clicked");
    if (color === 0) {
        ctx.save();
        ctx.beginPath();
        ctx.fillStyle = "red";
        ctx.arc(circle.x, circle.y, r, 0, 2 * Math.PI);
        ctx.fill();
        ctx.stroke();
        ctx.font = '15px sans-serif';
        ctx.fillStyle = "whitesmoke";
        ctx.textAlign = "center";
        ctx.textBaseline = 'bottom';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.textBaseline = 'top';
        ctx.fillText(String(valorAjustado) + "%", circle.x, circle.y);
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
        ctx.stroke();
        ctx.font = '15px sans-serif';
        ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'bottom';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.textBaseline = 'top';
        ctx.fillText(String(valorAjustado) + "%", circle.x, circle.y);
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
function CheckHit2(color, circle, letra, valorAjustado, r, ctx) {
    // alert("clicked");
    if (color === 0) {
        ctx.save();
        ctx.font = '15px sans-serif';
        ctx.fillStyle = "whitesmoke";
        ctx.textAlign = "center";
        ctx.textBaseline = 'bottom';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.textBaseline = 'top';
        ctx.fillText(String(valorAjustado) + "%", circle.x, circle.y);
        ctx.restore();
    }
    else if (color === 1) {
        ctx.save();
        ctx.font = '15px sans-serif';
        ctx.fillStyle = "red";
        ctx.textAlign = "center";
        ctx.textBaseline = 'bottom';
        ctx.fillText(letra, circle.x, circle.y);
        ctx.textBaseline = 'top';
        ctx.fillText(String(valorAjustado) + "%", circle.x, circle.y);
        ctx.restore();
    }
}

function draw2() {
    var canvas = document.getElementById("canvas2");
    // canvas.id = "canvas1";
    // var scaleF = 0.6;
    // canvas.width = 320;
    // canvas.height = 300;
    // document.body.appendChild(canvas);
    if (!canvas || !canvas.getContext) {
        return;
    }

    //設定
    var ctx = canvas.getContext("2d");

    // const c1 = {
    //     x: 40 * scaleF,
    //     y: 80 * scaleF,
    // };
    // const c2 = {
    //     x: 120 * scaleF,
    //     y: 40 * scaleF,
    // };
    // const c3 = {
    //     x: 200 * scaleF,
    //     y: 80 * scaleF,
    // };
    // const c4 = {
    //     x: 40 * scaleF,
    //     y: 160 * scaleF,
    // };
    // const c5 = {
    //     x: 120 * scaleF,
    //     y: 200 * scaleF,
    // };
    // const c6 = {
    //     x: 200 * scaleF,
    //     y: 160 * scaleF,
    // };

    // var circle = [c1, c2, c3, c4, c5, c6];

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
        ctx.textBaseline = 'bottom';
        ctx.fillText(letra[i], circle[i].x, circle[i].y);
        ctx.textBaseline = 'top';
        ctx.fillText(String(valorAjustado[3][i]) + "%", circle[i].x, circle[i].y);
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
                color2[i] = CheckHit(color2[i], circle[i], letra[i], valorAjustado[3][i], r, ctx, true);
            }
        }
        IndicaResultado();
    });
}

////////////////////////////
//キャラ選択画面用描画関数
////////////////////////////
var elegida = [-1,-1,-1,-1,-1,-1];
var personas;
var jsonDataOri;
var jsonData;
var canvasFlag = 0;

var array01 = new Array(limite);
var array02 = new Array(limite);
var array03 = new Array(limite);
var connectElegido = [array01,array02,array03];
//ダメージ計算に関係ある系
// 0:攻撃力UP
// 1:与えるダメージUP
// 2:Charge後ダメージUP
// 3:BlastダメージUP
// 4:敵状態異常時ダメージUP
// 5:防御無視
// 6:月咲にコネクトで攻撃力UP
//
//ダメージ計算に関係無い系
// 10:AccelMPUP
// 11:MP獲得量UP
// 12:MP回復
// 13:クリティカル


// var canvasAry = [];
// var canvasAryOn = [];
function draw3() {
    var canvas3;
    var w = $(window).width();
    var wSize = 768;
    

    if (w < wSize) {
        //画面サイズが768px未満のときの処理
        canvas3 = document.getElementById("canvas3");
        canvasFlag = 1;
    }
    else {
        canvas3 = document.getElementById("canvas31");
        canvasFlag = 1;
    }
    if (!canvas3 || !canvas3.getContext) {
        return;
    }

    //設定
    var ctx3 = canvas3.getContext("2d");
    // var personas ;
    
    //JSONGET処理
    // $.getJSON("Scripts/magia_json/magia.json", function (data) {
        
    //     personas = data["personas"].length;
    //     jsonData = data;
    // });
    // var jsonData;
    
    var xhr = new XMLHttpRequest;
    (function (handleload) {
        // var xhr = new XMLHttpRequest;

        xhr.addEventListener('load', handleload, false);
        xhr.open('GET', 'Scripts/magia_json/magia.json', false);//同期処理。
        xhr.send(null);
        //xhr.send();
    }(function handleLoad(event) {
        var xhr = event.target,
            obj = JSON.parse(xhr.responseText);
        
        personas = obj.personas.length;
        console.log(obj);
        jsonDataOri = obj;
        jsonData = angular.copy(jsonDataOri);
    }));

    var scaleF = 1;
    var charaR = 30;
    var x0 = charaR;
    var charaXori = [personas];
    var charaY = 35;
    var offset = 2 * charaR + 10;
    var nombre = [personas];
    var indiceCambia = [personas];
    
    var Xoffset = canvas3.width;
    var Yoffset = charaY * 2;
    
    for (let i = 0; i < personas; i++) {
        charaXori[i] = i === 0 ? x0 : charaXori[i - 1] + offset;
        nombre[i] = jsonData.personas[i].name.substring(jsonData.personas[i].name.indexOf("　") + 1);
    }
    var charaX = angular.copy(charaXori);

    //プリロード処理
    // personas = 3;
    // for (let i = 0; i < personas; i++) {
    //     let loadCount = 0;
    //     var m_canvas = document.createElement("canvas");
    //     canvasAry.push(m_canvas);
    //     let img = new Image();
    //     img.src = "png/" + jsonData.personas[i].data1;
    //     // img.onload = function () {
    //     //    loadCount++;
    //     // };
    //     // $("img").one("load", function() {
    //     //     //do stuff
    //     //  }).each(function() {
    //     //     if(this.complete || /*for IE 10-*/ $(this).height() > 0)
    //     //       $(this).load();
    //     //  });
    //     // $("img").one('load', function(){
    //     //     alert('読み込みました。');
    //     //     loadCount++;
    //     //   }).load();
    //     // img.addEventListener('load', function () {
    //     //     loadCount++;
    //     // });
    //     (function(){
    //         if(!img.naturalWidth){ //naturalWidthがセットされていなければ
    //             setTimeout(arguments.callee); //次のフレームで再度チェック
    //             return; //以下の処理を停止
    //         }
        
    //         //naturalWidthがセットされていれば以下の処理を実行
    //         alert(img.naturalWidth + '/' + img.naturalHeight);
    //         loadCount++;
    //     })();
    //     var m_canvas1 = document.createElement("canvas");
    //     canvasAryOn.push(m_canvas1);
    //     let img1 = new Image();
    //     img1.src = "png/" + jsonData.personas[i].data2;
    //     // + "?_=" + (new Date().getTime())
    //     img1.onload = function () {
    //        loadCount++;
    //     };
    //     var count = 0;
    //     // while (loadCount !== 1 || (img.naturalHeight>0)) {
    //     //     //load待ち
           
    //     //     console.log(img.complete);
    //     // }
    //     var m_ctx = m_canvas.getContext("2d");
    //     m_canvas.width = 2 * charaR;
    //     m_canvas.height = 2 * charaR;
    //     m_ctx.drawImage = (img, 0, 0, charaR * 2, charaR * 2);
    //     var m_ctx1 = m_canvas1.getContext("2d");
    //     m_canvas1.width = 2 * charaR;
    //     m_canvas1.height = 2 * charaR;
    //     m_ctx1.drawImage = (img1, 0, 0, charaR * 2, charaR * 2);
    // }

    // function LoadImage(src) {
    //     return new Promise((resolve, reject) => {
    //       const img = new Image();
    //       img.onload = () => resolve(img);
    //       img.onerror = (e) => reject(e);
    //       img.src = src;
    //     });
    // }

    // // personas = 3;
    // async function Prerender() {
    //     // let loadCount = 0;
    //     for (let i = 0; i < personas; i++) {
    //         var m_canvas = document.createElement("canvas");
    //         canvasAry.push(m_canvas);

    //         let img = await LoadImage("png/" + jsonData.personas[i].data1);
    //         // let img = new Image();
    //         // img.src = "png/" + jsonData.personas[i].data1;
    //         // img.onload = function () {
    //             var m_ctx = m_canvas.getContext("2d");
    //             m_canvas.width = 2 * charaR;
    //             m_canvas.height = 2 * charaR;
    //             m_ctx.drawImage = (img, 0, 0, charaR * 2, charaR * 2);
               
    //         // };

    //         // const result = await myPromise(canvasAry,jsonData.personas[i].data1);
            
    //         // const result1 = await myPromise(canvasAryOn,jsonData.personas[i].data2);

    //         var m_canvas1 = document.createElement("canvas");
    //         canvasAryOn.push(m_canvas1);

    //         let img1 = await LoadImage("png/" + jsonData.personas[i].data2);

    //         // let img1 = new Image();
    //         // img1.src = "png/" + jsonData.personas[i].data2;
    //         // img1.onload = function () {
    //             var m_ctx1 = m_canvas1.getContext("2d");
    //             m_canvas1.width = 2 * charaR;
    //             m_canvas1.height = 2 * charaR;
    //             m_ctx1.drawImage = (img1, 0, 0, charaR * 2, charaR * 2);
    //         // };
    //         console.log("完了 " + img.src);
    //     }
    // }

    // Prerender();

    //あいうえお順ソート
    jsonData.personas.sort(function (a, b) {
        if (a.name > b.name) {
            return 1;
        } else {
            return -1;
        }
    })

    //imageのみ準備
    var imageAry1 = [personas];
    for (let i = 0; i < personas; i++) {
        imageAry1[i] = new Image();
        imageAry1[i].src = "png/" + jsonData.personas[i].data1;
    }
    var imageAry2 = [personas];
    for (let i = 0; i < personas; i++) {
        imageAry2[i] = new Image();
        imageAry2[i].src = "png/" + jsonData.personas[i].data2;
    }

    //キャラ数表示
    document.getElementById("MainContent_indicaPersonas").innerText = "登録 " + jsonDataOri.personas.length + "キャラ表示";

    for (let i = 0; i < personas; i++) {
        if (jsonData.personas[i].data1 !== "") {
            // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry1[i]);
            Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry1[i], Xoffset, Yoffset, canvasFlag);
            // ctx3.drawImage(canvasAry[i], charaX[i] - charaR, charaY - charaR);
        }
        else {
            DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
            DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
        }
    }

    console.log(Date() + " event end");
    var pointerFlag = false;
    // console.log(pointerFlag + "0");

    //向き判定
    var direction;

    var number;
    var clickX;
    var clickY;

    var isStatic = 0;

    canvas3.addEventListener("pointerdown", function (e) {
        isStatic = 1;
        //エリア内ならフラグon
        //ポインタがcanvas内にあるか判定
        const rect = canvas3.getBoundingClientRect();
        clickX = e.clientX - rect.left;
        clickY = e.clientY - rect.top;
        if ((clickX < canvas3.width) && (clickY < canvas3.height)) {
            pointerFlag = true;
            // console.log(pointerFlag + " down");
            $("#MainContent_debug").text(pointerFlag + " pointerOn");
        }
        
        number = -1;
        //ポインタが円内にあるか判定
        for (let i = 0; i < personas; i++) {
            if ((clickX - charaX[i]) ** 2 + (clickY - charaY) ** 2 < charaR ** 2) {
                number = i;
                break;
            }
            if (number === -1) {
                if ((clickX - charaX[i] + Xoffset) ** 2 + (clickY - charaY - Yoffset) ** 2 < charaR ** 2) {
                    number = i;
                    break;
                }
            }
        }
    });
    //ポインタ乗ったらドラッグ処理
    canvas3.addEventListener('pointermove', function (e) {
        if (!pointerFlag) {
            // console.log(pointerFlag + " move");
            return;
        }
        // e.preventDefault();
        // e.stopPropagation();
        //オフセット位置取得
        const rect = canvas3.getBoundingClientRect();
        var px = e.clientX - rect.left - clickX;
        var py = e.clientY - rect.top - clickY;
        clickX = e.clientX - rect.left;
        clickY = e.clientY - rect.top;

        //canvas外は受け付けない
        if ((clickX > canvas3.width) || (clickX < 0) || (clickY > canvas3.height) || (clickY < 0)) {
            pointerFlag = false;
            // console.log(pointerFlag + " limit");
            $("#MainContent_debug").text(pointerFlag + " canvas外");
            return;
        }

        //両端の移動制限
        //左端
        if ((px > 0) && (charaX[0] >= x0)) {
            if (clickX > canvas3.width) {
                pointerFlag = false;
                // console.log(pointerFlag + " left");
                $("#MainContent_debug").text(pointerFlag + " 左端");
            }
            return;
        }
        //右端
        if ((px < 0) && (charaX[personas - 1] <= canvas3.width * 2 - (x0))) {
            if (clickX < 0) {
                pointerFlag = false;
                // console.log(pointerFlag + " right");
                $("#MainContent_debug").text(pointerFlag + " 右端");
            }
            return;
        }
        console.log("px " + px);
        //pxが少ない場合は色変え
        if (Math.abs(px) > 8)
            isStatic = 0;
        //描画メイン処理　ドラッグ
        var numero = $('input[name="ctl00$MainContent$seleccionado"]:checked').val() - 1;
        if (pointerFlag) {
            ctx3.clearRect(0, 0, canvas3.width, canvas3.height);
            for (let i = 0; i < personas; i++) {
                charaX[i] += px;
                
                if (elegida[numero] === i) {//色変え
                    if (jsonData.personas[i].data1 !== "") {
                        // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry2[i]);
                        //Draw2ndRow
                        Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry2[i], Xoffset, Yoffset, canvasFlag);
                    }
                    else {
                        DrawCircle(ctx3, charaX[i], charaY, charaR, -1);
                        DrawText(ctx3, charaX[i], charaY, nombre[i], -1);
                    }
                }
                else {//通常色
                    if (jsonData.personas[i].data1 !== "") {
                        // if(Math.abs(px) > 1)
                        // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry1[i]);
                        Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry1[i], Xoffset, Yoffset, canvasFlag);
                    }
                    else {
                        DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
                        DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
                    }
                }
                //sort時値表示
                var tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
                switch (tipoOrden) {
                    case "ATK":
                        {
                            // DrawImageText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].ATK);
                            Draw2ndText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].ATK, Xoffset, Yoffset, canvasFlag);
                            break;
                        }
                    case "DEF":
                        {
                            // DrawImageText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].DEF);
                            Draw2ndText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].DEF, Xoffset, Yoffset, canvasFlag);
                            break;
                        }
                    case "HP":
                        {
                            // DrawImageText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].HP);
                            Draw2ndText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].HP, Xoffset, Yoffset, canvasFlag);
                            break;
                        }
                    case "マギア値":
                    case "コネクト値":
                        {
                            Draw2ndText(ctx3, charaX[i], charaY, charaR, jsonData.personas[i].sortValue, Xoffset, Yoffset, canvasFlag);
                            break;
                        }
                }
            }
            direction = px;
            console.log(px);
            $("#MainContent_debug").text(pointerFlag + " drag描画処理中");
        }
    });



    //ポインタ離れたらドラッグ終了
    canvas3.addEventListener("pointerup", function (e) {
        pointerFlag = false;
        // console.log(pointerFlag + " UP");
        $("#MainContent_debug").text(pointerFlag + " pointerUp");

        //描画処理
        var numero = $('input[name="ctl00$MainContent$seleccionado"]:checked').val() - 1;
        if (isStatic === 1 && number !== -1) {
            console.log("elegida " + elegida[numero] + " number" + number);
            switch (elegida[numero]) {
                case number:
                    {//同色の場合は色を初期に戻す
                        //同じ番号の場合
                        //色を戻す処理
                        elegida[numero] = -1;
                        console.log(elegida[numero] + " 同番の場合色戻し");
                        ctx3.clearRect(0, 0, canvas3.width, canvas3.height);
                        for (let i = 0; i < personas; i++) {
                            //全て通常色
                            if (jsonData.personas[i].data1 !== "") {
                                // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry1[i]);
                                Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry1[i], Xoffset, Yoffset, canvasFlag);
                            }
                            else {
                                DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
                                DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
                            }
                        }

                        
                        //キャラ名表示
                        if (document.getElementById("MainContent_seleccionado_0").checked) {
                            document.getElementById("MainContent_seleccionado_0").nextSibling.innerText = "1人目選択 : 無";
                            document.getElementById("MainContent_nombre1").innerText = "攻撃側1人目 : 選択無";
                            if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
                                //ピュエラコンボの場合
                                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目選択 : 無";
                                document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目選択 : 無";
                            }

                        }
                        else if (document.getElementById("MainContent_seleccionado_1").checked) {
                            document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目選択 : 無";
                            document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : 選択無";
                        }
                        else if (document.getElementById("MainContent_seleccionado_2").checked) {
                            obj = document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目選択 : 無";
                            document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : 選択無";
                        }

                        //コネクト部分
                        else if (document.getElementById("radioConnect1").checked) {
                            document.querySelector('label[for="radioConnect1"]').innerText = "1回目 : 無";
                            //要素クリア
                            connectElegido[0].length = 0;
                            let array = new Array(limite);
                            connectElegido[0] = array;
                            break;
                        }
                        else if (document.getElementById("radioConnect2").checked) {
                            document.querySelector('label[for="radioConnect2"]').innerText = "2回目 : 無";
                            //要素クリア
                            connectElegido[1].length = 0;
                            let array = new Array(limite);
                            connectElegido[1] = array;
                            break;
                        }
                        else if (document.getElementById("radioConnect3").checked) {
                            document.querySelector('label[for="radioConnect3"]').innerText = "3回目 : 無";
                            //要素クリア
                            connectElegido[2].length = 0;
                            let array = new Array(limite);
                            connectElegido[2] = array;
                            break;
                        }
                        //覚醒補正部分の表示
                        var canvas1;
                        switch (numero) {
                            case 0:
                                {
                                    canvas1 = document.getElementById("canvas1");
                                    break;
                                }
                            case 1:
                                {
                                    canvas1 = document.getElementById("canvas12");
                                    break;
                                }
                            case 2:
                                {
                                    canvas1 = document.getElementById("canvas13");
                                    break;
                                }
                        }
                        if (!canvas1 || !canvas1.getContext) {
                            return;
                        }
                        var ctx1 = canvas1.getContext("2d");
                        ctx1.clearRect(0, 0, canvas1.width, canvas1.height);
                        
                        //データ側変更
                        for (let i = 0; i < 6; i++) {
                            valorAjustado[numero][i] = 0;
                        }
                        
                        for (let i = 0; i < 6; i++) {
                            //colorの仕様により、色変更処理
                            color[numero][i] = color[numero][i] === 0 ? 1 : 0;
                            CheckHit(color[numero][i], circle[i], letra[i], valorAjustado[numero][i], r, ctx1, false);
                            color[numero][i] = color[numero][i] === 0 ? 1 : 0;
                        }
                        DrawObject(ctx1, color[numero], circle, r, letra, checkOnoff[numero] + 2, valorAjustado[numero]);
                        break;
                    }
                default://他番号の場合
                    {
                        ctx3.clearRect(0, 0, canvas3.width, canvas3.height);
                        for (let i = 0; i < personas; i++) {
                            if (i === number) {
                                //新しく選択されたものの色を変える
                                if (jsonData.personas[i].data1 !== "") {
                                    // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry2[i]);
                                    Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry2[i], Xoffset, Yoffset, canvasFlag);
                                }
                                else {
                                    DrawCircle(ctx3, charaX[i], charaY, charaR, -1);
                                    DrawText(ctx3, charaX[i], charaY, nombre[i], -1);
                                }
                            }
                            else {
                                //全て通常色
                                if (jsonData.personas[i].data1 !== "") {
                                    // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry1[i]);
                                    Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry1[i], Xoffset, Yoffset, canvasFlag);
                                }
                                else {
                                    DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
                                    DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
                                }
                            }
                        }
                        elegida[numero] = number;
                        console.log(elegida[numero] + " 他番の場合色変え");
                        //キャラ名表示
                        // $("#MainContent_seleccionado1").text("選択キャラ : " + jsonData.personas[elegida].name);
                        if (document.getElementById("MainContent_seleccionado_0").checked) {
                            document.getElementById("MainContent_seleccionado_0").nextSibling.innerText = "1人目選択 : " + jsonData.personas[elegida[0]].name;
                            document.getElementById("MainContent_nombre1").innerText = "攻撃側1人目 : " + jsonData.personas[elegida[0]].name;
                            if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
                                //ピュエラコンボの場合
                                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目選択 : " + jsonData.personas[elegida[0]].name;
                                document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目選択 : " + jsonData.personas[elegida[0]].name;
                            }
                            for (let i = 0; i < 6; i++) {
                                valorAjustado[0][i] = jsonData.personas[elegida[0]].Despierta[i];
                            }
                            //ATK数値記入
                            $('#MainContent_TextBox666').val(jsonData.personas[number].ATK);
                            //魔法少女タイプ入力
                            if (jsonData.personas[number].TipoMagia === "円環マギア")
                                $("#MainContent_tipoPuella1").val("マギア");
                            else if (jsonData.personas[number].TipoMagia === "円環サポート")
                                $("#MainContent_tipoPuella1").val("サポート");
                            else
                                $("#MainContent_tipoPuella1").val(jsonData.personas[number].TipoMagia);
                        }
                        //2人目3人目の処理追加(通常の場合)
                        else if (document.getElementById("MainContent_seleccionado_1").checked) {
                            document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目選択 : " + jsonData.personas[elegida[1]].name;
                            document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : " + jsonData.personas[elegida[1]].name;
                            for (let i = 0; i < 6; i++) {
                                valorAjustado[1][i] = jsonData.personas[elegida[1]].Despierta[i];
                            }
                            //ATK数値記入
                            $('#MainContent_TextBox666_2').val(jsonData.personas[number].ATK);
                            //魔法少女タイプ入力
                            if (jsonData.personas[number].TipoMagia === "円環マギア")
                                $("#MainContent_tipoPuella2").val("マギア");
                            else if (jsonData.personas[number].TipoMagia === "円環サポート")
                                $("#MainContent_tipoPuella2").val("サポート");
                            else
                                $("#MainContent_tipoPuella2").val(jsonData.personas[number].TipoMagia);
                        }
                        else if (document.getElementById("MainContent_seleccionado_2").checked) {
                            obj = document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目選択 : " + jsonData.personas[elegida[2]].name;
                            document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : " + jsonData.personas[elegida[2]].name;
                            for (let i = 0; i < 6; i++) {
                                valorAjustado[2][i] = jsonData.personas[elegida[2]].Despierta[i];
                            }
                            //ATK数値記入
                            $('#MainContent_TextBox666_3').val(jsonData.personas[number].ATK);
                            //魔法少女タイプ入力
                            if (jsonData.personas[number].TipoMagia === "円環マギア")
                                $("#MainContent_tipoPuella3").val("マギア");
                            else if (jsonData.personas[number].TipoMagia === "円環サポート")
                                $("#MainContent_tipoPuella3").val("サポート");
                            else
                                $("#MainContent_tipoPuella3").val(jsonData.personas[number].TipoMagia);
                        }
                            
                        //コネクト処理
                        else if (document.getElementById("radioConnect1").checked) {
                            document.querySelector('label[for="radioConnect1"]').innerText = "1回目 : " + jsonData.personas[number].name;
                            //要素クリア
                            connectElegido[0].length = 0;
                            let array = new Array(limite);
                            connectElegido[0] = array;
                            GetConnect(jsonData.personas[number], 0);
                            break;
                        }
                        else if (document.getElementById("radioConnect2").checked) {
                            document.querySelector('label[for="radioConnect2"]').innerText = "2回目 : " + jsonData.personas[number].name;
                            //要素クリア
                            connectElegido[1].length = 0;
                            let array = new Array(limite);
                            connectElegido[1] = array;
                            GetConnect(jsonData.personas[number], 1);
                            break;
                        }
                        else if (document.getElementById("radioConnect3").checked) {
                            document.querySelector('label[for="radioConnect3"]').innerText = "3回目 : " + jsonData.personas[number].name;
                            //要素クリア
                            connectElegido[2].length = 0;
                            let array = new Array(limite);
                            connectElegido[2] = array;
                            GetConnect(jsonData.personas[number], 2);
                            break;
                        }
    
                        var canvas1;
                        switch (numero) {
                            case 0:
                                {
                                    canvas1 = document.getElementById("canvas1");
                                    break;
                                }
                            case 1:
                                {
                                    canvas1 = document.getElementById("canvas12");
                                    break;
                                }
                            case 2:
                                {
                                    canvas1 = document.getElementById("canvas13");
                                    break;
                                }
                        }
                        if (!canvas1 || !canvas1.getContext) {
                            return;
                        }
                        var ctx1 = canvas1.getContext("2d");
                        ctx1.clearRect(0, 0, canvas1.width, canvas1.height);
                        for (let i = 0; i < 6; i++) {
                            //colorの仕様により、色変更処理
                            color[numero][i] = color[numero][i] === 0 ? 1 : 0;
                            CheckHit(color[numero][i], circle[i], letra[i], valorAjustado[numero][i], r, ctx1, false);
                            color[numero][i] = color[numero][i] === 0 ? 1 : 0;
                        }
                        DrawObject(ctx1, color[numero], circle, r, letra, checkOnoff[numero] + 2, valorAjustado[numero]);
                        break;
                    }
            }
            //sort時値表示
            var tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
            var ordenLetra = "";
            
            for (let i = 0; i < personas; i++) {
                switch (tipoOrden) {
                    case "ATK":
                        {
                            ordenLetra = jsonData.personas[i].ATK;
                            break;
                        }
                    case "DEF":
                        {
                            ordenLetra = jsonData.personas[i].DEF;
                            break;
                        }
                    case "HP":
                        {
                            ordenLetra = jsonData.personas[i].HP;
                            break;
                        }
                    case "マギア値":
                    case "コネクト値":
                        {
                            ordenLetra = jsonData.personas[i].sortValue;
                            break;
                        }
                    default:
                        {
                            ordenLetra = "";
                            break;
                        }
                        
                }
                // DrawImageText(ctx3, charaX[i], charaY, charaR, ordenLetra);
                Draw2ndText(ctx3, charaX[i], charaY, charaR, ordenLetra, Xoffset, Yoffset, canvasFlag);
            }
            IndicaResultado();

            //testcode
            // $("label#MainContent_orden1_0").text("BBQ");
            // $("#MainContent_orden1_0").closest("label").text("BBQ");
            // document.getElementsByTagName('input')[21].value = "BBQ";
            // document.getElementsByTagName('label')[13].innerText = "BBQ";
            // document.getElementById("MainContent_orden1_0").nextSibling.innerText = "BBQ";//これがうまくいく
        }
    });

    canvas3.addEventListener('pointercancel', function (e) {
        pointerFlag = false;
        // console.log(pointerFlag + " cancel");
    });

    //コネクト情報取得
    function GetConnect(inputJson, selector) {
        //Jsonデータ確認し、データを格納
        let resultado;
        if (BusquedaConectiva("攻撃力UP", inputJson, 1)) {
            resultado = BusquedaConectiva("攻撃力UP", inputJson, 1);
            connectElegido[selector][0] = (ChangeRoman(resultado[1]) * 2.5) + 17.5;
        }
        if (BusquedaConectiva("与えるダメージUP", inputJson, 1)) {
            resultado = BusquedaConectiva("与えるダメージUP", inputJson, 1);
            connectElegido[selector][1] = (ChangeRoman(resultado[1]) * 2.5) + 17.5;
        }
        if (BusquedaConectiva("Charge後ダメージUP", inputJson, 1)) {
            resultado = BusquedaConectiva("Charge後ダメージUP", inputJson, 1);
            connectElegido[selector][2] = (ChangeRoman(resultado[1]) * 2.5) + 7.5;
        }
        if (BusquedaConectiva("BlastダメージUP", inputJson, 1)) {
            resultado = BusquedaConectiva("BlastダメージUP", inputJson, 1);
            connectElegido[selector][3] = (ChangeRoman(resultado[1]) * 5) + 35;
        }
        if (BusquedaConectiva("敵状態異常時ダメージUP", inputJson, 1)) {
            resultado = BusquedaConectiva("敵状態異常時ダメージUP", inputJson, 1);
            connectElegido[selector][4] = (ChangeRoman(resultado[1]) * 2.5) + 22.5;
        }
        if (BusquedaConectiva("防御無視", inputJson, 1)) {
            connectElegido[selector][5] = true;
        }
        if (BusquedaConectiva("月咲にコネクトで攻撃力UP", inputJson, 1)) {
            resultado = BusquedaConectiva("月咲にコネクトで攻撃力UP", inputJson, 1);
            connectElegido[selector][6] = (ChangeRoman(resultado[1]) * 2.5) + 17.5;
        }
        if (BusquedaConectiva("AcceleMPUP", inputJson, 1)) {
            resultado = BusquedaConectiva("AcceleMPUP", inputJson, 1);
            connectElegido[selector][10] = (ChangeRoman(resultado[1]) * 5) + 5;
        }
        if (BusquedaConectiva("MP獲得量UP", inputJson, 1)) {
            resultado = BusquedaConectiva("MP獲得量UP", inputJson, 1);
            connectElegido[selector][11] = (ChangeRoman(resultado[1]) * 2.5) + 7.5;
        }
        if (BusquedaConectiva("MP回復", inputJson, 1)) {
            resultado = BusquedaConectiva("MP回復", inputJson, 1);
            connectElegido[selector][12] = (ChangeRoman(resultado[1]) * 2.5) + 10;
        }
        if (BusquedaConectiva("確率でクリティカル", inputJson, 1)) {
            resultado = BusquedaConectiva("確率でクリティカル", inputJson, 1);
            connectElegido[selector][13] = (ChangeRoman(resultado[1]) * 5) + 25;
        }
    }

    function ChangeRoman(roman) {
        switch (roman) {
            case "Ⅰ": return 1;
            case "Ⅱ": return 2;
            case "Ⅲ": return 3;
            case "Ⅳ": return 4;
            case "Ⅴ": return 5;
            case "Ⅵ": return 6;
            case "Ⅶ": return 7;
            case "Ⅷ": return 8;
            case "Ⅸ": return 9;
            case "Ⅹ": return 10;
            case "Ⅺ": return 11;
            default: return 0;
        }
    }

    //フィルター選択時
    $('input[name="ctl00$MainContent$filtro1$0"]').change(function () {
        if ($("#MainContent_filtro1_0").prop("checked") === true) {
            //全チェック
            $('input[name*="ctl00$MainContent$filtro1"]').prop('checked', true);
        }
        else {
            //チェック全外し
            $('input[name*="ctl00$MainContent$filtro1"]').prop('checked', false);
        }
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$1"]').change(function () {
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$2"]').change(function () {
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$3"]').change(function () {
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$4"]').change(function () {
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$5"]').change(function () {
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$6"]').change(function () {
        CheckChanged();
    });
    $('#MainContent_tipo1').change(function () {
        CheckChanged();
    });
    $('#MainContent_tipoMagia').change(function () {
        CheckChanged();
        var ordenMagia = document.getElementById("MainContent_orden1_4");
        var textMagia = $('#MainContent_tipoMagia').val();
        if (textMagia.indexOf("マギア指定") === -1) {
            ordenMagia.disabled = false;
            if (jsonData.personas[0].sortValue !== null && $(window).width() > 768) {
                document.getElementById("MainContent_orden1_4").nextSibling.innerText = "マギア値:" + textMagia;
            }
            else if ($(window).width() > 768)
                document.getElementById("MainContent_orden1_4").nextSibling.innerText = "マギア値";
        }
        else {
            if ($("#MainContent_orden1_4").prop("checked") === true) {
                document.getElementById("MainContent_orden1_4").nextSibling.classList.remove("highlight");
                $("#MainContent_orden1_4").prop("checked", false);
            }
            ordenMagia.disabled = true;
            if ($(window).width() > 768)
                document.getElementById("MainContent_orden1_4").nextSibling.innerText = "マギア値";
        }

        let data = CuentaTodo("magia1",2);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
        ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
        ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });
    // var virgen = 0;
    $('#MainContent_tipoMagia').on('click', function () {
            let data = CuentaTodo("magia1",1);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
            ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
            ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });

    $('#MainContent_tipoMagia2').change(function () {
        CheckChanged();
        let data = CuentaTodo("magia2",2);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
        ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
        ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });
    $('#MainContent_tipoMagia2').on('click', function () {
            let data = CuentaTodo("magia2",1);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
            ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
            ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });
    $('#MainContent_gorila').change(function () {
        CheckChanged();
    });
    $('#MainContent_connect1').change(function () {
        CheckChanged();
        var ordenConnect = document.getElementById("MainContent_orden1_5");
        var textConnect = $('#MainContent_connect1').val();
        if (textConnect.indexOf("コネクト指定") === -1) {
            ordenConnect.disabled = false;
            if (jsonData.personas[0].sortValue !== null && $(window).width() > 768) {
                document.getElementById("MainContent_orden1_5").nextSibling.innerText = "コネクト値:" + textConnect;
            }
            else if ($(window).width() > 768)
                document.getElementById("MainContent_orden1_5").nextSibling.innerText = "コネクト値";
        }
        else {
            if ($("#MainContent_orden1_5").prop("checked") === true) {
                document.getElementById("MainContent_orden1_5").nextSibling.classList.remove("highlight");
                $("#MainContent_orden1_5").prop("checked", false);
            }
            ordenConnect.disabled = true;
            if ($(window).width() > 768)
                document.getElementById("MainContent_orden1_5").nextSibling.innerText = "コネクト値";
        }
        
        let data = CuentaTodo("connect1",2);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
        ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
        ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });
    $('#MainContent_connect1').on('click', function () {
            let data = CuentaTodo("connect1",1);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
            ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
            ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
            
    });

    $('#MainContent_connect2').change(function () {
        CheckChanged();
        let data = CuentaTodo("connect2",2);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
        ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
        ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
        ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });

    $('#MainContent_connect2').on('click', function () {
            let data = CuentaTodo("connect2",1);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia"), data[0]);
            ApareceCantidad(document.getElementById("MainContent_tipoMagia2"), data[1]);
            ApareceCantidad(document.getElementById("MainContent_connect1"), data[2]);
            ApareceCantidad(document.getElementById("MainContent_connect2"), data[3]);
    });
    
    //項目にフィルタ数追加
    function ApareceCantidad(list,data) {
        //書き換え処理
        for (let i = 1; i < list.length; i++){
            let s = list.options[i].value + "(" + data[i][1] + ")";
            list.options[i].text = s;
        }

    }
    
    //カウント処理
    function CuentaTodo(operacion,orden) {
        //コネクト選択肢取得
        var opcionesC1 = new Array;
        var opcionesC2 = new Array;
        for (let i = 0; i < $('#MainContent_connect1').children().length; i++){
            opcionesC1.push([document.getElementById("MainContent_connect1").options[i].value, 0]);
            opcionesC2.push([document.getElementById("MainContent_connect2").options[i].value, 0]);
        }
        var opcionesM1 = new Array;
        var opcionesM2 = new Array;
        for (let i = 0; i < $('#MainContent_tipoMagia').children().length; i++){
            opcionesM1.push([document.getElementById("MainContent_tipoMagia").options[i].value, 0]);
            opcionesM2.push([document.getElementById("MainContent_tipoMagia2").options[i].value, 0]);
        }

        //セレクト値で場合分け
        var c1 = $('#MainContent_connect1').val();
        var c2 = $('#MainContent_connect2').val();
        var m1 = $('#MainContent_tipoMagia').val();
        var m2 = $('#MainContent_tipoMagia2').val();
        var mc = [m1, m2, c1, c2];
        var tipoC = 1;
        if (!((c1 === opcionesC1[0][0]) && (c2 === opcionesC2[0][0]))) {
            if ((c1 !== opcionesC1[0][0]) && (c2 === opcionesC2[0][0]))
                tipoC = 2;
            else if((c1 === opcionesC1[0][0]) && (c2 !== opcionesC2[0][0]))
                tipoC = 3;
            else
                tipoC = 4;
        }
        var tipoM = 1;
        if (!((m1 === opcionesM1[0][0]) && (m2 === opcionesM2[0][0]))) {
            if ((m1 !== opcionesM1[0][0]) && (m2 === opcionesM2[0][0]))
                tipoM = 2;
            else if((m1 === opcionesM1[0][0]) && (m2 !== opcionesM2[0][0]))
                tipoM = 3;
            else
                tipoM = 4;
        } else {//tipoC=1,tipoM=1の場合
            if(tipoC === 1)
                operacion = "";
        }
        var opcion = [opcionesM1, opcionesM2, opcionesC1, opcionesC2];
        var opcionesOri = JSON.parse(JSON.stringify(opcion));
        //カウント処理一回目
        var opciones = CuentaMayor(opcionesM1, opcionesM2, opcionesC1, opcionesC2, tipoM,tipoC, mc,jsonData);
        //後で使うためにコピーを取っておく
        var opciones1 = JSON.parse(JSON.stringify(opciones));
        
        //二回目のcuentaTodoの場合は、以下の処理をキャンセルする
        if (orden === 2)
            operacion = "";

        //現在操作しているドロップダウンによるリセット
        var jsonData1;
        switch (operacion) {
            case "magia1":
                {
                    if (m1 !== opcionesM1[0][0]) {
                        m1 = opcionesM1[0][0];
                        jsonData1 = angular.copy(jsonDataOri);
                    }
                    break;
                }
            case "magia2":
                {
                    if (m2 !== opcionesM2[0][0]) {
                        m2 = opcionesM2[0][0];
                        jsonData1 = angular.copy(jsonDataOri);
                    }
                    break;
                }
            case "connect1":
                {
                    if (c1 !== opcionesC1[0][0]) {
                        c1 = opcionesC1[0][0];
                        jsonData1 = angular.copy(jsonDataOri);
                    }
                    break;
                }
            case "connect2":
                {
                    if (c2 !== opcionesC2[0][0]) {
                        c2 = opcionesC2[0][0];
                        jsonData1 = angular.copy(jsonDataOri);
                    }
                    break;
                }
            default://二回目の処理を行わない
                {
                    return [opciones1[0],opciones1[1],opciones1[2],opciones1[3]];
                }
        }
        //データがコピーされない場合は以後の処理を行う必要なし
        if (typeof jsonData1 === "undefined")
            return [opciones1[0], opciones1[1], opciones1[2], opciones1[3]];
        //コピーしたJsonデータに、属性と魔法少女タイプとディスクフィルタをかける
        //チェックされた属性値を取得
        let atributo = [7];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        //魔法少女タイプ取得
        let tipoPuella = $("#MainContent_tipo1").val();
        //ゴリラタイプ取得
        let tipoGorila = $("#MainContent_gorila").val();

        jsonData1.personas = jsonData1.personas.filter(function (value, index, array) {
            let checkFlag = 0;
            for (let i = 0; i < atributo.length; i++) {
                if (value.Attribute === atributo[i]) {
                    checkFlag++;
                }
            }
            if (checkFlag === 0)
                return false
            checkFlag = 0;

            if (tipoPuella !== "タイプ無") {
                if (value.TipoMagia === tipoPuella) {
                    checkFlag++;
                }
                else if (tipoPuella === "マギア" && value.TipoMagia === "円環マギア") {
                    checkFlag++;
                }
                else if (tipoPuella === "サポート" && value.TipoMagia === "円環サポート") {
                    checkFlag++;
                }
            } else
                checkFlag++;
            if (checkFlag === 0)
                return false
            checkFlag = 0;

            if (tipoGorila !== "ディスク") {
                switch (tipoGorila) {
                    case "B3枚":
                        {
                            if (value.Disk === "ABBBC") {
                                checkFlag++;
                            }
                            break;
                        }
                    case "A3枚":
                        {
                            if (value.Disk === "AAABC") {
                                checkFlag++;
                            }
                            break;
                        }
                    case "C3枚":
                        {
                            if (value.Disk === "ABCCC") {
                                checkFlag++;
                            }
                            break;
                        }
                    case "A2枚":
                        {
                            if ((value.Disk === "AABBC") || (value.Disk === "AABCC")) {
                                checkFlag++;
                            }
                            break;
                        }
                    case "B2枚":
                        {
                            if ((value.Disk === "AABBC") || (value.Disk === "ABBCC")) {
                                checkFlag++;
                            }
                            break;
                        }
                    case "C2枚":
                        {
                            if ((value.Disk === "ABBCC") || (value.Disk === "AABCC")) {
                                checkFlag++;
                            }
                            break;
                        }
                }
            } else
                checkFlag++;
            if (checkFlag === 0)
                return false
            return true;
        });

        var mc2 = [m1, m2, c1, c2];
        //セレクト値で場合分け
        tipoC = 1;
        if (!((c1 === opcionesC1[0][0]) && (c2 === opcionesC2[0][0]))) {
            if ((c1 !== opcionesC1[0][0]) && (c2 === opcionesC2[0][0]))
                tipoC = 2;
            else if((c1 === opcionesC1[0][0]) && (c2 !== opcionesC2[0][0]))
                tipoC = 3;
            else
                tipoC = 4;
        }
        tipoM = 1;
        if (!((m1 === opcionesM1[0][0]) && (m2 === opcionesM2[0][0]))) {
            if ((m1 !== opcionesM1[0][0]) && (m2 === opcionesM2[0][0]))
                tipoM = 2;
            else if((m1 === opcionesM1[0][0]) && (m2 !== opcionesM2[0][0]))
                tipoM = 3;
            else
                tipoM = 4;
        }
        opciones = JSON.parse(JSON.stringify(opcionesOri));
        //カウント処理二回目
        var opciones2 = CuentaMayor(opciones[0], opciones[1], opciones[2], opciones[3], tipoM,tipoC,mc2,jsonData1);
        
        //出力するセットを確定
        var output = opciones1;
        switch (operacion) {
            case "magia1":
                {
                    output[0] = opciones2[0];
                    break;
                }
            case "magia2":
                {
                    output[1] = opciones2[1];
                    break;
                }
            case "connect1":
                {
                    output[2] = opciones2[2];
                    break;
                }
            case "connect2":
                {
                    output[3] = opciones2[3];
                    break;
                }
        }
        
        return output;
    }

    function CuentaMayor(opcionesM1,opcionesM2,opcionesC1,opcionesC2,tipoM,tipoC,selector,inputJson) {
        //フラグ
        let flagC1 = new Array(opcionesC1.length);
        let flagC2 = new Array(opcionesC1.length);
        let flagM1 = new Array(opcionesC1.length);
        let flagM2 = new Array(opcionesC1.length);

        let m1 = selector[0];
        let m2 = selector[1];
        let c1 = selector[2];
        let c2 = selector[3];

        let flagCuenta;

        //通常カウント処理
        for (let i = 0; i < inputJson.personas.length; i++){
            let v = inputJson.personas[i];
            //カウント種類が4パターンある
            //1:通常。選択肢1と2が初期値
            //2:左側の選択肢が選択されている場合。左側は通常カウントでいいが、右側は、左側とand条件
            //3:右側の選択肢が選択されている場合
            //4:両方の選択肢が選択されている場合。2と3の組み合わせ
            //これをマギアとコネクトで更に重ねる。。。！！！
            //マギアとコネクトでカウントフラグ立てるのが良さそう

            //フラグリセット
            flagC1.length = 0;
            flagC2.length = 0;
            flagM1.length = 0;
            flagM2.length = 0;

            flagC1 = new Array(opcionesC1.length);
            flagC2 = new Array(opcionesC1.length);
            flagM1 = new Array(opcionesC1.length);
            flagM2 = new Array(opcionesC1.length);

            //二番目のループをスルーしてカウントしていいかのフラグ
            //tipoC,tipoMがそれぞれ1ならフラグが入る
            flagCuenta = new Array(opcionesC1.length);

            //コネクトカウント処理
            for (let j = 1; j < opcionesC1.length; j++) {
                switch (tipoC) {
                    case 1:
                        {
                            if (BusquedaConectiva(opcionesC1[j][0], v, 0)) {
                                flagC1[j] = 1;
                                flagC2[j] = 1;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (BusquedaConectiva(opcionesC1[j][0], v, 0)) {
                                flagC1[j] = 1;
                            }
                            if ((BusquedaConectiva(c1, v, 0)) &&
                                (BusquedaConectiva(opcionesC2[j][0], v, 0))) {
                                flagC2[j] = 1;
                            }
                            break;
                        }
                    case 3:
                        {
                            if ((BusquedaConectiva(opcionesC1[j][0], v, 0)) &&
                                (BusquedaConectiva(c2, v, 0))) {
                                flagC1[j] = 1;
                            }
                            if (BusquedaConectiva(opcionesC2[j][0], v, 0)) {
                                flagC2[j] = 1;
                            }
                            break;
                        }
                    case 4:
                        {
                            if ((BusquedaConectiva(opcionesC1[j][0], v, 0)) &&
                                (BusquedaConectiva(c2, v, 0))) {
                                flagC1[j] = 1;
                            }
                            if ((BusquedaConectiva(c1, v, 0)) &&
                                (BusquedaConectiva(opcionesC2[j][0], v, 0))) {
                                flagC2[j] = 1;
                            }
                            break;
                        }
                }//tipoC終わり
                //あるjの中で、さらにマギア側を調査
                flagM1[j] = new Array(opcionesM1.length);
                flagM2[j] = new Array(opcionesM1.length);
                for (let k = 1; k < opcionesM1.length; k++){
                    if ((flagCuenta[j] === 1)) {
                        break;//両方カウントOKならループ抜ける
                    }
                    switch (tipoM) {
                        case 1:
                            {
                                // if (BusquedaMagia(opcionesM1[k][0], v, 0)) {
                                //     flagM1[j][k] = 1;
                                //     flagM2[j][k] = 1;
                                // }
                                flagCuenta[j] = 1;
                                break;
                            }
                        case 2:
                            {
                                if (BusquedaMagia(opcionesM1[k][0], v, 0)) {
                                    flagM1[j][k] = 1;
                                }
                                if ((BusquedaMagia(m1, v, 0)) && (BusquedaMagia(opcionesM2[k][0], v, 0))) {
                                    flagM2[j][k] = 1;
                                }
                                break;
                            }
                        case 3:
                            {
                                if ((BusquedaMagia(opcionesM1[k][0], v, 0)) && (BusquedaMagia(m2, v, 0))) {
                                    flagM1[j][k] = 1;
                                }
                                if (BusquedaMagia(opcionesM2[k][0], v, 0)) {
                                    flagM2[j][k] = 1;
                                }
                                break;
                            }
                        case 4:
                            {
                                if ((BusquedaMagia(opcionesM1[k][0], v, 0)) && (BusquedaMagia(m2, v, 0))) {
                                    flagM1[j][k] = 1;
                                }
                                if ((BusquedaMagia(m1, v, 0)) && (BusquedaMagia(opcionesM2[k][0], v, 0))) {
                                    flagM2[j][k] = 1;
                                }
                                break;
                            }
                    }//tipoM終わり
                }
                //Cの最終判定
                if (flagC1[j] === 1) {
                    if (flagCuenta[j] === 1)
                        opcionesC1[j][1]++;
                    else {
                        for (let k = 1; k < flagM1[j].length; k++) {
                            if ((flagM1[j][k] === 1) && (flagM2[j][k] === 1)) {
                                opcionesC1[j][1]++;
                                if (opcionesC1[j][0] === "クリティカル")
                                    console.log(i + " " + v.name + " クリティカル")
                                break;
                            }
                            
                        }
                    }
                }
                if (flagC2[j] === 1) {
                    if (flagCuenta[j] === 1)
                        opcionesC2[j][1]++;
                    else {
                        for (let k = 1; k < flagM1[j].length; k++) {
                            if ((flagM1[j][k] === 1) && (flagM2[j][k] === 1)) {
                                opcionesC2[j][1]++;
                                break;
                            }
                            
                        }
                    }
                } 
            }

            //フラグリセット
            flagC1.length = 0;
            flagC2.length = 0;
            flagM1.length = 0;
            flagM2.length = 0;
            flagCuenta.length = 0;

            flagC1 = new Array(opcionesM1.length);
            flagC2 = new Array(opcionesM1.length);
            flagM1 = new Array(opcionesM1.length);
            flagM2 = new Array(opcionesM1.length);
            flagCuenta = new Array(opcionesM1.length);

            //マギアカウント処理
            for (let j = 1; j < opcionesM1.length; j++) {
                switch (tipoM) {
                    case 1:
                        {
                            if (BusquedaMagia(opcionesM1[j][0], v, 0)) {
                                flagM1[j] = 1;
                                flagM2[j] = 1;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (BusquedaMagia(opcionesM1[j][0], v, 0)) {
                                flagM1[j] = 1;
                            }
                            if ((BusquedaMagia(m1, v, 0)) &&
                                (BusquedaMagia(opcionesM2[j][0], v, 0))) {
                                flagM2[j] = 1;
                            }
                            break;
                        }
                    case 3:
                        {
                            if ((BusquedaMagia(opcionesM1[j][0], v, 0)) &&
                                (BusquedaMagia(m2, v, 0))) {
                                flagM1[j] = 1;
                            }
                            if (BusquedaMagia(opcionesM2[j][0], v, 0)) {
                                flagM2[j] = 1;
                            }
                            break;
                        }
                    case 4:
                        {
                            if ((BusquedaMagia(opcionesM1[j][0], v, 0)) &&
                                (BusquedaMagia(m2, v, 0))) {
                                flagM1[j] = 1;
                            }
                            if ((BusquedaMagia(m1, v, 0)) &&
                                (BusquedaMagia(opcionesM2[j][0], v, 0))) {
                                flagM2[j] = 1;
                            }
                            break;
                        }
                }//tipoM終わり
                //あるjの中で、さらにコネクト側を調査
                flagC1[j] = new Array(opcionesC1.length);
                flagC2[j] = new Array(opcionesC1.length);
                for (let k = 1; k < opcionesC1.length; k++){
                    if (flagCuenta[j] === 1) {
                        break;
                    }
                    switch (tipoC) {
                        case 1:
                            {
                                // if (BusquedaConectiva(opcionesC1[k][0], v, 0)) {
                                //     flagC1[j][k] = 1;
                                //     flagC2[j][k] = 1;
                                // }
                                flagCuenta[j] = 1;
                                break;
                            }
                        case 2:
                            {
                                if (BusquedaConectiva(opcionesC1[k][0], v, 0)) {
                                    flagC1[j][k] = 1;
                                }
                                if ((BusquedaConectiva(c1, v, 0)) && (BusquedaConectiva(opcionesC2[k][0], v, 0))) {
                                    flagC2[j][k] = 1;
                                }
                                break;
                            }
                        case 3:
                            {
                                if ((BusquedaConectiva(opcionesC1[k][0], v, 0)) && (BusquedaConectiva(c2, v, 0))) {
                                    flagC1[j][k] = 1;
                                }
                                if (BusquedaConectiva(opcionesC2[k][0], v, 0)) {
                                    flagC2[j][k] = 1;
                                }
                                break;
                            }
                        case 4:
                            {
                                if ((BusquedaConectiva(opcionesC1[k][0], v, 0)) && (BusquedaConectiva(c2, v, 0))) {
                                    flagC1[j][k] = 1;
                                }
                                if ((BusquedaConectiva(c1, v, 0)) && (BusquedaConectiva(opcionesC2[k][0], v, 0))) {
                                    flagC2[j][k] = 1;
                                }
                                break;
                            }
                    }//tipoC終わり
                }
                //Mの最終判定
                if (flagM1[j] === 1) {
                    if (flagCuenta[j] === 1)
                        opcionesM1[j][1]++;
                    else {
                        for (let k = 1; k < flagC1[j].length; k++) {
                            if ((flagC1[j][k] === 1) && (flagC2[j][k] === 1)) {
                                opcionesM1[j][1]++;
                                break;
                            }
                            
                        }
                    }
                }
                if (flagM2[j] === 1) {
                    if (flagCuenta[j] === 1)
                        opcionesM2[j][1]++;
                    else {
                        for (let k = 1; k < flagC1[j].length; k++) {
                            if ((flagC1[j][k] === 1) && (flagC2[j][k] === 1)) {
                                opcionesM2[j][1]++;
                                break;
                            }
                            
                        }
                    }
                } 
            }
            
        }//通常カウント終わり
        return [opcionesM1, opcionesM2, opcionesC1, opcionesC2];
    }

    $('input[name="ctl00$MainContent$seleccionado"]:radio').change(function () {
        elegida = [-1, -1, -1];
        var ordena = OrdenaMagia();
        ReDraw(ordena);
    });
    function CheckChanged(flag) {
        //チェックされた属性値を取得
        var atributo = [7];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });

        //マギアタイプ取得
        var tipoMagia = new Array(2);
        tipoMagia[0] = $("#MainContent_tipoMagia").val();
        tipoMagia[1] = $("#MainContent_tipoMagia2").val();
        //魔法少女タイプ取得
        var tipoPuella = $("#MainContent_tipo1").val();
        //ゴリラタイプ取得
        var tipoGorila = $("#MainContent_gorila").val();
        //コネクト取得
        var connect = [2];
        if (flag === "connect1") {
            connect[0] = document.getElementById("MainContent_connect1").options[0].value;
            connect[1] = $("#MainContent_connect2").val();
        }
        else if (flag === "connect2") {
            connect[0] = $("#MainContent_connect1").val();
            connect[1] = document.getElementById("MainContent_connect2").options[0].value;
        }
        else {
            connect[0] = $("#MainContent_connect1").val();
            connect[1] = $("#MainContent_connect2").val();
        }

        //全チェックを消すか確認
        if (atributo[0] === "全" && atributo.length < 7) {
            //消す
            $("#MainContent_filtro1_0").prop("checked",false);
        } else if (atributo[0] !== "全" && atributo.length === 6) {
            //付ける
            $("#MainContent_filtro1_0").prop("checked",true);
        }

        //フィルター処理
        var checkFlag = [jsonData.personas.length];
        jsonData = angular.copy(jsonDataOri);
        jsonData.personas = jsonData.personas.filter(function (value, index, array) {
            checkFlag[index] = 0;
            for (let i = 0; i < atributo.length; i++) {
                if (value.Attribute === atributo[i]) {
                    checkFlag[index] += 1;
                    // return true;
                }
            }
            if (tipoPuella !== "タイプ無") {
                if (value.TipoMagia === tipoPuella) {
                    checkFlag[index] += 3;
                }
                else if (tipoPuella === "マギア" && value.TipoMagia === "円環マギア"){
                    checkFlag[index] += 3;
                }
                else if (tipoPuella === "サポート" && value.TipoMagia === "円環サポート"){
                    checkFlag[index] += 3;
                }
            }

            let resultado = new Array(2);
            for (let i = 0; i < tipoMagia.length; i++){
                if (tipoMagia[i].indexOf("マギア指定") === -1) {
                    let sortFlag = ($("input[name='ctl00$MainContent$orden1']:checked").val() === "マギア値")&& i===0 ? 1 : 0;
                    switch (tipoMagia[i]) {
                        case "敵全体":
                            {
                                if (BusquedaMagia("全体", value, 1)) {
                                    resultado = BusquedaMagia("全体", value, 2);
                                    if(sortFlag===1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return false;
                                break;
                            }
                        case "敵単体":
                            {
                                if (BusquedaMagia("単体", value, 1)) {
                                    resultado = BusquedaMagia("単体", value, 2);
                                    if(sortFlag===1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return false;
                                break;
                            }
                        case "敵縦一列":
                            {
                                if (BusquedaMagia("縦", value, 1)) {
                                    resultado = BusquedaMagia("縦", value, 2);
                                    if(sortFlag===1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return false;
                                break;
                            }
                        //info2でソート
                        case "ランダム":
                            {
                                if (BusquedaMagia(tipoMagia[i], value, 1)) {
                                    resultado = BusquedaMagia(tipoMagia[i], value, 2);
                                    if(sortFlag===1) {
                                        value.sortValue = resultado[1];
                                        resultado = BusquedaMagia(tipoMagia[i], value, 1);
                                        value.sortValue += " " + resultado[1];//回数も表示
                                        
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                        //ソートしない場合
                        case "属性強化":
                        case "低HPほど威力UP":
                            {
                                if (BusquedaMagia(tipoMagia[i], value, 1)) {
                                    resultado = BusquedaMagia(tipoMagia[i], value, 2);
                                }
                                else
                                    return false;
                                break;
                            }
                        //info1でソート
                        case "HP回復":
                        case "HP自動回復":
                        case "MP回復":
                            {
                                if (BusquedaMagia(tipoMagia[i], value, 1)) {
                                    resultado = BusquedaMagia(tipoMagia[i], value, 1);
                                    if(sortFlag===1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return false;
                                break;
                            }
                    }
                }
            }

            if (tipoGorila !== "ディスク") {
                switch (tipoGorila) {
                    case "B3枚":
                        {
                            if (value.Disk === "ABBBC") {
                                checkFlag[index] += 5;
                            }
                            break;
                        }
                    case "A3枚":
                        {
                            if (value.Disk === "AAABC") {
                                checkFlag[index] += 5;
                            }
                            break;
                        }
                    case "C3枚":
                        {
                            if (value.Disk === "ABCCC") {
                                checkFlag[index] += 5;
                            }
                            break;
                        }
                    case "A2枚":
                        {
                            if ((value.Disk === "AABBC")||(value.Disk === "AABCC")) {
                                checkFlag[index] += 5;
                            }
                            break;
                        }
                    case "B2枚":
                        {
                            if ((value.Disk === "AABBC")||(value.Disk === "ABBCC")) {
                                checkFlag[index] += 5;
                            }
                            break;
                        }
                    case "C2枚":
                        {
                            if ((value.Disk === "ABBCC")||(value.Disk === "AABCC")) {
                                checkFlag[index] += 5;
                            }
                            break;
                        }
        
                }
            }

            //コネクトフィルタ
            for (let i = 0; i < connect.length; i++)
            {
                if (connect[i].indexOf("コネクト指定") === -1) {
                    let sortFlag = ($("input[name='ctl00$MainContent$orden1']:checked").val() === "コネクト値")&& i===0 ? 1 : 0;
                    
                    switch (connect[i]) {
                        case "攻撃力UP":
                            {
                                if (BusquedaConectiva(connect[i], value, 1)) {
                                    resultado = BusquedaConectiva(connect[i], value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "クリティカル":
                            {
                                if (BusquedaConectiva("確率でクリティカル", value, 1)) {
                                    resultado = BusquedaConectiva("確率でクリティカル", value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "回避":
                            {
                                if (BusquedaConectiva("必ず回避", value, 0)) {
                                    resultado = BusquedaConectiva("必ず回避", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で回避", value, 0)) {
                                    resultado = BusquedaConectiva("確率で回避", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "防御無視":
                            {
                                if (BusquedaConectiva(connect[i], value, 0)) {
                                    resultado = BusquedaConectiva(connect[i], value, 1);
                                }
                                else
                                    return;
                                break;
                            }
                        case "ダメージUP":
                            {
                                if (BusquedaConectiva("与えるダメージUP", value, 1)) {
                                    resultado = BusquedaConectiva("与えるダメージUP", value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "BlastダメUP":
                            {
                                if (BusquedaConectiva("BlastダメージUP", value, 1)) {
                                    resultado = BusquedaConectiva("BlastダメージUP", value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "拘束":
                            {
                                if (BusquedaConectiva("必ず拘束", value, 1)) {
                                    resultado = BusquedaConectiva("必ず拘束", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "スタン":
                            {
                                if (BusquedaConectiva("必ずスタン", value, 1)) {
                                    resultado = BusquedaConectiva("必ずスタン", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else if (BusquedaConectiva("確率でスタン", value, 1)) {
                                    resultado = BusquedaConectiva("確率でスタン", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "魅了":
                            {
                                if (BusquedaConectiva("必ず魅了", value, 1)) {
                                    resultado = BusquedaConectiva("必ず魅了", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で魅了", value, 1)) {
                                    resultado = BusquedaConectiva("確率で魅了", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "幻惑":
                            {
                                if (BusquedaConectiva("必ず幻惑", value, 1)) {
                                    resultado = BusquedaConectiva("必ず幻惑", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で幻惑", value, 1)) {
                                    resultado = BusquedaConectiva("確率で幻惑", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "HP回復":
                        case "MP回復":
                        case "MP獲得量UP":
                        case "AcceleMPUP":
                            {
                                if (BusquedaConectiva(connect[i], value, 1)) {
                                    resultado = BusquedaConectiva(connect[i], value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        
                        
                    }
                }
                
            }

            //フィルタ指定と結果の照合
            
            switch (checkFlag[index]) {
                case 0:
                case 3:
                case 5:
                case 8:
                    //atributoがhitしない場合はfalse
                    return false;
                case 1:
                    //atributoのみ選択した場合
                    //その他項目を選択したがhitしなかった場合
                    {
                        if (tipoPuella !== "タイプ無" || tipoGorila !== "ディスク") 
                            return false;
                        else 
                            return true;
                    }
                case 4:
                    //魔法少女タイプhit
                    //ゴリラがhitしなかった場合もある
                    {
                        if (tipoGorila !== "ディスク")
                            return false;
                        else
                            return true;
                    }
                case 6:
                    //ゴリラhit
                    //魔法少女タイプがhitしない場合もある
                    {
                        if (tipoPuella !== "タイプ無")
                            return false;
                        else
                            return true;
                        
                    }
                case 9:
                    return true;
            }

            console.log("フィルタ処理失敗 " + index + " " + value.name + " " + checkFlag[index]);
        });

        if (flag)
            return;

        if (jsonData.personas.length === 0) {
            ctx3.clearRect(0, 0, canvas3.width, canvas3.height);
            //キャラ数表示
            document.getElementById("MainContent_indicaPersonas").innerText = "登録 " + jsonDataOri.personas.length + "キャラ中に該当キャラ無し";
            return;
        }
        personas = jsonData.personas.length;
        charaX = angular.copy(charaXori);
        //ソート処理
        var ordena = OrdenaMagia();
        ReDraw(ordena);
    }

    //ソート方法選択時
    $('input[name="ctl00$MainContent$orden1"]:radio').change(function () {
        //ハイライト処理
        document.getElementById("MainContent_orden1_0").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_orden1_1").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_orden1_2").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_orden1_3").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_orden1_4").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_orden1_5").nextSibling.classList.remove("highlight");

        var elegido = $('input[name="ctl00$MainContent$orden1"]:checked').val();
        switch (elegido) {
            case "ATK":
                {
                    document.getElementById("MainContent_orden1_0").nextSibling.classList.add("highlight");
                    break;
                }
            case "DEF":
                {
                    document.getElementById("MainContent_orden1_1").nextSibling.classList.add("highlight");
                    break;
                }
            case "HP":
                {
                    document.getElementById("MainContent_orden1_2").nextSibling.classList.add("highlight");
                    break;
                }
            case "名前50音":
                {
                    document.getElementById("MainContent_orden1_3").nextSibling.classList.add("highlight");
                    break;
                }
            case "マギア値":
                {
                    document.getElementById("MainContent_orden1_4").nextSibling.classList.add("highlight");
                    break;
                }
            case "コネクト値":
                {
                    document.getElementById("MainContent_orden1_5").nextSibling.classList.add("highlight");
                    break;
                }
        }
        

        charaX = angular.copy(charaXori);//位置初期化
        CheckChanged();
        var ordena = OrdenaMagia();
        ReDraw(ordena);
    });

    function ReDraw(ordena) {
        //配列初期化
        imageAry1.length = 0;
        imageAry2.length = 0;
        nombre.length = 0;
        var numero = $('input[name="ctl00$MainContent$seleccionado"]:checked').val()-1;
        elegida[numero] = -1;

        for (let i = 0; i < jsonData.personas.length; i++){
            imageAry1[i] = new Image();
            imageAry1[i].src = "png/" + jsonData.personas[i].data1;

            imageAry2[i] = new Image();
            imageAry2[i].src = "png/" + jsonData.personas[i].data2;

            nombre[i] = jsonData.personas[i].name.substring(jsonData.personas[i].name.indexOf("　") + 1);
        }
        //描画
        ctx3.clearRect(0, 0, canvas3.width, canvas3.height);
        for (let i = 0; i < jsonData.personas.length; i++) {
            if (jsonData.personas[i].data1 !== "") {
                // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry1[i]);
                Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry1[i], Xoffset, Yoffset, canvasFlag);
                var text;
                if (ordena !== ""){
                    switch (ordena) {
                        case "ATK":
                        {
                            text = jsonData.personas[i].ATK;
                            break;
                        }
                        case "DEF":
                        {
                            text = jsonData.personas[i].DEF;
                            break;
                        }
                        case "HP":
                        {
                            text = jsonData.personas[i].HP;
                            break;
                            }
                        case "sortValue":
                        {
                            text = jsonData.personas[i].sortValue;
                            break;
                        }
                    }
                    // DrawImageText(ctx3, charaX[i], charaY, charaR, text);
                    Draw2ndText(ctx3, charaX[i], charaY, charaR, text, Xoffset, Yoffset, canvasFlag);
                    }
            }
            else 
            {
                DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
                DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
            }
        }
        //キャラ数表示
        document.getElementById("MainContent_indicaPersonas").innerText = "登録 " + jsonDataOri.personas.length + "キャラ中 " + jsonData.personas.length + "キャラ表示";
    }

    //ソート処理
    function OrdenaMagia() {
        var tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
        function compareFuncATK(a, b) {
            return b.ATK - a.ATK;
        }
        function compareFuncDEF(a, b) {
            return b.DEF - a.DEF;
        }
        function compareFuncHP(a, b) {
            return b.HP - a.HP;
        }
        
        // jsonData.personas.sort(function (a, b) {
            switch (tipoOrden) {
                case "ATK":
                    {
                        // if (a.ATK < b.ATK) {
                        //     return 1;
                        // } else {
                        //     return -1;
                        // }
                        jsonData.personas.sort(compareFuncATK);
                        break;
                    }
                case "DEF":
                    {
                        // if (a.DEF < b.DEF) {
                        //     return 1;
                        // } else {
                        //     return -1;
                        // }
                        jsonData.personas.sort(compareFuncDEF);
                        break;
                    }
                case "HP":
                    {
                        // if (a.HP < b.HP) {
                        //     return 1;
                        // } else {
                        //     return -1;
                        // }
                        jsonData.personas.sort(compareFuncHP);
                        break;
                    }
                case "名前50音":
                    {
                        jsonData.personas.sort(function (a, b) {
                            let an = a.nameHiragana.indexOf("　") !== -1 ? a.nameHiragana.substring(a.nameHiragana.indexOf("　") + 1) : a.nameHiragana;
                            let bn = b.nameHiragana.indexOf("　") !== -1 ? b.nameHiragana.substring(b.nameHiragana.indexOf("　") + 1) : b.nameHiragana;                            
                            if (an > bn) {
                                return 1;
                            } else {
                                return -1;
                            }
                        });
                        break;
                    }
                case "マギア値":
                case "コネクト値":
                    {
                        //ソートしなくてもいいものはソートしない
                        let sortValue = jsonData.personas[0].sortValue;
                        if (sortValue !== null) {
                            if (sortValue.indexOf("必ず") !== -1 || sortValue.indexOf("確率で") !== -1) {
                                //特別処理　必ずを確率より前に持ってくる
                                jsonData.personas.sort(function (a, b) {
                                    if (a.sortValue > b.sortValue) {
                                        return 1;
                                    } else {
                                        return -1;
                                    }
                                });
                            }
                            else if (sortValue.indexOf("全") !== -1 || sortValue.indexOf("自") !== -1) {
                                //特別処理　全を自より前に持ってくる
                                jsonData.personas.sort(function (a, b) {
                                    if (a.sortValue > b.sortValue) {
                                        return 1;
                                    } else {
                                        return -1;
                                    }
                                });
                            }
                            else {//通常ローマ数字ソート
                                jsonData.personas.sort(function (a, b) {
                                    let an = a.sortValue.indexOf("回") === -1 ? a.sortValue : a.sortValue.substring(0, a.sortValue.indexOf("回") - 2);
                                    let bn = b.sortValue.indexOf("回") === -1 ? b.sortValue : b.sortValue.substring(0, b.sortValue.indexOf("回") - 2);
                                    if (an < bn) {
                                        return 1;
                                    } else {
                                        return -1;
                                    }
                                });
                            }
                        }
                        break;
                    }
                default:
                    {
                        jsonData.personas.sort(function (a, b) {
                            if (a.name > b.name) {
                                return 1;
                            } else {
                                return -1;
                            }
                        });
                        break;
                    }
            }
            
        // })
        var returnValue = "";
        switch (tipoOrden) {
            case "ATK":{
                returnValue = "ATK";
                break;
            }
            case "DEF": {
                returnValue = "DEF";
                break;
            }
            case "HP":{
                returnValue = "HP";
                break;
            }
            case "マギア値":
            case "コネクト値": {
                returnValue = "sortValue"
                break;
                }
        }
        return returnValue;
    }


    //コネクトフィルタ用
    //selector 1はvalue、2はturnを返す 0は不要な場合
    function BusquedaConectiva(parabra, value, selector) {
        //valueの中から、parabraに一致するものを探し出す
        var vuelta = [2];
        var valor = [value.connectA1, value.connectA2, value.connectA3, value.connectA4, value.connectA5,value.connectD1,value.connectD2,value.connectD3
        ];
        for (let i = 0; i < valor.length; i++) {
            let flagP = 0;
            switch (parabra) {
                case "クリティカル":
                    {
                        if (("必ずクリティカル" === valor[i].name)||("確率でクリティカル" === valor[i].name))
                            flagP = 1;
                        break;
                    }
                case "ダメージUP":
                    {
                        if ("与えるダメージUP" === valor[i].name)
                            flagP = 1;
                        break;
                    }
                case "BlastダメUP":
                    {
                        if ("BlastダメージUP" === valor[i].name)
                            flagP = 1;
                        break;
                    }
                case "回避":
                    {
                        if (("必ず回避" === valor[i].name)||("確率で回避" === valor[i].name))
                            flagP = 1;
                        break;
                    }
                case "拘束":
                    {
                        if (("必ず拘束" === valor[i].name)||("確率で拘束" === valor[i].name))
                            flagP = 1;
                        break;
                    }
                case "魅了":
                    {
                        if (("必ず魅了" === valor[i].name)||("確率で魅了" === valor[i].name))
                            flagP = 1;
                        break;
                    }
                case "幻惑":
                    {
                        if (("必ず幻惑" === valor[i].name)||("確率で幻惑" === valor[i].name))
                            flagP = 1;
                        break;
                    }
                case "スタン":
                    {
                        if (("必ずスタン" === valor[i].name)||("確率でスタン" === valor[i].name))
                            flagP = 1;
                        break;
                    }
            }

            if ((parabra === valor[i].name)||flagP===1) {
                vuelta[0] = true;
                switch (selector) {
                    case 0:
                        {
                            vuelta[1] = valor[i].name;
                            break;
                        }
                    case 1:
                        {
                            vuelta[1] = valor[i].value;
                            break;
                        }
                    case 2:
                        {
                            vuelta[1] = valor[i].turn;
                            break;
                        }
                }
                return vuelta;
            }
        }
        return false;
    }

    //マギアフィルタ用
    function BusquedaMagia(parabra, value, selector) {
        var vuelta = [2];
        var valor = [value.Magia1, value.Magia2, value.Magia3, value.Magia4, value.Magia5, value.Magia6];
        var flagM = 0;
        
        for (let i = 0; i < valor.length; i++){
            switch (parabra) {
                case "敵全体":
                    {
                        if ("全体" === valor[i].name)
                            flagM = 1;
                        break;
                    }
                case "敵単体":
                    {
                        if ("単体" === valor[i].name)
                            flagM = 1;
                        break;
                    }
                case "敵縦一列":
                    {
                        if ("縦" === valor[i].name)
                            flagM = 1;
                        break;
                    }
                case "属性強化":
                    {
                        if ("属性強化" === valor[i].info1)
                            flagM = 1;
                        break;
                    }
                case "回避":
                {
                    if (("必ず回避" === valor[i].name)||("確率で回避" === valor[i].name))
                        flagM = 1;
                    break;
                }
            }
            if ((parabra === valor[i].name)||(flagM === 1)) {
                vuelta[0] = true;
                switch (selector) {
                    case 0:
                        {
                            vuelta[1] = valor[i].name;
                            break;
                        }
                    case 1:
                        {
                            vuelta[1] = valor[i].info1;
                            break;
                        }
                    case 2:
                        {
                            vuelta[1] = valor[i].info2;
                            break;
                        }
                }
                return vuelta;
            }
        }
        return false;
    }

}//draw3ここまで

//color 
//1 black それ以外 white
function DrawCircle(ctx, x, y, r, charColor) {
    ctx.beginPath();
    ctx.save();
    ctx.fillStyle = charColor === 1 ? "white" : "red";
    ctx.strokeStyle = "black";
    ctx.arc(x, y, r, 0, 2 * Math.PI);
    ctx.fill();
    ctx.stroke();
    ctx.restore();
}
function DrawText(ctx, x, y, letra, charColor) {
    ctx.save();
    ctx.font = "16px sans-serif";
    ctx.fillStyle = charColor === 1 ? "red" : "white";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(letra, x, y);
    ctx.restore();
}
function DrawImage(ctx, x, y, r, data) {
    var image = new Image();
    image = data;
    // console.log(Date() + " order " + data.src);
    if (image.complete) {
        ctx.drawImage(image, x - r, y - r, r * 2, r * 2);
        // console.log(Date() + " comp " + data.src);
    }
    else {
        image.addEventListener('load', function () {
            ctx.save();
            ctx.globalCompositeOperation = "destination-over";
            ctx.drawImage(image, x - r, y - r, r * 2, r * 2);
            ctx.restore();
            // console.log(Date() + " " + data.src);
        }, false);
    }
}
function DrawImageText(ctx, x, y, r, text) {
    ctx.save();
    ctx.font = "12px sans-serif";
    ctx.fillStyle = "purple";
    ctx.font = "bold 12px sans-serif";
    ctx.strokeStyle = "white";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.strokeText(text,x + r /6, y - r * 5 / 6);
    ctx.fillText(text, x + r / 6, y - r * 5 / 6);
    ctx.restore();

}
function Draw2ndRow(ctx, x, y, r, data, Xoffset, Yoffset, flag) {
    if (flag === 0) {//1行版
        DrawImage(ctx, x, y, r, data);
    }
    else {
        if (x < Xoffset) {
            DrawImage(ctx, x, y, r, data);
            //一段目の一番右の場合で、更にタマの左半分が上端にある場合は、下段も描写したい
            if (x + r / 2 > Xoffset - r) {
                DrawImage(ctx, x - Xoffset, y + Yoffset, r, data);
            }
        }
        else {//下段
            //二段目の一番左の場合で、タマの右半分しか描画出来ていない場合
            if (x < Xoffset + r) {
                DrawImage(ctx, x, y, r, data);
            }
            DrawImage(ctx, x - Xoffset, y + Yoffset, r, data);
        }
    }
}
function Draw2ndText(ctx, x, y, r, text, Xoffset, Yoffset, flag) {
    if (text === "" || text === null) {
        return;
    }
    if (flag === 0) {//携帯版
        DrawImageText(ctx, x, y, r, text);
    }
    else {
        if (x < Xoffset) {
            DrawImageText(ctx, x, y, r, text);
            //一段目の一番右の場合で、更にタマの左半分が上端にある場合は、下段も描写したい
            if (x + r / 2 > Xoffset - r) {
                DrawImageText(ctx, x - Xoffset, y + Yoffset, r, text);
            }
        }
        else {//下段
            //二段目の一番左の場合で、タマの右半分しか描画出来ていない場合
            if (x < Xoffset + r) {
                DrawImageText(ctx, x, y, r, text);
            }
            DrawImageText(ctx, x - Xoffset, y + Yoffset, r, text);
        }
    }
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
