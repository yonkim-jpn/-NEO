//デバイス判定
var deviceType = 0 ;//1:タッチ 2:マウス
const isIOS = /iP(hone|(o|a)d)/.test(navigator.userAgent);

function detectDeviceType( event ) {
	deviceType = event.changedTouches ? 1 : 2 ;

	document.removeEventListener ( "touchstart", detectDeviceType ) ;
    document.removeEventListener("mousemove", detectDeviceType);

    var str = location.href;//ページ名取得
    if (str.match("/")) {
        if (deviceType !== 2)
            $(".contents").find(".text").css("display", "block");
    }
}

document.addEventListener ( "touchstart", detectDeviceType ) ;
document.addEventListener ( "mousemove", detectDeviceType ) ;

//メモリアデータ格納用
var memoriaAjustadoA = new Array(3);//アビ用
var memoriaAjustadoS = new Array(2);//スキル用

//メモリアの変数をグローバル化
var xM = [20, 60, 100, 140];
var yM = [120, 120, 120];

//精神強化による効果値
var mentalEfectoA = new Array(3);//アビ用
var mentalEfectoS = new Array(3);//スキル用

//データ出力用配列
var salida1 = new Array();
var salida2 = new Array();
var salida3 = new Array();
var salida4 = new Array();
var salida = [salida1, salida2, salida3, salida4];

var salidaAntes;


var escogido;
var escogidoOri;
var xhr1 = new XMLHttpRequest;
(function (handleload) {
    xhr1.addEventListener('load', handleload, false);
    xhr1.open('GET', 'Scripts/magia_json/escogido.json', false);//同期処理。
    xhr1.send(null);
    //xhr.send();
}(function handleLoad(event) {
    var xhr1 = event.target,
        obj = JSON.parse(xhr1.responseText);
    
    personas = obj.personas.length;
    console.log(obj);
    escogidoOri = obj;
    escogido = angular.copy(escogidoOri);
}));
window.onload = function(){
    ////初期設定
    //document.getElementById("MainContent_RadioButtonList666_0").nextSibling.classList.add("accele");
    //document.getElementById("MainContent_RadioButtonList666_1").nextSibling.classList.add("blast");
    //document.getElementById("MainContent_RadioButtonList666_2").nextSibling.classList.add("charge");

    //document.getElementById("MainContent_RadioButtonList667_0").nextSibling.classList.add("accele");
    //document.getElementById("MainContent_RadioButtonList667_1").nextSibling.classList.add("blast");
    //document.getElementById("MainContent_RadioButtonList667_2").nextSibling.classList.add("charge");

    //document.getElementById("MainContent_RadioButtonList668_0").nextSibling.classList.add("accele");
    //document.getElementById("MainContent_RadioButtonList668_1").nextSibling.classList.add("blast");
    //document.getElementById("MainContent_RadioButtonList668_2").nextSibling.classList.add("charge");

    //document.getElementById("MainContent_RadioButtonList669_0").nextSibling.classList.add("accele");
    //document.getElementById("MainContent_RadioButtonList669_1").nextSibling.classList.add("blast");
    //document.getElementById("MainContent_RadioButtonList669_2").nextSibling.classList.add("charge");
};



//ATKとDEFより、基礎ダメージ算出
//ATKに値が入力されていない時のみエラー

function CalcDano(numero,numeroDeConnect) {
    //値取得
    var Atk = [3];
    Atk[0] = Number(escogido.personas[0].atk) || 0;
    Atk[1] = Number(escogido.personas[1].atk) || 0;
    Atk[2] = Number(escogido.personas[2].atk) || 0;
    var mAtk = [3];
    mAtk[0] = Number(escogido.personas[0].mAtk) || 0;
    mAtk[1] = Number(escogido.personas[1].mAtk) || 0;
    mAtk[2] = Number(escogido.personas[2].mAtk) || 0;
    var menteAtk = [0,0,0];//精神強化分
    if ($("#MainContent_espiritu").prop("checked")) {
        menteAtk[0] = Number(escogido.personas[0].menteATK) || 0;
        menteAtk[1] = Number(escogido.personas[1].menteATK) || 0;
        menteAtk[2] = Number(escogido.personas[2].menteATK) || 0;
    }
    var Def = Number(escogido.personas[3].def) || 0;
    var ignoraDef = new Array(3);//出力補助文字列
    //防御無視が入っている場合は0にする
    if (typeof connectAjustado[numeroDeConnect][5] !== "undefined") {
        Def = 0;
        ignoraDef[numeroDeConnect] = 100;
    }
    if (typeof memoriaAjustadoA[numero] !== "undefined") {
        if (memoriaAjustadoA[numero][5] === "必ず") {
            Def = 0;
        }
    }
    var mDef = Number(escogido.personas[3].mDef) || 0;
    //覚醒補正
    var atkDespierto = 0;
    var defDespierto = 0;
    
    if (color[numero][1] === 1) {
        atkDespierto = valorAjustado[numero][1];
    }
    atkDespierto = atkDespierto === 0 ? 0 : atkDespierto / 100;
    //相手側DEFにより
    if (color2[0] === 1) {
        defDespierto = valorAjustado2[0]/100;
    }

    //陣形補正
    var ordenAtk = [3];
    ordenAtk[0] = escogido.personas[0].jinkei || 0;
    ordenAtk[1] = escogido.personas[1].jinkei || 0;
    ordenAtk[2] = escogido.personas[2].jinkei || 0;
    var ordenDef = escogido.personas[3].jinkei || 0;
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
    var AtkUp = [0,0,0];
    
    var DefUp = Number(Zenhan($('#MainContent_DefUp').val())) || 0;
    //防御力補正 簡単じゃない
    //スキルを使った場合のみ有効になる
    for (let i = 0; i < 2; i++) {
        if (typeof memoriaAjustadoS[numero] !== "undefined") {
            if (memoriaAjustadoS[numero][i][6] > 0) {
                DefUp -= memoriaAjustadoS[numero][i][6];
            }
        }
    }

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
    //メモリア補正
    if (typeof memoriaAjustadoA[numero] !== "undefined") {
        AtkUp[numero] += memoriaAjustadoA[numero][0] || 0;
    }
    //精神補正
    //アビ
    if (mentalEfectoA[numero][0] !== null) {
        AtkUp[numero] += mentalEfectoA[numero][0] || 0;
    }
    AtkUp[numero] = (AtkUp[numero] > 200) ? 200 : AtkUp[numero];
    AtkUp[numero] = ((AtkUp[numero] < 5) && (AtkUp[numero] > -5)) ? 0 : AtkUp[numero];
    AtkUp[numero] = (AtkUp[numero] < -100) ? -100 : AtkUp[numero];
    DefUp = (DefUp > 200) ? 200 : DefUp;
    DefUp = ((DefUp < 5) && (DefUp > -5)) ? 0 : DefUp;
    DefUp = (DefUp < -100) ? -100 : DefUp;

    //出力データ作成
    salida[numeroDeConnect].unshift(GetSalida("攻撃力",AtkUp[numero]));
    salida[numeroDeConnect].push(GetSalida("防御無視",ignoraDef[numeroDeConnect]));

    AtkUp[numero] = 1 + AtkUp[numero] / 100;
    DefUp = 1 + DefUp / 100;

    //UPDOWN補正を加味して最終値出力
    var AtkReal = (Atk[numero] * (1 + atkDespierto) + mAtk[numero] + menteAtk[numero]) * AtkUp[numero] * ordenAtk[numero];
    var DefReal = (Def * (1 + defDespierto) + mDef) * DefUp * ordenDef;
    var dano = AtkReal - DefReal / 3;

    return dano;
}


