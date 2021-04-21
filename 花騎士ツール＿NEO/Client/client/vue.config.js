module.exports = {
    //Web Apiと組み合わせて動作確認用と、vue単体で動作確認用
    publicPath: process.env.NODE_ENV === 'production'
        ? './Static/'
        : './',
        //?'./Static/uma/'
        //: './uma/',
    //ビルド出力先
    outputDir: '../../Static',
    //都度出力ファイル名を変えないように
    filenameHashing: false,
    // build: {
    //     assetsPublicPath: './uma',
    //     assetsSubDirectory:'static'
    // },

    configureWebpack: {
        devtool: 'source-map'
    }, pages: {
        index: {
            entry: 'src/main.js', //必須パラメータ
            // title: 'ウマ娘評価値計算', //ページタイトル
        }
    }
}