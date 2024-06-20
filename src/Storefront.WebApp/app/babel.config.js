/* eslint-env node */
module.exports = {
  presets: [
    '@quasar/babel-preset-app'
  ],
  // Отключить console.log() в продакшене, нужно доустановить плагин
  // Комманда: npm i babel-plugin-transform-remove-console --save-dev 
  //   env: { 
  //     "production": {
  //         "plugins": ["transform-remove-console"]
  //     }
  //  }
}