//補正ダメージ計算関数
//補正係数は複雑なので、いったん考えない（2019/12/04）
function CalcAjustado(eleccion1, eleccion2, eleccion3, orden) {
    var eleccion = [eleccion1, eleccion2, eleccion3];
    var ventana = $("input[name='ctl00$MainContent$ventana']:checked").val();

    //1人攻撃の場合のメモリア選択用
    var ordenM = orden;
    if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "1")
        ordenM = 1;

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
                if ((ratioCori > 0)&&(eleccion[0] !== "C"))
                    chargeFlag = 1;
                break;
            }
        case 2:
            {
                if (eleccion[0] !== "C") {
                    calcC = 1;
                    cmp = 1;
                }
                else if(eleccion[1] !== "C")
                    chargeFlag = 1;
                break;
            }
        case 3:
            {
                if (eleccion[1] !== "C") {
                    calcC = 1;
                    cmp = 1;
                }
                else if(eleccion[2] !== "C")
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
    var tipoPuella = escogido.personas[ordenM-1].type;
    var calcMpB = 1;
    switch (tipoPuella) {
        case "マギア":
        case "円環マギア":
        case "サポート":
        case "円環サポート":
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
        case "エクシード"://仮
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
    var MpUpM = 0;
    if (typeof memoriaAjustadoA[ordenM - 1] !== "undefined")
        MpUpM = memoriaAjustadoA[ordenM - 1][11] || 0;
    var calcMpC = MpUpM === 0 ? 100 : 100 + MpUpM;
    //コネクト側
    var MpUpC = connectAjustado[orden - 1][11];
    if (typeof MpUpC === "undefined")
        MpUpC = 0;
    //精神強化補正
    var MpUpMental = 0;
    var Mp100UpMental = 0;
    if ($("#MainContent_espiritu").prop("checked")){
        MpUpMental = mentalEfectoA[ordenM - 1][11] || 0;
        //現在MP100以上の時入力
        Mp100UpMental = mentalEfectoA[ordenM - 1][14] || 0;
    }
    calcMpC += MpUpC + MpUpMental;
    calcMpC /= 100;

    //D:AccelMPUP
    //メモリア側 アクセルのみかかる
    // var AMpUpM;
    // switch (orden) {
    //     case 1:
    //         {
    //             // AMpUpM = Number($("#MainContent_AMpUp").val());
    //             break;
    //         }
    //     case 2:
    //         {
    //             // AMpUpM = Number($("#MainContent_AMpUp2").val());
    //             break;
    //         }
    //     case 3:
    //         {
    //             // AMpUpM = Number($("#MainContent_AMpUp3").val());
    //             break;
    //         }
    // } 
    // AMpUpM = 0;
    // var calcMpD = AMpUpM === 0 ? 1 : 1 + (7.5 + 2.5 * AMpUpM) / 100;
    //コネクト側
    var AMpUpC = connectAjustado[orden - 1][10];
    if (typeof AMpUpC === "undefined")
        AMpUpC = 0;
    //メモリア側
    var AMpUpM = 0;
    if (typeof memoriaAjustadoA[ordenM - 1] !== "undefined")
        AMpUpM = memoriaAjustadoA[ordenM - 1][10] || 0;
    //精神強化補正
    var AMpUpMental = 0;
    if ($("#MainContent_espiritu").prop("checked"))
        AMpUpMental = mentalEfectoA[ordenM -1][10];
    var calcMpD = 1 + (AMpUpC + AMpUpM + AMpUpM) / 100;
    calcMpD = eleccion[orden - 1] === "A" ? calcMpD : 1;


    //チャージ数による補正
    //そらまめ氏の指摘により、CとDの計算順を変更し、D処理後、小数点以下2位を切り上げ
    calcMp = Math.ceil(calcMp * calcMpD * 10) / 10;
    calcMp = Math.floor(calcMp * calcMpC * cmp * 10) / 10;

    //MP回復処理
    var aumentoMP = connectAjustado[orden - 1][12];
    if (typeof aumentoMP === "undefined")
        aumentoMP = 0;
    if (charaSelected[orden -1] === "天音　月夜")
        aumentoMP += (typeof connectAjustado[orden - 1][14] !== "undefined") ? connectAjustado[orden - 1][14] : 0;
    //精神強化補正
    //Blast攻撃時MP獲得
    if ($("#MainContent_espiritu").prop("checked")){
    if(eleccion[orden - 1] === "B")
        aumentoMP += mentalEfectoA[ordenM - 1][15] || 0;
    }
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
        case "光":
            {
                switch(mentalEfectoA[ordenM-1][26]){
                    case "闇":
                        {
                            calcD = 1.5;
                            break;
                        }
                    default:
                        {
                            calcD = 1;
                            break;
                        }
                }
                break;
            }
        case "闇":
            {
                switch(mentalEfectoA[ordenM-1][26]){
                    case "光":
                        {
                            calcD = 1.5;
                            break;
                        }
                    default:
                        {
                            calcD = 1;
                            break;
                        }
                }
                break;
            }
        case "火":
            {
                switch(mentalEfectoA[ordenM-1][26]){
                    case "水":
                        {
                            calcD = 1.5;
                            break;
                        }
                    case "木":
                        {
                            calcD = 0.5;
                            break;
                        }
                    default:
                        {
                            calcD = 1;
                            break;
                        }
                }
                break;
            }
        case "木":
            {
                switch(mentalEfectoA[ordenM-1][26]){
                    case "火":
                        {
                            calcD = 1.5;
                            break;
                        }
                    case "水":
                        {
                            calcD = 0.5;
                            break;
                        }
                    default:
                        {
                            calcD = 1;
                            break;
                        }
                }
                break;
            }
        case "水":
            {
                switch(mentalEfectoA[ordenM-1][26]){
                    case "木":
                        {
                            calcD = 1.5;
                            break;
                        }
                    case "火":
                        {
                            calcD = 0.5;
                            break;
                        }
                    default:
                        {
                            calcD = 1;
                            break;
                        }
                }
                break;
            }
        case "無"://並盛
            {
                calcD = 1;
                break;
            }
    }



    //e 補正係数
    var calcE = 1;

    //各種補正値の取得
    var t = connectAjustado[orden - 1][2]//Charge後ダメージUP
    if (typeof t === "undefined")
        t = 0;
    if(typeof memoriaAjustadoA[ordenM-1]!=="undefined")
        t += memoriaAjustadoA[ordenM - 1][2] || 0;
    if ($("#MainContent_espiritu").prop("checked")){
        //精神強化
            if (typeof mentalEfectoA[ordenM - 1][2] !== "undefined") {
            t += mentalEfectoA[ordenM - 1][2];
        }

    }
    t = t > 100 ? 100 : t;
    var u = connectAjustado[orden - 1][1];//与えるダメージUP
    if (typeof u === "undefined")
        u = 0;
    if(typeof memoriaAjustadoA[ordenM-1]!=="undefined")
        u += memoriaAjustadoA[ordenM - 1][1] || 0;
    if ($("#MainContent_espiritu").prop("checked")) {
        //精神強化
        if (typeof mentalEfectoA[ordenM - 1][1] !== "undefined") {
            u += mentalEfectoA[ordenM - 1][1];
        }
    }
    u = u > 100 ? 100 : u;
    var v = connectAjustado[orden - 1][3];//BlastダメージUP
    if (typeof v === "undefined")
        v = 0;
    if(typeof memoriaAjustadoA[ordenM-1]!=="undefined")
        v += memoriaAjustadoA[ordenM - 1][3] || 0;
    if ($("#MainContent_espiritu").prop("checked")) {
        //精神強化
        if (typeof mentalEfectoA[ordenM - 1][3] !== "undefined") {
            v += mentalEfectoA[ordenM - 1][3];
        }
    }
    v = v > 100 ? 100 : v;
    var t2 = 0;//ChargeディスクダメージUP
    if ($("#MainContent_espiritu").prop("checked")) {
        //精神強化
        if (typeof mentalEfectoA[ordenM - 1][19] !== "undefined") {
            t2 += mentalEfectoA[ordenM - 1][19];
        }
    }
    t2 = t2 > 100 ? 100 : t2;

    var w = 0;//ダメージアップ状態
    let x = 0;//敵状態異常時ダメージアップ
    let y = 0;//ダメージカット 守備側
    if (typeof memoriaAjustadoA[ordenM - 1] !== "undefined") {
        w += memoriaAjustadoA[ordenM - 1][7] || 0;
        x += memoriaAjustadoA[ordenM - 1][4] || 0;
    }
    if ($("#MainContent_espiritu").prop("checked")) {
        //精神強化
        w += mentalEfectoA[ordenM - 1][7] || 0;
        x += mentalEfectoA[ordenM - 1][4] || 0;
    }
    var zProbabilidad = connectAjustado[orden - 1][13];//クリティカル確率
    if (typeof zProbabilidad === "undefined")
        zProbabilidad = 0;
    if (typeof memoriaAjustadoA[ordenM - 1] !== "undefined") {
        var memoriaCri = memoriaAjustadoA[ordenM - 1][13] || 0;
    }
    if ($("#MainContent_espiritu").prop("checked")) {
        //精神強化
        //未コーディング
    }
    zProbabilidad = Math.max(zProbabilidad, memoriaCri);
    

    //出力データ作成
    salida[orden-1].unshift(GetSalida("ダメUP状態",w));
    salida[orden-1].unshift(GetSalida("C板ダメ",t2));
    salida[orden-1].unshift(GetSalida("C後ダメ",t));
    salida[orden-1].unshift(GetSalida("Bダメ", v));
    salida[orden-1].unshift(GetSalida("与ダメ",u));//これで配列の先頭に入る
    
    //t,t2とvはディスク構成によるので確認する
    t = chargeFlag === 1 ? t : 0;
    t2 = eleccion[orden - 1] === "C" ? t2 : 0;
    v = eleccion[orden - 1] === "B" ? v : 0;

    var calcX = 100 + t + t2 + u + v + w + x ;
    calcX = calcX > 300 ? 300 : calcX;

    calcE = (calcX - y ) / 100;
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
var limite = 31;
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
                                            if (typeof connectAjustado[i - 1][j] !== "undefined" && (j !== 12)&& (j !== 14)) {//MP回復のみ別処理
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
                                            if (typeof connectAjustado[i - 2][j] !== "undefined" && (j !== 12)&& (j !== 14)) {//MP回復のみ別処理
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
                                            if (typeof connectAjustado[i - 1][j] !== "undefined" && (j !== 12)&& (j !== 14)) {//MP回復のみ別処理
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
                                            if (typeof connectAjustado[i - 1][j] !== "undefined" && (j !== 12)&& (j !== 14)) {//MP回復のみ別処理
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
                            if (typeof connectAjustado[i - 1][j] !== "undefined"  && (j !== 12)&& (j !== 14)) {//MP回復のみ別処理
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
    let s;
    switch (name) {
        case "防御無視":
            {
                data = data > 100 ? 100 : data;
                s = [name, String(data) + "%"];
                break;
            }
        default:
            {
                s = [name, "+" + String(data) + "%"];
            }
    }
    
    
    return s;
}



//表示処理関数
function IndicaResultado() {
    //出力初期化
    let d1 = new Array();
    let d2 = new Array();
    let d3 = new Array();
    let d4 = new Array();
    salida = [d1, d2, d3, d4];//以下の過程を通ることによって、各関数で出力結果を書き込んでいく
    
    //ラジオボタン選択状態の取得
    var eleccion1 = $("input[name='ctl00$MainContent$RadioButtonList666']:checked").val();
    var eleccion2 = $("input[name='ctl00$MainContent$RadioButtonList667']:checked").val();
    var eleccion3 = $("input[name='ctl00$MainContent$RadioButtonList668']:checked").val();

    //コネクト効果作成
    GenerateConnect();

    //精神強化効果作成
    GenerateMentalEfecto();

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
                        ajusteDespierto[i] += valorAjustado[numero][3];
                    if ($("#MainContent_espiritu").prop("checked")) 
                        ajusteDespierto[i] += mentalEfectoA[numero][23] || 0;
                    break;
                }
            case "B":
                {
                    if (color[numero][5] === 1)
                        ajusteDespierto[i] = valorAjustado[numero][5];
                    if ($("#MainContent_espiritu").prop("checked")) 
                        ajusteDespierto[i] += mentalEfectoA[numero][24] || 0;
                    break;
                }
            case "C":
                {
                    if (color[numero][4] === 1)
                        ajusteDespierto[i] = valorAjustado[numero][4];
                    if ($("#MainContent_espiritu").prop("checked")) 
                        ajusteDespierto[i] += mentalEfectoA[numero][25] || 0;
                    break;
                }

        }
        
        ajusteDespierto[i] /= 100;
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
    
    if (typeof salidaAntes === "undefined")
        salidaAntes = new Array(3);
    if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
        IndicaModalSalida(salida[0], salidaAntes[0], "grid1m");
        salidaAntes[0] = angular.copy(salida[0]);
    }
    else{
        IndicaModalSalida(salida[1], salidaAntes[1], "grid2m");
        IndicaModalSalida(salida[2], salidaAntes[2], "grid3m");
        salidaAntes = angular.copy(salida);
    }
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

function IndicaModalSalida(data, dataAntes, g) {
    let grid = document.getElementById(g);
    let index = g.substring(4,5);

    

    //antesが無い場合もある
    if (typeof dataAntes === "undefined")
        dataAntes = new Array(data.length);
    //出力用文字列再構成
    var salidaF = [];
    for (let i = 0; i < data.length; i++){
        if (typeof dataAntes[i] === "undefined") {
            if (data[i][0] === "MP回復量")
                dataAntes[i] = ["", "+0"];
            else if (data[i][0] === "防御無視")
                dataAntes[i] = ["", "0%"];
            else
                dataAntes[i] = ["", "+0%"];
        }
        if (data[i][1] === dataAntes[i][1]) {
            //差が無くて値が入っている場合
            let v = data[i][1].replace(/[%+]/g, "");
            if (v !== "0") {
                let s = "+" + v + "%";
                if (data[i][0] === "MP回復量") {
                    s = "+" + v;
                } else if (data[i][0] === "防御無視")
                    s = v + "%";
                salidaF.push([data[i][0], s]);
            }
            continue;
        }
        //前と値が変化した場合のみ処理
        let v = data[i][1].replace(/[%+]/g, "");
        let vA = dataAntes[i][1].replace(/[%+]/g, "");
        if (vA < v) {
            var s = "+" + v + "% (+" + (v - vA) + "%)";
            if (data[i][0] === "MP回復量")
                s = "+" + v + " (+" + (v - vA) + ")";
            else if (data[i][0] === "防御無視")
            s = v + "% (+" + (v - vA) + "%)";
            salidaF.push([data[i][0], s]);
        }
        else {
            var s = "+" + v + "% (-" + (vA - v) + "%)";
            if (data[i][0] === "MP回復量")
                s = "+" + v + " (-" + (vA - v) + ")";
            else if (data[i][0] === "防御無視")
                s = v + "% (-" + (vA - v) + "%)";
            salidaF.push([data[i][0], s]);
        }
    }

    if (salidaF.length === 0)
        return;    
    var hot1 = new Handsontable(grid, {//前のテーブルともども消すためのダミー
        data: [1,1]
    });
    hot1.destroy();//テーブル削除

    var sizeH = 40 + salidaF.length * 30;
    hot1 = new Handsontable(grid, {//本番のテーブル
        data: [],
        rowHeaders: false,
        colHeaders: ['補正' + index, '値' + index],
        columnSorting: false,
        readOnly: true,
        height:sizeH,
        currentRowClassName: 'currentRow',
        currentColClassName: 'currentCol',        
        className: "htCenter",
        licenseKey: 'non-commercial-and-evaluation'
    });
    hot1.loadData(salidaF);

}

function IndicaData(data, g) {
    let grid = document.getElementById(g);

    var hot1 = new Handsontable(grid, {//前のテーブルともども消すためのダミー
        data: []
    });
    hot1.destroy();//テーブル削除
    hot1 = new Handsontable(grid, {//本番のテーブル
        data: [],
        rowHeaders: ['名前','ATK','DEF','HP'],
        colHeaders: false,
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
    
    
    $('input[name="ctl00$MainContent$TextBox668"]').change(function () {
        escogido.personas[3].def = Number(Zenhan($("#MainContent_TextBox668").val()));
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$TextBox669"]').change(function () {
        escogido.personas[3].mDef = Number(Zenhan($("#MainContent_TextBox669").val()));
        IndicaResultado();
    });


    $('input[name="ctl00$MainContent$ventana"]:radio').change(function () {
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$estadoAtk"]:radio').change(function () {
        
        var e0 = document.getElementById("MainContent_seleccionado_0");
        var e1 = document.getElementById("MainContent_seleccionado_1");
        var e2 = document.getElementById("MainContent_seleccionado_2");
        
        if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {//同一キャラで攻撃
            //キャラ選択で、2人目3人目がチェックされている時は1人目選択に変更する
            if (($('input[name="ctl00$MainContent$seleccionado"]:checked').val() === "2") || ($('input[name="ctl00$MainContent$seleccionado"]:checked').val() === "3")) {
                $('input:radio[name="ctl00$MainContent$seleccionado"]').val(["1"]);
                //ハイライト追加
                document.getElementById("MainContent_seleccionado_0").nextSibling.classList.add("highlight");
                //ハイライト削除
                document.getElementById("MainContent_seleccionado_1").nextSibling.classList.remove("highlight");
                document.getElementById("MainContent_seleccionado_2").nextSibling.classList.remove("highlight");
    
            }
            //ピュエラの時は、2人目以降の選択不可
            e0.disabled = false;
            e1.disabled = true;
            e2.disabled = true;
            let iData;

            //canvasコピー
            var canvas61 = document.getElementById("canvas61");
            if (!canvas61 || !canvas61.getContext) {
                return;
            }
            var canvas62 = document.getElementById("canvas62");
            if (!canvas62 || !canvas62.getContext) {
                return;
            }
            var ctx62 = canvas62.getContext("2d");
            ctx62.clearRect(0, 0,160, 120);
            ctx62.drawImage(canvas61, 0, 0, 160, 120, 0, 0, 160, 120);

            var canvas63 = document.getElementById("canvas63");
            if (!canvas63 || !canvas63.getContext) {
                return;
            }
            var ctx63 = canvas63.getContext("2d");
            ctx63.clearRect(0, 0,160, 120);
            ctx63.drawImage(canvas61, 0, 0, 160, 120, 0, 0, 160, 120);
            

            //名前コピー
            var nombre = document.getElementById("MainContent_seleccionado_0").nextSibling.innerText;
            if (nombre === "1人目 : 無") {
                nombre = "";//何もしない
                //コネクト入ってれば。。。
                var charaR = 30;
                if (typeof connectElegido[0][30] !== "undefined") {
                    var image1 = new Image();
                    image1.src = connectElegido[0][30];
                    DrawImage(ctx61, 30, 30, charaR, image1);
                }
                if (typeof connectElegido[1][30] !== "undefined") {
                    var image2 = new Image();
                    image2.src = connectElegido[1][30];
                    DrawImage(ctx62, 30, 30, charaR, image2);
                }
                if (typeof connectElegido[2][30] !== "undefined") {
                    var image3 = new Image();
                    image3.src = connectElegido[2][30];
                    DrawImage(ctx63, 30, 30, charaR, image3);
                }
            }
            else {
                nombre = nombre.substring(nombre.indexOf(" : ") + 3);
                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : " + nombre;
                document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : " + nombre;
                document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : " + nombre;
                document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : " + nombre;
                
                //コネクト入ってれば。。。
                if (typeof connectElegido[0][30] !== "undefined") {
                    var canvas61 = document.getElementById("canvas61");
                    if (!canvas61 || !canvas61.getContext) {
                        return;
                    }
                    var ctx61 = canvas61.getContext("2d");
                    ctx61.drawImage(canvas61, 0, 0, 60, 60, 90, 30, 30, 30);
                }
                if (typeof connectElegido[1][30] !== "undefined") {
                    var canvas62 = document.getElementById("canvas62");
                    if (!canvas62 || !canvas62.getContext) {
                        return;
                    }
                    var ctx62 = canvas62.getContext("2d");
                    ctx62.drawImage(canvas62, 0, 0, 60, 60, 90, 30, 30, 30);
                }
                if (typeof connectElegido[2][30] !== "undefined") {
                    var canvas63 = document.getElementById("canvas63");
                    if (!canvas63 || !canvas63.getContext) {
                        return;
                    }
                    var ctx63 = canvas63.getContext("2d");
                    ctx63.drawImage(canvas63, 0, 0, 60, 60, 90, 30, 30, 30);
                }
                //アイコン、データコピー
                // let image2a = document.getElementById('2a');
                // image2a.innerHTML = '<img src=' + "png/" + escogido.personas[0].data1 + ' alt=\"選択無\" >';
                iData = [[escogido.personas[0].nickName], [escogido.personas[0].atk], [escogido.personas[0].def], [escogido.personas[0].hp]];
                IndicaData(iData, "grid2a");
                // let image3a = document.getElementById('3a');
                // image3a.innerHTML = '<img src=' + "png/" + escogido.personas[0].data1 + ' alt=\"選択無\" >';
                iData = [[escogido.personas[0].nickName], [escogido.personas[0].atk], [escogido.personas[0].def], [escogido.personas[0].hp]];
                IndicaData(iData, "grid3a");
                
                
            }
            
            


            //アコーデオン閉じ
            $("#nombre23").collapse('hide');
        }
        else {//3キャラ選択

            //ハイライト処理(初期処理)
            if ($('input:radio[name="ctl00$MainContent$seleccionado"]:checked').val() === "1")
                document.getElementById("MainContent_seleccionado_0").nextSibling.classList.add("highlight");

            e0.disabled = false;
            e1.disabled = false;
            e2.disabled = false;
            //名前を下から拾う処理
            let w = $(window).width();
            let wSize = 768;
            var n2;
            var n3;
            var charaR = 30;
            if (w < wSize) {
                n2 = escogido.personas[1].nickName;
                n3 = escogido.personas[2].nickName;
            }
            else {
                n2 = escogido.personas[1].name;
                n3 = escogido.personas[2].name;
            }
            //n2,n3に値があれば入れる
            if (n2 === "") {
                n2 = "無";
                //アイコン、データを無表示
                // let image2a = document.getElementById('2a');
                // image2a.innerHTML = "未選択";
                canvas62 = document.getElementById("canvas62");
                if (!canvas62 || !canvas62.getContext) {
                    return;
                }
                var ctx62 = canvas62.getContext("2d");
                ctx62.clearRect(0, 0, 120, 60);
                if (typeof connectElegido[1][30] !== "undefined") {
                    var image2 = new Image();
                    image2.src = connectElegido[1][30];
                    DrawImage(ctx62, 90, 30, charaR, image2);
                }

                iData = [[""], [""], [""], [""]];
                IndicaData(iData, "grid2a");
            }
            else {
                // n2 = n2.substring(n2.indexOf(" : ") + 1);
                //アイコン、データ復元
                // let image2a = document.getElementById('2a');
                // image2a.innerHTML = '<img src=' + "png/" + escogido.personas[1].data1 + ' alt=\"選択無\" >';
                canvas62 = document.getElementById("canvas62");
                if (!canvas62 || !canvas62.getContext) {
                    return;
                }
                var ctx62 = canvas62.getContext("2d");
                var image2 = new Image();
                image2.src = "png/" + escogido.personas[1].data1;
                DrawImage(ctx62, 30, 30, 30, image2);
                //コネクト入ってれば。。。
                if (typeof connectElegido[1][30] !== "undefined") {
                    ctx62.drawImage(canvas62, 0, 0, 60, 60, 90, 30, 30, 30);
                }
                iData = [[escogido.personas[1].nickName], [escogido.personas[1].atk], [escogido.personas[1].def], [escogido.personas[1].hp]];
                IndicaData(iData, "grid2a");
            }
            if (n3 === "") {
                n3 = "無";
                 //アイコン、データを無表示
                //  let image3a = document.getElementById('3a');
                //  image3a.innerHTML = "未選択";
                 canvas63 = document.getElementById("canvas63");
                 if (!canvas63 || !canvas63.getContext) {
                     return;
                 }
                 var ctx63 = canvas63.getContext("2d");
                ctx63.clearRect(0, 0, 120, 60);
                if (typeof connectElegido[2][30] !== "undefined") {
                    var image3 = new Image();
                    image3.src = connectElegido[2][30];
                    DrawImage(ctx63, 90, 30, charaR, image3);
                }
                 iData = [[""], [""], [""], [""]];
                 IndicaData(iData, "grid3a");
            }
            else {
                // n3 = n3.substring(n3.indexOf(" : ") + 1);
                 //アイコン、データ復元
                // let image3a = document.getElementById('3a');
                // image3a.innerHTML = '<img src=' + "png/" + escogido.personas[2].data1 + ' alt=\"選択無\" >';
                canvas63 = document.getElementById("canvas63");
                if (!canvas63 || !canvas63.getContext) {
                    return;
                }
                var ctx63 = canvas63.getContext("2d");
                var image3 = new Image();
                image3.src = "png/" + escogido.personas[2].data1;
                DrawImage(ctx63, 30, 30, 30, image3);
                //コネクト入ってれば。。。
                if (typeof connectElegido[2][30] !== "undefined") {
                    ctx63.drawImage(canvas63, 0, 0, 60, 60, 90, 30, 30, 30);
                }
                iData = [[escogido.personas[2].nickName], [escogido.personas[2].atk], [escogido.personas[2].def], [escogido.personas[2].hp]];
                IndicaData(iData, "grid3a");
            }
            document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : " + n2;
            document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : " + n3;
            document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : " + n2;
            document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : " + n3;
            
            //canvas復活
            RegenerateCharaMemoria();

            //覚醒のアコーデオン開く
            $("#nombre23").collapse('show');
           

            
        }
        IndicaResultado();
    });
    $('input[name="ctl00$MainContent$seleccionado"]:radio').change(function () {
        //ハイライト解除
        document.getElementById("MainContent_seleccionado_0").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_seleccionado_1").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_seleccionado_2").nextSibling.classList.remove("highlight");
        document.getElementById("radioConnect1").nextElementSibling.classList.remove("highlight");
        document.getElementById("radioConnect2").nextElementSibling.classList.remove("highlight");
        document.getElementById("radioConnect3").nextElementSibling.classList.remove("highlight");
        
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
            case "connect1":
                {
                    document.getElementById("radioConnect1").nextElementSibling.classList.add("highlight");
                    break;
                }
            case "connect2":
                {
                    document.getElementById("radioConnect2").nextElementSibling.classList.add("highlight");
                    break;
                }
            case "connect3":
                {
                    document.getElementById("radioConnect3").nextElementSibling.classList.add("highlight");
                    break;
                }
        }
    })
    
    
    $('input[name="ctl00$MainContent$decimal"]').change(function () {
        IndicaResultado();
    });

    // $('input[name="ctl00$MainContent$espiritu"]').change(function () {
    //     if ($("#MainContent_espiritu").prop("checked"))
    //         $("#MainContent_espiritu2").prop("checked", true);
    //     else
    //         $("#MainContent_espiritu2").prop("checked", false);
    //     //HP,ATK,DEFのみソートしなおし
    //     let tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
    //     switch(tipoOrden){
    //         case "HP":
    //         case "ATK":
    //         case "DEF":
    //             {
    //                 let ordena = OrdenaMagia();
    //                 ReDraw(ordena);
    //                 break;
    //             }
    //     }
    //     IndicaResultado();
    // });

    // $('input[name="ctl00$MainContent$espiritu2"]').change(function () {
    //     if ($("#MainContent_espiritu2").prop("checked"))
    //         $("#MainContent_espiritu").prop("checked", true);
    //     else
    //         $("#MainContent_espiritu").prop("checked", false);
    //     //HP,ATK,DEFのみソートしなおし
    //     let tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
    //     switch(tipoOrden){
    //         case "HP":
    //         case "ATK":
    //         case "DEF":
    //             {
    //                 let ordena = OrdenaMagia();
    //                 ReDraw(ordena);
    //                 break;
    //             }
    //     }
    //     IndicaResultado();
    // });

    //陣形補正、メモリア攻撃力UPの取得
    $('input[name="ctl00$MainContent$ordendeBatalla4"]:radio').change(function () {
        IndicaResultado();
    });
    
    $('input[name="ctl00$MainContent$DefUp"]').change(function () {
        IndicaResultado();

        //表示処理
        var DefUp = Number(Zenhan($('#MainContent_DefUp').val())) || 0;
        DefUp = ChequeaPorcentaje(DefUp);
        ChequeaLimite(DefUp, 'MainContent_DefUp');

        $('#MainContent_DefUp').val(DefUp);
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
    draw(0);//攻撃側の覚醒
    draw(1);
    draw(2);
    draw2();//防衛側の覚醒
    draw3();
    draw5();
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
var mpersonas;
var menteDataOri;
var menteData;

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
// 14:月夜にコネクトでMPさらに回復:25回復


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
        // jsonData = angular.copy(jsonDataOri);
    }));

    var xhr2 = new XMLHttpRequest;
    (function (handleload) {//精神強化読み込み
        // var xhr = new XMLHttpRequest;

        xhr2.addEventListener('load', handleload, false);
        xhr2.open('GET', 'Scripts/magia_json/sub_mente.json', false);//同期処理。
        xhr2.send(null);
        //xhr.send();
    }(function handleLoad(event) {
        var xhr2 = event.target,
            obj = JSON.parse(xhr2.responseText);
        
        mpersonas = obj.personas.length;
        // console.log(obj);
        menteDataOri = obj;
        // menteData = angular.copy(menteDataOri);
    }));

    //精神強化データの融合
    for (let i = 0; i < personas; i++){
        for (let j = 0; j < mpersonas; j++) {
            if (jsonDataOri.personas[i].name === menteDataOri.personas[j].name2) {
                Object.assign(jsonDataOri.personas[i], menteDataOri.personas[j]);
            }
        }
    }

    jsonData = angular.copy(jsonDataOri);

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
    var region = { x: 0, y: 0, w: canvas3.width, h: canvas3.height };
    

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
            if (clickX > canvas3.width) {//ドラッグした先が右端を超えた場合
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
                    case "精神スキル":
                        {
                            let sortValue = jsonData.personas[i].sortValue1;
                            sortValue = (jsonData.personas[i].sortValue2 !== null)&&(typeof jsonData.personas[i].sortValue2 !=="undefined") ? sortValue + " " + jsonData.personas[i].sortValue2 : sortValue;
                            Draw2ndText(ctx3, charaX[i], charaY, charaR, sortValue, Xoffset, Yoffset, canvasFlag);
                            break;
                        }
                    case "精神アビ":
                        {
                            let text = "";
                            if (ChangeToRoman(jsonData.personas[i].sortValue1)) {
                                if ($("#MainContent_apareceAbiDetalle").prop("checked"))
                                    text = jsonData.personas[i].subValue;
                                else
                                    text = ChangeToRoman(jsonData.personas[i].sortValue1);
                            }
                            else
                                text = jsonData.personas[i].subValue;
                            Draw2ndText(ctx3, charaX[i], charaY, charaR, text, Xoffset, Yoffset, canvasFlag);
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
                            else {//アイコンがない時の処理。今は使ってない
                                DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
                                DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
                            }
                        }

                        
                        //キャラ名表示
                        if (document.getElementById("MainContent_seleccionado_0").checked) {
                            document.getElementById("MainContent_seleccionado_0").nextSibling.innerText = "1人目 : 無";
                            document.getElementById("MainContent_nombre1").innerText = "攻撃側1人目 : 選択無";
                            //アイコン画像表示
                            // let image1a = document.getElementById('1a');
                            // image1a.innerHTML = "未選択";
                            canvas61 = document.getElementById("canvas61");
                            if (!canvas61 || !canvas61.getContext) {
                                return;
                            }
                            var ctx61 = canvas61.getContext("2d");
                            ctx61.clearRect(0, 0, 60, 60);
                            //コネクト入ってれば。。
                            if (typeof connectElegido[0][30] !== "undefined") {
                                var image1 = new Image();
                                image1.src = connectElegido[0][30];
                                ctx61.clearRect(0, 0, 120, 60);
                                DrawImage(ctx61, 90, 30, charaR, image1);
                            }
                            let iData = [[""], [""], [""], [""]];
                            IndicaData(iData, "grid1a");
                            //選択配列初期化
                            escogido.personas[0] = angular.copy(escogidoOri.personas[0]);
                            if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
                                //ピュエラコンボの場合
                                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : 無";
                                document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : 無";
                                //アイコン画像表示
                                // let image2a = document.getElementById('2a');
                                // image2a.innerHTML = "未選択";
                                canvas62 = document.getElementById("canvas62");
                                if (!canvas62 || !canvas62.getContext) {
                                    return;
                                }
                                var ctx62 = canvas62.getContext("2d");
                                ctx62.clearRect(0, 0, 60, 60);
                                
                                iData = [[""], [""], [""], [""]];
                                IndicaData(iData, "grid2a");
                                //アイコン画像表示
                                // let image3a = document.getElementById('3a');
                                // image3a.innerHTML = "未選択";
                                canvas63 = document.getElementById("canvas63");
                                if (!canvas63 || !canvas63.getContext) {
                                    return;
                                }
                                var ctx63 = canvas63.getContext("2d");
                                ctx63.clearRect(0, 0, 60, 60);
                                iData = [[""], [""], [""], [""]];
                                IndicaData(iData, "grid3a");
                                //コネクト入ってれば。。。
                                if (typeof connectElegido[1][30] !== "undefined") {
                                    var image2 = new Image();
                                    image2.src = connectElegido[1][30];
                                    ctx62.clearRect(0, 0, 120, 60);
                                    DrawImage(ctx62, 90, 30, charaR, image2);
                                }
                                if (typeof connectElegido[2][30] !== "undefined") {
                                    var image3 = new Image();
                                    image3.src = connectElegido[2][30];
                                    ctx63.clearRect(0, 0, 120, 60);
                                    DrawImage(ctx63, 90, 30, charaR, image3);
                                }
                            }
                             

                        }
                        else if (document.getElementById("MainContent_seleccionado_1").checked) {
                            document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : 無";
                            document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : 選択無";
                            //アイコン画像表示
                            // let image2a = document.getElementById('2a');
                            // image2a.innerHTML = "未選択";
                            canvas62 = document.getElementById("canvas62");
                            if (!canvas62 || !canvas62.getContext) {
                                return;
                            }
                            var ctx62 = canvas62.getContext("2d");
                            ctx62.clearRect(0, 0, 60, 60);
                            //コネクト入ってれば。。。
                            if (typeof connectElegido[1][30] !== "undefined") {
                                var image2 = new Image();
                                image2.src = connectElegido[1][30];
                                ctx62.clearRect(0, 0, 120, 60);
                                DrawImage(ctx62, 90, 30, charaR, image2);
                            }
                            iData = [[""], [""], [""], [""]];
                            IndicaData(iData, "grid2a");
                            //選択配列初期化
                            escogido.personas[1] = angular.copy(escogidoOri.personas[0]);
                        }
                        else if (document.getElementById("MainContent_seleccionado_2").checked) {
                            obj = document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : 無";
                            document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : 選択無";
                            //アイコン画像表示
                            // let image3a = document.getElementById('3a');
                            // image3a.innerHTML = "未選択";
                            canvas63 = document.getElementById("canvas63");
                            if (!canvas63 || !canvas63.getContext) {
                                return;
                            }
                            var ctx63 = canvas63.getContext("2d");
                            ctx63.clearRect(0, 0, 60, 60);
                            //コネクト入ってれば。。。
                            if (typeof connectElegido[2][30] !== "undefined") {
                                var image3 = new Image();
                                image3.src = connectElegido[2][30];
                                ctx63.clearRect(0, 0, 120, 60);
                                DrawImage(ctx63, 90, 30, charaR, image3);
                            }
                            iData = [[""], [""], [""], [""]];
                            IndicaData(iData, "grid3a");
                            //選択配列初期化
                            escogido.personas[2] = angular.copy(escogidoOri.personas[0]);
                        }

                        //コネクト部分
                        else if (document.getElementById("radioConnect1").checked) {
                            document.querySelector('label[for="radioConnect1"]').innerText = "1回目 : 無";
                            //要素クリア
                            connectElegido[0].length = 0;
                            let array = new Array(limite);
                            connectElegido[0] = array;
                            //画像クリア
                            canvas61 = document.getElementById("canvas61");
                            if (!canvas61 || !canvas61.getContext) {
                                return;
                            }
                            var ctx61 = canvas61.getContext("2d");
                            ctx61.clearRect(60, 0, 60, 60);
                            break;
                        }
                        else if (document.getElementById("radioConnect2").checked) {
                            document.querySelector('label[for="radioConnect2"]').innerText = "2回目 : 無";
                            //要素クリア
                            connectElegido[1].length = 0;
                            let array = new Array(limite);
                            connectElegido[1] = array;
                            //画像クリア
                            canvas62 = document.getElementById("canvas62");
                            if (!canvas62 || !canvas62.getContext) {
                                return;
                            }
                            var ctx62 = canvas62.getContext("2d");
                            ctx62.clearRect(60, 0, 60, 60);
                            break;
                        }
                        else if (document.getElementById("radioConnect3").checked) {
                            document.querySelector('label[for="radioConnect3"]').innerText = "3回目 : 無";
                            //要素クリア
                            connectElegido[2].length = 0;
                            let array = new Array(limite);
                            connectElegido[2] = array;
                            //画像クリア
                            canvas63 = document.getElementById("canvas63");
                            if (!canvas63 || !canvas63.getContext) {
                                return;
                            }
                            var ctx63 = canvas63.getContext("2d");
                            ctx63.clearRect(60, 0, 60, 60);
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
                                else {//アイコンがない時の処理。今は使ってない
                                    DrawCircle(ctx3, charaX[i], charaY, charaR, -1);
                                    DrawText(ctx3, charaX[i], charaY, nombre[i], -1);
                                }
                            }
                            else {
                                //全て通常色
                                if (jsonData.personas[i].data1 !== ""){
                                    console.log(i);
                                    // DrawImage(ctx3, charaX[i], charaY, charaR, imageAry1[i]);
                                    Draw2ndRow(ctx3, charaX[i], charaY, charaR, imageAry1[i], Xoffset, Yoffset, canvasFlag);
                                }
                                else {//アイコンがない時の処理。今は使ってない
                                    DrawCircle(ctx3, charaX[i], charaY, charaR, 1);
                                    DrawText(ctx3, charaX[i], charaY, nombre[i], 1);
                                }
                            }
                        }
                        elegida[numero] = number;
                        console.log(elegida[numero] + " 他番の場合色変え");
                        let w = $(window).width();
                        let wSize = 768;
                        var numeroC;//コネクトキャラcanvas表示用
                        //キャラ名表示
                        // $("#MainContent_seleccionado1").text("選択キャラ : " + jsonData.personas[elegida].name);
                        if (document.getElementById("MainContent_seleccionado_0").checked) {
                            if (w < wSize){
                                document.getElementById("MainContent_nombre1").innerText = "攻撃側1人目 : " + jsonData.personas[elegida[0]].nickName;
                                document.getElementById("MainContent_seleccionado_0").nextSibling.innerText = "1人目 : " + jsonData.personas[elegida[0]].nickName;
                            }
                            else {
                                document.getElementById("MainContent_nombre1").innerText = "攻撃側1人目 : " + jsonData.personas[elegida[0]].name;
                                document.getElementById("MainContent_seleccionado_0").nextSibling.innerText = "1人目 : " + jsonData.personas[elegida[0]].name;
                            }
                                //アイコン画像,データ表示
                            // let image1a = document.getElementById('1a');
                            // image1a.innerHTML = '<img src=' + "png/" + jsonData.personas[number].data1 + ' alt=\"選択無\">';
                            let iData = [[jsonData.personas[number].nickName], [jsonData.personas[number].ATK], [jsonData.personas[number].DEF], [jsonData.personas[number].HP]];
                            IndicaData(iData, "grid1a");
                            if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
                                //ピュエラコンボの場合
                                if (w < wSize) {
                                    document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : " + jsonData.personas[elegida[0]].nickName;
                                    document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : " + jsonData.personas[elegida[0]].nickName;
                                 }
                                else {
                                    document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : " + jsonData.personas[elegida[0]].name;
                                    document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : " + jsonData.personas[elegida[0]].name;
                                }
                                //アイコン画像表示,データ表示
                                // let image2a = document.getElementById('2a');
                                // image2a.innerHTML = '<img src=' + "png/" + jsonData.personas[number].data1 + ' alt=\"選択無\">';
                                let iData = [[jsonData.personas[number].nickName], [jsonData.personas[number].ATK], [jsonData.personas[number].DEF], [jsonData.personas[number].HP]];
                                IndicaData(iData, "grid2a");
                                // let image3a = document.getElementById('3a');
                                // image3a.innerHTML = '<img src=' + "png/" + jsonData.personas[number].data1 + ' alt=\"選択無\">';
                                iData = [[jsonData.personas[number].nickName], [jsonData.personas[number].ATK], [jsonData.personas[number].DEF], [jsonData.personas[number].HP]];
                                IndicaData(iData, "grid3a");
                            }
                            for (let i = 0; i < 6; i++) {
                                valorAjustado[0][i] = jsonData.personas[elegida[0]].Despierta[i];
                            }
                            //名前
                            escogido.personas[0].name = jsonData.personas[number].name;
                            escogido.personas[0].nickName = jsonData.personas[number].nickName;
                            escogido.personas[0].data1 = jsonData.personas[number].data1;
                            
                            //ATK等数値記入
                            escogido.personas[0].atk = jsonData.personas[number].ATK;
                            escogido.personas[0].def = jsonData.personas[number].DEF;
                            escogido.personas[0].hp = jsonData.personas[number].HP;
                            //魔法少女タイプ入力
                            if (jsonData.personas[number].TipoMagia === "円環マギア")
                                escogido.personas[0].type = "マギア";
                            else if (jsonData.personas[number].TipoMagia === "円環サポート")
                                escogido.personas[0].type = "サポート";
                            else
                                escogido.personas[0].type = jsonData.personas[number].TipoMagia;
                            
                        }
                        //2人目3人目の処理追加(通常の場合)
                        else if (document.getElementById("MainContent_seleccionado_1").checked) {
                            if (w < wSize) {
                                document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : " + jsonData.personas[elegida[1]].nickName;
                                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : " + jsonData.personas[elegida[1]].nickName;
                            }
                            else {
                                document.getElementById("MainContent_nombre2").innerText = "攻撃側2人目 : " + jsonData.personas[elegida[1]].name;
                                document.getElementById("MainContent_seleccionado_1").nextSibling.innerText = "2人目 : " + jsonData.personas[elegida[1]].name;
                            }
                            for (let i = 0; i < 6; i++) {
                                valorAjustado[1][i] = jsonData.personas[elegida[1]].Despierta[i];
                            }
                             //名前
                             escogido.personas[1].name = jsonData.personas[number].name;
                             escogido.personas[1].nickName = jsonData.personas[number].nickName;
                             escogido.personas[1].data1 = jsonData.personas[number].data1;
                            //ATK等数値記入
                            escogido.personas[1].atk = jsonData.personas[number].ATK;
                            escogido.personas[1].def = jsonData.personas[number].DEF;
                            escogido.personas[1].hp = jsonData.personas[number].HP;
                            //魔法少女タイプ入力
                            if (jsonData.personas[number].TipoMagia === "円環マギア")
                                escogido.personas[1].type = "マギア";
                            else if (jsonData.personas[number].TipoMagia === "円環サポート")
                                escogido.personas[1].type = "サポート";
                            else
                                escogido.personas[1].type = jsonData.personas[number].TipoMagia;
                            //アイコン画像表示
                            // let image2a = document.getElementById('2a');
                            // image2a.innerHTML = '<img src=' + "png/" + jsonData.personas[number].data1 + ' alt=\"選択無\" >';
                            let iData = [[jsonData.personas[number].nickName], [jsonData.personas[number].ATK], [jsonData.personas[number].DEF], [jsonData.personas[number].HP]];
                            IndicaData(iData, "grid2a");
                        }
                        else if (document.getElementById("MainContent_seleccionado_2").checked) {
                            if (w < wSize) {
                                document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : " + jsonData.personas[elegida[2]].nickName;
                                obj = document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : " + jsonData.personas[elegida[2]].nickName;
                            }
                            else {
                                document.getElementById("MainContent_nombre3").innerText = "攻撃側3人目 : " + jsonData.personas[elegida[2]].name;
                                obj = document.getElementById("MainContent_seleccionado_2").nextSibling.innerText = "3人目 : " + jsonData.personas[elegida[2]].name;
                            }
                            for (let i = 0; i < 6; i++) {
                                valorAjustado[2][i] = jsonData.personas[elegida[2]].Despierta[i];
                            }
                             //名前
                             escogido.personas[2].name = jsonData.personas[number].name;
                             escogido.personas[2].nickName = jsonData.personas[number].nickName;
                             escogido.personas[2].data1 = jsonData.personas[number].data1;
                            //ATK他数値記入
                            escogido.personas[2].atk = jsonData.personas[number].ATK;
                            escogido.personas[2].def = jsonData.personas[number].DEF;
                            escogido.personas[2].hp = jsonData.personas[number].HP;
                            //魔法少女タイプ入力
                            if (jsonData.personas[number].TipoMagia === "円環マギア")
                                escogido.personas[2].type = "マギア";
                            else if (jsonData.personas[number].TipoMagia === "円環サポート")
                                escogido.personas[2].type = "サポート";
                            else
                                escogido.personas[2].type = jsonData.personas[number].TipoMagia;
                             //アイコン画像表示
                            //  let image3a = document.getElementById('3a');
                            // image3a.innerHTML = '<img src=' + "png/" + jsonData.personas[number].data1 + ' alt=\"選択無\" >';
                            let iData = [[jsonData.personas[number].nickName], [jsonData.personas[number].ATK], [jsonData.personas[number].DEF], [jsonData.personas[number].HP]];
                            IndicaData(iData, "grid3a");
                        }
                        
                        //コネクト処理
                        else if (document.getElementById("radioConnect1").checked) {
                            if (w < wSize)
                                document.querySelector('label[for="radioConnect1"]').innerText = "1回目 : " + jsonData.personas[number].nickName;
                            else
                                document.querySelector('label[for="radioConnect1"]').innerText = "1回目 : " + jsonData.personas[number].name;
                            //要素クリア
                            connectElegido[0].length = 0;
                            let array = new Array(limite);
                            connectElegido[0] = array;
                            GetConnect(jsonData.personas[number], 0);
                            // break;
                            numeroC = 0;
                        }
                        else if (document.getElementById("radioConnect2").checked) {
                            if (w < wSize)
                            document.querySelector('label[for="radioConnect2"]').innerText = "2回目 : " + jsonData.personas[number].nickName;                                
                            else
                            document.querySelector('label[for="radioConnect2"]').innerText = "2回目 : " + jsonData.personas[number].name;
                            //要素クリア
                            connectElegido[1].length = 0;
                            let array = new Array(limite);
                            connectElegido[1] = array;
                            GetConnect(jsonData.personas[number], 1);
                            // break;
                            numeroC = 1;
                        }
                        else if (document.getElementById("radioConnect3").checked) {
                            if (w < wSize)
                            document.querySelector('label[for="radioConnect3"]').innerText = "3回目 : " + jsonData.personas[number].nickName;
                        else
                            document.querySelector('label[for="radioConnect3"]').innerText = "3回目 : " + jsonData.personas[number].name;
                            //要素クリア
                            connectElegido[2].length = 0;
                            let array = new Array(limite);
                            connectElegido[2] = array;
                            GetConnect(jsonData.personas[number], 2);
                            // break;
                            numeroC = 2;
                        }
    
                        var canvas1;
                        var canvas6;
                        switch (numero) {
                            case 0:
                                {
                                    canvas1 = document.getElementById("canvas1");
                                    canvas6 = document.getElementById("canvas61");
                                    break;
                                }
                            case 1:
                                {
                                    canvas1 = document.getElementById("canvas12");
                                    canvas6 = document.getElementById("canvas62");
                                    break;
                                }
                            case 2:
                                {
                                    canvas1 = document.getElementById("canvas13");
                                    canvas6 = document.getElementById("canvas63");
                                    break;
                                }
                        }
                        switch (numeroC) {
                            case 0:
                                {
                                    canvas6 = document.getElementById("canvas61");
                                    break;
                                }
                            case 1:
                                {
                                    canvas6 = document.getElementById("canvas62");
                                    break;
                                }
                            case 2:
                                {
                                    canvas6 = document.getElementById("canvas63");
                                    break;
                                }
                        }
                        //canvas6
                        if (!canvas6 || !canvas6.getContext) {
                            return;
                        }
                        var ctx6 = canvas6.getContext("2d");

                        if (!Number.isNaN(numero)) {//コネクト以外の場合
                            //canvas1
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
                            // break;
                            //選択キャラの顔グラ表示
                            DrawImage(ctx6, 30, 30, charaR, imageAry1[number]);
                            if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1") {
                                //1人で攻撃
                                canvas62 = document.getElementById("canvas62");
                                if (!canvas62 || !canvas62.getContext) {
                                    return;
                                }
                                var ctx62 = canvas62.getContext("2d");
                                DrawImage(ctx62, 30, 30, charaR, imageAry1[number]);

                                canvas63 = document.getElementById("canvas63");
                                if (!canvas63 || !canvas63.getContext) {
                                    return;
                                }
                                var ctx63 = canvas63.getContext("2d");
                                DrawImage(ctx63, 30, 30, charaR, imageAry1[number]);
                                //コネクト入ってれば。。。
                                if (typeof connectElegido[0][30] !== "undefined") {
                                    ctx6.drawImage(canvas6, 0, 0, 60, 60, 90, 30, 30, 30);
                                }
                                if (typeof connectElegido[1][30] !== "undefined") {
                                    ctx62.drawImage(canvas62, 0, 0, 60, 60, 90, 30, 30, 30);
                                }
                                if (typeof connectElegido[2][30] !== "undefined") {
                                    ctx63.drawImage(canvas63, 0, 0, 60, 60, 90, 30, 30, 30);
                                }
                            }
                            else if ($('input[name="ctl00$MainContent$seleccionado"]:checked').val() === "1"){
                                //コネクト入ってれば。。。
                                if (typeof connectElegido[0][30] !== "undefined") {
                                    ctx6.drawImage(canvas6, 0, 0, 60, 60, 90, 30, 30, 30);
                                }
                            }
                            else if ($('input[name="ctl00$MainContent$seleccionado"]:checked').val() === "2"){
                                //コネクト入ってれば。。。
                                if (typeof connectElegido[1][30] !== "undefined") {
                                    ctx6.drawImage(canvas6, 0, 0, 60, 60, 90, 30, 30, 30);
                                }
                            }
                            else {
                                //コネクト入ってれば。。。
                                if (typeof connectElegido[2][30] !== "undefined") {
                                    ctx6.drawImage(canvas6, 0, 0, 60, 60, 90, 30, 30, 30);
                                }
                            }
                        
                        }
                        else {//コネクトの場合
                            DrawImage(ctx6, 90, 30, charaR, imageAry1[number]);
                            ctx6.drawImage(canvas6, 0, 0, 60, 60, 90, 30, 30, 30);
                            
                        }
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
                            if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                                let mAtk = Number(jsonData.personas[i].menteATK) || 0;
                                ordenLetra += mAtk;
                            }
                            break;
                        }
                    case "DEF":
                        {
                            ordenLetra = jsonData.personas[i].DEF;
                            if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                                let mAtk = Number(jsonData.personas[i].menteDEF) || 0;
                                ordenLetra += mAtk;
                            }
                            break;
                        }
                    case "HP":
                        {
                            ordenLetra = jsonData.personas[i].HP;
                            if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                                let mAtk = Number(jsonData.personas[i].menteHP) || 0;
                                ordenLetra += mAtk;
                            }
                            break;
                        }
                    case "マギア値":
                    case "コネクト値":
                    case "精神スキル":
                        {
                            let sortValue = jsonData.personas[i].sortValue1;
                            sortValue = (jsonData.personas[i].sortValue2 !== null)&&(typeof jsonData.personas[i].sortValue2 !=="undefined") ? sortValue +  " " + jsonData.personas[i].sortValue2 : sortValue;
                            ordenLetra = sortValue;
                            break;
                        }
                    case "精神アビ":
                        {
                            let sortValue = "";
                            if (ChangeToRoman(jsonData.personas[i].sortValue1)) {
                                if ($("#MainContent_apareceAbiDetalle").prop("checked"))
                                    sortValue = jsonData.personas[i].subValue;
                                else
                                    sortValue = ChangeToRoman(jsonData.personas[i].sortValue1);
                            }
                            else
                                sortValue = jsonData.personas[i].subValue;
                            ordenLetra = sortValue;
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
    canvas3.addEventListener('pointerout', function (e) {
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
        if (BusquedaConectiva("防御力UP", inputJson, 1)) {
            resultado = BusquedaConectiva("防御力UP", inputJson, 1);
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
        if (BusquedaConectiva("月夜にコネクトするとさらにMP回復", inputJson, 1)) {
            connectElegido[selector][14] = 25;
        }
        connectElegido[selector][30] = "png/" + inputJson.data1;
    }

    

    //フィルター選択時
    $('input[name="ctl00$MainContent$filtro1$0"]').change(function () {
        if ($("#MainContent_filtro1_0").prop("checked") === true) {
            //全のみチェック入れ、他はチェック外す
            // $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
            $('input[name*="ctl00$MainContent$filtro1$1"]').prop('checked', false);
            $('input[name*="ctl00$MainContent$filtro1$2"]').prop('checked', false);
            $('input[name*="ctl00$MainContent$filtro1$3"]').prop('checked', false);
            $('input[name*="ctl00$MainContent$filtro1$4"]').prop('checked', false);
            $('input[name*="ctl00$MainContent$filtro1$5"]').prop('checked', false);
            $('input[name*="ctl00$MainContent$filtro1$6"]').prop('checked', false);
        }
        else
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$1"]').change(function () {
        //チェックされた属性値を取得
        let atributo = [];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        if($("#MainContent_filtro1_1").prop("checked") === true){
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', false);
            // $('input[name*="ctl00$MainContent$filtro1$1"]').prop('checked', true);
        }
        else {
            // $('input[name*="ctl00$MainContent$filtro1$1"]').prop('checked', false);
            if (atributo.length === 0)
                $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        }

        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$2"]').change(function () {
        //チェックされた属性値を取得
        let atributo = [];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        if($("#MainContent_filtro1_2").prop("checked") === true){
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', false);
            // $('input[name*="ctl00$MainContent$filtro1$2"]').prop('checked', true);
        }
        else {
            // $('input[name*="ctl00$MainContent$filtro1$2"]').prop('checked', false);
            if (atributo.length === 0)
                $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        }
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$3"]').change(function () {
        //チェックされた属性値を取得
        let atributo = [];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        if($("#MainContent_filtro1_3").prop("checked") === true){
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', false);
            // $('input[name*="ctl00$MainContent$filtro1$3"]').prop('checked', true);
        }
        else {
            // $('input[name*="ctl00$MainContent$filtro1$3"]').prop('checked', false);
            if (atributo.length === 0)
                $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        }
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$4"]').change(function () {
        //チェックされた属性値を取得
        let atributo = [];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        if($("#MainContent_filtro1_4").prop("checked") === true){
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', false);
            // $('input[name*="ctl00$MainContent$filtro1$4"]').prop('checked', true);
        }
        else {
            // $('input[name*="ctl00$MainContent$filtro1$4"]').prop('checked', false);
            if (atributo.length === 0)
                $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        }
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$5"]').change(function () {
        //チェックされた属性値を取得
        let atributo = [];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        if($("#MainContent_filtro1_5").prop("checked") === true){
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', false);
            // $('input[name*="ctl00$MainContent$filtro1$5"]').prop('checked', true);
        }
        else {
            // $('input[name*="ctl00$MainContent$filtro1$5"]').prop('checked', false);
            if (atributo.length === 0)
                $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        }
        CheckChanged();
    });
    $('input[name="ctl00$MainContent$filtro1$6"]').change(function () {
        //チェックされた属性値を取得
        let atributo = [];
        let i = 0;
        $('input[name*="ctl00$MainContent$filtro1"]:checked').each(function () {
            //値を取得
            atributo[i] = $(this).val();
            i++;
        });
        if($("#MainContent_filtro1_6").prop("checked") === true){
            $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', false);
            // $('input[name*="ctl00$MainContent$filtro1$6"]').prop('checked', true);
        }
        else {
            // $('input[name*="ctl00$MainContent$filtro1$6"]').prop('checked', false);
            if (atributo.length === 0)
                $('input[name*="ctl00$MainContent$filtro1$0"]').prop('checked', true);
        }
        CheckChanged();
    });
    $('#MainContent_tipo1').change(function () {
        CheckChanged();
    });
    // var clickFlag = 0;//changeしたあと、clickに再び入ってしまうので、click部での処理をこのフラグでキャンセル
    // var mouseFlag = 1; //1:スルー　0:処理
    //change
    $('#MainContent_tipoMagia').change(function () {
        // console.log("chagne" + " m" + mouseFlag + "c" + clickFlag );
        CheckChanged();
        var ordenMagia = document.getElementById("MainContent_orden1_4");
        var textMagia = $('#MainContent_tipoMagia').val();
        if (textMagia.indexOf("マギア指定") === -1) {
            ordenMagia.disabled = false;
            if (((textMagia !== "属性強化") && (textMagia !== "低HPほど威力UP")) && $(window).width() > 768) {
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

        let data = CuentaTodo("magia1", 2);
        // mouseFlag = 1;
        // clickFlag = 1;
        // console.log("chagne2" + " m" + mouseFlag + "c" + clickFlag );
        ApareceCantidad("MainContent_tipoMagia", data[0]);
        ApareceCantidad("MainContent_tipoMagia2", data[1]);
        ApareceCantidad("MainContent_connect1", data[2]);
        ApareceCantidad("MainContent_connect2", data[3]);
});
    
    //click
    $('#MainContent_tipoMagia').on('click', function () {
        // console.log("click" + " m" + mouseFlag + "c" + clickFlag);
        // if (clickFlag === 0) {
            let data = CuentaTodo("magia1", 1);
            ApareceCantidad("MainContent_tipoMagia", data[0]);
            ApareceCantidad("MainContent_tipoMagia2", data[1]);
            ApareceCantidad("MainContent_connect1", data[2]);
            ApareceCantidad("MainContent_connect2", data[3]);
            console.log("click処理実行 敵全体 " + data[0][1][1]);
        //     clickFlag = 1;
        //     mouseFlag = 0;
        // } else {
        //     clickFlag = 0;
        // }
        // console.log("click2" + " m" + mouseFlag + "c" + clickFlag);
    });
    

    $('#MainContent_tipoMagia2').change(function () {
        CheckChanged();
        let data = CuentaTodo("magia2", 2);
        // clickFlag = 1;
        ApareceCantidad("MainContent_tipoMagia", data[0]);
        ApareceCantidad("MainContent_tipoMagia2", data[1]);
        ApareceCantidad("MainContent_connect1", data[2]);
        ApareceCantidad("MainContent_connect2", data[3]);
    });

    $('#MainContent_tipoMagia2').on('click', function () {
        // if(clickFlag !== 1){
            let data = CuentaTodo("magia2",1);
            ApareceCantidad("MainContent_tipoMagia", data[0]);
            ApareceCantidad("MainContent_tipoMagia2", data[1]);
            ApareceCantidad("MainContent_connect1", data[2]);
            ApareceCantidad("MainContent_connect2", data[3]);
        // }
        // clickFlag = 0;
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
            if (((textConnect !== "防御無視")&&(textConnect !== "回避無効")) && $(window).width() > 768) {
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
        
        let data = CuentaTodo("connect1", 2);
        // clickFlag = 1;
        ApareceCantidad("MainContent_tipoMagia", data[0]);
        ApareceCantidad("MainContent_tipoMagia2", data[1]);
        ApareceCantidad("MainContent_connect1", data[2]);
        ApareceCantidad("MainContent_connect2", data[3]);
    });
    $('#MainContent_connect1').on('click', function () {
        // if (clickFlag !== 1) {
            let data = CuentaTodo("connect1", 1);
            ApareceCantidad("MainContent_tipoMagia", data[0]);
            ApareceCantidad("MainContent_tipoMagia2", data[1]);
            ApareceCantidad("MainContent_connect1", data[2]);
            ApareceCantidad("MainContent_connect2", data[3]);
        // }
        // clickFlag = 0;
            
    });

    $('#MainContent_connect2').change(function () {
        CheckChanged();
        let data = CuentaTodo("connect2", 2);
        // clickFlag = 1;
        ApareceCantidad("MainContent_tipoMagia", data[0]);
        ApareceCantidad("MainContent_tipoMagia2", data[1]);
        ApareceCantidad("MainContent_connect1", data[2]);
        ApareceCantidad("MainContent_connect2", data[3]);
    });

    $('#MainContent_connect2').on('click', function () {
        // if (clickFlag !== 1) {
            let data = CuentaTodo("connect2", 1);
            ApareceCantidad("MainContent_tipoMagia", data[0]);
            ApareceCantidad("MainContent_tipoMagia2", data[1]);
            ApareceCantidad("MainContent_connect1", data[2]);
            ApareceCantidad("MainContent_connect2", data[3]);
        // }
        // clickFlag = 0;
    });

    $('#MainContent_menteS1').change(function () {
        CheckChanged();
        var ordenMenteS1 = document.getElementById("MainContent_orden1_6");
        var textMenteS1 = $('#MainContent_menteS1').val();
        if (textMenteS1.indexOf("精神強化スキル") === -1) {
            ordenMenteS1.disabled = false;
            if($(window).width() > 768)
                document.getElementById("MainContent_orden1_6").nextSibling.innerText = "精神スキル:" + textMenteS1;
        }
        else {
            if ($("#MainContent_orden1_6").prop("checked") === true) {
                document.getElementById("MainContent_orden1_6").nextSibling.classList.remove("highlight");
                $("#MainContent_orden1_6").prop("checked", false);
            }
            ordenMenteS1.disabled = true;
            if ($(window).width() > 768)
                document.getElementById("MainContent_orden1_6").nextSibling.innerText = "精神スキル";
        }
    });

    $('#MainContent_menteA1').change(function () {
        CheckChanged();
        var ordenMenteA1 = document.getElementById("MainContent_orden1_7");
        var textMenteA1 = $('#MainContent_menteA1').val();
        if (textMenteA1.indexOf("精神強化アビ") === -1) {
            ordenMenteA1.disabled = false;
            if($("#MainContent_orden1_7").prop("checked") === true)
                document.getElementById("MainContent_apareceAbiDetalle").disabled = false;
            if($(window).width() > 768)
                document.getElementById("MainContent_orden1_7").nextSibling.innerText = "精神アビ:" + textMenteA1;
        }
        else {
            if ($("#MainContent_orden1_7").prop("checked") === true) {
                document.getElementById("MainContent_orden1_7").nextSibling.classList.remove("highlight");
                $("#MainContent_orden1_7").prop("checked", false);
            }
            ordenMenteA1.disabled = true;
            document.getElementById("MainContent_apareceAbiDetalle").disabled = true;
            if ($(window).width() > 768)
                document.getElementById("MainContent_orden1_7").nextSibling.innerText = "精神アビ";
        }
    });

    $('#MainContent_menteA2').change(function () {
        CheckChanged();
    });

    $('input[name="ctl00$MainContent$apareceAbiDetalle"]').change(function () {
        // CheckChanged();
        if(($("#MainContent_orden1_7").prop("checked") === true)&&($('#MainContent_menteA1').val().indexOf("精神強化アビ") === -1))
            ReDraw("sortValueS");
    });


    //項目にフィルタ数追加
    function ApareceCantidad(list,data) {
        //書き換え処理
        let obj = document.getElementById(list);
        let listDummy = [obj.length];
        for (let i = 1; i < obj.length; i++){
            let s = obj.options[i].value + "(" + data[i][1] + ")";
            // obj.options[i].text = s;
            obj.options[i].innerText = s;
            // list.options[i].innerText = s;"name": "",
        }
        // list.append(listDummy);
        // console.log(list.name + " " + data[1][1])
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
                if (atributo[i] === "全") {
                    checkFlag++;
                }
                else if (value.Attribute === atributo[i]) {
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
        var output = opciones2;
        // var output = opciones1;
        // switch (operacion) {
        //     case "magia1":
        //         {
        //             output[0] = opciones2[0];
        //             break;
        //         }
        //     case "magia2":
        //         {
        //             output[1] = opciones2[1];
        //             break;
        //         }
        //     case "connect1":
        //         {
        //             output[2] = opciones2[2];
        //             break;
        //         }
        //     case "connect2":
        //         {
        //             output[3] = opciones2[3];
        //             break;
        //         }
        // }
        
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
        //精神強化
        var mskill1 = $("#MainContent_menteS1").val();
        var mabi1 = $("#MainContent_menteA1").val();
        var mabi2 = $("#MainContent_menteA2").val();
        //属性の全チェックを消すか確認
        if (atributo[0] === "全" && atributo.length > 1) {
            //消す
            $("#MainContent_filtro1_0").prop("checked", false);
        
        } else if (atributo[0] !== "全" && atributo.length === 0) {
            //付ける
            $("#MainContent_filtro1_0").prop("checked",true);
        }

        //フィルター処理
        var checkFlag = [jsonData.personas.length];
        jsonData = angular.copy(jsonDataOri);
        jsonData.personas = jsonData.personas.filter(function (value, index, array) {
            checkFlag[index] = 0;
            for (let i = 0; i < atributo.length; i++) {
                if (atributo[0] === "全")
                    checkFlag[index] += 1;
                else if (value.Attribute === atributo[i]) {
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
                    let sortFlag = (($("input[name='ctl00$MainContent$orden1']:checked").val() === "マギア値")&& i===0) ? 1 : 0;
                    switch (tipoMagia[i]) {
                        case "敵全体":
                            {
                                if (BusquedaMagia("全体", value, 1)) {
                                    resultado = BusquedaMagia("全体", value, 2);
                                    if(sortFlag===1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia(tipoMagia[i], value, 1);
                                        value.sortValue2 = resultado[1];//回数も表示
                                        
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
                                    resultado1 = BusquedaMagia(tipoMagia[i], value, 2);
                                }
                                else
                                    return false;
                                break;
                            }
                        //info1でソート
                        case "HP回復":
                        case "MP回復":
                        case "状態強化解除":
                        case "バフ解除":
                            {
                                if (BusquedaMagia(tipoMagia[i], value, 1)) {
                                    resultado = BusquedaMagia(tipoMagia[i], value, 1);
                                    if(sortFlag===1)
                                        value.sortValue1 = resultado[1];
                                }
                                else
                                    return false;
                                break;
                            }
                        //info1でソートし、info2を加算
                        case "BlastダメUP":
                            {
                                if (BusquedaMagia("BlastダメージUP", value, 1)) {
                                    resultado = BusquedaMagia("BlastダメージUP", value, 1);
                                    if(sortFlag===1) {
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia("BlastダメージUP", value, 2);
                                        value.sortValue2 = resultado[1];//ターン数も表示
                                        
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                            case "攻撃力UP":
                            {
                                if (BusquedaMagia("攻撃力UP", value, 1)) {
                                    resultado = BusquedaMagia("攻撃力UP", value, 1);
                                    if(sortFlag===1) {
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia("攻撃力UP", value, 2);
                                        value.sortValue2 = resultado[1];//ターン数も表示
                                        
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                            case "ダメージUP":
                            {
                                if (BusquedaMagia("ダメージUP", value, 1)) {
                                    resultado = BusquedaMagia("ダメージUP", value, 1);
                                    if(sortFlag===1) {
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia("ダメージUP", value, 2);
                                        value.sortValue2 = resultado[1];//ターン数も表示
                                        
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                        case "防御力DOWN":
                            {
                                if (BusquedaMagia("防御力DOWN", value, 1)) {
                                    resultado = BusquedaMagia("防御力DOWN", value, 1);
                                    if(sortFlag===1) {
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia("防御力DOWN", value, 2);
                                        value.sortValue2 = resultado[1];//ターン数も表示
                                        
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                        case "Charge後ダメUP":
                            {
                                if (BusquedaMagia("Charge後ダメージUP", value, 1)) {
                                    resultado = BusquedaMagia("Charge後ダメージUP", value, 1);
                                    if(sortFlag===1) {
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia("Charge後ダメージUP", value, 2);
                                        value.sortValue2 = resultado[1];//ターン数も表示
                                        
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                        case "HP自動回復":
                            {
                                if (BusquedaMagia(tipoMagia[i], value, 1)) {
                                    resultado = BusquedaMagia(tipoMagia[i], value, 1);
                                    if(sortFlag===1) {
                                        value.sortValue1 = resultado[1];
                                        resultado = BusquedaMagia(tipoMagia[i], value, 2);
                                        value.sortValue2 = resultado[1];//ターン数も表示
                                        
                                    }
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
                        case "防御力UP":
                            {
                                if (BusquedaConectiva(connect[i], value, 1)) {
                                    resultado = BusquedaConectiva(connect[i], value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で回避", value, 0)) {
                                    resultado = BusquedaConectiva("確率で回避", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "防御無視":
                        case "回避無効":
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
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        case "Charge後ダメUP":
                            {
                                if (BusquedaConectiva("Charge後ダメージUP", value, 1)) {
                                    resultado = BusquedaConectiva("Charge後ダメージUP", value, 1);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で拘束", value, 1)) {
                                    resultado = BusquedaConectiva("確率で拘束", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else if (BusquedaConectiva("確率でスタン", value, 1)) {
                                    resultado = BusquedaConectiva("確率でスタン", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で魅了", value, 1)) {
                                    resultado = BusquedaConectiva("確率で魅了", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で幻惑", value, 1)) {
                                    resultado = BusquedaConectiva("確率で幻惑", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                            case "挑発":
                            {
                                if (BusquedaConectiva("必ず挑発", value, 1)) {
                                    resultado = BusquedaConectiva("必ず挑発", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
                                }
                                else if (BusquedaConectiva("確率で挑発", value, 1)) {
                                    resultado = BusquedaConectiva("確率で挑発", value, 0);
                                    if(sortFlag === 1)
                                        value.sortValue1 = resultado[1];
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
                                        value.sortValue1 = resultado[1];
                                }
                                else
                                    return;
                                break;
                            }
                        
                        
                    }
                }
                
            }

            //精神強化スキル
            {
                if (mskill1 !== "精神強化スキル") {
                    //精神強化値が入力されていない場合スルー
                    if (typeof value.mSelect === "undefined") {
                        return false;
                    }
                    else{
                        var sortFlag = ($("input[name='ctl00$MainContent$orden1']:checked").val() === "精神スキル") ? 1 : 0;
                        switch (mskill1) {
                            //target
                            case "攻撃力UP":
                            case "攻撃力DOWN":
                            case "防御力DOWN":
                            case "拘束":
                            case "魅了":
                            case "幻惑":
                            case "スタン":
                            case "呪い":
                            case "霧":
                            case "暗闇":
                                {
                                    if (BusquedaMskill(mskill1, value, 1)) {
                                        resultado = BusquedaMskill(mskill1, value, 1);
                                        if (sortFlag === 1)
                                            value.sortValue1 = resultado[1];
                                        resultado = BusquedaMskill(mskill1, value, 2);//turget
                                        if (sortFlag === 1)
                                            value.sortValue2 = resultado[1];
                                    }
                                    else
                                        return;
                                    break;
                                }
                                //turn
                            case "AcceleMPUP":
                                {
                                    if (BusquedaMskill(mskill1, value, 1)) {
                                        resultado = BusquedaMskill(mskill1, value, 1);
                                        if (sortFlag === 1)
                                            value.sortValue1 = resultado[1];
                                        resultado = BusquedaMskill(mskill1, value, 3);//turn
                                        if (sortFlag === 1)
                                            value.sortValue2 = resultado[1];
                                    }
                                    else
                                        return;
                                    break;
                                }
                            default:
                                {
                                    if (BusquedaMskill(mskill1, value, 1)) {
                                        resultado = BusquedaMskill(mskill1, value, 1);
                                        if (sortFlag === 1)
                                            value.sortValue1 = resultado[1];
                                    }
                                    else
                                        return;
                                    break;
                                }
                        }
                    }
                }
            }

            //精神強化アビ
            {
                var mabi = [mabi1, mabi2];
                if (!((mabi[0] === "精神強化アビ1") && (mabi[0] === "精神強化アビ2"))) {
                    for (let i = 0; i < mabi.length; i++) {
                        if (mabi[i].indexOf("精神強化アビ") === -1) {
                            //精神強化値が入力されていない場合スルー
                            if (typeof value.mSelect === "undefined") {
                                return false;
                            }
                            else{
                                for (let i = 0; i < mabi.length; i++) {
                                    var sortFlag = ($("input[name='ctl00$MainContent$orden1']:checked").val() === "精神アビ") && i === 0 ? 1 : 0;
                                    if (mabi[i].indexOf("精神強化アビ") !== -1)
                                        continue;
                                    else {
                                        switch (mabi[i]) {
                                            case "呪い付与"://sortValue2の設定のみ
                                            case "拘束付与":
                                            case "魅了付与":
                                            case "幻惑付与":
                                            case "スタン付与":
                                            case "毒付与":
                                            case "やけど付与":
                                            case "霧付与":
                                            case "暗闇付与":
                                            case "スキル不可付与":
                                            case "マギア不可付与":
                                            case "属性ダメCUT状態":
                                                {
                                                    let resultado = BusquedaMabi(mabi[i], value);
                                                    if (resultado[0]) {
                                                        if (sortFlag === 1) {
                                                            value.sortValue1 = resultado[2];//数字
                                                            value.subValue = resultado[1];//テキスト
                                                            value.sortValue2 = resultado[1];//テキスト
                                                        }
                                                    }
                                                    else
                                                        return;
                                                    break;
                                                }
                                            default:
                                                {
                                                    let resultado = BusquedaMabi(mabi[i], value);
                                                    if (resultado[0]) {
                                                        if (sortFlag === 1) {
                                                            value.sortValue1 = resultado[2];//数字
                                                            value.subValue = resultado[1];//テキスト
                                                        }
                                                    }
                                                    else
                                                        return;
                                                    break;
                                                }
                                        }
                                    }
                    
                                }
                            }
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
        document.getElementById("MainContent_orden1_6").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_orden1_7").nextSibling.classList.remove("highlight");

        document.getElementById("MainContent_apareceAbiDetalle").disabled = true;//アビ詳細無効化


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
            case "精神スキル":
                {
                    document.getElementById("MainContent_orden1_6").nextSibling.classList.add("highlight");
                    break;
                }
            case "精神アビ":
                {
                    document.getElementById("MainContent_orden1_7").nextSibling.classList.add("highlight");
                    if ($('#MainContent_menteA1').val().indexOf("精神強化アビ") === -1)
                        document.getElementById("MainContent_apareceAbiDetalle").disabled = false;//アビ詳細有効化

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
                let text;
                if (ordena !== ""){
                    switch (ordena) {
                        case "ATK":
                            {
                                text = jsonData.personas[i].ATK;
                                if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                                    let mAtk = Number(jsonData.personas[i].menteATK) || 0;
                                    text += mAtk;
                                }
                                break;
                            }
                        case "DEF":
                            {
                                text = jsonData.personas[i].DEF;
                                if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                                    let mDef = Number(jsonData.personas[i].menteDEF) || 0;
                                    text += mDef;
                                }
                                break;
                            }
                        case "HP":
                            {
                                text = jsonData.personas[i].HP;
                                if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                                    let mHp = Number(jsonData.personas[i].menteHP) || 0;
                                    text += mHp;
                                }
                                break;
                            }
                        case "sortValue":
                            {
                                let sortValue = (jsonData.personas[i].sortValue1 !== null) && (typeof jsonData.personas[i].sortValue1 !== "undefined") ? jsonData.personas[i].sortValue1 : "";
                                sortValue = (jsonData.personas[i].sortValue2 !== null)&&(typeof jsonData.personas[i].sortValue2 !=="undefined") ? sortValue +  " " + jsonData.personas[i].sortValue2 : sortValue;
                                text = sortValue;
                                break;
                            }
                        case "sortValueS":
                            {
                                let sortValue = (jsonData.personas[i].sortValue1 !== null) && (typeof jsonData.personas[i].sortValue1 !== "undefined") ? jsonData.personas[i].sortValue1 : "";
                                if (ChangeToRoman(sortValue)) {
                                    if ($("#MainContent_apareceAbiDetalle").prop("checked"))
                                        text = jsonData.personas[i].subValue;
                                    else
                                        // text = ChangeToRoman(sortValue);
                                        //sortValue2があればsortValue2を使う
                                        text = (jsonData.personas[i].sortValue2 !== null)&&(typeof jsonData.personas[i].sortValue2 !=="undefined") ? jsonData.personas[i].sortValue2 : ChangeToRoman(sortValue);
                                }
                                else
                                    text = (jsonData.personas[i].subValue !== null)&&(typeof jsonData.personas[i].subValue !=="undefined") ? jsonData.personas[i].subValue : "";
                                break;
                            }
                    }
                    if(text!=="")
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
        if(deviceType === 2)
            var t2 = new ToolTipC(canvas3, region, 180);
    }

    //ソート処理
    function OrdenaMagia() {
        var tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
        function compareFuncATK(a, b) {
            let a2 = a.ATK;
            let b2 = b.ATK;
            if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                let ma = Number(a.menteATK) || 0;
                let mb = Number(b.menteATK) || 0;
                a2 += ma;
                b2 += mb;
            }
            return b2 - a2;
        }
        function compareFuncDEF(a, b) {
            let a2 = a.DEF;
            let b2 = b.DEF;
            if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                let ma = Number(a.menteDEF) || 0;
                let mb = Number(b.menteDEF) || 0;
                a2 += ma;
                b2 += mb;
            }
            return b2 - a2;
        }
        function compareFuncHP(a, b) {
            let a2 = a.HP;
            let b2 = b.HP;
            if ($("#MainContent_espiritu").prop("checked")) {//精神強化補正
                let ma = Number(a.menteHP) || 0;
                let mb = Number(b.menteHP) || 0;
                a2 += ma;
                b2 += mb;
            }
            return b2 - a2;
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
                            if (an > bn) 
                                return 1;
                            if (an < bn) 
                                return -1;
                            return 0;
                        });
                        break;
                    }
                case "マギア値":
                case "コネクト値":
                    {
                        //ソートしなくてもいいものはソートしない
                        let sortValue1 = jsonData.personas[0].sortValue1;
                        if ((sortValue1 !== null)&&(typeof sortValue1 !== "undefined")) {
                            if (sortValue1.indexOf("必ず") !== -1 || sortValue1.indexOf("確率で") !== -1) {
                                //特別処理　必ずを確率より前に持ってくる
                                jsonData.personas.sort(function (a, b) {
                                    if (a.sortValue1 > b.sortValue1)
                                        return 1;
                                    if (a.sortValue1 < b.sortValue1)
                                        return -1;
                                    return 0;
                                });
                            }
                            else if (sortValue1.indexOf("全") !== -1 || sortValue1.indexOf("自") !== -1) {
                                //特別処理　全を自より前に持ってくる
                                jsonData.personas.sort(function (a, b) {
                                    if (a.sortValue1 > b.sortValue1)
                                        return 1;
                                    if (a.sortValue1 < b.sortValue1)
                                        return -1;
                                    if ((a.sortValue2 !== null) && (typeof a.sortValue2 !== "undefined")) {
                                        if (a.sortValue2.indexOf("T") !== -1) {//マギアのBlastダメUP,Charge後ダメUPでターン数を第2キーに
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("T") - 1, 1) < b.sortValue2.substr(b.sortValue2.indexOf("T") - 1, 1))
                                                return 1;
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("T") - 1, 1) > b.sortValue2.substr(b.sortValue2.indexOf("T") - 1, 1))
                                                return -1;
                                        }
                                    }
                                    return 0;
                                });
                            }
                            else if (sortValue1.indexOf("敵全") !== -1 || sortValue1.indexOf("敵単") !== -1) {
                                //特別処理　敵全を敵自より前に持ってくる
                                jsonData.personas.sort(function (a, b) {
                                    if (a.sortValue1 > b.sortValue1) 
                                        return 1;
                                    if (a.sortValue1 < b.sortValue1) 
                                        return -1;
                                    if ((a.sortValue2 !== null)&&(typeof a.sortValue2 !== "undefined")) {
                                        if (a.sortValue2.indexOf("T") !== -1) {//ターン数を第2キーに
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("T") - 1, 1) < b.sortValue2.substr(b.sortValue2.indexOf("T") - 1, 1))
                                                return 1;
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("T") - 1, 1) > b.sortValue2.substr(b.sortValue2.indexOf("T") - 1, 1))
                                                return -1;
                                        }
                                    }
                                    return 0;
                                });
                            }
                            else {//通常ローマ数字ソート
                                jsonData.personas.sort(function (a, b) {
                                    if (a.sortValue1 < b.sortValue1) {
                                        return 1;
                                    }
                                    if (a.sortValue1 > b.sortValue1) {
                                        return -1;
                                    }
                                    if ((a.sortValue2 !== null)&&(typeof a.sortValue2 !== "undefined")) {
                                        if (a.sortValue2.indexOf("回") !== -1) {//マギアのランダムで回を第2キーに
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("回") - 1, 1) < b.sortValue2.substr(b.sortValue2.indexOf("回") - 1, 1))
                                                return 1;
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("回") - 1, 1) > b.sortValue2.substr(b.sortValue2.indexOf("回") - 1, 1))
                                                return -1;
                                        } else if (a.sortValue2.indexOf("T") !== -1) {//HP自動回復でターン数を第2キーに
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("T") - 1, 1) < b.sortValue2.substr(b.sortValue2.indexOf("T") - 1, 1))
                                                return 1;
                                            if (a.sortValue2.substr(a.sortValue2.indexOf("T") - 1, 1) > b.sortValue2.substr(b.sortValue2.indexOf("T") - 1, 1))
                                                return -1;
                                        }
                                    }
                                    return 0;
                                });
                            }
                        }
                        break;
                    }
                case "精神スキル":
                    {
                        //通常ローマ数字ソート
                        jsonData.personas.sort(function (a, b) {
                            let an = Number(ChangeRoman(a.sortValue1));
                            let bn = Number(ChangeRoman(b.sortValue1));
                            if (an < bn) {
                                return 1;
                            }
                            if (an > bn) {
                                return -1;
                            }
                            //第2ソート
                            if ((a.sortValue2 !== null)&&(typeof a.sortValue2 !== "undefined")) {
                                if (a.sortValue2.indexOf("敵") !== -1) {//状態異常でtargetを第2キーに
                                    if ((a.sortValue2==="敵単") > (b.sortValue2==="敵全"))
                                        return 1;
                                    if ((a.sortValue2==="敵単") < (b.sortValue2==="敵全"))
                                        return -1;
                                }
                            }
                            return 0;
                        });
                        break;
                    }
                case "精神アビ":
                    {
                        //特殊ソート
                        jsonData.personas.sort(function (a, b) {
                            if (a.sortValue1 < b.sortValue1) {
                                return 1;
                            }
                            if (a.sortValue1 > b.sortValue1) {
                                return -1;
                            }
                            return 0;
                        });
                        break;
                    }
                default:
                    {
                        jsonData.personas.sort(function (a, b) {
                            if (a.nameHiragana > b.nameHiragana)
                                return 1;
                            if (a.nameHiragana < b.nameHiragana)
                                return -1;
                            return 0;
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
            case "コネクト値":
            case "精神スキル":
                {
                returnValue = "sortValue"
                break;
                }
            
            case "精神アビ":
                {
                    returnValue = "sortValueS"
                    break;
                }
        }
        return returnValue;
    }

    //精神強化考慮チェック処理
    $('input[name="ctl00$MainContent$espiritu"]').change(function () {
        if ($("#MainContent_espiritu").prop("checked"))
            $("#MainContent_espiritu2").prop("checked", true);
        else
            $("#MainContent_espiritu2").prop("checked", false);
        //HP,ATK,DEFのみソートしなおし
        let tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
        switch(tipoOrden){
            case "HP":
            case "ATK":
            case "DEF":
                {
                    let ordena = OrdenaMagia();
                    ReDraw(ordena);
                    break;
                }
        }
        IndicaResultado();
    });

    $('input[name="ctl00$MainContent$espiritu2"]').change(function () {
        if ($("#MainContent_espiritu2").prop("checked"))
            $("#MainContent_espiritu").prop("checked", true);
        else
            $("#MainContent_espiritu").prop("checked", false);
        //HP,ATK,DEFのみソートしなおし
        let tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
        switch(tipoOrden){
            case "HP":
            case "ATK":
            case "DEF":
                {
                    let ordena = OrdenaMagia();
                    ReDraw(ordena);
                    break;
                }
        }
        IndicaResultado();
    });


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
                case "Charge後ダメUP":
                    {
                        if ("Charge後ダメージUP" === valor[i].name)
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
                case "挑発":
                    {
                        if (("必ず挑発" === valor[i].name)||("確率で挑発" === valor[i].name))
                            flagP = 1;
                        break;
                    }
                case "回避無効":
                    {
                        if (("回避無効" === valor[i].name)||("確率で回避無効" === valor[i].name))
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

    

    // The Tool-Tip instance:
function ToolTipC(canvas, region, width) {

    var me = this,                                // self-reference for event handlers
        div = document.createElement("div"),      // the tool-tip div
        parent = canvas.parentNode,               // parent node for canvas
        visible = false;                          // current status
    
    // set some initial styles, can be replaced by class-name etc.
    div.style.cssText = "position:fixed;padding:7px;background:gold;pointer-events:none;width:" + width + "px";
    
    var number;
    let widthC;

    // show the tool-tip
    this.show = function(pos) {
      if (!visible) {                             // ignore if already shown (or reset time)
        visible = true;                           // lock so it's only shown once
        setDivPos(pos);                           // set position
        parent.appendChild(div);                  // add to parent of canvas
        // setTimeout(hide, timeout);                // timeout for hide
      }
    }
    
    // hide the tool-tip
    function hide() {
      visible = false;                            // hide it after timeout
      parent.removeChild(div);                    // remove from DOM
    }
  
    // check mouse position, add limits as wanted... just for example:
    function check(e) {
      var pos = getPos(e),
          posAbs = {x: e.clientX, y: e.clientY};  // div is fixed, so use clientX/Y
        
            
        //ポインタがキャラアイコン内にあるか判定
        let clickX = pos.x;
        let clickY = pos.y;
        number = -1;
        loop: for (let i = 0; i < personas; i++) {
            if ((clickX - charaX[i]) ** 2 + (clickY - charaY) ** 2 < charaR ** 2) {
                number = i;
                break;
            }
            if (number === -1) {
                if ((clickX - charaX[i] + Xoffset) ** 2 + (clickY - charaY - Yoffset) ** 2 < charaR ** 2) {
                    number = i;
                    break loop;
                }
            }
        }
        if (!visible &&
            pos.x >= region.x && pos.x < region.x + region.w &&
            pos.y >= region.y && pos.y < region.y + region.h) {
            
            if (number !== -1) {
                
                let obj = jsonData.personas[number];
                var tipoOrden = $("input[name='ctl00$MainContent$orden1']:checked").val();
                let text = obj.name;
                switch (tipoOrden) {
                    case "ATK":
                    case "DEF":
                    case "HP":
                        {
                            if ($("#MainContent_espiritu").prop("checked")) {
                                let hp = Number(obj.HP) + Number(obj.menteHP) || Number(obj.HP);
                                let atk = Number(obj.ATK) + Number(obj.menteATK) || Number(obj.ATK);
                                let def = Number(obj.DEF) + Number(obj.menteDEF) || Number(obj.DEF);
                                text += "<br/>" + "HP:" + hp;
                                text += "<br/>" + "ATK:" + atk;
                                text += "<br/>" + "DEF:" + def;
                            }
                            else{
                                text += "<br/>" + "HP:" + obj.HP;
                                text += "<br/>" + "ATK:" + obj.ATK;
                                text += "<br/>" + "DEF:" + obj.DEF;
                            }
                            widthC = obj.name.length * 17<80? 80:obj.name.length * 17;
                            break;
                        }
                    case "コネクト値":
                        {
                            let C = [obj.connectA1, obj.connectA2, obj.connectA3, obj.connectA4, obj.connectA5, obj.connectD1, obj.connectD2, obj.connectD3];
                            for (let i = 0; i < C.length; i++){
                                if (C[i].name !== "") {
                                    text += "<br/>" + C[i].name;
                                    if (C[i].value !== "")
                                        text += "[" + C[i].value + "]";
                                    if (C[i].turn !== "")
                                        text += " " + C[i].turn;
                                }
                            }
                            widthC = obj.name.length * 17<180? 180:obj.name.length * 17;
                            break;
                        }
                    case "マギア値":
                        {
                            let M = [obj.Magia1, obj.Magia2, obj.Magia3, obj.Magia4, obj.Magia5,obj.Magia6];
                            for (let i = 0; i < M.length; i++){
                                if (M[i].name !== "") {
                                    text += "<br/>" + M[i].name;
                                    if ((M[i].info1 !== "")&&(typeof M[i].info1 !== "undefined"))
                                        text += "/" + M[i].info1;
                                    if ((M[i].info2 !== "")&&(typeof M[i].info2 !== "undefined"))
                                        text += "/" + M[i].info2;
                                }
                            }
                            widthC = obj.name.length * 17<180? 180:obj.name.length * 17;
                            break;
                        }
                    case "精神スキル":
                        {
                            let MS = [obj.mSkill1, obj.mSkill2];
                            for (let i = 0; i < MS.length; i++){
                                if (MS[i].name !== "") {
                                    text += `<br/>${MS[i].name}`;
                                    if ((MS[i].value !== "") && (typeof MS[i].value !== "undefined")) {
                                        if (MS[i].value === "必ず")
                                            text = text.substring(0,text.length-MS[i].name.length) + "必ず" + MS[i].name;
                                        else
                                            text += "/" + MS[i].value;
                                    }
                                    if ((MS[i].target !== "")&&(typeof MS[i].target !== "undefined"))
                                        text += "/" + MS[i].target;
                                    if ((MS[i].turn !== "")&&(typeof MS[i].turn !== "undefined"))
                                        text += "/" + MS[i].turn;

                                }
                            }
                            widthC = obj.name.length * 17 < 180 ? 180 : obj.name.length * 17;
                            break;
                        }
                    case "精神アビ":
                        {
                            let MA = [obj.mAbi1,obj.mAbi2,obj.mAbi3,obj.mAbi4,obj.mAbi5,obj.mAbi6,obj.mAbi7,obj.mAbi8,obj.mAbi9,obj.mAbi10,obj.mAbi11,obj.mAbi12,obj.mAbi13,obj.mAbi14,obj.mAbi15,obj.mAbi16,obj.mAbi17,obj.mAbi18,obj.mAbi19];
                            MA.sort();
                            text += " -精神アビ一覧-";
                            for (let i = 0; i < MA.length; i++){
                                if(MA[i].name !== ""){
                                    switch(MA[i].name){
                                        case "ドッペル＆マギアダメージUP":
                                            {
                                                text += `<br/>ドッペルダメージUP[${MA[i].value1}]&マギアダメージUP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "HP最大時攻撃力＆防御力UP":
                                            {
                                                text += `<br/>HP最大時攻撃力UP[${MA[i].value1}]&防御力UP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "BlastダメージUP＆AcceleMPUP":
                                            {
                                                text += `<br/>BlastダメージUP[${MA[i].value1}]&AcceleMPUP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "瀕死時攻撃力＆防御力UP":
                                            {
                                                text += `<br/>瀕死時攻撃力UP[${MA[i].value1}]&防御力UP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "瀕死時防御力UP＆防御力UP":
                                            {
                                                text += `<br/>瀕死時防御力UP[${MA[i].value1}]&防御力UP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "攻撃力UP＆瀕死時攻撃力UP":
                                            {
                                                text += `<br/>攻撃力UP[${MA[i].value1}]&瀕死時攻撃力UP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "HP自動回復&MP自動回復":
                                            {
                                                text += `<br/>HP自動回復[${MA[i].value1}]&MP自動回復[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "Charge後ダメージUP＆ChargeディスクダメージUP":
                                            {
                                                text += `<br/>Charge後ダメージUP[${MA[i].value1}]&ChargeディスクダメージUP[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "Charge combo時charge数+":
                                            {
                                                text += `<br/>Charge combo時charge数[+${MA[i].value1}]`;
                                                break;
                                            }
                                        case "攻撃時確率でスタン付与＆確率でクリティカル":
                                            {
                                                text += `<br/>攻撃時確率でスタン付与[${MA[i].value1}]&確率でクリティカル[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "拘束無効＆スタン無効":
                                            {
                                                if(MA[i].value1 === "必ず")
                                                    text += `<br/>必ず拘束無効`;
                                                else
                                                    text += `<br/>確率で拘束無効[${MA[i].value1}]`;
                                                if(MA[i].value2 === "必ず")
                                                    text += `&必ずスタン無効`;
                                                else
                                                    text += `&確率でスタン無効[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "確率で回避＆カウンター無効":
                                            {
                                                if(MA[i].value1 === "必ず")
                                                    text += `<br/>必ず回避無効`;
                                                else
                                                    text += `<br/>確率で回避無効[${MA[i].value1}]`;
                                                if(MA[i].value2 === "必ず")
                                                    text += `&必ずカウンター無効`;
                                                else
                                                    text += `&確率でカウンター無効[${MA[i].value2}]`;
                                                break;
                                            }
                                        case "呪い無効":
                                        case "確率で呪い無効":
                                        case "拘束無効":
                                        case "確率で拘束無効":
                                        case "魅了無効":
                                        case "確率で魅了無効":
                                        case "幻惑無効":
                                        case "確率で幻惑無効":
                                        case "スタン無効":
                                        case "確率でスタン無効":
                                        case "毒無効":
                                        case "確率で毒無効":
                                        case "スキル不可無効":
                                        case "確率でスキル不可無効":
                                        case "マギア不可無効":
                                        case "確率でマギア不可無効":
                                        case "回避無効":
                                        case "確率で回避無効":
                                        {
                                            let textSub = MA[i].name.indexOf("確率で")===-1 ? MA[i].name : MA[i].name.replace("確率で","");
                                            if(MA[i].value1 === "必ず")
                                                text += `<br/>必ず${textSub}`;
                                            else
                                                text += `<br/>確率で${textSub}[${MA[i].value1}]`;
                                            break;
                                        }
                                        default:
                                            text += "<br/>" + MA[i].name;
                                            if ((MA[i].value1 != "") && (typeof MA[i].value1 != "undefined")) {
                                                text += `[${MA[i].value1}]`;
                                            }
                                            if ((MA[i].value2 != "") && (typeof MA[i].value2 != "undefined")) {
                                                text += `/${MA[i].value2}`;
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        }
                    default:
                        {
                            text = "";
                        }
                }
                if (text !== "") {
                    div.innerHTML = text;
                    
                    div.style.cssText = "position:fixed;padding:7px;background:gold;pointer-events:none;z-index:1;width:" + widthC + "px";
                    me.show(posAbs);                          // show tool-tip at this pos
                }
            }
        }
        else if (number === -1)
            hide();
        else if ((clickX > canvas.width) || (clickX < 0) || (clickY < 0) || (clickY > canvas.heigth))
            hide();
        else {
            setDivPos(posAbs);                     // otherwise, update position
        }
    }
    
    // get mouse position relative to canvas
    function getPos(e) {
      var r = canvas.getBoundingClientRect();
      return {x: e.clientX - r.left, y: e.clientY - r.top}
    }
    
    // update and adjust div position if needed (anchor to a different corner etc.)
    function setDivPos(pos) {
      if (visible){
        if (pos.x < 0) pos.x = 0;
        if (pos.y < 0) pos.y = 0;
        // other bound checks here
        let right = pos.x - widthC;
        if (charaX[number] < Xoffset) {
            //一段目の一番右の場合で、更にタマの左半分が上端にある場合は、下段も描写したい
            if (charaX[number] + charaR / 2 > Xoffset - charaR) {
                div.style.left = right + "px";
            }
            else
            div.style.left = pos.x + "px";
        }
        else if (charaX[number] < Xoffset * 2) {
            //二段目の一番右の場合
            if (charaX[number] + charaR / 2 > Xoffset *2 - charaR) {
                div.style.left = right + "px";
            }
            else
            div.style.left = pos.x + "px";
        }
        else
            div.style.left = pos.x + "px";
        div.style.top = pos.y + "px";
      }
    }
    
    // we need to use shared event handlers:
    canvas.addEventListener("mousemove", check);
    // canvas.addEventListener("click", check);
    canvas.addEventListener("mouseout", hide);
    
  }

}//draw3ここまで

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
            case "BlastダメUP":
                {
                    if ("BlastダメージUP" === valor[i].name)
                        flagM = 1;
                break;
                }
            case "Charge後ダメUP":
            {
                if ("Charge後ダメージUP" === valor[i].name)
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

//精神強化スキルフィルタ用
//selector 1はvalue、2はtarget、3はturnを返す 0は不要な場合
function BusquedaMskill(parabra, value, selector) {
    var vuelta = [2];
    var valor = [value.mSkill1, value.mSkill2];
    var flagMskill = 0;
    for (let i = 0; i < valor.length; i++) {
        switch (parabra) {
            case "クリティカル":
                {
                    if ("確率でクリティカル" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "ダメージUP":
                {
                    if ("与えるダメージUP" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "BlastダメUP":
                {
                    if ("BlastダメージUP" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "Charge後ダメUP":
                {
                    if ("Charge後ダメージUP" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "追撃":
                {
                    if ("確率で追撃" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "ダメCUT状態":
                {
                    if ("ダメージカット状態" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "ダメCUT無視":
                {
                    if ("ダメージカット無視" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "回避":
                {
                    if ("確率で回避" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "防御無視":
                {
                    if ("確率で防御力無視" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "拘束":
                {
                    if ("確率で拘束" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "魅了":
                {
                    if ("確率で魅了" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "幻惑":
                {
                    if ("確率で幻惑" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "スタン":
                {
                    if ("確率でスタン" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "呪い":
                {
                    if ("確率で呪い" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "霧":
                {
                    if ("確率で霧" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
            case "暗闇":
                {
                    if ("確率で暗闇" === valor[i].name)
                        flagMskill = 1;
                    break;
                }
        }
    

        if ((parabra === valor[i].name) || (flagMskill === 1)) {
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
                        vuelta[1] = valor[i].target;
                        break;
                    }
                case 3:
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

//精神強化アビフィルタ用（これだけ特殊仕様)
//戻り値は、0:検索結果 1:normalValue 文字合計 2:specialValue 数字合計
//特殊sum処理がある
//また他と違い、データ内のvalue1,value2を項目により自動判定して結果を出力する
function BusquedaMabi(parabra, value) {
    var vuelta = [3];
    var normalValue = "";
    var specialValue = 0;
    var numeroDeAbi = 0;//精神強化効果値の計算に用いる。複数アビ合算時のみ使用
    var valor = [value.mAbi1, value.mAbi2, value.mAbi3, value.mAbi4, value.mAbi5, value.mAbi6, value.mAbi7, value.mAbi8, value.mAbi9, value.mAbi10,
        value.mAbi11,value.mAbi12,value.mAbi13,value.mAbi14,value.mAbi15,value.mAbi16,value.mAbi17,value.mAbi18,value.mAbi19,value.mAbi20];
    var flagMabi = 0;

    for (let i = 0; i < valor.length; i++){
        switch (parabra) {
            //通常value1系
            case "クリティカル":
                {
                    if ("確率でクリティカル" === valor[i].name) {
                        normalValue = valor[i].value1;
                        flagMabi = 1;
                    }
                    break;
                }
            case "ダメージUP":
                {
                    if ("与えるダメージUP" === valor[i].name) {
                        let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                        normalValue += s;
                        specialValue += ChangeRoman(valor[i].value1);
                        flagMabi = 1;
                        numeroDeAbi++;
                    }
                    break;
                }

            //通常value2系

            //special系
            case "攻撃力UP":
                {
                    switch (valor[i].name) {
                        case "攻撃力UP":
                        case "攻撃力UP＆瀕死時攻撃力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "瀕死時攻撃力UP":
                {
                    switch(valor[i].name){
                        case "瀕死時攻撃力UP":
                        case "瀕死時攻撃力＆防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "攻撃力UP＆瀕死時攻撃力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "HP最大時攻撃力UP":
                {
                    switch (valor[i].name) {
                        case "HP最大時攻撃力UP":
                        case "HP最大時攻撃力＆防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "防御力UP":
                {
                    switch (valor[i].name) {
                        case "防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "瀕死時防御力UP＆防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "瀕死時防御力UP":
                {
                    switch(valor[i].name){
                        case "瀕死時防御力UP":
                        case "瀕死時防御力UP＆防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "瀕死時攻撃力＆防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "HP最大時防御力UP":
                {
                    switch (valor[i].name) {
                        case "HP最大時防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "HP最大時攻撃力＆防御力UP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "BlastダメUP":
                {
                    switch (valor[i].name) {
                        case "BlastダメージUP":
                        case "BlastダメージUP＆AcceleMPUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "Charge後ダメUP":
                {
                    switch (valor[i].name) {
                        case "Charge後ダメージUP":
                        case "Charge後ダメージUP＆ChargeディスクダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "Charge板ダメUP":
                {
                    switch (valor[i].name) {
                        case "ChargeディスクダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "Charge後ダメージUP＆ChargeディスクダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "状態異常時ダメUP":
                {
                    switch (valor[i].name) {
                        case "敵状態異常時ダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "マギアダメージUP":
                {
                    switch (valor[i].name) {
                        case "マギアダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "ドッペル＆マギアダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "ドッペルダメージUP":
                {
                    switch (valor[i].name) {
                        case "ドッペルダメージUP":
                        case "ドッペル＆マギアダメージUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "ダメUP状態":
                {
                    switch(valor[i].name){
                        case "ダメージアップ状態":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "ダメCUT状態":
                    {
                        switch(valor[i].name){
                            case "ダメージカット状態":
                                {
                                    let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                    normalValue += s;
                                    specialValue += ChangeRoman(valor[i].value1);
                                    flagMabi = 1;
                                    numeroDeAbi++;
                                    break;
                                }
                        }
                        break;
                    }
    
            case "属性ダメCUT状態"://足せないタイプ もし足せるタイプになれば後で修正
                {
                    switch(valor[i].name){
                        case "火属性ダメージカット状態":
                            {
                                normalValue = "火:" + valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "水属性ダメージカット状態":
                            {
                                normalValue = "水:" + valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "木属性ダメージカット状態":
                            {
                                normalValue = "木:" + valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "光属性ダメージカット状態":
                            {
                                normalValue = "光:" + valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "闇属性ダメージカット状態":
                            {
                                normalValue = "闇:" + valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "火水木属性ダメージカット状態":
                        {
                            normalValue = "火水木:" + valor[i].value1;//文字
                            specialValue = ChangeRoman(valor[i].value1);//数字
                            flagMabi = 1;
                            break;
                        }
                    }
                    break;
                }
            case "回避"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "確率で回避":
                        case "確率で回避＆カウンター無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "回避無効"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "確率で回避無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "カウンター無効"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "確率でカウンター無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "確率で回避＆カウンター無効":
                            {
                                normalValue = valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value2);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "防御無視"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "確率で防御無視":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "ダメージカット無視"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "確率でダメージカット無視":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "挑発無視"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "確率で挑発無視":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "呪い付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で呪い付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "拘束付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で拘束付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "魅了付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で魅了付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "幻惑付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で幻惑付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "スタン付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率でスタン付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "毒付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で毒付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "やけど付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率でやけど付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "霧付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で霧付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "暗闇付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率で暗闇付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "スキル不可付与"://足せないタイプ？
                {
                    switch (valor[i].name) {
                        case "攻撃時確率でスキル不可付与":
                            {
                                normalValue = valor[i].value1 + "," + valor[i].value2;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "スキルクイック":
                {
                    switch (valor[i].name) {
                        case "確率でスキルクイック":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "HP自動回復":
                {
                    switch(valor[i].name){
                        case "HP自動回復":
                        case "HP自動回復&MP自動回復":
                        {
                            let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                            normalValue += s;
                            specialValue += ChangeRoman(valor[i].value1);
                            flagMabi = 1;
                            numeroDeAbi++;
                            break;
                        }
                    }
                    break;
                }
            case "MP自動回復":
                {
                    switch(valor[i].name){
                        case "MP自動回復":
                        {
                            let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                            normalValue += s;
                            specialValue += ChangeRoman(valor[i].value1);
                            flagMabi = 1;
                            numeroDeAbi++;
                            break;
                        }
                        case "HP自動回復&MP自動回復":
                        {
                            let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                            normalValue += s;
                            specialValue += ChangeRoman(valor[i].value2);
                            flagMabi = 1;
                            numeroDeAbi++;
                            break;
                        }

                    }
                    break;
                }
            case "AcceleMPUP":
                {
                    switch (valor[i].name) {
                        case "AcceleMPUP":
                        case "AcceleMPUP＆確率でクリティカル":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                        case "BlastダメージUP＆AcceleMPUP":
                            {
                                let s = specialValue === 0 ? valor[i].value2 : "," + valor[i].value2;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value2);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "MP獲得量UP":
                {
                    switch (valor[i].name) {
                        case "MP獲得量UP":
                        case "MP獲得量UP＆弱点属性で攻撃されたときMPUP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "MP100以上時MP獲得量UP":
                {
                    switch (valor[i].name) {
                        case "MP100以上時MP獲得量UP":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "Blast攻撃時MP獲得":
                {
                    switch (valor[i].name) {
                        case "Blast攻撃時MP獲得":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                            }
                    }
                    break;
                }
            case "呪い無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "呪い無効":
                        case "確率で呪い無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "拘束無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "拘束無効":
                        case "確率で拘束無効":
                        case "拘束無効＆スタン無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "魅了無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "魅了無効":
                        case "確率で魅了無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "幻惑無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "幻惑無効":
                        case "確率で幻惑無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "スタン無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "スタン無効":
                        case "確率でスタン無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                        case "拘束無効＆スタン無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value2);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "毒無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "毒無効":
                        case "確率で毒無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "スキル不可無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "スキル不可無効":
                        case "確率でスキル不可無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "マギア不可無効"://足せないタイプ
                {
                    switch (valor[i].name) {
                        case "マギア不可無効":
                        case "確率でマギア不可無効":
                            {
                                normalValue = valor[i].value1;//文字
                                specialValue = ChangeRoman(valor[i].value1);//数字
                                flagMabi = 1;
                                break;
                            }
                    }
                    break;
                }
            case "マギアダメカット":
                {
                    if ("マギアダメージカット" === valor[i].name) {
                        normalValue = valor[i].value1;
                        specialValue = ChangeRoman(valor[i].value1);//数字
                        flagMabi = 1;
                    }
                    else if("アクセル＆マギアダメージカット" === valor[i].name) {
                        normalValue = valor[i].value2;
                        specialValue = ChangeRoman(valor[i].value2);//数字
                        flagMabi = 1;
                    }
                    break;
                }
            case "Charge数+"://足せないタイプ
                {//+2が出てきたら項目名を考える
                    switch (valor[i].name) {
                        case "Charge combo時charge数+":
                            {
                                let s = specialValue === 0 ? valor[i].value1 : "," + valor[i].value1;
                                normalValue += s;
                                specialValue += ChangeRoman(valor[i].value1);
                                flagMabi = 1;
                                numeroDeAbi++;
                                break;
                                break;
                            }
                    }
                    break;
                }
            default://"対魔女ダメージアップ" "状態異常耐性UP"
                {
                    if (parabra === valor[i].name) {
                        normalValue = valor[i].value1;
                        flagMabi = 1;
                    }
                    break;
                }
           
        }
    }

    if (flagMabi === 1) {
        vuelta[0] = true;
        vuelta[1] = normalValue;
        vuelta[2] = specialValue;
        vuelta[3] = numeroDeAbi;
        return vuelta;
    }
    return false;
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
        case "ⅩⅡ": return 12;
        case "ⅩⅢ": return 13;
        case "ⅩⅣ": return 14;
        case "ⅩⅤ": return 15;
        case "ⅩⅥ": return 16;
        case "ⅩⅦ": return 17;
        case "ⅩⅧ": return 18;
        case "ⅩⅨ": return 19;
        case "ⅩⅩ": return 20;
        case "ⅩⅩⅤ": return 25;
        case "必ず": return 100;
        case "最大まで": return 100;
        case "10%": return 10;
        case "15%": return 15;
        case "1回": return 1;
        case "2回": return 2;
        case "3回": return 3;
        default: return 0;
    }
}

function ChangeToRoman(decimal) {
    switch (decimal) {
        case 1: return "Ⅰ";
        case 2: return "Ⅱ";
        case 3: return "Ⅲ";
        case 4: return "Ⅳ";
        case 5: return "Ⅴ";
        case 6: return "Ⅵ";
        case 7: return "Ⅶ";
        case 8: return "Ⅷ";
        case 9: return "Ⅸ";
        case 10: return "Ⅹ";
        case 11: return "Ⅺ";
        case 12: return "ⅩⅡ";
        case 13: return "ⅩⅢ";
        case 14: return "ⅩⅣ";
        case 15: return "ⅩⅤ";
        case 16: return "ⅩⅥ";
        case 17: return "ⅩⅦ";
        case 18: return "ⅩⅧ";
        case 19: return "ⅩⅨ";
        case 20: return "ⅩⅩ";
        case 21: return "ⅩⅩⅠ";
        case 22: return "ⅩⅩⅡ";
        case 23: return "ⅩⅩⅢ";
        case 24: return "ⅩⅩⅣ";
        case 25: return "ⅩⅩⅤ";
        case "+1": return false;
        default: return false;
    }
}


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
    ctx.font = "900 12px sans-serif";

    var textW = Math.ceil(ctx.measureText(text).width) + 5;
    var textH = 15;
    ProcessImage(ctx, x + r/6, y - r*5/6,textW,textH);

    ctx.strokeStyle = "yellow";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.strokeText(text, x + r / 6, y - r *5 / 6);
    ctx.restore();
    ctx.save();
    ctx.font = "600 12px sans-serif";
    ctx.fillStyle = "black";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(text, x + r / 6, y - r *5 / 6);
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



//////////////////////////////////////////////////////
//メモリア
//////////////////////////////////////////////////////

var mData;
var mDataOri;
var elegidaRow = -1;
var elegidaCol = -1;
var canvasFlagM;
var y;

var memoriaElegido = new Array(3);
for (let i = 0; i < 3; i++)
    memoriaElegido[i] = new Array(4);
// var memoriaAjustadoA;//アビ用
// var memoriaAjustadoS = new Array(2);//スキル用
var AtkAjustado = new Array(3);

//ダメージ計算に関係ある系
// 0:攻撃力UP
// 1:与えるダメージUP
// 2:Charge後ダメージUP
// 3:BlastダメージUP
// 4:敵状態異常時ダメージUP
// 5:防御力無視
// 6:防御力DOWN
// 7:ダメージアップ状態
// 8:HP最大時攻撃力UP  まだダメ
// 9:瀕死時攻撃力UP    まだダメ

//ダメージカット無視
//瀕死時攻撃力UPの扱い
//
//ダメージ計算に関係無い系
// 10:AccelMPUP
// 11:MP獲得量UP
// 12:MP回復
// 13:クリティカル    まだダメ
// 14:

//スタン,霧,魅了,毒,呪い,やけど,暗闇,拘束



function GetMemoria() {
    let memoriaAjustado = new Array(3);
    for (let i = 0; i < 3; i++){
        memoriaAjustado[i] = new Array(4);
        for (let j = 0; j < 4;j++)
            memoriaAjustado[i][j] = new Array(20);
    }
    //メモリアがセットされた場合、その値をmemoriaAjustadoに記入する
    for (let i = 0; i < 3; i++){
        for (let j = 0; j < 4; j++){
            if (typeof memoriaElegido[i][j] !== "undefined") {
                let resultado;
                if (BusquedaMemoria("攻撃力UP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("攻撃力UP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("攻撃力UP", memoriaElegido[i][j], 1);
                    memoriaAjustado[i][j][0] = (ChangeRoman(resultado[1]) * 5);
                }
                if (BusquedaMemoria("与えるダメージUP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("与えるダメージUP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("与えるダメージUP", memoriaElegido[i][j], 1);
                    memoriaAjustado[i][j][1] = (ChangeRoman(resultado[1]) * 5);
                }
                if (BusquedaMemoria("Charge後ダメージUP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("Charge後ダメージUP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("Charge後ダメージUP", memoriaElegido[i][j], 1);
                    memoriaAjustado[i][j][2] = (ChangeRoman(resultado[1]) * 2.5) + 2.5;
                }
                if (BusquedaMemoria("BlastダメージUP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("BlastダメージUP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("BlastダメージUP", memoriaElegido[i][j], 1);
                    memoriaAjustado[i][j][3] = (ChangeRoman(resultado[1]) * 5) + 10;
                }
                if (BusquedaMemoria("敵状態異常時ダメージUP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("敵状態異常時ダメージUP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("敵状態異常時ダメージUP", memoriaElegido[i][j], 1);
                    memoriaAjustado[i][j][4] = (ChangeRoman(resultado[1]) * 5) + 10;
                }
                if (BusquedaMemoria("防御力無視", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("防御力無視", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("防御力無視", memoriaElegido[i][j], 1);
                    // if (memoriaAjustado[i][j][5] !== "必ず") {
                        if (resultado[1] === "必ず")
                            memoriaAjustado[i][j][5] = "必ず";
                        else {//計算式不明の為テキトウに数値計算。不明のうちは"必ず"以外は使わない
                            // if (typeof memoriaAjustado[i][j][5] === "undefined")
                            memoriaAjustado[i][j][5] = (ChangeRoman(resultado[1]) * 5);
                            // else
                            //     memoriaAjustado[i][j][5] += (ChangeRoman(resultado[1]) * 5);
                        }
                }
                if (BusquedaMemoria("防御力DOWN", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("防御力DOWN", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("防御力DOWN", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][6] === "undefined")
                    memoriaAjustado[i][j][6] = (ChangeRoman(resultado[1]) * 5);
                    // else
                    //     memoriaAjustado[i][j][6] += (ChangeRoman(resultado[1]) * 5);
                }
                if (BusquedaMemoria("ダメージアップ状態", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("ダメージアップ状態", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("ダメージアップ状態", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][7] === "undefined")
                    memoriaAjustado[i][j][7] = (ChangeRoman(resultado[1]) * 5);
                    // else
                    //     memoriaAjustado[i][j][7] += (ChangeRoman(resultado[1]) * 5);
                }
                if (BusquedaMemoria("HP最大時攻撃力UP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("HP最大時攻撃力UP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("HP最大時攻撃力UP", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][8] === "undefined")
                    memoriaAjustado[i][j][8] = (ChangeRoman(resultado[1]) * 5);
                    // else
                    //     memoriaAjustado[i][j][8] += (ChangeRoman(resultado[1]) * 5);
                }
                if (BusquedaMemoria("瀕死時攻撃力UP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("瀕死時攻撃力UP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("瀕死時攻撃力UP", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][9] === "undefined")
                    memoriaAjustado[i][j][9] = (ChangeRoman(resultado[1]) * 5);
                    // else
                    //     memoriaAjustado[i][j][9] += (ChangeRoman(resultado[1]) * 5);
                }
                if (BusquedaMemoria("AcceleMPUP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("AcceleMPUP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("AcceleMPUP", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][10] === "undefined")
                    memoriaAjustado[i][j][10] = (ChangeRoman(resultado[1]) * 2.5) + 7.5;
                    // else
                    //     memoriaAjustado[i][j][10] += (ChangeRoman(resultado[1]) * 2.5) + 7.5;
                }
                if (BusquedaMemoria("MP獲得量UP", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("MP獲得量UP", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("MP獲得量UP", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][11] === "undefined")
                    memoriaAjustado[i][j][11] = (ChangeRoman(resultado[1]) * 2.5) + 2.5;
                    // else
                    //     memoriaAjustado[i][j][11] += (ChangeRoman(resultado[1]) * 2.5) + 2.5;
                }
                if (BusquedaMemoria("MP回復", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("MP回復", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("MP回復", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][12] === "undefined")
                    memoriaAjustado[i][j][12] = (ChangeRoman(resultado[1]) * 2.5) + 2.5;
                    // else
                    //     memoriaAjustado[i][j][12] += (ChangeRoman(resultado[1]) * 2.5) + 2.5;
                }
                if (BusquedaMemoria("確率でクリティカル", memoriaElegido[i][j], 0)) {
                    if (memoriaElegido[i][j].totu)
                        resultado = BusquedaMemoria("確率でクリティカル", memoriaElegido[i][j], 2);
                    else
                        resultado = BusquedaMemoria("確率でクリティカル", memoriaElegido[i][j], 1);
                    // if (typeof memoriaAjustado[i][j][13] === "undefined")
                    memoriaAjustado[i][j][13] =(ChangeRoman(resultado[1]) * 5) + 5;
                    // else
                    //     memoriaAjustado[i][j][13] = Math.max((ChangeRoman(resultado[1]) * 5) + 5, memoriaAjustado[i][j][13]);
                }
            }
        }//j
    }//i

    //初期化
    memoriaAjustadoA.length = 0;
    memoriaAjustadoA = new Array(3);
    memoriaAjustadoS = new Array(2);
    memoriaAjustadoS[0] = new Array(3);
    memoriaAjustadoS[1] = new Array(3);
    for (let i = 0; i < 3; i++){
        memoriaAjustadoA[i] = new Array(20);
        memoriaAjustadoS[0][i] = new Array(20);
        memoriaAjustadoS[1][i] = new Array(20);
    }
    for (let i = 0; i < 3; i++) {
        //アビリティ
        for (let j = 0; j < 2; j++){
            for (let k = 0; k < 20; k++){
                if (typeof memoriaAjustado[i][j][k] !== "undefined") {
                    if (typeof memoriaAjustadoA[i][k] !== "undefined") {
                        if (k === 5)//防御力無視
                        {
                            if (memoriaAjustadoA[i][k] !== "必ず") {
                                if (memoriaAjustado[i][j][k] === "必ず")
                                    memoriaAjustadoA[i][k] === "必ず";
                                else {
                                    memoriaAjustadoA[i][k] += memoriaAjustado[i][j][k];
                                }
                            }
                        }
                        else if (k === 13) {//確率でクリティカル
                                memoriaAjustadoA[i][k] = Math.max(memoriaAjustado[i][j][k], memoriaAjustadoA[i][k]);
                        }
                        else
                            memoriaAjustadoA[i][k] += memoriaAjustado[i][j][k];
                        
                    }
                    else 
                        memoriaAjustadoA[i][k] = memoriaAjustado[i][j][k];
                }
            }
        }
        //スキル
        for (let j = 2; j < 4; j++){
            for (let k = 0; k < 20; k++){
                if (typeof memoriaAjustado[i][j][k] !== "undefined") {
                    [j-2][i][k] = memoriaAjustado[i][j][k];
                }
            }
        }
    }

}

function GetAtk() {
    AtkAjustado.length = 0;
    for (let i = 0; i < 3; i++)
        AtkAjustado[i] = { "ATK": 0, "DEF": 0, "HP": 0 };
    for (let i = 0; i < 3; i++){
        for (let j = 0; j < 4; j++) {
            if (typeof memoriaElegido[i][j] !== "undefined") {
                AtkAjustado[i].ATK += memoriaElegido[i][j].ATK;
                AtkAjustado[i].DEF += memoriaElegido[i][j].DEF;
                AtkAjustado[i].HP += memoriaElegido[i][j].HP;
                    //escogidoに格納
                
            }
        }
        escogido.personas[i].mAtk = AtkAjustado[i].ATK;
    }
    
}

function BusquedaMemoria(parabra, value, selector) {
    //valueの中から、parabraに一致するものを探し出す
    var vuelta = [2];
    var valor = [value.efecto1, value.efecto2, value.efecto3, value.efecto4];

    var flagM = 0;

    for (let i = 0; i < valor.length;i++){
        switch (parabra) {
            case "ダメージUP":
                {
                    if ("与えるダメージUP" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "BlastダメUP":
                {
                    if ("BlastダメージUP" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "Charge後ダメUP":
                {
                    if ("Charge後ダメージUP" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "クリティカル":
                {
                    if ("確率でクリティカル" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "ダメUP状態":
                {
                    if ("ダメージアップ状態" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "ダメCUT状態":
                {
                    if ("ダメージカット状態" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "ダメージDOWN":
                {
                    if ("与えるダメージDOWN" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "MP溜まった状態":
                {
                    if ("自分のMPが10%溜まった状態でバトル開始" === valor[i].name)
                        flagM = 1;
                    else if ("自分のMPが15%溜まった状態でバトル開始" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "かばう":
                {
                    if ("かばう" === valor[i].name)
                        flagM = 1;
                    else if("瀕死の味方を必ずかばう" === valor[i].name)
                        flagM = 1;
                    break;
                    
                }
            case "デバフ無効":
                {
                    if ("デバフ効果無効" === valor[i].name)
                        flagM = 1;
                    break;
                }
            case "回避無効":
                {
                    if ("回避無効" === valor[i].name)
                        flagM = 1;
                    else if("確率で回避無効" === valor[i].name)
                        flagM = 1;
                    break;
                }
        }
        if ((parabra === valor[i].name)||flagM===1) {
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
                        vuelta[1] = valor[i].valueMax;
                        break;
                    }
                case 3:
                    {
                        vuelta[1] = valor[i].target;
                        break;
                    }
                case 4:
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

function draw5() {
    var canvas5;
    var w = $(window).width();
    var wSize = 768;
    

    if (w < wSize) {
        //画面サイズが768px未満のときの処理
        canvas5 = document.getElementById("canvas5");
        canvasFlagM = 1;
    }
    else {
        canvas5 = document.getElementById("canvas51");
        canvasFlagM = 0;
    }
    if (!canvas5 || !canvas5.getContext) {
        return;
    }
    

    //設定
    var ctx5 = canvas5.getContext("2d");

    //Json読み込み
    var xhr = new XMLHttpRequest;
    var memorias;  
    (function (handleload) {
        // var xhr = new XMLHttpRequest;

        xhr.addEventListener('load', handleload, false);
        xhr.open('GET', 'Scripts/magia_json/memoria.json', false);//同期処理。
        xhr.send(null);
        //xhr.send();
    }(function handleLoad(event) {
        var xhr = event.target,
            obj = JSON.parse(xhr.responseText);
        
        memorias = obj.tarjetas.length;
        console.log(obj);
        mDataOri = obj;
        mData = angular.copy(mDataOri);
    }));

      
    var scaleF = 1;
    var len = 60;
    var y0 = len;
    var offsetM = len +20;
    
    var letraM = [memorias];

    var Xoffset = canvas5.width;
    var Yoffset = canvas5.height;

    
    //列数
    var horizontal = Math.floor((Xoffset - 20) / (len + 20));
    var marginX = ((Xoffset - 20) % (len + 20) + 20)/2;
    var marginY = 30;

    var x = [horizontal];
    for (let i = 0; i < horizontal; i++)
        x[i] = i === 0 ? marginX + offsetM/2 : x[i - 1] + offsetM;
    
    //行数
    var vertical = Math.ceil(memorias / x.length);
    var yOri = [vertical];

    for(let i = 0; i < vertical; i++){
    yOri[i] = i === 0 ? y0 : yOri[i-1] + offsetM;
    letraM[i] = i + 1;
    }

    y = angular.copy(yOri);

    //imageのみ準備
    var imageAryM = [memorias];
    for (let i = 0; i < memorias; i++) {
        imageAryM[i] = new Image();
        imageAryM[i].src = "png/m/" + mData.tarjetas[i].data;
    }

    //初期描画
    for (let i = 0; i < vertical; i++){
        for (let j = 0; j < x.length; j++){
            if ((i * horizontal + j) >= memorias) {
                break;
            }
            DrawMemoria(ctx5,x[j],y[i],len,imageAryM[i*horizontal+j],1);
            // DrawText(ctx5,x[j],y[i],letra[i*x.length+j],1);
        }
    }

    //メモリア数表示
    document.getElementById("MainContent_indicaMemoria").innerText = "登録 " + mDataOri.tarjetas.length + "メモリア表示";
    var region = { x: 0, y: 0, w: canvas5.width, h: canvas5.height };
    
    
    //スクロール処理
    //スクロール関係処理
    var scrollOff = function( e ){
        e.preventDefault();
    }
    // スクロール禁止
    function no_scroll() {
        // スクロールをキャンセル
        canvas5.addEventListener( 'touchmove',scrollOff, false);
    }
    // スクロール禁止解除
    function return_scroll() {
        //removeEventListenerで「スクロールをキャンセル」をキャンセル
        canvas5.removeEventListener('touchmove', scrollOff, false);
    }


    var timer = null;
    var pointerFlagM = false;
    // console.log(pointerFlag + "0");

    
    var clickX;
    var clickY;

    var numberRow;
    var numberCol;
    var isStaticM = 0;

    canvas5.addEventListener("pointerdown", function (e) {
        pointerFlagM = false;
        isStaticM = 1;
        
        //エリア内ならフラグon
        //ポインタがcanvas内にあるか判定
        const rectM = canvas5.getBoundingClientRect();
        clickX = e.clientX - rectM.left;
        clickY = e.clientY - rectM.top;
        if((clickX< canvas5.width)&&(clickY<canvas5.height)){
            pointerFlagM = true;
            no_scroll();
        console.log(pointerFlagM + " down");
        }
        else {
            return_scroll();
        }
        
        //クリックした物体の判定処理
        // if(number === -1)
        //     return;
        numberCol = -1;//j
        numberRow = -1;//i
        //ポインタがメモリア内にあるか判定
        loop:for (let i = 0; i < vertical; i++){
            for (let j = 0; j < x.length; j++) {
                if ((clickX < x[j] + len / 2) && (x[j] - len / 2 < clickX) && (clickY < y[i] + len / 2) && (y[i] - len / 2 < clickY)) {
                    numberRow = i;
                    numberCol = j;
                    j = x.length - 1;
                    i = vertical - 1;
                    break loop;
                }
            }
        }
    });

    canvas5.addEventListener("dblclick", function (e) {
        //ポインタがエリア内にあるか        
        const rectM = canvas5.getBoundingClientRect();
        clickX = e.clientX - rectM.left;
        clickY = e.clientY - rectM.top;
        numberCol = -1;//j
        numberRow = -1;//i
        //ポインタがメモリア内にあるか判定
        loop:for (let i = 0; i < vertical; i++){
            for (let j = 0; j < x.length; j++) {
                if ((clickX < x[j] + len / 2) && (x[j] - len / 2 < clickX) && (clickY < y[i] + len / 2) && (y[i] - len / 2 < clickY)) {
                    numberRow = i;
                    numberCol = j;
                    j = x.length - 1;
                    i = vertical - 1;
                    break loop;
                }
            }
        }
        if (numberRow !== -1 && numberCol !== -1) {
            ProcessImageRipple(ctx5, x[numberCol], y[numberRow], len);
            DrawRectM(ctx5,x[numberCol], y[numberRow], len);
            $("#selectModal").modal(
                {
                    keyboard: false
                });
        }
    });

    
    //ダブルタップ用
    let tapCount = 0;
    canvas5.addEventListener("touchstart", function (e) {
        console.log("canvas5.touchstart");
        // if (isIOS !== true) {
            
            // シングルタップ判定
            if (!tapCount) {
                ++tapCount;
                console.log("タップ " + tapCount + "回");
                setTimeout(function () {
                    tapCount = 0;
                }, 450);

                // ダブルタップ判定
            } else {
                console.log("ダブルタップ " + tapCount + "回");
                e.preventDefault();
                tapCount = 0;

                //ポインタがエリア内にあるか        
                const rectM = canvas5.getBoundingClientRect();
                clickX = e.touches[0].clientX - rectM.left;
                clickY = e.touches[0].clientY - rectM.top;
                numberCol = -1;//j
                numberRow = -1;//i
                //ポインタがメモリア内にあるか判定
                loop: for (let i = 0; i < vertical; i++) {
                    for (let j = 0; j < x.length; j++) {
                        if ((clickX < x[j] + len / 2) && (x[j] - len / 2 < clickX) && (clickY < y[i] + len / 2) && (y[i] - len / 2 < clickY)) {
                            numberRow = i;
                            numberCol = j;
                            j = x.length - 1;
                            i = vertical - 1;
                            break loop;
                        }
                    }
                }
                if (numberRow !== -1 && numberCol !== -1) {
                    ProcessImageRipple(ctx5, x[numberCol], y[numberRow], len);
                    DrawRectM(ctx5, x[numberCol], y[numberRow], len);
                    $("#selectModal").modal(
                        {
                            keyboard: false
                        });
                }

            }
        // }
    } ) ;

    //ダブルタップios用
    // $("canvas5").data("dblTap", false).click(function () {
    //     console.log("canvas5.ios用ダブルタップ");
    //     if($(this).data("dblTap")){
    //         //ダブルタップ時の命令
    //         // console.log("ダブルタップ");
    //         //ポインタがエリア内にあるか        
    //         const rectM = canvas5.getBoundingClientRect();
    //         clickX = e.clientX - rectM.left;
    //         clickY = e.clientY - rectM.top;
    //         numberCol = -1;//j
    //         numberRow = -1;//i
    //         //ポインタがメモリア内にあるか判定
    //         loop:for (let i = 0; i < vertical; i++){
    //             for (let j = 0; j < x.length; j++) {
    //                 if ((clickX < x[j] + len / 2) && (x[j] - len / 2 < clickX) && (clickY < y[i] + len / 2) && (y[i] - len / 2 < clickY)) {
    //                     numberRow = i;
    //                     numberCol = j;
    //                     j = x.length - 1;
    //                     i = vertical - 1;
    //                     break loop;
    //                 }
    //             }
    //         }
    //         if (numberRow !== -1 && numberCol !== -1) {
    //             ProcessImageRipple(ctx5, x[numberCol], y[numberRow], len);
    //             DrawRectM(ctx5,x[numberCol], y[numberRow], len);
    //             $("#selectModal").modal(
    //                 {
    //                     keyboard: false
    //                 });
    //         }
    //         $(this).data("dblTap",false);
    //     }else{
    //         $(this).data("dblTap",true);
    //     }
    //     setTimeout(function(){
    //         $("p").data("dblTap",false);
    //     },500);
    // })


    // モーダルが開いた時の処理
    $('#selectModal').on('shown.bs.modal', function (e) {
        // var modal = e;
        // centeringModalSyncer();
        DrawM();

        if (typeof salidaAntes === "undefined")
        salidaAntes = new Array(3);
        IndicaModalSalida(salida[0], salidaAntes[0], "grid1m");
        if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "0") {
            IndicaModalSalida(salida[1], salidaAntes[1], "grid2m");
            IndicaModalSalida(salida[2], salidaAntes[2], "grid3m");
        }
    });
    //モーダルが閉じた時の処理
    var key1, key2;
    $('#selectModal').on('hidden.bs.modal', function (e) {
        // handler.removeListener(key2);
        var canvasM = document.getElementById("canvasM");
        canvasM.removeEventListener("dblclick", Modaldblclick);
        canvasM.removeEventListener("pointerdown", Modalpointerdown);

        //1人で攻撃する場合 canvasのコピー
        if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "1") {
            console.log("CopyCharaMemoria実行");
            CopyCharaMemoria();
        }
        
        //canvas5の描画エフェクト解除
        ctx5.clearRect(x[numberCol] - len / 2 -5, y[numberRow] - len / 2 -5 , len + 20, len + 20);
        DrawMemoria(ctx5, x[numberCol], y[numberRow], len, imageAryM[numberRow * horizontal + numberCol], 1);
        let ordena = $("input[name='ctl00$MainContent$ordenMemoria']:checked").val();
        let number = numberRow * horizontal + numberCol;
        let text;
        if (ordena !== "") {
            switch (ordena) {
                case "ATK":
                    {
                        text = mData.tarjetas[number].ATK !== null ? mData.tarjetas[number].ATK : 0;
                        break;
                    }
                case "DEF":
                    {
                        text = mData.tarjetas[number].DEF !== null ? mData.tarjetas[number].DEF : 0;
                        break;
                    }
                case "HP":
                    {
                        text = mData.tarjetas[number].HP !== null ? mData.tarjetas[number].HP : 0;
                        break;
                    }
                case "ミラーズ発動T":
                    {
                        if ((totu === "1") || (totu === "0") && (mData.tarjetas[number].ascend === "max"))
                            text = mData.tarjetas[number].turn.valueMax !== null ? MirrorsTurn(mData.tarjetas[number].turn.valueMax) : null;
                        else
                            text = mData.tarjetas[number].turn.value !== null ? MirrorsTurn(mData.tarjetas[number].turn.value) : null;
                        break;
                    }
                case "効果値":
                    {
                        text = mData.tarjetas[number].sortValue;
                        break;
                    }
            }
            if ((text !== "") && (typeof text !== "undefined") && (text !== null)) {
                DrawImageTextM(ctx5, x[numberCol], y[numberRow], len, text);
            }
        }
        IndicaResultado();
    });


    // var xM = [20, 60, 100, 140]; 変数のグローバル化
    // var yM = [120, 120, 120];　変数のグローバル化
    var margin = 5;//canvas内の余白
    var textModal = ["アビ1", "アビ2", "スキル1", "スキル2"];
    var colorModal = ["blue", "blue", "red", "red"];

    function DrawM(){
        var canvasM;
        canvasM = document.getElementById("canvasM");
        if (!canvasM || !canvasM.getContext) {
            return;
        }
        //設定
        var ctxM = canvasM.getContext("2d");
        
        //他のcanvas表示をコピー
        var canvas61;
        canvas61 = document.getElementById("canvas61");
        if (!canvas61 || !canvas61.getContext) {
            return;
        }
        var ctx61 = canvas61.getContext("2d");
        var canvas61;
        canvas62 = document.getElementById("canvas62");
        if (!canvas62 || !canvas62.getContext) {
            return;
        }
        var ctx62 = canvas62.getContext("2d");
        var canvas63;
        canvas63 = document.getElementById("canvas63");
        if (!canvas63 || !canvas63.getContext) {
            return;
        }
        var ctx63 = canvas63.getContext("2d");
        //初期描画
        ctxM.clearRect(0, 0, canvasM.width, canvasM.height);
        ctxM.drawImage(canvas61, 0, 0, 160, 120, 5, 30, 160, 120);
        if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "0") {
            ctxM.drawImage(canvas62, 0, 0, 160, 120, 175, 30, 160, 120);
            ctxM.drawImage(canvas63, 0, 0, 160, 120, 345, 30, 160, 120);
        }
        
        // //スクロールバー表示
        // if($(window).width() < 768)
        //     $(".scroll-area").mCustomScrollbar();

        var xOffset = 0;
        loop:for (let i = 0; i < 3; i++){
            if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "1") {
                if (i !== 0)
                    break loop;
            }
            for (let j = 0; j < 4; j++){
                DrawMtext(ctxM, xM[j] + xOffset + margin, yM[i], textModal[j]);
                DrawWaku(ctxM, xM[j] + xOffset + margin, yM[i], colorModal[j]);
            }
            xOffset += 170;
        }

        //文字や枠等
        
        if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "0") {
            DrawText(ctxM, 80, 15, "1回目の攻撃", 0);
            DrawText(ctxM, 250, 15, "2回目の攻撃", 0);
            DrawText(ctxM, 420, 15, "3回目の攻撃", 0);
        }
        else {
            DrawText(ctxM, 80, 15, "全ての攻撃", 0);
        }
        ctxM.beginPath();
        ctxM.moveTo(170, 0);
        ctxM.lineTo(170, 150);
        ctxM.stroke();

        ctxM.beginPath();
        ctxM.moveTo(340, 0);
        ctxM.lineTo(340, 150);
        ctxM.stroke();

        //メモリア消去のチェックはoff
        $("#MainContent_quitaM").prop("checked", false);


        var region = { x: 0, y: 0, w: canvasM.width, h: canvasM.height };

        if (deviceType === 2)
            var t1 = new ToolTipModal(canvasM, region, 220,xM,yM);
        //
        // canvasM.addEventListener("pointerdown", function (e) {
        canvasM.addEventListener("pointerdown", Modalpointerdown);


        

        // key2 = handler.addListener(canvasM, "dblclick", (function () {
            // return function (e) {
        canvasM.addEventListener("dblclick", Modaldblclick);

        // let tapCount = 0 ;
        canvasM.addEventListener("touchstart", ModalTouchStart);

        //iosでのダブルタップ時の拡大防止 キャプチャリングフェーズ版
        let flag = false;
        canvasM.addEventListener("touchend", function (event) {
            if (flag) {
                event.preventDefault();
            } else {
                flag=true;
                setTimeout(function () {
                    flag = false; 
                },500);
            }
        },true);
        
        // canvasM.addEventListener("dblclick", function (e) {
                    
            
                // });
        //     }
        // })(e), false);
    }//drawMおしまい

    function DrawMtext(ctx,x,y,text) {
        ctx.save();
        ctx.font = "10px sans-serif";
        ctx.fillStyle = "black";
        ctx.textAlign = "center";
        ctx.textBaseline = "middle";
        ctx.fillText(text,x,y);
        ctx.restore();
    }
    function DrawWaku(ctx, x, y, color) {
        ctx.beginPath();
        ctx.save();
        ctx.strokeStyle = color === "blue" ? "blue" : "red";
        ctx.strokeRect(x - 18, y - 18, 36, 36);
        ctx.restore();
    }
    //メモリア描画用
    function DrawModalMemoria(ctx, i, j, data,xOffset) {
        //Modal画面に描画
        DrawImage(ctx, xM[j] + xOffset + margin, yM[i], 18, data);
        
        //下のキャラセレクト画面へ描画
        DrawCharaMemoria(i, j, data);
        
    }

    

    

    function Modalpointerdown(e){
        //ポインタがcanvas内にあるか判定
        var canvasM = document.getElementById("canvasM");
        if (!canvasM || !canvasM.getContext) {
            return;
        }
        var ctxM = canvasM.getContext("2d");
        const rectM = canvasM.getBoundingClientRect();
        clickX = e.clientX - rectM.left;
        clickY = e.clientY - rectM.top;
        if (!((clickX < canvasM.width) && (clickY < canvasM.height))) {
            return;
        }

        //座標判定
        //メモリアエリアにあるか
        let xOffset = 170;
        var memoriaX = [xM[0] - 16 + margin, xM[3] + 16 + margin + xOffset * 2];
        var memoriaY = [yM[0] - 16, yM[2] + 16];
        xOffset = 0;
        if (!(clickX > memoriaX[0] && clickX < memoriaX[1]))
            return;
        if (!(clickY > memoriaY[0] && clickY < memoriaY[1]))
            return;
        loop: for (let i = 0; i < 3; i++) {
            if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "1") {
                if (i !== 0)
                    break loop;
            }
            for (let j = 0; j < 4; j++) {
                if (clickX > xM[j] - 16 + xOffset && clickX < xM[j] + 16 + xOffset) {
                    if (clickY > yM[i] - 16 && clickY < yM[i] + 16) {
                        //メモリア位置確定
                        //メモリア消去モードか確認
                        if ($("#MainContent_quitaM").prop("checked")) {
                            //消去処理
                            delete memoriaElegido[i][j];
                            
                            ctxM.clearRect(xM[j] + xOffset + margin - 18, yM[i] - 18, 36, 36);
                            DrawMtext(ctxM, xM[j] + xOffset + margin, yM[i], textModal[j]);
                            DrawWaku(ctxM, xM[j] + xOffset + margin, yM[i], colorModal[j]);

                            ClearCharaMemoria(i, j);
                            break loop;

                        }
                        else {
                            //メモリアタイプによって場合分け
                            if (typeof mData.tarjetas[numberRow * x.length + numberCol] !== "undefined") {
                                var type = mData.tarjetas[numberRow * x.length + numberCol].type;
                                switch (j) {
                                    case 0:
                                    case 1:
                                        {
                                            if (type === "アビリティ") {
                                                //OK
                                                DrawModalMemoria(ctxM, i, j, imageAryM[numberRow * x.length + numberCol], xOffset);
                                                let antes = memoriaElegido[i][j];
                                                //前と異なるメモリアか、antesがない場合
                                                if (typeof antes === "undefined" || antes.name !== mData.tarjetas[numberRow * x.length + numberCol].name) {
                                                    memoriaElegido[i][j] = angular.copy(mData.tarjetas[numberRow * x.length + numberCol]);
                                                    memoriaElegido[i][j].totu = true;
                                                }
                                                let on = memoriaElegido[i][j].totu === true ? "on" : "off";
                                                DrawTotu(ctxM, xM[j] + xOffset + margin, yM[i], 36, imageAryM[numberRow * x.length + numberCol], on);
                                                //下の凸画像
                                                DrawTotuC(i, xM[j], yM[i] - 30, 36, imageAryM[numberRow * x.length + numberCol], on);
                                            }
                                            //ダメ
                                            break;
                                        }
                                    case 2:
                                    case 3:
                                        {
                                            if (type === "スキル") {
                                                //OK
                                                DrawModalMemoria(ctxM, i, j, imageAryM[numberRow * x.length + numberCol], xOffset);
                                                let antes = memoriaElegido[i][j];
                                                if (typeof antes === "undefined" || antes.name !== mData.tarjetas[numberRow * x.length + numberCol].name) {
                                                    memoriaElegido[i][j] = angular.copy(mData.tarjetas[numberRow * x.length + numberCol]);
                                                    memoriaElegido[i][j].totu = true;
                                                }
                                                let on = memoriaElegido[i][j].totu === true ? "on" : "off";
                                                DrawTotu(ctxM, xM[j] + xOffset + margin, yM[i], 36, imageAryM[numberRow * x.length + numberCol], on);
                                                //下の凸画像
                                                DrawTotuC(i, xM[j], yM[i] - 30, 36, imageAryM[numberRow * x.length + numberCol], on);
                            
                                            }
                                            //ダメ
                                            break;
                                        }
                                }
                        
                                break loop;
                            }
                        }
                    }
                }
            }
            xOffset += 170;
        }
        GetMemoria();
        GetAtk();
        IndicaResultado();
        console.log("pointerDown終了" +  tapCountM );
    }//Modalのpointerdown終わり
    
    function Modaldblclick(e) {
        //場所の把握
        //ポインタがcanvas内にあるか判定
        var canvasM = document.getElementById("canvasM");
        if (!canvasM || !canvasM.getContext) {
            return;
        }
        var ctxM = canvasM.getContext("2d");
        const rectM = canvasM.getBoundingClientRect();
        clickX = e.clientX - rectM.left;
        clickY = e.clientY - rectM.top;
        if (!((clickX < canvasM.width) && (clickY < canvasM.height))) {
            return;
        }

        //座標判定
        //メモリアエリアにあるか
        let xOffset = 170;
        var memoriaX = [xM[0] - 16 + margin, xM[3] + 16 + margin + xOffset * 2];
        var memoriaY = [yM[0] - 16, yM[2] + 16];
        xOffset = 0;
        if (!(clickX > memoriaX[0] && clickX < memoriaX[1]))
            return;
        if (!(clickY > memoriaY[0] && clickY < memoriaY[1]))
            return;
        loop: for (let i = 0; i < 3; i++) {
            if ($("input[name='ctl00$MainContent$estadoAtk']:checked").val() === "1") {
                if (i !== 0)
                    break loop;
            }
            for (let j = 0; j < 4; j++) {
                if (clickX > xM[j] - 16 + xOffset && clickX < xM[j] + 16 + xOffset) {
                    if (clickY > yM[i] - 16 && clickY < yM[i] + 16) {
                        //すでにメモリアがセットされているか
                        if (typeof memoriaElegido[i][j] !== "undefined") {
                            //totuフラグ確認
                            let on = memoriaElegido[i][j].totu === true ? "off" : "on";
                            let img = new Image();
                            img.src = "png/m/" + memoriaElegido[i][j].data;
                            //凸画像
                            DrawTotu(ctxM, xM[j] + xOffset + margin, yM[i], 36, img, on);
                            //下の凸画像
                            DrawTotuC(i, xM[j], yM[i] - 30, 36, img, on);
                        
                            //凸フラグ変更
                            memoriaElegido[i][j].totu = on === "off" ? false : true;
                        }
                        break loop;
                    }
                }
            }
            xOffset += 170;
        }
        GetMemoria();
        GetAtk();
        IndicaResultado();
    }
    let tapCountM = 0;

    function ModalTouchStart(e) {
        // シングルタップ判定
        if( !tapCountM ) {
            ++tapCountM;

            setTimeout( function() {
                tapCountM = 0;
            }, 450 );

        // ダブルタップ判定
        } else {
            e.preventDefault();
            tapCountM = 0;
            console.log("modalダブルタップ");
            Modaldblclick(e.touches[0]);
        }
    }

    //ポインタ乗ったらドラッグ処理
    canvas5.addEventListener('pointermove', function (e) {
        if (!pointerFlagM) {
            return_scroll();
            console.log(pointerFlagM + " move");
            return;
        }
        no_scroll();
        //オフセット位置取得
        const rectM = canvas5.getBoundingClientRect();
        var pxM = e.clientX - rectM.left - clickX;
        var pyM = e.clientY - rectM.top - clickY;
        clickX = e.clientX - rectM.left;
        clickY = e.clientY - rectM.top;

        //canvas外は受け付けない
        if((clickX > canvas5.width)||(clickX < 0)||(clickY > canvas5.height)||(clickY < 0)){
            pointerFlagM = false;
            console.log(pointerFlagM + " limit");
            if(canvasFlagM===1)
                return_scroll();
            return;
        }

        //端の移動制限
        //上端
        if ((pyM > 0) && (y[0] >= y0)) {//下にドラッグした時
            if (clickY > canvas5.height) {//ドラッグした先が下端を超えた場合
                pointerFlagM = false;
            }
            console.log(pointerFlagM + " 上端");
            return;
        }
        //下端
        if ((pyM < 0) && (y[vertical-1] <= canvas5.height - y0)) {//上にドラッグした時
            if (clickY < 0) {//ドラッグした先が上端を超えた場合
                pointerFlagM = false;
            }
            console.log(pointerFlagM + " 下端");
            return;
        }
        
       //margin部分に乗ったらflagをoff
        if ((clickX > canvas5.width - marginX) || (clickX < marginX)|| (clickY < marginY))
            pointerFlagM = false;
        
        
        // console.log("px " + px);
        //pxが少ない場合は色変え
        if (Math.abs(pyM) > 8)
            isStaticM = 0;
        

        console.log(pointerFlagM + "移動前");
        //描画メイン処理
        if (pointerFlagM) {
            console.log("描画中 " + pointerFlagM);
            ctx5.clearRect(0, 0, canvas5.width, canvas5.height);
            //描画座標判定
            // let iMin = 0;
            // let iMax = vertical;
            // for (let i = 0; i < vertical; i++){
            //     if (y[i] < 0 - len)
            //         iMin = i;//最後のiを代入
            //     if (y[i] > canvas5.height + len) {
            //         iMax = i;
            //         break;
            //     }
            // }
            // console.log(iMin + "<" + iMax);
            for(let i = 0; i<vertical;i++){
                y[i] += pyM;
                for (let j = 0; j < x.length; j++) {
                    //数確認
                    if ((i * horizontal + j) >= mData.tarjetas.length) {
                        continue;
                    }
                    if (elegidaRow === i && elegidaCol === j) {//色変え
                        DrawMemoria(ctx5,x[j],y[i],len,imageAryM[i*horizontal+j],1);
                        // DrawText(ctx5, x[j], y[i], letra[i * x.length + j], -1);
                    }
                    else {//通常色
                        DrawMemoria(ctx5,x[j],y[i],len,imageAryM[i*horizontal+j],1);
                        // DrawText(ctx5, x[j], y[i], letra[i * x.length + j], 1);
                    }
                }
            }
            //ソートが入っている場合は値を表示
            let order = $("input[name='ctl00$MainContent$ordenMemoria']:checked").val();
            switch (order) {
                case "効果値":
                    {
                        order = "sortValue";
                        break;
                    }
                case "ミラーズ発動T":
                    {
                        order = "turn";
                        break;
                    }
            }
            RedrawM(order,1);
        }
        console.log("移動量" + pyM);
    });

    //ポインタ離れたらドラッグ終了
    canvas5.addEventListener("pointerup", function (e) {
        pointerFlagM = false;
        // console.log(pointerFlag + " UP");

        if(isStaticM === 1 && numberRow !== -1 ){
            // console.log("elegida " + elegida + " number" + number);
           if(elegidaRow === numberRow && elegidaCol === numberCol){
                //同色の場合は色を初期に戻す
                //同じ番号の場合
                //色を戻す処理
                        elegidaRow = -1;
                        elegidaCol = -1;
                        for (let i = 0; i < vertical; i++) {
                            for (let j = 0; j < x.length; j++) {
                                //数確認
                                if ((i * horizontal + j) >= mData.tarjetas.length) {
                                    continue;
                                }
                                DrawMemoria(ctx5,x[j],y[i],len,imageAryM[i*horizontal+j],1);
                                    // DrawText(ctx5, x[j], y[i], letra[i * x.length + j], 1);
                                }
                        }
            }
            else//他番号の場合
                {
                    for (let i = 0; i < vertical; i++){
                        for (let j = 0; j < x.length; j++){
                            //数確認
                            if ((i * horizontal + j) >= mData.tarjetas.length) {
                                continue;
                            }
                            if (numberRow === i && numberCol === j) {
                                //色変え
                                DrawMemoria(ctx5,x[j],y[i],len,imageAryM[i*horizontal+j],1);
                                // DrawText(ctx5, x[j], y[i], letra[i*x.length+j], -1);
                            }
                            else {
                                //通常色
                                DrawMemoria(ctx5,x[j],y[i],len,imageAryM[i*horizontal+j],1);
                                // DrawText(ctx5, x[j], y[i], letra[i*x.length+j], 1);
                            }
                        }
                    }
                    elegidaRow = numberRow;
                    elegidaCol = numberCol;
            }
            //ソートが入っている場合は値を表示
            let order = $("input[name='ctl00$MainContent$ordenMemoria']:checked").val();
            switch (order) {
                case "効果値":
                    {
                        order = "sortValue";
                        break;
                    }
                case "ミラーズ発動T":
                    {
                        order = "turn";
                        break;
                    }
            }
            RedrawM(order,1);
        }
        return_scroll();
    });

    canvas5.addEventListener('pointercancel',function(e){
        pointerFlagM = false;
        // console.log(pointerFlag + " cancel");
    });

    canvas5.addEventListener("pointerout", function() {
        pointerFlagM = false;
    });

    canvas5.addEventListener("focus", function() {
        pointerFlagM = false;
    });

    function FilterMemoria() {
        //選択項目チェック
        var tipo = $("input[name='ctl00$MainContent$tipoMemoria']:checked").val();
        var rarity = [2];
        var index = 0;
        $('input[name*="ctl00$MainContent$rarityCheckM"]:checked').each(function () {
            //値を取得
            rarity[index] = Number($(this).val());
            index++;
        });
        var orden = $("input[name='ctl00$MainContent$ordenMemoria']:checked").val();
        var memoria = [2];
        memoria[0] = $("#MainContent_memoria1").val();
        memoria[1] = $("#MainContent_memoria2").val();
        var disk = $("#MainContent_diskM").val();
        var totu = $("input[name='ctl00$MainContent$totuM']:checked").val();

        mData = angular.copy(mDataOri);
        mData.tarjetas = mData.tarjetas.filter(function (value, index, array) {
            if (tipo !== "全て") {
                if (value.type !== tipo)
                    return false;
            }
            let flagR = 0;
            if (rarity[0] === value.rarity)
                flagR++;
            if (rarity[1] === value.rarity)
                flagR++;
            if(flagR===0)
                return false;
            if (orden === "ミラーズ発動T") {
                if (value.type !== "スキル")
                    return false;
            }
            //効果値でのフィルタ
            for (let i = 0; i < memoria.length; i++){
                if (memoria[i].indexOf("効果") === -1) {
                    let sortFlag = (($("input[name='ctl00$MainContent$ordenMemoria']:checked").val() === "効果値")&& i===0) ? 1 : 0;
                    switch (memoria[i]) {
                        default:
                            {
                                if (BusquedaMemoria(memoria[i], value, 0)) {
                                    
                                    if (sortFlag === 1) {
                                        if((totu==="1")||(totu==="0" && value.ascend==="max")){
                                            resultado = BusquedaMemoria(memoria[i], value, 2);
                                        }
                                        else {
                                            resultado = BusquedaMemoria(memoria[i], value, 1);
                                        }
                                        value.sortValue = resultado[1];
                                    }
                                }
                                else
                                    return false;
                                break;
                            }
                    }
                }
            }
            //ディスク
            if (disk !== "ディスク系") {
                switch (disk) {
                    case "Aドロー": {
                        if (!BusquedaMemoria("Acceleドロー",value, 0))
                            return false;
                        break;
                    }
                    case "Bドロー": {
                        if (!BusquedaMemoria("Blastドロー",value, 0))
                            return false;
                        break;
                    }
                    case "Cドロー": {
                        if (!BusquedaMemoria("Chargeドロー",value, 0))
                            return false;
                        break;
                    }
                    case "自分のDisc": {
                        if (!BusquedaMemoria("自分のDiscドロー",value, 0))
                            return false;
                        break;
                    }
                    case "再度引く": {
                        if (!BusquedaMemoria("再度Discを引く",value, 0))
                            return false;
                        break;
                    }
                    case "同じ属性": {
                        if (!BusquedaMemoria("同じ属性ドロー",value, 0))
                            return false;
                        break;
                    }
                }
            }
            
            return true;
        });

        var orden = OrdenaMemoria();
        y = angular.copy(yOri);
        RedrawM(orden);
    }

    function OrdenaMemoria() {
        var tipoOrden = $("input[name='ctl00$MainContent$ordenMemoria']:checked").val();
        var totu = $("input[name='ctl00$MainContent$totuM']:checked").val();
        function compareFuncATK(a, b) {
            return b.ATK - a.ATK;
        }
        function compareFuncDEF(a, b) {
            return b.DEF - a.DEF;
        }
        function compareFuncHP(a, b) {
            return b.HP - a.HP;
        }
        switch (tipoOrden) {
            case "ATK":
                {
                    mData.tarjetas.sort(compareFuncATK);
                    break;
                }
            case "DEF":
                {
                    mData.tarjetas.sort(compareFuncDEF);
                    break;
                }
            case "HP":
                {
                    mData.tarjetas.sort(compareFuncHP);
                    break;
                }
            case "50音":
                {
                    mData.tarjetas.sort(function (a, b) {
                        if (a.nameHiragana > b.nameHiragana) 
                            return 1;
                        if (a.nameHiragana < b.nameHiragana) 
                            return -1;
                        return 0;
                    });
                    break;
                }
            case "ミラーズ発動T":
                {
                    mData.tarjetas.sort(function (a, b) {
                        var an, bn;
                        if ((totu === "1") || ((totu === "0") && (a.ascend === "max")))
                            an = a.turn.valueMax;
                        else
                            an = a.turn.value;
                        if ((totu === "1") || ((totu === "0") && (b.ascend === "max")))
                            bn = b.turn.valueMax;
                        else   
                            bn = b.turn.value;
                        an = MirrorsTurn(an);
                        bn = MirrorsTurn(bn);

                        if (an > bn) 
                            return 1;
                        if (an < bn) 
                            return -1;
                        if (a.nameHiragana > b.nameHiragana)
                            return 1;
                        if (a.nameHiragana < b.nameHiragana) 
                            return -1;
                        return 0;
                    });
                    break;
                }
            case "効果値":
                {
                    //通常ローマ数字ソート
                    mData.tarjetas.sort(function (a, b) {
                        let an = Number(ChangeRoman(a.sortValue));
                        let bn = Number(ChangeRoman(b.sortValue));
                        if (an < bn) {
                            return 1;
                        }
                        if (an > bn) {
                            return -1;
                        }
                    });
                    break;
                }
        }

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
            case "ミラーズ発動T":{
                returnValue = "turn";
                break;
            }
            case "効果値": {
                returnValue = "sortValue";
                break;
                }
        }
        return returnValue;
        
    }

    //modo "";通常
    //modo 1;ソートテキストのみ表示する
    function RedrawM(ordena, modo) {
        if (typeof modo === "undefined") {
            imageAryM.length = 0;
            for (let i = 0; i < mData.tarjetas.length; i++) {
                imageAryM[i] = new Image();
                imageAryM[i].src = "png/m/" + mData.tarjetas[i].data;
            }
        }

        var totu = $("input[name='ctl00$MainContent$totuM']:checked").val();
                                   
        //vertical再計算
        vertical = Math.ceil(mData.tarjetas.length / x.length);
        
        //描画
        if(typeof modo === "undefined")
            ctx5.clearRect(0, 0, canvas5.width, canvas5.height);
        for(let i = 0; i<vertical;i++){
            for (let j = 0; j < x.length; j++) {
                //カウント確認
                if ((i * horizontal + j) >= mData.tarjetas.length)
                    break;
                if (mData.tarjetas[i * horizontal + j].data !== "") {
                    if ((i * horizontal + j) >= memorias) {
                        continue;
                    }
                    if(typeof modo === "undefined")
                    DrawMemoria(ctx5, x[j], y[i], len, imageAryM[i * horizontal + j], 1);
                        // DrawText(ctx5, x[j], y[i], letra[i * x.length + j], 1);
                    let text;
                    if (ordena !== "") {
                        switch (ordena) {
                            case "ATK":
                                {
                                    text =  mData.tarjetas[i * horizontal + j].ATK !==null ? mData.tarjetas[i * horizontal + j].ATK:0;
                                    break;
                                }
                            case "DEF":
                                {
                                    text =  mData.tarjetas[i * horizontal + j].DEF !==null ? mData.tarjetas[i * horizontal + j].DEF:0;
                                    break;
                                }
                            case "HP":
                                {
                                    text =  mData.tarjetas[i * horizontal + j].HP !==null ? mData.tarjetas[i * horizontal + j].HP:0;
                                    break;
                                }
                            case "turn":
                                {
                                     if((totu==="1")||(totu === "0") && (mData.tarjetas[i * horizontal + j].ascend === "max"))
                                        text = mData.tarjetas[i * horizontal + j].turn.valueMax !==null ? MirrorsTurn(mData.tarjetas[i * horizontal + j].turn.valueMax):null;
                                    else
                                        text = mData.tarjetas[i * horizontal + j].turn.value !==null ? MirrorsTurn(mData.tarjetas[i * horizontal + j].turn.value):null;
                                    break;
                                }
                            case "sortValue":
                                {
                                    text = mData.tarjetas[i * horizontal + j].sortValue;
                                    break;
                                }
                        }
                        if ((text !== "") && (typeof text !== "undefined") && (text !== null)) {
                            DrawImageTextM(ctx5, x[j], y[i], len, text);
                        }
                    }
                }
            }
        }
        //メモリア数表示
        document.getElementById("MainContent_indicaMemoria").innerText = "登録 " + mDataOri.tarjetas.length + "メモリア中" + mData.tarjetas.length + "枚表示";

        // if(canvasFlagM===0)//モバイル版は今んとこツールチップ出せない。挙動が安定しない
        if(deviceType === 2)
            var t1 = new ToolTip(canvas5, region, 220,x,totu);

    }



    function MirrorsTurn(turn) {
        switch (turn) {
            case "15":
                {
                    turn = 8;
                    break;
                }
            case "13":
            case "12":
                {
                    turn = 7;
                    break;
                }
            case "10":
                {
                    turn = 6;
                    break;
                }
            case "9":
            case "8":
                {
                    turn = 5;
                    break;
                }
            case "7":
            case "6":
                {
                    turn = 4;
                    break;
                }
            case "5":
            case "4":
            case "3":
            case "2":
                {
                    turn = 3;
                    break;
                }
        }
        return turn;
    }

    $('input[name="ctl00$MainContent$tipoMemoria"]:radio').change(function () {
        FilterMemoria();
    });
    $('input[name="ctl00$MainContent$rarityCheckM$0"]').change(function () {
        FilterMemoria();
    });
    $('input[name="ctl00$MainContent$rarityCheckM$1"]').change(function () {
        FilterMemoria();
    });
    $('input[name="ctl00$MainContent$ordenMemoria"]:radio').change(function () {
        FilterMemoria();
        
        document.getElementById("MainContent_ordenMemoria_0").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_ordenMemoria_1").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_ordenMemoria_2").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_ordenMemoria_3").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_ordenMemoria_4").nextSibling.classList.remove("highlight");
        document.getElementById("MainContent_ordenMemoria_5").nextSibling.classList.remove("highlight");
        var textM = $("input[name='ctl00$MainContent$ordenMemoria']:checked").val();
        switch(textM){
            case "50音":
                {
                    document.getElementById("MainContent_ordenMemoria_0").nextSibling.classList.add("highlight");
                    break;
                }
            case "ATK":
                {
                    document.getElementById("MainContent_ordenMemoria_1").nextSibling.classList.add("highlight");
                    break;
                }
            case "DEF":
                {
                    document.getElementById("MainContent_ordenMemoria_2").nextSibling.classList.add("highlight");
                    break;
                }
            case "HP":
                {
                    document.getElementById("MainContent_ordenMemoria_3").nextSibling.classList.add("highlight");
                    break;
                }
            case "ミラーズターン":
                {
                    document.getElementById("MainContent_ordenMemoria_4").nextSibling.classList.add("highlight");
                    break;
                }
            case "効果値":
                {
                    document.getElementById("MainContent_ordenMemoria_5").nextSibling.classList.add("highlight");
                    break;
                }
        }

    });
    $('#MainContent_memoria1').change(function () {
        FilterMemoria();
        var ordenM = document.getElementById("MainContent_ordenMemoria_5");
        var textM = $('#MainContent_memoria1').val();
        if (textM.indexOf("効果") === -1) {
            ordenM.disabled = false;
            if ((textM !== "ここにテキスト入力") && $(window).width() > 768) {
                document.getElementById("MainContent_ordenMemoria_5").nextSibling.innerText = "効果値:" + textM;
            }
            else if ($(window).width() > 768)
                document.getElementById("MainContent_ordenMemoria_5").nextSibling.innerText = "効果値";
        }
        else {
            if ($("#MainContent_ordenMemoria_5").prop("checked") === true) {
                document.getElementById("MainContent_ordenMemoria_5").nextSibling.classList.remove("highlight");
                $("#MainContent_ordenMemoria_5").prop("checked", false);
            }
            ordenM.disabled = true;
            if ($(window).width() > 768)
                document.getElementById("MainContent_ordenMemoria_5").nextSibling.innerText = "効果値";
        }
    });
    $('#MainContent_memoria2').change(function () {
        FilterMemoria();
    });
    $('#MainContent_diskM').change(function () {
        FilterMemoria();
    });
    $('input[name="ctl00$MainContent$totuM"]:radio').change(function () {
        FilterMemoria();
    });

    //センタリングをする関数
    function centeringModalSyncer(){

        //画面(ウィンドウ)の幅を取得し、変数[w]に格納
        var w = $(window).width();

        //画面(ウィンドウ)の高さを取得し、変数[h]に格納
        var h = $(window).height();

        //コンテンツ(#modal-content)の幅を取得し、変数[cw]に格納
        var cw = $("#selectModal").outerWidth({margin:true});

        //コンテンツ(#modal-content)の高さを取得し、変数[ch]に格納
        var ch = $("#selectModal").outerHeight({margin:true});

        //コンテンツ(#modal-content)を真ん中に配置するのに、左端から何ピクセル離せばいいか？を計算して、変数[pxleft]に格納
        // var pxleft = ((w - cw)/2);
        var pxleft = 0;

        //コンテンツ(#modal-content)を真ん中に配置するのに、上部から何ピクセル離せばいいか？を計算して、変数[pxtop]に格納
        var pxtop = ((h - ch)/2);

        //[#modal-content]のCSSに[left]の値(pxleft)を設定
        $("#selectModal").css({"left": pxleft + "px"});

        //[#modal-content]のCSSに[top]の値(pxtop)を設定
        $("#selectModal").css({"top": pxtop + "px"});

    }
        
}//draw5終わり
    //color 
    //1 black それ以外 white
function DrawMemoria(ctx, x, y, l, data, color) {
    DrawImageM(ctx, x, y, l, data);
}

function DrawImageM(ctx, x, y, l, data) {
    var image = new Image();
    image = data;
    // console.log(data);
    if (image.complete) {
        ctx.drawImage(image, x - l/2, y - l/2, l, l);
    }
    else {
        image.addEventListener('load', function () {
            ctx.save();
            ctx.globalCompositeOperation = "destination-over";
            ctx.drawImage(image, x - l/2, y - l/2, l, l);
            ctx.restore();
        }, false);
    }
}

function DrawRectM(ctx, x, y, l) {
    ctx.save();
    ctx.shadowColor = "red";
    ctx.shadowOffsetX = 5;
    ctx.shadowOffsetY = 5;
    ctx.shadowBlur = 10;
    ctx.globalCompositeOperation = "destination-over";
    ctx.fillRect(x - l / 2, y - l / 2, l, l);
    ctx.restore();
}

function DrawImageTextM(ctx, x, y, l, text) {
//     ProcessImage(ctx, 10, 10 + 5,5,5);
    ctx.save();
    ctx.font = "900 12px sans-serif";

    var textW = Math.ceil(ctx.measureText(text).width) + 5;
    var textH = 15;
    ProcessImage(ctx, x, y + l/3,textW,textH);

    ctx.strokeStyle = "yellow";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.strokeText(text, x, y + l  / 3);
    ctx.restore();
    ctx.save();
    ctx.font = "600 12px sans-serif";
    ctx.fillStyle = "black";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(text, x, y + l  / 3);
    ctx.restore();

}

//文字入力部分の画像を明るくする
function ProcessImage(ctx, x, y,w,h) {
    var src = ctx.getImageData(x - w / 2, y - h / 2, w, h);
    var img = ctx.createImageData(w, h);
    var percent = 250;
    var offset = 100;
    for (let i = 0; i < w * h * 4; i += 4){
        //輝度調整
        var r = Math.floor(src.data[i] + offset);//R
        var g = Math.floor(src.data[i+1] + offset);//G
        var b = Math.floor(src.data[i+2] + offset);//B

        r = r === offset ? 245 : r;
        g = g === offset ? 245 : g;
        b = b === offset ? 245 : b;

        img.data[i] = Math.min(245,r);
        img.data[i+1] = Math.min(245,g);
        img.data[i+2] = Math.min(245,b);
        img.data[i+3] = 0xff;
    }
    ctx.putImageData(img, x - w / 2, y - h / 2);
}

//波紋　サンライトイエロー！
function ProcessImageRipple(ctx, xOri, yOri,length) {
    var src = ctx.getImageData(xOri - length / 2, yOri - length / 2, length, length);
    var img = ctx.createImageData(length, length);
    var angle = 5;
    var nWaves = 100;
    var angleRadian = angle * Math.PI * 2 / 360;
    var maxDistance = Math.sqrt(length * length + length * length);
    var scale = (Math.PI * 2.0 * nWaves) / maxDistance;

 
    for (var y = 0; y < length; y++) {
        for (var x = 0; x < length; x++) {
            var cx = x - length/2;
            var cy = y - length/2;
            var a = Math.sin(Math.sqrt(cx * cx + cy * cy) * scale) * angleRadian;
            var ca = Math.cos(a);
            var sa = Math.sin(a);
            var xs = Math.floor((cx * ca - cy * sa) + length/2);
            var ys = Math.floor((cy * ca + cx * sa) + length/2);
            if (xs >= 0 && xs < length && ys >= 0 && ys < length) {
                var i = (y * length + x) * 4;
                var j = (ys * length + xs) * 4;
                img.data[i + 0] = src.data[j + 0];// R 
                img.data[i + 1] = src.data[j + 1];// G 
                img.data[i + 2] = src.data[j + 2];// B 
                img.data[i + 3] = 0xff;
            }
        }
    }

    ctx.putImageData(img, xOri - length / 2, yOri - length / 2);
}

//メモリア限凸エフェクト
function DrawTotu(ctx,x,y,len,data,on) {
    //onの時は描画
    if (on === "on") {
        for (let i = 0; i < 4; i++) {
            let cx = x - (len/2) / 4 * 3 + (i * len / 4);
            let cy = y + (len/2) / 4 * 3;
            let l = 4;
            ctx.save();
            ctx.beginPath();
            ctx.moveTo(cx - l, cy);
            ctx.lineTo(cx, cy - l);
            ctx.lineTo(cx + l, cy);
            ctx.lineTo(cx, cy + l);
            ctx.closePath();
            let linearGrad = ctx.createLinearGradient(cx - l, cy - l, cx + l, cy + l);
            linearGrad.addColorStop(0, "white");
            linearGrad.addColorStop(0.5, "orange");
            linearGrad.addColorStop(1, "red");
            ctx.fillStyle = linearGrad;
            ctx.fill();
            ctx.restore();
        }
    }
    else {//off clearして元画像描画
        ctx.clearRect(x - len/2, y - len/2, len, len);
        DrawImageM(ctx, x, y, len,data);
    }
}

function DrawTotuC(i, x, y, len, data, on) {
    var canvas;
    switch (i)
    {
        case 0:
            {
                canvas = document.getElementById("canvas61");
                break;
            }
        case 1:
            {
                canvas = document.getElementById("canvas62");
                break;
            }
        case 2:
            {
                canvas = document.getElementById("canvas63");
                break;
            }
    }

    if (!canvas || !canvas.getContext) {
        return;
    }

    //設定
    var ctx = canvas.getContext("2d");
    DrawTotu(ctx, x, y, len, data, on);
}


function DrawText(ctx,x,y,letra,color){
    ctx.save();
    ctx.font = "16px sans-serif";
    ctx.fillStyle = color === 1 ? "white" : "black";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(letra,x,y);
    ctx.restore();
}

function DrawRect(ctx,x,y,len,color){
    ctx.beginPath();
    ctx.save();
    ctx.fillStyle = color ===1 ? "black" : "white";
    ctx.strokeStyle = "black";
    ctx.fillRect(x - len /2,y - len/2,len, len);
    ctx.stroke();
    ctx.restore();
}
//キャラ画面に描画
function DrawCharaMemoria(i, j, data) {
    var canvas;
    switch(i){
        case 0:
            {
                canvas = document.getElementById("canvas61");
                break;
            }
        case 1:
            {
                canvas = document.getElementById("canvas62");
                break;
            }
        case 2:
            {
                canvas = document.getElementById("canvas63");
                break;
            }
    }
    var ctx = canvas.getContext("2d");
    DrawImage(ctx, xM[j], yM[i]-30, 18, data);
}

//キャラ画面のメモリア表示クリア
function ClearCharaMemoria(i, j) {
    var canvas;
    switch(i){
        case 0:
            {
                canvas = document.getElementById("canvas61");
                break;
            }
        case 1:
            {
                canvas = document.getElementById("canvas62");
                break;
            }
        case 2:
            {
                canvas = document.getElementById("canvas63");
                break;
            }
    }
    var ctx = canvas.getContext("2d");
    ctx.clearRect(xM[j]-18, yM[i]-30-18, 36, 36);
}

function CopyCharaMemoria() {
    var canvas61 = document.getElementById("canvas61");
    var ctx61 = canvas61.getContext("2d");
    var canvas62 = document.getElementById("canvas62");
    var ctx62 = canvas62.getContext("2d");
    var canvas63 = document.getElementById("canvas63");
    var ctx63 = canvas63.getContext("2d");

    //canvas61をコピー
    ctx62.clearRect(60, 60, 100, 60);
    ctx62.drawImage(canvas61, 0, 60, 160, 120, 0, 60, 160, 120);
    console.log("ctx62 draw済み" + ctx62.canvas.width);
    ctx63.clearRect(60, 60, 100, 60);
    ctx63.drawImage(canvas61, 0, 60, 160, 120, 0, 60, 160, 120);
}

//キャラ画面2と3のメモリアの復活
function RegenerateCharaMemoria() {
    var canvas62 = document.getElementById("canvas62");
    var ctx62 = canvas62.getContext("2d");
    var canvas63 = document.getElementById("canvas63");
    var ctx63 = canvas63.getContext("2d");
    ctx62.clearRect(0, 60, 160, 60);
    ctx63.clearRect(0, 60, 160, 60);

    for (let i = 1; i < 3; i++){
        for (let j = 0; j < 4; j++){
            if (typeof memoriaElegido[i][j] !== "undefined") {
                let image = new Image();
                image.src = "png/m/" + memoriaElegido[i][j].data;
                DrawCharaMemoria(i, j, image);
            }
        }
    }

}

// The Tool-Tip instance:
function ToolTip(canvas, region, width,x,totu) {

    var me = this,                                // self-reference for event handlers
        div = document.createElement("div"),      // the tool-tip div
        parent = canvas.parentNode,               // parent node for canvas
        visible = false;                          // current status
    
    // set some initial styles, can be replaced by class-name etc.
    div.style.cssText = "position:fixed;padding:7px;background:gold;pointer-events:none;width:" + width + "px";
    
    var numberRow;
    var numberCol;

    // show the tool-tip
    this.show = function(pos) {
      if (!visible) {                             // ignore if already shown (or reset time)
        visible = true;                           // lock so it's only shown once
        setDivPos(pos);                           // set position
        parent.appendChild(div);                  // add to parent of canvas
        // setTimeout(hide, timeout);                // timeout for hide
      }
    }
    
    // hide the tool-tip
    function hide() {
      visible = false;                            // hide it after timeout
      parent.removeChild(div);                    // remove from DOM
    }
  
    // check mouse position, add limits as wanted... just for example:
    function check(e) {
      var pos = getPos(e),
          posAbs = {x: e.clientX, y: e.clientY};  // div is fixed, so use clientX/Y
        
            
        //ポインタがメモリア内にあるか判定
        numberRow = -1;
        numberCol = -1;
        let len = 60;
        let clickX = pos.x;
        let clickY = pos.y;
        loop: for (let i = 0; i < y.length; i++){
            for (let j = 0; j < x.length; j++) {
                if ((clickX < x[j] + len / 2) && (x[j] - len / 2 < clickX) && (clickY < y[i] + len / 2) && (y[i] - len / 2 < clickY)) {
                    numberRow = i;
                    numberCol = j;
                    break loop;
                }
            }
        }
        if (!visible &&
            pos.x >= region.x && pos.x < region.x + region.w &&
            pos.y >= region.y && pos.y < region.y + region.h) {
            
            if ((numberRow !== -1) && (numberCol !== -1)) {
                let obj = mData.tarjetas[numberRow * x.length + numberCol];
                let efecto = [obj.efecto1, obj.efecto2, obj.efecto3, obj.efecto4];
                let text = "「" + obj.name + "」";
                if ((obj.exclusive !== "")&&(obj.exclusive !== null))
                    text += "<br/>" + obj.exclusive + "専用";
                for (let k = 0; k < 4; k++) {
                    if (efecto[k].name !== "") {
                        if ((totu === "1") || (totu === "0" && obj.ascend === "max")) {
                            if ((efecto[k].valueMax.indexOf("必ず") !== -1) && (efecto[k].name.indexOf("必ず") !== -1))
                                text += "<br/>" + efecto[k].name;
                            else if (efecto[k].valueMax !== "必ず") {
                                if(efecto[k].valueMax !== "")
                                    text += "<br/>" + efecto[k].name + "[" + efecto[k].valueMax + "]";
                                else
                                    text += "<br/>" + efecto[k].name;
                            }
                            else if(efecto[k].valueMax === "")
                                text += "<br/>" + efecto[k].name;
                            else
                                text += "<br/>" + efecto[k].name + " " + efecto[k].valueMax;
                        }
                        else {
                            if ((efecto[k].value.indexOf("必ず") !== -1) && (efecto[k].name.indexOf("必ず") !== -1))
                                text += "<br/>" + efecto[k].name;
                            else if (efecto[k].value === "無")
                                continue;
                            else if (efecto[k].value !== "必ず") {
                                if (efecto[k].value !== "")
                                    text += "<br/>" + efecto[k].name + "[" + efecto[k].value + "]";
                                else
                                    text += "<br/>" + efecto[k].name;
                            }
                            else if (efecto[k].value === "")
                                text += "<br/>" + efecto[k].name;
                            else                                
                                text += "<br/>" + efecto[k].name + " " + efecto[k].value;
                        }
                        if (obj.type === "スキル") {
                            text += " " + efecto[k].target + " " + efecto[k].turn;
                        }
                        else if (efecto[k].target !== "") {
                            text += " " + efecto[k].target;
                        }
                        //おまけ
                        if (efecto[k].name === "自分のDiscドロー") {
                            for (let m = 0; m < jsonDataOri.personas.length; m++){
                                if (jsonDataOri.personas[m].name === obj.exclusive)
                                    text += "(" + jsonDataOri.personas[m].Disk + ")";
                            }
                        }
                    }
                    
                        
                }
                div.innerHTML = text;
                me.show(posAbs);                          // show tool-tip at this pos
            }
        }
        else if ((numberRow === -1) && (numberCol === -1))
            hide();
        else if ((clickX > canvas.width) || (clickX < 0) || (clickY < 0) || (clickY > canvas.heigth))
            hide();
        else {
            setDivPos(posAbs);                     // otherwise, update position
        }
    }
    
    // get mouse position relative to canvas
    function getPos(e) {
      var r = canvas.getBoundingClientRect();
      return {x: e.clientX - r.left, y: e.clientY - r.top}
    }
    
    // update and adjust div position if needed (anchor to a different corner etc.)
    function setDivPos(pos) {
      if (visible){
        if (pos.x < 0) pos.x = 0;
        if (pos.y < 0) pos.y = 0;
        // other bound checks here
        if (numberCol === x.length - 1)
            div.style.left = x[x.length-1] + "px";
        else
            div.style.left = pos.x + "px";
        div.style.top = pos.y + "px";
      }
    }
    
    // we need to use shared event handlers:
    canvas.addEventListener("mousemove", check);
    // canvas.addEventListener("click", check);
    canvas.addEventListener("mouseout", hide);
    
}

// The Tool-Tip instance:
function ToolTipModal(canvas, region, width,x,y) {

    var me = this,                                // self-reference for event handlers
        div = document.createElement("div"),      // the tool-tip div
        parent = canvas.parentNode,               // parent node for canvas
        visible = false;                          // current status
    
    // set some initial styles, can be replaced by class-name etc.
    div.style.cssText = "position:fixed;padding:7px;background:gold;pointer-events:none;width:" + width + "px;z-index:30";
    
    var numAtk;
    var numMemoria;

    // show the tool-tip
    this.show = function(pos) {
      if (!visible) {                             // ignore if already shown (or reset time)
        visible = true;                           // lock so it's only shown once
        setDivPos(pos);                           // set position
        parent.appendChild(div);                  // add to parent of canvas
        // setTimeout(hide, timeout);                // timeout for hide
      }
    }
    
    // hide the tool-tip
    function hide() {
      visible = false;                            // hide it after timeout
      parent.removeChild(div);                    // remove from DOM
    }
  
    // check mouse position, add limits as wanted... just for example:
    function check(e) {
      var pos = getPos(e),
          posAbs = {x: e.clientX, y: e.clientY, lx:e.layerX,ly:e.layerY};  // div is fixed, so use clientX/Y
        
            
        //ポインタがメモリア内にあるか判定
        numAtk = -1;
        numMemoria = -1;
        let len = 36;
        let xOffset = 0;
        let margin = 5;
        let clickX = pos.x;
        let clickY = pos.y;
        loop: for (let i = 0; i < 3; i++){
            for (let j = 0; j < 4; j++) {
                if ((clickX < x[j] + len / 2 + xOffset+margin) && (x[j] - len / 2 + xOffset+margin< clickX) && (clickY < y[i] + len / 2) && (y[i] - len / 2 < clickY)) {
                    numAtk = i;
                    numMemoria = j;
                    break loop;
                }
            }
            xOffset += 170;
        }
        if (!visible &&
            pos.x >= region.x && pos.x < region.x + region.w &&
            pos.y >= region.y && pos.y < region.y + region.h) {
            
            if ((numAtk !== -1) && (numMemoria !== -1) && (typeof memoriaElegido[numAtk][numMemoria] !== "undefined")) {
                let obj = memoriaElegido[numAtk][numMemoria];
                let efecto = [obj.efecto1, obj.efecto2, obj.efecto3, obj.efecto4];
                let text = "「" + obj.name + "」";
                let totu = memoriaElegido[numAtk][numMemoria].totu === true ? "1" : "0";
                if ((obj.exclusive !== "")&&(obj.exclusive !== null))
                    text += "<br/>" + obj.exclusive + "専用";
                for (let k = 0; k < 4; k++) {
                    if (efecto[k].name !== "") {
                        if ((totu === "1") || (totu === "0" && obj.ascend === "max")) {
                            if ((efecto[k].valueMax.indexOf("必ず") !== -1) && (efecto[k].name.indexOf("必ず") !== -1))
                                text += "<br/>" + efecto[k].name;
                            else if (efecto[k].valueMax !== "必ず") {
                                if(efecto[k].valueMax !== "")
                                    text += "<br/>" + efecto[k].name + "[" + efecto[k].valueMax + "]";
                                else
                                    text += "<br/>" + efecto[k].name;
                            }
                            else if(efecto[k].valueMax === "")
                                text += "<br/>" + efecto[k].name;
                            else
                                text += "<br/>" + efecto[k].name + " " + efecto[k].valueMax;
                        }
                        else {
                            if ((efecto[k].value.indexOf("必ず") !== -1) && (efecto[k].name.indexOf("必ず") !== -1))
                                text += "<br/>" + efecto[k].name;
                            else if (efecto[k].value === "無")
                                continue;
                            else if (efecto[k].value !== "必ず") {
                                if (efecto[k].value !== "")
                                    text += "<br/>" + efecto[k].name + "[" + efecto[k].value + "]";
                                else
                                    text += "<br/>" + efecto[k].name;
                            }
                            else if (efecto[k].value === "")
                                text += "<br/>" + efecto[k].name;
                            else                                
                                text += "<br/>" + efecto[k].name + " " + efecto[k].value;
                        }
                        if (obj.type === "スキル") {
                            text += " " + efecto[k].target + " " + efecto[k].turn;
                        }
                        else if (efecto[k].target !== "") {
                            text += " " + efecto[k].target;
                        }
                        //おまけ
                        if (efecto[k].name === "自分のDiscドロー") {
                            for (let m = 0; m < jsonDataOri.personas.length; m++){
                                if (jsonDataOri.personas[m].name === obj.exclusive)
                                    text += "(" + jsonDataOri.personas[m].Disk + ")";
                            }
                        }
                    }
                    
                        
                }
                div.innerHTML = text;
                me.show(posAbs);                          // show tool-tip at this pos
            }
            else{
                setDivPos(posAbs);                     // otherwise, update position
            }
        }
        else if ((numAtk === -1) && (numMemoria === -1))
            hide();
        else if ((clickX > canvas.width) || (clickX < 0) || (clickY < 0) || (clickY > canvas.heigth))
            hide();
        else {
            setDivPos(posAbs);                     // otherwise, update position
        }
    }
    
    // get mouse position relative to canvas
    function getPos(e) {
      var r = canvas.getBoundingClientRect();
      return {x: e.clientX - r.left, y: e.clientY - r.top}
    }
    
    // update and adjust div position if needed (anchor to a different corner etc.)
    function setDivPos(pos) {
      if (visible){
        if (pos.x < 0) pos.x = 0;
        if (pos.y < 0) pos.y = 0;
        // other bound checks here
        div.style.left = pos.x -canvas.getBoundingClientRect().left + "px";
        div.style.top = pos.y + "px";
      }
    }
    
    // we need to use shared event handlers:
    canvas.addEventListener("mousemove", check);
    // canvas.addEventListener("click", check);
    canvas.addEventListener("mouseout", hide);
    
  }

function draw6(selector) {
    var canvas6;
    var w = $(window).width();
    var wSize = 768;
    
    switch (selector)
    {
        case 1:
            {
                canvas6 = document.getElementById("canvas61");
                break;
            }
        case 2:
            {
                canvas6 = document.getElementById("canvas62");
                break;
            }
        case 3:
            {
                canvas6 = document.getElementById("canvas63");
                break;
            }
    }

    // if (w < wSize) {
    //     //画面サイズが768px未満のときの処理
    //     canvas5 = document.getElementById("canvas5");
    //     canvasFlagM = 1;
    // }
    // else {
    //     canvas5 = document.getElementById("canvas51");
    //     canvasFlagM = 0;
    // }
    if (!canvas6 || !canvas6.getContext) {
        return;
    }

    //設定
    var ctx6 = canvas6.getContext("2d");

    //この中に選択されたデータを表示していく
    //初期状態としては、キャラ選択無し、メモリア選択無しを表現したい

  }

/////////////////////////
//精神強化
////////////////////////

//プログラム指針
// BusquedaMskill,Mabiにてvalue取得
// キャラ選択時、精神強化による補正値を専用配列に入力

//ダメージ計算に関係ある系
// 0:攻撃力UP
// 1:与えるダメージUP
// 2:Charge後ダメージUP
// 3:BlastダメージUP
// 4:敵状態異常時ダメージUP
// 5:防御力無視
// 6:防御力DOWN
// 7:ダメージアップ状態
// 8:HP最大時攻撃力UP  まだダメ
// 9:瀕死時攻撃力UP    まだダメ
// 19:ChargeディスクダメージUP

//ダメージカット無視
//瀕死時攻撃力UPの扱い
//
//ダメージ計算に関係無い系
// 10:AccelMPUP
// 11:MP獲得量UP
// 12:MP回復
// 13:クリティカル    まだダメ
// 14:MP100以上時MP獲得量UP
// 15:Blast攻撃時MP獲得
//
//スタン,霧,魅了,毒,呪い,やけど,暗闇,拘束

//ステータス補正
// 20:HP増加
// 21:ATK増加
// 22:Def増加
// 23:A覚醒補正
// 24:B覚醒補正
// 25:C覚醒補正
// 26:属性（補正じゃないけどおまけ

//追加　マギア補正
// 30:マギア
// 31:マギアによる攻撃バフ
// 35:ドッペル
// 36:ドッペルによるバフ

function GenerateMentalEfecto() {
    //効果値配列初期化
    mentalEfectoA.length = 0;
    mentalEfectoA = new Array(3);
    mentalEfectoS.length = 0;
    mentalEfectoS = new Array(3);
    for (let i = 0; i < 3;i++){
        mentalEfectoA[i] = new Array(40);
        mentalEfectoS[i] = new Array(40);
    }

    //選択されたキャラの名前部分抜き出し
    var charaSelected = [document.getElementById("MainContent_seleccionado_0").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_1").nextSibling.innerText,
        document.getElementById("MainContent_seleccionado_2").nextSibling.innerText]
    for (let i = 0; i < 3; i++)
        charaSelected[i] = charaSelected[i].substring(charaSelected[i].indexOf(":") + 2);

    
    //キャラ選択状態により場合分け
    var loopMax = 3;
    //1人で3回攻撃の場合ループ1回
    if ($('input[name="ctl00$MainContent$estadoAtk"]:checked').val() === "1")
        loopMax = 1;

    for(let i = 0 ; i<loopMax;i++){
        //名前からキャラ特定　画面サイズにより略称の場合もある
        for(let j = 0; j<jsonData.personas.length;j++){
            if ((jsonData.personas[j].name === charaSelected[i]) || (jsonData.personas[j].nickName === charaSelected[i])) {
                //マギア取得
                mentalEfectoA[i][30] = { "type1":jsonData.personas[j].Magia1.name,"type2":jsonData.personas[j].Magia1.info1,"efecto":jsonData.personas[j].Magia1.info2};
                
                //属性取得
                mentalEfectoA[i][26] = jsonData.personas[j].Attribute;

                //精神強化値が入力されていない場合スルー
                if (typeof jsonData.personas[j].mSelect === "undefined") {
                    continue;
                }
                //キャラデータ取得
                //精神ステータス
                mentalEfectoA[i][20] = jsonData.personas[j].menteHP;
                mentalEfectoA[i][21] = jsonData.personas[j].menteATK;
                mentalEfectoA[i][22] = jsonData.personas[j].menteDEF;
                mentalEfectoA[i][23] = jsonData.personas[j].Aup;
                mentalEfectoA[i][24] = jsonData.personas[j].Bup;
                mentalEfectoA[i][25] = jsonData.personas[j].Cup;
                
                //精神アビ
                if (BusquedaMabi("攻撃力UP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("攻撃力UP", jsonData.personas[j]);
                    if (resultado[0]) {
                        mentalEfectoA[i][0] = resultado[2] * 5;//数字
                        // value.subValue = resultado[1];//テキスト
                    }
                }
                if (BusquedaMabi("ダメージUP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("ダメージUP", jsonData.personas[j]);
                    if (resultado[0]) {
                        mentalEfectoA[i][1] = resultado[2] * 5;//数字
                        // value.subValue = resultado[1];//テキスト
                    }
                }
                if (BusquedaMabi("Charge後ダメUP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("Charge後ダメUP", jsonData.personas[j]);
                    if (resultado[0]) {
                        //Ⅱの確率が計算に合わないため、数値分解して算出する
                        let value = resultado[1].split(",");
                        mentalEfectoA[i][2] = 0;
                        for (let k = 0; k < value.length; k++){
                            switch(value[k]){
                                case "Ⅱ":
                                {
                                    mentalEfectoA[i][2] += 16.5;
                                    break;
                                }
                                case "Ⅲ":
                                case "Ⅳ":
                                {
                                    mentalEfectoA[i][2] += ChangeRoman(value[k]) * 5 + 5;
                                    break;
                                }
                            }
                        }
                    }
                }
                
                if (BusquedaMabi("BlastダメUP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("BlastダメUP", jsonData.personas[j]);
                    if (resultado[0]) {
                        //Ⅱの確率が計算に合わないため、数値分解して算出する
                        let value = resultado[1].split(",");
                        mentalEfectoA[i][3] = 0;
                        for (let k = 0; k < value.length; k++){
                            switch(value[k]){
                                case "Ⅱ":
                                {
                                    mentalEfectoA[i][3] += 16.5;
                                    break;
                                }
                                case "Ⅲ":
                                case "Ⅳ":
                                {
                                    mentalEfectoA[i][3] += ChangeRoman(value[k]) * 5 + 5;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (BusquedaMabi("状態異常時ダメUP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("状態異常時ダメUP", jsonData.personas[j]);
                    if (resultado[0]) {
                        //現状Ⅱしかないので計算は仮
                        mentalEfectoA[i][4] = resultado[2] * 10;//数字
                    }
                }
                if (BusquedaMabi("防御無視", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("防御無視", jsonData.personas[j]);
                    if (resultado[0]) {
                        mentalEfectoA[i][5] = resultado[2] * 5 + 5;//数字
                    }
                }

                //防御力DOWNは精神スキルのみなので今はコーディングしない

                if (BusquedaMabi("ダメUP状態", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("ダメUP状態", jsonData.personas[j]);
                    if (resultado[0]) {
                        //現状Ⅱしかないので計算は仮
                        mentalEfectoA[i][7] = resultado[2] * 10;//数字
                    }
                }

                if (BusquedaMabi("Charge板ダメUP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("Charge板ダメUP", jsonData.personas[j]);
                    if (resultado[0]) {
                        mentalEfectoA[i][19] = resultado[2] * 5;//数字
                        // value.subValue = resultado[1];//テキスト
                    }
                }

                //MP関係
                if (BusquedaMabi("AcceleMPUP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("AcceleMPUP", jsonData.personas[j]);
                    if (resultado[0]) {
                        mentalEfectoA[i][10] = resultado[2] * 2.5 + resultado[3] * 7.5;//数字
                    }
                }
                if (BusquedaMabi("MP獲得量UP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("MP獲得量UP", jsonData.personas[j]);
                    if (resultado[0]) {//明日香だけⅤで20%なのが合わない
                        //明日香のⅤの確率が計算に合わないため、数値分解して算出する
                        let value = resultado[1].split(",");
                        mentalEfectoA[i][11] = 0;
                        for (let k = 0; k < value.length; k++){
                            switch(value[k]){
                                case "Ⅴ":
                                {
                                    mentalEfectoA[i][11] += 20;
                                    break;
                                }
                                case "Ⅰ":
                                case "Ⅱ":
                                case "Ⅲ":
                                case "Ⅳ":
                                {
                                    mentalEfectoA[i][11] += ChangeRoman(value[k]) * 2.5 + 2.5;//数字
                                    break;
                                }
                            }
                        }
                    }
                }
                if (BusquedaMabi("MP100以上時MP獲得量UP", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("MP100以上時MP獲得量UP", jsonData.personas[j]);
                    if (resultado[0]) {
                        //Ⅲの確率が計算に合わないため、数値分解して算出する
                        let value = resultado[1].split(",");
                        mentalEfectoA[i][14] = 0;
                        for (let k = 0; k < value.length; k++){
                            switch(value[k]){
                                case "Ⅲ":
                                {
                                    mentalEfectoA[i][14] += 22;
                                    break;
                                }
                                case "Ⅳ":
                                {
                                    mentalEfectoA[i][14] += 24;
                                    break;
                                }
                                case "Ⅰ":
                                case "Ⅱ":
                                {
                                    mentalEfectoA[i][14] += ChangeRoman(value[k]) * 5 + 10;//数字
                                    break;
                                }
                            }
                        }
                    }
                }
                if (BusquedaMabi("Blast攻撃時MP獲得", jsonData.personas[j])) {
                    let resultado = BusquedaMabi("Blast攻撃時MP獲得", jsonData.personas[j]);
                    if (resultado[0]) {
                        //Ⅲの確率が計算に合わないため、数値分解して算出する
                        let value = resultado[1].split(",");
                        mentalEfectoA[i][15] = 0;
                        for (let k = 0; k < value.length; k++){
                            switch(value[k]){
                                case "Ⅱ":
                                {
                                    mentalEfectoA[i][15] += 2;
                                    break;
                                }
                                case "Ⅲ":
                                {
                                    mentalEfectoA[i][15] += 3;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
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
    piedraUnidad = piedraUnidad < 0 ? 0 : piedraUnidad;
    var kakin = (piedraUnidad + prikoneCuenta) * billete;
    //あまり分算出
    var sobraFinal = Math.floor(piedraNecesitada % piedra);
    if (sobraFinal <= 0) {
        //プリコネカウントを見直して再計算
        piedraNecesitada = piedraNecesitadaOri - prikoneCuenta * 7500;
        sobraFinal = Math.floor(piedraNecesitada % piedra);
    }




    //最終結果表示
    if (juego == "プリコネ") {
        if ((piedraNecesitada < 0) && (!((piedraNecesitadaOri >= 5000) && (cuenta7500 == 3)))) {
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

//ミラーズpt計算
//

function CalcMirrors() {
    let turn = $('input[name="ctl00$MainContent$Mturn"]:checked').val();
    let connect = $('input[name="ctl00$MainContent$Mconnect"]:checked').val();
    let magia = $('input[name="ctl00$MainContent$Mmagia"]:checked').val();
    let doppel = $('input[name="ctl00$MainContent$Mdoppel"]:checked').val();
    let skill = $('input[name="ctl00$MainContent$Mskill"]:checked').val();
    let mhp = $('input[name="ctl00$MainContent$Mhp"]:checked').val();
    // let hp2 = $("#Mhp2").val();
    // let hp3 = $("#Mhp3").val();
    // let hp4 = $("#Mhp4").val();
    // let hp5 = $("#Mhp5").val();
    let pt = $('input[name="ctl00$MainContent$Mpt"]:checked').val();
    let death = $('input[name="ctl00$MainContent$Mdeath"]:checked').val();
    let bonus = $('input[name="ctl00$MainContent$Mbonus"]:checked').val();
    let breakpt = $('input[name="ctl00$MainContent$Mbreak"]:checked').val();

    let calcT = 0;
    //ポイント計算
    switch(turn){
        case "1T":
            {
                // calcT = 1.85;//2.45?
                calcT = 2.45;
                break;
            }
        case "2T":
            {
                // calcT = 1.75;//2.25?
                calcT = 2.45;
                break;
            }
        case "3T":
            {
                // calcT = 1.55;//2.05?
                calcT = 2.25;
                break;
        }
        case "4T":
            {
                calcT = 1.65;//変更なし
                break;
            }
        case "5T":
            {
                calcT = 1.35;//変更なし
                break;
            }
    }

    let calcC = 0;
    if (connect !== "0回")
        calcC = Number(connect.substr(0, 1)) * 0.2;
    let calcM = 0;
    let magiaTable = [0.16, 0.06, 0.05, 0.04, 0.03, 0.02];
    for (let i = 0; i < Number(magia.substr(0, 1)); i++){
        calcM += magiaTable[i];
    }

    let calcDoppel = 0;
    let doppelTable = [0.3, 0.1, 0.09, 0.05, 0.03, 0.02];
    for (let i = 0; i < Number(doppel.substr(0, 1)); i++){
        calcDoppel += doppelTable[i];
    }
    let calcS = 0;
    if (skill !== "0回")
        calcS = Number(skill.substr(0, 1)) * 0.05;
    let calcH = 0;
    switch(mhp){
        case "1"://100-80
            {
                calcH = 0;
                break;
            }
        case "2"://79-70
            {
                calcH = -0.05;
                break;
            }
        case "3"://69-60
            {
                calcH = -0.1;
                break;
            }
        case "4"://59-50
            {
                calcH = -0.15;
                break;
            }
        case "5"://49-0
            {
                calcH = -0.2;
                break;
            }
    }
    let deathPt = 0;
    switch (pt) {
        case "2人PT":
            {
                deathPt = -0.08;
                break;
            }
        case "3人PT":
            {
                deathPt = -0.05;
                break;
            }
        case "4人PT":
            {
                deathPt = -0.04;
                break;
            }
        case "5人PT":
            {
                deathPt = -0.03;
                break;
            }
    }
    let calcD = deathPt * death.substr(0,1);
    let calcB = Number(bonus);
    let calcBp = Number(breakpt);

    let calcFinal = (calcT + calcC + calcM + calcDoppel + calcS + calcH + calcD) * calcB * calcBp;
    calcFinal = Math.floor(calcFinal * 1000);
    // calcFinal *= 10;

    //表示用
    calcT = Math.floor(calcT * 1000);
    calcC = Math.floor(calcC * 1000);
    calcM = Math.floor(calcM * 1000);
    calcDoppel = Math.floor(calcDoppel * 1000);
    calcS = Math.floor(calcS * 1000);
    calcH = Math.ceil(calcH * 1000);
    calcD = Math.ceil(calcD * 1000);

    //表示
    let Mtotal = document.getElementById("MainContent_Mtotal");
    let Mturn = document.getElementById("MainContent_MturnL");
    let Mconnect = document.getElementById("MainContent_MconnectL");
    let Mmagia = document.getElementById("MainContent_MmagiaL");
    let Mdoppel = document.getElementById("MainContent_MdoppelL");
    let Mskill = document.getElementById("MainContent_MskillL");
    let Mhp = document.getElementById("MainContent_MhpL");
    let Mdeath = document.getElementById("MainContent_Msurvive");



    Mtotal.innerText = "あなたのポイントは " + calcFinal + "ptです";
    Mturn.innerText = "戦闘ターン数 " + calcT + "pt";
    Mconnect.innerText = "コネクト数 " + calcC + "pt";
    Mmagia.innerText = "マギア発動回数 " + calcM + "pt";
    Mdoppel.innerText = "ドッペル発動回数 " + calcDoppel + "pt";
    Mskill.innerText = "スキル回数 " + calcS + "pt";
    Mhp.innerText = "残HPボーナス " + calcH + "pt";
    Mdeath.innerText = "生存人数 " + calcD + "pt";        

}

window.onload = function () {
    let str = location.href;//ページ名取得
    if (str.match("/Magia_damage_calc")) {
        //初期設定
        document.getElementById("MainContent_RadioButtonList666_0").nextSibling.classList.add("accele");
        document.getElementById("MainContent_RadioButtonList666_1").nextSibling.classList.add("blast");
        document.getElementById("MainContent_RadioButtonList666_2").nextSibling.classList.add("charge");

        document.getElementById("MainContent_RadioButtonList667_0").nextSibling.classList.add("accele");
        document.getElementById("MainContent_RadioButtonList667_1").nextSibling.classList.add("blast");
        document.getElementById("MainContent_RadioButtonList667_2").nextSibling.classList.add("charge");

        document.getElementById("MainContent_RadioButtonList668_0").nextSibling.classList.add("accele");
        document.getElementById("MainContent_RadioButtonList668_1").nextSibling.classList.add("blast");
        document.getElementById("MainContent_RadioButtonList668_2").nextSibling.classList.add("charge");

        document.getElementById("MainContent_RadioButtonList669_0").nextSibling.classList.add("accele");
        document.getElementById("MainContent_RadioButtonList669_1").nextSibling.classList.add("blast");
        document.getElementById("MainContent_RadioButtonList669_2").nextSibling.classList.add("charge");
        $('input[name="ctl00$MainContent$Mturn"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mconnect"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mskill"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mmagia"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mdoppel"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mhp"]:radio').change(function () {
            CalcMirrors();
        });
        // $('input[name="Mhp1"]').change(function () {
        //     CalcMirrors();
        // });
        // $('input[name="Mhp2"]').change(function () {
        //     CalcMirrors();
        // });
        // $('input[name="Mhp3"]').change(function () {
        //     CalcMirrors();
        // });
        // $('input[name="Mhp4"]').change(function () {
        //     CalcMirrors();
        // });
        // $('input[name="Mhp5"]').change(function () {
        //     CalcMirrors();
        // });
        $('input[name="ctl00$MainContent$Mpt"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mdeath"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mbonus"]:radio').change(function () {
            CalcMirrors();
        });
        $('input[name="ctl00$MainContent$Mbreak"]:radio').change(function () {
            CalcMirrors();
        });
    }
}